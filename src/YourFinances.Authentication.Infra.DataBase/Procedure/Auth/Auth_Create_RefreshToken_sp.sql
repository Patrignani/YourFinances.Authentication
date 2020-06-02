CREATE PROCEDURE [dbo].[Auth_Create_RefreshToken_sp]
	@UserId int,
	@ExpirationDate datetime,
	@Refresh varchar(50)
AS
BEGIN
	
	INSERT INTO 
		[dbo].[Sessions]
		([UserId]
		,[RefreshToken]
		,[ExpirationDate]
		,[CreateDate]
		,[Active])
	VALUES
		(@UserId,
		@Refresh,
		@ExpirationDate,
		GETUTCDATE(),
		1)

END
