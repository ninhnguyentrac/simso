ALTER TABLE [dbo].[tbl_SimSoDep_NguHanh]
    ADD CONSTRAINT [FK_tbl_SimSoDep_NguHanh_tbl_SimSoDep_NguHanh] FOREIGN KEY ([TuongSinh]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;

