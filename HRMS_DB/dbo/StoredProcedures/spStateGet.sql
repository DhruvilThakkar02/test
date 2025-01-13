
CREATE PROCEDURE [dbo].[spStateGet]
 @StateId INT -- Input parameter to specify the StateID
AS
BEGIN
 -- Retrieve the state record by StateID
 SELECT 
* FROM tblState
 WHERE StateId = @StateId -- Filter by the given StateID
 
END;
GO

