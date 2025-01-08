using Dapper;
using HRMS.Entities.User.Login.LoginRequestEntities;
using HRMS.Entities.User.Login.LoginResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.Passwords;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoginRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<LoginResponseEntity> LoginAsync(LoginRequestEntity request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SubdomainName", request.SubdomainName);
            parameters.Add("@UserNameOrEmail", request.UserNameOrEmail);
            parameters.Add("@Password", request.Password);

            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserName", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            parameters.Add("@TenantId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@StoredPasswordHash", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            parameters.Add("@ErrorMessage", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

      
            var result = await _dbConnection.ExecuteAsync(
                LoginStoreProcedure.Userlogin,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            var errorMessage = parameters.Get<string>("@ErrorMessage");

      
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return new LoginResponseEntity
                {
                    ErrorMessage = errorMessage
                };
            }
            var storedPasswordHash = parameters.Get<string>("@StoredPasswordHash");
            bool isPasswordValid = PasswordHashingUtility.VerifyPassword(request.Password, storedPasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResponseEntity
                {
                    ErrorMessage = "Invalid credentials" 
                };
            }

            var userId = parameters.Get<int>("@UserId");
            var userName = parameters.Get<string>("@UserName");
            var tenantId = parameters.Get<int>("@TenantId");

            var loginResponse = new LoginResponseEntity
            {
                UserId = userId,
                UserName = userName,
                TenantId = tenantId
            };

            return loginResponse;
        }
    }
}
