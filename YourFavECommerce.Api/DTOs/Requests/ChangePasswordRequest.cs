using System.ComponentModel.DataAnnotations;

namespace YourFavECommerce.Api.DTOs.Requests
{
    public class ChangePasswordRequest
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
