using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

namespace YourFavECommerce.Api.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Service(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public T? GetOne(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {

            return GetAsync(expression, includes, tracked).GetAwaiter().GetResult().FirstOrDefault();

        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {

            IQueryable<T> entities = _dbSet;

            if(expression is not null)
            {
                entities = entities.Where(expression);
            }

            if(includes is not null)
            {
                foreach (var item in includes)
                {
                    entities = entities.Include(item);
                }
            }

            if (!tracked)
            {
                entities = entities.AsNoTracking();
            }

            return await entities.ToListAsync();
        }

        public bool Remove(int id, CancellationToken cancellationToken = default)
        {
            T? entityInDB = _dbSet.Find(id);

            if (entityInDB != null)
            {
                _dbSet.Remove(entityInDB);

                return true;
            }

            return false;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
