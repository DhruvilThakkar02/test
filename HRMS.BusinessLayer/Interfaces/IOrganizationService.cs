using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IOrganizationService
    {
        Task<List<OrganizationReadResponseDto>> GetOrganizations();
        Task<OrganizationReadResponseDto?> GetOrganizationById(int? id);
        Task<OrganizationCreateResponseDto> CreateOrganization(OrganizationCreateRequestDto dto);
        Task<OrganizationUpdateResponseDto> UpdateOrganization(OrganizationUpdateRequestDto dto);
        Task<OrganizationDeleteResponseDto?> DeleteOrganization(OrganizationDeleteRequestDto dto);
    }
}
