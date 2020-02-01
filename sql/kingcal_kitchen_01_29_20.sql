USE [KingCal]
GO
/****** Object:  Schema [Food]    Script Date: 1/29/2020 11:19:57 PM ******/
CREATE SCHEMA [Food]
GO
/****** Object:  Schema [User]    Script Date: 1/29/2020 11:19:57 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Food].[CouponCodes]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[CouponCodes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Percentage] [float] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CouponCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Food].[Foods]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Foods](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealNutritionFacts]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealNutritionFacts](
	[Id] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[Protein] [int] NOT NULL,
	[Carbs] [int] NOT NULL,
	[Fat] [int] NOT NULL,
	[Calories] [int] NOT NULL,
 CONSTRAINT [PK_NutritionFacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealOrderHistory]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealOrderHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PurchasedBy] [uniqueidentifier] NOT NULL,
	[PurchasedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MealOrderHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealOrders]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealOrders](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_MealOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealPrice]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealPrice](
	[Id] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_MealPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealProcurementOptions]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealProcurementOptions](
	[Id] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[InStore] [bit] NOT NULL,
	[Ship] [bit] NOT NULL,
 CONSTRAINT [PK_MealAquisitionOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealReviews]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealReviews](
	[Id] [uniqueidentifier] NOT NULL,
	[MealId] [uniqueidentifier] NOT NULL,
	[Stars] [int] NOT NULL,
	[Review] [nvarchar](max) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ApprovedBy] [uniqueidentifier] NULL,
	[ApprovedDate] [datetime] NULL,
 CONSTRAINT [PK_MealReviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Food].[Meals]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Meals](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[MealTypeId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Meals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Food].[MealTypes]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[MealTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MealTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Food].[Orders]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[MealProcurementOptionId] [uniqueidentifier] NOT NULL,
	[PurchasedBy] [uniqueidentifier] NOT NULL,
	[PurchasedDate] [datetime] NOT NULL,
	[ProcessedDate] [datetime] NULL,
	[CompletedDate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[RecipeIngredients]    Script Date: 1/29/2020 11:19:57 PM ******/
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
/****** Object:  Table [Food].[Recipes]    Script Date: 1/29/2020 11:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Food].[Recipes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Servings] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Food].[RecipeSteps]    Script Date: 1/29/2020 11:19:57 PM ******/
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
/****** Object:  Table [Food].[UnitsOfMeasure]    Script Date: 1/29/2020 11:19:57 PM ******/
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
/****** Object:  Table [User].[Roles]    Script Date: 1/29/2020 11:19:57 PM ******/
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
/****** Object:  Table [User].[UserRoles]    Script Date: 1/29/2020 11:19:57 PM ******/
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
/****** Object:  Table [User].[Users]    Script Date: 1/29/2020 11:19:57 PM ******/
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
ALTER TABLE [Food].[CouponCodes]  WITH CHECK ADD  CONSTRAINT [FK_CouponCodes_Users] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[CouponCodes] CHECK CONSTRAINT [FK_CouponCodes_Users]
GO
ALTER TABLE [Food].[CouponCodes]  WITH CHECK ADD  CONSTRAINT [FK_CouponCodes_Users1] FOREIGN KEY([DeletedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[CouponCodes] CHECK CONSTRAINT [FK_CouponCodes_Users1]
GO
ALTER TABLE [Food].[Foods]  WITH CHECK ADD  CONSTRAINT [FK_Foods_Users] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Foods] CHECK CONSTRAINT [FK_Foods_Users]
GO
ALTER TABLE [Food].[Foods]  WITH CHECK ADD  CONSTRAINT [FK_Foods_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Foods] CHECK CONSTRAINT [FK_Foods_Users1]
GO
ALTER TABLE [Food].[Foods]  WITH CHECK ADD  CONSTRAINT [FK_Foods_Users2] FOREIGN KEY([DeletedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Foods] CHECK CONSTRAINT [FK_Foods_Users2]
GO
ALTER TABLE [Food].[MealNutritionFacts]  WITH CHECK ADD  CONSTRAINT [FK_MealNutritionFacts_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealNutritionFacts] CHECK CONSTRAINT [FK_MealNutritionFacts_Meals]
GO
ALTER TABLE [Food].[MealOrderHistory]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderHistory_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealOrderHistory] CHECK CONSTRAINT [FK_MealOrderHistory_Meals]
GO
ALTER TABLE [Food].[MealOrderHistory]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderHistory_Users] FOREIGN KEY([PurchasedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[MealOrderHistory] CHECK CONSTRAINT [FK_MealOrderHistory_Users]
GO
ALTER TABLE [Food].[MealOrders]  WITH CHECK ADD  CONSTRAINT [FK_MealOrders_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealOrders] CHECK CONSTRAINT [FK_MealOrders_Meals]
GO
ALTER TABLE [Food].[MealOrders]  WITH CHECK ADD  CONSTRAINT [FK_MealOrders_Orders] FOREIGN KEY([OrderId])
REFERENCES [Food].[Orders] ([Id])
GO
ALTER TABLE [Food].[MealOrders] CHECK CONSTRAINT [FK_MealOrders_Orders]
GO
ALTER TABLE [Food].[MealPrice]  WITH CHECK ADD  CONSTRAINT [FK_MealPrice_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealPrice] CHECK CONSTRAINT [FK_MealPrice_Meals]
GO
ALTER TABLE [Food].[MealProcurementOptions]  WITH CHECK ADD  CONSTRAINT [FK_MealProcurementOptions_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealProcurementOptions] CHECK CONSTRAINT [FK_MealProcurementOptions_Meals]
GO
ALTER TABLE [Food].[MealReviews]  WITH CHECK ADD  CONSTRAINT [FK_MealReviews_Meals] FOREIGN KEY([MealId])
REFERENCES [Food].[Meals] ([Id])
GO
ALTER TABLE [Food].[MealReviews] CHECK CONSTRAINT [FK_MealReviews_Meals]
GO
ALTER TABLE [Food].[MealReviews]  WITH CHECK ADD  CONSTRAINT [FK_MealReviews_Users] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[MealReviews] CHECK CONSTRAINT [FK_MealReviews_Users]
GO
ALTER TABLE [Food].[MealReviews]  WITH CHECK ADD  CONSTRAINT [FK_MealReviews_Users1] FOREIGN KEY([ApprovedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[MealReviews] CHECK CONSTRAINT [FK_MealReviews_Users1]
GO
ALTER TABLE [Food].[Meals]  WITH CHECK ADD  CONSTRAINT [FK_Meals_MealTypes] FOREIGN KEY([MealTypeId])
REFERENCES [Food].[MealTypes] ([Id])
GO
ALTER TABLE [Food].[Meals] CHECK CONSTRAINT [FK_Meals_MealTypes]
GO
ALTER TABLE [Food].[Meals]  WITH CHECK ADD  CONSTRAINT [FK_Meals_Users] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Meals] CHECK CONSTRAINT [FK_Meals_Users]
GO
ALTER TABLE [Food].[Meals]  WITH CHECK ADD  CONSTRAINT [FK_Meals_Users1] FOREIGN KEY([DeletedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Meals] CHECK CONSTRAINT [FK_Meals_Users1]
GO
ALTER TABLE [Food].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_MealProcurementOptions] FOREIGN KEY([MealProcurementOptionId])
REFERENCES [Food].[MealProcurementOptions] ([Id])
GO
ALTER TABLE [Food].[Orders] CHECK CONSTRAINT [FK_Orders_MealProcurementOptions]
GO
ALTER TABLE [Food].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([PurchasedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
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
ALTER TABLE [Food].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users] FOREIGN KEY([CreatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users]
GO
ALTER TABLE [Food].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users1]
GO
ALTER TABLE [Food].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users2] FOREIGN KEY([DeletedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [Food].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users2]
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
ALTER TABLE [User].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users1] FOREIGN KEY([DeletedBy])
REFERENCES [User].[Users] ([Id])
GO
ALTER TABLE [User].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users1]
GO
