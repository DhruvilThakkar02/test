CREATE TABLE [dbo].[tblTenancyRoles] (
    [TenancyRoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [TenancyRoleName] VARCHAR (255) NOT NULL,
    [CreatedBy]       INT           NOT NULL,
    [UpdatedBy]       INT           NULL,
    [CreatedAt]       DATETIME      CONSTRAINT [DF__tmp_ms_xx__Creat__7073AF84] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]       DATETIME      CONSTRAINT [DF__tmp_ms_xx__Updat__7167D3BD] DEFAULT (getdate()) NULL,
    [IsActive]        BIT           CONSTRAINT [DF__tmp_ms_xx__IsAct__725BF7F6] DEFAULT ((0)) NULL,
    [IsDelete]        BIT           CONSTRAINT [DF__tmp_ms_xx__IsDel__73501C2F] DEFAULT ((0)) NULL,
    CONSTRAINT [PK__tmp_ms_x__643C727AC0B1D538] PRIMARY KEY CLUSTERED ([TenancyRoleId] ASC)
);
GO


