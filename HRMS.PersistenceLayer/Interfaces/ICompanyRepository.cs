using HRMS.Entities.Tenant.Company.CompanyRequestEntities;
using HRMS.Entities.Tenant.Company.CompanyResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyReadResponseEntity>> GetCompanies();
        Task<CompanyReadResponseEntity?> GetCompanyById(int? companyId);
        Task<CompanyCreateResponseEntity> CreateCompany(CompanyCreateRequestEntity company);
        Task<CompanyUpdateResponseEntity?> UpdateCompany(CompanyUpdateRequestEntity company);
        Task<int> DeleteCompany(CompanyDeleteRequestEntity company);
    }
}
