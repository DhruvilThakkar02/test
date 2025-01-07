CREATE PROCEDURE [dbo].[spCompanyBranchUpdate]
@CompanyBranchId INT, 
    @CompanyBranchName VARCHAR(200),
    @CompanyBranchHead VARCHAR(100),
    @ContactNumber VARCHAR(15),
    @Email VARCHAR(100),
    @AddressId INT,
    @AddressTypeId INT,
    @CompanyId INT,
    @UpdatedBy INT,
    @IsActive BIT

AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY

        -- Start transaction
        BEGIN TRANSACTION;

     -- Check if the user exists
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblCompanyBranch] WHERE CompanyBranchId = @CompanyBranchId)
        BEGIN
            SELECT -1 AS CompanyBranchId;
            RETURN;
        END

        UPDATE tblCompanyBranch
        SET 
            CompanyBranchName = @CompanyBranchName,
            CompanyBranchHead = @CompanyBranchHead,
            ContactNumber = @ContactNumber,
            Email = @Email,
            AddressId = @AddressId,
            AddressTypeId = @AddressTypeId,
            CompanyId = @CompanyId,
            UpdatedBy = @UpdatedBy,
            IsActive = @IsActive,  -- Update the status (active/inactive)
            UpdatedAt = GETDATE() 
        WHERE CompanyBranchId = @CompanyBranchId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated tblTenancyRoles details
	    SELECT * FROM [dbo].[tblCompanyBranch] WHERE (CompanyBranchId IS NULL OR CompanyBranchId = @CompanyBranchId);

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

