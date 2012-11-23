CREATE TABLE [dbo].[Gps]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Record] [int] NOT NULL,
[Date] [datetime] NOT NULL,
[Latitude] [decimal] (9, 6) NOT NULL,
[Longitude] [decimal] (9, 6) NOT NULL,
[Altitude] [decimal] (9, 1) NOT NULL,
[Temp] [decimal] (4, 1) NOT NULL,
[Status] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Course] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[GpsFix] [bit] NOT NULL,
[Signal] [tinyint] NOT NULL,
[MapLink] [varchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DeviceName] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
CREATE NONCLUSTERED INDEX [IDX_GPS] ON [dbo].[Gps] ([Date], [DeviceName]) INCLUDE ([Latitude], [Longitude])
GO
