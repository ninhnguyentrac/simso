CREATE TABLE [dbo].[tbl_SimSoDep_TinhThanh] (
    [MaTinhThanh]     INT            IDENTITY (1, 1) NOT NULL,
    [TenTinhThanh]    NVARCHAR (300) NULL,
    [MoTa]            NVARCHAR (500) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);



