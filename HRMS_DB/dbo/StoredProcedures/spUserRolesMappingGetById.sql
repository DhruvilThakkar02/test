
CREATE PROCEDURE [dbo].[spUserRolesMappingGetById]
    @UserRoleMappingId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate mandatory field
    IF @UserRoleMappingId IS NULL
    BEGIN
        RAISERROR ('UserRoleMappingId cannot be NULL.', 16, 1);
        RETURN;
    END;

    
    SELECT 
        *
    FROM 
        [dbo].[tblUserRoleMapping]
    WHERE 
        UserRoleMappingId = @UserRoleMappingId;
END;
GO

