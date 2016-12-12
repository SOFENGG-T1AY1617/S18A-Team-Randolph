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

    }

    public class delivery
    {
        public String location { get; set; }
        public String price { get; set; } 
    }

    public class user
    {
        public int userID { get; set; }
        public String idNumber { get; set; }
        public string name { get; set; }
        public string verified { get; set; }
    }

    public class docu
    {
        public String docuName { get; set; }
        public String regularPrice { get; set; }
        public String expressPrice { get; set; }
    }

    class adminDocumentManager
    {
        private DatabaseConnector db = new DatabaseConnector();

        public List<docu> getDocuList()
        {
            List<docu> docuList = new List<docu>();
            docu docu = new docu();

            MySqlConnection conn = null;
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select docuName, CONCAT('Php ', regularPrice) as 'Reg', CONCAT('Php ', expressPrice) as 'Exp' from document; ";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            docu = new docu();

                            docu.docuName = reader.GetString(0);
                            docu.regularPrice = reader.GetString(1);

                            if(reader.IsDBNull(2))
                            {
                                docu.expressPrice = "Not Available";
                            }
                            else
                            {
                                docu.expressPrice = reader.GetString(2);

                            }



                            docuList.Add(docu);
                        }

                        if (!reader.HasRows)
                        {
                            docu = null;
                        }
                    }
                }
            }

            return docuList;
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

    class adminVerifyManager{

        private DatabaseConnector db = new DatabaseConnector();
        

        public List<user> getUserList()
        {
            List<user> userList = new List<user>();
            user user = new user();

            MySqlConnection conn = null;
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT requestdocdb.user.idNumber, CONCAT(CONCAT(requestdocdb.user.lastName, ', '), requestdocdb.user.firstName) as 'Name', requestdocdb.user.verified FROM requestdocdb.user;";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new user();

                            //user.userID = reader.GetInt32(0);
                            user.idNumber = reader.GetString(0);
                            user.name = reader.GetString(1);
                            user.verified = reader.GetString(2);

                            userList.Add(user);
                        }

                        if (!reader.HasRows)
                        {
                            user = null;
                        }
                    }
                }
            }

            return userList;
        }
    }
}