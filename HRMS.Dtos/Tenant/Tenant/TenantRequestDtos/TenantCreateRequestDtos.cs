namespace HRMS.Dtos.Tenant.Tenant.TenantRequestDtos
{
    public class TenantCreateRequestDtos
    {
        public int OrganizationId { get; set; }
        public int DomainId { get; set; }
        public int SubdomainId { get; set; }    
        public string? TenantName { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
