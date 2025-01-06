CREATE PROCEDURE [dbo].[spSubdomainGet]
 @SubdomainId INT
AS
BEGIN
 SELECT * FROM tblSubdomains WHERE SubdomainId = @SubdomainId;
END
GO

