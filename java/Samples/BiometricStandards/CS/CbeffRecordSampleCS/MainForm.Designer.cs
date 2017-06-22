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
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSelectedStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.edToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addCbeffRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.InsertBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InsertAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.removeBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.addFCRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFIRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFMRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addIIRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fmRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fmRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.cbeffRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cbeffRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.fcRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fiRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.iiRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fcRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.iiRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.fiRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainMenuStrip.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.edToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(394, 24);
			this.mainMenuStrip.TabIndex = 3;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator4,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveAsToolStripMenuItem,
            this.saveSelectedStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "&Save";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// saveSelectedStripMenuItem
			// 
			this.saveSelectedStripMenuItem.Name = "saveSelectedStripMenuItem";
			this.saveSelectedStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveSelectedStripMenuItem.Text = "&Save Selected";
			this.saveSelectedStripMenuItem.Click += new System.EventHandler(this.saveSelectedStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// edToolStripMenuItem
			// 
			this.edToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCbeffRecordToolStripMenuItem,
            this.addFromFileToolStripMenuItem,
            this.toolStripSeparator5,
            this.InsertBeforeToolStripMenuItem,
            this.InsertAfterToolStripMenuItem,
            this.toolStripSeparator3,
            this.removeBranchToolStripMenuItem,
            this.toolStripSeparator6,
            this.addFCRecordToolStripMenuItem,
            this.addFIRecordToolStripMenuItem,
            this.addFMRecordToolStripMenuItem,
            this.addIIRecordToolStripMenuItem});
			this.edToolStripMenuItem.Name = "edToolStripMenuItem";
			this.edToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.edToolStripMenuItem.Text = "&Edit";
			// 
			// addCbeffRecordToolStripMenuItem
			// 
			this.addCbeffRecordToolStripMenuItem.Enabled = false;
			this.addCbeffRecordToolStripMenuItem.Name = "addCbeffRecordToolStripMenuItem";
			this.addCbeffRecordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addCbeffRecordToolStripMenuItem.Text = "&Add CbeffRecord";
			this.addCbeffRecordToolStripMenuItem.Click += new System.EventHandler(this.addCbeffRecordToolStripMenuItem_Click);
			// 
			// addFromFileToolStripMenuItem
			// 
			this.addFromFileToolStripMenuItem.Enabled = false;
			this.addFromFileToolStripMenuItem.Name = "addFromFileToolStripMenuItem";
			this.addFromFileToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addFromFileToolStripMenuItem.Text = "&Add CbeffRecord from file";
			this.addFromFileToolStripMenuItem.Click += new System.EventHandler(this.AddFromFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(210, 6);
			// 
			// InsertBeforeToolStripMenuItem
			// 
			this.InsertBeforeToolStripMenuItem.Enabled = false;
			this.InsertBeforeToolStripMenuItem.Name = "InsertBeforeToolStripMenuItem";
			this.InsertBeforeToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.InsertBeforeToolStripMenuItem.Text = "&Insert CbeffRecord before";
			this.InsertBeforeToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.InsertBeforeToolStripMenuItem.Click += new System.EventHandler(this.InsertBeforeToolStripMenuItem_Click);
			// 
			// InsertAfterToolStripMenuItem
			// 
			this.InsertAfterToolStripMenuItem.Enabled = false;
			this.InsertAfterToolStripMenuItem.Name = "InsertAfterToolStripMenuItem";
			this.InsertAfterToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.InsertAfterToolStripMenuItem.Text = "&Insert CbeffRecord after";
			this.InsertAfterToolStripMenuItem.Click += new System.EventHandler(this.InsertAfterToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
			// 
			// removeBranchToolStripMenuItem
			// 
			this.removeBranchToolStripMenuItem.Enabled = false;
			this.removeBranchToolStripMenuItem.Name = "removeBranchToolStripMenuItem";
			this.removeBranchToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.removeBranchToolStripMenuItem.Text = "&Remove Item";
			this.removeBranchToolStripMenuItem.Click += new System.EventHandler(this.removeBranchToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(210, 6);
			// 
			// addFCRecordToolStripMenuItem
			// 
			this.addFCRecordToolStripMenuItem.Enabled = false;
			this.addFCRecordToolStripMenuItem.Name = "addFCRecordToolStripMenuItem";
			this.addFCRecordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addFCRecordToolStripMenuItem.Text = "&Add FCRecord from file";
			this.addFCRecordToolStripMenuItem.Click += new System.EventHandler(this.addFCRecordToolStripMenuItem_Click);
			// 
			// addFIRecordToolStripMenuItem
			// 
			this.addFIRecordToolStripMenuItem.Enabled = false;
			this.addFIRecordToolStripMenuItem.Name = "addFIRecordToolStripMenuItem";
			this.addFIRecordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addFIRecordToolStripMenuItem.Text = "&Add FIRecord from file";
			this.addFIRecordToolStripMenuItem.Click += new System.EventHandler(this.addFIRecordToolStripMenuItem_Click);
			// 
			// addFMRecordToolStripMenuItem
			// 
			this.addFMRecordToolStripMenuItem.Enabled = false;
			this.addFMRecordToolStripMenuItem.Name = "addFMRecordToolStripMenuItem";
			this.addFMRecordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addFMRecordToolStripMenuItem.Text = "&Add FMRecord from file";
			this.addFMRecordToolStripMenuItem.Click += new System.EventHandler(this.addFMRecordToolStripMenuItem_Click);
			// 
			// addIIRecordToolStripMenuItem
			// 
			this.addIIRecordToolStripMenuItem.Enabled = false;
			this.addIIRecordToolStripMenuItem.Name = "addIIRecordToolStripMenuItem";
			this.addIIRecordToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.addIIRecordToolStripMenuItem.Text = "&Add IIRecord from file";
			this.addIIRecordToolStripMenuItem.Click += new System.EventHandler(this.addIIRecordToolStripMenuItem_Click);
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
			// fmRecordOpenFileDialog
			// 
			this.fmRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fmRecordOpenFileDialog.Filter = "FMRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// fmRecordSaveFileDialog
			// 
			this.fmRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fmRecordSaveFileDialog.Filter = "FMRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// cbeffRecordOpenFileDialog
			// 
			this.cbeffRecordOpenFileDialog.Filter = "CbeffRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// cbeffRecordSaveFileDialog
			// 
			this.cbeffRecordSaveFileDialog.DefaultExt = "CbeffRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 24);
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
			this.splitContainer2.Size = new System.Drawing.Size(394, 617);
			this.splitContainer2.SplitterDistance = 322;
			this.splitContainer2.TabIndex = 4;
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(394, 322);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(394, 291);
			this.propertyGrid.TabIndex = 2;
			// 
			// fcRecordOpenFileDialog
			// 
			this.fcRecordOpenFileDialog.DefaultExt = "FCRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// fiRecordOpenFileDialog
			// 
			this.fiRecordOpenFileDialog.Filter = "FIRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// iiRecordOpenFileDialog
			// 
			this.iiRecordOpenFileDialog.Filter = "IIRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// fcRecordSaveFileDialog
			// 
			this.fcRecordSaveFileDialog.Filter = "FCRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// iiRecordSaveFileDialog
			// 
			this.iiRecordSaveFileDialog.Filter = "IIRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// fiRecordSaveFileDialog
			// 
			this.fiRecordSaveFileDialog.Filter = "FIRecord |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 641);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.mainMenuStrip);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CbeffRecordSampleCS";
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem edToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog fmRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog fmRecordSaveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog cbeffRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog cbeffRecordSaveFileDialog;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeBranchToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem addCbeffRecordToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem addFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveSelectedStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem InsertBeforeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem InsertAfterToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem addFCRecordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFIRecordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFMRecordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addIIRecordToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog fcRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog fiRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog iiRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog fcRecordSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog iiRecordSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog fiRecordSaveFileDialog;
	}
}

