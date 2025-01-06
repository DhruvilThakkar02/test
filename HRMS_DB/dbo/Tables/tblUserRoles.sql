CREATE TABLE [dbo].[tblUserRoles] (
    [UserRoleId]        INT            IDENTITY (1, 1) NOT NULL,
    [UserRoleName]      NVARCHAR (255) NULL,
    [PermissionGroupId] INT            NULL,
    [CreatedBy]         INT            NOT NULL,
    [UpdatedBy]         INT            NULL,
    [CreatedAt]         DATETIME       NULL,
    [UpdatedAt]         DATETIME       NULL,
    [IsActive]          BIT            DEFAULT ((0)) NULL,
    [IsDelete]          BIT            DEFAULT ((0)) NULL
);
GO

