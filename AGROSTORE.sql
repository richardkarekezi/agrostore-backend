USE [AGROSTORE]
GO
/****** Object:  Table [dbo].[Fertilizer_Register]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fertilizer_Register](
	[IdFertilizer] [int] IDENTITY(1,1) NOT NULL,
	[FertilizerCode] [nvarchar](50) NOT NULL,
	[FertilizerName] [nvarchar](50) NOT NULL,
	[QuantityLimit] [float] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[MeasurementUnit] [nvarchar](50) NOT NULL,
	[Available] [bit] NOT NULL,
 CONSTRAINT [PK_Fertilizer_Register] PRIMARY KEY CLUSTERED 
(
	[FertilizerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [unique_FertilizerName] UNIQUE NONCLUSTERED 
(
	[FertilizerName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seed_Register]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seed_Register](
	[IdSeed] [int] IDENTITY(1,1) NOT NULL,
	[SeedCode] [nvarchar](50) NOT NULL,
	[SeedName] [nvarchar](50) NOT NULL,
	[FertilizerCode] [nvarchar](50) NOT NULL,
	[QuantityLimit] [float] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[MeasurementUnit] [nvarchar](50) NOT NULL,
	[Available] [bit] NOT NULL,
 CONSTRAINT [PK_Seed_Register] PRIMARY KEY CLUSTERED 
(
	[SeedCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [unique_SeedName] UNIQUE NONCLUSTERED 
(
	[SeedName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Seed_Details]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Seed_Details]
AS
SELECT dbo.Seed_Register.SeedCode, dbo.Seed_Register.SeedName, CONVERT(nvarchar(50), dbo.Seed_Register.QuantityLimit) + ' ' + dbo.Seed_Register.MeasurementUnit AS [Quantity limit], dbo.Seed_Register.UnitPrice, 
                  dbo.Fertilizer_Register.FertilizerName, dbo.Seed_Register.Available, dbo.Fertilizer_Register.FertilizerCode
FROM     dbo.Seed_Register INNER JOIN
                  dbo.Fertilizer_Register ON dbo.Seed_Register.FertilizerCode = dbo.Fertilizer_Register.FertilizerCode
GO
/****** Object:  Table [dbo].[Order_Details]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Details](
	[IdRecord] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](50) NOT NULL,
	[ItemType] [nvarchar](50) NOT NULL,
	[CodeItem] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[LandSize] [nvarchar](50) NOT NULL,
	[LandMeasurementUnit] [nvarchar](50) NOT NULL,
	[Quantity] [float] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[SubTotal] [float] NOT NULL,
 CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED 
(
	[OrderCode] ASC,
	[ItemType] ASC,
	[CodeItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Register]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Register](
	[IdOrder] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](50) NOT NULL,
	[FarmerName] [nvarchar](150) NOT NULL,
	[FarmerPhone] [nvarchar](50) NOT NULL,
	[RecordDate] [date] NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order_Register] PRIMARY KEY CLUSTERED 
(
	[OrderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_FERTILIZER]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATE_FERTILIZER]
@FertilizerName nvarchar(50),
@QuantityLimit float,
@UnitPrice float
as
declare @FertilizerCode nvarchar(50)
declare @Counter int
declare @CodeVal int
set @Counter=(select count(*) from Fertilizer_Register)
set @CodeVal=100000+@Counter;
set @FertilizerCode='F'+CONVERT(NVARCHAR(50),@CodeVal)
begin try
insert into Fertilizer_Register values (@FertilizerCode,@FertilizerName,@QuantityLimit,@UnitPrice,'KG',1)
select '200','SUCCESS'
end try
begin catch 
select convert(nvarchar(50),ERROR_NUMBER()),ERROR_MESSAGE();
end catch
GO
/****** Object:  StoredProcedure [dbo].[CREATE_ORDER]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATE_ORDER]
@FarmerName NVARCHAR(50),
@FarmerPhone NVARCHAR(50)
AS
DECLARE @OrderCode NVARCHAR(50)
Declare @Counter int
declare @CodeVal int
set @Counter=(select count(*) from Order_Register)
set @CodeVal=100000+@Counter;
set @OrderCode='OR'+CONVERT(NVARCHAR(50),@CodeVal)
BEGIN TRY
INSERT INTO Order_Register VALUES (@OrderCode,@FarmerName,@FarmerPhone,GETDATE(),'PENDING')
select '200','SUCCESS',max(OrderCode) from Order_Register where FarmerName=@FarmerName and FarmerPhone=@FarmerPhone 
END TRY
BEGIN CATCH
select convert(nvarchar(50),ERROR_NUMBER()),ERROR_MESSAGE(),'';
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[CREATE_ORDER_DETAILS]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATE_ORDER_DETAILS]
@OrderCode nvarchar(50),
@ItemType nvarchar(50),
@CodeItem nvarchar(50),
@LandSize float
as
declare @Quantity float
declare @UnitPrice float
declare @SubTotal float
declare @Status nvarchar(50)
declare @SeedQtyLimit float
declare @FertilizerQtyLimit float
declare @ItemName nvarchar(50)
select @Status=OrderStatus from Order_Register where OrderCode=@OrderCode
if @ItemType='Fertilizer'
begin
select @FertilizerQtyLimit=QuantityLimit,@UnitPrice=UnitPrice,@ItemName=FertilizerName from Fertilizer_Register where [FertilizerCode]=@CodeItem
set @Quantity=round((@LandSize*@FertilizerQtyLimit),0)
set @SubTotal=round((@Quantity*@UnitPrice),0)

end 
if @ItemType='Seed'
begin
select @FertilizerQtyLimit=QuantityLimit,@UnitPrice=UnitPrice,@ItemName=SeedName from Seed_Register where SeedCode=@CodeItem
set @Quantity=round((@LandSize*@FertilizerQtyLimit),0)
set @SubTotal=round((@Quantity*@UnitPrice),0)
end 

if @Status='PENDING'
BEGIN
begin try
insert into [dbo].[Order_Details] values(@OrderCode,@ItemType,@CodeItem,@ItemName,@LandSize,'Acre(s)',@Quantity,@UnitPrice,@SubTotal)
select '200','SUCCESS'
END TRY
BEGIN CATCH
select convert(nvarchar(50),ERROR_NUMBER()),ERROR_MESSAGE();
END CATCH
END
ELSE
BEGIN
select '405','ORDER STATUS: '+@Status

END

GO
/****** Object:  StoredProcedure [dbo].[CREATE_SEED]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATE_SEED]
@SeedName NVARCHAR(50),
@FertilizerCode nvarchar(50),
@QuantityLimit float,
@UnitPrice float
as 
declare @SeedCode nvarchar(50)
declare @Counter int
declare @CodeVal int
set @Counter=(select count(*) from Seed_Register)
set @CodeVal=100000+@Counter;
set @SeedCode='S'+CONVERT(NVARCHAR(50),@CodeVal)
BEGIN TRY
INSERT INTO Seed_Register VALUES (@SeedCode,@SeedName,@FertilizerCode,@QuantityLimit,@UnitPrice,'KG',1)
select '200','SUCCESS'
END TRY
BEGIN CATCH
select convert(nvarchar(50),ERROR_NUMBER()),ERROR_MESSAGE();
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Delete_OrderDetail]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Delete_OrderDetail]
@idRecord int,
@OrderCode nvarchar(50)
as
declare @Status nvarchar(50)
select @Status=OrderStatus from Order_Register where OrderCode=@OrderCode
if @Status='PENDING'
BEGIN
delete Order_Details where IdRecord=@idRecord
select '200','SUCCESS'
END
GO
/****** Object:  StoredProcedure [dbo].[DISCARD_ORDER]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DISCARD_ORDER]
@OrderCode NVARCHAR(50)
AS
UPDATE Order_Register SET OrderStatus='DISCARDED' WHERE OrderCode=@ORDERCODE and OrderStatus='PENDING'
select '200','SUCCESS'
GO
/****** Object:  StoredProcedure [dbo].[Update_Order_Status]    Script Date: 26/10/2023 19:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Update_Order_Status]
@Status nvarchar(50),
@OrderCode nvarchar(50)
as
update Order_Register set OrderStatus=@Status where OrderCode=@OrderCode
select '200','SUCCESS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Seed_Register"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 230
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Fertilizer_Register"
            Begin Extent = 
               Top = 7
               Left = 308
               Bottom = 170
               Right = 520
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Seed_Details'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Seed_Details'
GO
