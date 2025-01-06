
CREATE PROCEDURE [dbo].[spTenancyRoleDelete]
@TenancyRoleId INT = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblTenancyRoles] WHERE TenancyRoleId = @TenancyRoleId)
    BEGIN
        SELECT -1 AS TenancyRoleId;
        RETURN;
    END
    DELETE FROM [dbo].[tblTenancyRoles] WHERE TenancyRoleId = @TenancyRoleId;
    SELECT @TenancyRoleId AS TenancyRoleId;
END;
GO

