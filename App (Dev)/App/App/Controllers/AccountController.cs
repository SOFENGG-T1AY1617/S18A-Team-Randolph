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
            return View();
        }

        public ActionResult login()
        {
            //Account account = manager.getAccount();
            return View();
        }
        
        public ActionResult error()
        {
            return View();
        }
    }
}