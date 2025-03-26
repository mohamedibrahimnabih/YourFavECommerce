using System.ComponentModel.DataAnnotations;

namespace YourFavECommerce.Api.DTOs.Requests
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
