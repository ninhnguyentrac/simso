ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_LoaiGia] FOREIGN KEY ([MaLoaiGia]) REFERENCES [dbo].[tbl_SimSoDep_LoaiGia] ([MaLoaiGia]) ON DELETE NO ACTION ON UPDATE NO ACTION;

