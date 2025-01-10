
CREATE PROCEDURE [dbo].[spCompanyBranchDelete]
  @CompanyBranchId INT = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].tblCompanyBranch WHERE CompanyBranchId = @CompanyBranchId)
    BEGIN
        SELECT -1 AS CompanyBranchId;
        RETURN;
    END
    DELETE FROM [dbo].tblCompanyBranch WHERE CompanyBranchId = @CompanyBranchId;
    SELECT @CompanyBranchId AS CompanyBranchId;
END;
GO

