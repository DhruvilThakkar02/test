using HRMS.Entities.User.UserRoles.UserRolesRequestEntities;
using HRMS.Entities.User.UserRoles.UserRolesResponseEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRolesMappingRepository
    {
        Task<IEnumerable<UserRolesMappingReadResponseEntity>> GetAllUserRolesMapping();
        Task<UserRolesMappingReadResponseEntity?> GetByIdUserRolesMapping(int? id);
        Task<UserRolesMappingCreateResponseEntity> CreateUserRolesMapping(UserRolesMappingCreateRequestEntity rolesMapping);

        Task<UserRolesMappingUpdateResponseEntity?> UpdateUserRolesMapping(UserRolesMappingUpdateRequestEntity roleMapping);


    }
}
