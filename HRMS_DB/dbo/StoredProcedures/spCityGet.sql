CREATE PROCEDURE [dbo].[spCityGet]
    @CityId INT 
AS
BEGIN
   
    SELECT 
       *
    FROM tblCity
    WHERE CityId = @CityId 
       
END;
GO

