namespace HRMS.Entities.User.UserRoleMapping.UserRoleMappingRequestEntities
{
    public class UserRoleMappingCreateRequestEntity
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
