using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPOK_System.import;

namespace PPOK_System_Tests.tests {
	[TestClass]
	public class ImportTests {
		[TestMethod]
		public void testCsv() {
			// test file
			string fileName = "../../../../PPOK_System/PPOK_System/data/scrubbed_data.csv";
			var result = Import.csv(fileName);

			// check size of result
			Assert.AreEqual(result.Count, 984);

			// check zeroth row
			Assert.AreEqual(result[0].person.first_name, "STEPHNIE");
			Assert.AreEqual(result[0].person.last_name, "EIDSNESS");
			Assert.AreEqual(result[0].person.date_of_birth.Year, 2004);
			Assert.AreEqual(result[0].person.date_of_birth.Month, 10);
			Assert.AreEqual(result[0].person.date_of_birth.Day, 12);
			Assert.AreEqual(result[0].person.zip, "98008");

			Assert.AreEqual(result[0].rx.days_supply, 30);
			Assert.AreEqual(result[0].rx.date_filled.Year, 2008);
			Assert.AreEqual(result[0].rx.date_filled.Month, 05);
			Assert.AreEqual(result[0].rx.date_filled.Day, 06);

			Assert.AreEqual(result[0].drug.NDCUPCHRI, "60505006501");
			Assert.AreEqual(result[0].drug.drug_name, "Omeprazole Cap Delayed Release 20 MG");

			// check fourth row
			Assert.AreEqual(result[4].person.first_name, "Nina");
			Assert.AreEqual(result[4].person.last_name, "Paege Dp30");
			Assert.AreEqual(result[4].person.date_of_birth.Year, 1923);
			Assert.AreEqual(result[4].person.date_of_birth.Month, 01);
			Assert.AreEqual(result[4].person.date_of_birth.Day, 26);
			Assert.AreEqual(result[4].person.zip, "");

			Assert.AreEqual(result[4].rx.days_supply, 25);
			Assert.AreEqual(result[4].rx.date_filled.Year, 2008);
			Assert.AreEqual(result[4].rx.date_filled.Month, 05);
			Assert.AreEqual(result[4].rx.date_filled.Day, 06);

			Assert.AreEqual(result[4].drug.NDCUPCHRI, "65027225");
			Assert.AreEqual(result[4].drug.drug_name, "Olopatadine HCl Ophth Soln 0.2% (Base Equivalent)");
		}
	}
}
