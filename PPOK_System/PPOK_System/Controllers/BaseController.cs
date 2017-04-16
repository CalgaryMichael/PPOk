using PPOK_System.Service.Authentication.User;
using System.Web.Mvc;

namespace PPOK_System.Controllers {
    public class BaseController : Controller {
		protected virtual new UserPrincipal User {
			get { return HttpContext.User as UserPrincipal; }
		}
	}
}