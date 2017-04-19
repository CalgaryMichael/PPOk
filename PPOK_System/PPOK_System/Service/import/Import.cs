using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service;
using PPOK_System.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace PPOK_System.import {
	public class Import {
		private static Database db = new Database(SystemContext.DefaultConnectionString);
		private static Person user;

		public static List<Prescription> HandleImport(HttpPostedFileBase file, string email) {
			user = db.ReadSinglePerson(email);
			StreamReader reader = new StreamReader(file.InputStream);
			var results = Csv(reader);
			return DetermineContent(results);
		}


		public static List<Prescription> Csv(StreamReader reader) {
			List<Prescription> fileContents = new List<Prescription>();
			reader.ReadLine();			// initialize first row

			// read until file is ended
			while (!reader.EndOfStream) {
				Prescription lineContent = new Prescription();

				int num = 0;
				DateTime dt = DateTime.Now;
				string pattern = "yyyyMMdd";

				// read in CSV file and create objects
				var line = reader.ReadLine();
				var values = line.Split(',');

				// import Person info
				Person p = new Person();
				if (Int32.TryParse(values[0], out num))
					p.person_id = num;
				p.first_name = values[1];
				p.last_name = values[2];
				if (DateTime.TryParseExact(values[3], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
					p.date_of_birth = dt;
				else
					p.date_of_birth = DateTime.Now;
				if (values[4].Length > 5)
					values[4] = values[4].Remove(5);
				p.zip = values[4];
				p.phone = values[5];
				p.email = values[6];
				p.person_type = "Customer";
				lineContent.customer = p;

				// import Drug info
				Drug d = new Drug();
				d.drug_id = values[11];
				d.drug_name = values[12];
				lineContent.drug = d;

				// import Prescription info
				lineContent.person_id = p.person_id;
				lineContent.drug_id = d.drug_id;
				if (DateTime.TryParseExact(values[7], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
					lineContent.date_filled = dt;
				else
					lineContent.date_filled = DateTime.Now;
				if (Int32.TryParse(values[8], out num))
					lineContent.prescription_id = num;

				if (Int32.TryParse(values[9], out num))
					lineContent.days_supply = num;

				if (Int32.TryParse(values[10], out num))
					lineContent.number_refills = num;

				fileContents.Add(lineContent);
			}

			return fileContents;
		}


		public static List<Prescription> DetermineContent(List<Prescription> contents) {
			List<Prescription> updateList = new List<Prescription>();
			var store_id = user.store_id;

			foreach (Prescription c in contents) {
				c.customer.store_id = store_id;
				if (!db.Exists<Person>(c.customer.person_id)) {
					db.Create(c.customer);
					//generateContact(c.customer);
				}

				if (!db.Exists<Drug>(c.drug.drug_id))
					db.Create(c.drug);

				if (!db.Exists<Prescription>(c.prescription_id))
					db.Create(c);

				try {
					var dbContent = db.ReadSinglePrescription(c.customer.person_id, c.drug.drug_id);
					if (c.date_filled > dbContent.date_filled) {
						c.customer.store_id = dbContent.customer.store_id;
						c.customer.password = dbContent.customer.password;
						c.prescription_id = dbContent.prescription_id;
						updateList.Add(c);
					}
				} catch (Exception e) {
					Debug.WriteLine(c.prescription_id + ": " + e.StackTrace);
				}
			}

			return updateList;
		}


		public static void UpdateContent(List<Prescription> contents) {
			foreach (Prescription rx in contents) {
				db.Update(rx.customer);
				db.Update(rx.drug);
				db.Update(rx);
			}
		}
	}
}