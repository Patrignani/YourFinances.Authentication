CREATE PROCEDURE [dbo].[Auth_Password_sp]
	@ClientId varchar(150),
	@ClientSecrete varchar(150),
	@Password varchar(250),
	@Email varchar(200),
	@ExpirationDate datetime
AS
BEGIN TRAN 
BEGIN TRY

	Declare @UserAdd Table(Id INT, Identification VARCHAR(150), [AcceptTerm] BIT, 
		[Email] VARCHAR(200), [AccountId] INT)

	Declare @ClientIdentification  varchar(150) = (SELECT TOP 1 Identification 
		FROM [Client] WHERE ClientId =@ClientId AND ClientSecret=@ClientSecrete and Active =1)

	if @ClientIdentification IS NOT NULL
	BEGIN

		INSERT INTO @UserAdd SELECT [Id], [Identification], [AcceptTerm],[Email], [AccountId] FROM [USER] 
			WHERE [Password] = @Password AND Email = @Email AND Active=1

		IF(SELECT COUNT(*) FROM @UserAdd) > 0
		BEGIN
			DECLARE @Refresh varchar(50) 
			DECLARE @UserId int = (SELECT TOP 1 Id FROM @UserAdd)
			
			EXEC @Refresh = Auth_Create_RefreshToken_sp @UserId, @ExpirationDate

			SELECT Id, Identification, Email, AcceptTerm, AccountId, @Refresh AS RefreshToken, @ClientIdentification AS ClientIdentification 
				FROM @UserAdd
		ENDNÃO ESTÁ RETORNANDO O TOKEN
	END
COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH

