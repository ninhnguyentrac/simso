CREATE TABLE [dbo].[tbl_SimSoDep_BanMenh] (
    [IdBanMenh]  INT            IDENTITY (1, 1) NOT NULL,
    [Nam]        INT            NOT NULL,
    [IdNguHanh]  INT            NOT NULL,
    [TenBanMenh] NVARCHAR (100) NULL,
    [MoTa]       NVARCHAR (200) NULL
);

