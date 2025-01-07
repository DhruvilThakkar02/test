CREATE TABLE [dbo].[tblUser] (
    [UserId]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (50)  NOT NULL,
    [LastName]      NVARCHAR (50)  NOT NULL,
    [UserName]      NVARCHAR (50)  NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [Password]      NVARCHAR (50)  NOT NULL,
    [Gender]        NVARCHAR (50)  NULL,
    [DateOfBirth]   DATE           NULL,
    [CreatedAt]     DATETIME2 (3)  CONSTRAINT [DF__tblUser__Created__37A5467C] DEFAULT (sysdatetime()) NOT NULL,
    [UpdatedAt]     DATETIME2 (3)  CONSTRAINT [DF__tblUser__Updated__38996AB5] DEFAULT (sysdatetime()) NOT NULL,
    [IsActive]      BIT            NULL,
    [CreatedBy]     INT            CONSTRAINT [DF_tblUser_CreatedBy] DEFAULT ((1)) NOT NULL,
    [UpdatedBy]     INT            NULL,
    [TenantId]      INT            NULL,
    [MiddleName]    NVARCHAR (100) NULL,
    [IsDelete]      BIT            CONSTRAINT [DF_tblUser_IsDelete] DEFAULT ((0)) NULL,
    [UserRoleId]    INT            NULL,
    [TenancyRoleId] INT            NULL,
    CONSTRAINT [PK__tblUser__1788CC4C5F945034] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
GO




