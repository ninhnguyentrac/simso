ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_DauSoChiTiet] FOREIGN KEY ([MaDauSoChiTiet]) REFERENCES [dbo].[tbl_SimSoDep_DauSoChiTiet] ([MaDauSoChiTiet]) ON DELETE NO ACTION ON UPDATE NO ACTION;

