ALTER TABLE [dbo].[tbl_SimSoDep_LoaiSimChiTiet]
    ADD CONSTRAINT [FK_LoaiSimChiTiet_LoaiSim] FOREIGN KEY ([MaLoaiSim]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSim] ([MaLoaiSim]) ON DELETE NO ACTION ON UPDATE NO ACTION;

