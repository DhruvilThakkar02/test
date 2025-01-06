CREATE TABLE [dbo].[tblTenants] (
    [TenantId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [OrganizationId] INT           NOT NULL,
    [TenantName]     NVARCHAR (55) NOT NULL,
    [CreatedBy]      INT           NULL,
    [UpdatedBy]      INT           NULL,
    [CreatedAt]      DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedAt]      DATETIME      DEFAULT (getdate()) NULL,
    [IsActive]       BIT           DEFAULT ((0)) NULL,
    [IsDelete]       BIT           DEFAULT ((0)) NULL,
    [DomainId]       BIGINT        DEFAULT ((1)) NULL,
    [SubdomainId]    BIGINT        DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([TenantId] ASC)
);
GO

