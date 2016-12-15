using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [System.Runtime.InteropServices.Guid("4DB6BB15-3DDB-40A2-AE96-61EE591F24B3")]
    public class TransactionController : Controller
    {
        Account user = null;
        
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
            // redirect to order/step1
        }

        public ActionResult order()
        {
            // order documents
            // get documents info from db put in a list
            // ViewBag.documents = <listname>;
            user = Session["user"] as Account;
            var documentManager = new documentManager();
            ViewBag.bachelorsDocuments = documentManager.getAvailableDocument("Bachelors");
            ViewBag.mastersDocuments = documentManager.getAvailableDocument("Masters");
            ViewBag.doctorateDocuments = documentManager.getAvailableDocument("Doctorate");
            ViewBag.degrees = user.degrees;
            ViewBag.name = user.firstName;

            ViewBag.documentsJSON = JsonConvert.SerializeObject(documentManager.getAllDocuments());
            ViewBag.degreesJSON = JsonConvert.SerializeObject(user.degrees);
            return View();
        }

        public ActionResult payment()
        {
            return View();
        }

        public ActionResult cart()
        {
            // view cart
            user = Session["user"] as Account;
            ViewBag.name = user.firstName;
            return View();
        }

        public ActionResult info()
        {
            // fill up infos (mailing info, personal, academic)
            user = Session["user"] as Account;
            ViewBag.name = user.firstName;
            ViewBag.mailInfos = user.mailInfos;
            return View();
        }

        public ActionResult success()
        {
            // checkout and done
            user = Session["user"] as Account;
            string deliveryMethod = Session["deliveryMethod"] as string;

            ViewBag.name = user.firstName;
            ViewBag.userID = user.userID;
            ViewBag.fullName = user.firstName + " " + user.lastName;
            ViewBag.currentAddress = user.currentAddress;
            ViewBag.contactNumber = user.phoneNo;
            ViewBag.email = user.email;
            ViewBag.placeOfBirth = user.placeOfBirth;
            ViewBag.method = deliveryMethod;
            if(deliveryMethod == "shipping")
            {
                MailingInformation mail = Session["mail"] as MailingInformation;
                DeliveryRate deliv = new DeliveryRate();
                DeliveryRateManager dmanager = new DeliveryRateManager();
                deliv = dmanager.getDeliveryRate(dmanager.getLocation(mail.locationID));
                ViewBag.mailingAddress = mail.streetname + " " + mail.addressline + " " + mail.city + " " + mail.country + " " + mail.zipcode;
                ViewBag.delivArea = deliv.location;
                ViewBag.delivCharge = deliv.price;
                ViewBag.dateNow = DateTime.Today.ToString();
                ViewBag.dateCourier = DateTime.Today.AddDays(5).ToString();
                ViewBag.dueDate = DateTime.Today.AddDays(5+deliv.delay).ToString();
            } else
            {
                var mail = Session["mail"] as string;
                ViewBag.pickupArea = mail;
                ViewBag.dateNow = DateTime.Today.ToString();
                ViewBag.dueDate = DateTime.Today.AddDays(5).ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult pickup(string pickupArea)
        {
            var user = Session["user"] as Account;
            Session["deliveryMethod"] = "pickup";
            Session["mail"] = pickupArea;
            return RedirectToAction("success", "Transaction"); // go to next step
            //return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult exist(string existID)
        {
            var user = Session["user"] as Account;
            MailingInfoModel manager = new MailingInfoModel();

            MailingInformation mail = manager.getMail(Int32.Parse(existID)); // eto yung mail
            Session["deliveryMethod"] = "shipping";
            Session["mail"] = mail;
            return RedirectToAction("success", "Transaction"); // go to next step
            //return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult SaveInfo(string newAddress, string newZipCode, string newStreet, string newCity, string newCountry,
            string newDelivArea, string newContactNumber, string newContactPerson)
        {
            var user = Session["user"] as Account;
            MailingInformation mail = new MailingInformation();
            DeliveryRateManager delivManager = new DeliveryRateManager();
            MailingInfoModel manager = new MailingInfoModel();
            mail.addressline = newAddress;
            mail.city = newCity;
            mail.streetname = newStreet;
            mail.zipcode = newZipCode;
            mail.contactNumber = newContactNumber;
            mail.contactPerson = newContactPerson;
            mail.country = newCountry;
            mail.userID = user.userID;
            mail.locationID = delivManager.getLocationID(newDelivArea);
            ViewBag.mailDetails = mail.addressline + " " + mail.city + " " + mail.streetname + " " + mail.zipcode + " " + mail.contactNumber + " " + mail.contactPerson + " " + mail.country + " ";

            manager.addMailingInfo(mail);
            AccountManager aman = new AccountManager();
            Account acc = aman.getAccount(user.email, user.password);
            Session["deliveryMethod"] = "shipping";
            Session["user"] = acc;
            Session["mail"] = mail;
            return RedirectToAction("success", "Transaction"); // go to next step
            //return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void checkout(string jsonCart, int userID, float price, int mailingID)
        {
            List<Order> cart = JsonConvert.DeserializeObject<List<Order>>(jsonCart);
            Transaction temp = new Transaction();
            Debug.WriteLine("userID: "+userID);
            Debug.WriteLine("Mailing: " + mailingID);
            string deliveryProcessing;
            bool hasExpress = false;
            bool hasRegular = false;

            for (int i = 0; i < cart.Count(); i++)
            {
                if ((cart.ElementAt(i).deliveryRate).Equals("regular"))
                {
                    hasRegular = true;
                }

                if ((cart.ElementAt(i).deliveryRate).Equals("express"))
                {
                    hasExpress = true;
                }
            }

            DateTime dateValue = DateTime.Now;
            DateTime dateToday = DateTime.Now;
            temp.dateRequested = dateToday.ToString("yyyy-MM-ddHH:mm:ss");
            if (hasExpress && hasRegular)
            {
                deliveryProcessing = "mixed";
                temp.dateDue = dateValue.AddDays(3).ToString("yyyy-MM-ddHH:mm:ss");
                temp.estimatedDeliveryDate = temp.dateDue;
            }
            else if (hasExpress)
            {
                deliveryProcessing = "express";
                temp.dateDue = dateValue.AddDays(3).ToString("yyyy-MM-ddHH:mm:ss");
                temp.estimatedDeliveryDate = temp.dateDue;
            }
            else
            {
                deliveryProcessing = "regular";
                temp.dateDue = dateValue.AddDays(6).ToString("yyyy-MM-ddHH:mm:ss");
                temp.estimatedDeliveryDate = temp.dateDue;
            }

            temp.userID = userID;
            temp.price = price;
            temp.mailingID = mailingID;
            temp.deliveryProcessing = deliveryProcessing;
          

            transactionManager tManager = new transactionManager();
            tManager.saveTransaction(temp);
            //Vars to Access DB
            orderManager orDB = new orderManager();

            for (int i = 0; i < cart.Count(); i++)
            {
                orDB.saveOrder(cart.ElementAt(i), tManager.getTransaction(userID).ElementAt(0).transactionID);
            }
        }
    }
}