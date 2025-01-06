CREATE PROCEDURE [dbo].[spTenancyRoleUpdate]
@TenancyRoleId INT,
@TenancyRoleName NVARCHAR(100),
@UpdatedBy INT = NULL,
@IsActive BIT = NULL,
@IsDelete BIT = NULL

AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY

        -- Start transaction
        BEGIN TRANSACTION;

     -- Check if the user exists
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblTenancyRoles] WHERE TenancyRoleId = @TenancyRoleId)
        BEGIN
            SELECT -1 AS TenancyRoleId;
            RETURN;
        END

        UPDATE tblTenancyRoles
        SET 
            TenancyRoleName = @TenancyRoleName,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME(),
            IsActive = @IsActive,
            IsDelete = @IsDelete
        WHERE TenancyRoleId = @TenancyRoleId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated tblTenancyRoles details
	    SELECT * FROM [dbo].[tblTenancyRoles] WHERE (@TenancyRoleId IS NULL OR TenancyRoleId = @TenancyRoleId);

        END TRY
        BEGIN CATCH

            -- Handle errors and roll back the transaction if needed
            IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

            DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
            SELECT 
                @ErrorMessage = ERROR_MESSAGE(), 
                @ErrorSeverity = ERROR_SEVERITY(), 
                @ErrorState = ERROR_STATE()

            PRINT 'Error: ' + @ErrorMessage;

            RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
            
    END CATCH
END;
GO

