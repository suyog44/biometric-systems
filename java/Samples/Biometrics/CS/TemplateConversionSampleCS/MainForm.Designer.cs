namespace Neurotec.Samples
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.rtbRight = new System.Windows.Forms.RichTextBox();
			this.btnConvertAndSave = new System.Windows.Forms.Button();
			this.gbOpenning = new System.Windows.Forms.GroupBox();
			this.rbNLRecord = new System.Windows.Forms.RadioButton();
			this.rbNLTemplate = new System.Windows.Forms.RadioButton();
			this.rbNTemplate = new System.Windows.Forms.RadioButton();
			this.rbNFTemplate = new System.Windows.Forms.RadioButton();
			this.rbNFRecords = new System.Windows.Forms.RadioButton();
			this.rbFMRecordISO = new System.Windows.Forms.RadioButton();
			this.rbFMRecordANSI = new System.Windows.Forms.RadioButton();
			this.rbANTemplate = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.rbNLRecordRight = new System.Windows.Forms.RadioButton();
			this.rbNLTemplateRight = new System.Windows.Forms.RadioButton();
			this.rbNTemplateRight = new System.Windows.Forms.RadioButton();
			this.rbNFTemplateRight = new System.Windows.Forms.RadioButton();
			this.rbNFRecordsRight = new System.Windows.Forms.RadioButton();
			this.rbFMRecordISORight = new System.Windows.Forms.RadioButton();
			this.rbFMRecordANSIRight = new System.Windows.Forms.RadioButton();
			this.rbANTemplateRight = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.openImageButton = new System.Windows.Forms.Button();
			this.rtbLeft = new System.Windows.Forms.RichTextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.gbOpenning.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(716, 552);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(708, 526);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Template conversion";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.rtbRight, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnConvertAndSave, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.gbOpenning, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.openImageButton, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rtbLeft, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(702, 520);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// rtbRight
			// 
			this.rtbRight.BackColor = System.Drawing.SystemColors.Window;
			this.rtbRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbRight.Location = new System.Drawing.Point(354, 275);
			this.rtbRight.Name = "rtbRight";
			this.rtbRight.ReadOnly = true;
			this.rtbRight.Size = new System.Drawing.Size(345, 242);
			this.rtbRight.TabIndex = 5;
			this.rtbRight.TabStop = false;
			this.rtbRight.Text = "";
			this.rtbRight.WordWrap = false;
			// 
			// btnConvertAndSave
			// 
			this.btnConvertAndSave.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnConvertAndSave.Image = ((System.Drawing.Image)(resources.GetObject("btnConvertAndSave.Image")));
			this.btnConvertAndSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnConvertAndSave.Location = new System.Drawing.Point(436, 243);
			this.btnConvertAndSave.Name = "btnConvertAndSave";
			this.btnConvertAndSave.Size = new System.Drawing.Size(181, 23);
			this.btnConvertAndSave.TabIndex = 4;
			this.btnConvertAndSave.Text = "Convert template and save";
			this.btnConvertAndSave.UseVisualStyleBackColor = true;
			this.btnConvertAndSave.Click += new System.EventHandler(this.btnConvertAndSave_Click);
			// 
			// gbOpenning
			// 
			this.gbOpenning.Controls.Add(this.rbNLRecord);
			this.gbOpenning.Controls.Add(this.rbNLTemplate);
			this.gbOpenning.Controls.Add(this.rbNTemplate);
			this.gbOpenning.Controls.Add(this.rbNFTemplate);
			this.gbOpenning.Controls.Add(this.rbNFRecords);
			this.gbOpenning.Controls.Add(this.rbFMRecordISO);
			this.gbOpenning.Controls.Add(this.rbFMRecordANSI);
			this.gbOpenning.Controls.Add(this.rbANTemplate);
			this.gbOpenning.Controls.Add(this.label6);
			this.gbOpenning.Controls.Add(this.label1);
			this.gbOpenning.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbOpenning.Location = new System.Drawing.Point(3, 3);
			this.gbOpenning.Name = "gbOpenning";
			this.gbOpenning.Size = new System.Drawing.Size(345, 232);
			this.gbOpenning.TabIndex = 0;
			this.gbOpenning.TabStop = false;
			this.gbOpenning.Text = "Template to convert from";
			// 
			// rbNLRecord
			// 
			this.rbNLRecord.AutoSize = true;
			this.rbNLRecord.Location = new System.Drawing.Point(69, 158);
			this.rbNLRecord.Name = "rbNLRecord";
			this.rbNLRecord.Size = new System.Drawing.Size(234, 17);
			this.rbNLRecord.TabIndex = 6;
			this.rbNLRecord.TabStop = true;
			this.rbNLRecord.Text = "Neurotechnology Face Record (NLRecord)";
			this.rbNLRecord.UseVisualStyleBackColor = true;
			this.rbNLRecord.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbNLTemplate
			// 
			this.rbNLTemplate.AutoSize = true;
			this.rbNLTemplate.Location = new System.Drawing.Point(69, 181);
			this.rbNLTemplate.Name = "rbNLTemplate";
			this.rbNLTemplate.Size = new System.Drawing.Size(257, 17);
			this.rbNLTemplate.TabIndex = 7;
			this.rbNLTemplate.TabStop = true;
			this.rbNLTemplate.Text = "Neurotechnology Faces Template (NLTemplate)";
			this.rbNLTemplate.UseVisualStyleBackColor = true;
			this.rbNLTemplate.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbNTemplate
			// 
			this.rbNTemplate.AutoSize = true;
			this.rbNTemplate.Location = new System.Drawing.Point(69, 204);
			this.rbNTemplate.Name = "rbNTemplate";
			this.rbNTemplate.Size = new System.Drawing.Size(219, 17);
			this.rbNTemplate.TabIndex = 8;
			this.rbNTemplate.TabStop = true;
			this.rbNTemplate.Text = "Neurotechnology Template (NTemplate)";
			this.rbNTemplate.UseVisualStyleBackColor = true;
			this.rbNTemplate.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbNFTemplate
			// 
			this.rbNFTemplate.AutoSize = true;
			this.rbNFTemplate.Location = new System.Drawing.Point(69, 135);
			this.rbNFTemplate.Name = "rbNFTemplate";
			this.rbNFTemplate.Size = new System.Drawing.Size(262, 17);
			this.rbNFTemplate.TabIndex = 5;
			this.rbNFTemplate.TabStop = true;
			this.rbNFTemplate.Text = "Neurotechnology Fingers Template (NFTemplate)";
			this.rbNFTemplate.UseVisualStyleBackColor = true;
			this.rbNFTemplate.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbNFRecords
			// 
			this.rbNFRecords.AutoSize = true;
			this.rbNFRecords.Location = new System.Drawing.Point(69, 112);
			this.rbNFRecords.Name = "rbNFRecords";
			this.rbNFRecords.Size = new System.Drawing.Size(239, 17);
			this.rbNFRecords.TabIndex = 4;
			this.rbNFRecords.TabStop = true;
			this.rbNFRecords.Text = "Neurotechnology Finger Record (NFRecord)";
			this.rbNFRecords.UseVisualStyleBackColor = true;
			this.rbNFRecords.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbFMRecordISO
			// 
			this.rbFMRecordISO.AutoSize = true;
			this.rbFMRecordISO.Location = new System.Drawing.Point(69, 89);
			this.rbFMRecordISO.Name = "rbFMRecordISO";
			this.rbFMRecordISO.Size = new System.Drawing.Size(193, 17);
			this.rbFMRecordISO.TabIndex = 3;
			this.rbFMRecordISO.TabStop = true;
			this.rbFMRecordISO.Text = "ISO/IEC 19794-2:2005 (FMRecord)";
			this.rbFMRecordISO.UseVisualStyleBackColor = true;
			this.rbFMRecordISO.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbFMRecordANSI
			// 
			this.rbFMRecordANSI.AutoSize = true;
			this.rbFMRecordANSI.Location = new System.Drawing.Point(69, 66);
			this.rbFMRecordANSI.Name = "rbFMRecordANSI";
			this.rbFMRecordANSI.Size = new System.Drawing.Size(195, 17);
			this.rbFMRecordANSI.TabIndex = 2;
			this.rbFMRecordANSI.TabStop = true;
			this.rbFMRecordANSI.Text = "ANSI INCITS 378-2004 (FMRecord)";
			this.rbFMRecordANSI.UseVisualStyleBackColor = true;
			this.rbFMRecordANSI.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// rbANTemplate
			// 
			this.rbANTemplate.AutoSize = true;
			this.rbANTemplate.Location = new System.Drawing.Point(69, 43);
			this.rbANTemplate.Name = "rbANTemplate";
			this.rbANTemplate.Size = new System.Drawing.Size(203, 17);
			this.rbANTemplate.TabIndex = 1;
			this.rbANTemplate.TabStop = true;
			this.rbANTemplate.Text = "ANSI/NIST-ITL 1-2000 (ANTemplate)";
			this.rbANTemplate.UseVisualStyleBackColor = true;
			this.rbANTemplate.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Template:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.rbNLRecordRight);
			this.groupBox2.Controls.Add(this.rbNLTemplateRight);
			this.groupBox2.Controls.Add(this.rbNTemplateRight);
			this.groupBox2.Controls.Add(this.rbNFTemplateRight);
			this.groupBox2.Controls.Add(this.rbNFRecordsRight);
			this.groupBox2.Controls.Add(this.rbFMRecordISORight);
			this.groupBox2.Controls.Add(this.rbFMRecordANSIRight);
			this.groupBox2.Controls.Add(this.rbANTemplateRight);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(354, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(345, 232);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Template to convert to";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 27);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Template:";
			// 
			// rbNLRecordRight
			// 
			this.rbNLRecordRight.AutoSize = true;
			this.rbNLRecordRight.Location = new System.Drawing.Point(66, 158);
			this.rbNLRecordRight.Name = "rbNLRecordRight";
			this.rbNLRecordRight.Size = new System.Drawing.Size(234, 17);
			this.rbNLRecordRight.TabIndex = 7;
			this.rbNLRecordRight.TabStop = true;
			this.rbNLRecordRight.Text = "Neurotechnology Face Record (NLRecord)";
			this.rbNLRecordRight.UseVisualStyleBackColor = true;
			// 
			// rbNLTemplateRight
			// 
			this.rbNLTemplateRight.AutoSize = true;
			this.rbNLTemplateRight.Location = new System.Drawing.Point(66, 181);
			this.rbNLTemplateRight.Name = "rbNLTemplateRight";
			this.rbNLTemplateRight.Size = new System.Drawing.Size(252, 17);
			this.rbNLTemplateRight.TabIndex = 8;
			this.rbNLTemplateRight.TabStop = true;
			this.rbNLTemplateRight.Text = "Neurotechnology Face Template (NLTemplate)";
			this.rbNLTemplateRight.UseVisualStyleBackColor = true;
			// 
			// rbNTemplateRight
			// 
			this.rbNTemplateRight.AutoSize = true;
			this.rbNTemplateRight.Location = new System.Drawing.Point(66, 204);
			this.rbNTemplateRight.Name = "rbNTemplateRight";
			this.rbNTemplateRight.Size = new System.Drawing.Size(224, 17);
			this.rbNTemplateRight.TabIndex = 9;
			this.rbNTemplateRight.TabStop = true;
			this.rbNTemplateRight.Text = "Neurotechnology Templates (NTemplate)";
			this.rbNTemplateRight.UseVisualStyleBackColor = true;
			// 
			// rbNFTemplateRight
			// 
			this.rbNFTemplateRight.AutoSize = true;
			this.rbNFTemplateRight.Location = new System.Drawing.Point(66, 135);
			this.rbNFTemplateRight.Name = "rbNFTemplateRight";
			this.rbNFTemplateRight.Size = new System.Drawing.Size(262, 17);
			this.rbNFTemplateRight.TabIndex = 6;
			this.rbNFTemplateRight.TabStop = true;
			this.rbNFTemplateRight.Text = "Neurotechnology Fingers Template (NFTemplate)";
			this.rbNFTemplateRight.UseVisualStyleBackColor = true;
			// 
			// rbNFRecordsRight
			// 
			this.rbNFRecordsRight.AutoSize = true;
			this.rbNFRecordsRight.Location = new System.Drawing.Point(66, 112);
			this.rbNFRecordsRight.Name = "rbNFRecordsRight";
			this.rbNFRecordsRight.Size = new System.Drawing.Size(239, 17);
			this.rbNFRecordsRight.TabIndex = 5;
			this.rbNFRecordsRight.TabStop = true;
			this.rbNFRecordsRight.Text = "Neurotechnology Finger Record (NFRecord)";
			this.rbNFRecordsRight.UseVisualStyleBackColor = true;
			// 
			// rbFMRecordISORight
			// 
			this.rbFMRecordISORight.AutoSize = true;
			this.rbFMRecordISORight.Location = new System.Drawing.Point(66, 89);
			this.rbFMRecordISORight.Name = "rbFMRecordISORight";
			this.rbFMRecordISORight.Size = new System.Drawing.Size(193, 17);
			this.rbFMRecordISORight.TabIndex = 4;
			this.rbFMRecordISORight.TabStop = true;
			this.rbFMRecordISORight.Text = "ISO/IEC 19794-2:2005 (FMRecord)";
			this.rbFMRecordISORight.UseVisualStyleBackColor = true;
			// 
			// rbFMRecordANSIRight
			// 
			this.rbFMRecordANSIRight.AutoSize = true;
			this.rbFMRecordANSIRight.Location = new System.Drawing.Point(66, 66);
			this.rbFMRecordANSIRight.Name = "rbFMRecordANSIRight";
			this.rbFMRecordANSIRight.Size = new System.Drawing.Size(195, 17);
			this.rbFMRecordANSIRight.TabIndex = 3;
			this.rbFMRecordANSIRight.TabStop = true;
			this.rbFMRecordANSIRight.Text = "ANSI INCITS 378-2004 (FMRecord)";
			this.rbFMRecordANSIRight.UseVisualStyleBackColor = true;
			// 
			// rbANTemplateRight
			// 
			this.rbANTemplateRight.AutoSize = true;
			this.rbANTemplateRight.Location = new System.Drawing.Point(66, 43);
			this.rbANTemplateRight.Name = "rbANTemplateRight";
			this.rbANTemplateRight.Size = new System.Drawing.Size(203, 17);
			this.rbANTemplateRight.TabIndex = 2;
			this.rbANTemplateRight.TabStop = true;
			this.rbANTemplateRight.Text = "ANSI/NIST-ITL 1-2000 (ANTemplate)";
			this.rbANTemplateRight.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(0, 13);
			this.label5.TabIndex = 1;
			// 
			// openImageButton
			// 
			this.openImageButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.openImageButton.Image = ((System.Drawing.Image)(resources.GetObject("openImageButton.Image")));
			this.openImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.openImageButton.Location = new System.Drawing.Point(85, 243);
			this.openImageButton.Name = "openImageButton";
			this.openImageButton.Size = new System.Drawing.Size(181, 23);
			this.openImageButton.TabIndex = 1;
			this.openImageButton.Text = "Open template";
			this.openImageButton.UseVisualStyleBackColor = true;
			this.openImageButton.Click += new System.EventHandler(this.openImageButton1_Click);
			// 
			// rtbLeft
			// 
			this.rtbLeft.BackColor = System.Drawing.SystemColors.Window;
			this.rtbLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbLeft.Location = new System.Drawing.Point(3, 275);
			this.rtbLeft.Name = "rtbLeft";
			this.rtbLeft.ReadOnly = true;
			this.rtbLeft.Size = new System.Drawing.Size(345, 242);
			this.rtbLeft.TabIndex = 2;
			this.rtbLeft.TabStop = false;
			this.rtbLeft.Text = "";
			this.rtbLeft.WordWrap = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(732, 576);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Neurotechnology Template Conversion Sample";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.gbOpenning.ResumeLayout(false);
			this.gbOpenning.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox gbOpenning;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button openImageButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton rbFMRecordISO;
		private System.Windows.Forms.RadioButton rbFMRecordANSI;
		private System.Windows.Forms.RadioButton rbANTemplate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.RadioButton rbNFTemplate;
		private System.Windows.Forms.RadioButton rbNFRecords;
		private System.Windows.Forms.RadioButton rbNLRecord;
		private System.Windows.Forms.RadioButton rbNLTemplate;
		private System.Windows.Forms.RadioButton rbNTemplate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rbNLRecordRight;
		private System.Windows.Forms.RadioButton rbNLTemplateRight;
		private System.Windows.Forms.RadioButton rbNTemplateRight;
		private System.Windows.Forms.RadioButton rbNFTemplateRight;
		private System.Windows.Forms.RadioButton rbNFRecordsRight;
		private System.Windows.Forms.RadioButton rbFMRecordISORight;
		private System.Windows.Forms.RadioButton rbFMRecordANSIRight;
		private System.Windows.Forms.RadioButton rbANTemplateRight;
		private System.Windows.Forms.Button btnConvertAndSave;
		private System.Windows.Forms.RichTextBox rtbRight;
		private System.Windows.Forms.RichTextBox rtbLeft;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
	}
}

