﻿namespace HRMS.Dtos.User.UserRoles.UserRolesResponseDtos
{
    public class UserRolesUpdateResponseDto
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;
        public int PermissionGroupId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
