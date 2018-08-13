ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet]
    ADD CONSTRAINT [FK_tbl_SimSoDep_DauSoChiTiet_tbl_SimSoDep_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

