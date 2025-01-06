namespace HRMS.Entities.Tenant.Organization.OrganizationRequestEntities
{
    public class OrganizationUpdateRequestEntity
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
