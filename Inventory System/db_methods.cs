using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;


namespace DataBase
{
	class DatabaseManager
	{
		OdbcConnection con = new OdbcConnection();
		OdbcDataAdapter oda = new OdbcDataAdapter();

		private DataTable tblResult;
		public DataTable Result
		{
			get { return tblResult; }
		}

		public void Display(string sql)
		{
			oda.SelectCommand = new OdbcCommand(sql, con);
			tblResult = new DataTable();
			oda.Fill(tblResult);
			con.Close();
		}

		public void Insert(string query)
		{
			OdbcCommand oc = new OdbcCommand(query, con);
			oc.ExecuteNonQuery();
			con.Close();
		}

		public void Connect()
		{
				con.ConnectionString = "Driver={MySQL ODBC 5.3 ANSI Driver};" +
				"Server=localhost;" +
				"UID=root;" +
				"PWD=;" +
				"Database=inventory_db";
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
