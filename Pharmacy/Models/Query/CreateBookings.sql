CREATE PROCEDURE [dbo].[sp_CreateBookings]
	@id INT,
	@total REAL,
	@create_at DATE,
	@userId NVARCHAR(128)
AS
Begin
	SET IDENTITY_INSERT Bookings ON
	INSERT Bookings(Id,Total,Create_at,UserId) VALUES(@id,@total,@create_at,@userId)
	SET IDENTITY_INSERT Bookings OFF
END