ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_LoaiDoDaiSo] FOREIGN KEY ([MaLoaiDoDai]) REFERENCES [dbo].[tbl_SimSoDep_LoaiDoDaiSo] ([MaLoaiDoDai]) ON DELETE NO ACTION ON UPDATE NO ACTION;

