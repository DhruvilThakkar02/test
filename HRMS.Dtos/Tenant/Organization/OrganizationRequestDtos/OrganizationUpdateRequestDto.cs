namespace HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos
{
    public class OrganizationUpdateRequestDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}