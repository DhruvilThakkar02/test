CREATE TABLE [dbo].[tblSubdomains] (
    [SubdomainId]   INT            IDENTITY (1, 1) NOT NULL,
    [SubdomainName] NVARCHAR (100) NULL,
    [CreatedBy]     INT            NOT NULL,
    [UpdatedBy]     INT            NULL,
    [CreatedAt]     DATETIME       NULL,
    [UpdatedAt]     DATETIME       NULL,
    [IsActive]      BIT            CONSTRAINT [DF_Subdomains_IsActive] DEFAULT ((0)) NULL,
    [IsDelete]      BIT            CONSTRAINT [DF_Subdomains_IsDelete] DEFAULT ((0)) NULL,
    [DomainId]      INT            NOT NULL
);
GO


