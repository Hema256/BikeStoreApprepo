using AutoMapper;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;

namespace OnlineLearningPlatform.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<OrderItemDto,OrderItem>().ReverseMap();
            CreateMap<CategoryDTO,Category>().ReverseMap();
        }
    }
}
