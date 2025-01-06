
CREATE PROCEDURE [dbo].[spUserRolesGet]
@UserRoleId INT = NULL
AS
BEGIN
 -- Select a single role if RoleId is provided or all roles if RoleId is NULL
 SELECT * FROM  [dbo].[tblUserRoles] WHERE (@UserRoleId IS NULL OR [UserRoleId] = @UserRoleId )
END;
GO

