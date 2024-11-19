using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    public class OrderService : IOrderService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> PlaceOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
