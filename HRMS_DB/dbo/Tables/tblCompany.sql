CREATE TABLE [dbo].[tblCompany] (
    [CompanyId]         INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName]       VARCHAR (255) NOT NULL,
    [Industry]          VARCHAR (100) NULL,
    [CompanyType]       VARCHAR (50)  NULL,
    [FoundedDate]       DATE          NULL,
    [NumberOfEmployees] INT           NULL,
    [WebsiteURL]        VARCHAR (255) NULL,
    [TaxNumber]         VARCHAR (100) NULL,
    [GstNumber]         VARCHAR (100) NULL,
    [PfNumber]          VARCHAR (100) NULL,
    [PhoneNumber]       VARCHAR (15)  NULL,
    [Logo]              VARCHAR (255) NULL,
    [Email]             VARCHAR (100) NULL,
    [AddressId]         INT           NULL,
    [TenantId]          INT           NOT NULL,
    [CreatedBy]         INT           NOT NULL,
    [UpdatedBy]         INT           NULL,
    [CreatedAt]         DATETIME      NOT NULL,
    [UpdatedAt]         DATETIME      NULL,
    [IsActive]          BIT           NOT NULL,
    [IsDelete]          BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);
GO

