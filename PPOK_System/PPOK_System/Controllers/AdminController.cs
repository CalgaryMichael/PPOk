using PPOK_System.Models;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using System.Web.Mvc;


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

        [HttpPost]
        public ActionResult AddPharmacy2(Store s)
        {
            
            PPOK_System.Domain.Models.Person p = new PPOK_System.Domain.Models.Person();
            int? storeID = s.store_id;
            
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