﻿ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSim]
    ADD CONSTRAINT [PK_LoaiSim] PRIMARY KEY CLUSTERED ([MaLoaiSim] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

