
CREATE PROCEDURE [dbo].[spOrganizationUpdate]
    @OrganizationID INT,
    @OrganizationName NVARCHAR(255),
    @UpdatedBy INT,
    @IsActive BIT = NULL,
    @IsDelete BIT = NULL
AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the organization exists
         IF NOT EXISTS (SELECT 1 FROM [dbo].[tblOrganization] WHERE OrganizationID = @OrganizationID)
        BEGIN
            SELECT -1 AS OrganizationID;
            RETURN;
        END

        -- Update the organization record
        UPDATE [dbo].[tblOrganization]
        SET 
            OrganizationName = @OrganizationName,
            UpdatedAt = GETDATE(),-- Automatically set to current date and time
            UpdatedBy = @UpdatedBy,
            IsActive = @IsActive,
            IsDelete = @IsDelete
        WHERE OrganizationID = @OrganizationID;
    
    -- Commit the transaction
    COMMIT TRANSACTION;

    -- Return the updated organization details
	SELECT 
        OrganizationID,
        OrganizationName,
        CreatedBy,
        UpdatedBy,
        CreatedAt,
        UpdatedAt,
        IsActive,
        IsDelete
    FROM [dbo].[tblOrganization]
    WHERE (@OrganizationID IS NULL OR OrganizationID = @OrganizationID);
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

