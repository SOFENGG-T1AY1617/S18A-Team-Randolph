using App.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Admin
    {
        public int adminID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string idNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public char gender { get; set; }
        public int birthYear { get; set; }
        public int birthDay { get; set; }
        public int birthMonth { get; set; }
    }

    public class monitor
    {
        public int transactionID { get; set; }
        public string transactionDate { get; set; }
        public string docuName { get; set; }
        public string status { get; set; }
    }

    public class delivery
    {
        public String location { get; set; }
        public String price { get; set; } 
    }

    class adminManager
    {
        private DatabaseConnector db = new DatabaseConnector();

        public Admin saveAdmin(Admin acc)
        {
            Admin admin = acc;
            MySqlConnection conn = new MySqlConnection(db.getConnString());

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            using (conn)
            {
                using (adapter)
                {
                    adapter.SelectCommand = new MySqlCommand("SELECT * FROM requestdocdb.admin", conn);

                    adapter.InsertCommand = new MySqlCommand("INSERT INTO requestdocdb.admin"
                                                             + " (adminID, email, password, lastName, firstName, middleName, gender,"
                                                             + " birthYear, birthMonth, birthDay) "
                                                             + "VALUES (@adminID, @email, @password, @lastName, @firstName, @middleName, @gender,"
                                                             + " @birthYear, @birthMonth, @birthDay)", conn);

                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("adminID", MySqlDbType.Int32, 11, "adminID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("email", MySqlDbType.VarChar, 100, "email"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("password", MySqlDbType.VarChar, 100, "password"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("lastName", MySqlDbType.VarChar, 100, "lastName"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("firstName", MySqlDbType.VarChar, 100, "firstName"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("middleName", MySqlDbType.VarChar, 100, "middleName"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("gender", MySqlDbType.VarChar, 1, "gender"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("birthYear", MySqlDbType.Int32, 11, "birthYear"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("birthMonth", MySqlDbType.Int32, 11, "birthMonth"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("birthDay", MySqlDbType.Int32, 11, "birthDay"));


                    using (DataSet dataSet = new DataSet())
                    {
                        adapter.Fill(dataSet, "admin");

                        DataRow newRow = dataSet.Tables[0].NewRow();

                        newRow["adminID"] = acc.adminID;
                        newRow["email"] = acc.email;
                        newRow["password"] = acc.password;
                        newRow["lastName"] = acc.lastName;
                        newRow["firstName"] = acc.firstName;
                        newRow["middleName"] = acc.middleName;
                        newRow["gender"] = acc.gender;
                        newRow["birthYear"] = acc.birthYear;
                        newRow["birthMonth"] = acc.birthMonth;
                        newRow["birthDay"] = acc.birthDay;


                        dataSet.Tables[0].Rows.Add(newRow);

                        adapter.Update(dataSet, "admin");
                    }
                }
            }

            conn.Close();
            return this.getAccount(acc.email, acc.password);
        }

        public Admin getAccount(string email, string password)
        {
            Admin admin = new Models.Admin();
            MySqlConnection conn = null;

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM admin WHERE email LIKE '" + email + "' and password like '" + password + "';";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admin.adminID = reader.GetInt32(0);
                            admin.email = reader.GetString(1);
                            admin.password = reader.GetString(2);
                            admin.lastName = reader.GetString(3);
                            admin.firstName = reader.GetString(4);
                            admin.middleName = reader.GetString(5);
                            admin.gender = reader.GetChar(6);
                            admin.birthYear = reader.GetInt32(7);
                            admin.birthMonth = reader.GetInt32(8);
                            admin.birthDay = reader.GetInt32(9);
                            

                        }

                        if (!reader.HasRows)
                        {
                            admin = null;
                        }
                    }
                }
            }

            conn.Close();
            return admin;
        }
    }

    class adminMonitorManager
    {
        private DatabaseConnector db = new DatabaseConnector();

        public List<monitor> getMonitorList()
        {
            List<monitor> moniList = new List<monitor>();
            monitor moni = new monitor();

            MySqlConnection conn = null;
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select transactions.transactionID, dateRequested, docuName, status from transactions, orders, document where transactions.transactionID = orders.transactionID and orders.docuID = document.docuID order by transactions.transactionID; ";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            moni = new monitor();

                            moni.transactionID = reader.GetInt32(0);
                            moni.transactionDate = reader.GetString(1);
                            moni.docuName = reader.GetString(2);
                            moni.status = reader.GetString(3);

                            moniList.Add(moni);
                        }

                        if (!reader.HasRows)
                        {
                            moni = null;
                        }
                    }
                }
            }

            return moniList;
        }
    }

    class adminDeliveryManager{

        private DatabaseConnector db = new DatabaseConnector();

        public List<delivery> getDeliveryList()
        {
            List<delivery> delivList = new List<delivery>();
            delivery deliv = new delivery();

            MySqlConnection conn = null;
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select location as 'Delivery Area', CONCAT('Php ', price) as 'Initial Charge' from requestdocdb.deliveryrates order by 'Delivery Area'; ";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            deliv = new delivery();

                            deliv.location = reader.GetString(0);
                            deliv.price = reader.GetString(1);

                            delivList.Add(deliv);
                        }

                        if (!reader.HasRows)
                        {
                            deliv = null;
                        }
                    }
                }
            }

            return delivList;
        }

        public void editDelivRate(string location, float price)
        {
            MySqlConnection conn = new MySqlConnection(db.getConnString());
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();

            using (conn)
            {
                using (sda)
                {
                    sda.SelectCommand = new MySqlCommand("SELECT * from deliveryrates");
                    sda.UpdateCommand = new MySqlCommand("UPDATE deliveryrates SET price='" + price + "' WHERE location='" + location + "'");

                    //sda.UpdateCommand.Parameters.Add(new MySqlParameter("price", MySqlDbType.Float, "price"));

                    sda.Update(dt);
                    
                }
            }
        }

    }

    
}