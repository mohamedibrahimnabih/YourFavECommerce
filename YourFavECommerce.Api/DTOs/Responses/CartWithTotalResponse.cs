namespace YourFavECommerce.Api.DTOs.Responses
{
    public class CartWithTotalResponse
    {
        public IEnumerable<CartResponse>? CartResponses { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
