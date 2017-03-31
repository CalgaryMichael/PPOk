using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOK_System.Models
{
    public class Schedule
    {
        public int task_id { get; set; }
        public int rx_id { get; set; }
        public string response { get; set; }
        public DateTime day_to_send { get; set; }
        public Person person { get; set; }
}
}