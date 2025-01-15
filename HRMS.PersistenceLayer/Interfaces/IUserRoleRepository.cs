using HRMS.Entities.User.UserRole.UserRoleRequestEntities;
using HRMS.Entities.User.UserRole.UserRoleResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleReadResponseEntity>> GetUserRoles();
        Task<UserRoleReadResponseEntity?> GetUserRoleById(int? roleId);
        Task<UserRoleCreateResponseEntity> CreateUserRole(UserRoleCreateRequestEntity roles);
        Task<UserRoleUpdateResponseEntity?> UpdateUserRole(UserRoleUpdateRequestEntity roles);
        Task<int> DeleteUserRole(UserRoleDeleteRequestEntity roles);

    }
}
