USE [master]
GO
/****** Object:  Database [Dominium]    Script Date: 23/11/2023 22:14:08 ******/
CREATE DATABASE [Dominium]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Dominium', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Dominium.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Dominium_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Dominium_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Dominium] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Dominium].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Dominium] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Dominium] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Dominium] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Dominium] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Dominium] SET ARITHABORT OFF 
GO
ALTER DATABASE [Dominium] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Dominium] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Dominium] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Dominium] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Dominium] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Dominium] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Dominium] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Dominium] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Dominium] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Dominium] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Dominium] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Dominium] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Dominium] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Dominium] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Dominium] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Dominium] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Dominium] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Dominium] SET RECOVERY FULL 
GO
ALTER DATABASE [Dominium] SET  MULTI_USER 
GO
ALTER DATABASE [Dominium] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Dominium] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Dominium] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Dominium] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Dominium] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Dominium] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Dominium', N'ON'
GO
ALTER DATABASE [Dominium] SET QUERY_STORE = ON
GO
ALTER DATABASE [Dominium] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Dominium]
GO
/****** Object:  Table [dbo].[TPropiedades]    Script Date: 23/11/2023 22:14:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TPropiedades](
	[PropiedadID] [int] IDENTITY(1,1) NOT NULL,
	[Categoria] [nvarchar](50) NULL,
	[Precio] [money] NULL,
	[ProvinciaID] [int] NULL,
	[UbicacionExacta] [nvarchar](250) NULL,
	[Habitaciones] [int] NULL,
	[Banos] [int] NULL,
	[Area] [nvarchar](10) NULL,
	[Piso] [int] NULL,
	[Estacionamiento] [nvarchar](20) NULL,
	[Imagen] [nvarchar](250) NULL,
	[IDVendedor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PropiedadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TProvincias]    Script Date: 23/11/2023 22:14:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TProvincias](
	[ProvinciaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreProvincia] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProvinciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TUsers]    Script Date: 23/11/2023 22:14:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Password] [nvarchar](255) NULL,
	[Rol] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TPropiedades] ON 

INSERT [dbo].[TPropiedades] ([PropiedadID], [Categoria], [Precio], [ProvinciaID], [UbicacionExacta], [Habitaciones], [Banos], [Area], [Piso], [Estacionamiento], [Imagen], [IDVendedor]) VALUES (6, N'De Lujo', 1000000.0000, 1, N'100 metros al lado de multiplaza Escazu', 6, 5, N'880', 1, N'5', N'/Images/6.jpg', 1)
INSERT [dbo].[TPropiedades] ([PropiedadID], [Categoria], [Precio], [ProvinciaID], [UbicacionExacta], [Habitaciones], [Banos], [Area], [Piso], [Estacionamiento], [Imagen], [IDVendedor]) VALUES (7, N'De Lujo', 60000000.0000, 6, N'Santa Teresa', 4, 2, N'222', 2, N'2', N'/Images/7.jpg', 3)
SET IDENTITY_INSERT [dbo].[TPropiedades] OFF
GO
SET IDENTITY_INSERT [dbo].[TProvincias] ON 

INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (1, N'San Jose')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (2, N'Limón')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (3, N'Puntarenas')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (4, N'Heredia')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (5, N'Alajuela')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (6, N'Guanacaste')
INSERT [dbo].[TProvincias] ([ProvinciaID], [NombreProvincia]) VALUES (7, N'Cartago')
SET IDENTITY_INSERT [dbo].[TProvincias] OFF
GO
SET IDENTITY_INSERT [dbo].[TUsers] ON 

INSERT [dbo].[TUsers] ([UserID], [FirstName], [LastName], [Email], [PhoneNumber], [Password], [Rol]) VALUES (1, N'Mathias', N'Jimenez', N'mathi@gmail.com', N'71146631', N'm123', 2)
INSERT [dbo].[TUsers] ([UserID], [FirstName], [LastName], [Email], [PhoneNumber], [Password], [Rol]) VALUES (2, N'David', N'Rodriguez', N'david@gmail.com', N'98798798', N'd123', 1)
INSERT [dbo].[TUsers] ([UserID], [FirstName], [LastName], [Email], [PhoneNumber], [Password], [Rol]) VALUES (3, N'Arthuro', N'Chaves', N'arthy@gmail.com', N'83456332', N'a123', 2)
SET IDENTITY_INSERT [dbo].[TUsers] OFF
GO
ALTER TABLE [dbo].[TPropiedades]  WITH CHECK ADD FOREIGN KEY([IDVendedor])
REFERENCES [dbo].[TUsers] ([UserID])
GO
ALTER TABLE [dbo].[TPropiedades]  WITH CHECK ADD FOREIGN KEY([ProvinciaID])
REFERENCES [dbo].[TProvincias] ([ProvinciaID])
GO
/****** Object:  StoredProcedure [dbo].[RegisterUsers]    Script Date: 23/11/2023 22:14:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterUsers]
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @Password NVARCHAR(255),
	@Rol INT
AS
BEGIN
    INSERT INTO TUsers (FirstName, LastName, Email, PhoneNumber, Password, Rol)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @Rol);
END;
GO
USE [master]
GO
ALTER DATABASE [Dominium] SET  READ_WRITE 
GO
