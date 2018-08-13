ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [FK_DatHang_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;

