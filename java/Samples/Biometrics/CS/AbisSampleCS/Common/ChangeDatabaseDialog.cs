using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class ChangeDatabaseDialog : Form
	{
		#region Public constructor

		public ChangeDatabaseDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient { get; set; }
		public bool ClearDatabase { get { return chbClear.Checked && !rbRemoteServer.Checked; } }

		#endregion

		#region Private methods

		private void ApplyValues()
		{
			ConnectionType type = GetSelected();
			BiometricClient.DatabaseConnection = null;
			BiometricClient.RemoteConnections.Clear();
			switch (type)
			{
				case ConnectionType.SQLiteDatabase:
					{
						string dbPath = Path.Combine(Utilities.GetUserLocalDataDir("NeurotechnologySample"), "BiometricsV50.db");
						BiometricClient.SetDatabaseConnectionToSQLite(dbPath);
						break;
					}
				case ConnectionType.OdbcDatabase:
					{
						BiometricClient.SetDatabaseConnectionToOdbc(tbConnectionString.Text, cbTableName.Text);
						break;
					}
				case ConnectionType.RemoteMatchingServer:
					{
						int port = Convert.ToInt32(nudClientPort.Value);
						int adminPort = Convert.ToInt32(nudAdminPort.Value);
						string host = tbHostName.Text;
						BiometricClient.RemoteConnections.AddToCluster(host, port, adminPort);
						break;
					}
				default:
					break;
			}

			SampleDbSchema selected = (SampleDbSchema)cbSchema.SelectedItem;
			if (!selected.IsEmpty)
			{
				BiometricClient.BiographicDataSchema = selected.BiographicData;
				if (selected.CustomData != null)
					BiometricClient.CustomDataSchema = selected.CustomData;
			}
		}

		private ConnectionType GetSelected()
		{
			return rbSQLite.Checked ? ConnectionType.SQLiteDatabase : (rbOdbc.Checked ? ConnectionType.OdbcDatabase : ConnectionType.RemoteMatchingServer);
		}

		private void TogleRadioButtons()
		{
			ConnectionType selected = GetSelected();
			btnListTables.Enabled = cbTableName.Enabled = tbConnectionString.Enabled = selected == ConnectionType.OdbcDatabase;
			tbHostName.Enabled = nudClientPort.Enabled = nudAdminPort.Enabled = selected == ConnectionType.RemoteMatchingServer;
			chbClear.Enabled = selected == ConnectionType.OdbcDatabase || selected == ConnectionType.SQLiteDatabase;
			chbClear.Checked = chbClear.Checked && chbClear.Enabled;
			cbLocalOperations.Enabled = selected == ConnectionType.RemoteMatchingServer;
		}

		#endregion

		#region Private events

		private void BtnOKClick(object sender, EventArgs e)
		{
			SampleDbSchema schema = (SampleDbSchema)cbSchema.SelectedItem;
			if (rbRemoteServer.Checked)
			{
				if (schema.HasCustomData)
				{
					StringBuilder sb = new StringBuilder();
					int count = schema.CustomData.Elements.Count;
					for (int i = 0; i < count; i++)
					{
						sb.Append(schema.CustomData.Elements[i].Name);
						if (i + 1 != count) sb.Append(", ");
					}
					string msg = "Current schema contains custom data (Columns: {0}). Only biographic data is supported with remote matching server. Please select different schema or edit current one.";
					Utilities.ShowInformation(string.Format(msg, sb.ToString()));
					return;
				}
			}

			if (!rbSQLite.Checked && SettingsManager.WarnHasSchema && !schema.IsEmpty)
			{
				string msg = "Please note that for biometric client will not automaticly create columns specified in database schema for odbc connection or matching server."
							+ " User must ensure that columns specified in schema exists. Continue anyway?";
				if (Utilities.ShowQuestion(this, msg))
				{
					SettingsManager.WarnHasSchema = false;
				}
				else
				{
					return;
				}
			}

			try
			{
				BiometricClient = new NBiometricClient() { UseDeviceManager = true };
				ApplyValues();
				bool isRemoteServerChecked = rbRemoteServer.Checked;
				int operationsIndex = cbLocalOperations.SelectedIndex;

				LongActionDialog.ShowDialog(this, "Initializing biometric client ... ", new Action<NBiometricClient>(biometricClient =>
				{
					biometricClient.Initialize();

					if (isRemoteServerChecked)
					{
						NBiometricOperations localOperations = NBiometricOperations.None;
						NBiometricOperations[] operations =
						{
							NBiometricOperations.None,
							NBiometricOperations.Detect,
							NBiometricOperations.DetectSegments,
							NBiometricOperations.Segment,
							NBiometricOperations.AssessQuality,
							NBiometricOperations.CreateTemplate,
						};
						for (int i = 0; i <= operationsIndex; i++)
						{
							localOperations |= operations[i];
						}
						biometricClient.LocalOperations = localOperations;
					}

					SettingsManager.LoadSettings(biometricClient);
					SettingsManager.LoadPreferedDevices(biometricClient);
					if (ClearDatabase)
					{
						NBiometricStatus status = biometricClient.Clear();
						if (status != NBiometricStatus.Ok)
						{
							Utilities.ShowInformation("Failed to clear database: {0}", status);
						}
					}
				}), BiometricClient);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
				BiometricClient.Dispose();
				BiometricClient = null;
				return;
			}

			SettingsManager.RemoteServerAddress = tbHostName.Text;
			SettingsManager.RemoteServerAdminPort = Convert.ToInt32(nudAdminPort.Value);
			SettingsManager.RemoteServerPort = Convert.ToInt32(nudClientPort.Value);
			SettingsManager.TableName = cbTableName.Text;
			SettingsManager.OdbcConnectionString = tbConnectionString.Text;
			SettingsManager.ConnectionType = GetSelected();

			int index = cbSchema.SelectedIndex;
			SettingsManager.CurrentSchemaIndex = index + 1 != cbSchema.Items.Count ? index : -1;
			SettingsManager.LocalOperationsIndex = cbLocalOperations.SelectedIndex;

			DialogResult = DialogResult.OK;
		}

		private void ChangeDatabaseDialogLoad(object sender, EventArgs e)
		{
			foreach (var item in SettingsManager.Schemas)
			{
				cbSchema.Items.Add(item);
			}
			cbSchema.Items.Add(SampleDbSchema.Empty);
			cbSchema.SelectedIndex = SettingsManager.CurrentSchemaIndex;
			if (cbSchema.SelectedIndex == -1) cbSchema.SelectedItem = SampleDbSchema.Empty;

			switch (SettingsManager.ConnectionType)
			{
				case ConnectionType.SQLiteDatabase:
					rbSQLite.Checked = true;
					break;
				case ConnectionType.OdbcDatabase:
					rbOdbc.Checked = true;
					break;
				case ConnectionType.RemoteMatchingServer:
					rbRemoteServer.Checked = true;
					break;
				default:
					break;
			}
			tbConnectionString.Text = SettingsManager.OdbcConnectionString;
			cbTableName.Text = SettingsManager.TableName;
			tbHostName.Text = SettingsManager.RemoteServerAddress;
			nudClientPort.Value = SettingsManager.RemoteServerPort;
			nudAdminPort.Value = SettingsManager.RemoteServerAdminPort;
			cbLocalOperations.SelectedIndex = SettingsManager.LocalOperationsIndex;
		}

		private void ChangeDatabaseDialogFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
			{
				if (BiometricClient != null) BiometricClient.Dispose();
				BiometricClient = null;
			}
		}

		private void BtnListTablesClick(object sender, EventArgs e)
		{
			OdbcConnection sqlConnection = null;
			try
			{
				sqlConnection = new OdbcConnection(tbConnectionString.Text);
				sqlConnection.Open();
				using (DataTable table = sqlConnection.GetSchema(OdbcMetaDataCollectionNames.Tables))
				{
					string[] names = new string[table.Rows.Count];
					for (int i = 0; i < names.Length; i++)
					{
						DataRow row = table.Rows[i];
						names[i] = row["TABLE_NAME"].ToString();
					}

					cbTableName.Items.Clear();
					foreach (var item in names)
					{
						cbTableName.Items.Add(item);
					}
					if (cbTableName.Items.Count > 0) cbTableName.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				if (sqlConnection != null)
				{
					sqlConnection.Close();
				}
			}
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			if (rb != null && rb.Checked)
			{
				TogleRadioButtons();
				if (rb != rbSQLite && !((SampleDbSchema)cbSchema.SelectedItem).IsEmpty)
				{
					toolTip.Show("Please make sure database schema is correct for current database or remote matching server", lblDbSchema);
				}
			}
		}

		private void BtnEditClick(object sender, EventArgs e)
		{
			SchemaBuilderForm builderForm = new SchemaBuilderForm();
			SampleDbSchema[] schemas = SettingsManager.Schemas.ToArray();
			SampleDbSchema current = (SampleDbSchema)cbSchema.SelectedItem;
			builderForm.Schema = current;
			if (current.IsEmpty) return;
			if (builderForm.ShowDialog() == DialogResult.OK)
			{
				current = builderForm.Schema;
				cbSchema.Items[cbSchema.SelectedIndex] = current;
				schemas[cbSchema.SelectedIndex] = current;
				SettingsManager.Schemas = schemas;
			}
		}

		private void CbSchemaSelectedIndexChanged(object sender, EventArgs e)
		{
			btnEdit.Enabled = !((SampleDbSchema)cbSchema.SelectedItem).IsEmpty;
		}

		#endregion
	}
}
