CREATE PROCEDURE [dbo].[Account_Create_sp]
	@Identification varchar(250),
	@Active bit,
	@UserId int
AS
BEGIN	
	IF(SELECT COUNT(*) FROM [USER] WHERE [Id] = @UserId AND  [AccountId] IS NULL) = 0
	BEGIN
		INSERT [ACCOUNT]([Identification], [Active]) VALUES (@Identification,@Active)
		DECLARE @AccountId  int = CAST(SCOPE_IDENTITY() as int)

		UPDATE [USER] SET [AccountId] = @AccountId WHERE [Id] = @UserId

		SELECT @AccountId;
	END
END
