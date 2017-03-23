using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
	public class UserController : Controller
    {
		Database db = new Database();

		public ActionResult Index()
        {
			return View();
		}

        public ActionResult CatchAll()
        {
            return View();
        }
    }
}