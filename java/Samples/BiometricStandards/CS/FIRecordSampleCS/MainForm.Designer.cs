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
			this.treeView = new System.Windows.Forms.TreeView();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFingerAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.openToolStripMenuItemCbeff = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.edToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFingerViewFromImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveIngerAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fiRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fiRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.cbeffRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cbeffRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.imageSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.fiView = new Neurotec.Biometrics.Standards.Gui.FIView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.mainMenuStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(268, 205);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(268, 309);
			this.propertyGrid.TabIndex = 2;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.edToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(944, 24);
			this.mainMenuStrip.TabIndex = 3;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveFingerAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.openToolStripMenuItemCbeff,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.newToolStripMenuItem.Text = "&New ...";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.openToolStripMenuItem.Text = "&Open FIRecord ...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveFingerAsToolStripMenuItem
			// 
			this.saveFingerAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.saveFingerAsToolStripMenuItem.Enabled = false;
			this.saveFingerAsToolStripMenuItem.Name = "saveFingerAsToolStripMenuItem";
			this.saveFingerAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + S";
			this.saveFingerAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveFingerAsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.saveFingerAsToolStripMenuItem.Text = "&Save FIRecord ...";
			this.saveFingerAsToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
			// 
			// openToolStripMenuItemCbeff
			// 
			this.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff";
			this.openToolStripMenuItemCbeff.Size = new System.Drawing.Size(213, 22);
			this.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ...";
			this.openToolStripMenuItemCbeff.Click += new System.EventHandler(this.openToolStripMenuItemCbeff_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(210, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// edToolStripMenuItem
			// 
			this.edToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFingerViewFromImageToolStripMenuItem,
            this.removeFingerToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveIngerAsImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.convertToolStripMenuItem});
			this.edToolStripMenuItem.Name = "edToolStripMenuItem";
			this.edToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.edToolStripMenuItem.Text = "&Edit";
			// 
			// addFingerViewFromImageToolStripMenuItem
			// 
			this.addFingerViewFromImageToolStripMenuItem.Enabled = false;
			this.addFingerViewFromImageToolStripMenuItem.Name = "addFingerViewFromImageToolStripMenuItem";
			this.addFingerViewFromImageToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.addFingerViewFromImageToolStripMenuItem.Text = "Add finger view ...";
			this.addFingerViewFromImageToolStripMenuItem.Click += new System.EventHandler(this.addFingerViewFromImageToolStripMenuItem_Click);
			// 
			// removeFingerToolStripMenuItem
			// 
			this.removeFingerToolStripMenuItem.Enabled = false;
			this.removeFingerToolStripMenuItem.Name = "removeFingerToolStripMenuItem";
			this.removeFingerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.removeFingerToolStripMenuItem.Text = "&Remove finger view";
			this.removeFingerToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
			// 
			// saveIngerAsImageToolStripMenuItem
			// 
			this.saveIngerAsImageToolStripMenuItem.Enabled = false;
			this.saveIngerAsImageToolStripMenuItem.Name = "saveIngerAsImageToolStripMenuItem";
			this.saveIngerAsImageToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.saveIngerAsImageToolStripMenuItem.Text = "&Save finger view as image ...";
			this.saveIngerAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveFingerToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
			// 
			// convertToolStripMenuItem
			// 
			this.convertToolStripMenuItem.Enabled = false;
			this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
			this.convertToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.convertToolStripMenuItem.Text = "&Convert ...";
			this.convertToolStripMenuItem.Click += new System.EventHandler(this.convertToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// fiRecordOpenFileDialog
			// 
			this.fiRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fiRecordOpenFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*";
			// 
			// fiRecordSaveFileDialog
			// 
			this.fiRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fiRecordSaveFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*";
			// 
			// cbeffRecordOpenFileDialog
			// 
			this.cbeffRecordOpenFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// cbeffRecordSaveFileDialog
			// 
			this.cbeffRecordSaveFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// imageOpenFileDialog
			// 
			this.imageOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// imageSaveFileDialog
			// 
			this.imageSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// fiView
			// 
			this.fiView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fiView.FeatureColor = System.Drawing.Color.Red;
			this.fiView.Location = new System.Drawing.Point(0, 0);
			this.fiView.Name = "fiView";
			this.fiView.Record = null;
			this.fiView.Size = new System.Drawing.Size(672, 518);
			this.fiView.TabIndex = 4;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.fiView);
			this.splitContainer1.Size = new System.Drawing.Size(944, 518);
			this.splitContainer1.SplitterDistance = 268;
			this.splitContainer1.TabIndex = 3;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer2.Size = new System.Drawing.Size(268, 518);
			this.splitContainer2.SplitterDistance = 205;
			this.splitContainer2.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 542);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FIRecord Editor";
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem edToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog fiRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog fiRecordSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog imageSaveFileDialog;
		private Neurotec.Biometrics.Standards.Gui.FIView fiView;
		private System.Windows.Forms.ToolStripMenuItem saveFingerAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveIngerAsImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeFingerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem addFingerViewFromImageToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemCbeff;
		private System.Windows.Forms.OpenFileDialog cbeffRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog cbeffRecordSaveFileDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

