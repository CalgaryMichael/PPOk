using PPOK_System.import;
using PPOK_System.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
    public class ImportController : Controller {

		// POST: Import
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file) {
			var updateList = new List<Prescription>();
			if (file.ContentLength > 0)
				updateList = Import.HandleImport(file);

			return View(updateList);
		}


		// POST: Import/Upload
		[HttpPost]
		public ActionResult Upload(List<Prescription> updateList) {
			Import.UpdateContent(updateList);
			return Redirect("/Pharmacy");
		}
	}
}