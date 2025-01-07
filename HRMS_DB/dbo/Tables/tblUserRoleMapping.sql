CREATE TABLE [dbo].[tblUserRoleMapping] (
    [UserRoleMappingId] INT      IDENTITY (1, 1) NOT NULL,
    [UserId]            INT      NOT NULL,
    [UserRoleId]        INT      NOT NULL,
    [CreatedBy]         INT      NOT NULL,
    [UpdatedBy]         INT      NULL,
    [CreatedAt]         DATETIME DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]         DATETIME NULL,
    [IsActive]          BIT      DEFAULT ((1)) NOT NULL,
    [IsDelete]          BIT      DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserRoleMappingId] ASC)
);
GO

