using Microsoft.EntityFrameworkCore;
using Stock_Buy.API.Data;
using Stock_Buy.API.Domain;
using Stock_Buy.API.ExceptionHandling;
using Stock_Buy.API.Services.Abstract;
using static Stock_Buy.API.Contracts.V1.ApiRoutes;

namespace Stock_Buy.API.Services.Concrete
{
    public class BundleService : BaseService<Bundle>, IBundleService
    {
        public BundleService(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Bundle> AddAsync(Bundle bundle)
        {
            var bundleWithSameNameExists = await _entities.AnyAsync(x => x.Name == bundle.Name);

            DuplicateEntityException.ThrowIfTrue(bundleWithSameNameExists, nameof(bundle.Name), nameof(Bundle));

            return await base.AddAsync(bundle);
        }

        public async Task AddAssociateBundlesAsync(Guid bundleId, List<AssociatedBundle> associatedBundles)
        {
            await EnsureEntityWithGivenIdExistsAsync(bundleId);

            await _dbContext.AssociatedBundles.AddRangeAsync(associatedBundles);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAssociatePartsAsync(Guid bundleId, List<AssociatedPart> associatedParts)
        {
            await EnsureEntityWithGivenIdExistsAsync(bundleId);

            await _dbContext.AssociatedParts.AddRangeAsync(associatedParts);
            await _dbContext.SaveChangesAsync();
        }

        public override async Task<Bundle> GetByIdAsync(Guid id)
        {
            var bundle = await base.GetByIdAsync(id);

            return await AssignAssociatedProductsToBundleAsync(bundle);

            #region Local functions

            async Task<Bundle> AssignAssociatedProductsToBundleAsync(Bundle bundle)
            {
                bundle.AssociatedParts = await _dbContext.AssociatedParts
                  .Where(x => x.BundleId == bundle.Id)
                  .Include(x => x.Part)
                  .Select(x => (Part)x)
                  .ToListAsync() ?? new();

                bundle.AssociatedBundles = await _dbContext.AssociatedBundles
                .Where(x => x.ParentBundleId == bundle.Id)
                .Include(x => x.ChildBundle)
                .Select(x => (Bundle)x)
                .ToListAsync() ?? new();

                for (var i = 0; i < bundle.AssociatedBundles.Count; i++)
                {
                    var quantityNeeded = bundle.AssociatedBundles[i].QuantityNeeded;
                    bundle.AssociatedBundles[i] = await AssignAssociatedProductsToBundleAsync(bundle.AssociatedBundles[i]);
                    bundle.AssociatedBundles[i].QuantityNeeded = quantityNeeded;
                }
                return bundle;
            }

            #endregion
        }

        public async Task<int> GetMaxProductionAmountAsync(Guid id)
        {
            await EnsureEntityWithGivenIdExistsAsync(id);

            return await CalculateMaxProductionAmountAsync(id);

            #region Local functions

            async Task<int> CalculateMaxProductionAmountAsync(Guid id)
            {
                var associatedPartsWithStockAndNeededQuantities = await _dbContext.AssociatedParts
                    .Where(x => x.BundleId == id)
                    .Select(x => new { StockQuantity = x.Part.StockQuantity, NeededQuantity = x.QuantityNeeded })
                    .ToListAsync();

                var associatedBundlesWithIdsAndNeededQuantities = await _dbContext.AssociatedBundles
                    .Where(x => x.ParentBundleId == id)
                    .Select(x => new { Id = x.ChildBundleId, NeededQuantity = x.QuantityNeeded })
                    .ToListAsync();

                int? maxProductionForParts = associatedPartsWithStockAndNeededQuantities.Any()
                    ? associatedPartsWithStockAndNeededQuantities.Min(x => x.StockQuantity / x.NeededQuantity)
                    : null;

                var productionsForBundlesTasks = associatedBundlesWithIdsAndNeededQuantities
                    .Select(async x => await CalculateMaxProductionAmountAsync(x.Id) / x.NeededQuantity)
                    .ToList();

                var productionsForBundles = await Task.WhenAll(productionsForBundlesTasks);

                int? maxProductionForBundles = productionsForBundles.Any()
                    ? productionsForBundles.Min()
                    : null;

                var maxProduction = maxProductionForParts == null || maxProductionForBundles == null
                    ? Math.Max(maxProductionForParts ?? 0, maxProductionForBundles ?? 0)
                    : Math.Min((int)maxProductionForParts, (int)maxProductionForBundles);

                return maxProduction;
            }

            #endregion
        }

        public async Task UpdateAssociatedBundleAsync(AssociatedBundle associatedBundle)
        {
            var parentBundle = await _dbContext.Bundles.FirstOrDefaultAsync(x => x.Id == associatedBundle.ParentBundleId);
            NotFoundException.ThrowIfNull(parentBundle, associatedBundle.ParentBundleId, nameof(Bundle));

            var childBundle = await _dbContext.Bundles.FirstOrDefaultAsync(x => x.Id == associatedBundle.ChildBundleId);
            NotFoundException.ThrowIfNull(parentBundle, associatedBundle.ChildBundleId, nameof(Bundle));

            var relation = await _dbContext.AssociatedBundles
                .FirstOrDefaultAsync(
                x => x.ParentBundleId == associatedBundle.ParentBundleId && x.ChildBundleId == associatedBundle.ChildBundleId);

            if (relation is null)
            {
                throw new NotFoundException($"No association found with bundle {parentBundle!.Name} and bundle {childBundle!.Name} !");
            }
            else
            {
                relation.QuantityNeeded = associatedBundle.QuantityNeeded;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAssociatedPartAsync(AssociatedPart associatedPart)
        {
            var bundle = await _dbContext.Bundles.FirstOrDefaultAsync(x => x.Id == associatedPart.BundleId);
            NotFoundException.ThrowIfNull(bundle, associatedPart.BundleId, nameof(Bundle));

            var part = await _dbContext.Parts.FirstOrDefaultAsync(x => x.Id == associatedPart.PartId);
            NotFoundException.ThrowIfNull(part, associatedPart.PartId, nameof(Part));

            var relation = await _dbContext.AssociatedParts
                .FirstOrDefaultAsync(
                x => x.BundleId == associatedPart.BundleId && x.PartId == associatedPart.PartId);

            if (relation is null)
            {
                throw new NotFoundException($"No association found with bundle {bundle!.Name} and part {part!.Name} !");
            }
            else
            {
                relation.QuantityNeeded = associatedPart.QuantityNeeded;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
