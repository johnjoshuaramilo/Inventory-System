using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace DataBase
{
	class DatabaseManager
	{
		MySqlConnection con = new MySqlConnection();
		MySqlDataAdapter oda = new MySqlDataAdapter();

		private DataTable tblResult;
		public DataTable Result
		{
			get { return tblResult; }
		}

		public void Display(string sql)
		{
			oda.SelectCommand = new MySqlCommand(sql, con);
			tblResult = new DataTable();
			oda.Fill(tblResult);
			con.Close();
		}

		public void Insert(string query)
		{
			MySqlCommand oc = new MySqlCommand(query, con);
			oc.ExecuteNonQuery();
			con.Close();
		}

		public void Connect()
		{
				con.ConnectionString = 	"Server=localhost;" +
				"UID=root;" +
				"PWD=;" +
				"Database=db_inventory;" +
				"Convert Zero Datetime = True";

				/*

			con.ConnectionString = "Driver={MySQL ODBC 5.3 ANSI Driver};" +
				"Server=remotemysql.com;" +
				"UID=5lKXZGoclC;" +
				"PWD=qH7ZMtmg8r;" +
				"Database=5lKXZGoclC";
				*/
			con.Open();
		}
	}
}
