
CREATE PROCEDURE [dbo].[spUserRolesMappingUpdate]
    @UserRoleMappingId INT,
    @UserId INT = NULL,
    @UserRoleId INT = NULL,
    @UpdatedBy INT,
    @IsActive BIT = NULL,
    @IsDelete BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate mandatory fields
    IF @UserRoleMappingId IS NULL
    BEGIN
        RAISERROR ('UserRoleMappingId cannot be NULL.', 16, 1);
        RETURN;
    END;

    IF @UpdatedBy IS NULL
    BEGIN
        RAISERROR ('UpdatedBy cannot be NULL.', 16, 1);
        RETURN;
    END;

    -- Update the record
    UPDATE [dbo].[tblUserRoleMapping]
    SET 
        UserId = ISNULL(@UserId, UserId),
        UserRoleId = ISNULL(@UserRoleId, UserRoleId),
        UpdatedBy = @UpdatedBy,
        UpdatedAt = SYSDATETIME(),
        IsActive = ISNULL(@IsActive, IsActive),
        IsDelete = ISNULL(@IsDelete, IsDelete)
    WHERE UserRoleMappingId = @UserRoleMappingId;

    -- Check if the record was updated
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR ('No record found with the specified UserRoleMappingId.', 16, 1);
    END;
END;
GO

