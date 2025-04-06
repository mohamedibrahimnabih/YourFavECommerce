using FluentValidation;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Validator
{
    public class CatgeoryValidator : AbstractValidator<CatgeoryRequest>
    {
        public CatgeoryValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().Length(5).WithMessage("{PropertyName} Invalid Data: {PropertyValue}");
        }
    }
}
