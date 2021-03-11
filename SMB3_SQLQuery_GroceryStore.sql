USE [inew2330sp21]
GO

-- Table Creation Queries

CREATE TABLE [group3sp212330].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[HireDate] [date] NOT NULL,
	[isAdmin] [bit] NOT NULL, 
	PRIMARY KEY (EmployeeID));

	CREATE TABLE [group3sp212330].[Customers](
	[CustomerID] [int] IDENTITY(10000 ,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[Phone] [varchar](20) NULL,
	[Email] [varchar] (50) NOT NULL, 
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	PRIMARY KEY (CustomerID));
	
	CREATE TABLE [group3sp212330].[Orders](	
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[TotalPrice] [money] NULL,
	[EmployeeID] [int] NULL,
	PRIMARY KEY (OrderID),
	CONSTRAINT FK_Orders_CustomerID FOREIGN KEY (CustomerID) REFERENCES [group3sp212330].[Customers](CustomerID),
	CONSTRAINT FK_Orders_EmployeeID FOREIGN KEY (EmployeeID) REFERENCES [group3sp212330].[Employees](EmployeeID));

	CREATE TABLE [group3sp212330].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](20) NOT NULL,
	[Description] [varchar](100) NULL,
	PRIMARY KEY (CategoryID));

	CREATE TABLE [group3sp212330].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Quantity] [varchar](20) NULL,
	[Price] [money] NOT NULL,
	[inStock] [bit] NOT NULL,	
	PRIMARY KEY (ProductID),
	CONSTRAINT FK_Products_CategoryID FOREIGN KEY (CategoryID) REFERENCES [group3sp212330].[Categories](CategoryID));


	CREATE TABLE [group3sp212330].[OrderInfo](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [money] NULL, 
	PRIMARY KEY (OrderID, ProductID),
	CONSTRAINT FK_OrderInfo_OrderID FOREIGN KEY (OrderID) REFERENCES [group3sp212330].Orders(OrderID),
	CONSTRAINT FK_OrderInfo_ProductID FOREIGN KEY (ProductID) REFERENCES [group3sp212330].Products(ProductID));

-- Employee Insertion Queries (2 Admins, 8 Employees)

	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Wahooo1!','Mario', 'Mario', '2021-01-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Yahooo2@','Luigi', 'Mario', '2021-01-01', '1');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Cakeee3#','Princess', 'Toadstool', '2021-01-01', '1');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Yoshii4$','T', 'Yoshisaur', '2021-01-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Aaaaah5%','Toad', 'Toadsworth', '2021-01-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Marioo6^','King', 'Koopa', '2021-02-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Moneyy7&','Wario', 'Scapelli', '2021-02-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Momaaa8*','Bowser', 'Junior', '2021-02-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('PowPow9(','Morton', 'Koopa', '2021-02-01', '0');


	INSERT INTO [group3sp212330].[Employees] ([Password], [FirstName], [LastName], [HireDate], [isAdmin])
	VALUES ('Bleckbleck1)','Count', 'Bleck', '2021-03-01', '0');


-- Customer Insertion Queries (Threw in two just in case we need them while building)

	INSERT INTO [group3sp212330].[Customers] ([FirstName], [LastName],[Email],[UserName],[Password])
	VALUES ('Brian','Ortiz','baortiz89400@mymail.tstc.edu','EffectedSage0','BrianSage0@');

	INSERT INTO [group3sp212330].[Customers] ([FirstName], [LastName],[Phone],[Email],[UserName],[Password])
	VALUES ('Scott','Greenboard','254-632-8495','saGrenboard@mymail.tstc.edu','Noax','TheGreenBoard03');

-- Category Insertion Queries (More to come as we work)

	INSERT INTO [group3sp212330].Categories ([CategoryName],[Description])
	VALUEs ('Dairy','Dairy products or milk products are a type of food produced from or containing the milk of mammals.');  

	INSERT INTO [group3sp212330].Categories ([CategoryName],[Description])
	VALUEs ('Grain','Food made from wheat, rice, oats, cornmeal, barley, or another cereal grain is a grain product.');

-- Product Insertion Queries (More to come as we work)

	INSERT INTO [group3sp212330].Products ([CategoryID],[ProductName],[Quantity],[Price],[inStock])
	VALUES ('1','Whole Milk','250','1.99','1');

	INSERT INTO [group3sp212330].Products ([CategoryID],[ProductName],[Quantity],[Price],[inStock])
	VALUES ('1','2 Percent Milk','150','2.29','1');

	INSERT INTO [group3sp212330].Products ([CategoryID],[ProductName],[Quantity],[Price],[inStock])
	VALUES ('2','Honey Nut Cheerios','0','2.49','0');

-- Possible Select Queries that may be used by the C# application
	
--Assuming Brian from Customers is trying to log in, Username and Password are likely passed in by a variable
	SELECT * FROM group3sp212330.Customers
	WHERE Username = 'EffectedSage0'
	AND Password = 'BrianSage0@';

--Query that can be used at sign-up to check if a username is already taken
	SELECT * FROM group3sp212330.Customers
	WHERE Username = 'EffectedSage0';

--Query that can be used at recovery to check which email address to send info to (can also double as a check to see if email is taken at signup)
	SELECT * FROM group3sp212330.Customers
	WHERE Username = 'baortiz89400@mymail.tstc.edu';

--Assuming Luigi from Employees is trying to log in. (EmployeeID doubles as Username in our system)
	SELECT * FROM group3sp212330.Employees
	WHERE EmployeeID = 2
	AND Password = 'Yahooo2@';

--Assuming a customer is browsing for something with a search bar
	SELECT * FROM group3sp212330.Products
	WHERE ProductName LIKE '%Honey%';

--Assuming a customer is browsing soley for Dairy products
	SELECT * FROM group3sp212330.Products
	WHERE CategoryID = 1;

--Query that would be used to check any unhandled orders
	SELECT * FROM group3sp212330.Orders
	WHERE EmployeeID IS NULL;

--Query to display all items from a certain order (1 for example, no orders in table yet as that will be handled by app)
	SELECT * FROM group3sp212330.OrderInfo
	WHERE OrderID = 1;
