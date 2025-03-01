CREATE TABLE [dbo].[Inventory] (
    [id]         INT IDENTITY (1, 1) NOT NULL,
    [user_id]    INT NOT NULL,
    [char_id]    INT NOT NULL,
    [duplicates] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Inventory_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Account] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Inventory_Character] FOREIGN KEY ([char_id]) REFERENCES [dbo].[Character] ([id])
);

