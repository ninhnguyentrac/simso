ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich]
    ADD CONSTRAINT [FK_tbl_SimSoDep_KinhDich_tbl_SimSoDep_BatQuai1] FOREIGN KEY ([IdQueThuong]) REFERENCES [dbo].[tbl_SimSoDep_BatQuai] ([IdQueBatQuai]) ON DELETE NO ACTION ON UPDATE NO ACTION;

