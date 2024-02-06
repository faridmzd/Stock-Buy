using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Pagination;
using Stock_Buy.API.Services.Concrete;
using System.Linq.Expressions;

namespace Stock_Buy.API.Services.Abstract
{
    public interface IBaseService<T> where T : Entity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task EnsureEntityWithGivenIdExistsAsync(Guid id);
        Task<PagedResponse<TResult>> GetPaginatedAsync<TResult>(PaginationFilter filter, Expression<Func<T, TResult>> projection);
    }
}
