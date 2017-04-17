using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
    public class PharmacyController : Controller {
		Database db = new Database(SystemContext.DefaultConnectionString);

        // GET: Pharmacy
        public ActionResult Index() {
			var p = db.ReadAllMessages();
            return View(p);
        }


		// GET: /Pharmacy/EditCustomer/{id}
        public ActionResult EditCustomer(int id) {
			var p = db.ReadSinglePerson(id);
            return PartialView(p);
        }


		// POST: /Pharmacy/EditPharmacy/{store}
		[HttpPost]
		public ActionResult EditCustomer(Person p) {
			db.Update(p);
			return RedirectToAction("manageCustomer", "Pharmacy");
		}


        public ActionResult Recall()
        {
            return View();
        }


        public ActionResult History()
        {
            return View();
        }


		// GET: /Pharmacy/PersonHistory/
        public ActionResult manageCustomer() {
			var p = db.ReadAllPersons();
            return View(p);
        }
		

		// POST: /Pharmacy/PersonHistory/{id}
		[HttpPost]
		public ActionResult PersonHistory(int id) {
			var msg = db.ReadAllMessagesForPerson(id);
			return PartialView(msg);
		}
	}
}