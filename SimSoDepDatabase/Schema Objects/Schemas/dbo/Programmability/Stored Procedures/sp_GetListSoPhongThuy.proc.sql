-- =============================================
-- Author:		NinhNguyenTrac
-- Create date: 05/04/2013
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetListSoPhongThuy] 
-- Add the parameters for the stored procedure here
	@LoaiQue INT,
	@NguHanh INT,
	@LoaiAmDuong INT,
	@LoaiAmDuong1 INT,
	@SoNuoc INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	SELECT ss.MaSoDienThoai, ss.DinhDangHienThi, ss.GiaTien, ss.SoNuoc
	FROM   tbl_SimSoDep_KinhDich kd
	       JOIN tbl_SimSoDep_SimSo ss
	            ON  kd.IdKinhDich = ss.IdKinhDich
	WHERE  ss.LoaiAmDuong IN (@LoaiAmDuong, @LoaiAmDuong1)
	       AND kd.LoaiQue = @LoaiQue
	       AND ss.IdNguHanh = @NguHanh
	       AND ss.SoDienThoai LIKE '%8%'
	       AND ss.SoNuoc>=@SoNuoc
END