using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;


namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRolesService
    {
        Task<IEnumerable<UserRoleReadResponseDto>> GetUserRoles();
        Task<UserRoleReadResponseDto?> GetUserRoleById(int? rolesId);
        Task<UserRoleCreateResponseDto> CreateUserRole(UserRoleCreateRequestDto rolesDto);
        Task<UserRoleUpdateResponseDto> UpdateUserRole(UserRoleUpdateRequestDto rolesDTo);
        Task<UserRoleDeleteResponseDto?> DeleteUserRole(UserRoleDeleteRequestDto rolesDto);
    }
}
