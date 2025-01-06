using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;


namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRolesService
    {
        Task<IEnumerable<UserRolesReadResponseDto>> GetUserRoles();
        Task<UserRolesReadResponseDto?> GetUserRoleById(int? rolesId);
        Task<UserRolesCreateResponseDto> CreateUserRole(UserRolesCreateRequestDto rolesDto);
        Task<UserRolesUpdateResponseDto> UpdateUserRoles(UserRolesUpdateRequestDto rolesDTo);
        Task<UserRolesDeleteResponseDto?> DeleteUserRoles(UserRolesDeleteRequestDto rolesDto);
    }
}
