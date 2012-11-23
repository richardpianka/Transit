CREATE TABLE [dbo].[StopTimes]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[TripId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StopId] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StopSequence] [int] NOT NULL,
[Timepoint] [bit] NOT NULL
)
GO
