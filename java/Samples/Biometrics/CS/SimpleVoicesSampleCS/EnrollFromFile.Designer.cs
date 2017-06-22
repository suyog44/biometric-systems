namespace Neurotec.Samples
{
	partial class EnrollFromFile
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollFromFile));
			this.btnOpen = new System.Windows.Forms.Button();
			this.lblSoundFile = new System.Windows.Forms.Label();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.btnExtract = new System.Windows.Forms.Button();
			this.nudPhraseId = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chbExtractTextIndependent = new System.Windows.Forms.CheckBox();
			this.chbExtractTextDependent = new System.Windows.Forms.CheckBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSaveVoice = new System.Windows.Forms.Button();
			this.saveVoiceFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			((System.ComponentModel.ISupportInitialize)(this.nudPhraseId)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
			this.btnOpen.Location = new System.Drawing.Point(3, 54);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(110, 23);
			this.btnOpen.TabIndex = 4;
			this.btnOpen.Tag = "Open";
			this.btnOpen.Text = "Open file";
			this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// lblSoundFile
			// 
			this.lblSoundFile.AutoSize = true;
			this.lblSoundFile.Location = new System.Drawing.Point(177, 59);
			this.lblSoundFile.Name = "lblSoundFile";
			this.lblSoundFile.Size = new System.Drawing.Size(94, 13);
			this.lblSoundFile.TabIndex = 5;
			this.lblSoundFile.Text = "Sound file location";
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveTemplate.Image")));
			this.btnSaveTemplate.Location = new System.Drawing.Point(3, 199);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(110, 23);
			this.btnSaveTemplate.TabIndex = 7;
			this.btnSaveTemplate.Text = "Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// btnExtract
			// 
			this.btnExtract.Location = new System.Drawing.Point(3, 154);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(110, 23);
			this.btnExtract.TabIndex = 8;
			this.btnExtract.Text = "&Extract";
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.BtnExtractClick);
			// 
			// nudPhraseId
			// 
			this.nudPhraseId.Location = new System.Drawing.Point(184, 157);
			this.nudPhraseId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudPhraseId.Name = "nudPhraseId";
			this.nudPhraseId.Size = new System.Drawing.Size(93, 20);
			this.nudPhraseId.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(123, 159);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Phrase Id:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chbExtractTextIndependent);
			this.groupBox1.Controls.Add(this.chbExtractTextDependent);
			this.groupBox1.Location = new System.Drawing.Point(3, 83);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 65);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// chbExtractTextIndependent
			// 
			this.chbExtractTextIndependent.AutoSize = true;
			this.chbExtractTextIndependent.Checked = true;
			this.chbExtractTextIndependent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbExtractTextIndependent.Location = new System.Drawing.Point(6, 42);
			this.chbExtractTextIndependent.Name = "chbExtractTextIndependent";
			this.chbExtractTextIndependent.Size = new System.Drawing.Size(182, 17);
			this.chbExtractTextIndependent.TabIndex = 16;
			this.chbExtractTextIndependent.Text = "Extract text independent features";
			this.chbExtractTextIndependent.UseVisualStyleBackColor = true;
			this.chbExtractTextIndependent.CheckedChanged += new System.EventHandler(this.ChbExtractTextIndependentCheckedChanged);
			// 
			// chbExtractTextDependent
			// 
			this.chbExtractTextDependent.AutoSize = true;
			this.chbExtractTextDependent.Checked = true;
			this.chbExtractTextDependent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbExtractTextDependent.Location = new System.Drawing.Point(6, 19);
			this.chbExtractTextDependent.Name = "chbExtractTextDependent";
			this.chbExtractTextDependent.Size = new System.Drawing.Size(174, 17);
			this.chbExtractTextDependent.TabIndex = 15;
			this.chbExtractTextDependent.Text = "Extract text dependent features";
			this.chbExtractTextDependent.UseVisualStyleBackColor = true;
			this.chbExtractTextDependent.CheckedChanged += new System.EventHandler(this.ChbExtractTextDependentCheckedChanged);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.lblStatus.Location = new System.Drawing.Point(0, 180);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(104, 13);
			this.lblStatus.TabIndex = 16;
			this.lblStatus.Text = "Extraction Status";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(115, 59);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Sound File:";
			// 
			// btnSaveVoice
			// 
			this.btnSaveVoice.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveVoice.Image")));
			this.btnSaveVoice.Location = new System.Drawing.Point(116, 199);
			this.btnSaveVoice.Name = "btnSaveVoice";
			this.btnSaveVoice.Size = new System.Drawing.Size(125, 23);
			this.btnSaveVoice.TabIndex = 19;
			this.btnSaveVoice.Text = "Save Voice Audio";
			this.btnSaveVoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveVoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveVoice.UseVisualStyleBackColor = true;
			this.btnSaveVoice.Click += new System.EventHandler(this.BtnSaveVoiceClick);
			// 
			// saveVoiceFileDialog
			// 
			this.saveVoiceFileDialog.Filter = "Wave audio files (*.wav;*.wave)|*.wav;*.wave";
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 3);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Media,Biometrics.VoiceExtraction";
			this.licensePanel.Size = new System.Drawing.Size(395, 45);
			this.licensePanel.TabIndex = 18;
			// 
			// EnrollFromFile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSaveVoice);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudPhraseId);
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.lblSoundFile);
			this.Controls.Add(this.btnOpen);
			this.Name = "EnrollFromFile";
			this.Size = new System.Drawing.Size(398, 297);
			this.Load += new System.EventHandler(this.EnrollFromFileLoad);
			this.VisibleChanged += new System.EventHandler(this.EnrollFromFileVisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.nudPhraseId)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label lblSoundFile;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.NumericUpDown nudPhraseId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label label5;
		private LicensePanel licensePanel;
		private System.Windows.Forms.Button btnSaveVoice;
		private System.Windows.Forms.SaveFileDialog saveVoiceFileDialog;
		private System.Windows.Forms.CheckBox chbExtractTextIndependent;
		private System.Windows.Forms.CheckBox chbExtractTextDependent;
	}
}
