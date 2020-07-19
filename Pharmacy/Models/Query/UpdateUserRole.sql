CREATE PROCEDURE[dbo].[sp_UpdateUserRole]
	@id AS NVARCHAR(256)
AS
BEGIN
    UPDATE [dbo].[AspNetUserRoles] SET  Roleid=@id
END