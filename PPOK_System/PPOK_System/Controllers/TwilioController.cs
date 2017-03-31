using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers
{
    public class TwilioController : Controller
    {
        public void Send(string msg)
        {
            TwilioManager.TwManager TW = new TwilioManager.TwManager();
            TW.sendmsg(msg);
        }
    }
}