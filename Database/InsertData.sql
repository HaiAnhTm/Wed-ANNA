USE [E-Commerce-Glasses]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (2, N'Cigrcham', N'daac438f8adf0b9ffdd1b621452cf738')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (3, N'Cigrcham1', N'506c4815d6009203b53c2b7fe1274ff4')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (4, N'Cigrcham1234', N'c1dafba2609911a50d5bc0f14eba3dff')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (5, N'Cigrcham12312312', N'4a5a991b10b6fffb542aaf60e04378f6')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (6, N'Admin123', N'e64b78fc3bc91bcbc7dc232ba8ec59e0')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (7, N'Admin', N'e64b78fc3bc91bcbc7dc232ba8ec59e0')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Consumer] ON 

INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (2, 2, N'Cigrcham1', N'jaklsjfkasdf', NULL, N'123123', N'\Assets\images\images_Consumer\wallpapersden.com_firewatch-4k-uhd_2560x1440245341087.jpg', N'')
INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (3, 3, N'Cigrcham1', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (4, 4, N'Cigrcham1234', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (5, 5, N'Cigrcham12312312', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (6, 6, N'123123', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Consumer] OFF
GO
SET IDENTITY_INSERT [dbo].[DetailBill] ON 

INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (33, N'{"5":5}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (34, N'{"5":5}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (35, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (36, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (37, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (38, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (39, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (40, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (41, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (42, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (43, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (44, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (45, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (46, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (47, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (48, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (49, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (50, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (51, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (52, N'{"5":11}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (53, N'{"5":17}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (54, N'{"5":4}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (55, N'{"5":5}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (56, N'{"6":22,"5":4}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (57, N'{"5":5,"6":3}')
INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (58, N'{"5":11}')
SET IDENTITY_INSERT [dbo].[DetailBill] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusDelivery] ON 

INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (1, N'Đã giao hàng')
INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (2, N'Đang giao hàng')
INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (3, N'Chưa giao hàng')
SET IDENTITY_INSERT [dbo].[StatusDelivery] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusDiscount] ON 

INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (1, N'Hết hạn')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (2, N'Hoạt động')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (3, N'Không hoạt động')
SET IDENTITY_INSERT [dbo].[StatusDiscount] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [PercentValue], [Image], [CodeDiscount]) VALUES (3, 2, N'12312', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2300-01-01T00:00:00.000' AS DateTime), 123, 100, N'\Assets\images\images_Discount\29735-cube-glass-black-3D-4K241812395.jpg', N'OVG6q0vZ')
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([IdBild], [IdConsumer], [PercentDiscount], [IdDetailDiscount], [DateOfPurchase], [TotalBill], [TotalPay], [IdDetailDelivery], [IdDetailBill]) VALUES (20, 2, NULL, NULL, CAST(N'2024-03-09T14:20:24.383' AS DateTime), 71439, 71439, 3, 57)
INSERT [dbo].[Bill] ([IdBild], [IdConsumer], [PercentDiscount], [IdDetailDiscount], [DateOfPurchase], [TotalBill], [TotalPay], [IdDetailDelivery], [IdDetailBill]) VALUES (21, 2, NULL, NULL, CAST(N'2024-03-09T15:38:49.287' AS DateTime), 2574, 2574, 3, 58)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProduct] ON 

INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (3, N'fasdfasdf')
INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (4, N'asdfasdf')
INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (5, N'asdfasdf')
SET IDENTITY_INSERT [dbo].[TypeProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProductSale] ON 

INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (1, N'Bán hàng')
INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (2, N'Không bán')
SET IDENTITY_INSERT [dbo].[TypeProductSale] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([IdProduct], [IdTypeProduct], [NameProduct], [Price], [Description], [Image], [Discount], [Quantity], [IdTypeSale]) VALUES (5, 4, N'234', 234, N'234234', N'\Assets\images\images_Product\29735-cube-glass-black-3D-4K245351526.jpg', 23, 32174, 1)
INSERT [dbo].[Product] ([IdProduct], [IdTypeProduct], [NameProduct], [Price], [Description], [Image], [Discount], [Quantity], [IdTypeSale]) VALUES (6, 5, N'asdfas', 23423, N'4234', N'\Assets\images\images_Product\wallpapersden.com_firewatch-4k-uhd_2560x1440242649376.jpg', 23, 234209, 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Manager] ON 

INSERT [dbo].[Manager] ([IdManager], [IdAccount], [Username], [Position], [PhoneNumber], [Address], [Image], [DateOfBirth]) VALUES (1, 7, N'Admin', N'Manager', N'068686868', N'Viet Nam', NULL, CAST(N'2000-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Manager] OFF
GO
