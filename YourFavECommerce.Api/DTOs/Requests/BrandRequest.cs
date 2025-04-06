using System.ComponentModel.DataAnnotations;

namespace YourFavECommerce.Api.DTOs.Requests
{
    public record BrandRequest(
        [Required] string Name,
        string? Note,
        [AllowedValues(true, false, ErrorMessage = "true / false only are allowed.")] bool Status
    );
}
