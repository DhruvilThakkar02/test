
 CREATE PROCEDURE [dbo].[spUserRoleMappingAdd]
    @UserRoleMappingId INT OUTPUT,
    @UserId INT,
    @UserRoleId INT,
    @CreatedBy INT,
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

   

    -- Insert the record
    INSERT INTO [dbo].[tblUserRoleMapping] (
        UserId, 
        UserRoleId, 
        CreatedBy, 
        CreatedAt, 
        IsActive, 
        IsDelete
    )
    VALUES (
        @UserId, 
        @UserRoleId, 
        @CreatedBy,  
        SYSDATETIME(),  
        @IsActive, 
        @IsDelete
    );

    -- Return the new ID
    SET @UserRoleMappingId = SCOPE_IDENTITY();
END;
GO

