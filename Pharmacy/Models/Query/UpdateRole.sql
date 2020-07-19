CREATE PROCEDURE[dbo].[sp_UpdateRole]
	@name AS NVARCHAR(256)
AS
BEGIN
    UPDATE [dbo].[AspNetRoles] SET  Name=@name
END