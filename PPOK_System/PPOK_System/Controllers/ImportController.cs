using PPOK_System.import;
using PPOK_System.Domain.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
    public class ImportController : BaseController {

		// POST: Import
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file) {
			var updateList = new List<Prescription>();
			if (file.ContentLength > 0)
				updateList = Import.HandleImport(file, User.Identity.Name.Split(',')[0]);

			return PartialView(updateList);
		}


		// POST: Import/Upload
		[HttpPost]
		public ActionResult Upload(List<Prescription> updateList) {
			if (updateList != null)
				Import.UpdateContent(updateList);
			return Redirect("/Pharmacy");
		}
	}
}