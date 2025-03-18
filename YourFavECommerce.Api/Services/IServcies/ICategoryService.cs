using System.Linq.Expressions;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Services.IServcies
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? Get(Expression<Func<Category, bool>> expression);

        Category Add(Category category);
        bool Edit(int id, Category category);
        bool Remove(int id);
    }
}
