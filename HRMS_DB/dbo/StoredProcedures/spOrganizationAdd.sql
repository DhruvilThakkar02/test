
CREATE PROCEDURE [dbo].[spOrganizationAdd]
@OrganizationID INT OUTPUT,
@OrganizationName NVARCHAR(100) = NULL,
@CreatedBy INT = NULL,

@IsActive BIT = NULL,
@CreatedAt DATETIME = NULL

AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if CreatedBy is provided
	    IF @CreatedBy IS NULL
        BEGIN
            RAISERROR ('CreatedBy cannot be NULL.', 16, 1);
            RETURN;
        END;


        -- Insert a new organization record
        INSERT INTO tblOrganization (OrganizationName, CreatedBy,  CreatedAt,  IsActive, IsDelete)
        VALUES (@OrganizationName, @CreatedBy, SYSDATETIME(),  1, 0);

        -- Capture the OrganizationID of the inserted record
        SET @OrganizationID = SCOPE_IDENTITY();

        SELECT * FROM [dbo].[tblOrganization] WHERE OrganizationID = @OrganizationID;

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

