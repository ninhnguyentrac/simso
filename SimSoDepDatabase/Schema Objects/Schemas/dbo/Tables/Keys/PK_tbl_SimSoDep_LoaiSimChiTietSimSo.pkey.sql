﻿ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo]
    ADD CONSTRAINT [PK_tbl_SimSoDep_LoaiSimChiTietSimSo] PRIMARY KEY CLUSTERED ([MaLoaiSimChiTiet] ASC, [MaSoDienThoai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
