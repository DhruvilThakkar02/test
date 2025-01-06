CREATE PROCEDURE [dbo].[spUserRoleMappingUpdate]
    @UserRoleMappingId INT,
    @UserId INT = NULL,
    @UserRoleId INT = NULL,
    @UpdatedBy INT,
    @IsActive BIT = NULL,
    @IsDelete BIT = NULL
AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the UserRoleMappingId exists
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUserRoleMapping] WHERE UserRoleMappingId = @UserRoleMappingId)
        BEGIN
            SELECT -1 AS UserRoleMappingId;
            RETURN;
        END

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

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated record
        SELECT * FROM [dbo].[tblUserRoleMapping] 
        WHERE UserRoleMappingId = @UserRoleMappingId;

    END TRY
    BEGIN CATCH
        -- Rollback transaction in case of error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Capture and rethrow the error
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(), 
            @ErrorSeverity = ERROR_SEVERITY(), 
            @ErrorState = ERROR_STATE();

        PRINT 'Error: ' + @ErrorMessage;

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

