using PPOK_System.Domain.Models;

namespace PPOK_System.Service.Authentication.User {
	public class UserPrincipalSerialize : IUserPrincipal {
		public UserPrincipalSerialize() : base() {}
		public UserPrincipalSerialize(string email) : base() {
			Email = email;
		}

		public UserPrincipalSerialize(Person person) : base() {
			FirstName = person.first_name;
			LastName = person.last_name;
			Phone = person.phone;
			Email = person.email;
			Store = person.store;
			setRole(person.person_type);
		}
	}
}