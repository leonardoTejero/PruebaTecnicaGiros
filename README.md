Script para generar base de datos


USE [master]
GO
/****** Object:  Database [BdPaises]    Script Date: 25/06/2023 10:43:24 a. m. ******/
CREATE DATABASE [BdPaises]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BdPaises', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BdPaises.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BdPaises_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BdPaises_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BdPaises] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BdPaises].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BdPaises] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BdPaises] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BdPaises] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BdPaises] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BdPaises] SET ARITHABORT OFF 
GO
ALTER DATABASE [BdPaises] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BdPaises] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BdPaises] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BdPaises] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BdPaises] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BdPaises] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BdPaises] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BdPaises] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BdPaises] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BdPaises] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BdPaises] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BdPaises] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BdPaises] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BdPaises] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BdPaises] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BdPaises] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BdPaises] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BdPaises] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BdPaises] SET  MULTI_USER 
GO
ALTER DATABASE [BdPaises] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BdPaises] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BdPaises] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BdPaises] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BdPaises] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BdPaises] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BdPaises] SET QUERY_STORE = OFF
GO
USE [BdPaises]
GO
/****** Object:  Table [dbo].[CIUDADES]    Script Date: 25/06/2023 10:43:24 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CIUDADES](
	[id_ciudad] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[id_pais] [int] NOT NULL,
 CONSTRAINT [PK_CIUDADES] PRIMARY KEY CLUSTERED 
(
	[id_ciudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIROS]    Script Date: 25/06/2023 10:43:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIROS](
	[GIR_GIRO_ID] [int] IDENTITY(1,1) NOT NULL,
	[GIR_RECIBO] [varchar](50) NOT NULL,
	[GIR_CIUDAD_ID] [int] NOT NULL,
 CONSTRAINT [PK_GIROS] PRIMARY KEY CLUSTERED 
(
	[GIR_GIRO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAISES]    Script Date: 25/06/2023 10:43:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAISES](
	[id_pais] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_PAISES] PRIMARY KEY CLUSTERED 
(
	[id_pais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CIUDADES] ON 

INSERT [dbo].[CIUDADES] ([id_ciudad], [nombre], [id_pais]) VALUES (1, N'Villanueva', 1)
INSERT [dbo].[CIUDADES] ([id_ciudad], [nombre], [id_pais]) VALUES (2, N'Monterrey', 1)
INSERT [dbo].[CIUDADES] ([id_ciudad], [nombre], [id_pais]) VALUES (3, N'Caracas', 2)
SET IDENTITY_INSERT [dbo].[CIUDADES] OFF
GO
SET IDENTITY_INSERT [dbo].[GIROS] ON 

INSERT [dbo].[GIROS] ([GIR_GIRO_ID], [GIR_RECIBO], [GIR_CIUDAD_ID]) VALUES (1, N'Prueba 1 villa', 1)
INSERT [dbo].[GIROS] ([GIR_GIRO_ID], [GIR_RECIBO], [GIR_CIUDAD_ID]) VALUES (2, N'prueba 2 villa', 1)
INSERT [dbo].[GIROS] ([GIR_GIRO_ID], [GIR_RECIBO], [GIR_CIUDAD_ID]) VALUES (3, N'recibo monterrrey ', 2)
SET IDENTITY_INSERT [dbo].[GIROS] OFF
GO
SET IDENTITY_INSERT [dbo].[PAISES] ON 

INSERT [dbo].[PAISES] ([id_pais], [nombre]) VALUES (1, N'Colombia')
INSERT [dbo].[PAISES] ([id_pais], [nombre]) VALUES (2, N'Venezuela')
INSERT [dbo].[PAISES] ([id_pais], [nombre]) VALUES (3, N'Per�')
INSERT [dbo].[PAISES] ([id_pais], [nombre]) VALUES (4, N'Estados Unidos')
SET IDENTITY_INSERT [dbo].[PAISES] OFF
GO
ALTER TABLE [dbo].[CIUDADES]  WITH CHECK ADD  CONSTRAINT [FK_CIUDADES_PAISES] FOREIGN KEY([id_pais])
REFERENCES [dbo].[PAISES] ([id_pais])
GO
ALTER TABLE [dbo].[CIUDADES] CHECK CONSTRAINT [FK_CIUDADES_PAISES]
GO
ALTER TABLE [dbo].[GIROS]  WITH CHECK ADD  CONSTRAINT [FK_GIROS_CIUDADES] FOREIGN KEY([GIR_CIUDAD_ID])
REFERENCES [dbo].[CIUDADES] ([id_ciudad])
GO
ALTER TABLE [dbo].[GIROS] CHECK CONSTRAINT [FK_GIROS_CIUDADES]
GO
/****** Object:  StoredProcedure [dbo].[sp_guardarPais]    Script Date: 25/06/2023 10:43:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[sp_guardarPais]
@nombre varchar(100)
as
	insert into PAISES values(@nombre)
GO
/****** Object:  StoredProcedure [dbo].[sp_obtenerPaises]    Script Date: 25/06/2023 10:43:25 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_obtenerPaises]

as
	select * from Paises
GO
USE [master]
GO
ALTER DATABASE [BdPaises] SET  READ_WRITE 
GO
