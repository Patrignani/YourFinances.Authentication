
SELECT * FROM [USER] WHERE [Id] = 1 AND  [AccountId] IS NULL

DECLARE @AccountId int = (select top 1 AccountId from [user])

update [USER] set AccountId = null

delete Account where id = @AccountId

