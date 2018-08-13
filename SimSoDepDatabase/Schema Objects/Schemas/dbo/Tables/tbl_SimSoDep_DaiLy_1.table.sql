CREATE TABLE [dbo].[tbl_SimSoDep_DaiLy] (
    [MaDaiLy]     INT            IDENTITY (1, 1) NOT NULL,
    [TenDaiLy]    NVARCHAR (200) NULL,
    [DiDong]      NCHAR (15)     NULL,
    [MayBan]      NCHAR (15)     NULL,
    [DiaChi]      NVARCHAR (500) NULL,
    [Email]       NVARCHAR (50)  NULL,
    [MaTinhThanh] INT            NULL
);

