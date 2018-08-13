ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo]
    ADD CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;

