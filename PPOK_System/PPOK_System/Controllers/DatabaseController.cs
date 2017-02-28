using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
	public class DatabaseController : Controller {
		Database db = new Database();

		// GET: Database
		public ActionResult Index() {
			db.initDatabase();
			return View();
		}
	}
}