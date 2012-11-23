CREATE TABLE [dbo].[Stops]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StopId] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StopName] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StopLat] [decimal] (9, 6) NOT NULL,
[StopLon] [decimal] (9, 6) NOT NULL
)
GO
CREATE NONCLUSTERED INDEX [IDX_STOPS] ON [dbo].[Stops] ([StopLat], [StopLon]) INCLUDE ([StopId], [StopName])
GO
