ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

