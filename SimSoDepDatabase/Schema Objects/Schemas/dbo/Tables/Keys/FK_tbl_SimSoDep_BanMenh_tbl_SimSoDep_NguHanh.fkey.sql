ALTER TABLE [dbo].[tbl_SimSoDep_BanMenh]
    ADD CONSTRAINT [FK_tbl_SimSoDep_BanMenh_tbl_SimSoDep_NguHanh] FOREIGN KEY ([IdNguHanh]) REFERENCES [dbo].[tbl_SimSoDep_NguHanh] ([IdNguHanh]) ON DELETE NO ACTION ON UPDATE NO ACTION;

