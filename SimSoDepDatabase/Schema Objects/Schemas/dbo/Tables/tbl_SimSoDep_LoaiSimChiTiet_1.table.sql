CREATE TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTiet] (
    [MaLoaiSimChiTiet]  INT            IDENTITY (1, 1) NOT NULL,
    [MaLoaiSim]         INT            NOT NULL,
    [TenLoaiSimChiTiet] NVARCHAR (200) NOT NULL,
    [MoTa]              NVARCHAR (300) NULL,
    [Alias]             NVARCHAR (100) NULL,
    [EnumFixed]         NVARCHAR (100) NULL,
    [SapXep]            INT            NULL,
    [Status]            INT            NULL,
    [MetaDescription]   NTEXT          NULL,
    [MetaKeyword]       NTEXT          NULL
);



