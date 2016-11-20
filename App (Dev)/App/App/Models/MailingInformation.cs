using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class MailingInformation
    {
        public int mailingID { get; set; }
        public string zipcode { get; set; }
        public string streetname { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int locationID { get; set; }
        public string contactNo { get; set; }
        public int userID { get; set; }
        public string addressline { get; set; }
    }

    public class MailingInfoModel
    {
        private DatabaseConnector db = new DatabaseConnector();

        public void addMailingInfo(MailingInformation mailInfo)
        {
            MySqlConnection conn = new MySqlConnection(db.getConnString());

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            using (conn)
            {
                using (adapter)
                {
                    adapter.InsertCommand = new MySqlCommand("insert into requestdocdb.mailingaddress"
                                                             + " (mailingID, zipcode, streetName, city, country, locationID, contactNo, userID, addressline) "
                                                             + "VALUES (@mailingID, @zipcode, @streetName, @city, @country, @locationID, @contactNo, @userID, @addressline)", conn);


                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("mailingID", MySqlDbType.Int32, 11, "mailingID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("zipcode", MySqlDbType.VarChar, 50, "zipcode"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("streetName", MySqlDbType.VarChar, 50, "streetName"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("city", MySqlDbType.VarChar, 50, "city"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("country", MySqlDbType.VarChar, 100, "country"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("locationID", MySqlDbType.VarChar, 50, "locationID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("contactNo", MySqlDbType.VarChar, 20, "contactNo"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("userID", MySqlDbType.Int32, 11, "userID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("addressline", MySqlDbType.VarChar, 200, "addressline"));

                    using (DataSet dataSet = new DataSet())
                    {
                        adapter.Fill(dataSet, "mailingaddress");

                        DataRow newRow = dataSet.Tables[0].NewRow();

                        newRow["mailingID"] = mailInfo.mailingID;
                        newRow["zipcode"] = mailInfo.zipcode;
                        newRow["streetName"] = mailInfo.streetname;
                        newRow["city"] = mailInfo.city;
                        newRow["country"] = mailInfo.country;
                        newRow["locationID"] = mailInfo.locationID;
                        newRow["contactNo"] = mailInfo.contactNo;
                        newRow["userID"] = mailInfo.userID;
                        newRow["addressline"] = mailInfo.addressline;

                        dataSet.Tables[0].Rows.Add(newRow);

                        adapter.Update(dataSet, "mailingaddress");
                    }
                }
            }
            conn.Close();
        }

        public List<MailingInformation> getMailInfos(int userID)
        {
            List<MailingInformation> listmailinfo = new List<MailingInformation>();
            MySqlConnection conn = null;

            using (conn = new MySqlConnection(db.getConnString()))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM mailingaddress WHERE userID LIKE '" + userID + "';";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MailingInformation mi = new Models.MailingInformation();
                            mi.userID = reader.GetInt32(0);
                            mi.zipcode = reader.GetString(1);
                            mi.streetname = reader.GetString(2);
                            mi.city = reader.GetString(3);
                            mi.country = reader.GetString(4);
                            mi.locationID = reader.GetInt32(5);
                            mi.contactNo = reader.GetString(6);
                            mi.userID = reader.GetInt32(7);
                            mi.addressline = reader.GetString(8);

                            listmailinfo.Add(mi);
                        }

                        if (!reader.HasRows)
                        {
                            listmailinfo = null;
                        }
                    }
                }
            }

            conn.Close();
            return listmailinfo;
        }
    }
}