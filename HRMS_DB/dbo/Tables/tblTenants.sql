CREATE TABLE [dbo].[tblTenants] (
    [TenantId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [OrganizationId] INT           NOT NULL,
    [TenantName]     NVARCHAR (55) NOT NULL,
    [CreatedBy]      INT           NOT NULL,
    [UpdatedBy]      INT           NULL,
    [CreatedAt]      DATETIME      CONSTRAINT [DF__tblTenant__Creat__55009F39] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]      DATETIME      CONSTRAINT [DF__tblTenant__Updat__55F4C372] DEFAULT (getdate()) NULL,
    [IsActive]       BIT           CONSTRAINT [DF__tblTenant__IsAct__56E8E7AB] DEFAULT ((0)) NULL,
    [IsDelete]       BIT           CONSTRAINT [DF__tblTenant__IsDel__57DD0BE4] DEFAULT ((0)) NULL,
    [DomainId]       BIGINT        CONSTRAINT [DF__tblTenant__Domai__58D1301D] DEFAULT ((1)) NULL,
    [SubdomainId]    BIGINT        CONSTRAINT [DF__tblTenant__Subdo__59C55456] DEFAULT ((1)) NULL,
    CONSTRAINT [PK__tblTenan__2E9B47E12A4C5E72] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__Creat__55009F39] DEFAULT (getdate()) FOR [CreatedAt];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__Domai__58D1301D] DEFAULT ((1)) FOR [DomainId];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__IsAct__56E8E7AB] DEFAULT ((0)) FOR [IsActive];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__IsDel__57DD0BE4] DEFAULT ((0)) FOR [IsDelete];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__Subdo__59C55456] DEFAULT ((1)) FOR [SubdomainId];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [DF__tblTenant__Updat__55F4C372] DEFAULT (getdate()) FOR [UpdatedAt];
GO


ALTER TABLE [dbo].[tblTenants]
    ADD CONSTRAINT [PK__tblTenan__2E9B47E12A4C5E72] PRIMARY KEY CLUSTERED ([TenantId] ASC);
GO

