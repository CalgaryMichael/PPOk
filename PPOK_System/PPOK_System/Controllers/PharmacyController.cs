using PPOK_System.import;
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

        public ActionResult History()
        {
            return View();
        }

        public ActionResult manageCustomer()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }


		// POST: Pharmacy/Upload/
		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase file) {
			if (file.ContentLength > 0) {
				//var fileName = Path.GetFileName(file.FileName);
				//var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
				//file.SaveAs(path);
				Import.HandleImport(file);
			}

			return RedirectToAction("Index");
		}
	}
}