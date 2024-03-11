USE [master]
GO
/****** Object:  Database [E-Commerce-Glasses]    Script Date: 3/11/2024 10:20:45 PM ******/
CREATE DATABASE [E-Commerce-Glasses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'E-Commerce-Glasses', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\E-Commerce-Glasses.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'E-Commerce-Glasses_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\E-Commerce-Glasses_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [E-Commerce-Glasses] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [E-Commerce-Glasses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [E-Commerce-Glasses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ARITHABORT OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [E-Commerce-Glasses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [E-Commerce-Glasses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET  ENABLE_BROKER 
GO
ALTER DATABASE [E-Commerce-Glasses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [E-Commerce-Glasses] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET RECOVERY FULL 
GO
ALTER DATABASE [E-Commerce-Glasses] SET  MULTI_USER 
GO
ALTER DATABASE [E-Commerce-Glasses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [E-Commerce-Glasses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [E-Commerce-Glasses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [E-Commerce-Glasses] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [E-Commerce-Glasses] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [E-Commerce-Glasses] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'E-Commerce-Glasses', N'ON'
GO
ALTER DATABASE [E-Commerce-Glasses] SET QUERY_STORE = ON
GO
ALTER DATABASE [E-Commerce-Glasses] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [E-Commerce-Glasses]
GO
/****** Object:  User [cigrcham]    Script Date: 3/11/2024 10:20:45 PM ******/
CREATE USER [cigrcham] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IdAccount] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_DangNhap] PRIMARY KEY CLUSTERED 
(
	[IdAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[IdBild] [int] IDENTITY(1,1) NOT NULL,
	[IdConsumer] [int] NULL,
	[PercentDiscount] [float] NULL,
	[IdDetailDiscount] [int] NULL,
	[DateOfPurchase] [datetime] NULL,
	[TotalBill] [bigint] NULL,
	[TotalPay] [bigint] NULL,
	[IdDetailDelivery] [int] NULL,
	[IdDetailBill] [int] NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[IdBild] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consumer]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consumer](
	[IdConsumer] [int] IDENTITY(1,1) NOT NULL,
	[IdAccount] [int] NULL,
	[Username] [nvarchar](200) NULL,
	[Address] [nvarchar](500) NULL,
	[DateOfBirth] [datetime] NULL,
	[NumberPhone] [nvarchar](50) NULL,
	[Image] [nvarchar](500) NULL,
	[ListCart] [nvarchar](max) NULL,
 CONSTRAINT [PK_KhachHang_1] PRIMARY KEY CLUSTERED 
(
	[IdConsumer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailBill]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailBill](
	[IdDetailBill] [int] IDENTITY(1,1) NOT NULL,
	[ListBill] [nvarchar](max) NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[IdDetailBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[IdDiscount] [int] IDENTITY(1,1) NOT NULL,
	[IdStatus] [int] NULL,
	[TitleDiscount] [nvarchar](200) NULL,
	[DateOfStart] [datetime] NULL,
	[DateOfEnd] [datetime] NULL,
	[Quantity] [bigint] NULL,
	[PercentValue] [float] NULL,
	[Image] [nvarchar](500) NULL,
	[CodeDiscount] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[IdDiscount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[IdManager] [int] IDENTITY(1,1) NOT NULL,
	[IdAccount] [int] NULL,
	[Username] [nvarchar](200) NULL,
	[Position] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Image] [nvarchar](500) NULL,
	[DateOfBirth] [datetime] NULL,
 CONSTRAINT [PK_QuanLy] PRIMARY KEY CLUSTERED 
(
	[IdManager] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[IdTypeProduct] [int] NULL,
	[NameProduct] [nvarchar](500) NULL,
	[Price] [bigint] NULL,
	[Description] [nvarchar](1000) NULL,
	[Image] [nvarchar](500) NULL,
	[Discount] [float] NULL,
	[Quantity] [bigint] NULL,
	[IdTypeSale] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RateProduct]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateProduct](
	[IdRate] [int] IDENTITY(1,1) NOT NULL,
	[IdConsumer] [int] NULL,
	[IdProduct] [int] NULL,
	[TitleRate] [nvarchar](500) NULL,
	[ContentRate] [nvarchar](500) NULL,
	[DateOfRate] [datetime] NULL,
	[RateStar] [int] NULL,
 CONSTRAINT [PK_DanhGiaSanPham] PRIMARY KEY CLUSTERED 
(
	[IdRate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusDelivery]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusDelivery](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](200) NULL,
 CONSTRAINT [PK_TrangThaiGiaoHang] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusDiscount]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusDiscount](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](200) NULL,
 CONSTRAINT [PK_TrangThaiKhuyenMai] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeProduct]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeProduct](
	[IdTypeProduct] [int] IDENTITY(1,1) NOT NULL,
	[TypeProductName] [nvarchar](500) NULL,
 CONSTRAINT [PK_LoaiSanPham] PRIMARY KEY CLUSTERED 
(
	[IdTypeProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeProductSale]    Script Date: 3/11/2024 10:20:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeProductSale](
	[IdTypeSale] [int] IDENTITY(1,1) NOT NULL,
	[StatusProduct] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypeProductSale] PRIMARY KEY CLUSTERED 
(
	[IdTypeSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Consumer] FOREIGN KEY([IdConsumer])
REFERENCES [dbo].[Consumer] ([IdConsumer])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Consumer]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_DetailBill] FOREIGN KEY([IdDetailBill])
REFERENCES [dbo].[DetailBill] ([IdDetailBill])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_DetailBill]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Discount] FOREIGN KEY([IdDetailDiscount])
REFERENCES [dbo].[Discount] ([IdDiscount])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Discount]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_StatusDelivery] FOREIGN KEY([IdDetailDelivery])
REFERENCES [dbo].[StatusDelivery] ([IdStatus])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_StatusDelivery]
GO
ALTER TABLE [dbo].[Consumer]  WITH CHECK ADD  CONSTRAINT [FK_Consumer_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([IdAccount])
GO
ALTER TABLE [dbo].[Consumer] CHECK CONSTRAINT [FK_Consumer_Account]
GO
ALTER TABLE [dbo].[Discount]  WITH CHECK ADD  CONSTRAINT [FK_Discount_StatusDiscount] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[StatusDiscount] ([IdStatus])
GO
ALTER TABLE [dbo].[Discount] CHECK CONSTRAINT [FK_Discount_StatusDiscount]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_Manager_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([IdAccount])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_Manager_Account]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_TypeProduct] FOREIGN KEY([IdTypeProduct])
REFERENCES [dbo].[TypeProduct] ([IdTypeProduct])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_TypeProduct]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_TypeProductSale] FOREIGN KEY([IdTypeSale])
REFERENCES [dbo].[TypeProductSale] ([IdTypeSale])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_TypeProductSale]
GO
ALTER TABLE [dbo].[RateProduct]  WITH CHECK ADD  CONSTRAINT [FK_RateProduct_Consumer] FOREIGN KEY([IdConsumer])
REFERENCES [dbo].[Consumer] ([IdConsumer])
GO
ALTER TABLE [dbo].[RateProduct] CHECK CONSTRAINT [FK_RateProduct_Consumer]
GO
ALTER TABLE [dbo].[RateProduct]  WITH CHECK ADD  CONSTRAINT [FK_RateProduct_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[RateProduct] CHECK CONSTRAINT [FK_RateProduct_Product]
GO
USE [master]
GO
ALTER DATABASE [E-Commerce-Glasses] SET  READ_WRITE 
GO
