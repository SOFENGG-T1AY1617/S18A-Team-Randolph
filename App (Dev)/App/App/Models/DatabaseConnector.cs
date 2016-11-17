using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace App.Models
{
    public class DatabaseConnector
    {
        private MySqlConnection connection { get; set; }
        private string server { get; set; }
        private string database { get; set; }
        private string uid { get; set; }
        private string password { get; set; }

        //Constructor
        public DatabaseConnector()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "";        //Server, EX) localhost
            database = "";      //DB name
            uid = "";           //Username
            password = "";      //password

            //Concat Connection Information
            string connectionString;
            //connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            //Ready for MYSQL connection
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0: //0: Cannot connect to server.
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045: //1045: Invalid user name and/or password.
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Execute Query
        public bool ExecuteQuery_noReturn(String tempQuery)
        //no return => UPDATE, INSERT, DELETE...
        {
            string query = tempQuery;

            //open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                return true;
            }

            return false;
        }

        public List<string> ExecuteQuery_yesReturn(String tempQuery)
        //yes return => SEARCH, GET ...
        {
            string query = tempQuery;
            List<string> queryResult = new List<string>();

            //open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    queryResult.Add(reader.GetString(0));
                }
                reader.Close();

                //close connection
                this.CloseConnection();
            }

            return queryResult;
        }
    }
}