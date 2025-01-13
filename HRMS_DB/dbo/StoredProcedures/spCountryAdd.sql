CREATE PROCEDURE [dbo].[spCountryAdd]
	@CountryId int OUTPUT,
 @CountryName VARCHAR(255), 
	@CreatedBy INT, 
 @IsActive BIT, 
 @IsDelete BIT = 0 
AS
BEGIN
 SET NOCOUNT ON;

 BEGIN TRY
 BEGIN TRANSACTION;
 IF @CreatedBy IS NULL
 BEGIN
 RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
 ROLLBACK TRANSACTION;
 RETURN;
 END;
 
 INSERT INTO tblCountry
 (CountryName, CreatedBy, IsActive, IsDelete, CreatedAt, UpdatedAt)
 VALUES
 (@CountryName, @CreatedBy, @IsActive, @IsDelete,SYSDATETIME(), SYSDATETIME());

 
 SET @CountryId = SCOPE_IDENTITY();
		 SELECT * FROM [dbo].[tblCountry] WHERE CountryId = @CountryId;
 COMMIT TRANSACTION;

 END TRY
 BEGIN CATCH
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

