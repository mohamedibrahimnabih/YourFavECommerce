using System.Linq.Expressions;
using System.Threading.Tasks;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

namespace YourFavECommerce.Api.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default)
        {
            Category? categoryInDb = _dbContext.Categories.Find(id);

            if (categoryInDb != null)
            {
                categoryInDb.Name = category.Name;
                categoryInDb.Description = category.Description;
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken = default)
        {
            Category? categoryInDb = _dbContext.Categories.Find(id);

            if (categoryInDb != null)
            {
                categoryInDb.Status = !categoryInDb.Status;
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
