CREATE TABLE [dbo].[tbl_SimSoDep_LoaiGia] (
    [MaLoaiGia]       INT            IDENTITY (1, 1) NOT NULL,
    [TenLoaiGia]      NVARCHAR (300) NOT NULL,
    [MoTa]            NVARCHAR (500) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [GiaTu]           BIGINT         NULL,
    [Den]             BIGINT         NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);



