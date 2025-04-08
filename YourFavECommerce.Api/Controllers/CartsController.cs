using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using YourFavECommerce.Api.DTOs.Responses;
using YourFavECommerce.Api.Models;
using YourFavECommerce.Api.Services.IServcies;

namespace YourFavECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartsController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToCart([FromRoute] int productId, [FromQuery] int count, CancellationToken cancellationToken)
        {
            var appUser = _userManager.GetUserId(User);

            if(appUser is not null)
            {
                var cartInDB = _cartService.GetOne(e => e.ProductId == productId && e.ApplicationUserId == appUser);

                if (cartInDB is not null)
                {
                    cartInDB.Count += count;
                }
                else
                {
                    await _cartService.AddAsync(new()
                    {
                        ProductId = productId,
                        Count = count,
                        ApplicationUserId = appUser
                    }, cancellationToken);
                }

                await _cartService.CommitAsync();

                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCarts()
        {
            var appUser = _userManager.GetUserId(User);

            if (appUser is not null)
            {
                var carts = await _cartService.GetAsync(e => e.ApplicationUserId == appUser, includes: [e => e.Product]);

                var totalPrice = carts.Sum(e => e.Product.Price * e.Count);

                var cartResponse = carts.Select(e=>e.Product).Adapt<IEnumerable<CartResponse>>();
                CartWithTotalResponse cartWithTotalResponse = new()
                {
                    CartResponses = cartResponse,
                    TotalPrice = totalPrice
                };

                return Ok(cartWithTotalResponse);
            }

            return NotFound();
        }
    }
}
