namespace HRMS.Entities.Tenant.Organization.OrganizationRequestEntities
{
    public class OrganizationCreateRequestEntity
    {
        public string OrganizationName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
