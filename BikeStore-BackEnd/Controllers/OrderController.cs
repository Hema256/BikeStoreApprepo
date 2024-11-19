using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeStore_BackEnd.Dto; // Ensure this is the correct namespace for your OrderDTO
using BikeStore_BackEnd.Iservices; // Ensure this is the correct namespace for IOrderService

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // GET: api/order/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PlaceOrder(OrderDTO orderDto)
        {
            var createdOrder = await _orderService.PlaceOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        // PUT: api/order
        [HttpPut]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(OrderDTO orderDto)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(orderDto);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
