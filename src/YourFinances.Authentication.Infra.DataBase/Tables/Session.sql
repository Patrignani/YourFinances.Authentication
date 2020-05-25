CREATE TABLE [dbo].[Sessions]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [RefreshToken] NCHAR(150) NOT NULL, 
    [ExpirationDate] DATETIME NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    FOREIGN KEY (UserId) REFERENCES [User]([Id])
)

GO


CREATE UNIQUE INDEX [IX_Sessions_Uniq_RefrershToken] ON [dbo].[Sessions] ([RefreshToken])

GO

CREATE INDEX [IX_Sessions_RefreshToken_UserId] ON [dbo].[Sessions] ([UserId],[RefreshToken])
