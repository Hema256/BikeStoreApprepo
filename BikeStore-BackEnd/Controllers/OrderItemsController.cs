using AutoMapper;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public OrderItemsController(IOrderItemService orderItemService, IMapper mapper)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
        }

        // GET: api/orderitems/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }
        // GET: api/orderitems/order/{orderId}
        [HttpGet("orderItems/{orderItemId}")]
        public async Task<IActionResult> GetOrderItemsByOrderItemId(int orderItemId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderItemId);
            return Ok(orderItems);
        }

        // POST: api/orderitems
        [HttpPost]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
            {
                return BadRequest("Invalid order item data.");
            }

            var addedItem = await _orderItemService.AddOrderItemAsync(orderItemDto);
            return CreatedAtAction(nameof(GetOrderItemsByOrderId), new { orderId = addedItem.OrderId }, addedItem);
        }

        // PUT: api/orderitems/{orderItemId}
        [HttpPut("{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(int orderItemId, [FromBody] OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
            {
                return BadRequest("Invalid order item data.");
            }

            var updatedItem = await _orderItemService.UpdateOrderItemAsync(orderItemId, orderItemDto);
            if (updatedItem == null)
            {
                return NotFound($"Order item with ID {orderItemId} not found.");
            }

            return Ok(updatedItem);
        }

        // DELETE: api/orderitems/{orderItemId}
        [HttpDelete("{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem(int orderItemId)
        {
            var result = await _orderItemService.DeleteOrderItemAsync(orderItemId);
            if (!result)
            {
                return NotFound($"Order item with ID {orderItemId} not found.");
            }

            return NoContent(); // Successfully deleted
        }
    }
}
