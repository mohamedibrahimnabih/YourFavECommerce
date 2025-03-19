using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Mapping
{
    public static class MappingProfiler
    {
        //public static Category MapToCatgeory(this CatgeoryRequest category)
        //{
        //    return new()
        //    {
        //        Name = category.Name,
        //        Description = category.Note,
        //        Status = category.Status
        //    };
        //}

        //public static CatgeoryResponse MapToCatgeoryResponse(this Category category)
        //{
        //    return new()
        //    {
        //        Id = category.Id,
        //        Name = category.Name,
        //        Note = category.Description,
        //        Status = category.Status
        //    };
        //}

        //public static IEnumerable<CatgeoryResponse> MapToCatgeoriesResponse(this IEnumerable<Category> category)
        //{
        //    //List<CatgeoryResponse> catgeoryResponses = [];

        //    //foreach (var item in category)
        //    //{
        //    //    catgeoryResponses.Add(new CatgeoryResponse()
        //    //    {
        //    //        Id = item.Id,
        //    //        Name = item.Name,
        //    //        Description = item.Description,
        //    //        Status = item.Status
        //    //    });
        //    //}

        //    return category.Select(MapToCatgeoryResponse);
        //}
    }
}
