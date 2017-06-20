namespace Neurotec.Samples
{
	partial class IdentifyVoice
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdentifyVoice));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblTemplatesCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenTemplates = new System.Windows.Forms.Button();
			this.matchingGroupBox = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chbUniquePhrases = new System.Windows.Forms.CheckBox();
			this.btnDefault = new System.Windows.Forms.Button();
			this.cbMatchingFar = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblFileForIdentification = new System.Windows.Forms.Label();
			this.btnOpenAudio = new System.Windows.Forms.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnIdentify = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.groupBox1.SuspendLayout();
			this.matchingGroupBox.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.lblTemplatesCount);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnOpenTemplates);
			this.groupBox1.Location = new System.Drawing.Point(3, 51);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(472, 57);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Templates loading";
			// 
			// lblTemplatesCount
			// 
			this.lblTemplatesCount.AutoSize = true;
			this.lblTemplatesCount.Location = new System.Drawing.Point(142, 30);
			this.lblTemplatesCount.Name = "lblTemplatesCount";
			this.lblTemplatesCount.Size = new System.Drawing.Size(82, 13);
			this.lblTemplatesCount.TabIndex = 7;
			this.lblTemplatesCount.Text = "templates count";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Templates loaded:";
			// 
			// btnOpenTemplates
			// 
			this.btnOpenTemplates.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTemplates.Image")));
			this.btnOpenTemplates.Location = new System.Drawing.Point(12, 24);
			this.btnOpenTemplates.Name = "btnOpenTemplates";
			this.btnOpenTemplates.Size = new System.Drawing.Size(30, 23);
			this.btnOpenTemplates.TabIndex = 5;
			this.btnOpenTemplates.UseVisualStyleBackColor = true;
			this.btnOpenTemplates.Click += new System.EventHandler(this.BtnOpenTemplatesClick);
			// 
			// matchingGroupBox
			// 
			this.matchingGroupBox.Controls.Add(this.label3);
			this.matchingGroupBox.Controls.Add(this.chbUniquePhrases);
			this.matchingGroupBox.Controls.Add(this.btnDefault);
			this.matchingGroupBox.Controls.Add(this.cbMatchingFar);
			this.matchingGroupBox.Location = new System.Drawing.Point(228, 14);
			this.matchingGroupBox.Name = "matchingGroupBox";
			this.matchingGroupBox.Size = new System.Drawing.Size(238, 68);
			this.matchingGroupBox.TabIndex = 21;
			this.matchingGroupBox.TabStop = false;
			this.matchingGroupBox.Text = "Matcher properties";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 24;
			this.label3.Text = "Matching FAR:";
			// 
			// chbUniquePhrases
			// 
			this.chbUniquePhrases.AutoSize = true;
			this.chbUniquePhrases.Checked = true;
			this.chbUniquePhrases.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbUniquePhrases.Location = new System.Drawing.Point(92, 48);
			this.chbUniquePhrases.Name = "chbUniquePhrases";
			this.chbUniquePhrases.Size = new System.Drawing.Size(122, 17);
			this.chbUniquePhrases.TabIndex = 23;
			this.chbUniquePhrases.Text = "Unique phrases only";
			this.chbUniquePhrases.UseVisualStyleBackColor = true;
			this.chbUniquePhrases.CheckedChanged += new System.EventHandler(this.ChbUniquePhrasesCheckedChanged);
			// 
			// btnDefault
			// 
			this.btnDefault.Location = new System.Drawing.Point(171, 19);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(63, 23);
			this.btnDefault.TabIndex = 20;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.BtnDefaultClick);
			// 
			// cbMatchingFar
			// 
			this.cbMatchingFar.FormattingEnabled = true;
			this.cbMatchingFar.Location = new System.Drawing.Point(92, 21);
			this.cbMatchingFar.Name = "cbMatchingFar";
			this.cbMatchingFar.Size = new System.Drawing.Size(73, 21);
			this.cbMatchingFar.TabIndex = 19;
			this.cbMatchingFar.Leave += new System.EventHandler(this.CbMatchingFarLeave);
			this.cbMatchingFar.Enter += new System.EventHandler(this.CbMatchingFarEnter);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.lblFileForIdentification);
			this.groupBox2.Controls.Add(this.btnOpenAudio);
			this.groupBox2.Location = new System.Drawing.Point(6, 114);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(469, 53);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Voice template / file for identification";
			// 
			// lblFileForIdentification
			// 
			this.lblFileForIdentification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFileForIdentification.Location = new System.Drawing.Point(125, 29);
			this.lblFileForIdentification.Name = "lblFileForIdentification";
			this.lblFileForIdentification.Size = new System.Drawing.Size(338, 13);
			this.lblFileForIdentification.TabIndex = 10;
			this.lblFileForIdentification.Text = "file for identification";
			// 
			// btnOpenAudio
			// 
			this.btnOpenAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenAudio.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenAudio.Image")));
			this.btnOpenAudio.Location = new System.Drawing.Point(9, 24);
			this.btnOpenAudio.Name = "btnOpenAudio";
			this.btnOpenAudio.Size = new System.Drawing.Size(110, 23);
			this.btnOpenAudio.TabIndex = 8;
			this.btnOpenAudio.Text = "Open";
			this.btnOpenAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpenAudio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenAudio.UseVisualStyleBackColor = true;
			this.btnOpenAudio.Click += new System.EventHandler(this.BtnOpenAudioFileClick);
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listView.Location = new System.Drawing.Point(9, 88);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(457, 91);
			this.listView.TabIndex = 2;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ID";
			this.columnHeader1.Width = 300;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Score";
			this.columnHeader2.Width = 100;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.matchingGroupBox);
			this.groupBox3.Controls.Add(this.listView);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.btnIdentify);
			this.groupBox3.Location = new System.Drawing.Point(6, 173);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(472, 185);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Identification";
			// 
			// btnIdentify
			// 
			this.btnIdentify.Enabled = false;
			this.btnIdentify.Location = new System.Drawing.Point(9, 19);
			this.btnIdentify.Name = "btnIdentify";
			this.btnIdentify.Size = new System.Drawing.Size(110, 23);
			this.btnIdentify.TabIndex = 0;
			this.btnIdentify.Text = "&Identify";
			this.btnIdentify.UseVisualStyleBackColor = true;
			this.btnIdentify.Click += new System.EventHandler(this.BtnIdentifyClick);
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.VoiceMatching,Biometrics.VoiceExtraction";
			this.licensePanel.Size = new System.Drawing.Size(472, 45);
			this.licensePanel.TabIndex = 26;
			// 
			// IdentifyVoice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Name = "IdentifyVoice";
			this.Size = new System.Drawing.Size(478, 361);
			this.Load += new System.EventHandler(this.IdentifyVoiceLoad);
			this.VisibleChanged += new System.EventHandler(this.IdentifyVoiceVisibleChanged);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.matchingGroupBox.ResumeLayout(false);
			this.matchingGroupBox.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblTemplatesCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpenTemplates;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.ComboBox cbMatchingFar;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblFileForIdentification;
		private System.Windows.Forms.Button btnOpenAudio;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnIdentify;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private LicensePanel licensePanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chbUniquePhrases;
	}
}
