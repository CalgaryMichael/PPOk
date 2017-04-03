using Dapper;
using PPOK_System.Models;
using PPOK_System.Service.SQL;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PPOK_System.Service {
	public class Database {
		private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		private string master = ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString;


		#region Database Creation

		// Initialize the Database
		public void initDatabase() {
			CreateDatabase();
			CreateTables();
			InsertData();
		}

		// Drop & Create Database
		private void CreateDatabase() {
			using (IDbConnection db = new SqlConnection(master)) {
				db.Execute(Scripts.CreateDatabaseSql);
			}
		}


		// Create Tables to fill the Database
		private void CreateTables() {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.CreateTablesSql);
			}
		}


		// Fill tables with dummy data
		private void InsertData() {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.InsertDummyDataSql);
			}
		}

		#endregion


		#region Create

		// TODO: turn this into a Scripts dictionary to pull sql from
		// Create new row in "store" table
		public void Create(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "INSERT INTO store VALUES(@address, @city, @state, @zip)";
				db.Execute(sqlQuery, s);
			}
		}


		// Create new row in "person" table
		public void Create(Person p) {
			if (!p.person_id.HasValue)
				p.person_id = GenerateId<Person>();
			p.password = SHA1.Encode(p.password);
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"INSERT INTO person VALUES(@person_id, @store_id, @first_name,
																@last_name, @zip, @phone, @email,
																@password, @date_of_birth, @person_type)";
				db.Execute(sqlQuery, p);
			}
		}


		// Create new row in "prescription" table
		public void Create(Prescription p) {
			if (!p.prescription_id.HasValue)
				p.prescription_id = GenerateId<Prescription>();
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"INSERT INTO prescription VALUES(@prescription_id, @person_id, @drug_id,
																	@date_filled, @days_supply, @number_refills)";
				db.Execute(sqlQuery, p);
			}
		}


		// Create new row in "drug" table
		public void Create(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "INSERT INTO drug VALUES(@drug_id, @drug_name)";
				db.Execute(sqlQuery, d);
			}
		}


		// Create new row in "message_history" table
		public void Create(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "INSERT INTO message VALUES(@prescription_id, @response, @fill_date, @pick_up_date)";
				db.Execute(sqlQuery, m);
			}
		}


		// Create new row in "contact_preference" table
		public void Create(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "INSERT INTO contact_preference VALUES(@person_id, @contact_type, @preference)";
				db.Execute(sqlQuery, c);
			}
		}
		

		#endregion


		#region Read

		// Populate List<Store> with rows in the Db
		public List<Store> ReadAllStore() {
			var lookup = new Dictionary<int, Store>();

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT s.*, p.*
								FROM store AS s, person AS p
								WHERE s.store_id = p.store_id";
				var result = db.Query<Store, Person, Store>(sql,
					(s, p) => {
						Store store;
						if (!lookup.TryGetValue(s.store_id.Value, out store)) { 
							store = s;
							lookup.Add(s.store_id.Value, store);
						}

						if (store.pharmacists == null)
							store.pharmacists = new List<Person>();

						store.pharmacists.Add(p);

						return store;
					},
					splitOn: "store_id,person_id").AsList();
			}

			return lookup.Values.ToList();
		}


		// Populate single Store with row in the Db
		public Store ReadSingleStore(int id) {
			Store store = null;

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT s.*, p.*
								FROM store AS s, person AS p
								WHERE s.store_id = p.store_id
									AND s.store_id = @store_id";
				var result = db.Query<Store, Person, Person>(sql,
					(s, p) => {
						if (store == null)
							store = s;

						p.store = s;
						return p;
					}, new { store_id = id },
					splitOn: "store_id,person_id").AsList();
				if (store != null) {
					store.pharmacists = result;
				}
			}

			return store;
		}


		// Populate List<Drug> with rows in the Db
		public List<Drug> ReadAllDrugs() {
			using (IDbConnection db = new SqlConnection(connection)) {
				return db.Query<Drug>("SELECT * FROM drug").ToList();
			}
		}


		// Populate single Drug with row in the Db
		public Drug ReadSingleDrug(string id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				return db.Query<Drug>("SELECT * FROM drug WHERE drug_id = @drug_id", new { drug_id = id }).FirstOrDefault();
			}
		}


		// Populate List<Prescriptions> with rows in the Db
		public List<Prescription> ReadAllPrescriptions() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p1.*, p2.*, d.*
								FROM prescription AS p1, person AS p2, drug AS d
								WHERE p1.person_id = p2.person_id
									AND p1.drug_id = d.drug_id";
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					},
					splitOn: "prescription_id,person_id,drug_id").AsList();

				return result;
			}
		}


		// Populate single Prescriptions with row in the Db
		public List<Prescription> ReadAllPrescriptionsForPerson(int? id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p1.*, p2.*, d.*
								FROM prescription AS p1, person AS p2, drug AS d
								WHERE p1.person_id = p2.person_id
									AND p1.drug_id = d.drug_id
									AND p1.person_id = @person_id";
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { person_id = id },
					splitOn: "prescription_id,person_id,drug_id").AsList();

				return result;
			}
		}


		// Populate single Prescriptions with row in the Db
		public Prescription ReadSinglePrescription(int? id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p1.*, p2.*, d.*
								FROM prescription AS p1, person AS p2, drug AS d
								WHERE p1.person_id = p2.person_id
									AND p1.drug_id = d.drug_id
									AND p1.prescription_id = @rx_id";
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { rx_id = id },
					splitOn: "prescription_id,person_id,drug_id").FirstOrDefault();

				return result;
			}
		}


		// Populate single Prescriptions with row in the Db
		public Prescription ReadSinglePrescription(int? person_id, string drug_id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p1.*, p2.*, d.*
								FROM prescription AS p1, person AS p2, drug AS d
								WHERE p1.person_id = p2.person_id
									AND p2.person_id = @id
									AND p1.drug_id = d.drug_id
									AND d.drug_id = @code";
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { id = person_id, code = drug_id },
					splitOn: "prescription_id,person_id,drug_id").FirstOrDefault();

				return result;
			}
		}


		// Populate List<Message> with rows in the Db
		public List<Message> ReadAllMessages() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT m.*, p1.*, p2.*, d.*
								FROM message_history AS m, prescription AS p1, person AS p2, drug AS d
								WHERE m.prescription_id = p1.prescription_id
									AND p1.person_id = p2.person_id
									AND p1.drug_id = d.drug_id";
				var result = db.Query<Message, Prescription, Person, Drug, Message>(sql,
					(m, p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;
						m.prescription = p1;

						return m;
					},
					splitOn: "message_id,prescription_id,person_id,drug_id").AsList();

				return result;
			}
		}


		// Populate single Message with row in the Db
		public Message ReadSingleMessage(int id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT m.*, p1.*, p2.*, d.*
								FROM message_history AS m, prescription AS p1, person AS p2, drug AS d
								WHERE m.prescription_id = p1.prescription_id
									AND p1.person_id = p2.person_id
									AND p1.drug_id = d.drug_id
									AND m.message_id = @message_id";
				var result = db.Query<Message, Prescription, Person, Drug, Message>(sql,
					(m, p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;
						m.prescription = p1;

						return m;
					}, new { message_id = id },
					splitOn: "message_id,prescription_id,person_id,drug_id").FirstOrDefault();

				return result;
			}
		}


		// Populate List<Person> with row in the Db
		public List<Person> ReadAllPersons() {
			var lookup = new Dictionary<int, Person>();

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p.*, s.*, c.*
								FROM person AS p, store AS s, contact_preference AS c
								WHERE p.person_id = c.person_id
									AND s.store_id = p.store_id";
				var result = db.Query<Person, Store, ContactPreference, ContactPreference>(sql,
					(p, s, c) => { 
						Person person;
						if (!lookup.TryGetValue(p.person_id.Value, out person))
							lookup.Add(p.person_id.Value, person = p);

						if (person.store == null)
							person.store = s;

						if (person.contact_preference == null)
							person.contact_preference = new List<ContactPreference>();
						c.person = p;
						person.contact_preference.Add(c);

						return c;
					},
					splitOn: "person_id,store_id,preference_id").AsList();

				return lookup.Values.ToList();
			}
		}


		// Populate Single Person with all Store and ContactPreferences tied to it
		public Person ReadSinglePerson(int id) {
			Person person = null;

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p.*, s.*, c.*
								FROM person AS p, store AS s, contact_preference AS c
								WHERE p.person_id = c.person_id
									AND s.store_id = p.store_id
									AND p.person_id = @person_id";
				var result = db.Query<Person, Store, ContactPreference, Person>(sql,
					(p, s, c) => {
						if (person == null) {
							person = p;
							person.store = s;
						} 
						c.person = person;

						if (person.contact_preference == null)
							person.contact_preference = new List<ContactPreference>();
						person.contact_preference.Add(c);

						return p;
					}, new { person_id = id },
					splitOn: "person_id,store_id,preference_id").FirstOrDefault();

				return person;
			}
		}


		// Populate Single Person with all Store and ContactPreferences tied to it
		public Person ReadSinglePerson(string email) {
			Person person = null;

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT p.*, s.*, c.*
								FROM person AS p, store AS s, contact_preference AS c
								WHERE p.person_id = c.person_id
									AND s.store_id = p.store_id
									AND p.email = @email";
				var result = db.Query<Person, Store, ContactPreference, Person>(sql,
					(p, s, c) => {
						if (person == null) {
							person = p;
							person.store = s;
						}
						c.person = person;

						if (person.contact_preference == null)
							person.contact_preference = new List<ContactPreference>();
						person.contact_preference.Add(c);

						return p;
					}, new { email = email },
					splitOn: "person_id,store_id,preference_id").FirstOrDefault();

				return person;
			}
		}


		// Read and get items from scheduler for todays date
		public List<Schedule> GetSchedules() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = @"SELECT s.*, p.person_id, pers.*
								FROM scheduler AS s, prescription AS p, person AS pers
								WHERE Convert(date, s.day_to_send) = Convert(date, GETDATE())
									AND s.prescription_id = p.prescription_id
									AND p.person_id = pers.person_id";
				var result = db.Query<Schedule, Prescription, Person, Schedule>(sql,
					(s, p, pers) => {
						s.person = pers;
						return s;
					},
					splitOn: "prescription_id,person_id").AsList();
				return result;
			}
		}


		#endregion


		#region Update

		// Update row in "store" table
		public void Update(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE store
									SET address = @address, city = @city, state = @state, zip = @zip
									WHERE store_id = @store_id";
				db.Execute(sqlQuery, s);
			}
		}


		// Update row in "person" table
		public void Update(Person p) {
			p.password = SHA1.Encode(p.password);
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE person
									SET store_id = @store_id, first_name = @first_name, last_name = @last_name,
										zip = @zip, email = @email, password = @password, phone = @phone,
										date_of_birth = @date_of_birth, person_type = @person_type
									WHERE person_id = @person_id";
				db.Execute(sqlQuery, p);
			}
		}


		// Update row in "person" table
		public void Update(Prescription p) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE prescription
									SET person_id = @person_id, drug_id = @drug_id, date_filled = @date_filled,
										days_supply = @days_supply, number_refills = @number_refills
									WHERE prescription_id = @prescription_id";
				db.Execute(sqlQuery, p);
			}
		}


		// Update row in "drug" table
		public void Update(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE drug
									SET drug_name = @drug_name
									WHERE drug_id = @drug_id";
				db.Execute(sqlQuery, d);
			}
		}


		// Update row in "message_history" table
		public void Update(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE message_hisory
									SET prescription_id = @prescription_id, response = @response,
										fill_date = @fill_date, pick_up_date = @pick_up_date
									WHERE message_id = @message_id";
				db.Execute(sqlQuery, m);
			}
		}


		// Update row in "contact_preference" table
		public void Update(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = @"UPDATE contact_preference
									SET person_id = @person_id, contact_type = @contact_type, prefenece = @preference
									WHERE preference_id = @preference_id";
				db.Execute(sqlQuery, c);
			}
		}
		
		#endregion


		#region Delete

		// Delete row in "store" table
		public void Delete(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM store WHERE store_id = @store_id";
				db.Execute(sqlQuery, s);
			}
		}


		// Delete row in "person" table
		public void Delete(Person u) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM person WHERE person_id = @person_id";
				db.Execute(sqlQuery, u);
			}
		}


		// Delete row in "prescription" table
		public void Delete(Prescription s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM prescription WHERE prescription_id = @prescription_id";
				db.Execute(sqlQuery, s);
			}
		}


		// Delete row in "drug" table
		public void Delete(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM drug WHERE drug_id = @drug_id";
				db.Execute(sqlQuery, d);
			}
		}


		// Delete row in "message_history" table
		public void Delete(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM message_history WHERE message_id = @message_id";
				db.Execute(sqlQuery, m);
			}
		}


		// Delete row in "contact_preference" table
		public void Delete(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sqlQuery = "DELETE FROM contact_preference WHERE preference_id = @preference_id";
				db.Execute(sqlQuery, c);
			}
		}

		#endregion


		#region Misc

		// Generate ID Number
		public int GenerateId<T>() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.Query<int>($"SELECT Max([{name}_id]) FROM [{name}]").First() + 1;
			}
		}


		// See if T exsists in Database by int id
		public bool Exists<T>(int? id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.ExecuteScalar<bool>($"SELECT COUNT(1) FROM {name} WHERE {name}_id=@id", new { id });
			}
		}


		// See if T exsists in Database by string id
		public bool Exists<T>(string id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.ExecuteScalar<bool>($"SELECT COUNT(1) FROM {name} WHERE {name}_id=@id", new { id });
			}
		}

		#endregion
	}
}