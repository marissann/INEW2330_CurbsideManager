SELECT * FROM group3sp212330.OrdersTest;
SELECT * FROM group3sp212330.OrderInfoTest;
SELECT * FROM group3sp212330.Products;
--SELECT * FROM group3sp212330.Employees;

INSERT INTO group3sp212330.OrdersTest
(CustomerID, OrderDate, TotalPrice, EmployeeID)
VALUES
(10001, '2021-3-25', 32.96, 1004);

INSERT INTO group3sp212330.OrderInfoTest
(OrderID, ProductID, Quantity, Price)
VALUES
(2, 1048, 2, 9.99),
(2, 1044, 1, 9.99),
(2, 1024, 1, 2.99);