ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTietSimSo]
    ADD CONSTRAINT [FK_tbl_SimSoDep_LoaiSimChiTietSimSo_tbl_SimSoDep_LoaiSimChiTiet] FOREIGN KEY ([MaLoaiSimChiTiet]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSimChiTiet] ([MaLoaiSimChiTiet]) ON DELETE NO ACTION ON UPDATE NO ACTION;

