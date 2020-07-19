CREATE PROCEDURE [dbo].[sp_CreateBill]
	@numbill INT,
	@create_at DATE,
	@total REAL,
	@discount INT
AS
Begin
	SET IDENTITY_INSERT Bills OFF
	INSERT Bills(numBill,Create_at,Total,Discount) VALUES  (@numbill,@create_at,@total,@discount)
	SET IDENTITY_INSERT Bills ON
END