using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.DTOs.Responses
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string MainImg { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
