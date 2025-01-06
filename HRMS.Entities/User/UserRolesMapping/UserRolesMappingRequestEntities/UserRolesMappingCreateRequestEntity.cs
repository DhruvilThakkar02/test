namespace HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities
{
    public class UserRolesMappingCreateRequestEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
    }
}
