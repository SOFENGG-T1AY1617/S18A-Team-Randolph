﻿using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        adminManager adminManager = new adminManager();
        adminVerifyManager verifyManager = new adminVerifyManager();
        adminDeliveryManager delivManager = new adminDeliveryManager();
        adminDocumentManager docuManager = new adminDocumentManager();
        adminMonitorManager monitorManager = new adminMonitorManager();

        // GET: Admin
        public ActionResult adminDelivery()
        {
            var delivList = delivManager.getDeliveryList();

            return View(delivList);
        }

        // GET: Admin
        public ActionResult adminDeliveryEdit()
        {
            var delivList = delivManager.getDeliveryList();

            return View(delivList);
        }

        [HttpPost]
        public ActionResult changeDelivRate(String location, float price)
        {
            delivManager.editDelivRate(location, price);

            return RedirectToAction("adminDeliveryEdit");
        }

        // GET: Admin
        public ActionResult adminDocumentCharges()
        {
            var docuList = docuManager.getDocuList();

            return View(docuList);
        }

        // GET: Admin
        public ActionResult adminIndex()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminMonitor()
        {
            var monitorList = monitorManager.getMonitorList();

            return View(monitorList);
        }

        // GET: Admin
        public ActionResult adminOrderList()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminRegister()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminSearch()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminTransactionHistory()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminUpdateDate()
        {
            return View();
        }

        // GET: Admin
        public ActionResult adminVerify()
        {           
            var userList = verifyManager.getUserList();
            //ViewBag.idNumber = userList[1].idNumber;

            return View(userList);
        }

        [HttpPost]
        public ActionResult save(string email, string password,
            string lastName, string firstName, string middleName, string gender, string birthday)
        {
            /*
             Note: phonenumber for adminount is also phone number in mailing
             gradRadio = yes or no
             yrLvl = 1 to 8
             lastTerm = 1, 2,3
             */

            int errorCtr = 0;

            Admin admin = new Admin();

            if (!(email == ""))
            {
                admin.email = email;
            }
            else
            {
                errorCtr++;
                ViewBag.emailError = "Please enter your email address";
            }

            if (!(password == ""))
            {
                admin.password = password;
            }
            else
            {
                errorCtr++;
                ViewBag.passwordError = "Please enter a password";
            }

            if (!(lastName == ""))
            {
                admin.lastName = lastName;
            }
            else
            {
                errorCtr++;
                ViewBag.lastNameError = "Please enter your last name";
            }

            if (!(firstName == ""))
            {
                admin.firstName = firstName;
            }
            else
            {
                errorCtr++;
                ViewBag.firstNameError = "Please enter your first name";
            }

            if (!(middleName == ""))
            {
                admin.middleName = middleName;
            }
            else
            {
                errorCtr++;
                ViewBag.middleNameError = "Please enter your middle name";
            }

            if (!(gender == ""))
            {
                if (gender == "Male")
                    admin.gender = 'M';
                else admin.gender = 'F';
            }
            else
            {
                errorCtr++;
                ViewBag.genderError = "Please select your gender";
            }

            if (!(birthday == ""))
            {
                string year = birthday[0] + "" + birthday[1] + "" + birthday[2] + "" + birthday[3] + "";
                string month = birthday[5] + "" + birthday[6] + "";
                string day = birthday[8] + "" + birthday[9] + "";

                admin.birthYear = Int32.Parse(year);
                admin.birthMonth = Int32.Parse(month);
                admin.birthDay = Int32.Parse(day);
            }
            else
            {
                errorCtr++;
                ViewBag.birthdayError = "Please select your birthday";
            }

            if (errorCtr == 0)
            {
                admin = adminManager.saveAdmin(admin);
                Session["user"] = admin;
                return RedirectToAction("adminIndex", "Admin"); // go to next step
            }
            else
            {
                return View("adminIndex", admin);
            }
        }
    }
}