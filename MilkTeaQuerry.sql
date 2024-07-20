create database [MilkTea]

USE [MilkTea]
GO
-- Employees table
CREATE TABLE [dbo].[Employees](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[fullName] [nvarchar](250) NULL,
	[phoneNumber] [varchar](15) NULL,
	[address] [nvarchar](500) NULL,
	[idCard] [varchar](150) NULL,
	[dateWork] [date] NULL,
	[img] [varbinary](max) NULL,
	[status] [bit] NULL,
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Employees]([fullName],[phoneNumber],[address],[status]) VALUES ('Nguyễn Đức Huấn','0908892160','Việt Nam',1);
INSERT [dbo].[Employees]([fullName],[phoneNumber],[address],[status]) VALUES ('Ngô Xuân Sơn','0908892160','Việt Nam',1);
GO
-- Login table
CREATE TABLE [dbo].[Login](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[idEmployee] [bigint] NULL,
	[userName] [varchar](250) NULL,
	[password] [varchar](max) NULL,
	[isUse] [bit] NULL,
	FOREIGN KEY (idEmployee) REFERENCES Employees(id)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Login] ( [idEmployee], [userName], [password], [isUse]) VALUES (1, N'Manager', N'6b86b273ff34fce19d6b804eff5a3f57', 1);
INSERT [dbo].[Login] ([idEmployee], [userName], [password], [isUse]) VALUES (2, N'Employee', N'6b86b273ff34fce19d6b804eff5a3f57', 1);
GO
-- Role table
Create TABLE [dbo].[TbRole](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleName] NVARCHAR(250) NULL
	)on [PRIMARY]
	GO
	INSERT [dbo].[TbRole] ([RoleName])VALUES ('admin')
	INSERT [dbo].[TbRole] ([RoleName])VALUES ('SaleStaff')
	Go
-- LoginRole table
CREATE TABLE [dbo].[LoginRole](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[idLogin] [bigint] NOT NULL,
	[idRole] [int] NOT NULL,
	FOREIGN KEY (idLogin) REFERENCES Login(id),
	FOREIGN KEY (idRole) REFERENCES TbRole(id)
) ON [PRIMARY]
GO

INSERT [dbo].[LoginRole] ( [idLogin], [idRole]) VALUES (1, 1)
INSERT [dbo].[LoginRole] ( [idLogin], [idRole]) VALUES (2, 2)
GO
-- GroupProduct table
CREATE TABLE [dbo].[tbGroupProduct](
	[idGr] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[nameGr] [nvarchar](500) NULL,
	[descriptionGr] [nvarchar](500) NULL,
) ON [PRIMARY]
GO
INSERT [dbo].[tbGroupProduct] ( [nameGr], [descriptionGr]) VALUES ( N'Đồ uống', N'danh sách đồ uống')
INSERT [dbo].[tbGroupProduct] ( [nameGr], [descriptionGr]) VALUES ( N'Đồ ăn vặt', NULL)
GO
-- Product table
CREATE TABLE [dbo].[tbProduct](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[idGroupProduct] [bigint] NULL,
	[name] [nvarchar](500) NULL,
	[unit] [nvarchar](50) NULL,
	[unitPrice] [float] NULL,
	[description] [nvarchar](500) NULL,
	[img] [varbinary](max) NULL,
	FOREIGN KEY (idGroupProduct) REFERENCES tbGroupProduct(idGr)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
-- GroupTb table
CREATE TABLE [dbo].[tbGroupTb](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](500) NULL,
) ON [PRIMARY]
GO
INSERT [dbo].[tbGroupTb] ( [name], [description]) VALUES ( N'Phòng lạnh', N'lầu 1 đó')
INSERT [dbo].[tbGroupTb] ( [name], [description]) VALUES ( N'Sảnh trước', N'tầng trệt')
-- Table (tbTable) table
CREATE TABLE [dbo].[tbTable](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[nameTb] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[idGroup] [bigint] NULL,
	[status] [bit] NULL,
	FOREIGN KEY (idGroup) REFERENCES tbGroupTb(id)
) ON [PRIMARY]
GO
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 1', N'không có gì dâu', 2, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 2', N'cái này là bàn 2', 2, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 3', N'cái này là bàn 3', 2, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 4', N'cái này là bàn 4', 2, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 5', N'đây là bàn 5', 2, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 6', N'đây là bàn 6', 1, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 7', N'đây là bàn 7', 1, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 8', N'đây là bàn 8', 1, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 9', N'đây là bàn 9', 1, 0)
INSERT [dbo].[tbTable] ( [nameTb], [description], [idGroup], [status]) VALUES (N'Bàn 10', N'không cần', 1, 0)
-- Bill table
CREATE TABLE [dbo].[tbBill](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[idTable] [bigint] NULL,
	[billDate] [datetime] NULL,
	[totalMoney] [float] NULL,
	[status] [bit] NULL,
	[description] [nvarchar](500) NULL,
	[idUser] [bigint] NULL,
	FOREIGN KEY (idTable) REFERENCES tbTable(id),
	FOREIGN KEY (idUser) REFERENCES Login(id)
) ON [PRIMARY]
GO
-- BillDetail table
CREATE TABLE [dbo].[tbBillDetailt]([id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[unitPrice] [float] NULL,
	[quantity] [int] NULL,
	[idBill] [bigint] NULL,
	[idProduct] [bigint] NULL,
	[intoMoney] [float] NULL,
	[description] [nvarchar](500) NULL,
	FOREIGN KEY (idBill) REFERENCES tbBill(id),
	FOREIGN KEY (idProduct) REFERENCES tbProduct(id)
) ON [PRIMARY]
GO
-- Store table
CREATE TABLE [dbo].[tbStore](
	[id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[nameStore] [nvarchar](500) NULL,
	[addressStore] [nvarchar](500) NULL,
	[phoneStore] [nvarchar](500) NULL,
	[taxCode] [nvarchar](250) NULL,
) ON [PRIMARY]
GO
