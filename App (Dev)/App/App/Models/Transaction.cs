using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace App.Models
{
    public class Transaction
    {
        public int userID { get; set; }
        public float price { get; set; }
        public int transactionID { get; set; }
        public int mailingID { get; set; }
        public string deliveryProcessing { get; set; }
        //----------------------------------------------
        // dateValue.ToString("yyyy-MM-ddHH:mm:ss");
        public string estimatedDeliveryDate { get; set; }
        public string dateRequested { get; set; } 
        public string dateDue { get; set; }
        //-----------------------------------------------
        public int orderID { get; set; }
    }

    class transactionManager
    {
            private DatabaseConnector db = new DatabaseConnector();
            private DateTime dateValue;

            public void saveTransaction(Transaction temp)
            {
                    Transaction tran = temp;
                    MySqlConnection conn = new MySqlConnection(db.getConnString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter();

                    using(conn)
                    {
                            using(adapter)
                            {
                                    adapter.SelectCommand = new MySqlCommand("SELECT * FROM requestdocdb.transactions", conn);

                                    adapter.InsertCommand = new MySqlCommand("insert into requestdocdb.transactions"
                                                             + " (userID, price, transcationID, mailingID, deliveryProcessing, estimatedDeliveryDate, dateRequested, dateDue, orderID) "
                                                             + "VALUES (@userID, @price, @transcationID, @mailingID, @deliveryProcessing, @estimatedDeliveryDate, @dateRequested, @dateDue, @orderID)", conn);

                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("userID", MySqlDbType.Int32, 11, "userID"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("price", MySqlDbType.Float, 4, "price"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("transactionID", MySqlDbType.Int32, 11, "transactionID"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("mailingID", MySqlDbType.Int32, 11, "mailingID"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("deliveryProcessing", MySqlDbType.VarChar, 100, "deliveryProcessing"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("estimatedDeliveryDate", MySqlDbType.VarChar, 100, "estimatedDeliveryDate"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("dateRequested", MySqlDbType.VarChar, 100, "dateRequested"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("dateDue", MySqlDbType.VarChar, 100, "dateDue"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("orderID", MySqlDbType.Int32, 11, "orderID"));

                                    using (DataSet dataSet = new DataSet())
                                    {
                                        adapter.Fill(dataSet, "transactions");

                                        DataRow newRow = dataSet.Tables[0].NewRow();

                                        newRow["userID"] = tran.userID;
                                        newRow["price"] = tran.price;
                                        newRow["transcationID"] = tran.transactionID;
                                        newRow["mailingID"] = tran.mailingID;
                                        newRow["deliveryProcessing"] = tran.deliveryProcessing;
                                        newRow["estimatedDeliveryDate"] = tran.estimatedDeliveryDate;
                                        newRow["dateRequested"] = tran.dateRequested;
                                        newRow["dateDue"] = tran.dateDue;
                                        newRow["orderID"] = tran.orderID;

                                        dataSet.Tables[0].Rows.Add(newRow);

                                        adapter.Update(dataSet, "transactions");
                                    }
                            }
                    }
            }

        //return type is list but return one transaction
       
    }
}