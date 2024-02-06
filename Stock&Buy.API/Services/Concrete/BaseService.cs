using Microsoft.EntityFrameworkCore;
using Stock_Buy.API.Data;
using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Pagination;
using Stock_Buy.API.ExceptionHandling;
using Stock_Buy.API.Services.Abstract;
using System.Linq.Expressions;

namespace Stock_Buy.API.Services.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _entities;
        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _entities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var itemDeleted = await _entities.Where(b => b.Id == id).ExecuteDeleteAsync() >= 1;

            NotFoundException.ThrowIfFalse(itemDeleted, id, nameof(T));
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync() ?? new();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity =  await _entities.FirstOrDefaultAsync(x => x.Id == id);
            NotFoundException.ThrowIfNull(entity, id, nameof(T));
            return entity!;
        }

        public virtual async Task EnsureEntityWithGivenIdExistsAsync(Guid id)
        {
            NotFoundException.ThrowIfFalse(await _entities.AnyAsync(x => x.Id == id), id, nameof(T));
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResponse<TResult>> GetPaginatedAsync<TResult>(PaginationFilter filter, Expression<Func<T, TResult>> projection)
        {
            ArgumentNullException.ThrowIfNull(nameof(filter));
            ArgumentNullException.ThrowIfNull(nameof(projection));

            IQueryable<T> query = _entities
                  .OrderBy(e => e.Id)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .AsNoTracking();

            var entities = await query.Select(projection).ToListAsync() ?? new List<TResult>();

            var totalCount = await _entities.CountAsync();

            return PagedResponse<TResult>.Create(entities, filter.PageNumber, filter.PageSize, totalCount);
        }
    }
}
