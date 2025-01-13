namespace HRMS.Entities.User.UserRole.UserRoleRequestEntities
{
    public class UserRoleCreateRequestEntity
    {
        public string UserRoleName { get; set; } = string.Empty;
        public int PermissionGroupId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
