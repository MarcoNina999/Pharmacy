CREATE PROCEDURE [dbo].[sp_ListUserRole]

AS
Begin
	SELECT [u].[First_Name], [u].[Last_Name], [u].[Ci],[r].[Name]
	FROM AspNetUsers as u, AspNetRoles as r,AspNetUserRoles as a
	WHERE u.Id=a.UserId AND a.RoleId=r.Id
END