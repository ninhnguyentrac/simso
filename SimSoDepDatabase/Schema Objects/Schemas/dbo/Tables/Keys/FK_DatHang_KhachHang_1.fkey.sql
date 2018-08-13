ALTER TABLE [dbo].[tbl_SimSoDep_DatHang]
    ADD CONSTRAINT [FK_DatHang_KhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [dbo].[tbl_SimSoDep_KhachHang] ([MaKhachHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

