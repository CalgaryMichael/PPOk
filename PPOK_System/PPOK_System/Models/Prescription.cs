using System;

namespace PPOK_System.Models {
	public class Prescription {
		public int? prescription_id { get; set; }
		public int? person_id { get; set; }
		public string drug_id { get; set; }
		public DateTime date_filled { get; set; }
		public int? days_supply { get; set; }
		public int? number_refills { get; set; }

		public Person customer;
		public Drug drug;
	}
}