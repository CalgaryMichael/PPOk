using System;
using System.Collections.Generic;

namespace PPOK_System.Models {
	public class User {
		public int user_id { get; set; }
		public int store_id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public DateTime dob { get; set; }
		public string PersonType { get; set; }

		public Store store { get; set; }
		// consider changing the second string to be an enumerable
		// that contains the different contact preferences
		public Dictionary<string, string> contact_preference { get; set; }
	}
}