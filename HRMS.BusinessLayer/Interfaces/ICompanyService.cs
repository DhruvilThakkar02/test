using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Company.CompanyResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyReadResponseDto>> GetCompanys();
        Task<CompanyReadResponseDto?> GetCompanyById(int? companyId);
        Task<CompanyCreateResponseDto> CreateCompany(CompanyCreateRequestDto dto);
        Task<CompanyUpdateResponseDto> UpdateCompany(CompanyUpdateRequestDto companyDto);
        Task<CompanyDeleteResponseDto?> DeleteCompany(CompanyDeleteRequestDto companyDto);
    }
}
