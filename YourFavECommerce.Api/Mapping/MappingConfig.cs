using Mapster;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CatgeoryResponse>()
                .Map(des => des.Note, src => src.Description);

            config.NewConfig<CatgeoryRequest, Category>()
               .Map(des => des.Description, src => src.Note);
        }
    }
}
