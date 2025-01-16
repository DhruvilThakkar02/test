

using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Dtos.User.User.UserResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadResponseDto>> GetUsers();
        Task<UserReadResponseDto?> GetUserById(int? userId);
        Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto userDto);
        Task<UserUpdateResponseDto> UpdateUser(UserUpdateRequestDto userDto);
        Task<UserDeleteResponseDto?> DeleteUser(UserDeleteRequestDto userDto);
    }
}
