CREATE TABLE [dbo].[FavoriteTrain] (
    [Id]      INT            NOT NULL,
    [UserId]  NVARCHAR (450) NULL,
    [TrainId] INT            NULL,
    CONSTRAINT [PK_FavoriteTrain] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FavoriteTrain_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_FavoriteTrain_Train] FOREIGN KEY ([TrainId]) REFERENCES [dbo].[Train] ([Id])
);

