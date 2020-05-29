CREATE PROCEDURE [dbo].[Auth_RefreshToken_sp]
	@ClientId varchar(150),
	@ClientSecrete varchar(150),
	@RefreshToken varchar(50),
	@ExpirationDate datetime,
	@UserId int
As
BEGIN
	Declare @ClientIdentification  varchar(150) = (SELECT TOP 1 Identification 
	FROM [Client] WHERE ClientId =@ClientId AND ClientSecret=@ClientSecrete and Active =1)

	if @ClientIdentification IS NOT NULL
		BEGIN
			DECLARE @SessionId int = (SELECT TOP 1 Id FROM [Sessions] WHERE [RefreshToken] = @RefreshToken AND ExpirationDate < GETUTCDATE())

			if  @SessionId IS NOT NULL
				BEGIN
					DECLARE @Refresh varchar(50) 
		
					EXEC @Refresh = Auth_Create_RefreshToken_sp @UserId, @ExpirationDate
					UPDATE [Sessions] SET SessionId=@SessionId WHERE [RefreshToken] = @RefreshToken 
				END
		END
END