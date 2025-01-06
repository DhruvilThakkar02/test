using Dapper;
using HRMS.Entities.Tenant.TenancyRole.TenancyRoleRequestEntities;
using HRMS.Entities.Tenant.TenancyRole.TenancyRoleResponseEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Tenant;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class TenancyRoleRepository : ITenancyRoleRepository
    {
        private readonly IDbConnection _dbConnection;

        public TenancyRoleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<TenancyRoleReadResponseEntity>> GetTenancyRoles()
        {
            var roles = await _dbConnection.QueryAsync<TenancyRoleReadResponseEntity>(TenancyRoleStoredProcedure.GetTenancyRoles, commandType: CommandType.StoredProcedure);
            return roles;
        }

        public async Task<TenancyRoleReadResponseEntity?> GetTenancyRoleById(int? tenancyroleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenancyRoleId", tenancyroleId);

            var role = await _dbConnection.QueryFirstOrDefaultAsync<TenancyRoleReadResponseEntity>(TenancyRoleStoredProcedure.GetTenancyRoleById, parameters, commandType: CommandType.StoredProcedure);
            return role;
        }

        public async Task<TenancyRoleCreateResponseEntity> CreateTenancyRole(TenancyRoleCreateRequestEntity tenancyrole)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenancyRoleId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TenancyRoleName", tenancyrole.TenancyRoleName);
            parameters.Add("@CreatedBy", tenancyrole.CreatedBy);
            parameters.Add("@IsActive", tenancyrole.IsActive);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(TenancyRoleStoredProcedure.CreateTenancyRole, parameters, commandType: CommandType.StoredProcedure);

            var roleId = parameters.Get<int>("@TenancyRoleId");

            var createdTenancyRole = new TenancyRoleCreateResponseEntity
            {
                TenancyRoleId = roleId,
                TenancyRoleName = tenancyrole.TenancyRoleName,
                CreatedBy = tenancyrole.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = result?.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = tenancyrole.IsActive,
                IsDelete = result?.IsDelete
            };

            return createdTenancyRole;
        }

        public async Task<TenancyRoleUpdateResponseEntity?> UpdateTenancyRole(TenancyRoleUpdateRequestEntity tenancyrole)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenancyRoleId", tenancyrole.TenancyRoleId);
            parameters.Add("@TenancyRoleName", tenancyrole.TenancyRoleName);
            parameters.Add("@UpdatedBy", tenancyrole.UpdatedBy);
            parameters.Add("@IsActive", tenancyrole.IsActive);
            parameters.Add("@IsDelete", tenancyrole.IsDelete);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<TenancyRoleUpdateResponseEntity>(TenancyRoleStoredProcedure.UpdateTenancyRole, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.TenancyRoleId == -1)
            {
                return null;
            }

            var updatedTenancyRole = new TenancyRoleUpdateResponseEntity
            {
                TenancyRoleId = tenancyrole.TenancyRoleId,
                TenancyRoleName = tenancyrole.TenancyRoleName,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = tenancyrole.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = tenancyrole.IsActive,
                IsDelete = tenancyrole.IsDelete
            };

            return updatedTenancyRole;

        }

        public async Task<int> DeleteTenancyRole(TenancyRoleDeleteRequestEntity tenancyrole)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenancyRoleId", tenancyrole.TenancyRoleId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(TenancyRoleStoredProcedure.DeleteTenancyRole, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
