CREATE TABLE [dbo].[Account]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Identification] VARCHAR(250) NOT NULL, 
    [Active] BIT NOT NULL 
)

GO

CREATE INDEX [IX_Account_Id] ON [dbo].[Account] ([Id])
