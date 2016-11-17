using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult cart()
        {
            // view cart
            return View();
        }

        public ActionResult info()
        {
            // fill up infos (mailing info, personal, academic)
            return View();
        }

        public ActionResult confirm()
        {
            // confirm
            return View();
        }

        public ActionResult success()
        {
            // checkout and done
            return View();
        }
    }
}