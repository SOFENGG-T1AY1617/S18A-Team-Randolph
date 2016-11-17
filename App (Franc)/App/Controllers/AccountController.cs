using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        AccountManager manager = new AccountManager();

        public ActionResult Index()
        {
            // working
            // must go to home index
            return RedirectToAction("Index", "Home");
        }

        public ActionResult login()
        {
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        public ActionResult regerror()
        {
            string message = "";
            // put code for validation (like, wrong email address, etc)


            ViewBag.message = message;
            return View();
        }

        public ActionResult error()
        {
            return View();
        }

        [HttpPost]
        public void verify(string email, string password)
        {
            //Console.WriteLine(email, password);
            ViewBag.String = "email: " + email + " password: " + password;

            //ViewData["string"] = "email " + email + " password " + password;

            var acc = manager.getAccount(email, password);

            // Means acc does not exist 
            if(acc == null)
            {
                RedirectToAction("error", "Account");
            }
            else {
                //ViewBag.exist = true;
                //ViewBag.String = "Account exists!";
                // ViewBag.acc = acc;
                RedirectToAction("Index", "Home");

            }
           
            //RedirectToRoute("Transaction/Step1");
        }

        [HttpPost]
        public ActionResult save(int userID, string lastName, string firstName, string middleName, char gender, int birthYear, 
                                     int birthMonth, int birthDay, string citizenship, string placeOfBirth, string currentAddress,
                                     string phoneNo, string alternatePhoneNo, string email, string alternateEmail, string password)
        {
            Account acc = new Account();

            acc.userID = userID;
            acc.lastName = lastName;
            acc.firstName = firstName;
            acc.middleName = middleName;
            acc.gender = gender;
            acc.birthYear = birthYear;
            acc.birthMonth = birthMonth;
            acc.birthDay = birthDay;
            acc.citizenship = citizenship;
            acc.email = email;
            acc.alternateEmail = alternateEmail;
            acc.password = password;
            acc.placeOfBirth = placeOfBirth;
            acc.currentAddress = currentAddress;
            acc.phoneNo = phoneNo;
            acc.alternatePhoneNo = alternatePhoneNo;

            manager.saveAccount(acc);

            return RedirectToAction("Index", "Home");
        }
    }
}