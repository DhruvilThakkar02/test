CREATE TABLE [dbo].[tblCompanyBranch] (
    [CompanyBranchId]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyBranchName] VARCHAR (200) NOT NULL,
    [CompanyBranchHead] VARCHAR (100) NOT NULL,
    [ContactNumber]     VARCHAR (15)  NOT NULL,
    [Email]             VARCHAR (100) NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NOT NULL,
    [CompanyId]         INT           NOT NULL,
    [CreatedBy]         INT           NOT NULL,
    [UpdatedBy]         INT           NULL,
    [IsActive]          BIT           DEFAULT ((1)) NOT NULL,
    [IsDelete]          BIT           DEFAULT ((0)) NOT NULL,
    [CreatedAt]         DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedAt]         DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([CompanyBranchId] ASC)
);
GO

