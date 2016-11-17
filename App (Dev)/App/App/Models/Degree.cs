using App.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Degree
    {
        public int degreeID { get; set; }
        public string degreeName { get; set; }
        public string level { get; set; }
        public int yearAdmitted { get; set; }
        public string campusAttended { get; set; }
        public string admittedAs { get; set; }
        public string graduated { get; set; }
        public int yearLevel { get; set; }
        public int userID { get; set; }
        public string lastSchoolAttendedPrevDlsu { get; set; }

    }

    class degreeManager
    {
        public void saveDegree(Degree deg)
        {
            Degree degree = deg;
            MySqlConnection conn = new MySqlConnection(db.getConnString());

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            using (conn)
            {
                using (adapter)
                {
                    adapter.SelectCommand = new MySqlCommand("SELECT * FROM requestdocdb.degreesofuser", conn);

                    adapter.InsertCommand = new MySqlCommand("insert into requestdocdb.degreesofuser"
                                                             + " (degreeID, degreeName, level, yearAdmitted, campusAttended, admittedAs, graduated,"
                                                             + " yearLevel, userID, lastSchoolAttendedPrevDlsu) "
                                                             + "VALUES (@degreeID, @degreeName, @level, @yearAdmitted, @campusAttended, @admittedAs, @graduated, "
                                                             + "@yearLevel, @userID, @lastSchoolAttendedPrevDlsu)", conn);


                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("degreeID", MySqlDbType.Int32, 11, "degreeID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("degreeName", MySqlDbType.VarChar, 50, "degreeName"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("level", MySqlDbType.VarChar, 50, "level"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("yearAdmitted", MySqlDbType.Int32, 11, "yearAdmitted"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("campusAttended", MySqlDbType.VarChar, 200, "campusAttended"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("admittedAs", MySqlDbType.VarChar, 50, "admittedAs"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("graduated", MySqlDbType.VarChar, 3, "graduated"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("yearLevel", MySqlDbType.Int32, 11, "yearLevel"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("userID", MySqlDbType.Int32, 11, "userID"));
                    adapter.InsertCommand.Parameters.Add(new MySqlParameter("lastSchoolAttendedPrevDlsu", MySqlDbType.VarChar, 100, "lastSchoolAttendedPrevDlsu"));

                    using (DataSet dataSet = new DataSet())
                    {
                        adapter.Fill(dataSet, "degreesofuser");

                        DataRow newRow = dataSet.Tables[0].NewRow();

                        newRow["degreeID"] = deg.degreeID;
                        newRow["degreeName"] = deg.degreeName;
                        newRow["level"] = deg.level;
                        newRow["yearAdmitted"] = deg.yearAdmitted;
                        newRow["campusAttended"] = deg.campusAttended;
                        newRow["admittedAs"] = deg.admittedAs;
                        newRow["graduated"] = deg.graduated;
                        newRow["yearLevel"] = deg.yearLevel;
                        newRow["userID"] = deg.userID;
                        newRow["lastSchoolAttendedPrevDlsu"] = deg.lastSchoolAttendedPrevDlsu;

                        dataSet.Tables[0].Rows.Add(newRow);

                        adapter.Update(dataSet, "degreesofuser");
                    }
                }
            }
            conn.Close();
        }

        
    }

}