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
            ViewBag.name = user.firstName;
            return View();
        }

        public ActionResult info()
        {
            // fill up infos (mailing info, personal, academic)
            var user = Session["user"] as Account;
            ViewBag.name = user.firstName;
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