using System.Linq.Expressions;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Services.IServcies
{
    public interface IBrandServices
    {
        IEnumerable<Brand> GetAll();
        Brand? Get(Expression<Func<Brand, bool>> expression);

        Brand Add(Brand brand);
        bool Edit(int id, Brand brand);
        bool Remove(int id);
    }
}
