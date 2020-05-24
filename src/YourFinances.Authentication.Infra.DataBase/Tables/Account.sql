CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Identification] NCHAR(250) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0
)
