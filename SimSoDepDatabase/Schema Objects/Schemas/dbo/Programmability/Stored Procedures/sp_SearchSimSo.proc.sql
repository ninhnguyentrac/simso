-- =============================================
-- Author:		NinhNguyenTrac
-- Create date: 03/04/2103
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_SearchSimSo
-- Add the parameters for the stored procedure here
	@SearchString NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	SELECT ss.MaSoDienThoai,
	       ss.SoDienThoai,
	       ss.GiaTien,
	       nm.TenNhaMang AS TenNhaMang
	FROM   tbl_SimSoDep_SimSo ss
	       INNER JOIN tbl_SimSoDep_NhaMang nm
	            ON  ss.MaNhaMang = nm.MaNhaMang
	WHERE  ss.SoDienThoai LIKE '%'+@SearchString+'%'
END