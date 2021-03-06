USE [ToDoList]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 6/12/2017 1:47:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categories_tasks]    Script Date: 6/12/2017 1:47:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories_tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NULL,
	[task_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tasks]    Script Date: 6/12/2017 1:47:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (1, N'ulnlin')
INSERT [dbo].[categories] ([id], [name]) VALUES (2, N'phlegm')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[categories_tasks] ON 

INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (1, 1, 2)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (2, 2, 3)
SET IDENTITY_INSERT [dbo].[categories_tasks] OFF
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([id], [description]) VALUES (1, N'agaweg')
INSERT [dbo].[tasks] ([id], [description]) VALUES (2, N'agaweg')
INSERT [dbo].[tasks] ([id], [description]) VALUES (3, N'trbstg')
SET IDENTITY_INSERT [dbo].[tasks] OFF
