CREATE PROCEDURE [dbo].[Auth_RefreshToken_sp]
	@ClientId varchar(150),
	@ClientSecrete varchar(150),
	@RefreshToken varchar(50),
	@ExpirationDate datetime
As
BEGIN
	Declare @ClientIdentification  varchar(150) = (SELECT TOP 1 Identification 
	FROM [Client] WHERE ClientId =@ClientId AND ClientSecret=@ClientSecrete and Active =1)

	Declare @UserAdd Table(Id INT, Identification VARCHAR(150), [AcceptTerm] BIT, 
	[Email] VARCHAR(200), [AccountId] INT)

	if @ClientIdentification IS NOT NULL
		BEGIN
			DECLARE @SessionId INT = (SELECT TOP 1 Id FROM [Sessions] WHERE [RefreshToken] = @RefreshToken
			AND [ExpirationDate] > GETUTCDATE() AND [Active] = 1)
			
			DECLARE  @UserId INT = (SELECT TOP 1 UserId FROM [Sessions] WHERE [Id] = @SessionId) 

			if  @SessionId IS NOT NULL
				BEGIN
					INSERT INTO @UserAdd SELECT [Id], [Identification], [AcceptTerm],[Email], [AccountId] FROM [USER] 
					WHERE [Id] = @UserId

					DECLARE @Refresh varchar(50) = NEWID()
		
					EXEC Auth_Create_RefreshToken_sp @UserId, @ExpirationDate, @Refresh

					UPDATE [Sessions] SET SessionId=@SessionId WHERE [RefreshToken] = @Refresh 
					UPDATE [Sessions] SET [Active]=0 WHERE [Id] = @SessionId
	
					SELECT Id, Identification, Email, AcceptTerm, AccountId, @Refresh AS RefreshToken, @ClientIdentification AS ClientIdentification 
					FROM @UserAdd
				END
		END
END