
create PROCEDURE [dbo].[spAddressTypeDelete]
@AddressTypeId INT = NULL
AS
BEGIN

    BEGIN TRY
        -- Check if UserId exists in tblUser
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblAddressType] WHERE AddressTypeId = @AddressTypeId)
        BEGIN
            -- Return -1 if UserId does not exist
            ROLLBACK TRANSACTION
            SELECT -1 AS AddressTypeId;
            RETURN;
        END

        -- Start transaction
        BEGIN TRANSACTION;

        -- Delete the user record from tblUser
	    DELETE FROM [dbo].[tblAddressType] WHERE AddressTypeId = @AddressTypeId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the deleted UserId
        SELECT @AddressTypeId AS AddressTypeId;
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

