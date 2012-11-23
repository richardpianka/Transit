CREATE TABLE [dbo].[Shapes]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ShapeId] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ShapePtLat] [decimal] (9, 6) NOT NULL,
[ShapePtLon] [decimal] (9, 6) NOT NULL,
[ShapePtSequence] [int] NOT NULL
)
GO
