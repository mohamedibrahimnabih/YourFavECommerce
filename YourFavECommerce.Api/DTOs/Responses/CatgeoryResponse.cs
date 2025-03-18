using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.DTOs.Responses
{
    public class CatgeoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Note { get; set; }
        public bool Status { get; set; }

        public static explicit operator CatgeoryResponse(Category category)
        {
            return new()
            {
                Id = category.Id,
                Name = category.Name,
                Note = category.Description,
                Status = category.Status
            };
        }
    }
}
