CREATE TABLE [dbo].[tbl_SimSoDep_ClientIp] (
    [IdClientIp] BIGINT        IDENTITY (1, 1) NOT NULL,
    [IpAddress]  NVARCHAR (50) NOT NULL,
    [ViewTime]   DATETIME      NULL
);

