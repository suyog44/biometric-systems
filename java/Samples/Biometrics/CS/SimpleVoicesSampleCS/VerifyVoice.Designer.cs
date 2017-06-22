namespace Neurotec.Samples
{
	partial class VerifyVoice
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyVoice));
			this.btnDefault = new System.Windows.Forms.Button();
			this.matchingFarLabel = new System.Windows.Forms.Label();
			this.lblSecondTemplate = new System.Windows.Forms.Label();
			this.lblFirstTemplate = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnVerify = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpen1 = new System.Windows.Forms.Button();
			this.btnOpen2 = new System.Windows.Forms.Button();
			this.gbMatching = new System.Windows.Forms.GroupBox();
			this.chbUniquePhrases = new System.Windows.Forms.CheckBox();
			this.cbMatchingFAR = new System.Windows.Forms.ComboBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.lblMsg = new System.Windows.Forms.Label();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.gbMatching.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnDefault
			// 
			this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDefault.Location = new System.Drawing.Point(161, 11);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(63, 23);
			this.btnDefault.TabIndex = 20;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.BtnDefaultClick);
			// 
			// matchingFarLabel
			// 
			this.matchingFarLabel.AutoSize = true;
			this.matchingFarLabel.Location = new System.Drawing.Point(6, 16);
			this.matchingFarLabel.Name = "matchingFarLabel";
			this.matchingFarLabel.Size = new System.Drawing.Size(78, 13);
			this.matchingFarLabel.TabIndex = 18;
			this.matchingFarLabel.Text = "Matching &FAR:";
			// 
			// lblSecondTemplate
			// 
			this.lblSecondTemplate.AutoSize = true;
			this.lblSecondTemplate.Location = new System.Drawing.Point(201, 155);
			this.lblSecondTemplate.Name = "lblSecondTemplate";
			this.lblSecondTemplate.Size = new System.Drawing.Size(85, 13);
			this.lblSecondTemplate.TabIndex = 50;
			this.lblSecondTemplate.Text = "second template";
			// 
			// lblFirstTemplate
			// 
			this.lblFirstTemplate.AutoSize = true;
			this.lblFirstTemplate.Location = new System.Drawing.Point(201, 126);
			this.lblFirstTemplate.Name = "lblFirstTemplate";
			this.lblFirstTemplate.Size = new System.Drawing.Size(66, 13);
			this.lblFirstTemplate.TabIndex = 49;
			this.lblFirstTemplate.Text = "first template";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(48, 155);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(147, 13);
			this.label3.TabIndex = 48;
			this.label3.Text = "Second template or audio file:";
			// 
			// btnVerify
			// 
			this.btnVerify.Enabled = false;
			this.btnVerify.Location = new System.Drawing.Point(12, 179);
			this.btnVerify.Name = "btnVerify";
			this.btnVerify.Size = new System.Drawing.Size(121, 23);
			this.btnVerify.TabIndex = 46;
			this.btnVerify.Text = "Verify";
			this.btnVerify.UseVisualStyleBackColor = true;
			this.btnVerify.Click += new System.EventHandler(this.BtnVerifyClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(48, 126);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129, 13);
			this.label1.TabIndex = 47;
			this.label1.Text = "First template or audio file:";
			// 
			// btnOpen1
			// 
			this.btnOpen1.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen1.Image")));
			this.btnOpen1.Location = new System.Drawing.Point(12, 121);
			this.btnOpen1.Name = "btnOpen1";
			this.btnOpen1.Size = new System.Drawing.Size(30, 23);
			this.btnOpen1.TabIndex = 21;
			this.btnOpen1.UseVisualStyleBackColor = true;
			this.btnOpen1.Click += new System.EventHandler(this.BtnOpen1Click);
			// 
			// btnOpen2
			// 
			this.btnOpen2.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen2.Image")));
			this.btnOpen2.Location = new System.Drawing.Point(12, 150);
			this.btnOpen2.Name = "btnOpen2";
			this.btnOpen2.Size = new System.Drawing.Size(30, 23);
			this.btnOpen2.TabIndex = 22;
			this.btnOpen2.UseVisualStyleBackColor = true;
			this.btnOpen2.Click += new System.EventHandler(this.BtnOpen2Click);
			// 
			// gbMatching
			// 
			this.gbMatching.Controls.Add(this.chbUniquePhrases);
			this.gbMatching.Controls.Add(this.btnDefault);
			this.gbMatching.Controls.Add(this.matchingFarLabel);
			this.gbMatching.Controls.Add(this.cbMatchingFAR);
			this.gbMatching.Location = new System.Drawing.Point(3, 52);
			this.gbMatching.MaximumSize = new System.Drawing.Size(600, 200);
			this.gbMatching.Name = "gbMatching";
			this.gbMatching.Size = new System.Drawing.Size(237, 63);
			this.gbMatching.TabIndex = 32;
			this.gbMatching.TabStop = false;
			this.gbMatching.Text = "Matcher properties";
			// 
			// chbUniquePhrases
			// 
			this.chbUniquePhrases.AutoSize = true;
			this.chbUniquePhrases.Checked = true;
			this.chbUniquePhrases.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbUniquePhrases.Location = new System.Drawing.Point(90, 40);
			this.chbUniquePhrases.Name = "chbUniquePhrases";
			this.chbUniquePhrases.Size = new System.Drawing.Size(122, 17);
			this.chbUniquePhrases.TabIndex = 22;
			this.chbUniquePhrases.Text = "Unique phrases only";
			this.chbUniquePhrases.UseVisualStyleBackColor = true;
			this.chbUniquePhrases.CheckedChanged += new System.EventHandler(this.chbUniquePhrasesCheckedChanged);
			// 
			// cbMatchingFAR
			// 
			this.cbMatchingFAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbMatchingFAR.FormattingEnabled = true;
			this.cbMatchingFAR.Location = new System.Drawing.Point(90, 13);
			this.cbMatchingFAR.Name = "cbMatchingFAR";
			this.cbMatchingFAR.Size = new System.Drawing.Size(65, 21);
			this.cbMatchingFAR.TabIndex = 19;
			this.cbMatchingFAR.Leave += new System.EventHandler(this.CbMatchingFARLeave);
			this.cbMatchingFAR.Enter += new System.EventHandler(this.CbMatchingFAREnter);
			// 
			// lblMsg
			// 
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new System.Drawing.Point(9, 220);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(33, 13);
			this.lblMsg.TabIndex = 45;
			this.lblMsg.Text = "score";
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
			this.licensePanel.Size = new System.Drawing.Size(438, 45);
			this.licensePanel.TabIndex = 51;
			// 
			// VerifyVoice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.gbMatching);
			this.Controls.Add(this.btnOpen2);
			this.Controls.Add(this.btnOpen1);
			this.Controls.Add(this.lblSecondTemplate);
			this.Controls.Add(this.lblFirstTemplate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnVerify);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblMsg);
			this.Name = "VerifyVoice";
			this.Size = new System.Drawing.Size(444, 246);
			this.Load += new System.EventHandler(this.VerifyVoiceLoad);
			this.VisibleChanged += new System.EventHandler(this.VerifyVoiceVisibleChanged);
			this.gbMatching.ResumeLayout(false);
			this.gbMatching.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Label matchingFarLabel;
		private System.Windows.Forms.Label lblSecondTemplate;
		private System.Windows.Forms.Label lblFirstTemplate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnVerify;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpen1;
		private System.Windows.Forms.Button btnOpen2;
		private System.Windows.Forms.GroupBox gbMatching;
		private System.Windows.Forms.ComboBox cbMatchingFAR;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label lblMsg;
		private LicensePanel licensePanel;
		private System.Windows.Forms.CheckBox chbUniquePhrases;
	}
}
