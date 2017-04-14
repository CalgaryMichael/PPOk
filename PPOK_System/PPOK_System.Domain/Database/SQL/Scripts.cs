using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PPOK_System.Domain.Service.SQL {
	public class Scripts {
		public static string SqlRoot { get; }

		public static string CreateDatabaseSql { get; }
		public static string CreateTablesSql { get; }
		public static string InsertDummyDataSql { get; }

		public static Dictionary<string, string> ScriptDictionary { get; }
		// TODO: implement these read/writes into scripts
		public static Dictionary<string, string> Create { get; }
		public static Dictionary<string, string> Update { get; }
		public static Dictionary<string, string> Delete { get; }

		static Scripts() {
			SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL";
			Debug.WriteLine(SqlRoot.ToString());
			ScriptDictionary = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);
			CreateDatabaseSql = ScriptDictionary["CreateDatabase"];
			CreateTablesSql = ScriptDictionary["CreateTables"];
			InsertDummyDataSql = ScriptDictionary["InsertDummyData"];
		}

	}
}