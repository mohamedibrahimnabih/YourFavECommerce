using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.DTOs.Responses
{
    public record ProductResponse(
        int Id,
        string Name,
        string? Description,
        string MainImg,
        decimal Price,
        int Quantity,
        double Rate,
        bool Status,
        decimal Discount,
        int CategoryId
    );
}
