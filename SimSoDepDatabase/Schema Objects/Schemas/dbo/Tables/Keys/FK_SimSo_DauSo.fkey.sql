﻿ALTER TABLE [dbo].[tbl_SimSoDep_SimSo]
    ADD CONSTRAINT [FK_SimSo_DauSo] FOREIGN KEY ([MaDauSo]) REFERENCES [dbo].[tbl_SimSoDep_DauSo] ([MaDauSo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

