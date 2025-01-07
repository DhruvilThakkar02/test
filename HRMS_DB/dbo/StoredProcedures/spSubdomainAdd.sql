CREATE PROCEDURE [dbo].[spSubdomainAdd]
@SubdomainId INT OUTPUT,
@DomainId INT = NULL,
@SubdomainName NVARCHAR(50) = NULL,
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
        
        -- Insert the subdomain with timestamps
        INSERT INTO [dbo].[tblSubdomains] (DomainId,  SubdomainName, CreatedBy, UpdatedBy, CreatedAt, UpdatedAt, IsActive)
        VALUES (@DomainId, @SubdomainName, @CreatedBy, @UpdatedBy, SYSDATETIME(), SYSDATETIME(),@IsActive);

         -- Capture the UserId of the inserted record
        SET @SubdomainId = SCOPE_IDENTITY();
        
        SELECT * FROM [dbo].[tblSubdomains] WHERE SubdomainId = @SubdomainId;

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

