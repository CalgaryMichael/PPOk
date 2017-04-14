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
    }
}