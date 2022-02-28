CREATE TABLE [dbo].[Train] (
    [Id]              INT         NOT NULL,
    [Name]            NCHAR (100) NOT NULL,
    [DestinationId]   INT         NULL,
    [DepartureId]     INT         NULL,
    [TypeId]          INT         NULL,
    [DestinationTime] DATETIME    NOT NULL,
    [DepartureTime]   DATETIME    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Train_City] FOREIGN KEY ([DestinationId]) REFERENCES [dbo].[City] ([Id]),
    CONSTRAINT [FK_Train_City1] FOREIGN KEY ([DepartureId]) REFERENCES [dbo].[City] ([Id])
);

