using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Account
    {
        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public char gender { get; set; }
        public int birthYear { get; set; }
        public int birthDay { get; set; }
        public int birthMonth { get; set; }
        public string citizenship { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string placeOfBirth { get; set; }
        public string currentAddress { get; set; }
        public string phoneNo { get; set; }
        public string alternatePhoneNo { get; set; }
        public string alternateEmail { get; set; }
    }
}