USE [master]
GO

/****** Object:  Database [CaloriesTracker]    Script Date: 21.04.2021 15:23:53 ******/
CREATE DATABASE [CaloriesTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CaloriesTracker', FILENAME = N'/var/opt/mssql/data/CaloriesTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CaloriesTracker_log', FILENAME = N'/var/opt/mssql/data/CaloriesTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CaloriesTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [CaloriesTracker] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [CaloriesTracker] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [CaloriesTracker] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [CaloriesTracker] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [CaloriesTracker] SET ARITHABORT OFF 
GO

ALTER DATABASE [CaloriesTracker] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [CaloriesTracker] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [CaloriesTracker] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [CaloriesTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [CaloriesTracker] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [CaloriesTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [CaloriesTracker] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [CaloriesTracker] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [CaloriesTracker] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [CaloriesTracker] SET  ENABLE_BROKER 
GO

ALTER DATABASE [CaloriesTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [CaloriesTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [CaloriesTracker] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [CaloriesTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [CaloriesTracker] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [CaloriesTracker] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [CaloriesTracker] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [CaloriesTracker] SET RECOVERY FULL 
GO

ALTER DATABASE [CaloriesTracker] SET  MULTI_USER 
GO

ALTER DATABASE [CaloriesTracker] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [CaloriesTracker] SET DB_CHAINING OFF 
GO

ALTER DATABASE [CaloriesTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [CaloriesTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [CaloriesTracker] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [CaloriesTracker] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [CaloriesTracker] SET QUERY_STORE = OFF
GO

ALTER DATABASE [CaloriesTracker] SET  READ_WRITE 
GO

USE [CaloriesTracker]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21.04.2021 15:21:28 ******/
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
/****** Object:  Table [dbo].[Activities]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Start] [datetime2](7) NOT NULL,
	[Finish] [datetime2](7) NOT NULL,
	[TotalCaloriesSpent] [real] NOT NULL,
	[UserProfileId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityExercise]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityExercise](
	[Id] [uniqueidentifier] NOT NULL,
	[ExerciseId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL,
	[NumberOfRepetitions] [int] NOT NULL,
	[NumberOfSets] [int] NOT NULL,
 CONSTRAINT [PK_ActivityExercise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eatings]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eatings](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Moment] [datetime2](7) NOT NULL,
	[TotalCalories] [real] NOT NULL,
	[UserProfileId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Eatings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exercises]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exercises](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CaloriesSpent] [real] NOT NULL,
 CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientEating]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientEating](
	[Id] [uniqueidentifier] NOT NULL,
	[IngredientId] [uniqueidentifier] NOT NULL,
	[EatingId] [uniqueidentifier] NOT NULL,
	[Grams] [real] NOT NULL,
 CONSTRAINT [PK_IngredientEating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientRecipe]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientRecipe](
	[Id] [uniqueidentifier] NOT NULL,
	[IngredientId] [uniqueidentifier] NOT NULL,
	[RecipeId] [uniqueidentifier] NOT NULL,
	[Grams] [real] NOT NULL,
 CONSTRAINT [PK_IngredientRecipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Calories] [real] NOT NULL,
	[Proteins] [real] NOT NULL,
	[Fats] [real] NOT NULL,
	[Carbohydrates] [real] NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Instruction] [nvarchar](max) NULL,
	[TotalCalories] [real] NOT NULL,
	[UserProfileId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 21.04.2021 15:21:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[Id] [uniqueidentifier] NOT NULL,
	[Weight] [real] NOT NULL,
	[Height] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Calories] [real] NOT NULL,
	[CurrentCalories] [real] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_Activities_UserProfiles_UserProfileId] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_Activities_UserProfiles_UserProfileId]
GO
ALTER TABLE [dbo].[ActivityExercise]  WITH CHECK ADD  CONSTRAINT [FK_ActivityExercise_Activities_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityExercise] CHECK CONSTRAINT [FK_ActivityExercise_Activities_ActivityId]
GO
ALTER TABLE [dbo].[ActivityExercise]  WITH CHECK ADD  CONSTRAINT [FK_ActivityExercise_Exercises_ExerciseId] FOREIGN KEY([ExerciseId])
REFERENCES [dbo].[Exercises] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityExercise] CHECK CONSTRAINT [FK_ActivityExercise_Exercises_ExerciseId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Eatings]  WITH CHECK ADD  CONSTRAINT [FK_Eatings_UserProfiles_UserProfileId] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Eatings] CHECK CONSTRAINT [FK_Eatings_UserProfiles_UserProfileId]
GO
ALTER TABLE [dbo].[IngredientEating]  WITH CHECK ADD  CONSTRAINT [FK_IngredientEating_Eatings_EatingId] FOREIGN KEY([EatingId])
REFERENCES [dbo].[Eatings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IngredientEating] CHECK CONSTRAINT [FK_IngredientEating_Eatings_EatingId]
GO
ALTER TABLE [dbo].[IngredientEating]  WITH CHECK ADD  CONSTRAINT [FK_IngredientEating_Ingredients_IngredientId] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IngredientEating] CHECK CONSTRAINT [FK_IngredientEating_Ingredients_IngredientId]
GO
ALTER TABLE [dbo].[IngredientRecipe]  WITH CHECK ADD  CONSTRAINT [FK_IngredientRecipe_Ingredients_IngredientId] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IngredientRecipe] CHECK CONSTRAINT [FK_IngredientRecipe_Ingredients_IngredientId]
GO
ALTER TABLE [dbo].[IngredientRecipe]  WITH CHECK ADD  CONSTRAINT [FK_IngredientRecipe_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IngredientRecipe] CHECK CONSTRAINT [FK_IngredientRecipe_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_UserProfiles_UserProfileId] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfiles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_UserProfiles_UserProfileId]
GO
ALTER TABLE [dbo].[UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_AspNetUsers_UserId]
GO

USE [CaloriesTracker]

INSERT INTO AspNetRoles VALUES
	('e164e274-d58e-40c7-9286-e41f8b482157', 'Manager', 'MANAGER', 'c4e3bc21-d0d2-483d-9b81-a3ccc0142da2'),
	('d29a222d-a777-4396-bccc-e76dc07396ee', 'Administrator', 'ADMINISTRATOR', '9231e546-e1ee-4a65-9f4e-4778b6ac6fe8'),
	('5eba0fe6-ed49-454b-bd7c-7a4ef2a8e6e1', 'User', 'USER', 'b545e1e7-09a0-4270-b904-6f9dec80f14e');

INSERT INTO Exercises VALUES
	('7c2a51b6-ffd3-4f82-8e21-92ca4053a37e', 'Pull-ups', 'Performed on the crossbar. Duration 40 seconds', 5),
	('291bf3d3-9c56-4f6c-b78e-9b100a2e7b55', 'Squats', 'From a standing position, feet shoulder width apart', 10);

INSERT INTO Ingredients VALUES 
	('c9d4c053-49b6-410c-bc78-2d54a9991870', 'Potato', 77.0, 2.0, 0.4, 16.3),
	('3d490a70-94ce-4d15-9494-5248280c2ce3', 'Pasta', 98.0, 3.6, 0.4, 20.0),
	('a1d8448e-b995-4783-b9d3-987c857c8c5d', 'Chicken breast', 113.0, 23.6, 1.9, 0.4);


