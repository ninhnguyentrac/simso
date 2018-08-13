ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_TinhThanh] FOREIGN KEY ([MaTinhThanh]) REFERENCES [dbo].[tbl_SimSoDep_TinhThanh] ([MaTinhThanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;

