CREATE TABLE [dbo].[tbl_SimSoDep_KhachHang] (
    [MaKhachHang]  INT             IDENTITY (1, 1) NOT NULL,
    [TenKhachHang] NVARCHAR (300)  NULL,
    [DiaChi]       NVARCHAR (1000) NULL,
    [SoDienThoai]  NCHAR (15)      NOT NULL,
    [Email]        NVARCHAR (30)   NULL,
    [MaTinhThanh]  INT             NULL,
    [GhiChu]       NVARCHAR (MAX)  NULL
);



