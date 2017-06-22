namespace Neurotec.Samples
{
	partial class EnrollFromMicrophone
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollFromMicrophone));
			this.gbMicrophones = new System.Windows.Forms.GroupBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.lbMicrophones = new System.Windows.Forms.ListBox();
			this.gbOptions = new System.Windows.Forms.GroupBox();
			this.chbExtractTextIndependent = new System.Windows.Forms.CheckBox();
			this.chbExtractTextDependent = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudPhraseId = new System.Windows.Forms.NumericUpDown();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.btnSaveVoice = new System.Windows.Forms.Button();
			this.saveVoiceFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.voiceView = new Neurotec.Biometrics.Gui.NVoiceView();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.btnForce = new System.Windows.Forms.Button();
			this.gbMicrophones.SuspendLayout();
			this.gbOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPhraseId)).BeginInit();
			this.SuspendLayout();
			// 
			// gbMicrophones
			// 
			this.gbMicrophones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbMicrophones.Controls.Add(this.btnForce);
			this.gbMicrophones.Controls.Add(this.btnStop);
			this.gbMicrophones.Controls.Add(this.btnRefresh);
			this.gbMicrophones.Controls.Add(this.btnStart);
			this.gbMicrophones.Controls.Add(this.lbMicrophones);
			this.gbMicrophones.Location = new System.Drawing.Point(0, 76);
			this.gbMicrophones.Name = "gbMicrophones";
			this.gbMicrophones.Size = new System.Drawing.Size(331, 110);
			this.gbMicrophones.TabIndex = 13;
			this.gbMicrophones.TabStop = false;
			this.gbMicrophones.Text = "Microphones list";
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(168, 80);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 11;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(6, 80);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 10;
			this.btnRefresh.Text = "Refresh list";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.BtnRefreshClick);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(87, 80);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 9;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// lbMicrophones
			// 
			this.lbMicrophones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbMicrophones.Location = new System.Drawing.Point(4, 19);
			this.lbMicrophones.Name = "lbMicrophones";
			this.lbMicrophones.Size = new System.Drawing.Size(321, 56);
			this.lbMicrophones.TabIndex = 6;
			this.lbMicrophones.SelectedIndexChanged += new System.EventHandler(this.LbMicrophonesSelectedIndexChanged);
			// 
			// gbOptions
			// 
			this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gbOptions.Controls.Add(this.chbCaptureAutomatically);
			this.gbOptions.Controls.Add(this.chbExtractTextIndependent);
			this.gbOptions.Controls.Add(this.chbExtractTextDependent);
			this.gbOptions.Controls.Add(this.label1);
			this.gbOptions.Controls.Add(this.nudPhraseId);
			this.gbOptions.Location = new System.Drawing.Point(337, 76);
			this.gbOptions.Name = "gbOptions";
			this.gbOptions.Size = new System.Drawing.Size(189, 110);
			this.gbOptions.TabIndex = 20;
			this.gbOptions.TabStop = false;
			this.gbOptions.Text = "Options";
			// 
			// chbExtractTextIndependent
			// 
			this.chbExtractTextIndependent.AutoSize = true;
			this.chbExtractTextIndependent.Checked = true;
			this.chbExtractTextIndependent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbExtractTextIndependent.Location = new System.Drawing.Point(6, 66);
			this.chbExtractTextIndependent.Name = "chbExtractTextIndependent";
			this.chbExtractTextIndependent.Size = new System.Drawing.Size(182, 17);
			this.chbExtractTextIndependent.TabIndex = 21;
			this.chbExtractTextIndependent.Text = "Extract text independent features";
			this.chbExtractTextIndependent.UseVisualStyleBackColor = true;
			this.chbExtractTextIndependent.CheckedChanged += new System.EventHandler(this.ChbExtractTextIndependentCheckedChanged);
			// 
			// chbExtractTextDependent
			// 
			this.chbExtractTextDependent.AutoSize = true;
			this.chbExtractTextDependent.Checked = true;
			this.chbExtractTextDependent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbExtractTextDependent.Location = new System.Drawing.Point(6, 43);
			this.chbExtractTextDependent.Name = "chbExtractTextDependent";
			this.chbExtractTextDependent.Size = new System.Drawing.Size(174, 17);
			this.chbExtractTextDependent.TabIndex = 20;
			this.chbExtractTextDependent.Text = "Extract text dependent features";
			this.chbExtractTextDependent.UseVisualStyleBackColor = true;
			this.chbExtractTextDependent.CheckedChanged += new System.EventHandler(this.ChbExtractTextDependentCheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Phrase Id:";
			// 
			// nudPhraseId
			// 
			this.nudPhraseId.Location = new System.Drawing.Point(64, 17);
			this.nudPhraseId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudPhraseId.Name = "nudPhraseId";
			this.nudPhraseId.Size = new System.Drawing.Size(93, 20);
			this.nudPhraseId.TabIndex = 18;
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveTemplate.Image")));
			this.btnSaveTemplate.Location = new System.Drawing.Point(1, 252);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(110, 23);
			this.btnSaveTemplate.TabIndex = 17;
			this.btnSaveTemplate.Text = "Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(-3, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(224, 13);
			this.label5.TabIndex = 22;
			this.label5.Text = "Select microphone, press start and say phrase";
			// 
			// btnSaveVoice
			// 
			this.btnSaveVoice.Enabled = false;
			this.btnSaveVoice.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveVoice.Image")));
			this.btnSaveVoice.Location = new System.Drawing.Point(121, 252);
			this.btnSaveVoice.Name = "btnSaveVoice";
			this.btnSaveVoice.Size = new System.Drawing.Size(125, 23);
			this.btnSaveVoice.TabIndex = 26;
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
			// voiceView
			// 
			this.voiceView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.voiceView.BackColor = System.Drawing.Color.Transparent;
			this.voiceView.Location = new System.Drawing.Point(0, 192);
			this.voiceView.Name = "voiceView";
			this.voiceView.Size = new System.Drawing.Size(523, 54);
			this.voiceView.TabIndex = 29;
			this.voiceView.Text = "voiceView";
			this.voiceView.Voice = null;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Devices.Microphones,Biometrics.VoiceExtraction";
			this.licensePanel.Size = new System.Drawing.Size(533, 45);
			this.licensePanel.TabIndex = 25;
			// 
			// chbCaptureAutomatically
			// 
			this.chbCaptureAutomatically.AutoSize = true;
			this.chbCaptureAutomatically.Checked = true;
			this.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(6, 86);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 22;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// btnForce
			// 
			this.btnForce.Location = new System.Drawing.Point(249, 80);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(75, 23);
			this.btnForce.TabIndex = 12;
			this.btnForce.Text = "Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// EnrollFromMicrophone
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.voiceView);
			this.Controls.Add(this.btnSaveVoice);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.gbOptions);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.gbMicrophones);
			this.Name = "EnrollFromMicrophone";
			this.Size = new System.Drawing.Size(533, 333);
			this.Load += new System.EventHandler(this.EnrollFromMicrophoneLoad);
			this.VisibleChanged += new System.EventHandler(this.EnrollFromMicrophoneVisibleChanged);
			this.gbMicrophones.ResumeLayout(false);
			this.gbOptions.ResumeLayout(false);
			this.gbOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPhraseId)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gbMicrophones;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ListBox lbMicrophones;
		private System.Windows.Forms.GroupBox gbOptions;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudPhraseId;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private LicensePanel licensePanel;
		private System.Windows.Forms.Button btnSaveVoice;
		private System.Windows.Forms.SaveFileDialog saveVoiceFileDialog;
		private System.Windows.Forms.CheckBox chbExtractTextIndependent;
		private System.Windows.Forms.CheckBox chbExtractTextDependent;
		private Neurotec.Biometrics.Gui.NVoiceView voiceView;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private System.Windows.Forms.Button btnForce;
	}
}
