CREATE PROCEDURE [dbo].[sp_ListUsers]

AS
Begin
	SELECT *
	FROM AspNetUsers
END