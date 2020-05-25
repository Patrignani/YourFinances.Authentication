CREATE TABLE [dbo].[Client]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ClientSecret] NCHAR(150) NOT NULL, 
    [ClientId] NCHAR(150) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0
)

GO


CREATE UNIQUE INDEX [IX_Client_Secret_Id] ON [dbo].[Client] ([ClientSecret],[ClientId])

GO

CREATE UNIQuE INDEX [IX_Client_Uniq_Secret] ON [dbo].[Client] ([ClientSecret])

GO

CREATE INDEX [IX_Client_Login] ON [dbo].[Client] ([ClientSecret],[ClientId],[Active])
