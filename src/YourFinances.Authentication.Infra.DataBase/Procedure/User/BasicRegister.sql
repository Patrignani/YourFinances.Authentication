CREATE PROCEDURE [dbo].[User_Basic_Register_sp]
	@Identification varchar(150),
	@Password varchar(250),
	@Email varchar(200),
	@AccountId int = null,
	@UserEditionId int = null
AS
BEGIN
	INSERT INTO [User]([Email],[Identification],[Password], [Active], [AccountId], [DateEdition], [EditionUserId]) 
		VALUES(@Email, @Identification, @Password, 1, @AccountId, GETUTCDATE(), @UserEditionId);
	SELECT CAST(SCOPE_IDENTITY() as int)
	RETURN 
END

