CREATE PROCEDURE [dbo].[spCityAdd]
    @CityId INT OUTPUT, -- Output parameter to return the created CityID
    @CityName VARCHAR(255), -- Name of the city
    @CreatedBy INT, -- User who is creating the city
    @IsActive BIT -- Active status of the city (1 for active, 0 for inactive)
AS
BEGIN
    -- Insert a new city into the tblCity table
    INSERT INTO tblCity (CityName, IsActive, IsDelete, CreatedBy, CreatedAt)
    VALUES 
    (
        @CityName, -- The city name
        @IsActive, -- Active status
        0, -- IsDelete set to 0 for not deleted
        @CreatedBy, -- The user who is creating the city
        SYSDATETIME() -- CreatedAt set to current timestamp
       
    );

    -- Return the CityID of the newly created city
    SELECT @CityId = SCOPE_IDENTITY(); -- Capture the ID of the last inserted row

    -- Optionally, print a success message
    PRINT 'City created successfully with CityID: ' + CAST(@CityId AS VARCHAR);
    
    -- Return the created CityID
    SELECT @CityId AS CreatedCityId;
END;
GO

