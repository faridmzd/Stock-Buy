using Microsoft.EntityFrameworkCore;
using Stock_Buy.API.Data;
using Stock_Buy.API.Domain;
using Stock_Buy.API.ExceptionHandling;
using Stock_Buy.API.Services.Abstract;

namespace Stock_Buy.API.Services.Concrete
{
    public class PartService : BaseService<Part>, IPartService
    {
        public PartService(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Part> AddAsync(Part part)
        {
            var partWithSameNameExists = await _entities.AnyAsync(x => x.Name == part.Name);

            DuplicateEntityException.ThrowIfTrue(partWithSameNameExists, nameof(part.Name), nameof(Part));

            return await base.AddAsync(part);
        }
    }
}
