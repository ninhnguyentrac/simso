ALTER TABLE [dbo].[tbl_SimSoDep_SimSoLoaiSim]
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSoLoaiSim_tbl_SimSoDep_SimSo] FOREIGN KEY ([MaSoDienThoai]) REFERENCES [dbo].[tbl_SimSoDep_SimSo] ([MaSoDienThoai]) ON DELETE NO ACTION ON UPDATE NO ACTION;

