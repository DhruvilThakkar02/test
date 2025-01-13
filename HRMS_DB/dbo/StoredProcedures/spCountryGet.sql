CREATE PROCEDURE [dbo].[spCountryGet]
 @CountryId INT -- Input parameter for CountryID
AS
BEGIN
 -- Retrieve the country record based on the provided CountryID
 SELECT 
 CountryId,
 CountryName,
 CreatedBy,
 UpdatedBy,
		IsActive,
 IsDelete,
 CreatedAt,
 UpdatedAt
 FROM tblCountry
 WHERE CountryId = @CountryId
 AND IsDelete = 0; -- Optionally exclude logically deleted records (IsDelete = 0)
END
GO

