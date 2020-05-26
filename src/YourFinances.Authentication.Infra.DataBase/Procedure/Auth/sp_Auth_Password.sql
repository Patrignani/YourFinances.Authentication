CREATE PROCEDURE [dbo].[sp_Auth_Password]
	@ClientId varchar(150),
	@ClientSecrete varchar(150),
	@Password varchar(250),
	@Email varchar(200),
	@ExpirationDate datetime
AS
BEGIN
	Declare @UserAdd Table(Id INT, Identification VARCHAR(150), [Active] BIT, [AcceptTerm] BIT, 
		[Email] VARCHAR(200), [AccountId] INT, [Password] VARCHAR(250))

	if(SELECT COUNT(*) [Client] WHERE ClientId =@ClientId AND ClientSecret=@ClientSecrete and Active =1) > 0
	BEGIN

		INSERT INTO @UserAdd SELECT * FROM [USER] 
			WHERE [Password] = @Password and Email = @Email and Active=1

		IF(SELECT COUNT(*) FROM @UserAdd) > 0
		BEGIN
			DECLARE @Refresh nchar(50) = NEWID()

			INSERT INTO [dbo].[Sessions]
				([UserId]
				,[RefreshToken]
				,[ExpirationDate]
				,[CreateDate])
			VALUES
				((SELECT TOP 1 Id FROM @UserAdd),
				convert(nchar(50), @Refresh),
				@ExpirationDate,
				GETUTCDATE())

			SELECT @Refresh as Refresh,* FROM @UserAdd
		END
		SELECT * FROM @UserAdd
	END
	SELECT * FROM @UserAdd
END
