ALTER TABLE [dbo].[tbl_SimSoDep_DaiLy]
    ADD CONSTRAINT [FK_tbl_SimSoDep_DaiLy_tbl_SimSoDep_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;

