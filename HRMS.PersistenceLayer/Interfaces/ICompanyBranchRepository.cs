using HRMS.Entities.CompanyBranch.CompanyBranchRequestEntities;
using HRMS.Entities.CompanyBranch.CompanyBranchResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ICompanyBranchRepository
    {
        Task<IEnumerable<CompanyBranchReadResponseEntity>> GetCompanyBranches();
        Task<CompanyBranchReadResponseEntity?> GetCompanyBranchById(int? id);
        Task<CompanyBranchCreateResponseEntity> CreateCompanyBranch(CompanyBranchCreateRequestEntity companyBranch);
        Task<CompanyBranchUpdateResponseEntity?> UpdateCompanyBranch(CompanyBranchUpdateRequestEntity companyBranch);
        Task<int> DeleteCompanyBranch(CompanyBranchDeleteRequestEntity companyBranch);
    }
}
