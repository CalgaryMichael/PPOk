using System.Collections.Generic;

namespace PPOK_System.Models {
	public class Store {
		public int store_id { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }

		public List<Person> pharmacists;
	}
}