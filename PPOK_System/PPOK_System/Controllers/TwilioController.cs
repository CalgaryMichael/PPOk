using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPOK_System.Controllers
{
    public class TwilioController : Controller
    {
        public void ScheduleNotification()
        {
            TwilioManager.TwManager TW = new TwilioManager.TwManager();
            TW.ScheduleSend();
        }
        //public void SchedulerStart()
        //{
        //    TwilioManager.TwManager TW = new TwilioManager.TwManager();
        //    TW.StartHangfire();
        //}
    }
}