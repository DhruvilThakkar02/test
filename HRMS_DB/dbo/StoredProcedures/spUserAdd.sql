
CREATE PROCEDURE [dbo].[spUserAdd]
@UserId INT OUTPUT,
@FirstName NVARCHAR(50) = NULL,
@MiddleName NVARCHAR(100) = NULL,
@LastName NVARCHAR(50) = NULL,
@UserName NVARCHAR(50) = NULL,
@Email NVARCHAR(50) = NULL,
@Password NVARCHAR(50) = NULL,
@Gender NVARCHAR(50) = NULL,
@DateOfBirth DATE = NULL,
@IsActive BIT = NULL,
@CreatedBy INT = NULL,
@UpdatedBy INT = NULL,
@TenantId INT = NULL,
@UserRoleId INT = NULL,
@TenancyRoleId INT = NULL
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
 
 -- Set UpdatedBy to CreatedBy if not provided
 SET @UpdatedBy = ISNULL(@UpdatedBy, @CreatedBy);

 -- Insert the user record into tblUser
 INSERT INTO [dbo].[tblUser] (FirstName, MiddleName, LastName, UserName, Email, Password, Gender, DateOfBirth, CreatedBy, UpdatedBy, IsActive, CreatedAt, UpdatedAt, TenantId, UserRoleId, TenancyRoleId)
 VALUES (@FirstName, @MiddleName, @LastName, @UserName, @Email, @Password, @Gender, @DateOfBirth, @CreatedBy, @UpdatedBy, @IsActive, SYSDATETIME(), SYSDATETIME(), @TenantId, @UserRoleId, @TenancyRoleId);

 -- Capture the UserId of the inserted record
 SET @UserId = SCOPE_IDENTITY();
 
 SELECT * FROM [dbo].[tblUser] WHERE UserId = @UserId;
 
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

