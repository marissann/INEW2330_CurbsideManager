USE inew2330sp21

--Add the IsActive column to the Employees table
ALTER TABLE group3sp212330.Employees
ADD IsActive BIT;

--Set the IsActive value to TRUE for all employees
--NOTE: ALTER TABLE doesn't let you add a NOT NULL column in non-empty tables, so IsActive is a nullable column
UPDATE group3sp212330.Employees
SET IsActive = 1;

SELECT * FROM group3sp212330.Employees;