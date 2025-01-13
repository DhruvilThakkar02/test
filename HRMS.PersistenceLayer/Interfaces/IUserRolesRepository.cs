using HRMS.Entities.User.UserRoles.UserRolesRequestEntities;
using HRMS.Entities.User.UserRoles.UserRolesResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<IEnumerable<UserRoleReadResponseEntity>> GetUserRoles();
        Task<UserRoleReadResponseEntity?> GetUserRoleById(int? roleId);
        Task<UserRoleCreateResponseEntity> CreateUserRole(UserRoleCreateRequestEntity roles);
        Task<UserRoleUpdateResponseEntity?> UpdateUserRole(UserRoleUpdateRequestEntity roles);
        Task<int> DeleteUserRole(UserRoleDeleteRequestEntity roles);

    }
}
