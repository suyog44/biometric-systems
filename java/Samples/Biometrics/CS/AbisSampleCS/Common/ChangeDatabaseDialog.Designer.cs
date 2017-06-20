namespace Neurotec.Samples
{
	partial class ChangeDatabaseDialog
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeDatabaseDialog));
			this.tbConnectionString = new System.Windows.Forms.TextBox();
			this.nudClientPort = new System.Windows.Forms.NumericUpDown();
			this.nudAdminPort = new System.Windows.Forms.NumericUpDown();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.chbClear = new System.Windows.Forms.CheckBox();
			this.lblClientPort = new System.Windows.Forms.Label();
			this.lblAdminPort = new System.Windows.Forms.Label();
			this.cbTableName = new System.Windows.Forms.ComboBox();
			this.btnListTables = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tbHostName = new System.Windows.Forms.TextBox();
			this.rbRemoteServer = new System.Windows.Forms.RadioButton();
			this.rbSQLite = new System.Windows.Forms.RadioButton();
			this.rbOdbc = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblDbSchema = new System.Windows.Forms.Label();
			this.cbSchema = new System.Windows.Forms.ComboBox();
			this.btnEdit = new System.Windows.Forms.Button();
			this.cbLocalOperations = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAdminPort)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbConnectionString
			// 
			this.tbConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.tbConnectionString, 2);
			this.tbConnectionString.Location = new System.Drawing.Point(121, 59);
			this.tbConnectionString.Name = "tbConnectionString";
			this.tbConnectionString.Size = new System.Drawing.Size(263, 20);
			this.tbConnectionString.TabIndex = 4;
			// 
			// nudClientPort
			// 
			this.nudClientPort.Location = new System.Drawing.Point(121, 181);
			this.nudClientPort.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
			this.nudClientPort.Name = "nudClientPort";
			this.nudClientPort.Size = new System.Drawing.Size(111, 20);
			this.nudClientPort.TabIndex = 9;
			this.nudClientPort.Value = new decimal(new int[] {
            25452,
            0,
            0,
            0});
			// 
			// nudAdminPort
			// 
			this.nudAdminPort.Location = new System.Drawing.Point(121, 207);
			this.nudAdminPort.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
			this.nudAdminPort.Name = "nudAdminPort";
			this.nudAdminPort.Size = new System.Drawing.Size(111, 20);
			this.nudAdminPort.TabIndex = 10;
			this.nudAdminPort.Value = new decimal(new int[] {
            24932,
            0,
            0,
            0});
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(309, 320);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(230, 320);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(73, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// chbClear
			// 
			this.chbClear.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.chbClear, 4);
			this.chbClear.Location = new System.Drawing.Point(3, 265);
			this.chbClear.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.chbClear.Name = "chbClear";
			this.chbClear.Size = new System.Drawing.Size(87, 17);
			this.chbClear.TabIndex = 11;
			this.chbClear.Text = "Clear all data";
			this.chbClear.UseVisualStyleBackColor = true;
			// 
			// lblClientPort
			// 
			this.lblClientPort.AutoSize = true;
			this.lblClientPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblClientPort.Location = new System.Drawing.Point(23, 178);
			this.lblClientPort.Name = "lblClientPort";
			this.lblClientPort.Size = new System.Drawing.Size(92, 26);
			this.lblClientPort.TabIndex = 13;
			this.lblClientPort.Text = "Client port:";
			this.lblClientPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblAdminPort
			// 
			this.lblAdminPort.AutoSize = true;
			this.lblAdminPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblAdminPort.Location = new System.Drawing.Point(23, 204);
			this.lblAdminPort.Name = "lblAdminPort";
			this.lblAdminPort.Size = new System.Drawing.Size(92, 26);
			this.lblAdminPort.TabIndex = 14;
			this.lblAdminPort.Text = "Admin port:";
			this.lblAdminPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbTableName
			// 
			this.cbTableName.FormattingEnabled = true;
			this.cbTableName.Location = new System.Drawing.Point(121, 98);
			this.cbTableName.Name = "cbTableName";
			this.cbTableName.Size = new System.Drawing.Size(118, 21);
			this.cbTableName.TabIndex = 5;
			// 
			// btnListTables
			// 
			this.btnListTables.Location = new System.Drawing.Point(309, 98);
			this.btnListTables.Name = "btnListTables";
			this.btnListTables.Size = new System.Drawing.Size(75, 23);
			this.btnListTables.TabIndex = 6;
			this.btnListTables.Text = "List";
			this.btnListTables.UseVisualStyleBackColor = true;
			this.btnListTables.Click += new System.EventHandler(this.BtnListTablesClick);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.tbHostName, 2, 6);
			this.tableLayoutPanel2.Controls.Add(this.rbRemoteServer, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.rbSQLite, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tbConnectionString, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.btnListTables, 3, 4);
			this.tableLayoutPanel2.Controls.Add(this.cbTableName, 2, 4);
			this.tableLayoutPanel2.Controls.Add(this.nudAdminPort, 2, 8);
			this.tableLayoutPanel2.Controls.Add(this.nudClientPort, 2, 7);
			this.tableLayoutPanel2.Controls.Add(this.rbOdbc, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label1, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.btnCancel, 3, 12);
			this.tableLayoutPanel2.Controls.Add(this.label2, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.btnOK, 2, 12);
			this.tableLayoutPanel2.Controls.Add(this.lblClientPort, 1, 7);
			this.tableLayoutPanel2.Controls.Add(this.chbClear, 0, 10);
			this.tableLayoutPanel2.Controls.Add(this.lblAdminPort, 1, 8);
			this.tableLayoutPanel2.Controls.Add(this.label3, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.label4, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.lblDbSchema, 0, 11);
			this.tableLayoutPanel2.Controls.Add(this.cbSchema, 2, 11);
			this.tableLayoutPanel2.Controls.Add(this.btnEdit, 3, 11);
			this.tableLayoutPanel2.Controls.Add(this.cbLocalOperations, 2, 9);
			this.tableLayoutPanel2.Controls.Add(this.label5, 1, 9);
			this.tableLayoutPanel2.Controls.Add(this.label6, 3, 9);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 13;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(387, 346);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// tbHostName
			// 
			this.tbHostName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.tbHostName, 2);
			this.tbHostName.Location = new System.Drawing.Point(121, 155);
			this.tbHostName.Name = "tbHostName";
			this.tbHostName.Size = new System.Drawing.Size(263, 20);
			this.tbHostName.TabIndex = 8;
			// 
			// rbRemoteServer
			// 
			this.rbRemoteServer.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.rbRemoteServer, 4);
			this.rbRemoteServer.Location = new System.Drawing.Point(3, 132);
			this.rbRemoteServer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.rbRemoteServer.Name = "rbRemoteServer";
			this.rbRemoteServer.Size = new System.Drawing.Size(140, 17);
			this.rbRemoteServer.TabIndex = 7;
			this.rbRemoteServer.TabStop = true;
			this.rbRemoteServer.Text = "Remote matching server";
			this.rbRemoteServer.UseVisualStyleBackColor = true;
			this.rbRemoteServer.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbSQLite
			// 
			this.rbSQLite.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.rbSQLite, 4);
			this.rbSQLite.Location = new System.Drawing.Point(3, 8);
			this.rbSQLite.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.rbSQLite.Name = "rbSQLite";
			this.rbSQLite.Size = new System.Drawing.Size(160, 17);
			this.rbSQLite.TabIndex = 2;
			this.rbSQLite.TabStop = true;
			this.rbSQLite.Text = "SQLite database connection";
			this.rbSQLite.UseVisualStyleBackColor = true;
			this.rbSQLite.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbOdbc
			// 
			this.rbOdbc.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.rbOdbc, 4);
			this.rbOdbc.Location = new System.Drawing.Point(3, 36);
			this.rbOdbc.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.rbOdbc.Name = "rbOdbc";
			this.rbOdbc.Size = new System.Drawing.Size(154, 17);
			this.rbOdbc.TabIndex = 3;
			this.rbOdbc.TabStop = true;
			this.rbOdbc.Text = "Odbc database connection";
			this.rbOdbc.UseVisualStyleBackColor = true;
			this.rbOdbc.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(23, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "Connection string:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(23, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 29);
			this.label2.TabIndex = 3;
			this.label2.Text = "Table name:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(23, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 26);
			this.label3.TabIndex = 18;
			this.label3.Text = "Server address:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.label4, 3);
			this.label4.Location = new System.Drawing.Point(23, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(303, 13);
			this.label4.TabIndex = 20;
			this.label4.Text = "                Example: Dsn=mysql_dsn;UID=user;PWD=password";
			// 
			// lblDbSchema
			// 
			this.lblDbSchema.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.lblDbSchema, 2);
			this.lblDbSchema.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDbSchema.Location = new System.Drawing.Point(3, 285);
			this.lblDbSchema.Name = "lblDbSchema";
			this.lblDbSchema.Size = new System.Drawing.Size(112, 29);
			this.lblDbSchema.TabIndex = 21;
			this.lblDbSchema.Text = "Database schema:";
			this.lblDbSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbSchema
			// 
			this.cbSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSchema.FormattingEnabled = true;
			this.cbSchema.Location = new System.Drawing.Point(121, 288);
			this.cbSchema.Name = "cbSchema";
			this.cbSchema.Size = new System.Drawing.Size(182, 21);
			this.cbSchema.TabIndex = 22;
			this.cbSchema.SelectedIndexChanged += new System.EventHandler(this.CbSchemaSelectedIndexChanged);
			// 
			// btnEdit
			// 
			this.btnEdit.Location = new System.Drawing.Point(309, 288);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 23;
			this.btnEdit.Text = "Edit";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.BtnEditClick);
			// 
			// cbLocalOperations
			// 
			this.cbLocalOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLocalOperations.FormattingEnabled = true;
			this.cbLocalOperations.Items.AddRange(new object[] {
            "None",
            "Detect",
            "Detect - DetectSegments",
            "Detect - Segment",
            "Detect - AssessQuality",
            "All"});
			this.cbLocalOperations.Location = new System.Drawing.Point(121, 233);
			this.cbLocalOperations.Name = "cbLocalOperations";
			this.cbLocalOperations.Size = new System.Drawing.Size(182, 21);
			this.cbLocalOperations.TabIndex = 24;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(23, 230);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 27);
			this.label5.TabIndex = 25;
			this.label5.Text = "Local operations:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Image = global::Neurotec.Samples.Properties.Resources.Help;
			this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label6.Location = new System.Drawing.Point(309, 230);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 27);
			this.label6.TabIndex = 26;
			this.label6.Text = "    ";
			this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 24000;
			this.toolTip.InitialDelay = 500;
			this.toolTip.ReshowDelay = 100;
			// 
			// ChangeDatabaseDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(387, 346);
			this.Controls.Add(this.tableLayoutPanel2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChangeDatabaseDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connection Settings";
			this.Load += new System.EventHandler(this.ChangeDatabaseDialogLoad);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangeDatabaseDialogFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAdminPort)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox tbConnectionString;
		private System.Windows.Forms.NumericUpDown nudClientPort;
		private System.Windows.Forms.NumericUpDown nudAdminPort;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox chbClear;
		private System.Windows.Forms.Label lblClientPort;
		private System.Windows.Forms.Label lblAdminPort;
		private System.Windows.Forms.ComboBox cbTableName;
		private System.Windows.Forms.Button btnListTables;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.RadioButton rbSQLite;
		private System.Windows.Forms.RadioButton rbOdbc;
		private System.Windows.Forms.RadioButton rbRemoteServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbHostName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblDbSchema;
		private System.Windows.Forms.ComboBox cbSchema;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ComboBox cbLocalOperations;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
	}
}
