
CREATE PROCEDURE [dbo].[spAddressTypeAdd]
    @AddressTypeId INT OUTPUT,
    @AddressTypeName NVARCHAR(100) = NULL,
    @IsActive BIT = NULL,
    @CreatedBy INT = NULL,
    @UpdatedBy INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if CreatedBy is provided
        IF @CreatedBy IS NULL
        BEGIN
            RAISERROR('CreatedBy cannot be NULL.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

        -- Insert address record
        INSERT INTO dbo.tblAddressType 
		(
            AddressTypeName,
            
            IsActive,
            CreatedBy,
			UpdatedBy,
            CreatedAt,
            UpdatedAt
          
        )
        VALUES (
            @AddressTypeName,
            
           
            @IsActive, 
            @CreatedBy,
			@UpdatedBy,
            SYSDATETIME(),
			SYSDATETIME()
        );

        -- Retrieve the new AddressId
        SET @AddressTypeId = SCOPE_IDENTITY();
		SELECT * FROM [dbo].[tblAddressType] WHERE AddressTypeId = @AddressTypeId;
 
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

