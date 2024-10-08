CREATE DATABASE Gestion
go
USE [Gestion]
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 14/09/2024 06:56:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[GoalId] [int] NULL,
	[Name] [nvarchar](80) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[AssignmentStatus] [nvarchar](10) NOT NULL,
	[IsImportant] [bit] NOT NULL,
 CONSTRAINT [PK__Assignme__32499E77B8AF2C1B] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goals]    Script Date: 14/09/2024 06:56:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goals](
	[GoalId] [int] IDENTITY(1,1) NOT NULL,
	[GoalName] [nvarchar](80) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CompletedTasks] [int] NOT NULL,
	[TotalTasks] [int] NOT NULL,
	[Percentage] [decimal](5, 2) NOT NULL,
 CONSTRAINT [PK__Goals__8A4FFFD1913D513C] PRIMARY KEY CLUSTERED 
(
	[GoalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assignments] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Goals] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK__Assignmen__GoalI__45F365D3] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goals] ([GoalId])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK__Assignmen__GoalI__45F365D3]
GO
