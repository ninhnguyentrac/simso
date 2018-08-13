ALTER TABLE [dbo].[tbl_SimSoDep_TangThem]
    ADD CONSTRAINT [FK_tbl_SimSoDep_TangThem_tbl_SimSoDep_DaiLy] FOREIGN KEY ([MaDaiLy]) REFERENCES [dbo].[tbl_SimSoDep_DaiLy] ([MaDaiLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;

