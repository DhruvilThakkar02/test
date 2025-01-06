namespace HRMS.Dtos.Tenant.TenancyRole.TenancyRoleResponseDtos
{
    public class TenancyRoleReadResponseDto
    {
        public int TenancyRoleId { get; set; }
        public string TenancyRoleName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

