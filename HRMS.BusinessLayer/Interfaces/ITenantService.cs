using HRMS.Dtos.Tenant.Tenant.TenantRequestDtos;
using HRMS.Dtos.Tenant.Tenant.TenantResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantReadResponseDtos>> GetTenants();
        Task<TenantReadResponseDtos?> GetTenantById(int? tenantId);
        Task<TenantCreateResponseDtos> CreateTenant(TenantCreateRequestDtos tenantDto);
        Task<TenantUpdateResponseDtos> UpdateTenant(TenantUpdateRequestDtos tenantDto);
        Task<TenantDeleteResponseDtos?> DeleteTenant(TenantDeleteRequestDtos tenantDto);
    }
}
