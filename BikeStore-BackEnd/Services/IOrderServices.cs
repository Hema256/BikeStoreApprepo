using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.Iservices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDTO> PlaceOrderAsync(OrderDTO orderDto);
        Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDto);
        Task DeleteOrderAsync(int id);
    }
}
