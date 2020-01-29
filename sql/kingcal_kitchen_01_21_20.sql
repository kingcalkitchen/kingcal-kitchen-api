USE [KingCal]
GO
/****** Object:  Schema [Food]    Script Date: 1/21/2020 6:25:57 AM ******/
CREATE SCHEMA [Food]
GO
/****** Object:  Table [Food].[Foods]    Script Date: 1/21/2020 6:25:58 AM ******/
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
/****** Object:  Table [Food].[RecipeIngredients]    Script Date: 1/21/2020 6:25:58 AM ******/
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
/****** Object:  Table [Food].[Recipes]    Script Date: 1/21/2020 6:25:58 AM ******/
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
/****** Object:  Table [Food].[RecipeServingPrice]    Script Date: 1/21/2020 6:25:58 AM ******/
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
/****** Object:  Table [Food].[RecipeSteps]    Script Date: 1/21/2020 6:25:58 AM ******/
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
/****** Object:  Table [Food].[UnitsOfMeasure]    Script Date: 1/21/2020 6:25:58 AM ******/
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
