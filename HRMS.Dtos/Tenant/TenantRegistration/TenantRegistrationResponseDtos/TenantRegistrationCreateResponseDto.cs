namespace HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationResponseDtos
{
    public class TenantRegistrationCreateResponseDto
    {
        public int UserId { get; set; }
        public int SubdomainId { get; set; }
        public int TenantId { get; set; }
        public int OrganizationId { get; set; }
        public int DomainId { get; set; }
        public int UserRoleId { get; set; }
        public string SubdomainName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
       

    }
}
