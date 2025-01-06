using HRMS.Entities.Tenant.Tenant.TenantRequestEntities;
using HRMS.Entities.Tenant.Tenant.TenantResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ITenantRepository
    {
        Task<IEnumerable<TenantReadResponseEntity>> GetTenants();
        Task<TenantReadResponseEntity?> GetTenantById(int? tenantId);
        Task<TenantCreateResponseEntity> CreateTenant(TenantCreateRequestEntity tenant);
        Task<TenantUpdateResponseEntity?> UpdateTenant(TenantUpdateRequestEntity tenant);
        Task<int> DeleteTenant(TenantDeleteRequestEntity tenant);
    }
}
