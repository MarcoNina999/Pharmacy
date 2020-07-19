CREATE PROCEDURE [dbo].[sp_RandomImage]

AS
Begin
	SELECT TOP 9 *
	FROM [Products] 
	ORDER BY newid();
END