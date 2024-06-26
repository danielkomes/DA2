USE [master]
GO
/****** Object:  Database [obligatorioDB]    Script Date: 15/11/2023 22:21:23 ******/
CREATE DATABASE [obligatorioDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'obligatorioDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\obligatorioDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'obligatorioDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\obligatorioDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [obligatorioDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [obligatorioDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [obligatorioDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [obligatorioDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [obligatorioDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [obligatorioDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [obligatorioDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [obligatorioDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [obligatorioDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [obligatorioDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [obligatorioDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [obligatorioDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [obligatorioDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [obligatorioDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [obligatorioDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [obligatorioDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [obligatorioDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [obligatorioDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [obligatorioDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [obligatorioDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [obligatorioDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [obligatorioDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [obligatorioDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [obligatorioDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [obligatorioDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [obligatorioDB] SET  MULTI_USER 
GO
ALTER DATABASE [obligatorioDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [obligatorioDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [obligatorioDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [obligatorioDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [obligatorioDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [obligatorioDB] SET QUERY_STORE = OFF
GO
USE [obligatorioDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/11/2023 22:21:23 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [real] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Brand] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Colors] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotions]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotions](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Promotions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Products] [nvarchar](max) NOT NULL,
	[PromotionId] [uniqueidentifier] NULL,
	[PaymentMethodId] [uniqueidentifier] NOT NULL,
	[Total] [real] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/11/2023 22:21:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Roles] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113010657_Update to PaymentMethodEntity', N'6.0.16')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'10000000-0000-0000-0000-000000000001', N'Camiseta de algodón', 250, N'Camiseta de algodón suave y cómoda', N'MarcaA', N'Ropa para hombres', N'["Verde","Amarillo"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'20000000-0000-0000-0000-000000000002', N'Pantalones vaqueros', 300, N'Pantalones vaqueros de estilo clásico', N'MarcaC', N'Ropa para hombres', N'["Azul","Rojo"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'30000000-0000-0000-0000-000000000003', N'Pantalones cortos', 150, N'Pantalones cortos cómodos para el verano', N'MarcaB', N'Ropa para hombres', N'["Blanco","Negro"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'40000000-0000-0000-0000-000000000004', N'Vestido veraniego', 350, N'Vestido ligero para el verano', N'MarcaB', N'Ropa para mujeres', N'["Azul"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'50000000-0000-0000-0000-000000000005', N'Blusa de seda', 280, N'Blusa de seda elegante', N'MarcaA', N'Ropa para mujeres', N'["Azul"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'60000000-0000-0000-0000-000000000006', N'Chaqueta deportiva', 400, N'Chaqueta deportiva resistente al agua', N'MarcaD', N'Ropa deportiva', N'["Azul"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'70000000-0000-0000-0000-000000000007', N'Vestido elegante', 450, N'Vestido elegante para ocasiones especiales', N'MarcaC', N'Ropa para mujeres', N'["Negro","Azul","Rosa","Blanco","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'80000000-0000-0000-0000-000000000008', N'Chaqueta de cuero', 550, N'Chaqueta de cuero de alta calidad', N'MarcaA', N'Ropa para hombres', N'["Negro","Marrón","Gris","Blanco"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'90000000-0000-0000-0000-000000000009', N'Camisa de lino', 280, N'Camisa de lino fresca para el verano', N'MarcaD', N'Ropa para hombres', N'["Blanco","Azul","Verde","Rojo","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'10000000-0000-0000-0000-000000000010', N'Abrigo de invierno', 750, N'Abrigo de invierno abrigado y elegante', N'MarcaB', N'Ropa para mujeres', N'["Negro","Gris","Marrón","Azul","Verde"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'11000000-0000-0000-0000-000000000011', N'Sudadera con capucha', 320, N'Sudadera con capucha cómoda y cálida', N'MarcaA', N'Ropa deportiva', N'["Negro","Azul","Gris","Rojo","Verde"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'12000000-0000-0000-0000-000000000012', N'Jeans ajustados', 280, N'Jeans ajustados de moda', N'MarcaC', N'Ropa para mujeres', N'["Azul","Negro","Gris","Blanco","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'13000000-0000-0000-0000-000000000013', N'Traje de negocios', 650, N'Traje de negocios elegante para hombres', N'MarcaD', N'Ropa para hombres', N'["Negro","Azul","Gris","Marrón","Blanco"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'14000000-0000-0000-0000-000000000014', N'Camiseta sin mangas', 180, N'Camiseta sin mangas cómoda para el gimnasio', N'MarcaA', N'Ropa deportiva', N'["Negro","Azul","Verde","Rojo","Blanco"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'15000000-0000-0000-0000-000000000015', N'Vestido de fiesta', 500, N'Vestido de fiesta elegante para mujeres', N'MarcaB', N'Ropa para mujeres', N'["Rojo","Negro","Azul","Morado","Rosa"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'16000000-0000-0000-0000-000000000016', N'Jersey de lana', 380, N'Jersey de lana suave y abrigado', N'MarcaC', N'Ropa para hombres', N'["Azul","Gris","Negro","Blanco","Rojo"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'17000000-0000-0000-0000-000000000017', N'Shorts deportivos', 150, N'Shorts deportivos para actividades al aire libre', N'MarcaD', N'Ropa deportiva', N'["Negro","Azul","Verde","Gris","Blanco"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'18000000-0000-0000-0000-000000000018', N'Chaqueta de esquí', 600, N'Chaqueta de esquí resistente al agua', N'MarcaA', N'Ropa deportiva', N'["Azul","Negro","Rojo","Blanco","Gris"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'19000000-0000-0000-0000-000000000019', N'Vestido de verano', 280, N'Vestido de verano ligero y colorido', N'MarcaB', N'Ropa para mujeres', N'["Amarillo","Azul","Rosa","Blanco","Verde"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'20000000-0000-0000-0000-000000000020', N'Pantalones de yoga', 200, N'Pantalones de yoga cómodos para mujeres', N'MarcaC', N'Ropa deportiva', N'["Negro","Azul","Rojo","Morado","Verde"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'21000000-0000-0000-0000-000000000021', N'Camisa a cuadros', 280, N'Camisa a cuadros de moda para hombres', N'MarcaA', N'Ropa para hombres', N'["Azul","Rojo","Negro","Gris","Verde"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'22000000-0000-0000-0000-000000000022', N'Vestido de novia', 800, N'Vestido de novia elegante y hermoso', N'MarcaB', N'Ropa para mujeres', N'["Blanco","Marfil","Rosa","Azul","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'23000000-0000-0000-0000-000000000023', N'Chaqueta bomber', 350, N'Chaqueta bomber de moda para hombres', N'MarcaC', N'Ropa para hombres', N'["Negro","Verde","Azul","Gris","Marrón"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'24000000-0000-0000-0000-000000000024', N'Vestido de cóctel', 450, N'Vestido de cóctel elegante para mujeres', N'MarcaA', N'Ropa para mujeres', N'["Negro","Rojo","Azul","Morado","Rosa"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'25000000-0000-0000-0000-000000000025', N'Camiseta de fútbol', 120, N'Camiseta de fútbol oficial de equipo', N'MarcaD', N'Ropa deportiva', N'["Rojo","Azul","Verde","Negro","Blanco"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'26000000-0000-0000-0000-000000000026', N'Jeans desgastados', 280, N'Jeans desgastados de estilo casual', N'MarcaA', N'Ropa para hombres', N'["Azul","Gris","Negro","Blanco","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'27000000-0000-0000-0000-000000000027', N'Vestido de noche', 550, N'Vestido de noche elegante para mujeres', N'MarcaB', N'Ropa para mujeres', N'["Negro","Azul","Rojo","Morado","Plateado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'28000000-0000-0000-0000-000000000028', N'Chaqueta de motociclista', 480, N'Chaqueta de motociclista de cuero', N'MarcaC', N'Ropa para hombres', N'["Negro","Marrón","Gris","Blanco","Rojo"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'29000000-0000-0000-0000-000000000029', N'Leggings deportivos', 180, N'Leggings deportivos para mujeres', N'MarcaD', N'Ropa deportiva', N'["Negro","Azul","Gris","Verde","Morado"]')
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [Description], [Brand], [Category], [Colors]) VALUES (N'30000000-0000-0000-0000-000000000030', N'Vestido de playa', 250, N'Vestido de playa ligero y fresco', N'MarcaA', N'Ropa para mujeres', N'["Azul","Morado","Rojo","Amarillo","Verde"]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'9e319064-6c06-4df3-aab4-00c5dee4af73', N'user25@example.com', N'', N'Address 25', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'4522b2f1-1976-4fa0-916d-00da805b4198', N'user6@example.com', N'', N'Address 6', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'27d9438c-3c5b-46a2-a1c8-041c0d05a423', N'user9@example.com', N'', N'Address 9', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'fed7989a-12dd-4505-8716-076320339cc1', N'user11@example.com', N'', N'Address 11', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'f92962d3-4e22-4f1b-bc92-200c1f82a75a', N'user30@example.com', N'', N'Address 30', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'd00dfb96-7e7d-4ac9-a9a3-3bd341ee6120', N'user3@example.com', N'', N'Address 3', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'3b0e43c8-df5f-4202-bed8-43cf045d4c86', N'user12@example.com', N'', N'Address 12', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'baade7fa-66f6-4cde-9e8d-45c04cacc324', N'user13@example.com', N'', N'Address 13', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'3fed4c35-7271-4f72-85b1-486d4ff2e021', N'user29@example.com', N'', N'Address 29', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'4d29bfeb-af42-4f9e-ade6-51654115374d', N'user22@example.com', N'', N'Address 22', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'042bbcd9-61e5-4b79-bb2b-561ca176e278', N'user19@example.com', N'', N'Address 19', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'05504c34-0d4d-4a68-ac12-5afa14917335', N'user8@example.com', N'', N'Address 8', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'902d54de-fbec-4863-a30a-6ce4d2916ed6', N'user26@example.com', N'', N'Address 26', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'022a9007-0931-4a27-841a-6f154ddd0685', N'user2@example.com', N'', N'Address 2', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'ce86596d-7cfd-4396-a2a9-7221bfc61f0e', N'user24@example.com', N'', N'Address 24', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'7a9daa0f-822f-47d3-9e47-8291c5cffb46', N'user5@example.com', N'', N'Address 5', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'cf756b08-5100-4c00-9191-86fd724ce5a0', N'user15@example.com', N'', N'Address 15', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'6eac094d-5f18-4203-a1a9-98cae252589c', N'user27@example.com', N'', N'Address 27', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'9feac67b-685b-440e-9d7f-9b3afb9b4083', N'user14@example.com', N'', N'Address 14', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'6073ef59-14fe-4e65-9321-a1d33d08f413', N'user4@example.com', N'', N'Address 4', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'39974eee-38d1-4911-bf37-a9a0f4c85cf2', N'user1@example.com', N'', N'Address 1', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'3de4b123-4b2e-4e75-bed1-c4a2883ed056', N'user23@example.com', N'', N'Address 23', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'8c1d3837-6641-413d-a0b0-d6f7e9ff1ea8', N'user21@example.com', N'', N'Address 21', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'57132401-f9ba-47a8-9bb3-df12c8ef6b45', N'user20@example.com', N'', N'Address 20', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'ba9843a8-d474-41fe-89af-e30434336f8a', N'admin@admin.com', N'', N'admin', N'[0]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'98c27537-ae2c-410d-b1ef-ebd8c17bfddb', N'user28@example.com', N'', N'Address 28', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'ae8b3f16-db02-40f4-bab7-ed35e99e9b9b', N'user16@example.com', N'', N'Address 16', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'e399756c-0b42-4812-af21-ee2b140dd30b', N'user17@example.com', N'', N'Address 17', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'f3f3460a-3743-4850-aacf-f2f46da3a0f1', N'user7@example.com', N'', N'Address 7', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'cb75a409-db2d-43e2-8c28-f467ad86f377', N'user18@example.com', N'', N'Address 18', N'[1]')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Roles]) VALUES (N'8444f4f4-56a0-457a-893c-f95921471a96', N'user10@example.com', N'', N'Address 10', N'[1]')
GO
/****** Object:  Index [IX_PaymentMethods_UserId]    Script Date: 15/11/2023 22:21:24 ******/
CREATE NONCLUSTERED INDEX [IX_PaymentMethods_UserId] ON [dbo].[PaymentMethods]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_PaymentMethodId]    Script Date: 15/11/2023 22:21:24 ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_PaymentMethodId] ON [dbo].[Purchases]
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_PromotionId]    Script Date: 15/11/2023 22:21:24 ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_PromotionId] ON [dbo].[Purchases]
(
	[PromotionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_UserId]    Script Date: 15/11/2023 22:21:24 ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_UserId] ON [dbo].[Purchases]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sessions_UserId]    Script Date: 15/11/2023 22:21:24 ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_UserId] ON [dbo].[Sessions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PaymentMethods]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMethods_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentMethods] CHECK CONSTRAINT [FK_PaymentMethods_Users_UserId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Promotions_PromotionId] FOREIGN KEY([PromotionId])
REFERENCES [dbo].[Promotions] ([Id])
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Promotions_PromotionId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Users_UserId]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [obligatorioDB] SET  READ_WRITE 
GO
