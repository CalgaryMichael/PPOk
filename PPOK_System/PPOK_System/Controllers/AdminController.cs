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
            db.Create(s);
            return RedirectToAction("Index", "Admin");
        }
    }
}