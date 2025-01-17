CREATE PROCEDURE [dbo].[spTenantRegistrationAdd]
 @SubdomainName NVARCHAR(100),
 @FirstName NVARCHAR(100),
 @LastName NVARCHAR(100),
 @UserName NVARCHAR(100),
 @Email NVARCHAR(255),
 @Password NVARCHAR(MAX),
 @UserRoleId INT OUTPUT, 
 @UserId INT OUTPUT,
 @SubdomainId INT OUTPUT,
 @TenantId INT OUTPUT,
 @OrganizationId INT OUTPUT,
 @DomainId INT OUTPUT 
AS
BEGIN
 SET NOCOUNT ON;

 BEGIN TRY
 BEGIN TRANSACTION;

 -- Insert into Subdomains
 INSERT INTO tblSubdomains (SubdomainName, DomainId, CreatedBy, CreatedAt, isActive, isDelete)
 VALUES (@SubdomainName, 1, -1,SYSDATETIME(), 0, 0);
 SET @SubdomainId = SCOPE_IDENTITY();
 SET @DomainId = 1; 

 -- Insert into Tenants
 INSERT INTO tblTenants (TenantName, SubdomainId, DomainId, OrganizationId, CreatedBy, CreatedAt, isActive, isDelete)
 VALUES (@SubdomainName, @SubdomainId, @DomainId, 1, -1, SYSDATETIME(), 0, 0);
 SET @TenantId = SCOPE_IDENTITY();
 SET @OrganizationId = 1;

 -- Set UserRoleId before inserting into tblUser
 SET @UserRoleId = 1;

 -- Insert into User
 INSERT INTO tblUser (FirstName, LastName, UserName, Email, Password, TenantId, UserRoleId, CreatedBy, CreatedAt, isActive, isDelete)
 VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @TenantId, @UserRoleId, -1, SYSDATETIME(), 0, 0);
 SET @UserId = SCOPE_IDENTITY();

 -- Insert into UserRoleMapping
 INSERT INTO tblUserRoleMapping (UserId, UserRoleId, CreatedBy, CreatedAt, IsActive, IsDelete)
 VALUES (@UserId, @UserRoleId, @UserId, SYSDATETIME(), 1, 0);

 -- Update created by fields
 UPDATE tblUser SET CreatedBy = @UserId WHERE UserId = @UserId;
 UPDATE tblTenants SET CreatedBy = @UserId WHERE TenantId = @TenantId;
 UPDATE tblSubdomains SET CreatedBy = @UserId WHERE SubdomainId = @SubdomainId;

 COMMIT TRANSACTION;
 END TRY
 BEGIN CATCH
 IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION;

 THROW;
 END CATCH;
END;
GO

