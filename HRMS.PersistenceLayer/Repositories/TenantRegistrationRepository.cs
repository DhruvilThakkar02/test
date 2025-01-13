using Dapper;
using HRMS.Entities.Tenant.TenantRegistration.TenantRegistrationRequestEntities;
using HRMS.Entities.Tenant.TenantRegistration.TenantRegistrationResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.Passwords;
using HRMS.Utility.Helpers.SqlHelpers.Tenant;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class TenantRegistrationRepository : ITenantRegistrationRepository
    {
        private readonly IDbConnection _dbConnection;

        public TenantRegistrationRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<TenantRegistrationCreateResponseEntity> CreateTenantRegistration(TenantRegistrationCreateRequestEntity tenantRegistration)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@SubdomainId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TenantId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OrganizationId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserRoleId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@DomainId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@SubdomainName", tenantRegistration.SubdomainName);
            parameters.Add("@FirstName", tenantRegistration.FirstName);
            parameters.Add("@LastName", tenantRegistration.LastName);
            parameters.Add("@UserName", tenantRegistration.UserName);
            parameters.Add("@Email", tenantRegistration.Email);
            parameters.Add("@Password", tenantRegistration.Password);

            await _dbConnection.ExecuteAsync( TenantRegistrationStoreProcedures.CreateTenantRegistration,parameters,commandType: CommandType.StoredProcedure);

            var userId = parameters.Get<int>("@UserId");
            var subdomainId = parameters.Get<int>("@SubdomainId");
            var tenantId = parameters.Get<int>("@TenantId");
            var organizationId = parameters.Get<int>("@OrganizationId");
            var domainId = parameters.Get<int>("@DomainId");
            var userRoleId = parameters.Get<int>("@UserRoleId");
            var hashedPassword = PasswordHashingUtility.HashPassword(tenantRegistration.Password);

            var createdTenantregistration = new TenantRegistrationCreateResponseEntity
            {
                UserId = userId,
                SubdomainId = subdomainId,
                TenantId = tenantId,
                OrganizationId = organizationId,
                DomainId = domainId,
                SubdomainName = tenantRegistration.SubdomainName,
                FirstName = tenantRegistration.FirstName,
                LastName = tenantRegistration.LastName,
                UserName = tenantRegistration.UserName,
                Email = tenantRegistration.Email,
                Password = hashedPassword,
                UserRoleId = userRoleId,
            };
          return createdTenantregistration;
        }
    }
}
