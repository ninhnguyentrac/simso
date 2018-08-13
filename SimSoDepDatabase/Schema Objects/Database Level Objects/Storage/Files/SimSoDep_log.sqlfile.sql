ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [SimSoDep_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\SimSoDep_log.ldf', SIZE = 20096 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

