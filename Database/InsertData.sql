USE [E-Commerce-Glasses]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [Username], [Password]) VALUES (7, N'Admin', N'e64b78fc3bc91bcbc7dc232ba8ec59e0')
SET IDENTITY_INSERT [dbo].[Account] OFF
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
SET IDENTITY_INSERT [dbo].[TypeProductSale] ON 

INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (1, N'Bán hàng')
INSERT [dbo].[TypeProductSale] ([IdTypeSale], [StatusProduct]) VALUES (2, N'Không bán')
SET IDENTITY_INSERT [dbo].[TypeProductSale] OFF
GO
SET IDENTITY_INSERT [dbo].[Manager] ON 

INSERT [dbo].[Manager] ([IdManager], [IdAccount], [Username], [Position], [PhoneNumber], [Address], [Image], [DateOfBirth]) VALUES (1, 7, N'Admin', N'Manager', N'068686868', N'Viet Nam', NULL, CAST(N'2000-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Manager] OFF
GO