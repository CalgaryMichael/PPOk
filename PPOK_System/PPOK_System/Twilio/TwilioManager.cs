using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Linq;
using System.Web;
using Twilio.Clients;
using Hangfire;
using PPOK_System.Service;
using PPOK_System.Models;

namespace PPOK_System.TwilioManager
{
    public class TwManager
    {
        //public void StartHangfire()
        //{
        //    RecurringJob.AddOrUpdate(() => ScheduleSend(),Cron.Daily);
        //}
        public void call()
        {
            string AccountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            string AuthToken = "ACc4455ec9d784ae580638ecac36ad7fea";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            CallResource.Create(
                from: new PhoneNumber("14054000298"),
                to: new PhoneNumber("14059191824"),
                url: new Uri("~/Twilio/TwillioXML/PickUp.xml"));
        }
        public void SendNotifications(List<Schedule> scheduleList)
        {
            const string accountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            const string authToken = "2a6f621c0a94b7e60c906e06e15966fb";

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);

            var people = new Dictionary<string, string>();
            foreach (Schedule s in scheduleList)
            {
                MessageResource.Create(
                    from: new PhoneNumber("405-400-0298"),
                    to: new PhoneNumber(s.person.phone),

                    body: $"{s.person.first_name}, " + "Would you like to refill your perscription? Send '1' for refill");
            }
        }
        public void ScheduleSend()
        {
            Database db = new Database();
            List<Schedule> schedules = db.GetSchedules();
            SendNotifications(schedules);
        }
    }
}