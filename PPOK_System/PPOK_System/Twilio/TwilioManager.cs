using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Linq;
using System.Web;
using Twilio.Clients;
using Hangfire;

namespace PPOK_System.TwilioManager
{
    public class TwManager
    {
        public void StartHangfire()
        {
            RecurringJob.AddOrUpdate(
    () => Console.WriteLine("Recurring!"),
          Cron.Daily);
        }
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
        public void sendmsg(string msg)
        {
            const string accountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            const string authToken = "2a6f621c0a94b7e60c906e06e15966fb";

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);

            var people = new Dictionary<string, string>() {
				{"+14059191824", "Weston Vidaurri"}
			};

            foreach (var person in people)
            {
                MessageResource.Create(
                    from: new PhoneNumber("405-400-0298"),
                    to: new PhoneNumber(person.Key),
                                                     
                    body: $"Hey {person.Value} "+msg);

            }
        }
    }
}