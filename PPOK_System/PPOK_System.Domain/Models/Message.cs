using System;

// Represents a message sent to a customer and their response
namespace PPOK_System.Domain.Models {
	public class Message {
		public int? message_id { get; set; }
		public int? prescription_id { get; set; }
		public string response { get; set; }
		public DateTime fill_date { get; set; }
		public DateTime pick_up_time { get; set; }
		public bool filled { get; set; }

		public Prescription prescription { get; set; }
	}
}