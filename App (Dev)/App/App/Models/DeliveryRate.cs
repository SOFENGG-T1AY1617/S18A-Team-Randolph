using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class DeliveryRate
    {
        public int locationId { get; set; }
        public float price { get; set; }
        public int delay { get; set; }
        public string location { get; set; }
    }

    class DeliveryRateManager
    {
        private DatabaseConnector db = new DatabaseConnector();

        public DeliveryRate getDeliveryRate(string location)
        {
            DeliveryRate dr = new DeliveryRate();
            MySqlConnection conn = null;

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM deliveryrates WHERE location LIKE '" + location + "';";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dr.locationId = reader.GetInt32(0);
                            dr.price = reader.GetFloat(1);
                            dr.delay = reader.GetInt32(2);
                            dr.location = reader.GetString(3);
                        }

                        if (!reader.HasRows)
                        {
                            dr = null;
                        }
                    }
                }
            }

            conn.Close();
            return dr;
        }
    }
}