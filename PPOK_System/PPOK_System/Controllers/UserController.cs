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
            //get all the things (and stuff - lori) from form by name
            //put it into models, and assign them to the Person
            //update said person's preferences
            return View();
        }
    }
}