CREATE PROCEDURE [dbo].[sp_CreateSaleDetails]
	@id INT,
	@numbill INT,
	@subTotal REAL,
	@discount MONEY,
	@quantity INT,
	@saleid INT,
	@productid INT
AS
Begin
	SET IDENTITY_INSERT SaleDetails ON
	INSERT SaleDetails(Id,NumBill,SubTotal,Discount,Quantity,SaleId,ProductId) VALUES  (@id,@numbill,@subTotal,@discount,@quantity,@saleid,@productid)
	SET IDENTITY_INSERT SaleDetails OFF
END