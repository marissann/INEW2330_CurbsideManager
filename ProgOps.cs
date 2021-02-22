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
                _sqlResultsCommand.Dispose();
                _daEmployees.Dispose();
                _dtEmployeesTable.Dispose();
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
                {
                    MessageBox.Show("Employee " + username.Text + " has successfully logged in.");
                    userFound = true;
                }

                //dispose of connection objects
                _sqlResultsCommand.Dispose();
                _daEmployees.Dispose();
                _dtEmployeesTable.Dispose();

                //If an employee is found, check if that employee is an admin
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

                        //Return an 'A'
                        return 'A';
                    }
                    else
                    {
                        MessageBox.Show("Employee " + username.Text + " has successfully logged in.");

                        //dispose of connection objects
                        _sqlResultsCommand.Dispose();
                        _daEmployees.Dispose();
                        _dtEmployeesTable.Dispose();

                        //Return an 'E'
                        return 'E';
                    }                    
                }
                else
                {
                    //Start query to check if credentials match a customer
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
                    {
                        MessageBox.Show("Customer " + username.Text + " has successfully logged in.");
                        userFound = true;
                    }

                    //dispose of connection objects
                    _sqlResultsCommand.Dispose();
                    _daEmployees.Dispose();
                    _dtEmployeesTable.Dispose();
                }

                if (userFound)
                {
                    //return a 'C' if a customer was found
                    return 'C';
                }
                else
                {
                    //return an 'F' if no user was found
                    return 'F';
                }
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
                    return 'Z';
                }
                else
                {//handles generic ones here
                    MessageBox.Show(ex.Message, "Error on DatabaseCommand", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 'Z';
                }
            }
        }
    }
}
