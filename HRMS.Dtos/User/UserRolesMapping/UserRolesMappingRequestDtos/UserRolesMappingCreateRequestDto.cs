namespace HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos
{
    public class UserRolesMappingCreateRequestDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
