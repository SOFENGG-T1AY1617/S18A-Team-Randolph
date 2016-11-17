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
        AccountManager accManager = new AccountManager();
        DegreeManager degManager = new DegreeManager();

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
        public ActionResult verify(string email, string password)
        {
            //Console.WriteLine(email, password);
            ViewBag.String = "email: " + email + " password: " + password;

            //ViewData["string"] = "email " + email + " password " + password;

            var acc = accManager.getAccount(email, password);

            // Means acc does not exist 
            if (acc == null)
            {
                return RedirectToAction("error", "Account");
            }
            else
            {
                //ViewBag.exist = true;
                //ViewBag.String = "Account exists!";
                // ViewBag.acc = acc;
                return RedirectToAction("order", "Transaction");

            }

            //RedirectToRoute("Transaction/Step1");
        }

        [HttpPost]
        public ActionResult save(int userID, string lastName, string firstName, string middleName, char gender, int birthYear,
                                     int birthMonth, int birthDay, string citizenship, string placeOfBirth, string currentAddress,
                                     string phoneNo, string alternatePhoneNo, string email, string alternateEmail, string password,
                                 int degreeID, string degreeName, string level, int yearAdmitted, string campusAttended, string admittedAs,
                                     string graduated, int yearLevel, string lastSchoolAttendedPrevDlsu)
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

            accManager.saveAccount(acc);


            Degree deg = new Degree();

            deg.degreeID = degreeID;
            deg.degreeName = degreeName;
            deg.level = level;
            deg.yearAdmitted = yearAdmitted;
            deg.campusAttended = campusAttended;
            deg.admittedAs = admittedAs;
            deg.graduated = graduated;
            deg.yearLevel = yearLevel;
            deg.userID = userID;
            deg.lastSchoolAttendedPrevDlsu = lastSchoolAttendedPrevDlsu;

            degManager.saveDegree(deg);

            return RedirectToAction("Index", "Home");
        }
    }
}