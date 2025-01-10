CREATE PROCEDURE [dbo].[spCompanyAdd]
@CompanyId INT OUTPUT,
@CompanyName NVARCHAR(100) = NULL,
@Industry NVARCHAR(100) = NULL,
@CompanyType NVARCHAR(100) = NULL,
@FoundedDate DATE = NULL,
@NumberOfEmployees INT = NULL,
@WebsiteUrl NVARCHAR(200) = NULL,
@TaxNumber NVARCHAR(50) = NULL,
@GstNumber NVARCHAR(50) = NULL,
@PfNumber NVARCHAR(50) = NULL,
@PhoneNumber NVARCHAR(15) = NULL,
@Logo NVARCHAR(Max) = NULL,
@Email NVARCHAR(100) = NULL,
@AddressId INT = NULL,
@TenantId INT = NULL,
@CreatedBy INT = NULL,
@IsActive BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF @CreatedBy IS NULL
        BEGIN
            RAISERROR ('CreatedBy cannot be NULL..', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;
        
        INSERT INTO [dbo].[tblCompany] (
            CompanyName, Industry, CompanyType, FoundedDate, NumberOfEmployees, 
            WebsiteUrl, TaxNumber, GstNumber, PfNumber, PhoneNumber, 
            Logo, Email, AddressId, TenantId, CreatedBy, IsActive, CreatedAt, UpdatedAt
        )
        VALUES (
            @CompanyName, @Industry, @CompanyType, @FoundedDate, @NumberOfEmployees, 
            @WebsiteUrl, @TaxNumber, @GstNumber, @PfNumber, @PhoneNumber, 
            @Logo, @Email, @AddressId, @TenantId, @CreatedBy, @IsActive, 
            SYSDATETIME(), SYSDATETIME()
        );

        SET @CompanyId = SCOPE_IDENTITY();
        
        SELECT * FROM [dbo].[tblCompany] WHERE CompanyId = @CompanyId;
        
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



