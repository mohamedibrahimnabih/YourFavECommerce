using System.Linq.Expressions;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Services.IServcies
{
    public interface ICategoryService : IService<Category>
    {
        Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default);
        Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken = default);
    }
}
