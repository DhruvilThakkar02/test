using HRMS.Dtos.User.UserRole.UserRoleRequestDtos;
using HRMS.Dtos.User.UserRole.UserRoleResponseDtos;
namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleReadResponseDto>> GetUserRoles();
        Task<UserRoleReadResponseDto?> GetUserRoleById(int? rolesId);
        Task<UserRoleCreateResponseDto> CreateUserRole(UserRoleCreateRequestDto rolesDto);
        Task<UserRoleUpdateResponseDto> UpdateUserRole(UserRoleUpdateRequestDto rolesDTo);
        Task<UserRoleDeleteResponseDto?> DeleteUserRole(UserRoleDeleteRequestDto rolesDto);
    }
}
