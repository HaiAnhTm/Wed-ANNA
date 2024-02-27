USE [E-Commerce-Glasses]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (1, N'Cigrcham', N'daac438f8adf0b9ffdd1b621452cf738')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Consumer] ON 

INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (1, 1, N'Cigrcham', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Consumer] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusDiscount] ON 

INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (1, N'Hết hạn')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (2, N'Hoạt động')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (3, N'Không hoạt động')
SET IDENTITY_INSERT [dbo].[StatusDiscount] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (1, 1, N'12312', CAST(N'2000-01-02T00:00:00.000' AS DateTime), CAST(N'2000-01-03T00:00:00.000' AS DateTime), 123, 20, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440241102668.jpg', N'123123')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (2, 1, N'123123123', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 12313, 12, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440245352055.jpg', N'123123')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (3, 1, N'234234', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 123, 20, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440245357959.jpg', N'123123123')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (4, 1, N'asdf', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 123123, 123, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440245544976.jpg', N'asdfasdf')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (5, 1, N'12312', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 123, 12, N'\Assets\images\images_Discount\29735-cube-glass-black-3D-4K245551210.jpg', N'123123')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (6, 1, N'123123', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 123, 12312, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440245011927.jpg', N'123123')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [Percent], [Image], [CodeDiscount]) VALUES (7, 1, N'123123', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2000-01-01T00:00:00.000' AS DateTime), 123, 123123, N'\Assets\images\images_Discount\wallpapersden.com_firewatch-4k-uhd_2560x1440245557543.jpg', NULL)
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProduct] ON 

INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (9, N'23423423123123')
INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (10, N'123123123123')
SET IDENTITY_INSERT [dbo].[TypeProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProductSale] ON 

INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (1, 1)
INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (2, 0)
SET IDENTITY_INSERT [dbo].[TypeProductSale] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([IdProduct], [IdTypeProduct], [NameProduct], [Price], [Description], [Image], [Discount], [Quantity], [IdTypeSale]) VALUES (7, 9, N'12312', 123123, N'123', N'\Assets\images\images_Product\wallpapersden.com_firewatch-4k-uhd_2560x1440242931264.jpg', 123, 123123, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
