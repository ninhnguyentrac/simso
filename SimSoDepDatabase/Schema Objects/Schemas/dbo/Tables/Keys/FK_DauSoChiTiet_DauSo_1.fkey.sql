ALTER TABLE [dbo].[tbl_SimSoDep_DauSoChiTiet]
    ADD CONSTRAINT [FK_DauSoChiTiet_DauSo] FOREIGN KEY ([MaDauSo]) REFERENCES [dbo].[tbl_SimSoDep_DauSo] ([MaDauSo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

