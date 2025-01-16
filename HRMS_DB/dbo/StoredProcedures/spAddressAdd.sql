CREATE PROCEDURE [dbo].[spAddressAdd]
    @AddressId INT OUTPUT,
    @AddressLine1 NVARCHAR(100) = NULL,
    @AddressLine2 NVARCHAR(100) = NULL,
    @CityId INT = NULL,
    @StateId INT = NULL,
    @CountryId INT = NULL,
    @PostalCode BIGINT = NULL,
	@AddressTypeId int =NULL,
    @IsActive BIT = 0,
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
        INSERT INTO dbo.tblAddress 
		(
            AddressLine1,
            AddressLine2,
            CityId,
            StateId,
            CountryId,
            PostalCode,
			AddressTypeId,
            IsActive,
            CreatedBy,
			
            CreatedAt
           
          
        )
        VALUES (
            @AddressLine1,
            @AddressLine2,
            @CityId,
            @StateId,
            @CountryId,
            @PostalCode,
			@AddressTypeId,
            @IsActive, 
            @CreatedBy,
			
            SYSDATETIME()
			
        );

        -- Retrieve the new AddressId
        SET @AddressId = SCOPE_IDENTITY();
		SELECT * FROM [dbo].[tblAddress] WHERE AddressId = @AddressId;
 
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

