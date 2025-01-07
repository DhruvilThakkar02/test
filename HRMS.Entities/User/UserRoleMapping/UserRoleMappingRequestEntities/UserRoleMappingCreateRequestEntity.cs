namespace HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities
{
    public class UserRoleMappingCreateRequestEntity
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
