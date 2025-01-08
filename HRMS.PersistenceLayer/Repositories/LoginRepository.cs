using Dapper;
using HRMS.Entities.User.Login.LoginRequestEntities;
using HRMS.Entities.User.Login.LoginResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.JwtSecretKey;
using HRMS.Utility.Helpers.Passwords;
using HRMS.Utility.Helpers.SqlHelpers.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.PersistenceLayer.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly JwtSecretKey _jwtSecretKey;

        public LoginRepository(IDbConnection dbConnection, IOptions<JwtSecretKey> jwtSecretKey)
        {
            _dbConnection = dbConnection;
            _jwtSecretKey = jwtSecretKey.Value;
        }
        public async Task<LoginResponseEntity> Login(LoginRequestEntity request)
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
            bool isPasswordValid = PasswordHashingUtility.Verify(request.Password, storedPasswordHash);


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

            var user = new LoginResponseEntity
            {
                UserId = userId,
                UserName = userName,
                TenantId = tenantId
            };

            var token = await GenerateJwtToken(user);
            var loginResponse = new LoginResponseEntity
            {
                UserId = userId,
                UserName = userName,
                TenantId = tenantId,
                Token = token
            };

            return loginResponse;
        }
        internal async Task<string> GenerateJwtToken(LoginResponseEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_jwtSecretKey.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("UserId", user.UserId.ToString()),
                    }
                    ),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });
            return tokenHandler.WriteToken(token);
        }


    }
}
