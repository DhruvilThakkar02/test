namespace HRMS.Entities.Tenant.TenancyRole.TenancyRoleRequestEntities
{
    public class TenancyRoleUpdateRequestEntity
    {
        public int TenancyRoleId { get; set; }
        public string TenancyRoleName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
