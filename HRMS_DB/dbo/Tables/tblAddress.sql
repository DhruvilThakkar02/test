CREATE TABLE [dbo].[tblAddress] (
    [AddressId]     INT            IDENTITY (1, 1) NOT NULL,
    [AddressLine1]  NVARCHAR (100) NOT NULL,
    [AddressLine2]  NVARCHAR (100) NOT NULL,
    [CityId]        INT            NOT NULL,
    [StateId]       INT            NULL,
    [CountryId]     INT            NOT NULL,
    [PostalCode]    BIGINT         NULL,
    [AddressTypeId] INT            NOT NULL,
    [IsActive]      BIT            NULL,
    [IsDelete]      BIT            NULL,
    [CreatedBy]     INT            NOT NULL,
    [UpdatedBy]     INT            NULL,
    [CreatedAt]     DATETIME       NULL,
    [UpdatedAt]     DATETIME       NULL
);
GO

