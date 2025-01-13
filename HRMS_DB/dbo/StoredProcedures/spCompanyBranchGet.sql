
CREATE PROCEDURE [spCompanyBranchGet]
    @CompanyBranchId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            CompanyBranchId,
            CompanyBranchName,
            CompanyBranchHead,
            ContactNumber,
            Email,
            AddressId,
            AddressTypeId,
            CompanyId,
            CreatedBy,
            UpdatedBy,
            IsActive,
            IsDelete,
            CreatedAt,
            UpdatedAt
        FROM tblCompanyBranch
        WHERE CompanyBranchId = @CompanyBranchId
          AND IsDelete = 0; -- Exclude soft-deleted records
    END TRY
    BEGIN CATCH
        -- Handle errors
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

