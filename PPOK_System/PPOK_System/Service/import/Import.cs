using PPOK_System.Models;
using PPOK_System.Service;
using System;
using System.Collections.Generic;
using System.IO;

namespace PPOK_System.import {
	public class Content {
		public Person person { get; set; }
		public Prescription rx { get; set; }
		public Drug drug { get; set; }
	}


	public class Import {
		public static List<Content> csv(string fileName) {
			// find filepath in system
			using (var fs = File.OpenRead(fileName)) {
				using (var reader = new StreamReader(fs)) {
					List<Content> fileContents = new List<Content>();
					reader.ReadLine();			// initialize first row

					// read until file is ended
					while (!reader.EndOfStream) {
						int num = 0;
						DateTime dt = DateTime.MinValue;
						Content lineContent = new Content();

						// read in CSV file and create objects
						var line = reader.ReadLine();
						var values = line.Split(',');

						// import Person info
						Person p = new Person();
						if (Int32.TryParse(values[0], out num))
							p.person_id = num;
						p.first_name = values[1];
						p.last_name = values[2];
						string pattern = "yyyyMMdd";
						if (DateTime.TryParseExact(values[3], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
							p.date_of_birth = dt;
						p.zip = values[4];
						p.phone = values[5];
						p.email = values[6];
						lineContent.person = p;

						// import Prescription info
						Prescription rx = new Prescription();
						if (DateTime.TryParseExact(values[7], pattern, null, System.Globalization.DateTimeStyles.None, out dt))
							rx.date_filled = dt;
						
						if (Int32.TryParse(values[8], out num))
							rx.rx_id = num;

						if (Int32.TryParse(values[9], out num))
							rx.days_supply = num;

						if (Int32.TryParse(values[10], out num))
							rx.number_refills = num;
						lineContent.rx = rx;

						// import Drug info
						Drug d = new Drug();
						d.NDCUPCHRI = values[11];
						d.drug_name = values[12];
						lineContent.drug = d;

						fileContents.Add(lineContent);
					}

					return fileContents;
				}
			}
		}
	}
}