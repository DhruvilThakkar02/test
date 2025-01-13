
CREATE PROCEDURE [dbo].[spStateUpdate]
@StateId INT = NULL,
@StateName NVARCHAR(50) = NULL,
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
	 IF NOT EXISTS (SELECT 1 FROM [dbo].[tblState] WHERE StateId = @StateId)
 BEGIN
 SELECT -1 AS StateId;
 RETURN;
 END
	
 -- Update user details
	 UPDATE [dbo].[tblState]
	 SET StateName = @StateName,
	 IsActive = @IsActive,
 IsDelete = @IsDelete,
 UpdatedBy = @UpdatedBy,
 UpdatedAt = SYSDATETIME()
	 WHERE StateId = @StateId;

 -- Commit the transaction
 COMMIT TRANSACTION;

 -- Return the updated user details
	 SELECT * FROM [dbo].[tblState] WHERE (@StateId IS NULL OR StateId = @StateId);

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

