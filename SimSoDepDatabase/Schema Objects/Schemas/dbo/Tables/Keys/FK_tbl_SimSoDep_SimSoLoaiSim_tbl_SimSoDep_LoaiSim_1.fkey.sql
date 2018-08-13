ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim]
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_LoaiSim] FOREIGN KEY ([MaLoaiSim]) REFERENCES [dbo].[tbl_SimSoDep_LoaiSim] ([MaLoaiSim]) ON DELETE NO ACTION ON UPDATE NO ACTION;

