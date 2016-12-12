using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        adminVerifyManager verifyManager = new adminVerifyManager();
        adminDeliveryManager delivManager = new adminDeliveryManager();
        adminDocumentManager docuManager = new adminDocumentManager();

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
            return View();
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

    }
}