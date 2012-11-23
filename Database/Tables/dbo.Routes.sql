CREATE TABLE [dbo].[Routes]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[AgencyId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteShortName] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteType] [int] NOT NULL,
[RouteColor] [char] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteTextColor] [char] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteDesc] [varchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RouteLongName] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
