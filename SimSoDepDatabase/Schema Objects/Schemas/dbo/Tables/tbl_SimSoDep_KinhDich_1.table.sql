CREATE TABLE [dbo].[tbl_SimSoDep_KinhDich] (
    [IdQueThuong] INT             NOT NULL,
    [IdQueHa]     INT             NOT NULL,
    [TenQue]      NVARCHAR (50)   NOT NULL,
    [PhienAm]     NVARCHAR (50)   NULL,
    [LoiDich]     NVARCHAR (2000) NULL,
    [YNghia]      NVARCHAR (3000) NULL,
    [HinhQue]     NVARCHAR (50)   NULL,
    [IdKinhDich]  INT             IDENTITY (1, 1) NOT NULL,
    [LoaiQue]     INT             NULL
);

