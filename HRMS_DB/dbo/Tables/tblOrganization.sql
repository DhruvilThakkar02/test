CREATE TABLE [dbo].[tblOrganization] (
    [OrganizationId]   INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationName] NVARCHAR (100) NOT NULL,
    [CreatedBy]        INT            NOT NULL,
    [UpdatedBy]        INT            NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [IsDelete]         BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt]        DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]        DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([OrganizationId] ASC)
);
GO

