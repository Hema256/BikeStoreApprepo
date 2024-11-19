using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.Iservices
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<OrderItemDto> AddOrderItemAsync(OrderItemDto orderItemDto);
        Task<OrderItemDto> UpdateOrderItemAsync(int orderItemId, OrderItemDto orderItemDto);
        Task<bool> DeleteOrderItemAsync(int orderItemId);
    }

}
