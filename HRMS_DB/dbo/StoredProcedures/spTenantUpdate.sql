CREATE PROCEDURE [dbo].[spTenantUpdate]
@TenantId INT = NULL,
@OrganizationId INT = NULL,
@DomainId INT = NULL,
@SubdomainId INT = NULL,
@TenantName NVARCHAR(55) = NULL,
@UpdatedBy INT = NULL,
@IsActive BIT = NULL,
@IsDelete BIT = NULL

AS
BEGIN
 BEGIN TRY

 -- Start transaction
 BEGIN TRANSACTION;

 -- Check if the user exists
	 IF NOT EXISTS (SELECT 1 FROM [dbo].[tblTenants] WHERE TenantId = @TenantId)
	BEGIN	
 SELECT -1 AS TenantId;
 RETURN;
 END

	 UPDATE [dbo].[tblTenants]
        SET OrganizationId = @OrganizationId,
        DomainId = @DomainId,
        SubdomainId = @SubdomainId,
        TenantName = @TenantName,
        UpdatedBy = @UpdatedBy,
        IsActive = @IsActive,
        IsDelete = @IsDelete,
        UpdatedAt = SYSDATETIME()
 WHERE TenantId = @TenantId;

 -- Commit the transaction
 COMMIT TRANSACTION;

 SELECT * FROM [dbo].[tblTenants] WHERE (@TenantId IS NULL OR TenantId = @TenantId);
 
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
	END
GO

