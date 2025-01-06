namespace HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos
{
    public class UserRoleMappingUpdateRequestDto
    {
        public int UserRoleMappingId { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
