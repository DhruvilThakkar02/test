create PROCEDURE [dbo].[spUserRoleMappingDelete]
@UserRoleMappingId INT = NULL
AS
BEGIN
 -- Check if the RoleId exists in the table
 IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUserRoleMapping] WHERE UserRoleMappingId = @UserRoleMappingId )
 BEGIN
 SELECT -1 AS UserRoleMappingId; -- Return -1 if the role doesn't exist
 RETURN;
 END

 -- Delete the role with the specified RoleId
 DELETE FROM [dbo].[tblUserRoleMapping] WHERE UserRoleMappingId = @UserRoleMappingId ;

 -- Return the deleted RoleId as confirmation
 SELECT @UserRoleMappingId AS UserRoleMappingId ;
END;
GO

