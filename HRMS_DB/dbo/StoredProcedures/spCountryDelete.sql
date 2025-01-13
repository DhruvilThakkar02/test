CREATE PROCEDURE [dbo].[spCountryDelete]
 @CountryId INT -- CountryID of the country to be deleted
AS
BEGIN
 -- Check if the country exists
 IF NOT EXISTS (SELECT 1 FROM tblCountry WHERE CountryId = @CountryId)
 BEGIN
		ROLLBACK TRANSACTION;
 PRINT 'Country not found.';
 RETURN; -- Exit if the country does not exist
 END

 -- Delete the country record
 DELETE FROM tblCountry WHERE CountryId = @CountryId;

 -- Optionally, print a success message
 PRINT 'Country deleted successfully.';
END
GO

