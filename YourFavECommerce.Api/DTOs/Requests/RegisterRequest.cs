using System.ComponentModel.DataAnnotations;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Validations;

namespace YourFavECommerce.Api.DTOs.Requests
{
    public class RegisterRequest
    {
        [MinLength(3)]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(3)]
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        public ApplicationUserGender Gender { get; set; }

        [OverYears(18)]
        public DateOnly BirthOfDate { get; set; }
    }
}
