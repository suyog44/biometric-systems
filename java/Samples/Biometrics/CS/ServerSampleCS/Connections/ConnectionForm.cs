using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Samples.Code;

namespace Neurotec.Samples.Connections
{
	public partial class ConnectionForm : Form
	{
		#region Public constructor

		public ConnectionForm()
		{
			InitializeComponent();
			LoadSettings();
		}

		#endregion

		#region Private fields

		private DatabaseConnection _db;

		#endregion Private fields

		#region Public properties

		public string Server
		{
			get { return tbMMAServer.Text; }
			set { tbMMAServer.Text = value; }
		}

		public int ClientPort
		{
			get { return (int)nudPort.Value; }
			set { nudPort.Value = value; }
		}

		public int AdminPort
		{
			get { return (int)nudAdminPort.Value; }
			set { nudAdminPort.Value = value; }
		}

		public string Password
		{
			get { return mtbPasword.Text; }
			set { mtbPasword.Text = value; }
		}

		public string UserName
		{
			get { return tbUsername.Text; }
			set { tbUsername.Text = value; }
		}

		public bool UseDb
		{
			get { return rbDatabase.Checked; }
			set
			{
				if (value) rbDatabase.Checked = true;
				else rbDirectory.Checked = true;
				ToggleRadioButtons();
			}
		}

		public string TemplateDir
		{
			get { return tbPath.Text; }
			set { tbPath.Text = value; }
		}

		public string DbServer
		{
			get { return tbDBServer.Text; }
			set { tbDBServer.Text = value; }
		}

		public string DbTable
		{
			get { return cbTable.Text; }
		}

		public string DbUser
		{
			get { return tbDBUser.Text; }
			set { tbDBUser.Text = value; }
		}

		public string DbPassword
		{
			get { return tbDBPassword.Text; }
			set { tbDBPassword.Text = value; }
		}

		public bool IsAccelerator
		{
			get { return chbIsAccelerator.Checked; }
			set { chbIsAccelerator.Checked = value; }
		}

		public DatabaseConnection Db
		{
			get { return _db; }
			set
			{
				_db = value;
				if (value == null) return;

				DbServer = value.Server;
				DbUser = value.User;
				DbPassword = value.Password;
				cbTable.Items.Clear();
				cbTable.Items.AddRange(value.GetTables());
				cbTable.SelectedItem = value.Table;
				ListCollumns();
				cbId.SelectedItem = value.IdColumn;
				cbTemplate.SelectedItem = value.TemplateColumn;
			}
		}

		#endregion

		#region Private methods

		private void LoadSettings()
		{
			UseDb = SettingsAccesor.UseDb;
			DbServer = SettingsAccesor.DbServer;
			DbUser = SettingsAccesor.DbUser;
			DbPassword = SettingsAccesor.DbPassword;
			Server = SettingsAccesor.Server;
			ClientPort = SettingsAccesor.ClientPort;
			AdminPort = SettingsAccesor.AdminPort;
			UserName = SettingsAccesor.UserName;
			Password = SettingsAccesor.Password;
			TemplateDir = SettingsAccesor.TemplateDir;
			IsAccelerator = SettingsAccesor.IsAccelerator;
		}

		private void ToggleRadioButtons()
		{
			gbDatabase.Enabled = rbDatabase.Checked;
			btnBrowse.Enabled = tbPath.Enabled = rbDirectory.Checked;
		}

		private void SaveSettings()
		{
			SettingsAccesor.UseDb = UseDb;
			SettingsAccesor.DbServer = DbServer;
			string value = DbTable;
			if (!string.IsNullOrEmpty(value))
				SettingsAccesor.DbTable = value;
			SettingsAccesor.DbUser = DbUser;
			SettingsAccesor.DbPassword = DbPassword;
			value = cbId.SelectedItem as string;
			if (!string.IsNullOrEmpty(value))
				SettingsAccesor.IdColumn = value;
			value = cbTemplate.SelectedItem as string;
			if (!string.IsNullOrEmpty(value))
				SettingsAccesor.TemplateColumn = value;
			SettingsAccesor.Password = Password;
			SettingsAccesor.UserName = UserName;
			SettingsAccesor.ClientPort = ClientPort;
			SettingsAccesor.AdminPort = AdminPort;
			SettingsAccesor.Server = Server;
			SettingsAccesor.TemplateDir = TemplateDir;
			SettingsAccesor.IsAccelerator = IsAccelerator;
			SettingsAccesor.SaveSettings();
		}

		private void DatabaseSettingsReset()
		{
			Db = null;
			tbDBServer.Text = @"mysql_dsn";
			tbDBUser.Text = @"user";
			tbDBPassword.Text = @"pass";
			try
			{
				var conn = new DatabaseConnection();
				string hostValue = conn.GetConfigValue("Host");
				if (hostValue != null)
				{
					tbDBServer.Text = hostValue;
				}
				string userValue = conn.GetConfigValue("User");
				if (userValue != null)
				{
					tbDBUser.Text = userValue;
				}
				string passwordValue = conn.GetConfigValue("Password");
				if (passwordValue != null)
				{
					tbDBPassword.Text = passwordValue;
				}
			}
			catch { }
			cbTable.Items.Clear();
			cbTemplate.Items.Clear();
			cbId.Items.Clear();
		}

		private void ListCollumns()
		{
			cbId.Items.Clear();
			cbTemplate.Items.Clear();
			if (cbTable.SelectedItem != null)
			{
				string[] columns = Db.GetColumns(DbTable);
				if (columns != null)
				{
					cbId.Items.AddRange(columns);
					cbTemplate.Items.AddRange(columns);
				}
			}
		}

		#endregion

		#region Private form events

		private void BtnConnectClick(object sender, EventArgs e)
		{
			if (Db != null)
			{
				if (Db.Server == DbServer && Db.User == DbUser &&
					Db.Password == DbPassword)
					return;
			}
			_db = new DatabaseConnection();

			try
			{
				if (_db == null) throw new ApplicationException("Specified db is null");

				_db.Server = DbServer;
				_db.User = DbUser;
				_db.Password = DbPassword;
				_db.CheckConnection();
				cbTable.Items.Clear();
				string[] tables = _db.GetTables();
				if (tables != null)
				{
					cbTable.Items.AddRange(tables);
					string table = SettingsAccesor.DbTable;
					if (cbTable.Items.Contains(table))
					{
						cbTable.SelectedItem = table;
					}
					else if (tables.Length > 0)
						cbTable.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				_db = null;
				Utilities.ShowError("Failed to connect to db. Reason: {0}", ex);
			}
		}

		private void CbTableSelectedIndexChanged(object sender, EventArgs e)
		{
			if (Db != null) Db.Table = cbTable.Text;
			ListCollumns();

			string collumn = SettingsAccesor.IdColumn;
			if (cbId.Items.Contains(collumn))
				cbId.SelectedItem = collumn;
			else if (cbId.Items.Count > 0)
				cbId.SelectedIndex = 0;

			collumn = SettingsAccesor.TemplateColumn;
			if (cbTemplate.Items.Contains(collumn))
				cbTemplate.SelectedItem = collumn;
			else if (cbTemplate.Items.Count > 0)
				cbTemplate.SelectedIndex = 0;
		}

		private void BtnResetClick(object sender, EventArgs e)
		{
			DatabaseSettingsReset();
		}

		private void ChbIsAcceleratorCheckedChanged(object sender, EventArgs e)
		{
			tbUsername.Enabled = chbIsAccelerator.Checked;
			mtbPasword.Enabled = chbIsAccelerator.Checked;
		}

		private void BtnCheckConnectionClick(object sender, EventArgs e)
		{
			if (AcceleratorConnection.CheckConnection(tbMMAServer.Text, (int)nudAdminPort.Value))
			{
				Utilities.ShowInformation("Connection test successful");
			}
			else
			{
				Utilities.ShowError("Connection failed");
			}
		}

		private void BtnBrowseClick(object sender, EventArgs e)
		{
			string path = TemplateDir;
			if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
				folderBrowserDialog.SelectedPath = path;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
				TemplateDir = folderBrowserDialog.SelectedPath;
		}

		private void RbDirectoryCheckedChanged(object sender, EventArgs e)
		{
			ToggleRadioButtons();
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			if (UseDb)
			{
				if (Db == null)
				{
					Utilities.ShowInformation("Connection with database must be established before proceeding");
					return;
				}
				Db.Table = cbTable.Text;
				Db.TemplateColumn = cbTemplate.Text;
				Db.IdColumn = cbId.Text;
			}
			else if (string.IsNullOrEmpty(TemplateDir) || !Directory.Exists(TemplateDir))
			{
				Utilities.ShowInformation("Specified directory doesn't exists");
				return;
			}

			SaveSettings();
			DialogResult = DialogResult.OK;
		}

		#endregion
	}
}
