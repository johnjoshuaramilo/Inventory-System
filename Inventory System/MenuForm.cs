using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PagedList;
using DataBase;


namespace Inventory_System
{
	public partial class MenuForm : Form
	{
		#region dragable
		Point lastPoint;
		private void dragDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void dragMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}
		#endregion

		DatabaseManager db = new DatabaseManager();
		DateTime now = DateTime.Now;
		LogIn parent;
		bool update = false;
		bool suppupdate = false;
		bool purchupdate = false;
		string supname = "";
		string tagnum = "";
		string fID = "";
		string sql = "";
		string currentuser = "";
		string kapasidad = "";
		string purchsup = "";
		string DOP = "";
		public MenuForm(LogIn p, string access, string user)
		{
			parent = p;
			currentuser = user;
			kapasidad = access;
			InitializeComponent();
			imgExit.Click += ImgExit_Click;
			this.Load += MenuForm_Load;
			btnExit.Click += BtnExit_Click;
			btnLogs.Click += BtnLogs_Click;
			btnLogHistory.Click += BtnLogHistory_Click;
			btnItem.Click += BtnItem_Click;
			btnSave.Click += BtnSave_Click;
			btnUpdate.Click += BtnUpdate_Click;
			btnDelete.Click += BtnDelete_Click;
			btnClear.Click += BtnClear_Click;
			btnItemLog.Click += BtnItemLog_Click;
			btnChange.Click += BtnChange_Click;
			btnSupplier.Click += BtnSupplier_Click;
			btnHome.Click += BtnHome_Click;
			btnAccount.Click += BtnAccount_Click;
			btnHelp.Click += BtnHelp_Click;
			txtSearch.KeyDown += TxtSearch_KeyDown;
			txtAccSearch.KeyDown += TxtAccSearch_KeyDown;
			btnASave.Click += BtnASave_Click;
			btnAUpdate.Click += BtnAUpdate_Click;
			btnAClear.Click += BtnAClear_Click;
			btnADelete.Click += BtnADelete_Click;
			btnAccLog.Click += BtnAccLog_Click;
			btnNewSupp.Click += BtnNewSupp_Click;
			btnUpdSupp.Click += BtnUpdSupp_Click;
			btnSuppDelete.Click += BtnSuppDelete_Click;
			btnSupSave.Click += BtnSupSave_Click;
			btnSuppLog.Click += BtnSuppLog_Click;
			btnPurchLog.Click += BtnPurchLog_Click;
			btnNewPurch.Click += BtnNewPurch_Click;
			btnPurchSave.Click += BtnPurchSave_Click;
			btnUpdPurch.Click += BtnUpdPurch_Click;
			btnDeletePurch.Click += BtnDeletePurch_Click;
			btnPrint.Click += BtnPrint_Click;
			btnExport.Click += BtnExport_Click;
			txtSSearch.KeyDown += TxtSSearch_KeyDown;
			txtPSearch.KeyDown += TxtPSearch_KeyDown;
			timer1.Start();
		}

		





		#region Menu Selection Panel
		private void BtnChange_Click(object sender, EventArgs e)
		{
			string sql = "UPDATE tblloghistory SET fTimeOut = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") + "'" +
					", fAccess = '" + kapasidad + "'" +
					"WHERE fTimeIn = '" + parent.User + "'";
			db.Connect();
			db.Insert(sql);
			this.Close();
			LogIn frm = new LogIn();
			frm.Show();
		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			string sql = "UPDATE tblloghistory SET fTimeOut = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") + "'" +
					", fAccess = '" + kapasidad + "'" +
					"WHERE fTimeIn = '" + parent.User + "'";
			db.Connect();
			db.Insert(sql);
			Environment.Exit(0);
		}

		private void BtnItem_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(0);
			UpdateItemGrid("SELECT fTagNumber AS 'Tag Number', fID AS 'Stock Number', fItemName AS 'Item Name', fLifeSpan AS 'Life Span', fAcquired AS 'Date Aquired', fCost AS 'Purchase Cost'," +
				" fValue AS 'Depreciated Value', fStatus AS 'Status' FROM tblitems");
		}

		private void BtnSupplier_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(1);
			UpdateSupplierGrid("SELECT fSupplierName AS 'Supplier Name', fTelNumber AS 'Telephone Number', fContactName AS 'Contact Name', fMobileNumber AS 'Mobile Number'," +
				"fEmail AS 'Email' FROM tblsupplier");
			UpdatePurchaseGrid("SELECT fDate AS 'Date of Purchase', fSupplier AS 'Supplier NAme', fTotal AS 'Total Quantity', fCost AS 'Total Cost' FROM tblpurchase");

		}

		private void BtnLogs_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(2);
		}

		private void BtnHome_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(3);
		}

		private void BtnAccount_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(4);
			UpdateAccountGrid("SELECT fID AS 'ID Number', fUserName AS 'User Name', fAccess AS 'Access Level', fFirstName AS 'First Name', fLastName AS 'Last Name', " +
				" fDepartment AS 'Department' FROM tblusers");
		}

		private void BtnHelp_Click(object sender, EventArgs e)
		{
			tabControl1.SelectTab(5);
		}
		#endregion

		#region Item Menu

		private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			sql = "SELECT fTagNumber AS 'Tag Number', fID AS 'Stock Number', fItemName AS 'Item Name', fLifeSpan AS 'Life Span', fAcquired AS 'Date Aquired', fCost AS 'Purchase Cost'," +
				" fValue AS 'Depreciated Value', fStatus AS 'Status' FROM tblitems WHERE " +
				"fTagNumber LIKE '%" + txtSearch.Text + "%' OR " +
				"fID LIKE '%" + txtSearch.Text + "%' OR " +
				"fItemName LIKE '%" + txtSearch.Text + "%' OR " +
				"fLifeSpan LIKE '%" + txtSearch.Text + "%' OR " +
				"fAcquired LIKE '%" + txtSearch.Text + "%' OR " +
				"fCost LIKE '%" + txtSearch.Text + "%' OR " +
				"fValue LIKE '%" + txtSearch.Text + "%' OR " +
				"fStatus LIKE '%" + txtSearch.Text + "%'";
			UpdateItemGrid(sql);
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			ClearItemFields();
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView1.SelectedRows[0].Index;
				if (row < 0) return;
				tagnum = dataGridView1.Rows[row].Cells[0].Value.ToString();
				sql = "SELECT * FROM tblitems WHERE fTagNumber = '" + tagnum + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;
				string itemname = result.Rows[0][2].ToString();

				sql = "DELETE FROM tblitems WHERE fTagNumber = '" + tagnum + "'";
				db.Connect();
				db.Insert(sql);

				sql = "INSERT INTO tblitemlog(fItem,fDate, fAction) VALUES " +
					"('" + itemname +
					"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
					"', '" + "DELETE" +
					"') ";
				db.Connect();
				db.Insert(sql);


				UpdateItemGrid("SELECT fTagNumber AS 'Tag Number', fID AS 'Stock Number', fItemName AS 'Item Name', fLifeSpan AS 'Life Span', fAcquired AS 'Date Aquired', fCost AS 'Purchase Cost'," +
				" fValue AS 'Depreciated Value', fStatus AS 'Status' FROM tblitems");

			}
		}

		private void BtnUpdate_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView1.SelectedRows[0].Index;
				if (row < 0) return;
				tagnum = dataGridView1.Rows[row].Cells[0].Value.ToString();
				update = true;
				txtTagNumber.Enabled = false;
				txtItemName.Enabled = false;
				txtLifeSpan.Enabled = false;
				txtAcquired.Enabled = false;
				txtCost.Enabled = false;
				txtValue.Enabled = false;

				sql = "SELECT * FROM tblitems WHERE fTagNumber = '" + tagnum + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				txtID.Text = result.Rows[0][0].ToString();
				txtTagNumber.Text = result.Rows[0][1].ToString();
				txtItemName.Text = result.Rows[0][2].ToString();
				txtLifeSpan.Text = result.Rows[0][3].ToString();
				txtAcquired.Text = result.Rows[0][4].ToString();
				txtCost.Text = result.Rows[0][5].ToString();
				txtValue.Text = result.Rows[0][6].ToString();
				comboStatus.Text = result.Rows[0][7].ToString();
			}
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			string action = "";
			if(string.IsNullOrEmpty(txtTagNumber.Text)|| string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtLifeSpan.Text) || string.IsNullOrEmpty(txtAcquired.Text) || string.IsNullOrEmpty(txtCost.Text) || string.IsNullOrEmpty(txtValue.Text) || string.IsNullOrEmpty(comboStatus.Text))
			{
				MessageBox.Show("A field is empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				if (update == false)
				{

					sql = "INSERT INTO tblitems(fTagNumber,fItemName, fLifeSpan, fAcquired, fCost, fValue, fStatus) VALUES " +
								"('" + txtTagNumber.Text +
								"', '" + txtItemName.Text +
								"', '" + txtLifeSpan.Text +
								"', '" + txtAcquired.Text +
								"', '" + txtCost.Text +
								"', '" + txtValue.Text +
								"', '" + comboStatus.Text +
								"') ";
					action = "ADD";
				}
				else if (update == true)
				{
					action = "UPDATE";
					sql = "UPDATE tblitems SET fStatus = '" + comboStatus.Text + "'" +
						" WHERE fTagNumber = '" + tagnum + "'";


					txtTagNumber.Enabled = true;
					txtItemName.Enabled = true;
					txtLifeSpan.Enabled = true;
					txtAcquired.Enabled = true;
					txtCost.Enabled = true;
					txtValue.Enabled = true;
				}

				db.Connect();
				db.Insert(sql);


				sql = "INSERT INTO tblitemlog(fItem,fDate, fAction) VALUES " +
						"('" + txtItemName.Text +
						"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
						"', '" + action +
						"') ";
				db.Connect();
				db.Insert(sql);

				UpdateItemGrid("SELECT fTagNumber AS 'Tag Number', fID AS 'Stock Number', fItemName AS 'Item Name', fLifeSpan AS 'Life Span', fAcquired AS 'Date Aquired', fCost AS 'Purchase Cost'," +
					" fValue AS 'Depreciated Value', fStatus AS 'Status' FROM tblitems");
				ClearItemFields();
			}
		}

		#endregion

		#region Supplier

		private void TxtSSearch_KeyDown(object sender, KeyEventArgs e)
		{
			sql = "SELECT fSupplierName AS 'Supplier Name', fTelNumber AS 'Telephone Number',  fContactName AS 'Contact Name'," +
				" fMobileNumber AS 'Mobile Number', fEmail AS 'Email' FROM tblsupplier WHERE " +
				"fSupplierName LIKE '%" + txtSSearch.Text + "%' OR " +
				"fTelNumber LIKE '%" + txtSSearch.Text + "%' OR " +
				"fContactName LIKE '%" + txtSSearch.Text + "%' OR " +
				"fMobileNumber LIKE '%" + txtSSearch.Text + "%' OR " +
				"fEmail LIKE '%" + txtSSearch.Text + "%'";
			UpdateSupplierGrid(sql);
		}

		private void BtnSupSave_Click(object sender, EventArgs e)
		{
			string action = "";
			if (string.IsNullOrEmpty(txtSupName.Text) || string.IsNullOrEmpty(txtTelNum.Text) || string.IsNullOrEmpty(txtContactName.Text) || string.IsNullOrEmpty(txtMobile.Text) || string.IsNullOrEmpty(txtEmail.Text))
			{
				MessageBox.Show("A field is empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				if (suppupdate == false)
				{

					sql = "INSERT INTO tblsupplier(fSupplierName,fTelNumber, fContactName, fMobileNumber, fEmail) VALUES " +
								"('" + txtSupName.Text +
								"', '" + txtTelNum.Text +
								"', '" + txtContactName.Text +
								"', '" + txtMobile.Text +
								"', '" + txtEmail.Text +
								"') ";
					action = "ADD";
				}
				else if (suppupdate == true)
				{
					action = "UPDATE";
					sql = "UPDATE tblsupplier SET fTelNumber = '" + txtTelNum.Text + "'" +
						", fContactName = '" + txtContactName.Text + "'" +
						", fMobileNumber = '" + txtMobile.Text + "'" +
						", fEmail = '" + txtEmail.Text + "'" +
						" WHERE fSupplierName = '" + supname + "'";
					txtSupName.Enabled = true;
				}

				db.Connect();
				db.Insert(sql);


				sql = "INSERT INTO tblsupplierlog(fSupplier,fDate, fAction) VALUES " +
						"('" + txtSupName.Text +
						"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
						"', '" + action +
						"') ";
				db.Connect();
				db.Insert(sql);

				UpdateSupplierGrid("SELECT fSupplierName AS 'Supplier Name', fTelNumber AS 'Telephone Number', fContactName AS 'Contact Name', fMobileNumber AS 'Mobile Number'," +
				"fEmail AS 'Email' FROM tblsupplier");
				ClearSupplierFields();
			}
		}


		private void BtnSuppDelete_Click(object sender, EventArgs e)
		{
			if (dataGridView5.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView5.SelectedRows[0].Index;
				if (row < 0) return;
				supname = dataGridView5.Rows[row].Cells[0].Value.ToString();
				sql = "SELECT * FROM tblsupplier WHERE fSupplierName = '" + supname + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				sql = "DELETE FROM tblsupplier WHERE fSupplierName = '" + supname + "'";
				db.Connect();
				db.Insert(sql);

				sql = "INSERT INTO tblsupplierlog(fSupplier,fDate, fAction) VALUES " +
					"('" + supname +
					"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
					"', '" + "DELETE" +
					"') ";
				db.Connect();
				db.Insert(sql);


				UpdateSupplierGrid("SELECT fSupplierName AS 'Supplier Name', fTelNumber AS 'Telephone Number', fContactName AS 'Contact Name', fMobileNumber AS 'Mobile Number'," +
				"fEmail AS 'Email' FROM tblsupplier");

			}
		}


		private void BtnUpdSupp_Click(object sender, EventArgs e)
		{
			if (dataGridView5.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				pnlSupplier.Visible = true;
				int row = dataGridView5.SelectedRows[0].Index;
				if (row < 0) return;
				supname = dataGridView5.Rows[row].Cells[0].Value.ToString();

				suppupdate = true;
				txtSupName.Enabled = false;
				lblSupplier.Text = "Update Supplier";

				sql = "SELECT * FROM tblsupplier WHERE fSupplierName = '" + supname + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				txtSupName.Text = result.Rows[0][1].ToString();
				txtTelNum.Text = result.Rows[0][2].ToString();
				txtContactName.Text = result.Rows[0][3].ToString();
				txtMobile.Text = result.Rows[0][4].ToString();
				txtEmail.Text = result.Rows[0][5].ToString();

			}
		}

		private void BtnNewSupp_Click(object sender, EventArgs e)
		{
			pnlSupplier.Visible = true;
			lblSupplier.Text = "Add Supplier";
			suppupdate = false;

		}
		#endregion

		#region Purchase

		private void TxtPSearch_KeyDown(object sender, KeyEventArgs e)
		{
			sql = "SELECT fDate AS 'Date of Purchase', fSupplier AS 'Purchase Supplier', fTotal AS 'Total Quantity', fCost AS 'Total Cost' FROM tblpurchase WHERE " +
				"fDate LIKE '%" + txtPSearch.Text + "%' OR " +
				"fSupplier LIKE '%" + txtPSearch.Text + "%' OR " +
				"fTotal LIKE '%" + txtPSearch.Text + "%' OR " +
				"fCost LIKE '%" + txtPSearch.Text + "%'";
			UpdatePurchaseGrid(sql);
		}

		private void BtnDeletePurch_Click(object sender, EventArgs e)
		{
			
			if (dataGridView4.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView4.SelectedRows[0].Index;
				if (row < 0) return;
				purchsup = dataGridView4.Rows[row].Cells[1].Value.ToString();
				sql = "SELECT * FROM tblpurchase WHERE fSupplier = '" + purchsup + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				sql = "DELETE FROM tblpurchase WHERE fSupplier = '" + purchsup + "'";
				db.Connect();
				db.Insert(sql);
				string action = "DELETE";
				sql = "INSERT INTO tblpurchaselog(fEmployee,fPurchaseSupplier,fDate, fAction) VALUES " +
						"('" + currentuser +
						"', '" + txtPurchSupp.Text +
						"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
						"', '" + action +
						"') ";
				db.Connect();
				db.Insert(sql);


				UpdatePurchaseGrid("SELECT fDate AS 'Date of Purchase', fSupplier AS 'Purchase Supplier', fTotal AS 'Total Quantity', fCost AS 'Total Cost' FROM tblpurchase");

			}
		}

		private void BtnUpdPurch_Click(object sender, EventArgs e)
		{
			if (dataGridView5.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				pnlPurch.Visible = true;
				int row = dataGridView4.SelectedRows[0].Index;
				if (row < 0) return;
				purchsup = dataGridView4.Rows[row].Cells[1].Value.ToString();

				purchupdate = true;
				lblPurch.Text = "Update Purchase";

				sql = "SELECT * FROM tblpurchase WHERE fSupplier = '" + purchsup + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				txtDOP.Text = result.Rows[0][1].ToString();
				txtPurchSupp.Text = result.Rows[0][2].ToString();
				txtQty.Text = result.Rows[0][3].ToString();
				txtTotalCost.Text = result.Rows[0][4].ToString();

			}
		}


		private void BtnPurchSave_Click(object sender, EventArgs e)
		{
			string action = "";
			if (string.IsNullOrEmpty(txtDOP.Text) || string.IsNullOrEmpty(txtPurchSupp.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtTotalCost.Text))
			{
				MessageBox.Show("A field is empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				if (purchupdate == false)
				{

					sql = "INSERT INTO tblpurchase(fDate,fSupplier, fTotal, fCost) VALUES " +
								"('" + txtDOP.Text +
								"', '" + txtPurchSupp.Text +
								"', '" + txtQty.Text +
								"', '" + txtTotalCost.Text +
								"') ";
					action = "ADD";
				}
				else if (purchupdate == true)
				{
					action = "UPDATE";
					sql = "UPDATE tblpurchase SET fDate = '" + txtDOP.Text + "'" +
						", fSupplier = '" + txtPurchSupp.Text + "'" +
						", fTotal = '" + txtQty.Text + "'" +
						", fCost = '" + txtTotalCost.Text + "'" +
						" WHERE fSupplier = '" + purchsup + "'";
				}

				db.Connect();
				db.Insert(sql);


				sql = "INSERT INTO tblpurchaselog(fEmployee,fPurchaseSupplier,fDate, fAction) VALUES " +
						"('" + currentuser +
						"', '" + txtPurchSupp.Text +
						"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
						"', '" + action +
						"') ";
				db.Connect();
				db.Insert(sql);

				UpdatePurchaseGrid("SELECT fDate AS 'Date of Purchase', fSupplier AS 'Purchase Supplier', fTotal AS 'Total Quantity', fCost AS 'Total Cost' FROM tblpurchase");
				ClearPurchFields();
			}
		}

		private void BtnNewPurch_Click(object sender, EventArgs e)
		{
			pnlPurch.Visible = true;
			lblPurch.Text = "Add Purchase";
			suppupdate = false;
		}
		#endregion

		#region Logs Menu

		private void BtnExport_Click(object sender, EventArgs e)
		{
			exporttopdf(dataGridView2, "File Name");
		}
		Bitmap bmp;
		private void BtnPrint_Click(object sender, EventArgs e)
		{
			int height = dataGridView2.Height;
			dataGridView2.Height = dataGridView2.RowCount * dataGridView2.RowTemplate.Height * 2;
			bmp = new Bitmap(dataGridView2.Width, dataGridView2.Height);
			dataGridView2.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, dataGridView2.Width, dataGridView2.Height));
			dataGridView2.Height = height;
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.PrintPreviewControl.Zoom = 1;
			printPreviewDialog1.ShowDialog();
		}


		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(bmp, 0, 0);
		}

		public void exporttopdf(DataGridView dgw, string filename)
		{
			BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
			PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
			pdftable.DefaultCell.Padding = 3;
			pdftable.WidthPercentage = 100;
			pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
			pdftable.DefaultCell.BorderWidth = 1;


			iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
			//Add Header
			foreach (DataGridViewColumn column in dgw.Columns)
			{
				PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
				cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
				pdftable.AddCell(cell);
			}

			//Add Datarow
			foreach(DataGridViewRow row in dgw.Rows)
			{
				foreach(DataGridViewCell cell in row.Cells)
				{
					pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
				}
			}

			var savefiledialoge = new SaveFileDialog();
			savefiledialoge.FileName = filename;
			savefiledialoge.DefaultExt = ".pdf";
			if (savefiledialoge.ShowDialog()==DialogResult.OK)
			{
				using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
				{
					Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
					PdfWriter.GetInstance(pdfdoc, stream);
					pdfdoc.Open();
					pdfdoc.Add(pdftable);
					pdfdoc.Close();
					stream.Close();
				}
			}
		}


		private void BtnPurchLog_Click(object sender, EventArgs e)
		{
			UpdateLogGrid("SELECT fPurchaseSupplier AS 'Purchase Supplier', fDate AS 'Date', fAction AS 'Action' FROM tblpurchaselog");
		}


		private void BtnSuppLog_Click(object sender, EventArgs e)
		{
			UpdateLogGrid("SELECT fSupplier AS 'Supplier Name', fDate AS 'Date', fAction AS 'Action' FROM tblsupplierlog");
		}

		private void BtnAccLog_Click(object sender, EventArgs e)
		{
			UpdateLogGrid("SELECT fAccount AS 'User Name', fDate AS 'Date', fAction AS 'Action' FROM tblacclog");
		}

		private void BtnItemLog_Click(object sender, EventArgs e)
		{
			UpdateLogGrid("SELECT fItem AS 'Item Name', fDate AS 'Date', fAction AS 'Action' FROM tblitemlog");
		}

		private void BtnLogHistory_Click(object sender, EventArgs e)
		{
			UpdateLogGrid("SELECT fUserName AS 'UserName', fTimeIn AS 'Time In', fTimeOut AS 'Time Out' FROM tblloghistory");
		}
		#endregion

		#region Account Menu

		private void BtnADelete_Click(object sender, EventArgs e)
		{
			if (dataGridView3.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView3.SelectedRows[0].Index;
				if (row < 0) return;
				fID = dataGridView3.Rows[row].Cells[0].Value.ToString();
				sql = "SELECT * FROM tblusers WHERE fID = '" + fID + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;
				string account = result.Rows[0][2].ToString();

				sql = "DELETE FROM tblusers WHERE fID = '" + fID + "'";
				db.Connect();
				db.Insert(sql);

				sql = "INSERT INTO tblacclog(fItem,fDate, fAction) VALUES " +
					"('" + account +
					"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
					"', '" + "DELETE" +
					"') ";
				db.Connect();
				db.Insert(sql);


				UpdateAccountGrid("SELECT fID AS 'ID Number', fUserName AS 'User Name', fAccess AS 'Access Level', fFirstName AS 'First Name', fLastName AS 'Last Name'," +
				" fDepartment AS 'Department' FROM tblusers");

			}
		}


		private void BtnAClear_Click(object sender, EventArgs e)
		{
			ClearAccountFields();
		}

		private void BtnAUpdate_Click(object sender, EventArgs e)
		{
			if (dataGridView3.SelectedRows.Count == 0)
				MessageBox.Show("No Row Selected", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				int row = dataGridView3.SelectedRows[0].Index;
				if (row < 0) return;
				fID = dataGridView3.Rows[row].Cells[0].Value.ToString();

				update = true;
				txtAID.Enabled = false;
				txtUserName.Enabled = false;

				sql = "SELECT * FROM tblusers WHERE fID = '" + fID + "'";
				db.Connect();
				db.Display(sql);
				DataTable result = db.Result;

				txtAID.Text = result.Rows[0][0].ToString();
				txtUserName.Text = result.Rows[0][1].ToString();
				txtPassword.Text = result.Rows[0][2].ToString();
				comboAccess.Text = result.Rows[0][3].ToString();
				txtFName.Text = result.Rows[0][4].ToString();
				txtLName.Text = result.Rows[0][5].ToString();
				comboDepartment.Text = result.Rows[0][6].ToString();

			}
		}

		private void TxtAccSearch_KeyDown(object sender, KeyEventArgs e)
		{
			sql = "SELECT fID AS 'ID Number', fUserName AS 'User Name', fAccess AS 'Access Level', fFirstName AS 'First Name', fLastName AS 'Last Name'," +
				" fDepartment AS 'Department' FROM tblusers WHERE " +
				"fID LIKE '%" + txtAccSearch.Text + "%' OR " +
				"fUserName LIKE '%" + txtAccSearch.Text + "%' OR " +
				"fAccess LIKE '%" + txtAccSearch.Text + "%' OR " +
				"fFirstName LIKE '%" + txtAccSearch.Text + "%' OR " +
				"fLastName LIKE '%" + txtAccSearch.Text + "%' OR " +
				"fDepartment LIKE '%" + txtAccSearch.Text + "%'";
			UpdateAccountGrid(sql);
		}

		private void BtnASave_Click(object sender, EventArgs e)
		{
			string action = "";
			if ( string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(comboAccess.Text) || string.IsNullOrEmpty(txtFName.Text) || string.IsNullOrEmpty(txtLName.Text) || string.IsNullOrEmpty(comboDepartment.Text))
			{
				MessageBox.Show("A field is empty", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				if (update == false)
				{

					sql = "INSERT INTO tblusers(fUserName,fPassword, fAccess, fFirstName, fLastName, fDepartment) VALUES " +
								"('" + txtUserName.Text +
								"', '" + txtPassword.Text +
								"', '" + comboAccess.Text +
								"', '" + txtFName.Text +
								"', '" + txtLName.Text +
								"', '" + comboDepartment.Text +
								"') ";
					action = "ADD";
				}
				else if (update == true)
				{
					action = "UPDATE";
					sql = "UPDATE tblusers SET fPassword = '" + txtPassword.Text + "'" +
						", fAccess = '" + comboAccess.Text + "'" +
						", fFirstName = '" + txtFName.Text + "'" +
						", fLastName = '" + txtLName.Text + "'" +
						", fDepartment = '" + comboDepartment.Text + "'" +
						" WHERE fID = '" + fID + "'";

					txtUserName.Enabled = true;
					
				}

				db.Connect();
				db.Insert(sql);


				sql = "INSERT INTO tblacclog(fAccount,fDate, fAction) VALUES " +
						"('" + txtUserName.Text +
						"', '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
						"', '" + action +
						"') ";
				db.Connect();
				db.Insert(sql);

				UpdateAccountGrid("SELECT fID AS 'ID Number', fUserName AS 'User Name', fAccess AS 'Access Level', fFirstName AS 'First Name', fLastName AS 'Last Name'," +
				" fDepartment AS 'Department' FROM tblusers");
				ClearAccountFields();
			}
		}
			#endregion



		private void MenuForm_Load(object sender, EventArgs e)
		{
			if(kapasidad == "regular")
			{
				btnLogs.Enabled = false;
				btnAccount.Enabled = false;
				panel2.Enabled = false;
				toolStrip2.Enabled = false;
				toolStrip1.Enabled = false;

			}
			else if (kapasidad == "admin")
			{
				btnLogs.Enabled = true;
				btnAccount.Enabled = true;
				panel2.Enabled = true;
				toolStrip2.Enabled = true;
				toolStrip1.Enabled = true;

			}

			db.Connect();
			db.Display("SELECT * FROM tblusers WHERE fUserName = '" + currentuser + "'");
			DataTable result = db.Result;

			if(currentuser == "admin")
			{
				lblCurrentUser.Text = "Company Owner";
				lblDepartment.Text = "Head Office";
			}
			else
			{
				lblCurrentUser.Text = result.Rows[0][4].ToString() + " " + result.Rows[0][5].ToString();
				lblDepartment.Text = result.Rows[0][6].ToString();
			}
			
			lblUserName.Text = currentuser;
			tabControl1.SelectTab(3);
			pnlSupplier.Visible = false;
			pnlPurch.Visible = false;
		}

		public void UpdateItemGrid(string sql)
		{
			db.Connect();
			db.Display(sql);
			dataGridView1.DataSource = db.Result;
		}

		public void UpdateLogGrid(string sql)
		{
			db.Connect();
			db.Display(sql);
			dataGridView2.DataSource = db.Result;
		}

		public void UpdateAccountGrid(string sql)
		{
			db.Connect();
			db.Display(sql);
			dataGridView3.DataSource = db.Result;
		}

		public void UpdateSupplierGrid(string sql)
		{
			db.Connect();
			db.Display(sql);
			dataGridView5.DataSource = db.Result;
		}

		public void UpdatePurchaseGrid(string sql)
		{
			db.Connect();
			db.Display(sql);
			dataGridView4.DataSource = db.Result;
		}

		private void ImgExit_Click(object sender, EventArgs e)
		{
			string sql = "UPDATE tblloghistory SET fTimeOut = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") + "'" +
					", fAccess = '" + kapasidad + "'" +
					"WHERE fTimeIn = '" + parent.User + "'";
			db.Connect();
			db.Insert(sql);
			this.Close();
			LogIn frm = new LogIn();
			frm.Show();
		}

		void ClearItemFields()
		{
			txtItemName.Text = "";
			txtID.Text = "";
			txtAcquired.Text = "";
			txtCost.Text = "";
			txtLifeSpan.Text = "";
			txtSearch.Text = "";
			txtTagNumber.Text = "";
			txtValue.Text = "";
			comboStatus.Text = " ";
			update = false;
		}

		void ClearAccountFields()
		{
			txtID.Text = "";
			txtUserName.Text = "";
			txtPassword.Text = "";
			comboAccess.Text = " ";
			txtFName.Text = "";
			txtLName.Text = "";
			comboDepartment.Text = " ";
			update = false;
		}

		void ClearSupplierFields()
		{
			txtSupName.Text = "";
			txtTelNum.Text = "";
			txtContactName.Text = "";
			txtMobile.Text = " ";
			txtEmail.Text = "";
			suppupdate = false;
			pnlSupplier.Visible = false;
		}


		void ClearPurchFields()
		{
			txtDOP.Text = "";
			txtPurchSupp.Text = "";
			txtQty.Text = "";
			txtTotalCost.Text = " ";
			purchupdate = false;
			pnlPurch.Visible = false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");

		}
	}
}
