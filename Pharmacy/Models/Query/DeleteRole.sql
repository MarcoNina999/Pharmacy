CREATE PROCEDURE[dbo].[sp_DeleteRole]
	@id AS NVARCHAR(128)
AS
BEGIN
    DELETE FROM [dbo].[AspNetRoles]
	WHERE Id=@id
END