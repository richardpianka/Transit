CREATE TABLE [dbo].[Trips]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RouteId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TripId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ShapeId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
