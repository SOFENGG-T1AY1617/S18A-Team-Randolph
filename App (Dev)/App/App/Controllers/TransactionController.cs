﻿using App.Models;
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
            var user = Session["user"] as Account;
            ViewBag.name = user.firstName;
            ViewBag.degrees = user.degrees;
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
            ViewBag.name = user.firstName;
            return View();
        }
    }
}