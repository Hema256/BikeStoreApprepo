using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.IServices
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDto);
        Task<string> LoginUser(UserLoginDTO userLoginDto);
        Task<UserDTO> GetUserProfile(int userId);
        Task<UserDTO> UpdateUserProfile(UserDTO userDto);
        Task LogoutUser(int userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
    }
}
