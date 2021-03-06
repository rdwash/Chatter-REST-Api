USE [ChatterApiDB]
GO
/****** Object:  User [chatterDbAdmin]    Script Date: 8/23/2017 6:52:12 AM ******/
CREATE USER [chatterDbAdmin] FOR LOGIN [chatterDbAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [chatterDbAdmin]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 8/23/2017 6:52:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[message] [nvarchar](500) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 8/23/2017 6:52:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Message] ON 

INSERT [dbo].[Message] ([id], [userid], [message], [type], [created_at]) VALUES (1, 1, N'This is a test', N'messages', CAST(N'2017-10-04 17:44:30.000' AS DateTime))
INSERT [dbo].[Message] ([id], [userid], [message], [type], [created_at]) VALUES (2, 1, N'Wow you''re testing this.', N'messages', CAST(N'2017-10-08 17:44:30.000' AS DateTime))
INSERT [dbo].[Message] ([id], [userid], [message], [type], [created_at]) VALUES (4, 1, N'You gotta go with option 1', N'messages', CAST(N'2017-08-23 04:46:37.733' AS DateTime))
INSERT [dbo].[Message] ([id], [userid], [message], [type], [created_at]) VALUES (5, 1, N'You gotta go with option 2', N'messages', CAST(N'2017-08-23 04:53:17.227' AS DateTime))
SET IDENTITY_INSERT [dbo].[Message] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [username], [type]) VALUES (1, N'user12345', N'users')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User]
GO
