using System;
using System.Collections.Generic;

namespace PPOK_System.Models {
	public class Person {
		public int person_id { get; set; }
		public int store_id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public DateTime date_of_birth { get; set; }
		public string person_type { get; set; }

		public Store store { get; set; }
		public List<ContactPreference> contact_preference { get; set; }
	}
}