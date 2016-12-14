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
            var user = Session["user"] as Account;
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

        public ActionResult cart()
        {
            // view cart
            var user = Session["user"] as Account;
            var mail = Session["mail"] as MailingInformation;
            var deliveryMethod = Session["deliveryMethod"] as string;
            ViewBag.name = user.firstName;
            return View();
        }

        public ActionResult info()
        {
            // fill up infos (mailing info, personal, academic)
            var user = Session["user"] as Account;
            ViewBag.name = user.firstName;
            ViewBag.mailInfos = user.mailInfos;
            return View();
        }

        public ActionResult success()
        {
            // checkout and done
            var user = Session["user"] as Account;
            ViewBag.userID = user.userID;
            ViewBag.firstName = user.firstName;
            ViewBag.lastName = user.lastName;
            ViewBag.currentAddress = user.currentAddress;
            ViewBag.contactNumber = user.phoneNo;
            ViewBag.email = user.email;
            ViewBag.placeOfBirth = user.placeOfBirth;

            return View();
        }

        [HttpPost]
        public ActionResult pickup(string pickupArea)
        {
            var user = Session["user"] as Account;
            Session["deliveryMethod"] = "pickup";
            Session["mail"] = pickupArea;
            return RedirectToAction("cart", "Transaction"); // go to next step
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
            return RedirectToAction("cart", "Transaction"); // go to next step
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
            return RedirectToAction("cart", "Transaction"); // go to next step
            //return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult checkOut(string json)
        {
            List<Document> cart = JsonConvert.DeserializeObject<List<Document>>(json);
            for (int i = 0; i < cart.Count; i++)
            {
                Debug.WriteLine(cart.ElementAt(i).docuName);
                Debug.WriteLine(cart.Count);
            }
            return RedirectToAction("success", "Transaction");

            
        }
    }
}