ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_tbl_SimSoDep_SimSo_tbl_SimSoDep_KinhDich] FOREIGN KEY ([IdKinhDich]) REFERENCES [dbo].[tbl_SimSoDep_KinhDich] ([IdKinhDich]) ON DELETE NO ACTION ON UPDATE NO ACTION;

