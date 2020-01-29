USE [KingCal]
GO
/****** Object:  Schema [Food]    Script Date: 1/28/2020 7:15:09 PM ******/
CREATE SCHEMA [Food]
GO
/****** Object:  Schema [User]    Script Date: 1/28/2020 7:15:09 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Food].[Foods]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Foods](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[RecipeIngredients]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[RecipeIngredients](
	[Id] [uniqueidentifier] NOT NULL,
	[RecipeId] [uniqueidentifier] NOT NULL,
	[FoodId] [uniqueidentifier] NOT NULL,
	[UnitOfMeasureId] [uniqueidentifier] NOT NULL,
	[Amount] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[Recipes]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Recipes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Servings] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[RecipeServingPrice]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[RecipeServingPrice](
	[Id] [uniqueidentifier] NOT NULL,
	[RecipeId] [uniqueidentifier] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_RecipeServingPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[RecipeSteps]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[RecipeSteps](
	[Id] [uniqueidentifier] NOT NULL,
	[RecipeId] [uniqueidentifier] NOT NULL,
	[Step] [int] NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RecipeSteps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Food].[UnitsOfMeasure]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[UnitsOfMeasure](
	[Id] [uniqueidentifier] NOT NULL,
	[UnitOfMeasure] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_UnitsOfMeasure] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[Roles]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[UserRoles]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[UserRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[Users]    Script Date: 1/28/2020 7:15:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_User_New_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [Food].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Foods] FOREIGN KEY([FoodId])
REFERENCES [Food].[Foods] ([Id])
GO
ALTER TABLE [Food].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Foods]
GO
ALTER TABLE [Food].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [Food].[Recipes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Food].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Recipe]
GO
ALTER TABLE [Food].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_RecipeIngredients] FOREIGN KEY([Id])
REFERENCES [Food].[RecipeIngredients] ([Id])
GO
ALTER TABLE [Food].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_RecipeIngredients]
GO
ALTER TABLE [Food].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_UnitOfMeasure] FOREIGN KEY([UnitOfMeasureId])
REFERENCES [Food].[UnitsOfMeasure] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Food].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_UnitOfMeasure]
GO
ALTER TABLE [Food].[RecipeServingPrice]  WITH CHECK ADD  CONSTRAINT [FK_RecipeServingPrice_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [Food].[Recipes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Food].[RecipeServingPrice] CHECK CONSTRAINT [FK_RecipeServingPrice_Recipe]
GO
ALTER TABLE [Food].[RecipeSteps]  WITH CHECK ADD  CONSTRAINT [FK_RecipeSteps_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [Food].[Recipes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Food].[RecipeSteps] CHECK CONSTRAINT [FK_RecipeSteps_Recipe]
GO
ALTER TABLE [User].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [User].[Roles] ([Id])
GO
ALTER TABLE [User].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [User].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [User].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [User].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [User].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_CreatedBy]
GO
USE [master]
GO
ALTER DATABASE [KingCal] SET  READ_WRITE 
GO
