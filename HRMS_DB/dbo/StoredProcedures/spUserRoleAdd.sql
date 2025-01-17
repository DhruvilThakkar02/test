CREATE PROCEDURE [dbo].[spUserRoleAdd]
@UserRoleId INT OUTPUT,
@UserRoleName NVARCHAR(255) = NULL,
@PermissionGroupId INT = NULL,
@CreatedBy INT = NULL,
@IsActive BIT = NULL

AS
BEGIN
SET NOCOUNT ON;

 BEGIN TRY
 -- Start transaction
 BEGIN TRANSACTION;

 -- Check if CreatedBy is provided
 IF @CreatedBy IS NULL

 BEGIN
 RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
 ROLLBACK TRANSACTION;
 RETURN;
 END;
 
 

 -- Insert the new role into the table
 INSERT INTO [dbo].[tblUserRole] 
 ([UserRoleName], [PermissionGroupId], [CreatedBy], [CreatedAt],  [IsActive])
 VALUES 
 (@UserRoleName, @PermissionGroupId, @CreatedBy,  SYSDATETIME(),  @IsActive);

 -- Retrieve the newly inserted UserRoleId
 SET @UserRoleId = SCOPE_IDENTITY();

 SELECT * FROM [dbo].[tblUserRole] WHERE UserRoleId = @UserRoleId;

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

