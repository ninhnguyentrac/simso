ALTER TABLE [dbo].[tbl_SimSoDep_ClientIp]
    ADD CONSTRAINT [DF_tbl_SimSoDep_ClientIp_ViewTime] DEFAULT (getdate()) FOR [ViewTime];

