using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Linq;
using System.Web;

namespace PPOK_System.Twilio
{
    public class TwilioTest
    {
        public static void sendmsg(string msg)
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
                                                     
                    body: $"Hey {person.Value} Monkey Party at 6PM. Bring Bananas!");

            }
        }
    }
}