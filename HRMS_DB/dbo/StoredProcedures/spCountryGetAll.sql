CREATE PROCEDURE [dbo].[spCountryGetAll]
AS
BEGIN
 -- Retrieve all records from the tblCountry table
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
 WHERE IsDelete = 0; 
END
GO

