﻿ALTER TABLE [dbo].[tbl_SimSoDep_KinhDich]
    ADD CONSTRAINT [PK_tbl_SimSoDep_KinhDich] PRIMARY KEY CLUSTERED ([IdKinhDich] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
