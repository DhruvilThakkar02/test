using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRoleMappingService
    {
        Task<IEnumerable<UserRoleMappingReadResponseDto>> GetUserRolesMapping();
        Task<UserRoleMappingReadResponseDto?> GetUserRoleMappingById(int? roleid);
        Task<UserRoleMappingCreateResponseDto> CreateUserRoleMapping(UserRoleMappingCreateRequestDto rolesMappingDto);
        Task<UserRoleMappingUpdateResponseDto?> UpdateUserRoleMapping(UserRoleMappingUpdateRequestDto rolesMappingDto);

        Task<UserRoleMappingDeleteResponseDto?> DeleteUserRoleMapping(UserRoleMappingDeleteRequestDto rolesMappingDto);
    }
}
