using PPOK_System.Models;
using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PPOK_System.Controllers {
    public class HomeController : Controller {
		Database db = new Database();

        // GET: Home
        public ActionResult Index() {
            return RedirectToAction("Login");
        }


		// GET: Home/Login
		[HttpGet]
		public ActionResult Login() {
			return View();
		}


		// POST: Home/Login
		[HttpPost]
		public ActionResult Login(Person loginAttempt) {
			var person = db.ReadSinglePerson(loginAttempt.email);

			if (person.password == loginAttempt.password) {
				FormsAuthentication.SetAuthCookie(person.email, true);

				if (person.person_type == "admin") {
					return RedirectToAction("Index", "Admin");
				} else if (person.person_type == "pharm") {
					return RedirectToAction("Index", "Pharmacy");
				} else {
					return RedirectToAction("Index", "User");
				}
			} else {
				// throw IsValid error
			}

			return View(loginAttempt);
		}


		// GET: Home/Logout
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}