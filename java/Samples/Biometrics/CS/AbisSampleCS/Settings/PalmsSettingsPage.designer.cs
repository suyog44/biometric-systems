namespace Neurotec.Samples
{
	partial class PalmsSettingsPage
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbScanners = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbTemplateSize = new System.Windows.Forms.ComboBox();
			this.nudMaximalRotation = new System.Windows.Forms.NumericUpDown();
			this.cbMatchingSpeed = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nudQuality = new System.Windows.Forms.NumericUpDown();
			this.chbReturnBinarized = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.nudRecordCount = new System.Windows.Forms.NumericUpDown();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnDisconnect = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaximalRotation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRecordCount)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbScanners, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbTemplateSize, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.nudMaximalRotation, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.cbMatchingSpeed, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.nudQuality, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.chbReturnBinarized, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.nudRecordCount, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.btnConnect, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnDisconnect, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(508, 187);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "Palm scanner:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbScanners
			// 
			this.cbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScanners.FormattingEnabled = true;
			this.cbScanners.Location = new System.Drawing.Point(149, 3);
			this.cbScanners.Name = "cbScanners";
			this.cbScanners.Size = new System.Drawing.Size(191, 21);
			this.cbScanners.TabIndex = 1;
			this.cbScanners.SelectedIndexChanged += new System.EventHandler(this.CbScannersSelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(140, 27);
			this.label2.TabIndex = 3;
			this.label2.Text = "Template size:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbTemplateSize
			// 
			this.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTemplateSize.FormattingEnabled = true;
			this.cbTemplateSize.Location = new System.Drawing.Point(149, 32);
			this.cbTemplateSize.Name = "cbTemplateSize";
			this.cbTemplateSize.Size = new System.Drawing.Size(191, 21);
			this.cbTemplateSize.TabIndex = 4;
			this.cbTemplateSize.SelectedIndexChanged += new System.EventHandler(this.CbTemplateSizeSelectedIndexChanged);
			// 
			// nudMaximalRotation
			// 
			this.nudMaximalRotation.Location = new System.Drawing.Point(149, 86);
			this.nudMaximalRotation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudMaximalRotation.Name = "nudMaximalRotation";
			this.nudMaximalRotation.Size = new System.Drawing.Size(71, 20);
			this.nudMaximalRotation.TabIndex = 9;
			this.nudMaximalRotation.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudMaximalRotation.ValueChanged += new System.EventHandler(this.NudMaximalRotationValueChanged);
			// 
			// cbMatchingSpeed
			// 
			this.cbMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMatchingSpeed.FormattingEnabled = true;
			this.cbMatchingSpeed.Location = new System.Drawing.Point(149, 59);
			this.cbMatchingSpeed.Name = "cbMatchingSpeed";
			this.cbMatchingSpeed.Size = new System.Drawing.Size(191, 21);
			this.cbMatchingSpeed.TabIndex = 11;
			this.cbMatchingSpeed.SelectedIndexChanged += new System.EventHandler(this.CbMatchingSpeedSelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(140, 26);
			this.label4.TabIndex = 8;
			this.label4.Text = "Maximal rotation:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(140, 27);
			this.label5.TabIndex = 10;
			this.label5.Text = "Matching speed:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(140, 26);
			this.label3.TabIndex = 5;
			this.label3.Text = "Quality threshold:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nudQuality
			// 
			this.nudQuality.Location = new System.Drawing.Point(149, 112);
			this.nudQuality.Name = "nudQuality";
			this.nudQuality.Size = new System.Drawing.Size(71, 20);
			this.nudQuality.TabIndex = 6;
			this.nudQuality.ValueChanged += new System.EventHandler(this.NudQualityValueChanged);
			// 
			// chbReturnBinarized
			// 
			this.chbReturnBinarized.AutoSize = true;
			this.chbReturnBinarized.Location = new System.Drawing.Point(149, 164);
			this.chbReturnBinarized.Name = "chbReturnBinarized";
			this.chbReturnBinarized.Size = new System.Drawing.Size(141, 17);
			this.chbReturnBinarized.TabIndex = 7;
			this.chbReturnBinarized.Text = "Return binarized image";
			this.chbReturnBinarized.UseVisualStyleBackColor = true;
			this.chbReturnBinarized.CheckedChanged += new System.EventHandler(this.ChbReturnBinarizedCheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(3, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(140, 26);
			this.label6.TabIndex = 12;
			this.label6.Text = "Generalization record count:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nudRecordCount
			// 
			this.nudRecordCount.Location = new System.Drawing.Point(149, 138);
			this.nudRecordCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.nudRecordCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.nudRecordCount.Name = "nudRecordCount";
			this.nudRecordCount.Size = new System.Drawing.Size(71, 20);
			this.nudRecordCount.TabIndex = 13;
			this.nudRecordCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(348, 3);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 14;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnDisconnect
			// 
			this.btnDisconnect.Location = new System.Drawing.Point(430, 3);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
			this.btnDisconnect.TabIndex = 15;
			this.btnDisconnect.Text = "Disconnect";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
			// 
			// PalmsSettingsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PalmsSettingsPage";
			this.PageName = "Palms settings";
			this.Size = new System.Drawing.Size(508, 187);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaximalRotation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRecordCount)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbScanners;
		private System.Windows.Forms.CheckBox chbReturnBinarized;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbTemplateSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudQuality;
		private System.Windows.Forms.NumericUpDown nudMaximalRotation;
		private System.Windows.Forms.ComboBox cbMatchingSpeed;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nudRecordCount;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnDisconnect;

	}
}
