using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    public class OrderItemService : IOrderItemService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper;  // Assuming you're using AutoMapper for mapping

        public OrderItemService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderItemId)
        {
            var orderItems = await _context.OrderItems
                .Where(oi => oi.OrderItemId == orderItemId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        }

        public async Task<OrderItemDto> AddOrderItemAsync(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<OrderItemDto> UpdateOrderItemAsync(int orderItemId, OrderItemDto orderItemDto)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null) return null;

            _mapper.Map(orderItemDto, orderItem);
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null) return false;

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
