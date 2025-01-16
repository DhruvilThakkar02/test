using HRMS.Entities.User.UserRoleMapping.UserRoleMappingRequestEntities;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRoleMappingRepository
    {
        Task<IEnumerable<UserRoleMappingReadResponseEntity>> GetUserRolesMapping();
        Task<UserRoleMappingReadResponseEntity?> GetUserRoleMappingById(int? id);
        Task<UserRoleMappingCreateResponseEntity> CreateUserRoleMapping(UserRoleMappingCreateRequestEntity rolesMapping);
        Task<UserRoleMappingUpdateResponseEntity?> UpdateUserRoleMapping(UserRoleMappingUpdateRequestEntity roleMapping);

        Task<int> DeleteUserRoleMapping(UserRoleMappingDeleteRequestEntity rolesMapping);
    }
}
