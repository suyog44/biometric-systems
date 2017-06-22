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
			this.tabPageEnrollFromFile = new System.Windows.Forms.TabPage();
			this.enrollFromFilePanel = new Neurotec.Samples.EnrollFromFile();
			this.tabPageEnrollFromMicrophone = new System.Windows.Forms.TabPage();
			this.enrollFromMicrophonePanel = new Neurotec.Samples.EnrollFromMicrophone();
			this.tabPageVerify = new System.Windows.Forms.TabPage();
			this.verifyVoicePanel = new Neurotec.Samples.VerifyVoice();
			this.tabPageIdentify = new System.Windows.Forms.TabPage();
			this.identifyVoicePanel = new Neurotec.Samples.IdentifyVoice();
			this.tabControl1.SuspendLayout();
			this.tabPageEnrollFromFile.SuspendLayout();
			this.tabPageEnrollFromMicrophone.SuspendLayout();
			this.tabPageVerify.SuspendLayout();
			this.tabPageIdentify.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageEnrollFromFile);
			this.tabControl1.Controls.Add(this.tabPageEnrollFromMicrophone);
			this.tabControl1.Controls.Add(this.tabPageVerify);
			this.tabControl1.Controls.Add(this.tabPageIdentify);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(551, 398);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
			// 
			// tabPageEnrollFromFile
			// 
			this.tabPageEnrollFromFile.Controls.Add(this.enrollFromFilePanel);
			this.tabPageEnrollFromFile.Location = new System.Drawing.Point(4, 22);
			this.tabPageEnrollFromFile.Name = "tabPageEnrollFromFile";
			this.tabPageEnrollFromFile.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEnrollFromFile.Size = new System.Drawing.Size(546, 322);
			this.tabPageEnrollFromFile.TabIndex = 0;
			this.tabPageEnrollFromFile.Text = "Enroll From File";
			this.tabPageEnrollFromFile.UseVisualStyleBackColor = true;
			// 
			// enrollFromFilePanel
			// 
			this.enrollFromFilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.enrollFromFilePanel.Location = new System.Drawing.Point(3, 3);
			this.enrollFromFilePanel.Name = "enrollFromFilePanel";
			this.enrollFromFilePanel.Size = new System.Drawing.Size(540, 316);
			this.enrollFromFilePanel.TabIndex = 0;
			// 
			// tabPageEnrollFromMicrophone
			// 
			this.tabPageEnrollFromMicrophone.Controls.Add(this.enrollFromMicrophonePanel);
			this.tabPageEnrollFromMicrophone.Location = new System.Drawing.Point(4, 22);
			this.tabPageEnrollFromMicrophone.Name = "tabPageEnrollFromMicrophone";
			this.tabPageEnrollFromMicrophone.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEnrollFromMicrophone.Size = new System.Drawing.Size(546, 322);
			this.tabPageEnrollFromMicrophone.TabIndex = 1;
			this.tabPageEnrollFromMicrophone.Text = "Enroll From Microphone";
			this.tabPageEnrollFromMicrophone.UseVisualStyleBackColor = true;
			// 
			// enrollFromMicrophonePanel
			// 
			this.enrollFromMicrophonePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.enrollFromMicrophonePanel.Location = new System.Drawing.Point(3, 3);
			this.enrollFromMicrophonePanel.Name = "enrollFromMicrophonePanel";
			this.enrollFromMicrophonePanel.Size = new System.Drawing.Size(540, 316);
			this.enrollFromMicrophonePanel.TabIndex = 0;
			// 
			// tabPageVerify
			// 
			this.tabPageVerify.Controls.Add(this.verifyVoicePanel);
			this.tabPageVerify.Location = new System.Drawing.Point(4, 22);
			this.tabPageVerify.Name = "tabPageVerify";
			this.tabPageVerify.Size = new System.Drawing.Size(546, 322);
			this.tabPageVerify.TabIndex = 2;
			this.tabPageVerify.Text = "Verify Voice";
			this.tabPageVerify.UseVisualStyleBackColor = true;
			// 
			// verifyVoicePanel
			// 
			this.verifyVoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;

			this.verifyVoicePanel.Location = new System.Drawing.Point(0, 0);
			this.verifyVoicePanel.Name = "verifyVoicePanel";
			this.verifyVoicePanel.Size = new System.Drawing.Size(546, 322);
			this.verifyVoicePanel.TabIndex = 0;
			// 
			// tabPageIdentify
			// 
			this.tabPageIdentify.Controls.Add(this.identifyVoicePanel);
			this.tabPageIdentify.Location = new System.Drawing.Point(4, 22);
			this.tabPageIdentify.Name = "tabPageIdentify";
			this.tabPageIdentify.Size = new System.Drawing.Size(543, 372);
			this.tabPageIdentify.TabIndex = 3;
			this.tabPageIdentify.Text = "Identify Voice";
			this.tabPageIdentify.UseVisualStyleBackColor = true;
			// 
			// identifyVoicePanel
			// 
			this.identifyVoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.identifyVoicePanel.Location = new System.Drawing.Point(0, 0);
			this.identifyVoicePanel.Name = "identifyVoicePanel";
			this.identifyVoicePanel.Size = new System.Drawing.Size(543, 372);
			this.identifyVoicePanel.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(551, 398);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Simple Voices Sample";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabPageEnrollFromFile.ResumeLayout(false);
			this.tabPageEnrollFromMicrophone.ResumeLayout(false);
			this.tabPageVerify.ResumeLayout(false);
			this.tabPageIdentify.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageEnrollFromFile;
		private System.Windows.Forms.TabPage tabPageEnrollFromMicrophone;
		private System.Windows.Forms.TabPage tabPageVerify;
		private System.Windows.Forms.TabPage tabPageIdentify;
		private EnrollFromFile enrollFromFilePanel;
		private EnrollFromMicrophone enrollFromMicrophonePanel;
		private VerifyVoice verifyVoicePanel;
		private IdentifyVoice identifyVoicePanel;
	}
}

