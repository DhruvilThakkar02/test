

CREATE PROCEDURE [dbo].[spCountryUpdate]
 @CountryId INT, -- CountryID to identify the record to update
 @CountryName VARCHAR(255), -- New name of the country
 @UpdatedBy INT, -- ID of the user who is updating the country
 @IsActive BIT, -- New status of the country (active/inactive)
 @IsDelete BIT = NULL -- Logical delete flag (optional, default to NULL)
 
AS
BEGIN
 SET NOCOUNT ON;

 BEGIN TRY
 BEGIN TRANSACTION;

 -- Ensure the country exists and is not logically deleted
 IF NOT EXISTS (SELECT 1 FROM tblCountry WHERE CountryId = @CountryId AND IsDelete = 0)
 BEGIN
			ROLLBACK TRANSACTION;
 SELECT -1 AS CountryId; -- Indicate failure if country does not exist or is deleted
 RETURN;
 END

 -- Update the country record
 UPDATE tblCountry
 SET
 CountryName = @CountryName, -- Update the CountryName
 UpdatedBy = @UpdatedBy, -- Update the UpdatedBy field
 IsActive = @IsActive, -- Update the IsActive flag
 UpdatedAt = SYSDATETIME(), -- Update the UpdatedAt field with the current timestamp
 IsDelete = ISNULL(@IsDelete, IsDelete) -- Update the IsDelete flag (if provided)
 WHERE CountryId = @CountryId
 AND IsDelete = 0; -- Ensure only non-deleted records are updated

 COMMIT TRANSACTION;

 -- Capture the updated CountryID into the output parameter
 

 -- Optionally, return the updated country record
 SELECT * FROM tblCountry WHERE CountryId = @CountryId;

 -- Optionally, print a success message
 PRINT 'Country updated successfully.';

 END TRY
 BEGIN CATCH
 -- Rollback the transaction in case of error
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION;

 -- Declare error variables
 DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;

 -- Capture the error details
 SELECT 
 @ErrorMessage = ERROR_MESSAGE(), 
 @ErrorSeverity = ERROR_SEVERITY(), 
 @ErrorState = ERROR_STATE();

 -- Print the error message
 PRINT 'Error: ' + @ErrorMessage;

 -- Raise the error to propagate it
 RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);

 END CATCH
END;
GO

