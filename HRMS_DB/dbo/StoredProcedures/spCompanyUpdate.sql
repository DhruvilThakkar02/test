CREATE PROCEDURE [dbo].[spCompanyUpdate]
@CompanyId INT = NULL,
@CompanyName NVARCHAR(255) = NULL,
@Industry NVARCHAR(255) = NULL,
@CompanyType NVARCHAR(255) = NULL,
@FoundedDate DATE = NULL,
@NumberOfEmployees INT = NULL,
@WebsiteUrl NVARCHAR(255) = NULL,
@TaxNumber NVARCHAR(50) = NULL,
@GstNumber NVARCHAR(50) = NULL,
@PfNumber NVARCHAR(50) = NULL,
@PhoneNumber NVARCHAR(50) = NULL,
@Logo Nvarchar(MAX) = NULL,
@Email NVARCHAR(100) = NULL,
@AddressId INT = NULL,
@TenantId INT = NULL,
@UpdatedBy INT = NULL,
@IsActive BIT = NULL,
@IsDelete BIT = NULL
AS
BEGIN
SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblCompany] WHERE CompanyId = @CompanyId)
        BEGIN
            SELECT -1 AS CompanyId;
            RETURN;
        END
        UPDATE [dbo].[tblCompany]
        SET CompanyName = @CompanyName,
            Industry = @Industry,
            CompanyType = @CompanyType,
            FoundedDate = @FoundedDate,
            NumberOfEmployees = @NumberOfEmployees,
            WebsiteUrl = @WebsiteUrl,
            TaxNumber = @TaxNumber,
            GstNumber = @GstNumber,
            PfNumber = @PfNumber,
            PhoneNumber = @PhoneNumber,
            Logo = @Logo,
            Email = @Email,
            AddressId = @AddressId,
            TenantId = @TenantId,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME(),
            IsActive = @IsActive,
            IsDelete = @IsDelete
        WHERE CompanyId = @CompanyId;
        COMMIT TRANSACTION;
        SELECT * FROM [dbo].[tblCompany] WHERE (@CompanyId IS NULL OR CompanyId = @CompanyId);

    END TRY
    BEGIN CATCH
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

