CREATE TABLE [dbo].[tbl_SimSoDep_LoaiSim] (
    [MaLoaiSim]       INT            IDENTITY (1, 1) NOT NULL,
    [TenLoaiSim]      NVARCHAR (200) NOT NULL,
    [MoTa]            NVARCHAR (300) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [Status]          INT            NULL,
    [SapXep]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);



