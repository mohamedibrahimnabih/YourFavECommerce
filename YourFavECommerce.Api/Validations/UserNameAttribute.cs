//using Microsoft.AspNetCore.Identity;
//using System.ComponentModel.DataAnnotations;
//using YourFavECommerce.Api.Models;

//namespace YourFavECommerce.Api.Validations
//{
//    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
//    public sealed class UserNameAttribute : ValidationAttribute
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public UserNameAttribute(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public override bool IsValid(object? value)
//        {
//            if (value is string v)
//            {
//                var appUser = _userManager.FindByNameAsync(v);

//                if (appUser == null)
//                    return true;
//            }

//            return false;
//        }

//        public override string FormatErrorMessage(string name)
//        {
//            return base.FormatErrorMessage(name);
//        }
//    }
//}
