namespace Neurotec.Samples.Connections
{
	partial class ConnectionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rbDirectory = new System.Windows.Forms.RadioButton();
			this.rbDatabase = new System.Windows.Forms.RadioButton();
			this.tbPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.gbDatabase = new System.Windows.Forms.GroupBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.cbId = new System.Windows.Forms.ComboBox();
			this.cbTemplate = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cbTable = new System.Windows.Forms.ComboBox();
			this.tbDBPassword = new System.Windows.Forms.MaskedTextBox();
			this.tbDBUser = new System.Windows.Forms.TextBox();
			this.tbDBServer = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.gbAccelerator = new System.Windows.Forms.GroupBox();
			this.btnCheckConnection = new System.Windows.Forms.Button();
			this.chbIsAccelerator = new System.Windows.Forms.CheckBox();
			this.nudAdminPort = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.nudPort = new System.Windows.Forms.NumericUpDown();
			this.tbUsername = new System.Windows.Forms.TextBox();
			this.tbMMAServer = new System.Windows.Forms.TextBox();
			this.mtbPasword = new System.Windows.Forms.MaskedTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.gbTemplates = new System.Windows.Forms.GroupBox();
			this.gbDatabase.SuspendLayout();
			this.gbAccelerator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAdminPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
			this.gbTemplates.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(147, 549);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(228, 549);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// rbDirectory
			// 
			this.rbDirectory.AutoSize = true;
			this.rbDirectory.Location = new System.Drawing.Point(6, 19);
			this.rbDirectory.Name = "rbDirectory";
			this.rbDirectory.Size = new System.Drawing.Size(163, 17);
			this.rbDirectory.TabIndex = 0;
			this.rbDirectory.TabStop = true;
			this.rbDirectory.Text = "Load templates from directory";
			this.rbDirectory.UseVisualStyleBackColor = true;
			this.rbDirectory.CheckedChanged += new System.EventHandler(this.RbDirectoryCheckedChanged);
			// 
			// rbDatabase
			// 
			this.rbDatabase.AutoSize = true;
			this.rbDatabase.Location = new System.Drawing.Point(6, 75);
			this.rbDatabase.Name = "rbDatabase";
			this.rbDatabase.Size = new System.Drawing.Size(167, 17);
			this.rbDatabase.TabIndex = 3;
			this.rbDatabase.TabStop = true;
			this.rbDatabase.Text = "Load templates from database";
			this.rbDatabase.UseVisualStyleBackColor = true;
			// 
			// tbPath
			// 
			this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbPath.Location = new System.Drawing.Point(23, 42);
			this.tbPath.Name = "tbPath";
			this.tbPath.Size = new System.Drawing.Size(229, 20);
			this.tbPath.TabIndex = 1;
			this.tbPath.Text = "c:\\";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(256, 40);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(38, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
			// 
			// gbDatabase
			// 
			this.gbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbDatabase.Controls.Add(this.btnReset);
			this.gbDatabase.Controls.Add(this.btnConnect);
			this.gbDatabase.Controls.Add(this.cbId);
			this.gbDatabase.Controls.Add(this.cbTemplate);
			this.gbDatabase.Controls.Add(this.label7);
			this.gbDatabase.Controls.Add(this.label6);
			this.gbDatabase.Controls.Add(this.cbTable);
			this.gbDatabase.Controls.Add(this.tbDBPassword);
			this.gbDatabase.Controls.Add(this.tbDBUser);
			this.gbDatabase.Controls.Add(this.tbDBServer);
			this.gbDatabase.Controls.Add(this.label5);
			this.gbDatabase.Controls.Add(this.label4);
			this.gbDatabase.Controls.Add(this.label3);
			this.gbDatabase.Controls.Add(this.label1);
			this.gbDatabase.Location = new System.Drawing.Point(6, 98);
			this.gbDatabase.Name = "gbDatabase";
			this.gbDatabase.Size = new System.Drawing.Size(288, 212);
			this.gbDatabase.TabIndex = 4;
			this.gbDatabase.TabStop = false;
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(186, 101);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(93, 23);
			this.btnReset.TabIndex = 10;
			this.btnReset.Text = "&Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(91, 101);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(89, 23);
			this.btnConnect.TabIndex = 9;
			this.btnConnect.Text = "&Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.BtnConnectClick);
			// 
			// cbId
			// 
			this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbId.FormattingEnabled = true;
			this.cbId.Location = new System.Drawing.Point(91, 184);
			this.cbId.Name = "cbId";
			this.cbId.Size = new System.Drawing.Size(190, 21);
			this.cbId.TabIndex = 16;
			// 
			// cbTemplate
			// 
			this.cbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTemplate.FormattingEnabled = true;
			this.cbTemplate.Location = new System.Drawing.Point(91, 157);
			this.cbTemplate.Name = "cbTemplate";
			this.cbTemplate.Size = new System.Drawing.Size(189, 21);
			this.cbTemplate.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(2, 187);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "ID collumn:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(2, 160);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Template collumn:";
			// 
			// cbTable
			// 
			this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTable.FormattingEnabled = true;
			this.cbTable.Location = new System.Drawing.Point(91, 130);
			this.cbTable.Name = "cbTable";
			this.cbTable.Size = new System.Drawing.Size(188, 21);
			this.cbTable.TabIndex = 12;
			this.cbTable.SelectedIndexChanged += new System.EventHandler(this.CbTableSelectedIndexChanged);
			// 
			// tbDBPassword
			// 
			this.tbDBPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDBPassword.Location = new System.Drawing.Point(58, 65);
			this.tbDBPassword.Name = "tbDBPassword";
			this.tbDBPassword.Size = new System.Drawing.Size(223, 20);
			this.tbDBPassword.TabIndex = 8;
			this.tbDBPassword.UseSystemPasswordChar = true;
			// 
			// tbDBUser
			// 
			this.tbDBUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDBUser.Location = new System.Drawing.Point(58, 40);
			this.tbDBUser.Name = "tbDBUser";
			this.tbDBUser.Size = new System.Drawing.Size(223, 20);
			this.tbDBUser.TabIndex = 6;
			// 
			// tbDBServer
			// 
			this.tbDBServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDBServer.Location = new System.Drawing.Point(58, 14);
			this.tbDBServer.Name = "tbDBServer";
			this.tbDBServer.Size = new System.Drawing.Size(223, 20);
			this.tbDBServer.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(2, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Table:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(2, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "PWD:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "UID:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "DSN:";
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.Description = "Select directory with templates";
			// 
			// gbAccelerator
			// 
			this.gbAccelerator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbAccelerator.Controls.Add(this.btnCheckConnection);
			this.gbAccelerator.Controls.Add(this.chbIsAccelerator);
			this.gbAccelerator.Controls.Add(this.nudAdminPort);
			this.gbAccelerator.Controls.Add(this.label12);
			this.gbAccelerator.Controls.Add(this.nudPort);
			this.gbAccelerator.Controls.Add(this.tbUsername);
			this.gbAccelerator.Controls.Add(this.tbMMAServer);
			this.gbAccelerator.Controls.Add(this.mtbPasword);
			this.gbAccelerator.Controls.Add(this.label11);
			this.gbAccelerator.Controls.Add(this.label10);
			this.gbAccelerator.Controls.Add(this.label9);
			this.gbAccelerator.Controls.Add(this.label8);
			this.gbAccelerator.Location = new System.Drawing.Point(2, 3);
			this.gbAccelerator.Name = "gbAccelerator";
			this.gbAccelerator.Size = new System.Drawing.Size(301, 218);
			this.gbAccelerator.TabIndex = 0;
			this.gbAccelerator.TabStop = false;
			this.gbAccelerator.Text = "Server connection";
			// 
			// btnCheckConnection
			// 
			this.btnCheckConnection.Location = new System.Drawing.Point(90, 101);
			this.btnCheckConnection.Name = "btnCheckConnection";
			this.btnCheckConnection.Size = new System.Drawing.Size(110, 23);
			this.btnCheckConnection.TabIndex = 6;
			this.btnCheckConnection.Text = "Check Connection";
			this.btnCheckConnection.UseVisualStyleBackColor = true;
			this.btnCheckConnection.Click += new System.EventHandler(this.BtnCheckConnectionClick);
			// 
			// chbIsAccelerator
			// 
			this.chbIsAccelerator.AutoSize = true;
			this.chbIsAccelerator.Location = new System.Drawing.Point(6, 134);
			this.chbIsAccelerator.Name = "chbIsAccelerator";
			this.chbIsAccelerator.Size = new System.Drawing.Size(91, 17);
			this.chbIsAccelerator.TabIndex = 7;
			this.chbIsAccelerator.Text = "Is Accelerator";
			this.chbIsAccelerator.UseVisualStyleBackColor = true;
			this.chbIsAccelerator.CheckedChanged += new System.EventHandler(this.ChbIsAcceleratorCheckedChanged);
			// 
			// nudAdminPort
			// 
			this.nudAdminPort.Location = new System.Drawing.Point(90, 75);
			this.nudAdminPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.nudAdminPort.Name = "nudAdminPort";
			this.nudAdminPort.Size = new System.Drawing.Size(205, 20);
			this.nudAdminPort.TabIndex = 5;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 76);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Admin port:";
			// 
			// nudPort
			// 
			this.nudPort.Location = new System.Drawing.Point(90, 45);
			this.nudPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.nudPort.Name = "nudPort";
			this.nudPort.Size = new System.Drawing.Size(205, 20);
			this.nudPort.TabIndex = 3;
			// 
			// tbUsername
			// 
			this.tbUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbUsername.Enabled = false;
			this.tbUsername.Location = new System.Drawing.Point(90, 157);
			this.tbUsername.Name = "tbUsername";
			this.tbUsername.Size = new System.Drawing.Size(204, 20);
			this.tbUsername.TabIndex = 9;
			this.tbUsername.Text = "Admin";
			// 
			// tbMMAServer
			// 
			this.tbMMAServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbMMAServer.Location = new System.Drawing.Point(90, 18);
			this.tbMMAServer.Name = "tbMMAServer";
			this.tbMMAServer.Size = new System.Drawing.Size(205, 20);
			this.tbMMAServer.TabIndex = 1;
			this.tbMMAServer.Text = "localhost";
			// 
			// mtbPasword
			// 
			this.mtbPasword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mtbPasword.Enabled = false;
			this.mtbPasword.Location = new System.Drawing.Point(90, 183);
			this.mtbPasword.Name = "mtbPasword";
			this.mtbPasword.Size = new System.Drawing.Size(204, 20);
			this.mtbPasword.TabIndex = 11;
			this.mtbPasword.Text = "Admin";
			this.mtbPasword.UseSystemPasswordChar = true;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 46);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(57, 13);
			this.label11.TabIndex = 2;
			this.label11.Text = "Client port:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(20, 186);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(56, 13);
			this.label10.TabIndex = 10;
			this.label10.Text = "Password:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(20, 160);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(61, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "User name:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 21);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(81, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Server address:";
			// 
			// gbTemplates
			// 
			this.gbTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbTemplates.Controls.Add(this.rbDirectory);
			this.gbTemplates.Controls.Add(this.btnBrowse);
			this.gbTemplates.Controls.Add(this.gbDatabase);
			this.gbTemplates.Controls.Add(this.tbPath);
			this.gbTemplates.Controls.Add(this.rbDatabase);
			this.gbTemplates.Location = new System.Drawing.Point(2, 227);
			this.gbTemplates.Name = "gbTemplates";
			this.gbTemplates.Size = new System.Drawing.Size(301, 316);
			this.gbTemplates.TabIndex = 1;
			this.gbTemplates.TabStop = false;
			this.gbTemplates.Text = "Templates";
			// 
			// ConnectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 575);
			this.Controls.Add(this.gbTemplates);
			this.Controls.Add(this.gbAccelerator);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(280, 290);
			this.Name = "ConnectionForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Connection settings";
			this.gbDatabase.ResumeLayout(false);
			this.gbDatabase.PerformLayout();
			this.gbAccelerator.ResumeLayout(false);
			this.gbAccelerator.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAdminPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
			this.gbTemplates.ResumeLayout(false);
			this.gbTemplates.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.RadioButton rbDirectory;
		private System.Windows.Forms.RadioButton rbDatabase;
		private System.Windows.Forms.TextBox tbPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox gbDatabase;
		private System.Windows.Forms.TextBox tbDBUser;
		private System.Windows.Forms.TextBox tbDBServer;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.MaskedTextBox tbDBPassword;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbTable;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.ComboBox cbId;
		private System.Windows.Forms.ComboBox cbTemplate;
		private System.Windows.Forms.GroupBox gbAccelerator;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbMMAServer;
		private System.Windows.Forms.MaskedTextBox mtbPasword;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbUsername;
		private System.Windows.Forms.GroupBox gbTemplates;
		private System.Windows.Forms.NumericUpDown nudPort;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.NumericUpDown nudAdminPort;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox chbIsAccelerator;
		private System.Windows.Forms.Button btnCheckConnection;
	}
}
