CREATE PROCEDURE [dbo].[spUserGet]
@UserId INT = NULL
AS
BEGIN
 SET NOCOUNT ON;

    BEGIN TRY
    
        -- Start transaction
        BEGIN TRANSACTION;

    -- Select the user record or all records from tblUser based on the UserId
	SELECT TOP 1 * 
    FROM [dbo].[tblUser] 
    WHERE (@UserId IS NULL OR UserId = @UserId);

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
            @ErrorState = ERROR_STATE()
        
        PRINT 'Error: ' + @ErrorMessage;

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

