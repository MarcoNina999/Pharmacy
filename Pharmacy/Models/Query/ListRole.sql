CREATE PROCEDURE [dbo].[sp_ListRole]

AS
Begin
	SELECT [Name]
	FROM AspNetRoles
END