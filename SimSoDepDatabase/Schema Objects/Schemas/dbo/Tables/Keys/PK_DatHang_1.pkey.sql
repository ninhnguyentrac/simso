﻿ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [PK_DatHang] PRIMARY KEY CLUSTERED ([MaDatHang] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

