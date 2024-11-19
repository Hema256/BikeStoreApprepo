using AutoMapper;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;

namespace BikeStore_BackEnd.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserLoginDTO, User>(); // If needed for login
        }
    }
}
