ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [DF_tbl_SimSoDep_DatHang_ThoiGian] DEFAULT (getdate()) FOR [ThoiGian];

