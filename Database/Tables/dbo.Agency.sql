CREATE TABLE [dbo].[Agency]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[AgencyId] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AgencyName] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
