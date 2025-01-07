CREATE PROCEDURE [dbo].[spUserUpdate]
@UserId INT = NULL,
@FirstName NVARCHAR(50) = NULL,
@MiddleName NVARCHAR(100) = NULL,
@LastName NVARCHAR(50) = NULL,
@UserName NVARCHAR(50) = NULL,
@Email NVARCHAR(50) = NULL,
@Password NVARCHAR(50) = NULL,
@Gender NVARCHAR(50) = NULL,
@DateOfBirth DATE = NULL,
@IsActive BIT = NULL,
@IsDelete BIT = NULL,
@UpdatedBy INT = NULL,
@TenantId INT = NULL,
@UserRoleId INT = NULL,
@TenancyRoleId INT = NULL

AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY

        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the user exists
	    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUser] WHERE UserId = @UserId)
        BEGIN
            SELECT -1 AS UserId;
            RETURN;
        END
	
        -- Update user details
	    UPDATE [dbo].[tblUser]
	    SET FirstName = @FirstName,
            MiddleName = @MiddleName,
	        LastName = @LastName,
            UserName = @UserName,
	        Email = @Email,
	        Password = @Password,
            Gender = @Gender,
            DateOfBirth = @DateOfBirth,
	        IsActive = @IsActive,
            IsDelete = @IsDelete,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = SYSDATETIME(),
            TenantId = @TenantId,
            UserRoleId = @UserRoleId,
            TenancyRoleId = @TenancyRoleId
	    WHERE UserId = @UserId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Return the updated user details
	    SELECT * FROM [dbo].[tblUser] WHERE (@UserId IS NULL OR UserId = @UserId);

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

