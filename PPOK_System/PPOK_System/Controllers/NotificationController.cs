using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;

namespace PPOK_System.Controllers {
    public class NotificationController : Controller {
        TwilioManager.TwManager TW = new TwilioManager.TwManager();
        public void ScheduleNotification() {
            TW.ScheduleSend();
        }
        public void SchedulerStart()
        {
            TW.StartHangfire();
        }

        [HttpPost]
        public ActionResult replyTest(SmsRequest request)
        {
           
            if (request.Body == "1")
            {
                TW.SendResponse(request.From, "we will start your refill");
            }
            else if (request.Body == "2")
            {
                TW.SendResponse(request.From, "we will not refill your medication");
            }
            return null;
        }
    }
}