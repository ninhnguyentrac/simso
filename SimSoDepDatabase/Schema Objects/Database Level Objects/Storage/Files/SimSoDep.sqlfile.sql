﻿ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [SimSoDep], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\SimSoDep.mdf', SIZE = 4096 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

