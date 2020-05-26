CREATE PROCEDURE [dbo].[sp_User_Basic_Register]
	@Identification varchar(150),
	@Password varchar(250),
	@Email varchar(200)
AS
BEGIN
	INSERT INTO [User](Email,Identification,Password, Active) Values(@Email, @Identification, @Password, 1);
	SELECT CAST(SCOPE_IDENTITY() as int)
	RETURN 
END

