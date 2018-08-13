CREATE TABLE [dbo].[tbl_SimSoDep_DauSo] (
    [MaDauSo]         INT            IDENTITY (1, 1) NOT NULL,
    [TenDauSo]        NVARCHAR (100) NOT NULL,
    [MoTa]            NVARCHAR (150) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [Status]          INT            NULL,
    [MaNhaMang]       INT            NULL,
    [SapXep]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);



