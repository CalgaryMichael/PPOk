using System;

namespace PPOK_System.Models {
	public class Message {
		public int message_id { get; set; }
		public int rx_id { get; set; }
		public string response { get; set; }
		public DateTime fill_date { get; set; }
		public DateTime pick_up_time { get; set; }
	}
}