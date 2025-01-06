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
        public async Task<TenantRegistrationCreateResponseEntity> CreateTenantRegistration(TenantRegistrationCreateRequestEntity tenantregistration)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@SubdomainId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TenantId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OrganizationId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@UserRoleId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@DomainId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@SubdomainName", tenantregistration.SubdomainName);
            parameters.Add("@FirstName", tenantregistration.FirstName);
            parameters.Add("@LastName", tenantregistration.LastName);
            parameters.Add("@UserName", tenantregistration.UserName);
            parameters.Add("@Email", tenantregistration.Email);
            parameters.Add("@Password", tenantregistration.Password);

            await _dbConnection.ExecuteAsync( TenantRegistrationStoreProcedures.CreateTenantRegistration,parameters,commandType: CommandType.StoredProcedure);

            var userId = parameters.Get<int>("@UserId");
            var subdomainId = parameters.Get<int>("@SubdomainId");
            var tenantId = parameters.Get<int>("@TenantId");
            var organizationId = parameters.Get<int>("@OrganizationId");
            var domainId = parameters.Get<int>("@DomainId");
            var userRoleId = parameters.Get<int>("@UserRoleId");
            var hashedPassword = PasswordHashingUtility.HashPassword(tenantregistration.Password);

            var createdTenantregistration = new TenantRegistrationCreateResponseEntity
            {
                UserId = userId,
                SubdomainId = subdomainId,
                TenantId = tenantId,
                OrganizationId = organizationId,
                DomainId = domainId,
                SubdomainName = tenantregistration.SubdomainName,
                FirstName = tenantregistration.FirstName,
                LastName = tenantregistration.LastName,
                UserName = tenantregistration.UserName,
                Email = tenantregistration.Email,
                Password = hashedPassword,
                UserRoleId = userRoleId,
            };
          return createdTenantregistration;
        }
    }
}
