namespace Neurotec.Samples
{
	partial class IrisesSettingsPage
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
			this.chbFastExtraction = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaximalRotation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
			this.tableLayoutPanel1.Controls.Add(this.chbFastExtraction, 1, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 187);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Iris scanner:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbScanners
			// 
			this.cbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScanners.FormattingEnabled = true;
			this.cbScanners.Location = new System.Drawing.Point(97, 3);
			this.cbScanners.Name = "cbScanners";
			this.cbScanners.Size = new System.Drawing.Size(191, 21);
			this.cbScanners.TabIndex = 1;
			this.cbScanners.SelectedIndexChanged += new System.EventHandler(this.CbScannersSelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 27);
			this.label2.TabIndex = 3;
			this.label2.Text = "Template size:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbTemplateSize
			// 
			this.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTemplateSize.FormattingEnabled = true;
			this.cbTemplateSize.Location = new System.Drawing.Point(97, 30);
			this.cbTemplateSize.Name = "cbTemplateSize";
			this.cbTemplateSize.Size = new System.Drawing.Size(191, 21);
			this.cbTemplateSize.TabIndex = 4;
			this.cbTemplateSize.SelectedIndexChanged += new System.EventHandler(this.CbTemplateSizeSelectedIndexChanged);
			// 
			// nudMaximalRotation
			// 
			this.nudMaximalRotation.Location = new System.Drawing.Point(97, 84);
			this.nudMaximalRotation.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nudMaximalRotation.Name = "nudMaximalRotation";
			this.nudMaximalRotation.Size = new System.Drawing.Size(71, 20);
			this.nudMaximalRotation.TabIndex = 9;
			this.nudMaximalRotation.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.nudMaximalRotation.ValueChanged += new System.EventHandler(this.NudMaximalRotationValueChanged);
			// 
			// cbMatchingSpeed
			// 
			this.cbMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMatchingSpeed.FormattingEnabled = true;
			this.cbMatchingSpeed.Location = new System.Drawing.Point(97, 57);
			this.cbMatchingSpeed.Name = "cbMatchingSpeed";
			this.cbMatchingSpeed.Size = new System.Drawing.Size(191, 21);
			this.cbMatchingSpeed.TabIndex = 11;
			this.cbMatchingSpeed.SelectedIndexChanged += new System.EventHandler(this.CbMatchingSpeedSelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 26);
			this.label4.TabIndex = 8;
			this.label4.Text = "Maximal rotation:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 27);
			this.label5.TabIndex = 10;
			this.label5.Text = "Matching speed:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 26);
			this.label3.TabIndex = 5;
			this.label3.Text = "Quality threshold:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nudQuality
			// 
			this.nudQuality.Location = new System.Drawing.Point(97, 110);
			this.nudQuality.Name = "nudQuality";
			this.nudQuality.Size = new System.Drawing.Size(71, 20);
			this.nudQuality.TabIndex = 6;
			this.nudQuality.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudQuality.ValueChanged += new System.EventHandler(this.NudQualityValueChanged);
			// 
			// chbFastExtraction
			// 
			this.chbFastExtraction.AutoSize = true;
			this.chbFastExtraction.Location = new System.Drawing.Point(97, 136);
			this.chbFastExtraction.Name = "chbFastExtraction";
			this.chbFastExtraction.Size = new System.Drawing.Size(95, 17);
			this.chbFastExtraction.TabIndex = 2;
			this.chbFastExtraction.Text = "Fast extraction";
			this.chbFastExtraction.UseVisualStyleBackColor = true;
			this.chbFastExtraction.CheckedChanged += new System.EventHandler(this.ChbFastExtractionCheckedChanged);
			// 
			// IrisesSettingsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "IrisesSettingsPage";
			this.Size = new System.Drawing.Size(294, 187);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaximalRotation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbScanners;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbTemplateSize;
		private System.Windows.Forms.NumericUpDown nudMaximalRotation;
		private System.Windows.Forms.ComboBox cbMatchingSpeed;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudQuality;
		private System.Windows.Forms.CheckBox chbFastExtraction;
	}
}
