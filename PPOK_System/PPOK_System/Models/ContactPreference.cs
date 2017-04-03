// Represents a user's preference of contact
namespace PPOK_System.Models {
	public class ContactPreference {
		public int? preference_id { get; set; }
		public int? person_id { get; set; }
		public string contact_type { get; set; }
		public string refillPreference { get; set; }
        public string recallPreference { get; set; }


        public Person person { get; set; }
	}
}