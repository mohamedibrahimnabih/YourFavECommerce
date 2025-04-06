using System.ComponentModel.DataAnnotations;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.DTOs.Requests
{
    //public class CatgeoryRequest
    //{
    //    public string Name { get; set; } = string.Empty;
    //    public string? Note { get; set; }
    //    public bool Status { get; set; }

    //    public static explicit operator Category(CatgeoryRequest category)
    //    {
    //        return new()
    //        {
    //            Name = category.Name,
    //            Description = category.Note,
    //            Status = category.Status
    //        };
    //    }
    //}

    public record CatgeoryRequest(
        string Name,
        string? Note,
        [AllowedValues(true, false, ErrorMessage = "true / false only are allowed.")] bool Status
    );
}
