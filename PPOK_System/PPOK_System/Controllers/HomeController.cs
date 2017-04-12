using PPOK_System.Models;
using PPOK_System.Service;
using PPOK_System.Service.Authentication;
using PPOK_System.TwilioManager;
using System.Web.Mvc;
using System.Web.Security;

namespace PPOK_System.Controllers {
    public class HomeController : Controller {
		Database db = new Database();

        // GET: Home
        public ActionResult Index() {
            //FormsAuthentication.SignOut();
            TwManager tw = new TwManager();

            tw.StartHangfire();
            return RedirectToAction("Login");
        }


		// GET: Home/Login
		[HttpGet]
		public ActionResult Login() {
			if (User.Identity.IsAuthenticated) {
				var email = User.Identity.Name.Split(',')[0];
				var person = db.ReadSinglePerson(email);

				if (person.person_type == "admin") {
					return RedirectToAction("Index", "Admin");
				} else if (person.person_type == "pharm") {
					return RedirectToAction("Index", "Pharmacy");
				} else if (person.person_type == "customer") {
					return RedirectToAction("Index", "User");
				}
			}

			return View();
		}


		// POST: Home/Login
		[HttpPost]
		public ActionResult Login(Person loginAttempt) {
			var person = db.ReadSinglePerson(loginAttempt.email);

			if (Password.Authenticate(loginAttempt.password, person.password)) {
				string cookie = person.email + "," + person.person_type;
				FormsAuthentication.SetAuthCookie(cookie, false);

				if (person.person_type == "admin") {
					return RedirectToAction("Index", "Admin");
				} else if (person.person_type == "pharm") {
					return RedirectToAction("Index", "Pharmacy");
				} else {
					return RedirectToAction("Index", "User");
				}
			} else {
				ModelState.AddModelError("", "Login data is incorrect!");
			}

			return View(loginAttempt);
		}


		// GET: Home/Logout
		public ActionResult Logout() {
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Home");
		}
	}
}