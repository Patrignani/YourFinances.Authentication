CREATE PROCEDURE [dbo].[Auth_Create_RefreshToken_sp]
	@UserId int,
	@ExpirationDate datetime
AS
BEGIN
	DECLARE @Refresh varchar(50) = NEWID()

	INSERT INTO 
		[dbo].[Sessions]
		([UserId]
		,[RefreshToken]
		,[ExpirationDate]
		,[CreateDate])
	VALUES
		(@UserId,
		@Refresh,
		@ExpirationDate,
		GETUTCDATE())

	SELECT @Refresh
END
