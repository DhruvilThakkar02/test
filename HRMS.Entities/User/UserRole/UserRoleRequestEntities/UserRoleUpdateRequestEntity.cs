namespace HRMS.Entities.User.UserRole.UserRoleRequestEntities
{
    public class UserRoleUpdateRequestEntity
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;
        public int PermissionGroupId { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
