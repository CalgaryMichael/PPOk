using System;
using System.Security.Principal;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Models;
using System.Diagnostics;

namespace PPOK_System.Service.Authentication.User {
	public enum Roles { Admin, Pharmacist, Customer }

	public class IUserPrincipal : IPrincipal {
		public IIdentity Identity { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public Store Store { get; set; }
		public Roles Role { get; set; }

		public IUserPrincipal() {}


		public IUserPrincipal(Person person) : base() {
			Identity = new GenericIdentity(person.email);
			Email = person.email;
		}


		public void setRole(string role) {
			foreach (Roles r in Enum.GetValues(typeof(Roles))) {
				if (r.ToString().Equals(role)) {
					Role = r;
				}
			}
		}


		public bool IsInRole(string role) {
			Debug.WriteLine(Role.ToString());
			return Role.ToString().Equals(role);
		}


		public string getName() { return FirstName + " " + LastName; }
	}


	public class UserPrincipal : IUserPrincipal {
		public UserPrincipal(Person person) : base() {
			Identity = new GenericIdentity(person.email);
			FirstName = person.first_name;
			LastName = person.last_name;
			Phone = person.phone;
			Email = person.email;
			Store = person.store;
			setRole(person.person_type);
		}

		public UserPrincipal(string email) : base() {
			var db = new Database(SystemContext.DefaultConnectionString);
			var person = db.ReadSinglePerson(email);

			Identity = new GenericIdentity(email);
			FirstName = person.first_name;
			LastName = person.last_name;
			Phone = person.phone;
			Email = person.email;
			Store = person.store;
			setRole(person.person_type);
		}

		public UserPrincipal(UserPrincipalSerialize ups) : base() {
			Identity = new GenericIdentity(ups.Email);
			FirstName = ups.FirstName;
			LastName = ups.LastName;
			Phone = ups.Phone;
			Email = ups.Email;
			Store = ups.Store;
			Role = ups.Role;
		}
	}
}
