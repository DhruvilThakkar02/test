namespace HRMS.Dtos.User.UserRoleMapping.UserRoleMappingResponseDtos
{
    public class UserRoleMappingCreateResponseDto
    {
        public int UserRoleMappingId { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}
