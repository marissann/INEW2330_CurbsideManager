using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SMB3_Curbside_Manager
{

    class ProgOps
    {
        //Variables and Constants
        //Bool to check if the app has connected to the database
        public static bool isConnected = false;

        //Set up connection string
        private const string CONNECT_STRING = @"Server=cstnt.tstc.edu;" +
                                                "Database=inew2330sp21;" +
                                                "User Id=group3sp212330;" +
                                                "Password=3815294;";
        //Build connection to the database
        private static SqlConnection _cntDatabase = new SqlConnection(CONNECT_STRING);
        // add cmd object
        private static SqlCommand _sqlResultsCommand;
        //add the data adapter
        private static SqlDataAdapter _daEmployees = new SqlDataAdapter();
        // add the data tables
        private static DataTable _dtEmployeesTable = new DataTable();
        private static StringBuilder errorMessages = new StringBuilder();

        //Variables to keep track of user name and data upon login
        public static int userID;
        public static string userFirstName;

        //information for the merchandise
        private static SqlDataAdapter _daMerchandise = new SqlDataAdapter();
        private static DataTable _dtMerchandiseTable = new DataTable();

        //Create adapter and table for Orders
        private static SqlDataAdapter _daOrderInfo = new SqlDataAdapter();
        private static DataTable _dtOrderInfoTable = new DataTable();

        //Create Adapter And Table For OrderInfo
        private static SqlDataAdapter _daOrders = new SqlDataAdapter();
        private static DataTable _dtOrdersTable = new DataTable();

        //Array to hold recovered login information
        private static string[] _recoveredLogin = new string[2];

        //Getter Functions for DataTables
        public static DataTable GetEmployeeTable
        {
            //Return the employee data table when called
            get { return _dtEmployeesTable; }
        }
        public static DataTable GetMerchandiseTable
        {
            //Return the merch data table when called
            get { return _dtMerchandiseTable; }
        }
        public static DataTable GetOrderInfoTable
        {
            //Return the order info data table when called
            get { return _dtOrderInfoTable; }
        }
        public static DataTable GetOrdersTable
        {
            //Return the order data table when called
            get { return _dtOrdersTable; }
        }

        //Functions
        public static void OpenDatabase()
        {
            try
            {
                //try to open the database
                _cntDatabase.Open();

                //Tell the user it worked
                MessageBox.Show("Connection to Database Opened Successfully!", "Connection Status",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) //Note: May change to SqlException down the road
            {
                MessageBox.Show(e.Message);


            }
        }
        public static void CloseDatabase()//Attemp to Close The Database
        {
            try
            {
                //closes Database 
                _cntDatabase.Close();
                //dispose of Database
                _cntDatabase.Dispose();
                //dispose of everything else
                /*
                _sqlResultsCommand.Dispose();
                _daEmployees.Dispose();
                _dtEmployeesTable.Dispose();
                */
                MessageBox.Show("Connection to Database Closed Successfully!", "Connection Status",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) //Note: May change to SqlException down the road
            {
                MessageBox.Show(e.Message);
            }
        }
        public static char LoginCommand(TextBox username, TextBox password)
        {

            //Pass username and password to the customers table using a query
            try
            {
                //local variables
                bool userFound = false;

                //string to build query
                string query = "Select * From group3sp212330.Employees " +
                               "WHERE Username = '" + username.Text + "' " +
                               "AND Password = '" + password.Text + "';";

                //establish command object
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                //establish data adapter
                _daEmployees = new SqlDataAdapter();
                _daEmployees.SelectCommand = _sqlResultsCommand;
                //fill data table
                _dtEmployeesTable = new DataTable();
                _daEmployees.Fill(_dtEmployeesTable);

                //Check if the passed data acquired a row
                if (_dtEmployeesTable.Rows.Count == 1)
                    userFound = true;

                //dispose of connection objects
                _sqlResultsCommand.Dispose();
                _daEmployees.Dispose();
                _dtEmployeesTable.Dispose();

                //If an employee is found, start process to check for admin
                if (userFound)
                {
                    //string to build query
                    query = "Select * From group3sp212330.Employees " +
                                   "WHERE Username = '" + username.Text + "' " +
                                   "AND Password = '" + password.Text + "' " +
                                   "AND IsAdmin = 1;";

                    //establish command object
                    _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                    //establish data adapter
                    _daEmployees = new SqlDataAdapter();
                    _daEmployees.SelectCommand = _sqlResultsCommand;
                    //fill data table
                    _dtEmployeesTable = new DataTable();
                    _daEmployees.Fill(_dtEmployeesTable);

                    //Check if the user found is an employee or an admin
                    if (_dtEmployeesTable.Rows.Count == 1)
                    {
                        //dispose of connection objects
                        _sqlResultsCommand.Dispose();
                        _daEmployees.Dispose();
                        _dtEmployeesTable.Dispose();

                        //Return an 'A' to signal an admin was found
                        return 'A';
                    }
                    else
                    {
                        //dispose of connection objects
                        _sqlResultsCommand.Dispose();
                        _daEmployees.Dispose();
                        _dtEmployeesTable.Dispose();

                        //Return an 'E' to signal an employee was found
                        return 'E';
                    }
                }
                //If an employee was not found, start process to check for customer login
                else
                {
                    //string to build query
                    query = "Select * " +
                            "From group3sp212330.Customers " +
                            "WHERE UserName = '" + username.Text + "' " +
                            "AND Password = '" + password.Text + "';";

                    //establish command object
                    _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                    //establish data adapter
                    _daEmployees = new SqlDataAdapter();
                    _daEmployees.SelectCommand = _sqlResultsCommand;
                    //fill data table
                    _dtEmployeesTable = new DataTable();
                    _daEmployees.Fill(_dtEmployeesTable);

                    //Check if the passed data acquired a row
                    if (_dtEmployeesTable.Rows.Count == 1)
                        userFound = true;

                    //dispose of connection objects
                    _sqlResultsCommand.Dispose();
                    _daEmployees.Dispose();
                    _dtEmployeesTable.Dispose();
                }

                //return a 'C' if a customer was found, otherwise return an 'F' for failed login
                if (userFound)
                    return 'C';
                else
                    return 'F';
            }
            catch (SqlException ex)
            {
                if (ex is SqlException)
                {//handles more specific SqlException here.
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on DatabaseCommand", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Returns 'Z' to signal a program error
                    return 'Z';
                }
                else
                {//handles generic ones here
                    MessageBox.Show(ex.Message, "Error on DatabaseCommand", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Returns 'Z' to signal a program error
                    return 'Z';
                }
            }
        }
        public static void InsertToDatabase(TextBox firstName, TextBox lastName, MaskedTextBox phone, TextBox Email, TextBox Username, TextBox Password)
        {

            try
            {
                string query = "INSERT INTO group3sp212330.Customers(FirstName , LastName , Phone , Email , UserName , Password)" +
                "values(" + "'" + firstName.Text + "','" + lastName.Text + "','" + phone.Text + "','" + Email.Text + "','" + Username.Text + "','" + Password.Text + "'" + ")";
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                _sqlResultsCommand.ExecuteNonQuery();

                //Dispose of the cmd obj
                _sqlResultsCommand.Dispose();
            }
            catch (SqlException ex)
            {
                if (ex is SqlException)
                {//handles more specific SqlException here.
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on DatabaseCommand", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static bool RecoveryCommand(TextBox email)
        {
            try
            {
                bool userFound = false;

                string query = "SELECT * FROM group3sp212330.Customers " +
                               "WHERE Email = '" + email.Text + "';";

                //establish command object //search for that query in that db
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                //establish data adapter  //after it runs the query, this is what we get and use that to fil the table (see below)
                _daEmployees = new SqlDataAdapter();
                _daEmployees.SelectCommand = _sqlResultsCommand;
                //fill data table
                //uses this to read and fill
                _dtEmployeesTable = new DataTable();
                _daEmployees.Fill(_dtEmployeesTable);

                //Check if the passed data acquired a row
                if (_dtEmployeesTable.Rows.Count == 1)
                    userFound = true;

                //dispose of connection objects
                _sqlResultsCommand.Dispose();
                _daEmployees.Dispose();
                _dtEmployeesTable.Dispose();

                return userFound;
            }
            catch (SqlException ex)
            {
                if (ex is SqlException)
                {//handles more specific SqlException here.
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on RecoveryCommand", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        public static void FetchMerchandise(TextBox ProductID, TextBox CategoryID, TextBox ProductName, TextBox Quanity, TextBox Price, TextBox InStock)
        {
            //string to build query
            string query = "select * From group3sp212330.Products ORDER BY ProductName";

            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daMerchandise = new SqlDataAdapter();
            _daMerchandise.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtMerchandiseTable = new DataTable();
            _daMerchandise.Fill(_dtMerchandiseTable);

            //bind to controls to data table
            ProductID.DataBindings.Add("Text", _dtMerchandiseTable, "ProductID");
            CategoryID.DataBindings.Add("Text", _dtMerchandiseTable, "CategoryID");
            ProductName.DataBindings.Add("Text", _dtMerchandiseTable, "ProductName");
            Quanity.DataBindings.Add("Text", _dtMerchandiseTable, "Quantity");
            Price.DataBindings.Add("Text", _dtMerchandiseTable, "Price");
            InStock.DataBindings.Add("Text", _dtMerchandiseTable, "inStock");

            ////dispose of connection objects
            //_sqlResultsCommand.Dispose();
            //_daMerchandise.Dispose();
            //_dtMerchandiseTable.Dispose();
        }
        public static void UpdateMerchOnClose()
        {
            try
            {
                //Save the updated table
                SqlCommandBuilder merchAdapterCommands = new SqlCommandBuilder(_daMerchandise);
                _daMerchandise.Update(_dtMerchandiseTable);
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Merchandise Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Merchandise Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void DisposeMerchObjects()
        {
            //dispose of connection objects
            _sqlResultsCommand.Dispose();
            _daMerchandise.Dispose();
            _dtMerchandiseTable.Dispose();
        }
        public static void FetchEmployees(TextBox EmployeeID, TextBox Username, TextBox Password,
                                       TextBox FirstName, TextBox LastName, TextBox HireDate, TextBox IsAdmin)
        {
            //string to build query
            string query = "select * From group3sp212330.Employees ORDER BY FirstName";

            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daEmployees = new SqlDataAdapter();
            _daEmployees.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtEmployeesTable = new DataTable();
            _daEmployees.Fill(_dtEmployeesTable);


            //bind controls to the data table
            EmployeeID.DataBindings.Add("Text", _dtEmployeesTable, "EmployeeID");
            Username.DataBindings.Add("Text", _dtEmployeesTable, "Username");
            Password.DataBindings.Add("Text", _dtEmployeesTable, "Password");
            FirstName.DataBindings.Add("Text", _dtEmployeesTable, "FirstName");
            LastName.DataBindings.Add("Text", _dtEmployeesTable, "LastName");
            HireDate.DataBindings.Add("Text", _dtEmployeesTable, "HireDate");
            IsAdmin.DataBindings.Add("Text", _dtEmployeesTable, "IsAdmin");
        }
        public static void UpdateEmployeeOnClose()
        {
            try
            {
                //Save the updated table
                SqlCommandBuilder employeeAdapterCommands = new SqlCommandBuilder(_daEmployees);
                _daEmployees.Update(_dtEmployeesTable);
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void DisposeEmployeeObjects()
        {
            //dispose of connection objects
            _sqlResultsCommand.Dispose();
            _daEmployees.Dispose();
            _dtEmployeesTable.Dispose();
        }
        public static void FetchOrders(TextBox OrderID, TextBox CustomerID, TextBox OrderDate,
                                       TextBox TotalPrice, TextBox EmployeeID, bool fromAdmin)
        {
            string query;

            //Filter for outstanding orders if from an employee, show all for admin
            if (fromAdmin)
            {
                query = "SELECT * FROM group3sp212330.Orders ORDER BY OrderID";
            }
            else
            {
                query = "SELECT * FROM group3sp212330.Orders WHERE EmployeeID IS NULL ORDER BY OrderID";
            }

            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daOrders = new SqlDataAdapter();
            _daOrders.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtOrdersTable = new DataTable();
            _daOrders.Fill(_dtOrdersTable);

            //bind data to controls
            OrderID.DataBindings.Add("Text", _dtOrdersTable, "OrderID");
            CustomerID.DataBindings.Add("Text", _dtOrdersTable, "CustomerID");
            OrderDate.DataBindings.Add("Text", _dtOrdersTable, "OrderDate");
            TotalPrice.DataBindings.Add("Text", _dtOrdersTable, "TotalPrice");
            EmployeeID.DataBindings.Add("Text", _dtOrdersTable, "EmployeeID");
        }
        public static void DisplayOrderInfo(TextBox tbxOrderID, DataGridView dgvOrders, bool onLoad)
        {
            string query;
            if (onLoad)
                query = "Select * From group3sp212330.OrderInfo Order by OrderID";
            else
                query = "SELECT * FROM group3sp212330.OrderInfo WHERE OrderID = " + tbxOrderID.Text + " ORDER BY OrderID";

            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daOrderInfo = new SqlDataAdapter();
            _daOrderInfo.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtOrderInfoTable = new DataTable();
            _daOrderInfo.Fill(_dtOrderInfoTable);
            dgvOrders.DataSource = _dtOrderInfoTable;
        }
        public static void UpdateOrderOnClose()
        {
            try
            {
                //Save the updated table
                SqlCommandBuilder orderAdapterCommands = new SqlCommandBuilder(_daOrders);
                _daOrders.Update(_dtOrdersTable);
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void DisposeAllOrderObjects()
        {
            //dispose of connection objects
            _sqlResultsCommand.Dispose();
            _daOrders.Dispose();
            _dtOrdersTable.Dispose();
            _daOrderInfo.Dispose();
            _dtOrderInfoTable.Dispose();
        }
        public static string[] RecoverLoginQuery(TextBox firstName, TextBox email)
        {
            try
            {
                //Set up the first query for username
                string query = "SELECT UserName FROM group3sp212330.Customers " +
                               "WHERE FirstName = '" + firstName.Text + "' AND Email = '" + email.Text + "';";

                //establish the command object 
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

                //Use the query to grab the username
                _recoveredLogin[0] = (string)_sqlResultsCommand.ExecuteScalar();

                //Dispose the command object before reuse
                _sqlResultsCommand.Dispose();

                //Alter the query to grab password
                query = "SELECT Password FROM group3sp212330.Customers " +
                        "WHERE FirstName = '" + firstName.Text + "' AND Email = '" + email.Text + "';";

                //Re-establish the command object
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

                //Use the query to grab the password
                _recoveredLogin[1] = (string)_sqlResultsCommand.ExecuteScalar();

                //Dispose the command object
                _sqlResultsCommand.Dispose();
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Set the first element to error for the form
                    _recoveredLogin[0] = "error";
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Set the first element to error for the form
                    _recoveredLogin[0] = "error";
                }
            }
            return _recoveredLogin;
        }
        public static void CreateNewOrder(TextBox tbxOrderID, DataGridView dgvOrders, bool Search = true)
        {
            string query;
            if (Search == true)
            {
                query = "Select ProductID , ProductName , Price From group3sp212330.Products Where Quantity > 0 Order by CategoryID";
            }
            else
            {
                query = "Select ProductID , ProductName , Price From group3sp212330.Products Where Quantity > 0 and ProductID = " + tbxOrderID.Text;
            }


            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daOrderInfo = new SqlDataAdapter();
            _daOrderInfo.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtOrderInfoTable = new DataTable();
            _daOrderInfo.Fill(_dtOrderInfoTable);
            dgvOrders.DataSource = _dtOrderInfoTable;

        }

        public static void DisplayMerchadise(TextBox tbxProductID, ComboBox cmbCategory, DataGridView dgvOrders, int search = 0)
        {
            string query;
            switch (search)
            {

                //Only ProductID
                case 1:
                    query = "Select * From group3sp212330.Products p inner join group3sp212330.Categories c " +
                        "on p.CategoryID = c.CategoryID WHERE ProductID = " + tbxProductID.Text;
                    break;
                //Both ProductID and Category
                case 2:
                    query = "Select * From group3sp212330.Products p inner join group3sp212330.Categories c " +
                        "on p.CategoryID = c.CategoryID WHERE ProductID = " + tbxProductID.Text + "and CategoryName = '" + cmbCategory.Text + "'";
                    break;
                case 3:
                    query = "Select * From group3sp212330.Products p inner join group3sp212330.Categories c " +
                        "on p.CategoryID = c.CategoryID WHERE CategoryName = '" + cmbCategory.Text + "'";
                    break;
                default:
                    query = "Select * From group3sp212330.Products p inner join group3sp212330.Categories c on p.CategoryID = c.CategoryID";
                    break;

            };


            //est cmd obj
            _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

            //est data adapter
            _daOrderInfo = new SqlDataAdapter();
            _daOrderInfo.SelectCommand = _sqlResultsCommand;

            //fill data table
            _dtOrderInfoTable = new DataTable();
            _daOrderInfo.Fill(_dtOrderInfoTable);
            dgvOrders.DataSource = _dtOrderInfoTable;
        }

        public static void SetUserInfo(TextBox username, TextBox password, bool isCustomer)
        {
            try
            {
                string query;
                if (isCustomer)
                {
                    //Set up the first query for Customer ID
                    query = "SELECT CustomerID FROM group3sp212330.Customers " +
                                   "WHERE UserName = '" + username.Text + "' AND Password = '" + password.Text + "';";
                }
                else
                {
                    //Set up the first query for Employee ID
                    query = "SELECT EmployeeID FROM group3sp212330.Employees " +
                                   "WHERE Username = '" + username.Text + "' AND Password = '" + password.Text + "';";
                }

                //establish the command object 
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

                //Use the query to grab the User's ID
                userID = (int)_sqlResultsCommand.ExecuteScalar();

                //Dispose the command object before reuse
                _sqlResultsCommand.Dispose();

                if (isCustomer)
                {
                    //Set up the first query for Customer First Name
                    query = "SELECT FirstName FROM group3sp212330.Customers " +
                                    "WHERE UserName = '" + username.Text + "' AND Password = '" + password.Text + "';";
                }
                else
                {
                    //Set up the first query for Employee ID
                    query = "SELECT FirstName FROM group3sp212330.Employees " +
                                    "WHERE Username = '" + username.Text + "' AND Password = '" + password.Text + "';";
                }
                //Re-establish the command object
                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);

                //Use the query to grab the password
                userFirstName = (string)_sqlResultsCommand.ExecuteScalar();

                //Dispose the command object
                _sqlResultsCommand.Dispose();
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Set the first element to error for the form
                    _recoveredLogin[0] = "error";
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Set the first element to error for the form
                    _recoveredLogin[0] = "error";
                }
            }
        }

        public static void CreateOrder(double totalPrice)
        {
            try
            {
                string query = "INSERT INTO group3sp212330.Orders (CustomerID, OrderDate, TotalPrice) " +
                               "VALUES (" + userID +  ", GETDATE(), " + totalPrice + ");";

                _sqlResultsCommand = new SqlCommand(query, _cntDatabase);
                _sqlResultsCommand.ExecuteNonQuery();

                //Dispose of the cmd obj
                _sqlResultsCommand.Dispose();
            }
            catch (SqlException ex)
            {
                //Check if an SqlException was caught
                if (ex is SqlException)
                {
                    //Set the error message and display it
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber " + ex.Errors[i].LineNumber + "\n" +
                            "Source " + ex.Errors[i].Source + "\n" +
                            "Procedure " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Else, handle a generic exception
                    MessageBox.Show(ex.Message, "Error on Employee Update On Close",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}