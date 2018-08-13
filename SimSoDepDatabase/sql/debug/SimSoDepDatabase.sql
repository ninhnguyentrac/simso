/*
Deployment script for SimSoDepDatabase
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, NUMERIC_ROUNDABORT, QUOTED_IDENTIFIER OFF;


GO
:setvar DatabaseName "SimSoDepDatabase"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [SimSoDep], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\SimSoDep.mdf', SIZE = 4096 KB, FILEGROWTH = 1024 KB)
    LOG ON (NAME = [SimSoDep_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\SimSoDep_log.ldf', SIZE = 20096 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS OFF,
                ANSI_PADDING OFF,
                ANSI_WARNINGS OFF,
                ARITHABORT OFF,
                CONCAT_NULL_YIELDS_NULL OFF,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER OFF,
                ANSI_NULL_DEFAULT OFF,
                CURSOR_DEFAULT GLOBAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY CHECKSUM,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[tbl_Global_User]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_Global_User] (
    [UserId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50) NOT NULL,
    [PassWord] NVARCHAR (50) NOT NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_Global_User...';


GO
ALTER TABLE [dbo].[tbl_Global_User]
    ADD CONSTRAINT [PK_tbl_Global_User] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_BanMenh]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_BanMenh] (
    [IdBanMenh]  INT            IDENTITY (1, 1) NOT NULL,
    [Nam]        INT            NOT NULL,
    [IdNguHanh]  INT            NOT NULL,
    [TenBanMenh] NVARCHAR (100) NULL,
    [MoTa]       NVARCHAR (200) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_BanMenh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_BanMenh]
    ADD CONSTRAINT [PK_tbl_SimSoDep_BanMenh] PRIMARY KEY CLUSTERED ([IdBanMenh] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_BatQuai]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_BatQuai] (
    [IdQueBatQuai] INT            IDENTITY (1, 1) NOT NULL,
    [TenQue]       NVARCHAR (100) NOT NULL,
    [HinhQue]      NVARCHAR (20)  NULL,
    [PhienAm]      NVARCHAR (50)  NULL,
    [MoTa]         NVARCHAR (500) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_BatQuai...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_BatQuai]
    ADD CONSTRAINT [PK_tbl_SimSoDep_BatQuai] PRIMARY KEY CLUSTERED ([IdQueBatQuai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_ClientIp]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_ClientIp] (
    [IdClientIp] BIGINT        IDENTITY (1, 1) NOT NULL,
    [IpAddress]  NVARCHAR (50) NOT NULL,
    [ViewTime]   DATETIME      NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_ClientIp...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_ClientIp]
    ADD CONSTRAINT [PK_tbl_SimSoDep_ClientIp] PRIMARY KEY CLUSTERED ([IdClientIp] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_DaiLy]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_DaiLy] (
    [MaDaiLy]     INT            IDENTITY (1, 1) NOT NULL,
    [TenDaiLy]    NVARCHAR (200) NULL,
    [DiDong]      NCHAR (15)     NULL,
    [MayBan]      NCHAR (15)     NULL,
    [DiaChi]      NVARCHAR (500) NULL,
    [Email]       NVARCHAR (50)  NULL,
    [MaTinhThanh] INT            NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_DaiLy...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DaiLy]
    ADD CONSTRAINT [PK_tbl_SimSoDep_DaiLy] PRIMARY KEY CLUSTERED ([MaDaiLy] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_DaiLyTrietKhau]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_DaiLyTrietKhau] (
    [Id]        INT    IDENTITY (1, 1) NOT NULL,
    [GiaTu]     BIGINT NULL,
    [Den]       BIGINT NULL,
    [TrietKhau] INT    NULL,
    [MaDaiLy]   INT    NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_DaiLyTrietKhau...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DaiLyTrietKhau]
    ADD CONSTRAINT [PK_tbl_SimSoDep_DaiLyTrietKhau] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_DatHang]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_DatHang] (
    [MaDatHang]     INT      IDENTITY (1, 1) NOT NULL,
    [MaKhachHang]   INT      NOT NULL,
    [MaSoDienThoai] BIGINT   NOT NULL,
    [ThoiGian]      DATETIME NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_DatHang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [PK_DatHang] PRIMARY KEY CLUSTERED ([MaDatHang] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_DauSo]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_DauSo] (
    [MaDauSo]         INT            IDENTITY (1, 1) NOT NULL,
    [TenDauSo]        NVARCHAR (100) NOT NULL,
    [MoTa]            NVARCHAR (150) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [Status]          INT            NULL,
    [MaNhaMang]       INT            NULL,
    [SapXep]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_DauSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DauSo]
    ADD CONSTRAINT [PK_DauSo] PRIMARY KEY CLUSTERED ([MaDauSo] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_DauSoChiTiet]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] (
    [MaDauSoChiTiet]  INT            IDENTITY (1, 1) NOT NULL,
    [MaDauSo]         INT            NULL,
    [TenDauSoChiTiet] NVARCHAR (200) NOT NULL,
    [MoTa]            NVARCHAR (300) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [MaNhaMang]       INT            NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_DauSoChiTiet...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet]
    ADD CONSTRAINT [PK_DauSoChiTiet] PRIMARY KEY CLUSTERED ([MaDauSoChiTiet] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_KhachHang]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_KhachHang] (
    [MaKhachHang]  INT             IDENTITY (1, 1) NOT NULL,
    [TenKhachHang] NVARCHAR (300)  NULL,
    [DiaChi]       NVARCHAR (1000) NULL,
    [SoDienThoai]  NCHAR (15)      NOT NULL,
    [Email]        NVARCHAR (30)   NULL,
    [MaTinhThanh]  INT             NULL,
    [GhiChu]       NVARCHAR (MAX)  NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_KhachHang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_KhachHang]
    ADD CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED ([MaKhachHang] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_KinhDich]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
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


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_KinhDich...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich]
    ADD CONSTRAINT [PK_tbl_SimSoDep_KinhDich] PRIMARY KEY CLUSTERED ([IdKinhDich] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_LoaiDoDaiSo]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_LoaiDoDaiSo] (
    [MaLoaiDoDai]     INT            IDENTITY (1, 1) NOT NULL,
    [TenLoaiDoDai]    NVARCHAR (100) NOT NULL,
    [MoTa]            NVARCHAR (150) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_LoaiDoDaiSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiDoDaiSo]
    ADD CONSTRAINT [PK_LoaiDoDaiSo] PRIMARY KEY CLUSTERED ([MaLoaiDoDai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_LoaiGia]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_LoaiGia] (
    [MaLoaiGia]       INT            IDENTITY (1, 1) NOT NULL,
    [TenLoaiGia]      NVARCHAR (300) NOT NULL,
    [MoTa]            NVARCHAR (500) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [GiaTu]           BIGINT         NULL,
    [Den]             BIGINT         NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_LoaiGia...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiGia]
    ADD CONSTRAINT [PK_LoaiGia] PRIMARY KEY CLUSTERED ([MaLoaiGia] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_LoaiSim]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
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


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_LoaiSim...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSim]
    ADD CONSTRAINT [PK_LoaiSim] PRIMARY KEY CLUSTERED ([MaLoaiSim] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_LoaiSimChiTiet]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
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


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_LoaiSimChiTiet...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTiet]
    ADD CONSTRAINT [PK_LoaiSimChiTiet] PRIMARY KEY CLUSTERED ([MaLoaiSimChiTiet] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo] (
    [MaLoaiSimChiTiet] INT      NOT NULL,
    [MaSoDienThoai]    BIGINT   NOT NULL,
    [CreateDate]       DATETIME NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_LoaiSimChiTietSimSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo]
    ADD CONSTRAINT [PK_tbl_SimSoDep_LoaiSimChiTietSimSo] PRIMARY KEY CLUSTERED ([MaLoaiSimChiTiet] ASC, [MaSoDienThoai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_NguHanh]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_NguHanh] (
    [IdNguHanh] INT             NOT NULL,
    [Ten]       NVARCHAR (50)   NULL,
    [TuongSinh] INT             NULL,
    [TuongKhac] INT             NULL,
    [MoTa]      NVARCHAR (1000) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_NguHanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh]
    ADD CONSTRAINT [PK_tbl_SimSoDep_NguHanh] PRIMARY KEY CLUSTERED ([IdNguHanh] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_NhaMang]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_NhaMang] (
    [MaNhaMang]       INT            IDENTITY (1, 1) NOT NULL,
    [TenNhaMang]      NVARCHAR (50)  NOT NULL,
    [TenMoTaDayDu]    NVARCHAR (150) NULL,
    [MoTa]            NVARCHAR (200) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_NhaMang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_NhaMang]
    ADD CONSTRAINT [PK_NhaMang] PRIMARY KEY CLUSTERED ([MaNhaMang] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_SimSo]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
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


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_SimSo_1...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [PK_SimSo_1] PRIMARY KEY CLUSTERED ([MaSoDienThoai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_SimSoLoaiSim]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim] (
    [MaLoaiSim]     INT      NOT NULL,
    [MaSoDienThoai] BIGINT   NOT NULL,
    [CreateDate]    DATETIME NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_SimSoLoaiSim_1...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim]
    ADD CONSTRAINT [PK_tbl_SimSoDep_SimSoLoaiSim_1] PRIMARY KEY CLUSTERED ([MaLoaiSim] ASC, [MaSoDienThoai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_TangThem]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_TangThem] (
    [Id]       INT    IDENTITY (1, 1) NOT NULL,
    [GiaTu]    BIGINT NULL,
    [Den]      BIGINT NULL,
    [TangThem] INT    NULL,
    [MaDaiLy]  INT    NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_tbl_SimSoDep_TangThem...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_TangThem]
    ADD CONSTRAINT [PK_tbl_SimSoDep_TangThem] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[tbl_SimSoDep_TinhThanh]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
CREATE TABLE [dbo].[tbl_SimSoDep_TinhThanh] (
    [MaTinhThanh]     INT            IDENTITY (1, 1) NOT NULL,
    [TenTinhThanh]    NVARCHAR (300) NULL,
    [MoTa]            NVARCHAR (500) NULL,
    [Alias]           NVARCHAR (100) NULL,
    [EnumFixed]       NVARCHAR (100) NULL,
    [SapXep]          INT            NULL,
    [Status]          INT            NULL,
    [MetaDescription] NTEXT          NULL,
    [MetaKeyword]     NTEXT          NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating PK_TinhThanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_TinhThanh]
    ADD CONSTRAINT [PK_TinhThanh] PRIMARY KEY CLUSTERED ([MaTinhThanh] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating DF_tbl_SimSoDep_ClientIp_ViewTime...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_ClientIp]
    ADD CONSTRAINT [DF_tbl_SimSoDep_ClientIp_ViewTime] DEFAULT (getdate()) FOR [ViewTime];


GO
PRINT N'Creating DF_tbl_SimSoDep_DatHang_ThoiGian...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [DF_tbl_SimSoDep_DatHang_ThoiGian] DEFAULT (getdate()) FOR [ThoiGian];


GO
PRINT N'Creating FK_tbl_SimSoDep_BanMenh_tbl_SimSoDep_NguHanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_BanMenh] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_BanMenh_tbl_SimSoDep_NguHanh] FOREIGN KEY ([IdNguHanh]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_DaiLy_tbl_SimSoDep_TinhThanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DaiLy] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_DaiLy_tbl_SimSoDep_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_DaiLyTrietKhau_tbl_SimSoDep_DaiLy...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DaiLyTrietKhau] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_DaiLyTrietKhau_tbl_SimSoDep_DaiLy] FOREIGN KEY ([MaDaiLy]) REFERENCES [dbo].[tbl_SimSoDep_DaiLy] ([MaDaiLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_DatHang_KhachHang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DatHang] WITH NOCHECK
    ADD CONSTRAINT [FK_DatHang_KhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[tbl_SimSoDep_KhachHang] ([MaKhachHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_DatHang_SimSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DatHang] WITH NOCHECK
    ADD CONSTRAINT [FK_DatHang_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_DauSo_tbl_SimSoDep_NhaMang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DauSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_DauSo_tbl_SimSoDep_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_DauSoChiTiet_DauSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] WITH NOCHECK
    ADD CONSTRAINT [FK_DauSoChiTiet_DauSo] FOREIGN KEY ([MaDauSo]) REFERENCES [dbo].[tbl_SimSoDep_DauSo] ([MaDauSo]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_DauSoChiTiet_tbl_SimSoDep_NhaMang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_DauSoChiTiet_tbl_SimSoDep_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_KhachHang_TinhThanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_KhachHang] WITH NOCHECK
    ADD CONSTRAINT [FK_KhachHang_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai] FOREIGN KEY ([IdQueHa]) REFERENCES [dbo].[tbl_SimSoDep_BatQuai] ([IdQueBatQuai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai1...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai1] FOREIGN KEY ([IdQueThuong]) REFERENCES [dbo].[tbl_SimSoDep_BatQuai] ([IdQueBatQuai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_LoaiSimChiTiet_LoaiSim...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTiet] WITH NOCHECK
    ADD CONSTRAINT [FK_LoaiSimChiTiet_LoaiSim] FOREIGN KEY ([MaLoaiSim]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSim] ([MaLoaiSim]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_LoaiSimChiTiet...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_LoaiSimChiTiet] FOREIGN KEY ([MaLoaiSimChiTiet]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSimChiTiet] ([MaLoaiSimChiTiet]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_SimSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh] FOREIGN KEY ([TuongSinh]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh1...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh1] FOREIGN KEY ([TuongKhac]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_DauSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_DauSo] FOREIGN KEY ([MaDauSo]) REFERENCES [dbo].[tbl_SimSoDep_DauSo] ([MaDauSo]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_DauSoChiTiet...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_DauSoChiTiet] FOREIGN KEY ([MaDauSoChiTiet]) REFERENCES [dbo].[tbl_SimSoDep_DauSoChiTiet] ([MaDauSoChiTiet]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_LoaiDoDaiSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_LoaiDoDaiSo] FOREIGN KEY ([MaLoaiDoDai]) REFERENCES [dbo].[tbl_SimSoDep_LoaiDoDaiSo] ([MaLoaiDoDai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_LoaiGia...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_LoaiGia] FOREIGN KEY ([MaLoaiGia]) REFERENCES [dbo].[tbl_SimSoDep_LoaiGia] ([MaLoaiGia]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_NhaMang...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_SimSo_TinhThanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_SimSo_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_DaiLy...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_DaiLy] FOREIGN KEY ([MaDaiLy]) REFERENCES [dbo].[tbl_SimSoDep_DaiLy] ([MaDaiLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_KinhDich...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_KinhDich] FOREIGN KEY ([IdKinhDich]) REFERENCES [dbo].[tbl_SimSoDep_KinhDich] ([IdKinhDich]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_NguHanh...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_NguHanh] FOREIGN KEY ([IdNguHanh]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_LoaiSim...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_LoaiSim] FOREIGN KEY ([MaLoaiSim]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSim] ([MaLoaiSim]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_SimSo...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_tbl_SimSoDep_TangThem_tbl_SimSoDep_DaiLy...';


GO
ALTER TABLE [dbo].[tbl_SimSoDep_TangThem] WITH NOCHECK
    ADD CONSTRAINT [FK_tbl_SimSoDep_TangThem_tbl_SimSoDep_DaiLy] FOREIGN KEY ([MaDaiLy]) REFERENCES [dbo].[tbl_SimSoDep_DaiLy] ([MaDaiLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating [dbo].[sp_GetListSoPhongThuy]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		NinhNguyenTrac
-- Create date: 05/04/2013
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetListSoPhongThuy] 
-- Add the parameters for the stored procedure here
	@LoaiQue INT,
	@NguHanh INT,
	@LoaiAmDuong INT,
	@LoaiAmDuong1 INT,
	@SoNuoc INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	SELECT ss.MaSoDienThoai, ss.DinhDangHienThi, ss.GiaTien, ss.SoNuoc
	FROM   tbl_SimSoDep_KinhDich kd
	       JOIN tbl_SimSoDep_SimSo ss
	            ON  kd.IdKinhDich = ss.IdKinhDich
	WHERE  ss.LoaiAmDuong IN (@LoaiAmDuong, @LoaiAmDuong1)
	       AND kd.LoaiQue = @LoaiQue
	       AND ss.IdNguHanh = @NguHanh
	       AND ss.SoDienThoai LIKE '%8%'
	       AND ss.SoNuoc>=@SoNuoc
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
PRINT N'Creating [dbo].[sp_SearchSimSo]...';


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
-- =============================================
-- Author:		NinhNguyenTrac
-- Create date: 03/04/2103
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_SearchSimSo
-- Add the parameters for the stored procedure here
	@SearchString NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	SELECT ss.MaSoDienThoai,
	       ss.SoDienThoai,
	       ss.GiaTien,
	       nm.TenNhaMang AS TenNhaMang
	FROM   tbl_SimSoDep_SimSo ss
	       INNER JOIN tbl_SimSoDep_NhaMang nm
	            ON  ss.MaNhaMang = nm.MaNhaMang
	WHERE  ss.SoDienThoai LIKE '%'+@SearchString+'%'
END
GO
SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[tbl_SimSoDep_BanMenh] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_BanMenh_tbl_SimSoDep_NguHanh];

ALTER TABLE [dbo].[tbl_SimSoDep_DaiLy] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_DaiLy_tbl_SimSoDep_TinhThanh];

ALTER TABLE [dbo].[tbl_SimSoDep_DaiLyTrietKhau] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_DaiLyTrietKhau_tbl_SimSoDep_DaiLy];

ALTER TABLE [dbo].[tbl_SimSoDep_DatHang] WITH CHECK CHECK CONSTRAINT [FK_DatHang_KhachHang];

ALTER TABLE [dbo].[tbl_SimSoDep_DatHang] WITH CHECK CHECK CONSTRAINT [FK_DatHang_SimSo];

ALTER TABLE [dbo].[tbl_SimSoDep_DauSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_DauSo_tbl_SimSoDep_NhaMang];

ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] WITH CHECK CHECK CONSTRAINT [FK_DauSoChiTiet_DauSo];

ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_DauSoChiTiet_tbl_SimSoDep_NhaMang];

ALTER TABLE [dbo].[tbl_SimSoDep_KhachHang] WITH CHECK CHECK CONSTRAINT [FK_KhachHang_TinhThanh];

ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai];

ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai1];

ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTiet] WITH CHECK CHECK CONSTRAINT [FK_LoaiSimChiTiet_LoaiSim];

ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_LoaiSimChiTiet];

ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_SimSo];

ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh];

ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh1];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_DauSo];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_DauSoChiTiet];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_LoaiDoDaiSo];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_LoaiGia];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_NhaMang];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_SimSo_TinhThanh];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_DaiLy];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_KinhDich];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSo] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_NguHanh];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_LoaiSim];

ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_SimSo];

ALTER TABLE [dbo].[tbl_SimSoDep_TangThem] WITH CHECK CHECK CONSTRAINT [FK_tbl_SimSoDep_TangThem_tbl_SimSoDep_DaiLy];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
