


CREATE PROCEDURE [dbo].[spStateAdd]
	@StateId INT OUTPUT , -- Output parameter to return the created StateID
 @StateName VARCHAR(255), -- Name of the state
	@CreatedBy INT, -- User who is creating the state
 @IsActive BIT -- Active status of the state (1 for active, 0 for inactive)
 
 
AS
BEGIN
 -- Insert a new state into the tblState table
 INSERT INTO tblState (StateName, IsActive, IsDelete, CreatedBy, CreatedAt, UpdatedAt)
 VALUES 
 (
 @StateName, -- The state name
 @IsActive, -- Active status
 0, -- IsDelete set to 0 for not deleted
 @CreatedBy, -- The user who is creating the state
 SYSDATETIME(), -- CreatedAt set to current timestamp
SYSDATETIME() -- UpdatedAt set to current timestamp
 );

 -- Return the StateID of the newly created state
 SELECT @StateId = SCOPE_IDENTITY(); -- Capture the ID of the last inserted row

 -- Optionally, print a success message
 PRINT 'State created successfully with StateID: ' + CAST(@StateId AS VARCHAR);
 
 -- Return the created StateID
 SELECT @StateId AS CreatedStateId;
END;
GO

