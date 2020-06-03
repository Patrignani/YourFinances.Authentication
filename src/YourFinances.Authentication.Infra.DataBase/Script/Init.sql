USE [YourFinances.Authentication]
GO

    INSERT INTO [dbo].[Client]
           ([Identification]
           ,[ClientSecret]
           ,[ClientId]
           ,[Active])
     VALUES
           ('Test Postman','Test-Postman-48996','PostMan',1)

           INSERT INTO [dbo].[User]
           ([Identification]
           ,[Active]
           ,[AcceptTerm]
           ,[Email]
           ,[AccountId]
           ,[Password],
           [DateEdition])
     VALUES
           ('Init',1,1,'anderson.patrignani@gmail.com',null,'65C995FD3DF7B1EF68A22D78F272D4FD5B40835837DB53D05ACEE9EADD9048B1C6708D977BC60BC20E9D2577B20065F351C2206D55B6EB0D3525F7E79C657146', GETUTCDATE())


		   INSERT INTO [dbo].[Account]
           ([Identification]
           ,[Active]
           ,[EditionUserId]
           ,[DateEdition])
     VALUES
           ('Casa',1,1,GETUTCDATE())


     update [user]  set AccountId = 1 where Id = 1       


GO
