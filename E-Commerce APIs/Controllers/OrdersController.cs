using CompanySystem.BLL;
using CompanySystem.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lab11
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        /*------------------------------------------------*/

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.PlaceOrderAsync(userId, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.GetUserOrdersAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.GetOrderByIdAsync(userId, id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPut("{id}/status")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            var result = await _orderManager.UpdateOrderStatusAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpDelete("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderManager.CancelOrderAsync(userId, id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
