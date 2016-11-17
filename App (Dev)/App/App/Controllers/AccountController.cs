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
        //AccountManager manager = new AccountManager();
        AccountModel accountModel = new AccountModel();

        public ActionResult Index()
        {
            // must go to home index
            return View();
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
            Console.WriteLine(email, password);
            ViewBag.Striing = "email: " + email + " password: " + password;
            accountModel.Login(email, password);
            //RedirectToRoute("Transaction/Step1");
        }
        
        
    }
}