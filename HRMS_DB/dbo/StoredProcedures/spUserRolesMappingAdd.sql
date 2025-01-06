
 CREATE PROCEDURE [dbo].[spUserRolesMappingAdd]
    @UserRoleMappingId INT OUTPUT,
    @UserId INT,
    @UserRoleId INT,
    @CreatedBy INT,
    @UpdatedBy INT = NULL,
    @IsActive BIT = 1,
    @IsDelete BIT = 0 
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate mandatory fields
    IF @CreatedBy IS NULL
    BEGIN
        RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
        RETURN;
    END;

    -- Set default value for UpdatedBy if NULL
    SET @UpdatedBy = ISNULL(@UpdatedBy, @CreatedBy);

    -- Insert the record
    INSERT INTO [dbo].[tblUserRoleMapping] (
        UserId, 
        UserRoleId, 
        CreatedBy, 
        UpdatedBy, 
        CreatedAt, 
        UpdatedAt, 
        IsActive, 
        IsDelete
    )
    VALUES (
        @UserId, 
        @UserRoleId, 
        @CreatedBy, 
        @UpdatedBy, 
        SYSDATETIME(), 
        SYSDATETIME(), 
        @IsActive, 
        @IsDelete
    );

    -- Return the new ID
    SET @UserRoleMappingId = SCOPE_IDENTITY();
END;
GO

