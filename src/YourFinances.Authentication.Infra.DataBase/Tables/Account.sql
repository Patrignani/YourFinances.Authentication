CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Identification] NCHAR(250) NOT NULL, 
    [Active] BIT NOT NULL 
)

GO

CREATE INDEX [IX_Account_Id] ON [dbo].[Account] ([Id])
