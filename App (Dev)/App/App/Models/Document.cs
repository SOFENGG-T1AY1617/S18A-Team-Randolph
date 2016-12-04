using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace App.Models
{
    public class Document
    {
            public int docuID { get; set; }
            public string docuName { get; set; }
            public float regularPrice { get; set; }
            public float expressPrice { get; set; }
            public string type { get; set; }
    }

    class documentManager
    {
            private DatabaseConnector db = new DatabaseConnector();

            public void saveDocument(Document temp)
            {
                    Document doc = temp;
                    MySqlConnection conn = new MySqlConnection(db.getConnString());

                    MySqlDataAdapter adapter = new MySqlDataAdapter();

                    using(conn)
                    {
                            using(adapter)
                            {
                                    adapter.SelectCommand = new MySqlCommand("SELECT * FROM requestdocdb.document", conn);

                                    adapter.InsertCommand = new MySqlCommand("insert into requestdocdb.document"
                                                             + " (docuID, docuName, regularPrice, expressPrice, docType) "
                                                             + "VALUES (@docuID, @docuName, @regularPrice, @expressPrice, @docType)", conn);

                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("docuID", MySqlDbType.Int32, 11, "docuID"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("docuName", MySqlDbType.VarChar, 100, "docuName"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("regularPrice", MySqlDbType.Float, 4, "regularPrice"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("expressPrice", MySqlDbType.Float, 4, "expressPrice"));
                                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("docType", MySqlDbType.VarChar, 100, "docType"));

                                    using (DataSet dataSet = new DataSet())
                                    {
                                        adapter.Fill(dataSet, "document");

                                        DataRow newRow = dataSet.Tables[0].NewRow();

                                        newRow["docuID"] = doc.docuID;
                                        newRow["docuName"] = doc.docuName;
                                        newRow["regularPrice"] = doc.regularPrice;
                                        newRow["expressPrice"] = doc.expressPrice;
                                        newRow["docType"] = doc.type;

                                        dataSet.Tables[0].Rows.Add(newRow);

                                        adapter.Update(dataSet, "document");
                                    }
                            }
                    }
            }

            public List<Document> getDocument(int docuID) // this is for cart
                                                          // User docuID from Order.getOrders
                                                          // its return type is list but return only one doc
            {
                    List<Document> listDoc = new List<Document>();
                    MySqlConnection conn = null;

                    using (conn = new MySqlConnection(db.getConnString()))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT * FROM document WHERE docuID == " + docuID + ";";
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Document doc = new Models.Document();
                                    doc.docuID = reader.GetInt32(0);
                                    doc.docuName = reader.GetString(1);
                                    doc.regularPrice = reader.GetFloat(2);
                                    doc.expressPrice = reader.GetFloat(3);
                                    doc.type = reader.GetString(4);

                                    listDoc.Add(doc);
                                    doc = new Models.Document();
                                }

                                if (!reader.HasRows)
                                {
                                    listDoc = null;
                                }
                            }
                        }
                    }
                    
                    conn.Close();
                    return listDoc;
            }

            public List<Document> getAvailableDocument(string sDegree) // Bachelors, Masters, Doctorate
            {
                    List<Document> listDoc = new List<Document>();
                    MySqlConnection conn = null;

                    using (conn = new MySqlConnection(db.getConnString()))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            if(sDegree.Equals("Bachelors"))
                            {
                                //get both and undergrad
                                cmd.CommandText = "SELECT * FROM document WHERE docuType LIKE 'both' OR docuType LIKE 'Undergrad';";
                            }
                            else
                            {
                                //get grad
                                cmd.CommandText = "SELECT * FROM document WHERE docuType LIKE 'Grad';";
                            }

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Document doc = new Models.Document();
                                    doc.docuID = reader.GetInt32(0);
                                    doc.docuName = reader.GetString(1);
                                    doc.regularPrice = reader.GetFloat(2);
                                    doc.expressPrice = reader.GetFloat(3);
                                    doc.type = reader.GetString(4);

                                    listDoc.Add(doc);
                                    doc = new Models.Document();
                                }

                                if (!reader.HasRows)
                                {
                                    listDoc = null;
                                }
                            }
                        }
                    }
                    
                    conn.Close();
                    return listDoc;
            }
    }
}