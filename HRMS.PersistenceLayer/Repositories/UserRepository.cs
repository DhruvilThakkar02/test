using Dapper;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.Passwords;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserReadResponseEntity>> GetUsers()
        {
            var users = await _dbConnection.QueryAsync<UserReadResponseEntity>(UserStoredProcedures.GetUsers, commandType: CommandType.StoredProcedure);

            foreach (var user in users)
            {
                user.DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth).ToDateTime(TimeOnly.MinValue);
            }

            return users;
        }

        public async Task<UserReadResponseEntity?> GetUserById(int? userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            var user = await _dbConnection.QueryFirstOrDefaultAsync<UserReadResponseEntity>(UserStoredProcedures.GetUserById, parameters, commandType: CommandType.StoredProcedure);
            return user;
        }

        public async Task<UserCreateResponseEntity> CreateUser(UserCreateRequestEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@MiddleName", user.MiddleName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@DateOfBirth", user.DateOfBirth);
            parameters.Add("@IsActive", user.IsActive);
            parameters.Add("@CreatedBy", user.CreatedBy);
            parameters.Add("@TenantId", user.TenantId);
            parameters.Add("@UserRoleId", user.UserRoleId);
            parameters.Add("@TenancyRoleId", user.TenancyRoleId);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(UserStoredProcedures.CreateUser, parameters, commandType: CommandType.StoredProcedure);

            var userId = parameters.Get<int>("@UserId");
            var hashedPassword = PasswordHashingUtility.HashPassword(user.Password);

            var createdUser = new UserCreateResponseEntity
            {
                UserId = userId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = hashedPassword,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                TenantId = user.TenantId,
                UserRoleId = user.UserRoleId,
                TenancyRoleId = user.TenancyRoleId,
                CreatedBy = user.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = result?.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = user.IsActive,
                IsDelete = result?.IsDelete
            };

            return createdUser;
        }

        public async Task<UserUpdateResponseEntity?> UpdateUser(UserUpdateRequestEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", user.UserId);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@MiddleName", user.MiddleName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@DateOfBirth", user.DateOfBirth);
            parameters.Add("@IsActive", user.IsActive);
            parameters.Add("@IsDelete", user.IsDelete);
            parameters.Add("@UpdatedBy", user.UpdatedBy);
            parameters.Add("@TenantId", user.TenantId);
            parameters.Add("@UserRoleId", user.UserRoleId);
            parameters.Add("@TenancyRoleId", user.TenancyRoleId);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<UserUpdateResponseEntity>(UserStoredProcedures.UpdateUser, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.UserId == -1)
            {
                return null;
            }

            result.Password = PasswordHashingUtility.HashPassword(user.Password);

            var updatedUser = new UserUpdateResponseEntity
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                TenantId = user.TenantId,
                UserRoleId = user.UserRoleId,
                TenancyRoleId = user.TenancyRoleId,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = user.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete
            };

            return updatedUser;
        }

        public async Task<int> DeleteUser(UserDeleteRequestEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", user.UserId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(UserStoredProcedures.DeleteUSer, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}