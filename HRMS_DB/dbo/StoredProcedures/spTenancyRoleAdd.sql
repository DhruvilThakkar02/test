
CREATE PROCEDURE [dbo].[spTenancyRoleAdd]
@TenancyRoleId INT OUTPUT,
@TenancyRoleName VARCHAR(255) = NULL,
@CreatedBy INT = NULL,
@UpdatedBy INT = NULL,
@IsActive BIT = NULL

AS
BEGIN
SET NOCOUNT ON;

    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if CreatedBy is provided
        IF @CreatedBy IS NULL

        BEGIN
            RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;
     
        -- Set UpdatedBy to CreatedBy if not provided
        SET @UpdatedBy = ISNULL(@UpdatedBy, @CreatedBy);

        INSERT INTO [dbo].[tblTenancyRoles] (TenancyRoleName, CreatedBy, UpdatedBy, IsActive, CreatedAt, UpdatedAt)
        VALUES (@TenancyRoleName, @CreatedBy, @UpdatedBy, @IsActive, SYSDATETIME(), SYSDATETIME());

        SET @TenancyRoleId = SCOPE_IDENTITY();

        SELECT * FROM [dbo].[tblTenancyRoles] WHERE TenancyRoleId = @TenancyRoleId;

        -- Commit the transaction
        COMMIT TRANSACTION;

    END TRY
        BEGIN CATCH
            -- Handle errors and roll back the transaction if needed
            IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

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

