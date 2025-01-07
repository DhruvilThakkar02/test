using Dapper;
using HRMS.Entities.Tenant.Subdomain.SubdomainRequestEntites;
using HRMS.Entities.Tenant.Subdomain.SubdomainResponseEntites;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Tenant;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class SubdomainRepository : ISubdomainRepository
    {
        private readonly IDbConnection _dbConnection;

        public SubdomainRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<SubdomainReadResponseEntity>> GetSubdomains()
        {
            var subdomains = await _dbConnection.QueryAsync<SubdomainReadResponseEntity>(SubdomainStoredProcedures.GetSubdomains, commandType: CommandType.StoredProcedure);
            return subdomains;
        }

        public async Task<SubdomainReadResponseEntity?> GetSubdomainById(int? subdomainId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SubdomainId", subdomainId);
            var subdomain = await _dbConnection.QueryFirstOrDefaultAsync<SubdomainReadResponseEntity>(SubdomainStoredProcedures.GetSubdomainById, parameters, commandType: CommandType.StoredProcedure);
            return subdomain;
        }

        public async Task<SubdomainCreateResponseEntity> CreateSubdomain(SubdomainCreateRequestEntity subdomain)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SubdomainId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@DomainId", subdomain.DomainId);
            parameters.Add("@SubdomainName", subdomain.SubdomainName);
            parameters.Add("@CreatedBy", subdomain.CreatedBy);
            parameters.Add("@IsActive", subdomain.IsActive);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(SubdomainStoredProcedures.CreateSubdomain, parameters, commandType: CommandType.StoredProcedure);

            var subdomainId = parameters.Get<int>("@SubdomainId");

            var CreatedSubdomain = new SubdomainCreateResponseEntity
            {
                SubdomainId = subdomainId,
                DomainId = subdomain.DomainId,
                SubdomainName = subdomain.SubdomainName,
                CreatedBy = subdomain.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = result?.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = subdomain.IsActive,
                IsDelete = result?.IsDelete
            };
            return CreatedSubdomain;
        }

        public async Task<SubdomainUpdateResponseEntity?> UpdateSubdomain(SubdomainUpdateRequestEntity subdomain)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SubdomainId", subdomain.SubdomainId);
            parameters.Add("@DomainId", subdomain.DomainId);
            parameters.Add("@SubdomainName", subdomain.SubdomainName);
            parameters.Add("@UpdatedBy", subdomain.UpdatedBy);
            parameters.Add("@IsActive", subdomain.IsActive);
            parameters.Add("@IsDelete", subdomain.IsDelete);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<SubdomainUpdateResponseEntity>(SubdomainStoredProcedures.UpdateSubdomain, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.SubdomainId == -1)
            {
                return null;
            }

            var updatedSubdomain = new SubdomainUpdateResponseEntity
            {
                SubdomainId = subdomain.SubdomainId,
                DomainId = subdomain.DomainId,
                SubdomainName = subdomain.SubdomainName,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = subdomain.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = subdomain.IsActive,
                IsDelete = subdomain.IsDelete
            };

            return updatedSubdomain;
        }

        public async Task<int> DeleteSubdomain(SubdomainDeleteRequestEntity subdomain)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SubdomainId", subdomain.SubdomainId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(SubdomainStoredProcedures.DeleteSubdomain, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
