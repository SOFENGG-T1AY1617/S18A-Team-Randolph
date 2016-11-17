using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    class Account {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public char gender { get; set; }
        public Date birthday { get; set; }
        public string placeOfBirth { get; set; }
        public string citizenship { get; set; }
        public string currentAddress { get; set; }
        public string phoneNo { get; set; }
        public string alternatePhoneNo { get; set; }
        public string email { get; set; }
        public string alternateEmail { get; set; }
        public string password { get; set; }
        public string level { get; set; }
        public int userID { get; set; }
        public string degreeName { get; set; }
        public bool graduated { get; set; }
        // wtf is graduate -> YES || NO -> txtbox
        public int yearLevel { get; set; }
        public string admittedAs { get; set; }
        public string LastSchoolAttendedPrevDLSU { get; set; }

        public Account(string lastName, string firstName, string middleName, char gender, Date birthday, string placeOfBirth, 
                        string citizenship, string currentAddress, string phoneNo, string alternatePhoneNo, string email, 
                        string alternateEmail, string password, string level, int userID, string degreeName, bool graduated, 
                        int yearLevel, string admittedAs, string LastSchoolAttendedPrevDLSU)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.gender = gender;
            this.birthday = birthday;
            this.placeOfBirth = placeOfBirth;
            this.citizenship = citizenship;
            this.currentAddress = currentAddress;
            this.phoneNo = phoneNo;
            this.alternatePhoneNo = alternatePhoneNo;
            this.email = email;
            this.alternateEmail = alternateEmail;
            this.password = password;
            this.level = level;
            this.userID = userID;
            this.degreeName = degreeName;
            this.graduated = graduated;
            // wtf is graduated -> YES || NO -> txtbox
            this.yearLevel = yearLevel;
            this.admittedAs = admittedAs;
            this.LastSchoolAttendedPrevDLSU = LastSchoolAttendedPrevDLSU;
        }

        public string getPersonalString()
        {
            return lastName + ", " + firstName + ", " + middleName + ", " + gender + ", " + birthday.toString() + ", " + citizenship + ", " + placeOfBirth + ", " + currentAddress + ", " + phoneNo + ", " + alternatePhoneNo + ", " + email + ", " + alternateEmail + ", " + password;
        }

        public string getAcademicString()
        {
            return degreeName + ", " + level + ", " + admittedAs + ", " + graduated + ", " + yearLevel + ", " + userID + ", " + LastSchoolAttendedPrevDLSU;
        }
    }

    class AccountModel
    {
        private DatabaseConnector connection = new DatabaseConnector();

        class queryBuilder_Account
        {
            private string columns { get; set; }
            private string tables { get; set; }
            private string conditions { get; set; }

            public queryBuilder_Account(string c, string t, string con)
            {
                columns = c;
                tables = t;
                conditions = con;
            }

            public string toSelectQuery_login()
            {
                Console.WriteLine("SELECT " + columns + " FROM " + tables + " WHERE " + conditions);
                return "SELECT " + columns + " FROM " + tables + " WHERE " + conditions;
            }

            public string toSelectQuery_register()
            {
                //"INSERT INTO tables (columns) VALUES(conditions)";
                Console.WriteLine("INSERT INTO " + tables + " (" + columns + ") VALUES (" + conditions + ")");
                return "INSERT INTO " + tables + " (" + columns + ") VALUES (" + conditions + ")";
            }
        }

        public Account Login(String userID, String password)
        {
            //HOW TO LOGIN !
            //get the input (ID and Password)
            //Check if userID exist and password is equal if the id exist

            string columns = "userID, password";
            string tables = "user";
            string conditions = "userID LIKE " + "'" + userID + "'" + " AND password LIKE " + "'" + password + "'";
            List<string> result;

            queryBuilder_Account LoginQuery = new queryBuilder_Account(columns, tables, conditions);

            result = connection.ExecuteQuery_yesReturn(LoginQuery.toSelectQuery_login());

            if (result != null)
            {
                return new Models.Account();
            }
            else
            {
                //login failed
                return false;
            }
        }


        public bool Register(Account account, string retry_password, int userID, int degreeID)
        {
            if (retry_password.CompareTo(account.password) == 0 && !IsNullOrEmpty(retry_password) && !IsNullOrEmpty(account.getPersonalString()))
            {
                //"INSERT INTO tables (columns) VALUES(conditions)";
                //user table
                string columns = "lastName, firstName, middleName, gender, birthYear, birthMonth, birthDay, citizenship, placeOfBirth, currentAddress, phoneNo, alternatePhoneNo, email, alternateEmail, password";
                string tables = "user";
                string conditions = account.getPersonalString();

                queryBuilder_Account RegQuery_Personal = new queryBuilder_Account(columns, tables, conditions);
                if (!connection.ExecuteQuery_noReturn(RegQuery_Personal.toSelectQuery_register()))
                {
                    Console.WriteLine("Error: Failed to insert to user table");
                    return false;
                }

                //degreeofuser table
                columns = "degreeName, level, admittedAs, graduated, yearLevel, userID , LastSchoolAttendedPrevDLSU";
                tables = "degreeofuser";
                conditions = account.getAcademicString();
                queryBuilder_Account RegQuery_Academic = new queryBuilder_Account(columns, tables, conditions);
                if (!connection.ExecuteQuery_noReturn(RegQuery_Academic.toSelectQuery_register()))
                {
                    Console.WriteLine("Error: Failed to insert to degreeofuser table");
                    return false;
                }

                return true;
            }
            else
            {
                Console.WriteLine("Error: Your retry_password does not match to password");
                return false;
            }
        }

        private bool IsNullOrEmpty(string retry_password)
        {
            throw new NotImplementedException();
        }

        /*class date
        {
            private int year { get; set; }
            private int month { get; set; }
            private int day { get; set; }

            public date(int year, int month, int day)
            {
                this.year = year;
                this.month = month;
                this.day = day;
            }

            public string toString()
            {
                return year + ", " + month + ", " + day;
            }
        }

        class personal_info
        {
            private string lastName { get; set; }
            private string firstName { get; set; }
            private string middleName { get; set; }
            private char gender { get; set; }
            private date birthday { get; set; }
            private string placeOfBirth { get; set; }
            private string citizenship { get; set; }
            private string currentAddress { get; set; }
            private string phoneNo { get; set; }
            private string alternatePhoneNo { get; set; }
            private string email { get; set; }
            private string alternateEmail { get; set; }
            private string password { get; set; }

            public personal_info(string lastName, string firstName, string middleName, char gender, date birthday, string placeOfBirth, string citizenship, string currentAddress, string phoneNo, string alternatePhoneNo, string email, string alternateEmail, string password)
            {
                this.lastName = lastName;
                this.firstName = firstName;
                this.middleName = middleName;
                this.gender = gender;
                this.birthday = birthday;
                this.placeOfBirth = placeOfBirth;
                this.citizenship = citizenship;
                this.currentAddress = currentAddress;
                this.phoneNo = phoneNo;
                this.alternatePhoneNo = alternatePhoneNo;
                this.email = email;
                this.alternateEmail = alternateEmail;
                this.password = password;
            }

            public string toString()
            {
                return lastName + ", " + firstName + ", " + middleName + ", " + gender + ", " + birthday.toString() + ", " + citizenship + ", " + placeOfBirth + ", " + currentAddress + ", " + phoneNo + ", " + alternatePhoneNo + ", " + email + ", " + alternateEmail + ", " + password;
            }
        }

        class academic_info
        {
            private string level { get; set; }
            private int userID { get; set; }
            private string degreeName { get; set; }
            private bool graduated { get; set; }
            // wtf is graduate -> YES || NO -> txtbox
            private int yearLevel { get; set; }
            private string admittedAs { get; set; }
            private string LastSchoolAttendedPrevDLSU { get; set; }

            public academic_info(string level, int userID, string degreeName, bool graduated, int yearLevel, string admittedAs, string LastSchoolAttendedPrevDLSU)
            {
                this.level = level;
                this.userID = userID;
                this.degreeName = degreeName;
                this.graduated = graduated;
                // wtf is graduated -> YES || NO -> txtbox
                this.yearLevel = yearLevel;
                this.admittedAs = admittedAs;
                this.LastSchoolAttendedPrevDLSU = LastSchoolAttendedPrevDLSU;
            }

            public string toString()
            {
                return degreeName + ", " + level + ", " + admittedAs + ", " + graduated + ", " + yearLevel + ", " + userID + ", " + LastSchoolAttendedPrevDLSU;
            }
        }*/


    }
}