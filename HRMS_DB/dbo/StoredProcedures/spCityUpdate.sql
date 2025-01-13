CREATE PROCEDURE [dbo].[spCityUpdate]
    @CityId INT = NULL,
    @CityName NVARCHAR(50) = NULL,
    @IsActive BIT = NULL,
    @IsDelete BIT = NULL,
    @UpdatedBy INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the city exists
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblCity] WHERE CityId = @CityId)
        BEGIN
            SELECT -1 AS CityId;
            RETURN;
        END
        
        -- Update city details
        UPDATE [dbo].[tblCity]
        SET CityName = @CityName,
            IsActive = @IsActive,
            IsDelete = @IsDelete,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME()
        WHERE CityId = @CityId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated city details
        SELECT * FROM [dbo].[tblCity] WHERE (@CityId IS NULL OR CityId = @CityId);

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

