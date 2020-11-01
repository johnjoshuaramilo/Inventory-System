using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase;

namespace Inventory_System
{
	public partial class LogIn : Form
	{
		DatabaseManager db = new DatabaseManager();
		#region dragable
		private void frmDrag(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}

		}
		Point lastPoint;
		private void mseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void lblDrag(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}

		private void lblDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}
		#endregion
		string ftime = "";

		public string User
		{
			get { return ftime; }
		}

		public LogIn()
		{
			InitializeComponent();
			imgExit.Click += ImgExit_Click;
			this.Load += LogIn_Load;
			txtUserName.Enter += TxtUser_Enter;
			txtUserName.Leave += TxtUser_Leave;
			txtUserName.KeyDown += TxtUserName_KeyDown;
			txtPassword.Enter += TxtPassword_Enter;
			txtPassword.Leave += TxtPassword_Leave;
			txtPassword.KeyDown += TxtPassword_KeyDown;
			btnLogIn.Click += BtnLogIn_Click;
		}

		private void BtnLogIn_Click(object sender, EventArgs e)
		{
			string access = "";
			string username = txtUserName.Text;
			string sql = "";
			if (txtUserName.Text.Equals("admin") && txtPassword.Text.Equals("companykoto"))
			{
				sql = "INSERT INTO tblloghistory(fUserName,fTimeIn) VALUES " +
					"('" + txtUserName.Text +
					"', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +
					"') ";
					db.Connect();
					db.Insert(sql);
				ftime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
				this.Hide();
				MenuForm frm = new MenuForm(this, access= "admin", username);
				frm.Show();
			}
			else
			{
				db.Connect();
				db.Display("SELECT * FROM tblusers WHERE fUserName = '" + txtUserName.Text + "' AND fPassword = '" + txtPassword.Text + "'");
				if (db.Result.Rows.Count == 1)
				{
					sql = "INSERT INTO tblloghistory(fUserName,fTimeIn) VALUES " +
				"('" + txtUserName.Text +
				"', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +
				"') ";
					db.Connect();
					db.Insert(sql);
					ftime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
					this.Hide();

					db.Connect();
					db.Display("SELECT fAccess FROM tblusers WHERE fUserName = '" + txtUserName.Text + "' AND fPassword = '" + txtPassword.Text + "'");
					DataTable result = db.Result;
					
					if (result.Rows[0][0].ToString() == "admin")
					{
						access = "admin";
						MenuForm frm = new MenuForm(this, access, username);
						frm.Show();
					}
					else if (result.Rows[0][0].ToString() == "regular")
						{
						access = "regular";
						MenuForm frm = new MenuForm(this, access, username);
							frm.Show();
						}
				}

				else
				{
					MessageBox.Show("Invalid Username or Password", "Login Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
			
			ClearFields();
		}

		void ClearFields()
		{
			txtUserName.Text = "";
			txtPassword.Text = "";
		}


		private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnLogIn.PerformClick();
			}
		}

		private void TxtPassword_Leave(object sender, EventArgs e)
		{
			if (txtPassword.Text == String.Empty)
			{
				txtPassword.PasswordChar = '\0';
				txtPassword.Text = "PASSWORD";
				txtPassword.ForeColor = Color.Gray;
			}
		}

		private void TxtPassword_Enter(object sender, EventArgs e)
		{
			if (txtPassword.Text == "PASSWORD")
			{
				txtPassword.PasswordChar = '*';
				txtPassword.Text = "";
				txtPassword.ForeColor = Color.Black;
			}
		}

		private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnLogIn.PerformClick();
			}
		}

		private void TxtUser_Leave(object sender, EventArgs e)
		{
			if (txtUserName.Text == String.Empty)
			{
				txtUserName.Text = "USERNAME";
				txtUserName.ForeColor = Color.Gray;
			}
		}

		private void TxtUser_Enter(object sender, EventArgs e)
		{
			if (txtUserName.Text == "USERNAME")
			{
				txtUserName.Text = "";
				txtUserName.ForeColor = Color.Black;
			}
		}

		private void LogIn_Load(object sender, EventArgs e)
		{
			txtUserName.Text = "USERNAME";
			txtUserName.ForeColor = Color.Gray;
			txtPassword.Text = "PASSWORD";
			txtPassword.ForeColor = Color.Gray;
		}

		private void ImgExit_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}


	}
}
