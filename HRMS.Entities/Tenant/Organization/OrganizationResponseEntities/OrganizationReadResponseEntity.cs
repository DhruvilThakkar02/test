namespace HRMS.Entities.Tenant.Organization.OrganizationResponseEntities
{
    public class OrganizationReadResponseEntity
    {
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
