create PROCEDURE [dbo].[spAddressTypeUpdate]
@AddressTypeId INT = NULL,
@AddressTypeName NVARCHAR(100) = NULL,
@IsActive BIT = NULL,
@IsDelete BIT = NULL,
@UpdatedBy INT = NULL

AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY

        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the user exists
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblAddressType] WHERE AddressTypeId = @AddressTypeId)
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT -1 AS AddressTypeId;
            RETURN;
        END
	
        -- Update user details
	    UPDATE [dbo].[tblAddressType]
	    SET AddressTypeName = @AddressTypeName,
	        IsActive = @IsActive,
            IsDelete = @IsDelete,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME()
          
	    WHERE AddressTypeId = @AddressTypeId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated user details
	    SELECT * FROM [dbo].[tblAddressType] WHERE (@AddressTypeId IS NULL OR AddressTypeId = @AddressTypeId);

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

