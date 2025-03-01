CREATE TABLE [dbo].[Character] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (MAX) NOT NULL,
    [type]        NVARCHAR (50)  NOT NULL,
    [image]       NVARCHAR (255) NOT NULL,
    [rarity]      INT            NOT NULL,
    [health]      INT            NOT NULL,
    [attack]      INT            NOT NULL,
    [defense]     INT            NOT NULL,
    [speed]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

