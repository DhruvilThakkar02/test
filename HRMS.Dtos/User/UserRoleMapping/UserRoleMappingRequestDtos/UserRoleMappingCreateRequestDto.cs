namespace HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos
{
    public class UserRoleMappingCreateRequestDto
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
