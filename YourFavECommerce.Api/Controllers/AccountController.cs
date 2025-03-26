using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using YourFavECommerce.Api.DTOs.Requests;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var applicationUser = registerRequest.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(applicationUser, registerRequest.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser, false);

                return NoContent();
            }

            return BadRequest(result.Errors.ToList());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var applicationUser = await _userManager.FindByEmailAsync(loginRequest.Email);

            if(applicationUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);

                if(result)
                {
                    await _signInManager.SignInAsync(applicationUser, loginRequest.RememberMe);

                    return NoContent();
                }
            }

            ModelStateDictionary keyValues = new();
            keyValues.AddModelError("Errors", "Invalid Data");
            return BadRequest(keyValues);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var applicationUser = await _userManager.GetUserAsync(User);

            if(applicationUser != null)
            {
                var result = await _userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

                if(result.Succeeded)
                {
                    return NoContent();
                }

                return BadRequest(result.Errors.ToList());
            }

            ModelStateDictionary keyValues = new();
            keyValues.AddModelError("Errors", "Invalid Data");
            return BadRequest(keyValues);
        }
    }
}
