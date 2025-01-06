using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRolesMappingRepository
    {
        Task<IEnumerable<UserRolesMappingReadResponseEntity>> GetUserRolesMapping();
        Task<UserRolesMappingReadResponseEntity?> GetUserRoleMappingById(int? id);
        Task<UserRolesMappingCreateResponseEntity> CreateUserRoleMapping(UserRolesMappingCreateRequestEntity rolesMapping);
        Task<UserRolesMappingUpdateResponseEntity> UpdateUserRolesMapping(UserRolesMappingUpdateRequestEntity roleMapping);
    }
}
