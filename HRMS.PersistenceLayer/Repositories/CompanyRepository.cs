using Dapper;
using HRMS.Entities.Tenant.Company.CompanyRequestEntities;
using HRMS.Entities.Tenant.Company.CompanyResponseEntities;
using HRMS.Entities.Tenant.Organization.OrganizationResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Tenant;
using System.ComponentModel.Design;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _dbConnection;
        public CompanyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<CompanyReadResponseEntity>> GetCompanies()
        {
            var companies = await _dbConnection.QueryAsync<CompanyReadResponseEntity>(CompanyStoreProcedures.GetCompanies, commandType: CommandType.StoredProcedure);
            return companies;
        }

        public async Task<CompanyReadResponseEntity?> GetCompanyById(int? companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var company = await _dbConnection.QueryFirstOrDefaultAsync<CompanyReadResponseEntity>(CompanyStoreProcedures.GetCompanyById, parameters, commandType: CommandType.StoredProcedure);
            return company;
        }
        public async Task<CompanyCreateResponseEntity> CreateCompany(CompanyCreateRequestEntity company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@CompanyName", company.CompanyName);
            parameters.Add("@Industry", company.Industry);
            parameters.Add("@CompanyType", company.CompanyType);
            parameters.Add("@FoundedDate", company.FoundedDate);
            parameters.Add("@NumberOfEmployees", company.NumberOfEmployees);
            parameters.Add("@WebsiteUrl", company.WebsiteUrl);
            parameters.Add("@TaxNumber", company.TaxNumber);
            parameters.Add("@GstNumber", company.GstNumber);
            parameters.Add("@PfNumber", company.PfNumber);
            parameters.Add("@PhoneNumber", company.PhoneNumber);
            parameters.Add("@Logo", company.Logo);
            parameters.Add("@Email", company.Email);
            parameters.Add("@AddressId", company.AddressId);
            parameters.Add("@TenantId", company.TenantId);
            parameters.Add("@CreatedBy", company.CreatedBy);
            parameters.Add("@IsActive", company.IsActive);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CompanyCreateResponseEntity>(CompanyStoreProcedures.CreateCompany, parameters, commandType: CommandType.StoredProcedure);

            var companyId = parameters.Get<int>("@CompanyId");

            var createdCompany = new CompanyCreateResponseEntity
            {
                CompanyId = companyId,
                CompanyName = company.CompanyName,
                Industry = company.Industry,
                CompanyType = company.CompanyType,
                FoundedDate = company.FoundedDate,
                NumberOfEmployees = company.NumberOfEmployees,
                WebsiteUrl = company.WebsiteUrl,
                TaxNumber = company.TaxNumber,
                GstNumber = company.GstNumber,
                PfNumber = company.PfNumber,
                PhoneNumber = company.PhoneNumber,
                Logo = company.Logo,
                Email = company.Email,
                AddressId = company.AddressId,
                TenantId = company.TenantId,
                CreatedBy = company.CreatedBy,
                UpdatedBy = result?.UpdatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = company.IsActive,
                IsDelete = result?.IsDelete
            };
            return createdCompany;
        }
        public async Task<CompanyUpdateResponseEntity?> UpdateCompany(CompanyUpdateRequestEntity company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", company.CompanyId);
            parameters.Add("@CompanyName", company.CompanyName);
            parameters.Add("@Industry", company.Industry);
            parameters.Add("@CompanyType", company.CompanyType);
            parameters.Add("@FoundedDate", company.FoundedDate);
            parameters.Add("@NumberOfEmployees", company.NumberOfEmployees);
            parameters.Add("@WebsiteUrl", company.WebsiteUrl);
            parameters.Add("@TaxNumber", company.TaxNumber);
            parameters.Add("@GstNumber", company.GstNumber);
            parameters.Add("@PfNumber", company.PfNumber);
            parameters.Add("@PhoneNumber", company.PhoneNumber);
            parameters.Add("@Logo", company.Logo);
            parameters.Add("@Email", company.Email);
            parameters.Add("@AddressId", company.AddressId);
            parameters.Add("@TenantId", company.TenantId);
            parameters.Add("@UpdatedBy", company.UpdatedBy);
            parameters.Add("@IsActive", company.IsActive);
            parameters.Add("@IsDelete", company.IsDelete);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CompanyUpdateResponseEntity>(CompanyStoreProcedures.UpdateCompany, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.CompanyId == -1)
            {
                return null;
            }
            var Updatecompany = new CompanyUpdateResponseEntity
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                Industry = company.Industry,
                CompanyType = company.CompanyType,
                FoundedDate = company.FoundedDate,
                NumberOfEmployees = company.NumberOfEmployees,
                WebsiteUrl = company.WebsiteUrl,
                TaxNumber = company.TaxNumber,
                GstNumber = company.GstNumber,
                PfNumber = company.PfNumber,
                PhoneNumber = company.PhoneNumber,
                Logo = company.Logo,
                Email = company.Email,
                AddressId = company.AddressId,
                TenantId = company.TenantId,
                CreatedBy =company.CreatedBy,
                UpdatedBy = result.UpdatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = company.IsActive,
                IsDelete = result?.IsDelete,
            };
            return Updatecompany;

        }
        public async Task<int> DeleteCompany(CompanyDeleteRequestEntity company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.CompanyId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(CompanyStoreProcedures.DeleteCompany, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
