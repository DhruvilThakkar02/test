using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRolesMappingService
    {
        Task<IEnumerable<UserRolesMappingReadResponseDto>> GetUserRolesMapping();
        Task<UserRolesMappingReadResponseDto?> GetUserRoleMappingById(int? roleid);
        Task<UserRolesMappingCreateResponseDto> CreateUserRoleMapping(UserRolesMappingCreateRequestDto rolesMappingDto);
        Task<UserRolesMappingUpdateResponseDto?> UpdateUserRolesMapping(UserRolesMappingUpdateRequestDto rolesMappingDto);
    }
}
