using System.Linq.Expressions;
using YourFavECommerce.Api.Data;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

namespace YourFavECommerce.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category Add(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = _dbContext.Categories.Find(id);

            if (categoryInDb != null)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public Category? Get(Expression<Func<Category, bool>> expression) =>  _dbContext.Categories.FirstOrDefault(expression);

        public IEnumerable<Category> GetAll() => _dbContext.Categories.ToList();

        public bool Remove(int id)
        {
            Category? categoryInDb = _dbContext.Categories.Find(id);

            if (categoryInDb != null)
            {
                _dbContext.Categories.Remove(categoryInDb);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
