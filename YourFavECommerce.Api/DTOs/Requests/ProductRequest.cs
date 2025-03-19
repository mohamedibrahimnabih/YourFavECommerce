using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.DTOs.Requests
{
    public record ProductRequest(
        string Name,
        string? Description,
        decimal Price,
        int Quantity,
        double Rate,
        IFormFile file,
        bool Status,
        decimal Discount,
        int CategoryId
    );
}
