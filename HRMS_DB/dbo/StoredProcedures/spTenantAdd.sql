CREATE PROCEDURE [dbo].[spTenantAdd]
@TenantId INT OUTPUT,
@OrganizationId INT = NULL,
@DomainId INT = NULL,
@SubdomainId INT = NULL,
@TenantName NVARCHAR(55) = NULL,
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

        -- Insert the tenant record into tblTenant
        INSERT INTO [dbo].[tblTenants] (OrganizationId,DomainId,SubdomainId,TenantName,CreatedBy, UpdatedBy, IsActive, CreatedAt, UpdatedAt)
        VALUES (@OrganizationId,@DomainId,@SubdomainId,@TenantName,@CreatedBy, @UpdatedBy,@IsActive, SYSDATETIME(), SYSDATETIME());
        
        -- Capture the TenantId of the inserted record
        SET @TenantId = SCOPE_IDENTITY();
        
        SELECT * FROM [dbo].[tblTenants] WHERE TenantId = @TenantId;
        
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

