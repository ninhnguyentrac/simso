﻿ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim]
    ADD CONSTRAINT [PK_tbl_SimSoDep_SimSoLoaiSim_1] PRIMARY KEY CLUSTERED ([MaLoaiSim] ASC, [MaSoDienThoai] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

