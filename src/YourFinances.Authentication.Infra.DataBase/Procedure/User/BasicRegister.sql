CREATE PROCEDURE [dbo].[BasicRegister]
@Identification varchar(150),
	@Password varchar(250),
	@Email varchar(200)
AS
BEGIN
	INSERT INTO [User](Email,Identification,Password) Values(@Email, @Identification, @Password);
	SELECT CAST(SCOPE_IDENTITY() as int)
	RETURN 
END

