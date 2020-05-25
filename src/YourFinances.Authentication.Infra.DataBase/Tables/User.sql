CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Identification] VARCHAR(150) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0 , 
    [AcceptTerm] BIT NOT NULL DEFAULT 0 , 
    [Email] VARCHAR(200) NOT NULL, 
    [AccountId] INT NULL, 
    [Password] VARCHAR(250) NOT NULL, 
    FOREIGN KEY ([AccountId]) REFERENCES Account(Id) 
)

GO

CREATE UNIQUE INDEX [IX_User_Uniq__AccountId_Identification] ON [dbo].[User] ([Identification], [AccountId])

GO

CREATE UNIQUE INDEX [IX_User_Uniq_Email_AccountId] ON [dbo].[User] ([AccountId],[Email])

GO


CREATE INDEX [IX_User_Email_Password_Active] ON [dbo].[User] ([Email],[Password],[Active])
