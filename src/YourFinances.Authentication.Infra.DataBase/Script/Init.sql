USE [YourFinances.Authentication]
GO

INSERT INTO [dbo].[Client]
           ([Identification]
           ,[ClientSecret]
           ,[ClientId]
           ,[Active])
     VALUES
           ('Test Postman','Test-Postman-48996','PostMan',1)

		   INSERT INTO [dbo].[Account]
           ([Identification]
           ,[Active])
     VALUES
           ('Casa',1)

INSERT INTO [dbo].[User]
           ([Identification]
           ,[Active]
           ,[AcceptTerm]
           ,[Email]
           ,[AccountId]
           ,[Password])
     VALUES
           ('Init',1,1,'anderson.patrignani@gmail.com',1,'65C995FD3DF7B1EF68A22D78F272D4FD5B40835837DB53D05ACEE9EADD9048B1C6708D977BC60BC20E9D2577B20065F351C2206D55B6EB0D3525F7E79C657146')

GO
