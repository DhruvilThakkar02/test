namespace HRMS.Entities.Tenant.Tenant.TenantRequestEntities
{
    public class TenantCreateRequestEntity
    {
        public int OrganizationId { get; set; }
        public int DomainId { get; set; }
        public int SubdomainId { get; set; }
        public string? TenantName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
