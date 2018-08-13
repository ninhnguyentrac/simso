-- =============================================
-- Author:		NinhNguyenTrac
-- Create date: 02/06/2013
-- Description:	Thong ke sim
-- =============================================
CREATE PROCEDURE sp_ThongKeSim
	-- Add the parameters for the stored procedure here
	@SimVietttel int,
	@SimVina int,
	@SimMobi int,
	@SimVietnam int,
	@SimSfone int,
	@SimBeeline int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @numSimVietTel bigint
	declare @numSimMobi bigint
	declare @numSimVina bigint
	declare @numSimVietnam bigint
	declare @numSimSfone bigint
	declare @numSimBeeline bigint
	select @numSimVietTel = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimVietttel
	select @numSimMobi = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimMobi
	select @numSimVina = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimVina
	select @numSimVietnam = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimVietnam
	select @numSimSfone = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimSfone
	select @numSimBeeline = COUNT(*) from tbl_SimSoDep_SimSo where MaNhaMang = @SimBeeline
	
	select @numSimVietTel as NumViettel, @numSimMobi as NumMobi, @numSimVina as NumVina, 
		   @numSimBeeline as Numbeeline, @numSimSfone as NumSfone, @numSimVietnam as NumVietnam
END