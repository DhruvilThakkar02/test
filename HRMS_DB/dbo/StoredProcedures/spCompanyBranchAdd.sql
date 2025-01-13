
CREATE PROCEDURE [dbo].[spCompanyBranchAdd]
    @CompanyBranchId INT OUTPUT, -- Added OUTPUT parameter for returning the new CompanyBranchId
    @CompanyBranchName VARCHAR(200),
    @CompanyBranchHead VARCHAR(100),
    @ContactNumber VARCHAR(15),
    @Email VARCHAR(100),
    @AddressId INT,
    @AddressTypeId INT,
    @CompanyId INT,
    @CreatedBy INT,
    @IsActive BIT = 1, -- Default value for IsActive is set to 1 (Active)
    @IsDelete BIT = 0 -- Default value for IsDelete is set to 0 (Not Deleted)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Start the transaction
        BEGIN TRANSACTION;

        -- Check if CreatedBy is provided
        IF @CreatedBy IS NULL
        BEGIN
            RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END;

      

        -- Insert the record into tblCompanyBranch
        INSERT INTO tblCompanyBranch (
            CompanyBranchName,
            CompanyBranchHead,
            ContactNumber,
            Email,
            AddressId,
            AddressTypeId,
            CompanyId,
            CreatedBy,
            IsActive,
            IsDelete,
            CreatedAt
        )
        VALUES (
            @CompanyBranchName,
            @CompanyBranchHead,
            @ContactNumber,
            @Email,
            @AddressId,
            @AddressTypeId,
            @CompanyId,
            @CreatedBy,
            @IsActive,
            @IsDelete,
            GETDATE() -- Current timestamp for CreatedAt
            
        );

        -- Capture the CompanyBranchId of the inserted record
        SET @CompanyBranchId = SCOPE_IDENTITY();

        -- Return the new record
        SELECT * FROM tblCompanyBranch WHERE CompanyBranchId = @CompanyBranchId;

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

