using PPOK_System.Domain.Service;
using PPOK_System.Models;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
	public class UserController : Controller {
		Database db = new Database(SystemContext.DefaultConnectionString);

		public ActionResult Index() {
			return View();
		}
    }
}