CREATE TABLE [dbo].[tblOrganization] (
    [OrganizationID]   INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationName] NVARCHAR (255) NOT NULL,
    [CreatedAt]        DATETIME2 (3)  NOT NULL,
    [UpdatedAt]        DATETIME2 (3)  NOT NULL,
    [CreatedBy]        INT            NOT NULL,
    [UpdatedBy]        INT            NULL,
    [IsActive]         BIT            DEFAULT ((0)) NULL,
    [IsDelete]         BIT            DEFAULT ((0)) NULL
);
GO

