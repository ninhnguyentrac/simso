ALTER TABLE [dbo].[tbl_SimSoDep_DauSo]
    ADD CONSTRAINT [FK_tbl_SimSoDep_DauSo_tbl_SimSoDep_NhaMang] FOREIGN KEY ([MaNhaMang]) REFERENCES [dbo].[tbl_SimSoDep_NhaMang] ([MaNhaMang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

