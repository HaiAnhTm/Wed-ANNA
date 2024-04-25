USE [E-Commerce-Glasses]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (7, N'Admin', N'e64b78fc3bc91bcbc7dc232ba8ec59e0')
INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (8, N'Haianhtm112', N'25f9e794323b453885f5181f1b624d0b')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Consumer] ON 

INSERT [dbo].[Consumer] ([IdConsumer], [IdAccount], [Username], [Address], [DateOfBirth], [NumberPhone], [Image], [ListCart]) VALUES (1, 8, N'Bùi Hải Anh', N'              ', CAST(N'2024-04-16T00:00:00.000' AS DateTime), NULL, N'\Assets\images\images_Consumer\z5103542819027_b3e4919fe6e840eda87c0b25ac5a0b2e240456144.jpg', N'{"2":5}')
SET IDENTITY_INSERT [dbo].[Consumer] OFF
GO
SET IDENTITY_INSERT [dbo].[Manager] ON 

INSERT [dbo].[Manager] ([IdManager], [IdAccount], [Username], [Position], [PhoneNumber], [Address], [Image], [DateOfBirth]) VALUES (1, 7, N'Admin', N'Manager', N'068686868', N'Viet Nam', NULL, CAST(N'2000-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Manager] OFF
GO
SET IDENTITY_INSERT [dbo].[DetailBill] ON 

INSERT [dbo].[DetailBill] ([IdDetailBill], [ListBill]) VALUES (1, N'{"1":10,"2":2}')
SET IDENTITY_INSERT [dbo].[DetailBill] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusDiscount] ON 

INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (1, N'Hết hạn')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (2, N'Hoạt động')
INSERT [dbo].[StatusDiscount] ([IdStatus], [Status]) VALUES (3, N'Không hoạt động')
SET IDENTITY_INSERT [dbo].[StatusDiscount] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [PercentValue], [Image], [CodeDiscount]) VALUES (1, 2, N'kjhj', CAST(N'2000-01-01T00:00:00.000' AS DateTime), CAST(N'2400-01-01T00:00:00.000' AS DateTime), 1, 123, N'\Assets\images\images_Discount\ANNA_SM21007_4891-scaled241248534.jpeg', N'5vtzWfIB')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [PercentValue], [Image], [CodeDiscount]) VALUES (2, 1, N'23', CAST(N'2024-04-17T09:06:00.000' AS DateTime), CAST(N'2024-04-24T10:06:00.000' AS DateTime), 2, 2, N'\Assets\images\images_Discount\ANNA_SM21007_4891-scaled245718188.jpeg', N'zQeqFMbi')
INSERT [dbo].[Discount] ([IdDiscount], [IdStatus], [TitleDiscount], [DateOfStart], [DateOfEnd], [Quantity], [PercentValue], [Image], [CodeDiscount]) VALUES (3, 2, N'kính mắt', CAST(N'2024-04-24T10:34:00.000' AS DateTime), CAST(N'2024-04-24T11:34:00.000' AS DateTime), 10, 50, N'\Assets\images\images_Discount\ANNA_AN086_5104-scaled242512298.jpeg', N'w22fvpFV')
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[StatusDelivery] ON 

INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (1, N'Đã giao hàng')
INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (2, N'Đang giao hàng')
INSERT [dbo].[StatusDelivery] ([IdStatus], [Status]) VALUES (3, N'Chưa giao hàng')
SET IDENTITY_INSERT [dbo].[StatusDelivery] OFF
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([IdBild], [IdConsumer], [PercentDiscount], [IdDetailDiscount], [DateOfPurchase], [TotalBill], [TotalPay], [IdDetailDelivery], [IdDetailBill]) VALUES (1, 1, NULL, NULL, CAST(N'2024-04-22T10:55:44.527' AS DateTime), 5100000, 5100000, 3, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProduct] ON 

INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (1, N'Gọng Kính')
INSERT [dbo].[TypeProduct] ([IdTypeProduct], [TypeProductName]) VALUES (2, N'Tròng Kính')
SET IDENTITY_INSERT [dbo].[TypeProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeProductSale] ON 

INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (1, N'Bán hàng')
INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (2, N'Không bán')
SET IDENTITY_INSERT [dbo].[TypeProductSale] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([IdProduct], [IdTypeProduct], [NameProduct], [Price], [Description], [Image], [Discount], [Quantity], [IdTypeSale]) VALUES (1, 1, N'GK.M GỌNG NHỰA AN221393 (50.18.145)', 350000, N'Gọng nhựa hình oval là một lựa chọn tuyệt vời cho những ai yêu thích phong cách hiện đại và năng động. Với chất liệu nhựa cao cấp, sản phẩm này sẽ giúp bạn tự tin và thoải mái khi sử dụng trong suốt thời gian dài. Đặc điểm nổi bật: Gọng hình oval Chất liệu nhựa cao cấp Thiết kế hiện đại, năng động', N'\Assets\images\images_Product\DMT05629-scaled244414211.jpeg', 5, 0, 1)
INSERT [dbo].[Product] ([IdProduct], [IdTypeProduct], [NameProduct], [Price], [Description], [Image], [Discount], [Quantity], [IdTypeSale]) VALUES (2, 1, N'GK. GỌNG NHỰA CỨNG AN086 (52.17.146)', 800000, N'Gọng nhựa cứng dáng vuông là một sản phẩm tuyệt vời để bảo vệ đôi mắt của bạn khỏi ánh sáng mạnh và bụi bẩn trong thời gian dài. Với thiết kế vừa vặn và chắc chắn, sản phẩm này sẽ giúp bạn thoải mái khi đeo trong suốt cả ngày', N'\Assets\images\images_Product\ANNA_AN086_5104-scaled240129891.jpeg', 10, 10, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
