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
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.fcView = new Neurotec.Biometrics.Standards.Gui.FCView();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.openToolStripMenuItemCbeff = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFaceFromImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFaceFromRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveFaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFaceAsDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fcRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.fcRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.rawImageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveImageFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.saveRawFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.cbeffRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cbeffRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.leftSplitContainer.Panel1.SuspendLayout();
			this.leftSplitContainer.Panel2.SuspendLayout();
			this.leftSplitContainer.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainSplitContainer
			// 
			this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
			this.mainSplitContainer.Name = "mainSplitContainer";
			// 
			// mainSplitContainer.Panel1
			// 
			this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
			// 
			// mainSplitContainer.Panel2
			// 
			this.mainSplitContainer.Panel2.Controls.Add(this.fcView);
			this.mainSplitContainer.Size = new System.Drawing.Size(729, 489);
			this.mainSplitContainer.SplitterDistance = 243;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// leftSplitContainer
			// 
			this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.leftSplitContainer.Name = "leftSplitContainer";
			this.leftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// leftSplitContainer.Panel1
			// 
			this.leftSplitContainer.Panel1.Controls.Add(this.treeView);
			// 
			// leftSplitContainer.Panel2
			// 
			this.leftSplitContainer.Panel2.Controls.Add(this.propertyGrid);
			this.leftSplitContainer.Size = new System.Drawing.Size(243, 489);
			this.leftSplitContainer.SplitterDistance = 184;
			this.leftSplitContainer.TabIndex = 0;
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(243, 184);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(243, 301);
			this.propertyGrid.TabIndex = 0;
			// 
			// fcView
			// 
			this.fcView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fcView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.fcView.FeatureColor = System.Drawing.Color.Red;
			this.fcView.Location = new System.Drawing.Point(0, 0);
			this.fcView.Name = "fcView";
			this.fcView.Record = null;
			this.fcView.Size = new System.Drawing.Size(482, 489);
			this.fcView.TabIndex = 0;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(729, 24);
			this.mainMenuStrip.TabIndex = 1;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator4,
            this.openToolStripMenuItemCbeff,
            this.fileToolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.newToolStripMenuItem.Text = "&New ...";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.openToolStripMenuItem.Text = "&Open ...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.saveToolStripMenuItem.Text = "&Save ...";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
			// 
			// openToolStripMenuItemCbeff
			// 
			this.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff";
			this.openToolStripMenuItemCbeff.Size = new System.Drawing.Size(184, 22);
			this.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ...";
			this.openToolStripMenuItemCbeff.Click += new System.EventHandler(this.openToolStripMenuItemCbeff_Click);
			// 
			// fileToolStripSeparator1
			// 
			this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
			this.fileToolStripSeparator1.Size = new System.Drawing.Size(181, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFaceFromImageToolStripMenuItem,
            this.addFaceFromRawToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveFaceToolStripMenuItem,
            this.saveFaceAsDataToolStripMenuItem,
            this.toolStripSeparator3,
            this.convertToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// addFaceFromImageToolStripMenuItem
			// 
			this.addFaceFromImageToolStripMenuItem.Enabled = false;
			this.addFaceFromImageToolStripMenuItem.Name = "addFaceFromImageToolStripMenuItem";
			this.addFaceFromImageToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.addFaceFromImageToolStripMenuItem.Text = "&Add face from image ...";
			this.addFaceFromImageToolStripMenuItem.Click += new System.EventHandler(this.addFaceFromImageToolStripMenuItem_Click);
			// 
			// addFaceFromRawToolStripMenuItem
			// 
			this.addFaceFromRawToolStripMenuItem.Enabled = false;
			this.addFaceFromRawToolStripMenuItem.Name = "addFaceFromRawToolStripMenuItem";
			this.addFaceFromRawToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.addFaceFromRawToolStripMenuItem.Text = "A&dd face from data ...";
			this.addFaceFromRawToolStripMenuItem.Click += new System.EventHandler(this.addFaceFromRawToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Enabled = false;
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.removeToolStripMenuItem.Text = "&Remove face ...";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
			// 
			// saveFaceToolStripMenuItem
			// 
			this.saveFaceToolStripMenuItem.Enabled = false;
			this.saveFaceToolStripMenuItem.Name = "saveFaceToolStripMenuItem";
			this.saveFaceToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.saveFaceToolStripMenuItem.Text = "&Save face as image ...";
			this.saveFaceToolStripMenuItem.Click += new System.EventHandler(this.saveFaceToolStripMenuItem_Click);
			// 
			// saveFaceAsDataToolStripMenuItem
			// 
			this.saveFaceAsDataToolStripMenuItem.Enabled = false;
			this.saveFaceAsDataToolStripMenuItem.Name = "saveFaceAsDataToolStripMenuItem";
			this.saveFaceAsDataToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.saveFaceAsDataToolStripMenuItem.Text = "Sa&ve face as data ...";
			this.saveFaceAsDataToolStripMenuItem.Click += new System.EventHandler(this.saveFaceAsDataToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
			// 
			// convertToolStripMenuItem
			// 
			this.convertToolStripMenuItem.Enabled = false;
			this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
			this.convertToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
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
			// fcRecordSaveFileDialog
			// 
			this.fcRecordSaveFileDialog.Filter = "FCRecord Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// fcRecordOpenFileDialog
			// 
			this.fcRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// rawImageOpenFileDialog
			// 
			this.rawImageOpenFileDialog.Filter = resources.GetString("rawImageOpenFileDialog.Filter");
			// 
			// saveRawFileDialog
			// 
			this.saveRawFileDialog.Filter = "JPEG Files (*.jpg;*.jpeg;*.jpe;*.jif;*.jfif;*.jfi;*.jpb;*.jpl)|*.jpg;*.jpeg;*.jpe" +
				";*.jif;*.jfif;*.jfi;*.jpb;*.jpl|JPEG 2000 Files (*.jp2)|*.jp2";
			// 
			// cbeffRecordOpenFileDialog
			// 
			this.cbeffRecordOpenFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// cbeffRecordSaveFileDialog
			// 
			this.cbeffRecordSaveFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(729, 513);
			this.Controls.Add(this.mainSplitContainer);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FCRecord Editor";
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			this.mainSplitContainer.ResumeLayout(false);
			this.leftSplitContainer.Panel1.ResumeLayout(false);
			this.leftSplitContainer.Panel2.ResumeLayout(false);
			this.leftSplitContainer.ResumeLayout(false);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainSplitContainer;
		private System.Windows.Forms.SplitContainer leftSplitContainer;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog fcRecordSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog fcRecordOpenFileDialog;
		private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
		private Neurotec.Biometrics.Standards.Gui.FCView fcView;
		private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFaceFromImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFaceFromRawToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.OpenFileDialog rawImageOpenFileDialog;
		private System.Windows.Forms.ToolStripMenuItem saveFaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveFaceAsDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.SaveFileDialog saveImageFileDialog;
		private System.Windows.Forms.SaveFileDialog saveRawFileDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemCbeff;
		private System.Windows.Forms.OpenFileDialog cbeffRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog cbeffRecordSaveFileDialog;
	}
}

