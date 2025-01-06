
CREATE PROCEDURE [dbo].[spUserRolesDelete]
@UserRoleId  INT = NULL
AS
BEGIN
    -- Check if the RoleId exists in the table
    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUserRoles] WHERE UserRoleId  = @UserRoleId )
    BEGIN
        SELECT -1 AS RoleId; -- Return -1 if the role doesn't exist
        RETURN;
    END

    -- Delete the role with the specified RoleId
    DELETE FROM [dbo].[tblUserRoles] WHERE UserRoleId  = @UserRoleId ;

    -- Return the deleted RoleId as confirmation
    SELECT @UserRoleId  AS UserRoleId ;
END;
GO

