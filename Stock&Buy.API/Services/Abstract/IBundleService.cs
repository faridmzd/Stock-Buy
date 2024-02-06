using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Bundles;

namespace Stock_Buy.API.Services.Abstract
{
    public interface IBundleService : IBaseService<Bundle>
    {
        Task<int> GetMaxProductionAmountAsync(Guid id);
        Task AddAssociatePartsAsync(Guid bundleId, List<AssociatedPart> associatedParts);
        Task AddAssociateBundlesAsync(Guid bundleId, List<AssociatedBundle> associatedBundles);
        Task UpdateAssociatedBundleAsync(AssociatedBundle associatedBundle);
        Task UpdateAssociatedPartAsync(AssociatedPart associatedPart);

    }
}
