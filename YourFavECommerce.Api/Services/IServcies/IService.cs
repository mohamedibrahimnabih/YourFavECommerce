using System.Linq.Expressions;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Services.IServcies
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);
        T? GetOne(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null, bool tracked = true);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        bool Remove(int id, CancellationToken cancellationToken = default);

        Task<bool> CommitAsync();
    }
}
