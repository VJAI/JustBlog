USE [JustBlog]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 11/10/2013 18:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UrlSlug] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/10/2013 18:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UrlSlug] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 11/10/2013 18:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[ShortDescription] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Meta] [nvarchar](1000) NOT NULL,
	[UrlSlug] [nvarchar](200) NOT NULL,
	[Published] [bit] NOT NULL,
	[PostedOn] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[Category] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTagMap]    Script Date: 11/10/2013 18:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTagMap](
	[Post_id] [int] NOT NULL,
	[Tag_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FKEAE98E9B93F2FE0]    Script Date: 11/10/2013 18:21:08 ******/
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FKEAE98E9B93F2FE0] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FKEAE98E9B93F2FE0]
GO
/****** Object:  ForeignKey [FK6C4CB8685C756BA2]    Script Date: 11/10/2013 18:21:08 ******/
ALTER TABLE [dbo].[PostTagMap]  WITH CHECK ADD  CONSTRAINT [FK6C4CB8685C756BA2] FOREIGN KEY([Post_id])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[PostTagMap] CHECK CONSTRAINT [FK6C4CB8685C756BA2]
GO
/****** Object:  ForeignKey [FK6C4CB86878409102]    Script Date: 11/10/2013 18:21:08 ******/
ALTER TABLE [dbo].[PostTagMap]  WITH CHECK ADD  CONSTRAINT [FK6C4CB86878409102] FOREIGN KEY([Tag_id])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[PostTagMap] CHECK CONSTRAINT [FK6C4CB86878409102]
GO
