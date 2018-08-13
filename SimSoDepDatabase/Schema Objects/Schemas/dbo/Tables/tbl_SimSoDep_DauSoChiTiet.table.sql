CREATE TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] (
    [MaDauSoChiTiet]  INT            IDENTITY (1, 1) NOT NULL,
    [MaDauSo]         INT            NULL,
    [TenDauSoChiTiet] NVARCHAR (200) NOT NULL,
    [MoTa]            NVARCHAR (300) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [MaNhaMang]       INT            NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL
);

