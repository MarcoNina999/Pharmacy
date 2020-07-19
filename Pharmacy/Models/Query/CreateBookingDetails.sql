CREATE PROCEDURE [dbo].[sp_CreateBookinDetails]
	@id INT,
	@subTotal REAL,
	@quantity INT,
	@bookingid INT,
	@productid INT
AS
Begin
	SET IDENTITY_INSERT BookingsDetails ON
	INSERT BookingsDetails(Id,SubTotal,Quantity,BookingId,ProductId) VALUES  (@id,@subTotal,@quantity,@bookingid,@productid)
	SET IDENTITY_INSERT BookingsDetails OFF
END