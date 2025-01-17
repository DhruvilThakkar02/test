using Dapper;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingRequestEntities;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class UserRoleMappingRepository : IUserRoleMappingRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRoleMappingRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserRoleMappingReadResponseEntity>> GetUserRolesMapping()
        {
            var allrolesmapping = await _dbConnection.QueryAsync<UserRoleMappingReadResponseEntity>(UserRoleMappingStoredProcedure.GetUserRolesMapping, commandType: CommandType.StoredProcedure);
            return allrolesmapping;
        }

        public async Task<UserRoleMappingReadResponseEntity?> GetUserRoleMappingById(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleMappingId", id);

            var roles = await _dbConnection.QueryFirstOrDefaultAsync<UserRoleMappingReadResponseEntity>(UserRoleMappingStoredProcedure.GetUserRoleMappingById, parameters, commandType: CommandType.StoredProcedure);
            return roles;
        }

        public async Task<UserRoleMappingCreateResponseEntity> CreateUserRoleMapping(UserRoleMappingCreateRequestEntity rolesMapping)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleMappingId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserId", rolesMapping.UserId);
            parameters.Add("@UserRoleId", rolesMapping.UserRoleId);
            parameters.Add("@CreatedBy", rolesMapping.CreatedBy);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<UserRoleMappingUpdateResponseEntity>(UserRoleMappingStoredProcedure.CreateUserRoleMapping, parameters, commandType: CommandType.StoredProcedure);

            var rolemappingId = parameters.Get<int>("@UserRoleMappingId");

            var createdRoleMapping = new UserRoleMappingCreateResponseEntity
            {
                UserRoleMappingId = rolemappingId,
                UserRoleId = rolesMapping.UserRoleId,
                UserId = rolesMapping.UserId,
                CreatedBy = rolesMapping.CreatedBy,
                UpdatedBy = result?.UpdatedBy,
                CreatedAt = DateTime.Now
            };

            return createdRoleMapping;

        }

        public async Task<UserRoleMappingUpdateResponseEntity?> UpdateUserRoleMapping(UserRoleMappingUpdateRequestEntity roleMapping)
        {
            var paramters = new DynamicParameters();
            paramters.Add("@UserRoleMappingId", roleMapping.UserRoleMappingId);
            paramters.Add("@UserId", roleMapping.UserId);
            paramters.Add("@UserRoleId", roleMapping.UserRoleId);
            paramters.Add("@UpdatedBy", roleMapping.UpdatedBy);


            var result = await _dbConnection.QuerySingleOrDefaultAsync<UserRoleMappingUpdateResponseEntity>(UserRoleMappingStoredProcedure.UpdateUserRoleMapping, paramters, commandType: CommandType.StoredProcedure);

            if (result == null || result.UserRoleMappingId == -1)
            {
                return null;
            }
            var updateUsermappingRoles = new UserRoleMappingUpdateResponseEntity
            {
                UserRoleMappingId = roleMapping.UserRoleMappingId,
                UserId = roleMapping.UserId,
                UserRoleId = roleMapping.UserRoleId,
                CreatedBy = result.CreatedBy,
                UpdatedBy = roleMapping.UpdatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = roleMapping.IsActive,
                IsDelete = roleMapping.IsDelete


            };
            return updateUsermappingRoles;

        }

        public async Task<int> DeleteUserRoleMapping(UserRoleMappingDeleteRequestEntity rolesMapping)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleMappingId", rolesMapping.UserRoleMappingId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(UserRoleMappingStoredProcedure.DeleteUserRoleMapping, parameters, commandType: CommandType.StoredProcedure);

            return result;



        }


    }
}
