
Create PROCEDURE [dbo].[spCompanyDelete]
@CompanyId INT = NULL
AS
BEGIN

    BEGIN TRY
        -- Check if UserId exists in tblUser
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblCompany] WHERE CompanyId = @CompanyId)
        BEGIN
            -- Return -1 if UserId does not exist
            SELECT -1 AS CompanyId;
            RETURN;
        END

        -- Start transaction
        BEGIN TRANSACTION;

        -- Delete the user record from tblUser
	    DELETE FROM [dbo].[tblCompany] WHERE CompanyId = @CompanyId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the deleted UserId
        SELECT @CompanyId AS CompanyId;
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

