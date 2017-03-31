using System;

namespace PPOK_System.Models {
    public class Schedule {
        public int task_id { get; set; }
        public int prescription_id { get; set; }
        public string response { get; set; }
        public DateTime day_to_send { get; set; }
        public Person person { get; set; }
	}
}