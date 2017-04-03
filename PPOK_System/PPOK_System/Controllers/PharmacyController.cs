using PPOK_System.import;
using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers
{
    public class PharmacyController : Controller
    {
		Database db = new Database();

        // GET: Pharmacy
        public ActionResult Index() {
			var p = db.ReadAllMessages();
            return View(p);
        }


        public ActionResult EditCustomer()
        {
            return View();
        }


        public ActionResult Recall()
        {
            return View();
        }


        public ActionResult History()
        {
            return View();
        }


        public ActionResult manageCustomer() {
			var p = db.ReadAllPersons();
            return View(p);
        }


        public ActionResult ResetPassword()
        {
            return View();
        }
	}
}