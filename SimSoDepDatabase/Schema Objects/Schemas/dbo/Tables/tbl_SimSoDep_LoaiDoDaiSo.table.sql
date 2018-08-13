CREATE TABLE [dbo].[tbl_SimSoDep_LoaiDoDaiSo] (
    [MaLoaiDoDai]  INT            IDENTITY (1, 1) NOT NULL,
    [TenLoaiDoDai] NVARCHAR (100) NOT NULL,
    [MoTa]         NVARCHAR (150) NULL,
    [Alias]        NVARCHAR (100) NULL,
    [EnumFixed]    NVARCHAR (100) NULL,
    [SapXep]       INT            NULL,
    [Status]       INT            NULL
);

