// Represents a user's preference of contact
namespace PPOK_System.Domain.Models {
	public class ContactPreference {
		public int? preference_id { get; set; }
		public int? person_id { get; set; }
		public string contact_type { get; set; }
		public string preference { get; set; }

        //public Person person { get; set; }
	}
}