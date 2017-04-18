using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using System.Web.Mvc;
using System.Collections.Generic;

namespace PPOK_System.Controllers
{
    public class AdminController : Controller
    {
        Database db = new Database(SystemContext.DefaultConnectionString);
        // GET: Admin
        public ActionResult Index()
        {
            var s = db.ReadAllStore();
            return View(s);
        }

        public ActionResult EditPharmacy(int id)
        {
            var s = db.ReadSingleStore(id);
            return PartialView(s);
        }


        //See why this isn't updating anymore
        [HttpPost]
        public ActionResult EditPharmacy(Store s)
        {
            db.Update(s);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult AddPharmacy()
        {
            return PartialView();
        }

        //get count of all store, add 1 to get new store id, make a dummy person for the new store.
        [HttpPost]
        public ActionResult AddPharmacy2(Store s)
        {
            List<PPOK_System.Domain.Models.Store> temp = new List<PPOK_System.Domain.Models.Store>();
            PPOK_System.Domain.Models.Person p = new PPOK_System.Domain.Models.Person();
            int? storeID;

            temp = db.ReadAllStore();
            storeID = temp.Count + 1;
            
            p.store_id = storeID;
            p.phone = "2222222222";
            p.person_id = null;
            p.last_name = " ";
            p.password = " ";
            p.zip = " ";
            p.email = " ";
            p.person_type = "dummy";
            p.date_of_birth = System.DateTime.Now;

            db.Create(s);
            db.Create(p);
            return RedirectToAction("Index", "Admin");
        }
    }
}