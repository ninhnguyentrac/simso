ALTER TABLE [dbo].[tbl_SimSoDep_KhachHang]
    ADD CONSTRAINT [FK_KhachHang_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;

