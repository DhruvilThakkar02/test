using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IUserRolesMappingService
    {
        Task<IEnumerable<UserRolesMappingReadResponseDto>> GetAllUserRolesMapping();

        Task<UserRolesMappingReadResponseDto?> GetByIdUserRolesMapping(int? roleid );
        Task<UserRolesMappingCreateResponseDto> CreateUserRolesMapping(UserRolesMappingCreateRequestDto rolesMappingDto);

        Task<UserRolesMappingUpdateResponseDto?> UpdateUserRolesMapping(UserRolesMappingUpdateRequestDto rolesMappingDto);
    }
}
