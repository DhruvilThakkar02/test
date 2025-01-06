namespace HRMS.Dtos.User.UserRoles.UserRolesRequestDtos
{
    public class UserRolesCreateRequestDto
    {
        public string UserRoleName { get; set; } = string.Empty;
        public int PermissionGroupId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
