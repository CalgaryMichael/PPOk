using System;

namespace PPOK_System.Models {
	public class Prescription {
		public int rx_id { get; set; }
		public int user_id { get; set; }
		public int drug_id { get; set; }
		public DateTime date_filled { get; set; }
		public int days_supply { get; set; }
		public int num_refills { get; set; }

		public Person customer;
		public Drug drug;
	}
}