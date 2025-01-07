using Dapper;
using HRMS.Entities.CompanyBranch.CompanyBranchRequestEntities;
using HRMS.Entities.CompanyBranch.CompanyBranchResponseEntities;
using HRMS.Entities.Tenant.TenancyRole.TenancyRoleRequestEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.CompanyBranch;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class CompanyBranchRepository : ICompanyBranchRepository
    {
        private readonly IDbConnection _dbConnection;

        public CompanyBranchRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CompanyBranchReadResponseEntity>> GetCompanyBranches()
        {
            var companyBranches = await _dbConnection.QueryAsync<CompanyBranchReadResponseEntity>(
                CompanyBranchStoreProcedures.GetCompanyBranches,
                commandType: CommandType.StoredProcedure
            );
            return companyBranches;
        }

        public async Task<CompanyBranchReadResponseEntity?> GetCompanyBranchById(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyBranchId", id);

            var companyBranch = await _dbConnection.QueryFirstOrDefaultAsync<CompanyBranchReadResponseEntity>(
                CompanyBranchStoreProcedures.GetCompanyBranchById,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return companyBranch;
        }

        public async Task<CompanyBranchCreateResponseEntity> CreateCompanyBranch(CompanyBranchCreateRequestEntity companyBranch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyBranchId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@CompanyBranchName", companyBranch.CompanyBranchName);
            parameters.Add("@CompanyBranchHead", companyBranch.CompanyBranchHead);
            parameters.Add("@ContactNumber", companyBranch.ContactNumber);
            parameters.Add("@Email", companyBranch.Email);
            parameters.Add("@AddressId", companyBranch.AddressId);
            parameters.Add("@AddressTypeId", companyBranch.AddressTypeId);
            parameters.Add("@CompanyId", companyBranch.CompanyId);
            parameters.Add("@CreatedBy", companyBranch.CreatedBy);
           

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CompanyBranchCreateResponseEntity>(CompanyBranchStoreProcedures.CreateCompanyBranch, parameters, commandType: CommandType.StoredProcedure);

            var companyBranchId = parameters.Get<int>("@CompanyBranchId");
            var createdCompanyBranch = new CompanyBranchCreateResponseEntity
            {
                CompanyBranchId = companyBranchId,
                CompanyBranchName = companyBranch.CompanyBranchName,
                CompanyBranchHead = companyBranch.CompanyBranchHead,
                ContactNumber = companyBranch.ContactNumber,
                Email = companyBranch.Email,
                AddressId = companyBranch.AddressId,
                AddressTypeId = companyBranch.AddressTypeId,
                CompanyId = companyBranch.CompanyId,
                CreatedBy = companyBranch.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = companyBranch.IsActive,
                IsDelete = result?.IsDelete
            };

            return createdCompanyBranch;
        }

        public async Task<CompanyBranchUpdateResponseEntity?> UpdateCompanyBranch(CompanyBranchUpdateRequestEntity companyBranch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyBranchId", companyBranch.CompanyBranchId);
            parameters.Add("@CompanyBranchName", companyBranch.CompanyBranchName);
            parameters.Add("@CompanyBranchHead", companyBranch.CompanyBranchHead);
            parameters.Add("@ContactNumber", companyBranch.ContactNumber);
            parameters.Add("@Email", companyBranch.Email);
            parameters.Add("@AddressId", companyBranch.AddressId);  // Ensure AddressId is added
            parameters.Add("@AddressTypeId", companyBranch.AddressTypeId);
            parameters.Add("@CompanyId", companyBranch.CompanyId);
            parameters.Add("@UpdatedBy", companyBranch.UpdatedBy);
            parameters.Add("@IsActive", companyBranch.IsActive);
            

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CompanyBranchUpdateResponseEntity>(CompanyBranchStoreProcedures.UpdateCompanyBranch, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.CompanyBranchId == -1)
            {
                return null;
            }

            var updatedCompanyBranch = new CompanyBranchUpdateResponseEntity
            {
                CompanyBranchId = companyBranch.CompanyBranchId,
                CompanyBranchName = companyBranch.CompanyBranchName,
                CompanyBranchHead = companyBranch.CompanyBranchHead,
                ContactNumber = companyBranch.ContactNumber,
                Email = companyBranch.Email,
                UpdatedAt = DateTime.Now,
                IsActive = companyBranch.IsActive,
                IsDelete = result?.IsDelete
            };

            return updatedCompanyBranch;
        }

        public async Task<int> DeleteCompanyBranch(CompanyBranchDeleteRequestEntity companyBranch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyBranchId", companyBranch.CompanyBranchId);
            var response = await _dbConnection.ExecuteScalarAsync<int>(CompanyBranchStoreProcedures.DeleteCompanyBranch, parameters, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
