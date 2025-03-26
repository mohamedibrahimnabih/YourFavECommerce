using System.ComponentModel.DataAnnotations;

namespace YourFavECommerce.Api.Validations
{
    public class OverYearsAttribute : ValidationAttribute
    {
        readonly int overYears;

        public OverYearsAttribute(int overYears)
        {
            this.overYears = overYears;
        }

        public override bool IsValid(object? value)
        {
            if(value is DateOnly date)
            {
                if (DateTime.Now.Year - date.Year > overYears)
                    return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be over than {overYears} years old";
        }

    }
}
