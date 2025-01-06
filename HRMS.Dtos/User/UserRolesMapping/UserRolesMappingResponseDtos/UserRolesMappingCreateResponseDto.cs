namespace HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos
{
    public class UserRolesMappingCreateResponseDto
    {
        public int UserRoleMappingId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}
