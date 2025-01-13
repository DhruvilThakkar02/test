namespace HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos
{
    public class UserRoleMappingCreateRequestDto
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
