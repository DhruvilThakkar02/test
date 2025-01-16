CREATE TABLE [dbo].[tblAddressType] (
    [AddressTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [AddressTypeName] NVARCHAR (100) NOT NULL,
    [IsActive]        BIT            NULL,
    [IsDelete]        BIT            NULL,
    [CreatedBy]       INT            NOT NULL,
    [UpdatedBy]       INT            NULL,
    [CreatedAt]       DATETIME       NULL,
    [UpdatedAt]       DATETIME       NULL
);
GO

