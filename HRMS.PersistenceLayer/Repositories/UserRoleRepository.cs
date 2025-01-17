using Dapper;
using HRMS.Entities.User.UserRole.UserRoleRequestEntities;
using HRMS.Entities.User.UserRole.UserRoleResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;


namespace HRMS.PersistenceLayer.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRoleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserRoleReadResponseEntity>> GetUserRoles()
        {
            var roles = await _dbConnection.QueryAsync<UserRoleReadResponseEntity>(UserRoleStoredProcedure.GetUserRoles, commandType: CommandType.StoredProcedure);
            return roles;
        }

        public async Task<UserRoleReadResponseEntity?> GetUserRoleById(int? roleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleId", roleId);

            var roles = await _dbConnection.QueryFirstOrDefaultAsync<UserRoleReadResponseEntity>(UserRoleStoredProcedure.GetUserRoleById, parameters, commandType: CommandType.StoredProcedure);

            return roles;
        }

        public async Task<UserRoleCreateResponseEntity> CreateUserRole(UserRoleCreateRequestEntity  roles)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserRoleName", roles.UserRoleName);
            parameters.Add("@PermissionGroupId", roles.PermissionGroupId);
            parameters.Add("@CreatedBy", roles.CreatedBy);
            parameters.Add("@IsActive", roles.IsActive);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(UserRoleStoredProcedure.CreateUserRole, parameters, commandType: CommandType.StoredProcedure);

            var userroleId = parameters.Get<int>("@UserRoleId");

            var createdRole = new UserRoleCreateResponseEntity
            {
                UserRoleId = userroleId,
                UserRoleName = roles.UserRoleName,
                PermissionGroupId = roles.PermissionGroupId,
                CreatedBy = roles.CreatedBy,
                CreatedAt = DateTime.Now,
                IsActive = roles.IsActive,
                IsDelete = result?.IsDelete
            };

            return createdRole;
        }

        public async Task<UserRoleUpdateResponseEntity?> UpdateUserRole(UserRoleUpdateRequestEntity roles)
        {
            var paramters = new DynamicParameters();
            paramters.Add("@UserRoleId", roles.UserRoleId);
            paramters.Add("@UserRoleName", roles.UserRoleName);
            paramters.Add("@PermissionGroupId", roles.PermissionGroupId);
            paramters.Add("@UpdatedBy", roles.UpdatedBy);
            paramters.Add("@IsActive", roles.IsActive);
            paramters.Add("@IsDelete", roles.IsDelete);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<UserRoleUpdateResponseEntity>(UserRoleStoredProcedure.UpdateUserRole, paramters, commandType: CommandType.StoredProcedure);

            if (result == null || result.UserRoleId == -1)
            {
                return null;
            }

            var updateUserRole = new UserRoleUpdateResponseEntity
            {
                UserRoleId = roles.UserRoleId,
                UserRoleName = roles.UserRoleName,
                PermissionGroupId = roles.PermissionGroupId,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = roles.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = roles.IsActive,
                IsDelete = roles.IsDelete
            };
            return updateUserRole;
        }

        public async Task<int> DeleteUserRole(UserRoleDeleteRequestEntity roles)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleId", roles.UserRoleId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(UserRoleStoredProcedure.DeleteUserRole, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
