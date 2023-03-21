USE FreeGames

select * from AspNetUsers

select * from AspNetRoles
select * from AspNetRoleClaims

select * from AspNetUserRoles
select * from AspNetUserClaims
select * from AspNetUserLogins

select * from AspNetUserTokens



--[Authorize(Roles = Roles.Admin)]
--[ClaimsAuthorize(ClaimTypes.Mensagem, "Enviar")]

select * from AspNetRoles
select * from AspNetUserRoles

select * from AspNetRoleClaims
select * from AspNetUserClaims

DECLARE @roleId AS UNIQUEIDENTIFIER = NEWID()
DECLARE @userId AS UNIQUEIDENTIFIER = (SELECT id FROM AspNetUsers WHERE    
UserName= 'teste@teste.com')
INSERT INTO AspNetRoles (Id, Name) VALUES (@roleId, 'Admin')
INSERT INTO AspNetUserRoles (RoleId, UserId) VALUES(@roleId, @userId)


INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue) 
VALUES('ea4598b1-4f14-406c-ad27-d46fb6463ad5', 'Mensagem', 'Enviar')

