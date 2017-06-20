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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbNewSubject = new System.Windows.Forms.ToolStripButton();
			this.tsbOpenSubject = new System.Windows.Forms.ToolStripButton();
			this.tsbGetSubject = new System.Windows.Forms.ToolStripButton();
			this.tsbSettings = new System.Windows.Forms.ToolStripButton();
			this.tbsChangeDatabase = new System.Windows.Forms.ToolStripButton();
			this.tsbAbout = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tabControl = new Neurotec.Samples.CloseableTabControl();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewSubject,
            this.tsbOpenSubject,
            this.tsbGetSubject,
            this.tsbSettings,
            this.tbsChangeDatabase,
            this.tsbAbout});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbNewSubject
			// 
			this.tsbNewSubject.Image = global::Neurotec.Samples.Properties.Resources.NewDocumentHS;
			this.tsbNewSubject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNewSubject.Name = "tsbNewSubject";
			this.tsbNewSubject.Size = new System.Drawing.Size(93, 22);
			this.tsbNewSubject.Text = "&New Subject";
			this.tsbNewSubject.Click += new System.EventHandler(this.TsbNewSubjectClick);
			// 
			// tsbOpenSubject
			// 
			this.tsbOpenSubject.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenSubject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenSubject.Name = "tsbOpenSubject";
			this.tsbOpenSubject.Size = new System.Drawing.Size(98, 22);
			this.tsbOpenSubject.Text = "&Open Subject";
			this.tsbOpenSubject.Click += new System.EventHandler(this.TsbOpenSubjectClick);
			// 
			// tsbGetSubject
			// 
			this.tsbGetSubject.Image = global::Neurotec.Samples.Properties.Resources.Get;
			this.tsbGetSubject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbGetSubject.Name = "tsbGetSubject";
			this.tsbGetSubject.Size = new System.Drawing.Size(87, 22);
			this.tsbGetSubject.Text = "&Get Subject";
			this.tsbGetSubject.Click += new System.EventHandler(this.TsbGetSubjectClick);
			// 
			// tsbSettings
			// 
			this.tsbSettings.Image = global::Neurotec.Samples.Properties.Resources.settings;
			this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSettings.Name = "tsbSettings";
			this.tsbSettings.Size = new System.Drawing.Size(69, 22);
			this.tsbSettings.Text = "&Settings";
			this.tsbSettings.Click += new System.EventHandler(this.TsbSettingsClick);
			// 
			// tbsChangeDatabase
			// 
			this.tbsChangeDatabase.Image = global::Neurotec.Samples.Properties.Resources.newSource;
			this.tbsChangeDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbsChangeDatabase.Name = "tbsChangeDatabase";
			this.tbsChangeDatabase.Size = new System.Drawing.Size(118, 22);
			this.tbsChangeDatabase.Text = "&Change database";
			this.tbsChangeDatabase.Click += new System.EventHandler(this.TbsChangeDatabaseClick);
			// 
			// tsbAbout
			// 
			this.tsbAbout.Image = global::Neurotec.Samples.Properties.Resources.Help;
			this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new System.Drawing.Size(60, 22);
			this.tsbAbout.Text = "&About";
			this.tsbAbout.Click += new System.EventHandler(this.TsbAboutClick);
			// 
			// tabControl
			// 
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControl.LastPageIndex = -1;
			this.tabControl.Location = new System.Drawing.Point(0, 25);
			this.tabControl.Name = "tabControl";
			this.tabControl.Padding = new System.Drawing.Point(16, 3);
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1008, 704);
			this.tabControl.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(550, 310);
			this.Name = "MainForm";
			this.Text = "Multibiometric Sample";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Shown += new System.EventHandler(this.MainFormShown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbNewSubject;
		private System.Windows.Forms.ToolStripButton tsbOpenSubject;
		private System.Windows.Forms.ToolStripButton tsbSettings;
		private System.Windows.Forms.ToolStripButton tsbAbout;
		private Neurotec.Samples.CloseableTabControl tabControl;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripButton tbsChangeDatabase;
		private System.Windows.Forms.ToolStripButton tsbGetSubject;

	}
}

