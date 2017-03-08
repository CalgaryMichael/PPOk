using Dapper;
using PPOK_System.Service.SQL;
using System;
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
			//InsertData();
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


		#endregion


		#region Read


		#endregion


		#region Update


		#endregion


		#region Delete


		#endregion
	}
}