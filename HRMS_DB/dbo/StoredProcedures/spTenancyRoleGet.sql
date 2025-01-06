
CREATE PROCEDURE [dbo].[spTenancyRoleGet]
@TenancyRoleId INT = NULL
AS
BEGIN
    SELECT TOP 1 * 
    FROM [dbo].[tblTenancyRoles] 
    WHERE (@TenancyRoleId IS NULL OR TenancyRoleId = @TenancyRoleId);
END;
GO

