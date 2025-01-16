CREATE PROCEDURE [dbo].[spAddressUpdate]
@AddressId INT = NULL,
@AddressLine1 NVARCHAR(50) = NULL,
@AddressLine2 NVARCHAR(100) = NULL,
@CityId int = NULL,
@StateId int = NULL,
@CountryId int = NULL,
@PostalCode int = NULL,
@AddressTypeId int = NULL,
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
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblAddress] WHERE AddressId = @AddressId)
        BEGIN
        ROLLBACK TRANSACTION
            SELECT -1 AS AddressId;
            RETURN;
        END
	
        -- Update user details
	    UPDATE [dbo].[tblAddress]
	    SET AddressLine1 = @AddressLine1,
            AddressLine2 = @AddressLine2,
	       CityId = @CityId,
            StateId = @StateId,
	        CountryId = @CountryId,
	        PostalCode = @PostalCode,
            AddressTypeId = @AddressTypeId,
           
	        IsActive = @IsActive,
            IsDelete = @IsDelete,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME()
       
	    WHERE AddressId = @AddressId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated user details
	    SELECT * FROM [dbo].[tblAddress] WHERE (@AddressId IS NULL OR AddressId = @AddressId);

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

