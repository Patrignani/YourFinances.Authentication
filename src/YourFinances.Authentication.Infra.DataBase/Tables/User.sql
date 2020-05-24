CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Identification] VARCHAR(150) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0, 
    [AcceptTerm] BIT NOT NULL DEFAULT 0, 
    [Email] VARCHAR(200) NOT NULL, 
    [AccountId] INT NOT NULL, 
    [Password] VARCHAR(MAX) NOT NULL, 
    [Salt] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_User_Account] FOREIGN KEY ([AccountId]) REFERENCES Account(Id) 
)
