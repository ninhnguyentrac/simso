CREATE TABLE [dbo].[tbl_SimSoDep_NhaMang] (
    [MaNhaMang]       INT            IDENTITY (1, 1) NOT NULL,
    [TenNhaMang]      NVARCHAR (50)  NOT NULL,
    [TenMoTaDayDu]    NVARCHAR (150) NULL,
    [MoTa]            NVARCHAR (200) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);



