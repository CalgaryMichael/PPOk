using PPOK_System.Models;
using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace PPOK_System.import {
	public class Content {
		public Person person { get; set; }
		public Prescription rx { get; set; }
		public Drug drug { get; set; }
	}


	public class Import {
		public static void HandleImport(HttpPostedFileBase file) {
			StreamReader reader = new StreamReader(file.InputStream);
			var results = Csv(reader);
			DetermineContent(results);
		}


		public static List<Content> Csv(StreamReader reader) {
			List<Content> fileContents = new List<Content>();
			reader.ReadLine();			// initialize first row

			// read until file is ended
			while (!reader.EndOfStream) {
				Content lineContent = new Content();

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
				p.person_type = "customer";
				lineContent.person = p;

				// import Drug info
				Drug d = new Drug();
				d.drug_id = values[11];
				d.drug_name = values[12];
				lineContent.drug = d;

				// import Prescription info
				Prescription rx = new Prescription();
				rx.person_id = p.person_id;
				rx.drug_id = d.drug_id;
				if (DateTime.TryParseExact(values[7], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
					rx.date_filled = dt;
				else
					rx.date_filled = DateTime.Now;
				if (Int32.TryParse(values[8], out num))
					rx.prescription_id = num;

				if (Int32.TryParse(values[9], out num))
					rx.days_supply = num;

				if (Int32.TryParse(values[10], out num))
					rx.number_refills = num;
				lineContent.rx = rx;

				fileContents.Add(lineContent);
			}

			return fileContents;
		}


		public static void DetermineContent(List<Content> contents) {
			Database db = new Database();

			foreach (Content c in contents) {
				if (!db.Exists<Person>(c.person.person_id))
					db.Create(c.person);

				if (!db.Exists<Drug>(c.drug.drug_id))
					db.Create(c.drug);

				if (!db.Exists<Prescription>(c.rx.prescription_id))
					db.Create(c.rx);

				try {
					var rel = db.ReadSinglePrescription(c.person.person_id, c.drug.drug_id);
					if (c.rx.date_filled > rel.date_filled) {
						db.Update(c.person);
						db.Update(c.drug);
						db.Update(c.rx);
					}
				} catch (Exception e) {
					Console.WriteLine(e.StackTrace);
				}
			}
		}
	}
}