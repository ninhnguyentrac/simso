CREATE TABLE [dbo].[tbl_SimSoDep_SimSo] (
    [MaSoDienThoai]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [SoDienThoai]     CHAR (15)     NOT NULL,
    [DinhDangHienThi] NVARCHAR (50) NULL,
    [GiaTien]         BIGINT        NULL,
    [TrangThaiBan]    BIT           NULL,
    [TrangThaiCuMoi]  BIT           NULL,
    [TrangThaiTonTai] BIT           NULL,
    [MaNhaMang]       INT           NOT NULL,
    [MaLoaiDoDai]     INT           NULL,
    [MaDauSo]         INT           NULL,
    [MaDauSoChiTiet]  INT           NULL,
    [MaLoaiSim]       INT           NULL,
    [MaTinhThanh]     INT           NULL,
    [MaLoaiGia]       INT           NULL,
    [MaDaiLy]         INT           NULL,
    [TrietKhau]       INT           NULL,
    [IdNguHanh]       INT           NULL,
    [IdKinhDich]      INT           NULL,
    [LoaiAmDuong]     INT           NULL,
    [SoNuoc]          INT           NULL,
    [GiaBanDau]       BIGINT        NULL
);





