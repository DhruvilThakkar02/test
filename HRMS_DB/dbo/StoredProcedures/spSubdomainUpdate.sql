CREATE PROCEDURE [dbo].[spSubdomainUpdate]
    @SubdomainId INT = NULL,
	@DomainId INT = NULL,
    @SubdomainName NVARCHAR(100) = NULL,
    @UpdatedBy INT = NULL,
	@IsActive bit = NULL,
	@IsDelete bit = NULL
AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the user exists
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblSubdomains] WHERE SubdomainId = @SubdomainId)
        BEGIN
            SELECT -1 AS SubdomainId;
            RETURN;
        END

    UPDATE [dbo].[tblSubdomains]
    SET		
        SubdomainName = @SubdomainName,		
        DomainId = @DomainId,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = SYSDATETIME(),
		IsActive = @IsActive,
		IsDelete = @IsDelete
    WHERE @SubdomainId   = @SubdomainId;

    -- Commit the transaction
    COMMIT TRANSACTION;

    -- Return the updated user details
	SELECT * FROM [dbo].[tblSubdomains] WHERE (@SubdomainId IS NULL OR SubdomainId = @SubdomainId);

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

