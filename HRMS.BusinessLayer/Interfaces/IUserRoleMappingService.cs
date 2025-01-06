using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRoleMappingService
    {
        Task<IEnumerable<UserRoleMappingReadResponseDto>> GetUserRolesMapping();
        Task<UserRoleMappingReadResponseDto?> GetUserRoleMappingById(int? roleid);
        Task<UserRoleMappingCreateResponseDto> CreateUserRoleMapping(UserRoleMappingCreateRequestDto rolesMappingDto);
        Task<UserRoleMappingUpdateResponseDto?> UpdateUserRolesMapping(UserRoleMappingUpdateRequestDto rolesMappingDto);

        Task<UserRoleMappingDeleteResponseDto?> DeleteUserRoleMapping(UserRoleMappingDeleteRequestDto rolesMappingDto);
    }
}
