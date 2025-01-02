﻿using Dapper;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class UserRolesMappingRepository : IUserRolesMappingRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRolesMappingRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserRolesMappingReadResponseEntity>> GetUserRolesMapping()
        {
            var allrolesmapping = await _dbConnection.QueryAsync<UserRolesMappingReadResponseEntity>(UserRolesMappingStoredProcedure.GetUserRolesMapping, commandType: CommandType.StoredProcedure);
            return allrolesmapping;
        }

        public async Task<UserRolesMappingReadResponseEntity?> GetUserRoleMappingById(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleMappingId", id);

            var roles = await _dbConnection.QueryFirstOrDefaultAsync<UserRolesMappingReadResponseEntity>(UserRolesMappingStoredProcedure.GetUserRoleMappingById, parameters, commandType: CommandType.StoredProcedure);
            return roles;
        }

        public async Task<UserRolesMappingCreateResponseEntity> CreateUserRoleMapping(UserRolesMappingCreateRequestEntity rolesMapping)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserRoleMappingId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserId", rolesMapping.UserId);
            parameters.Add("@RoleId", rolesMapping.RoleId);
            parameters.Add("@CreatedBy", rolesMapping.CreatedBy);

            var rolemapping = await _dbConnection.ExecuteScalarAsync<int>(UserRolesMappingStoredProcedure.CreateUserRoleMapping, parameters, commandType: CommandType.StoredProcedure);

            var rolemappingId = parameters.Get<int>("@UserRoleMappingId");

            var createdRoleMapping = new UserRolesMappingCreateResponseEntity
            {
                UserRoleMappingId = rolemappingId,
                RoleId = rolesMapping.RoleId,
                UserId = rolesMapping.UserId,
                CreatedBy = rolesMapping.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return createdRoleMapping;

        }

        //public async Task<UserRolesMappingUpdateResponseEntity> UpdateUserRolesMapping(UserRolesMappingUpdateRequestEntity rolesMapping)
        //{
        //    var paramters = new DynamicParameters();
        //    paramters.Add("@UserRoleMappingId",rolesMapping.UserRoleMappingId );
        //    paramters.Add("@UserId",rolesMapping.UserId );
        //    paramters.Add("@RoleId",rolesMapping.RoleId);
        //    paramters.Add("@UpdatedBy",rolesMapping.UpdatedBy );


        //    var result = await _dbConnection.ExecuteAsync(UserRolesMappingStoredProcedure.,)
        //}
    }
}
