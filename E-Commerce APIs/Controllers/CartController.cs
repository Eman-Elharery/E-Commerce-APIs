using CompanySystem.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lab11
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        /*------------------------------------------------*/

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.GetCartAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.AddToCartAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.UpdateCartItemAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.RemoveFromCartAsync(userId, productId);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _cartManager.ClearCartAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
