using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRoleMappingRepository
    {
        Task<IEnumerable<UserRoleMappingReadResponseEntity>> GetUserRolesMapping();
        Task<UserRoleMappingReadResponseEntity?> GetUserRoleMappingById(int? id);
        Task<UserRoleMappingCreateResponseEntity> CreateUserRoleMapping(UserRoleMappingCreateRequestEntity rolesMapping);
        Task<UserRoleMappingUpdateResponseEntity?> UpdateUserRoleMapping(UserRoleMappingUpdateRequestEntity roleMapping);

        Task<int>DeleteUserRoleMapping(UserRoleMappingDeleteRequestEntity rolesMapping);
    }
}
