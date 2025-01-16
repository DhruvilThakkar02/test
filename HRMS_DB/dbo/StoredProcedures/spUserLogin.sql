CREATE PROCEDURE [dbo].[spUserLogin]
    @SubdomainName NVARCHAR(255),
    @UserNameOrEmail NVARCHAR(255),
    @Password NVARCHAR(255),
    @UserId INT OUTPUT,
    @UserName NVARCHAR(255) OUTPUT,
    @TenantId INT OUTPUT,
    @UserRoleId INT OUTPUT,
    @UserRoleName NVARCHAR(255) OUTPUT,
    @StoredPasswordHash NVARCHAR(255) OUTPUT,
    @ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the subdomain is active
    IF EXISTS (SELECT 1 FROM tblSubdomains WHERE SubdomainName = @SubdomainName AND IsActive = 1)
    BEGIN
        -- Fetch user details based on username/email
        SELECT 
            @UserId = u.UserId,
            @UserName = u.UserName,
            @StoredPasswordHash = u.Password,
            @TenantId = u.TenantId,
            @UserRoleId = u.UserRoleId,
            @UserRoleName = ur.UserRoleName
        FROM tblUser u
        INNER JOIN tblSubdomains s ON u.TenantId = s.SubdomainId
        LEFT JOIN tblUserRole ur ON u.UserRoleId = ur.UserRoleId
        WHERE (u.UserName = @UserNameOrEmail OR u.Email = @UserNameOrEmail)
          AND s.SubdomainName = @SubdomainName
          AND s.IsActive = 1;

        -- Check if user exists
        IF @UserId IS NOT NULL
        BEGIN
            -- Return the user details
            SELECT 
                @UserId AS UserId,
                @UserName AS UserName,
                @TenantId AS TenantId,
                @UserRoleId AS UserRoleId,
                @UserRoleName AS UserRoleName;
        END
        ELSE
        BEGIN
            -- User not found
            SET @ErrorMessage = 'Invalid User';
        END
    END
    ELSE
    BEGIN
        -- Invalid subdomain
        SET @ErrorMessage = 'Invalid subdomain';
    END

    -- Return error message if any
    IF @ErrorMessage IS NOT NULL
    BEGIN
        SELECT @ErrorMessage AS ErrorMessage;
    END
END
GO

