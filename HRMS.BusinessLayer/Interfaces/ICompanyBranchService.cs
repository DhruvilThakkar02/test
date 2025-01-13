using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;
using HRMS.Dtos.CompanyBranch.CompanyBranchResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ICompanyBranchService
    {
        Task<List<CompanyBranchReadResponseDto>> GetCompanyBranches();
        Task<CompanyBranchReadResponseDto?> GetCompanyBranchById(int? id);
        Task<CompanyBranchCreateResponseDto> CreateCompanyBranch(CompanyBranchCreateRequestDto dto);
        Task<CompanyBranchUpdateResponseDto> UpdateCompanyBranch(CompanyBranchUpdateRequestDto dto);
        Task<CompanyBranchDeleteResponseDto?> DeleteCompanyBranch(CompanyBranchDeleteRequestDto dto);
    }
}
