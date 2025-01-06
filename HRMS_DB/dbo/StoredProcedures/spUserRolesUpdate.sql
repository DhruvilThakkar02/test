
CREATE PROCEDURE [dbo].[spUserRolesUpdate]
    @UserRoleId INT = NULL,
    @UserRoleName NVARCHAR(50) = NULL,
    @PermissionGroupId INT = NULL,
	@UpdatedBy INT = NULL,
    @IsActive BIT = NULL,
    @IsDelete BIT = NULL
   
AS
BEGIN
    -- Validate that the RoleId exists
    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUserRoles] WHERE UserRoleId  = @UserRoleId )
    BEGIN
        SELECT -1 AS RoleId; -- Indicate that the RoleId does not exist
        RETURN;
    END;

    -- Update the role information
    UPDATE [dbo].[tblUserRoles]
    SET UserRoleName = ISNULL(@UserRoleName, UserRoleName),
        PermissionGroupId = ISNULL(@PermissionGroupId, PermissionGroupId),
		 UpdatedBy = @UpdatedBy,
        UpdatedAt = SYSDATETIME(),
        IsActive = ISNULL(@IsActive, IsActive),
        IsDelete = ISNULL(@IsDelete, IsDelete)
       
    WHERE UserRoleId  = @UserRoleId ;

    -- Return the updated role information
    SELECT * FROM [dbo].[tblUserRoles] WHERE UserRoleId  = @UserRoleId ;
END;
GO

