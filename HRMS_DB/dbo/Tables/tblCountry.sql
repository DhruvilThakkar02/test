CREATE TABLE [dbo].[tblCountry] (
    [CountryId]   INT           IDENTITY (1, 1) NOT NULL,
    [CountryName] VARCHAR (255) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [UpdatedBy]   INT           NULL,
    [CreatedAt]   DATETIME      NOT NULL,
    [UpdatedAt]   DATETIME      NULL,
    [IsActive]    BIT           NOT NULL,
    [IsDelete]    BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CountryId] ASC)
);
GO

