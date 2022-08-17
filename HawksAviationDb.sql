set nocount on
set dateformat mdy

USE master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning HawksAviationDb.SQL at %s ....',1,1,@dttm) with nowait
GO

CHECKPOINT
go

raiserror('Creating Hawks Aviations database....',0,1)
go

go
use master
go
CREATE DATABASE [HawksAviationDb]
go
use [HawksAviationDb]
go
CHECKPOINT

create table Airports
(
	AirportId varchar(10) primary key not null,
	Name varchar(20) not null,
	City varchar(10) not null,
	Country varchar(10) not null
)
go

create table Flights
(
	FlightNo int not null primary key IDENTITY(11021,5),
	FlightId varchar(10) not null,
	Name varchar(20) not null,
	Start varchar(10) not null Foreign key references Airports,
	Destination varchar(10) not null Foreign key references Airports,
	Arrival DateTime not null default CURRENT_TIMESTAMP,
	Departure DateTime not null default CURRENT_TIMESTAMP,
	TotalSeats int default 0,
	AvailableSeats int default 0,
	Fare float default 1500.00,
	IsActive bit default 1,
	Outdated bit default 0,
	cancelled bit default 0
)
go

create table Customers
(
	CustomerID int not null primary key IDENTITY(451275,6),
	FirstName varchar(50) not null, 
	LastName varchar(50) not null, 
	Age int not null default 18,
	Gender varchar(10) not null,
	EmailID varchar(25) unique not null,
	MobileNumber varchar(10) unique not null, 
	Username varchar(15) not null unique,
	[Password] varchar(16) not null
)

CREATE NONCLUSTERED INDEX NCI_Cust_Email
ON dbo.Customers(EmailID)

CREATE NONCLUSTERED INDEX NCI_Cust_MobNum
ON dbo.Customers(MobileNumber)

CREATE NONCLUSTERED INDEX NCI_Cust_UsNa
ON dbo.Customers(Username)
go

create table [Admin]
(
	AdminId int not null primary key IDENTITY(11101,3),
	FirstName varchar(50) not null, 
	LastName varchar(50) not null, 
	EmailId varchar(25) unique not null,
	Username varchar(15) not null,
	[Password] varchar(16) not null,
	[Role] varchar(20) not null
)
go

create table [Users]
(
	Username varchar(15) not null primary key,
	FirstName varchar(50) not null, 
	LastName varchar(50) not null, 
	UserId int not null,
	EmailId varchar(25) unique not null,
	[Password] varchar(16) not null,
	[Role] varchar(20) not null
)
go

create table Bookings
(
	BookingID int not null primary key IDENTITY(104515545,6),
	FlightNo int not null Foreign key references Flights,
	CustomerID int not null Foreign key references Customers,
	Seats int not null,
	BookingAmount float not null,
	Arrival DateTime default CURRENT_TIMESTAMP,
	Departure DateTime default CURRENT_TIMESTAMP,
	[Status] varchar(50) default 'In Progress',
	IsCancelled bit default 0,
	IsCheckedIn bit default 0,
	Outdated bit default 0
)
go

create table ExceptionLog
(
	Id int not null primary key IDENTITY(11101,3),
	[DataTime] DateTime default CURRENT_TIMESTAMP,
	ErrorDescription varchar(max) default '',
	Data varchar(100) default '',
	StackTrace varchar(max) default ''
)
go

INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'CFA', N'CFIA', N'USA', N'California')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'CHI', N'CIA', N'INDIA', N'Chennai')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'DLI', N'DIA', N'INDIA', N'New Delhi')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'HYD', N'JNIA', N'INDIA', N'Hyderabad')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'KOL', N'KIA', N'INDIA', N'Kolkata')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'LAS', N'LAIA', N'USA', N'LasAngeles')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'MUM', N'CST', N'INDIA', N'Mumbai')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'NYC', N'JFKIA', N'USA', N'New York')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'THI', N'TTD', N'INDIA', N'Tirupati')
INSERT INTO [dbo].[Airports] ([AirportID], [Name], [Country], [City]) VALUES (N'VJW', N'VJA', N'INDIA', N'Vijayawada')

SET IDENTITY_INSERT [dbo].[Flights] ON
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11021, N'HW-1515', N'Hawk', N'NYC', N'MUM', N'2022-08-18 19:59:21', N'2022-08-18 19:59:21', 100, 5, 1500)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11036, N'HW-1515', N'Hawk', N'NYC', N'MUM', N'2022-08-18 19:59:21', N'2022-08-18 19:59:21', 270, 180, 4500)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11041, N'HW-1616', N'Hawk', N'NYC', N'MUM', N'2022-08-19 19:59:21', N'2022-08-19 19:59:21', 21, 0, 20000)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11061, N'HW-1518', N'Hawk', N'NYC', N'MUM', N'2022-08-18 19:59:21', N'2022-08-18 21:59:21', 500, 200, 2000)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11071, N'HW-1515', N'Hawk air', N'MUM', N'NYC', N'2022-07-21 13:00:11', N'2022-07-21 14:00:11', 120, 100, 1000)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11111, N'HW-1515', N'Hawk air', N'HYD', N'NYC', N'2022-07-30 20:00:11', N'2022-07-21 23:00:11', 150, 110, 5000)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11126, N'HA-1213', N'HawksAI', N'NYC', N'MUM', N'2022-07-30 17:35:00', N'2022-07-30 18:35:00', 120, 100, 1500)
INSERT INTO [dbo].[Flights] ([FlightNo], [FlightID], [Name], [Start], [Destination], [Arrival], [Departure], [TotalSeats], [AvailableSeats], [Fare]) VALUES (11131, N'HW-3543', N'Hawk Air', N'THI', N'MUM', N'2022-08-02 12:30:00', N'2022-08-02 13:30:00', 160, 150, 2000)
SET IDENTITY_INSERT [dbo].[Flights] OFF


SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451275, N'Shark', N'G', 25, N'Male', N'shshsks@mffm.com', N'4855125851', N'cust123', N'54321')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451287, N'Shark', N'G', 21, N'Male', N'shshsks@mfm.com', N'4855125854', N'hdhdjd', N'fkfkfkfkd')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451317, N'ss', N'f', 22, N'Male', N'sksksk@fhf.c', N'4585845554', N'cust126', N'123')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451383, N'SSS', N'GSS', 22, N'Male', N'ddhdu@hfh.cjd', N'4581247515', N'user456', N'456')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451407, N'Sai Shashank', N'Gandavarapu', 22, N'Male', N'shanki150@outlook.com', N'124512531', N'cust8008', N'8008')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451443, N'SS', N'G', 25, N'Male', N'dssdchyr@ddvs.nh', N'4855157851', N'user753', N'753')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451449, N'G', N'SS', 22, N'Male', N'xvfsfsbs@hf.nn', N'4524524821', N'user99', N'99')
INSERT INTO [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Age], [Gender], [EmailID], [MobileNumber], [Username], [Password]) VALUES (451473, N'Mary', N'Jane', 25, N'Female', N'csscscsfe@dcc.cd', N'4595578412', N'user741', N'741')
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT INTO [dbo].[Admin] ([AdminId], [FirstName], [LastName], [EmailId], [Username], [Password], [Role]) VALUES (11101, N'user123', N'23', N'ajdjdk@jfjf.com', N'user123', N'123456', N'Admin')
INSERT INTO [dbo].[Admin] ([AdminId], [FirstName], [LastName], [EmailId], [Username], [Password], [Role]) VALUES (11116, N'SS', N'G', N'dvdfv@ssd.bvr', N'user789', N'789', N'Admin')
SET IDENTITY_INSERT [dbo].[Admin] OFF


INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'cust123', N'Shark', N'G', N'shshsks@mffm.com', N'54321', N'Customer', 451275)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'cust126', N'ss', N'f', N'sksksk@fhf.c', N'123', N'Customer', 451317)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'cust8008', N'Sai Shashank', N'Gandavarapu', N'shanki150@outlook.com', N'8008', N'Customer', 451401)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user123', N'user1', N'23', N'ajdjdk@jfjf.com', N'123456', N'Admin', 11101)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user456', N'SS', N'G', N'ddhdu@hfh.cjd', N'456', N'Customer', 451383)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user741', N'SSSSSS', N'GDU', N'csscscsfe@dcc.cd', N'741', N'Customer', 451473)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user753', N'SS', N'G', N'dssdchyr@ddvs.nh', N'753', N'Customer', 451443)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user789', N'SS', N'G', N'dvdfv@ssd.bvr', N'789', N'Admin', 11116)
INSERT INTO [dbo].[Users] ([Username], [FirstName], [LastName], [EmailId], [Password], [Role], [UserId]) VALUES (N'user99', N'G', N'SS', N'xvfsfsbs@hf.nn', N'99', N'Customer', 451449)


SET IDENTITY_INSERT [dbo].[Bookings] ON
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515545, 11021, 451275, 5, 14512, N'2022-07-20 19:59:21', N'2022-07-20 21:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515551, 11021, 451275, 5, 14512, N'2022-07-20 19:59:21', N'2022-07-20 21:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515557, 11021, 451275, 5, 14512, N'2022-07-21 19:32:00', N'2022-07-21 19:32:00', N'Inprogress')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515563, 11021, 451275, 5, 7500, N'2022-07-21 19:47:17', N'2022-07-21 19:47:17', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515569, 11021, 451275, 5, 7500, N'2022-07-21 19:50:58', N'2022-07-21 19:50:58', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515575, 11021, 451275, 10, 15000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Cancel')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515587, 11021, 451275, 5, 7500, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Booked')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515593, 11021, 451275, 5, 7500, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515599, 11041, 451287, 5, 100000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515605, 11041, 451275, 5, 100000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Booked')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515611, 11041, 451383, 5, 100000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Booked')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515617, 11036, 451383, 10, 45000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Booked')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515623, 11036, 451383, 10, 45000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515629, 11036, 451383, 6, 27000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515635, 11036, 451383, 5, 22500, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515641, 11036, 451383, 2, 9000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515647, 11036, 451383, 6, 27000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Cancelled')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515653, 11036, 451383, 5, 22500, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515659, 11036, 451383, 10, 45000, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'CheckedIn')
INSERT INTO [dbo].[Bookings] ([BookingID], [FlightNo], [CustomerID], [Seats], [BookingAmount], [Arrival], [Departure], [Status]) VALUES (104515677, 11021, 451383, 5, 7500, N'2022-07-20 19:59:21', N'2022-07-20 19:59:21', N'Booked')
SET IDENTITY_INSERT [dbo].[Bookings] OFF
go

create PROCEDURE [dbo].[DeleteOldFlights]
        -- Add the parameters for the stored procedure here
        AS
        BEGIN
            -- SET NOCOUNT ON added to prevent extra result sets from
            -- interfering with SELECT statements.
            SET NOCOUNT ON;
			UPDATE Flights
			SET outdated = 1
			WHERE Arrival < CURRENT_TIMESTAMP;
            -- Insert statements for procedure here
        END
go

create PROCEDURE [dbo].[Fullybooked]
        -- Add the parameters for the stored procedure here
        AS
        BEGIN
            -- SET NOCOUNT ON added to prevent extra result sets from
            -- interfering with SELECT statements.
            SET NOCOUNT ON;
			UPDATE Flights
			SET isActive = 0
			WHERE AvailableSeats <= 0;
            -- Insert statements for procedure here
END
go

create PROCEDURE [dbo].[FullyNotbooked]
        -- Add the parameters for the stored procedure here
        AS
        BEGIN
            -- SET NOCOUNT ON added to prevent extra result sets from
            -- interfering with SELECT statements.
            SET NOCOUNT ON;
			UPDATE Flights
			SET isActive = 1
			WHERE AvailableSeats > 0;
            -- Insert statements for procedure here
END
go

create PROCEDURE [dbo].[DeleteOldBookings]
        -- Add the parameters for the stored procedure here
        AS
        BEGIN
            -- SET NOCOUNT ON added to prevent extra result sets from
            -- interfering with SELECT statements.
            SET NOCOUNT ON;
			UPDATE Bookings
			SET outdated = 1
			WHERE Arrival < CURRENT_TIMESTAMP;
            -- Insert statements for procedure here
        END
go


--EXEC [dbo].[DeleteOldFlights]
--EXEC [dbo].[DeleteOldBookings]
--EXEC [dbo].[Fullybooked]
--Select * from Flights