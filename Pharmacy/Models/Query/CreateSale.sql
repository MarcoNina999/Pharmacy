CREATE PROCEDURE [dbo].[sp_CreateSale]
	@id INT,
	@total REAL,
	@create_at DATE,
	@iva INT,
	@userId NVARCHAR(128)
AS
Begin
	SET IDENTITY_INSERT Sales ON
	INSERT Sales(Id,Total,Create_at,IVA,UserId) VALUES(@id,@total,@create_at,@iva,@userId)
	SET IDENTITY_INSERT Sales OFF
END