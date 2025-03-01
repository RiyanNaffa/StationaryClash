CREATE TABLE [dbo].[Account] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [username]    NVARCHAR (64)  NOT NULL,
    [password]    NVARCHAR (20)  NOT NULL,
    [description] NVARCHAR (MAX) NULL,
    [token]       INT            NOT NULL,
    [created]     DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UC_Account_Username] UNIQUE NONCLUSTERED ([username] ASC)
);

