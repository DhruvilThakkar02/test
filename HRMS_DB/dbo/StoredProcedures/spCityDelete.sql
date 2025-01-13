CREATE PROCEDURE [dbo].[spCityDelete]
    @CityId INT = NULL
AS
BEGIN
    BEGIN TRY
        -- Check if CityId exists in tblCity
        IF NOT EXISTS (SELECT 1 FROM [dbo].tblCity WHERE CityId = @CityId)
        BEGIN
            -- Return -1 if CityId does not exist
            SELECT -1 AS CityId;
            RETURN;
        END

        -- Start transaction
        BEGIN TRANSACTION;

        -- Delete the city record from tblCity
        DELETE FROM [dbo].tblCity WHERE CityId = @CityId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the deleted CityId
        SELECT @CityId AS CityId;
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

