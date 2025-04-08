using Microsoft.EntityFrameworkCore;

namespace YourFavECommerce.Api.Models
{
    //[PrimaryKey(nameof(ProductId), nameof(ApplicationUserId))]
    public class Cart
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;


        public int Count { get; set; }
    }
}
