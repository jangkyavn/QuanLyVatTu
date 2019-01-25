USE [Absoft_QuanLyVatTu]
GO
INSERT [dbo].[NguoiDung] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [Gender], [Avatar], [DateOfBirth], [CreatedDate], [Address], [Status]) VALUES (N'7bbf6244-263d-454c-a879-08d6805265f8', N'thiencnt', N'THIENCNT', N'thiencnt49@gmail.com', N'THIENCNT49@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOacMIxb6gORFgwOkS8+Xyiie/Dihs75rtCOW1aCpZyLR1eKorrHYZNjYppe5h26bg==', N'XTZ7JIIWWQVK64TPVPDYAQFWUCUNYYZ2', N'06d7e8fb-82d3-4202-b181-d6b94d83060c', N'0961249678', 0, 0, NULL, 1, 0, N'chu đức thiện', 1, NULL, CAST(N'2019-01-22T17:14:30.5669409' AS DateTime2), CAST(N'2019-01-22T17:14:30.5669439' AS DateTime2), N'23a minh khai hải phòng', 1)
INSERT [dbo].[NguoiDung] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [Gender], [Avatar], [DateOfBirth], [CreatedDate], [Address], [Status]) VALUES (N'c44d3121-12a4-4593-9ae5-08d680e7ece9', N'hoanganh', N'HOANGANH', N'hoanganh@gmail.com', N'HOANGANH@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAED8wSh3Ix6JFz5f5Ta78duw+Xx1J09AL+oA4Xs3tATuuBrSZ2Eoki7ovhEYnto7G+A==', N'T3EHNKFRHRLNVDH3GHQEGRHLSCXM3NXS', N'1a1772ee-e2f6-46cf-8532-b5b31d70f08c', N'1234567890', 0, 0, NULL, 1, 0, N'hoàng kỵ anh', 1, NULL, CAST(N'2019-01-23T11:04:51.5891265' AS DateTime2), CAST(N'2019-01-23T11:04:51.5892420' AS DateTime2), N'23 a minh khai
', 0)
INSERT [dbo].[NguoiDung] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [Gender], [Avatar], [DateOfBirth], [CreatedDate], [Address], [Status]) VALUES (N'55aa823f-13bf-4f3d-9516-ef307f88ed8e', N'Admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEEreu6HSU/51wsJswIziLnwcB8fE38gIbOU7f0PfGwIQ811Xlxe1BTcfd03ejESrdQ==', N'B6KUYDTD2WSVBSCPUWPBPJLY73G6ZR7W', N'ac0cec49-49a6-4f7f-a704-cca6a6021da6', NULL, 0, 0, NULL, 1, 0, N'Administrator', 1, NULL, CAST(N'2019-01-22T17:11:43.5001170' AS DateTime2), CAST(N'2019-01-22T17:11:43.5414731' AS DateTime2), NULL, 1)
INSERT [dbo].[VaiTro] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'18f397a3-5e16-4580-2aa6-08d68052021f', N'Member', N'MEMBER', N'b733f3b7-c46a-4a51-85cf-a584346da740')
INSERT [dbo].[VaiTro] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'748bfb47-1443-4cdb-2aa7-08d68052021f', N'Admin', N'ADMIN', N'db90e34c-0b89-4f14-87f5-a93e590b8a55')
INSERT [dbo].[VaiTro] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'abe6d214-ca68-4b11-2aa8-08d68052021f', N'Moderator', N'MODERATOR', N'eb062e8f-5b96-4a05-80c3-f6454d97793b')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'7bbf6244-263d-454c-a879-08d6805265f8', N'18f397a3-5e16-4580-2aa6-08d68052021f')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'c44d3121-12a4-4593-9ae5-08d680e7ece9', N'18f397a3-5e16-4580-2aa6-08d68052021f')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'7bbf6244-263d-454c-a879-08d6805265f8', N'748bfb47-1443-4cdb-2aa7-08d68052021f')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'55aa823f-13bf-4f3d-9516-ef307f88ed8e', N'748bfb47-1443-4cdb-2aa7-08d68052021f')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'7bbf6244-263d-454c-a879-08d6805265f8', N'abe6d214-ca68-4b11-2aa8-08d68052021f')
INSERT [dbo].[VaiTroNguoiDung] ([UserId], [RoleId]) VALUES (N'55aa823f-13bf-4f3d-9516-ef307f88ed8e', N'abe6d214-ca68-4b11-2aa8-08d68052021f')
SET IDENTITY_INSERT [dbo].[DonViTinh] ON 

INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (1, N'Xe ')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (2, N'Cái')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (3, N'Chiếc')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (4, N'Bộ')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (5, N'Khẩu')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (6, N'Viên')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (7, N'Ống')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (8, N'Cặp')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (9, N'Máy')
INSERT [dbo].[DonViTinh] ([MaDVT], [TenDVT]) VALUES (10, N'Máy')
SET IDENTITY_INSERT [dbo].[DonViTinh] OFF
SET IDENTITY_INSERT [dbo].[HangMucVatTu] ON 

INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (10, N'Xe và vật tư thay thế', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (11, N'Vũ khí trang thiết bị', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (12, N'Hàng viện trợ Mỹ', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (13, N'Trang thiết bị cục quân y cấp', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (14, N'Hàng tài trợ trong nước', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (15, N'Danh mục thể thao văn hóa', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (16, N'Danh mục thuốc', NULL)
INSERT [dbo].[HangMucVatTu] ([MaHM], [TenHM], [GhiChu]) VALUES (17, N'abc2', N'ass2')
SET IDENTITY_INSERT [dbo].[HangMucVatTu] OFF
SET IDENTITY_INSERT [dbo].[HangSanXuat] ON 

INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (1, N'Sam Sung', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (2, N'Sony', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (3, N'Arian', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (4, N'Nikochi', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (5, N'Canon', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (6, N'Rotex', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (7, N'Warsaw', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (8, N'Viphaco', NULL)
INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [GhiChu]) VALUES (9, N'Apple', N'')
SET IDENTITY_INSERT [dbo].[HangSanXuat] OFF
INSERT [dbo].[KhoHang] ([MaKho], [MaPhieuNhap], [MaVatTu], [SoLuongTon]) VALUES (1, 5, 8, 5)
INSERT [dbo].[KhoHang] ([MaKho], [MaPhieuNhap], [MaVatTu], [SoLuongTon]) VALUES (1, 5, 9, 6)
SET IDENTITY_INSERT [dbo].[KhoVatTu] ON 

INSERT [dbo].[KhoVatTu] ([MaKho], [TenKho], [DiaChi], [DienThoai], [GhiChu]) VALUES (1, N'Kho số 1', N'Địa chỉ 1', N'12345467890', N'Không có')
INSERT [dbo].[KhoVatTu] ([MaKho], [TenKho], [DiaChi], [DienThoai], [GhiChu]) VALUES (2, N'Kho số 2', N'Địa chỉ 2', N'1234567890', N'Không có')
INSERT [dbo].[KhoVatTu] ([MaKho], [TenKho], [DiaChi], [DienThoai], [GhiChu]) VALUES (3, N'Kho số 3', N'Địa chỉ 3', N'1234567890', N'Không có')
INSERT [dbo].[KhoVatTu] ([MaKho], [TenKho], [DiaChi], [DienThoai], [GhiChu]) VALUES (4, N'Kho 4', N'Hà Nội', N'0123454444', N'')
SET IDENTITY_INSERT [dbo].[KhoVatTu] OFF
SET IDENTITY_INSERT [dbo].[LoaiVatTu] ON 

INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (25, 10, N'Phương tiện các loại', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (26, 10, N'Trang thiết bị sửa chữa', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (27, 11, N'Súng', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (28, 11, N'Đạn', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (29, 11, N'Thiết bị bảo vệ', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (30, 12, N'Hàng viện trợ Mỹ', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (31, 13, N'Trang thiết bị cục quân y cấp', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (32, 14, N'Hàng tài trợ trong nước', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (33, 15, N'Danh mục thể thao văn hóa', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (34, 16, N'Thuốc giảm đau gây nghiện', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (35, 16, N'Thuốc tiền mê', NULL)
INSERT [dbo].[LoaiVatTu] ([MaLoaiVatTu], [MaHM], [TenLoai], [GhiChu]) VALUES (36, 13, N'bang tiep te', N'')
SET IDENTITY_INSERT [dbo].[LoaiVatTu] OFF
SET IDENTITY_INSERT [dbo].[NguonCungCap] ON 

INSERT [dbo].[NguonCungCap] ([MaNguon], [TenNguon], [GhiChu]) VALUES (1, N'CQL cấp', N'Cục quản lý')
INSERT [dbo].[NguonCungCap] ([MaNguon], [TenNguon], [GhiChu]) VALUES (2, N'CQY cấp', N'Cục quân y')
SET IDENTITY_INSERT [dbo].[NguonCungCap] OFF
SET IDENTITY_INSERT [dbo].[NhanSu] ON 

INSERT [dbo].[NhanSu] ([MaNS], [HoTen], [NgaySinh], [QueQuan], [DanToc], [TonGiao], [CapBacSHSQ], [ChucVu], [NgayNhapNguTuyenDung], [XuatThan], [BanThan], [QuaTrinhHocTap], [ThanNhan], [GhiChu]) VALUES (1, N'BÙI ĐỨC THÀNH', N'12/10/1977', N'Vĩnh Bảo, Hải Phòng', N'Kinh', N'Không', N'Trung tá ', N'Giám đốc', N'09/1996', N'Viên chức', N'Bộ đội', N'Học viện Quân y, ĐH, Bác sĩ đa khoa(96-03) Học viện Quân y ,CH,GMHS (09-11) BVTWQD108,NCS Gây mê hồi sức (12-nay)', N'Bố: Bùi Duy Quân - 1948- HT Mẹ : Nguyễn Thị Huệ 1951 - NT', NULL)
INSERT [dbo].[NhanSu] ([MaNS], [HoTen], [NgaySinh], [QueQuan], [DanToc], [TonGiao], [CapBacSHSQ], [ChucVu], [NgayNhapNguTuyenDung], [XuatThan], [BanThan], [QuaTrinhHocTap], [ThanNhan], [GhiChu]) VALUES (2, N'NGUYỄN THÀNH CÔNG', N'24/11/1981', N'Ý Yên , Nam Định', N'Kinh', N'Không', N'Thiếu tá', N'Phó giám đốc chuyên môn', N'02/2006', N'Công nhân', N'Bộ đội', N'Đại học y Hải Phòng ĐH, BS ĐK(99-05) Đại học y dược TPHCM, CH Ung thư (10-13)', N'Bố: Nguyễn Quý Cường 1946 - HT Mẹ: Phùng Thị Hà - 1952 - HT', NULL)
SET IDENTITY_INSERT [dbo].[NhanSu] OFF
INSERT [dbo].[NhapChiTiet] ([MaPhieuNhap], [MaVatTu], [MaNuoc], [MaHang], [Model], [Seri], [SoKhung], [SoMay], [SoDangKy], [DotMua], [NamSX], [PhanCap], [NguonGoc], [SoLuong], [DonGia], [GhiChu]) VALUES (5, 8, 1, 1, N'string', N'string', N'string', N'string', N'string', N'string', N'string', N'string', N'string', 5, CAST(12000.00 AS Decimal(18, 2)), N'string')
INSERT [dbo].[NhapChiTiet] ([MaPhieuNhap], [MaVatTu], [MaNuoc], [MaHang], [Model], [Seri], [SoKhung], [SoMay], [SoDangKy], [DotMua], [NamSX], [PhanCap], [NguonGoc], [SoLuong], [DonGia], [GhiChu]) VALUES (5, 9, 1, 1, N'string', N'string', N'string', N'string', N'string', N'string', N'string', N'string', N'string', 6, CAST(150000.00 AS Decimal(18, 2)), N'string')
SET IDENTITY_INSERT [dbo].[NhapVatTu] ON 

INSERT [dbo].[NhapVatTu] ([MaPhieuNhap], [MaHM], [MaKho], [NgayNhap], [NguoiNhap], [TongSoTien], [TongSoLuong], [ChietKhau], [GhiChu], [Status]) VALUES (5, 1, 1, N'string', N'string', CAST(960000.00 AS Decimal(18, 2)), 11, 10, N'string', 1)
SET IDENTITY_INSERT [dbo].[NhapVatTu] OFF
SET IDENTITY_INSERT [dbo].[NuocSanXuat] ON 

INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (1, N'Việt Nam')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (2, N'Trung Quốc')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (3, N'Lào')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (4, N'Campuchia')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (5, N'Ả rập xê út')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (6, N'Romani')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (7, N'Canada')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (8, N'Mỹ')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (9, N'Anh')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (10, N'Pháp')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (11, N'Nhật Bản')
INSERT [dbo].[NuocSanXuat] ([MaNuoc], [TenNuoc]) VALUES (12, N'Thái lọ')
SET IDENTITY_INSERT [dbo].[NuocSanXuat] OFF
SET IDENTITY_INSERT [dbo].[VatTu] ON 

INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (8, 25, 2, N'Rơ mooc đơn trục', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (9, 25, 2, N'Rơ mooc đa trục', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (10, 26, 4, N'Dụng cụ bảo dưỡng xe ô tô', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (11, 26, 4, N'Bơm hơi và ống hơi', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (12, 27, 5, N'Súng ngắn 7,62mm K54M VN', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (13, 27, 5, N'Súng tiểu liên 7,62mm AKM LX', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (14, 28, 6, N'Đạn 7,62-K56-T-VN', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (15, 28, 6, N'Đạn 7,62-K51-T-VN', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (16, 29, 3, N'Mũ sắt chống đạn', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (17, 29, 3, N'Áo giáp chống đạn', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (18, 29, 3, N'Dao găm Military 168T MIL', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (19, 30, 2, N'Nẹp cố định chân', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (20, 30, 2, N'Máy sốc điện tự động(loại nhỏ)', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (21, 31, 2, N'Bơm tiêm điện', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (22, 31, 2, N'Máy ly tâm', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (24, 32, 2, N'Bể mềm xăng dầu', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (25, 32, 2, N'Bơm dầu tay', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (26, 33, 4, N'Micro karaoke', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (27, 33, 4, N'Chân loa sắt', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (28, 34, 7, N'Fentanyl 0,1mg', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (29, 34, 7, N'Morphin 10mg', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (30, 35, 7, N'Midazolam 5mg/ml', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (31, 35, 7, N'Atropin 0,25 mg/ml', NULL)
INSERT [dbo].[VatTu] ([MaVatTu], [MaLoaiVatTu], [MaDVT], [TenVT], [GhiChu]) VALUES (32, 36, 4, N'bang ve sinh', N'abc')
SET IDENTITY_INSERT [dbo].[VatTu] OFF
