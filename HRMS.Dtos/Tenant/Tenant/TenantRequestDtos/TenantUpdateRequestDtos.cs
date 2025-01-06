namespace HRMS.Dtos.Tenant.Tenant.TenantRequestDtos
{
    public class TenantUpdateRequestDtos
    {
        public int TenantId { get; set; }
        public int OrganizationId { get; set; }
        public int DomainId { get; set; }
        public int SubdomainId { get; set; }
        public string? TenantName { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
