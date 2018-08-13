ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_DaiLy] FOREIGN KEY ([MaDaiLy]) REFERENCES [dbo].[tbl_SimSoDep_DaiLy] ([MaDaiLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;

