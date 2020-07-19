CREATE PROCEDURE [dbo].[sp_InsertRole]
	@id AS NVARCHAR(128),
	@name AS NVARCHAR(256)   
AS
BEGIN
	INSERT [dbo].[AspNetRoles]VALUES (@id,@name)
END