using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers
{
    public class PharmacyController : Controller
    {
        // GET: Pharmacy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCustomer()
        {
            return View();
        }

        public ActionResult Recall()
        {
            return View();
        }
    }
}