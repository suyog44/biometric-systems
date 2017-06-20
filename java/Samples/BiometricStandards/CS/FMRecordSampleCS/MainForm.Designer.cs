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
			this.newFMRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.openToolStripMenuItemCbeff = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.edToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFingerViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.activeToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pointerToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFeatureToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fmRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fmRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.fmView = new Neurotec.Biometrics.Standards.Gui.FMView();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnPointerTool = new System.Windows.Forms.ToolStripButton();
			this.btnAddFeatureTool = new System.Windows.Forms.ToolStripButton();
			this.btnDeleteFeature = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.cbeffRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cbeffRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainMenuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
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
			this.treeView.Size = new System.Drawing.Size(246, 271);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(246, 243);
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
            this.newFMRecordToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.openToolStripMenuItemCbeff,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newFMRecordToolStripMenuItem
			// 
			this.newFMRecordToolStripMenuItem.Name = "newFMRecordToolStripMenuItem";
			this.newFMRecordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newFMRecordToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.newFMRecordToolStripMenuItem.Text = "&New FMRecord ...";
			this.newFMRecordToolStripMenuItem.Click += new System.EventHandler(this.newFMRecordToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openToolStripMenuItem.Text = "&Open FMRecord ...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.saveAsToolStripMenuItem.Enabled = false;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.saveAsToolStripMenuItem.Text = "&Save FMRecord ...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
			// 
			// openToolStripMenuItemCbeff
			// 
			this.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff";
			this.openToolStripMenuItemCbeff.Size = new System.Drawing.Size(215, 22);
			this.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ...";
			this.openToolStripMenuItemCbeff.Click += new System.EventHandler(this.openToolStripMenuItemCbeff_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// edToolStripMenuItem
			// 
			this.edToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFingerViewToolStripMenuItem,
            this.removeFingerToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.activeToolToolStripMenuItem,
            this.convertToolStripMenuItem});
			this.edToolStripMenuItem.Name = "edToolStripMenuItem";
			this.edToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.edToolStripMenuItem.Text = "&Edit";
			// 
			// addFingerViewToolStripMenuItem
			// 
			this.addFingerViewToolStripMenuItem.Enabled = false;
			this.addFingerViewToolStripMenuItem.Name = "addFingerViewToolStripMenuItem";
			this.addFingerViewToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
			this.addFingerViewToolStripMenuItem.Text = "A&dd finger view";
			this.addFingerViewToolStripMenuItem.Click += new System.EventHandler(this.addFingerViewToolStripMenuItem_Click);
			// 
			// removeFingerToolStripMenuItem
			// 
			this.removeFingerToolStripMenuItem.Enabled = false;
			this.removeFingerToolStripMenuItem.Name = "removeFingerToolStripMenuItem";
			this.removeFingerToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
			this.removeFingerToolStripMenuItem.Text = "&Remove finger view";
			this.removeFingerToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Enabled = false;
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete selected minutia/core/delta";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(295, 6);
			// 
			// activeToolToolStripMenuItem
			// 
			this.activeToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointerToolToolStripMenuItem,
            this.addFeatureToolToolStripMenuItem});
			this.activeToolToolStripMenuItem.Name = "activeToolToolStripMenuItem";
			this.activeToolToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
			this.activeToolToolStripMenuItem.Text = "Active &tool";
			// 
			// pointerToolToolStripMenuItem
			// 
			this.pointerToolToolStripMenuItem.Name = "pointerToolToolStripMenuItem";
			this.pointerToolToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.pointerToolToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.pointerToolToolStripMenuItem.Text = "&Pointer tool";
			this.pointerToolToolStripMenuItem.Click += new System.EventHandler(this.pointerToolToolStripMenuItem_Click);
			// 
			// addFeatureToolToolStripMenuItem
			// 
			this.addFeatureToolToolStripMenuItem.Name = "addFeatureToolToolStripMenuItem";
			this.addFeatureToolToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.addFeatureToolToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.addFeatureToolToolStripMenuItem.Text = "Add &feature tool";
			this.addFeatureToolToolStripMenuItem.Click += new System.EventHandler(this.addFeatureToolToolStripMenuItem_Click);
			// 
			// convertToolStripMenuItem
			// 
			this.convertToolStripMenuItem.Enabled = false;
			this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
			this.convertToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
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
			// fmRecordOpenFileDialog
			// 
			this.fmRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fmRecordOpenFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*";
			// 
			// fmRecordSaveFileDialog
			// 
			this.fmRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			this.fmRecordSaveFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*";
			// 
			// fmView
			// 
			this.fmView.BackColor = System.Drawing.SystemColors.ControlLight;
			this.fmView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fmView.DrawFeatureArea = true;
			this.fmView.FeatureAreaColor = System.Drawing.Color.LightGray;
			this.fmView.Location = new System.Drawing.Point(0, 0);
			this.fmView.MinutiaColor = System.Drawing.Color.Red;
			this.fmView.Name = "fmView";
			this.fmView.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.fmView.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.fmView.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.fmView.SingularPointColor = System.Drawing.Color.Red;
			this.fmView.Size = new System.Drawing.Size(694, 518);
			this.fmView.TabIndex = 6;
			this.fmView.TreeColor = System.Drawing.Color.Crimson;
			this.fmView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.fmView.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.fmView.TreeWidth = 2;
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Enabled = false;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnPointerTool,
            this.btnAddFeatureTool,
            this.btnDeleteFeature});
			this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip.Location = new System.Drawing.Point(243, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(76, 23);
			this.toolStrip.TabIndex = 7;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// btnPointerTool
			// 
			this.btnPointerTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPointerTool.Image = global::Neurotec.Samples.Properties.Resources.Pointer;
			this.btnPointerTool.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPointerTool.Name = "btnPointerTool";
			this.btnPointerTool.Size = new System.Drawing.Size(23, 20);
			this.btnPointerTool.Text = "Pointer tool - Use it to move or rotate details. (Ctrl + P)";
			this.btnPointerTool.Click += new System.EventHandler(this.btnPointerTool_Click);
			// 
			// btnAddFeatureTool
			// 
			this.btnAddFeatureTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAddFeatureTool.Image = global::Neurotec.Samples.Properties.Resources.AddFeatrue;
			this.btnAddFeatureTool.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAddFeatureTool.Name = "btnAddFeatureTool";
			this.btnAddFeatureTool.Size = new System.Drawing.Size(23, 20);
			this.btnAddFeatureTool.Text = "Add Feature tool - Add new minutiae, cores or deltas. (Ctrl + F)";
			this.btnAddFeatureTool.Click += new System.EventHandler(this.btnAddFeatureTool_Click);
			// 
			// btnDeleteFeature
			// 
			this.btnDeleteFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDeleteFeature.Enabled = false;
			this.btnDeleteFeature.Image = global::Neurotec.Samples.Properties.Resources.Delete;
			this.btnDeleteFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDeleteFeature.Name = "btnDeleteFeature";
			this.btnDeleteFeature.Size = new System.Drawing.Size(23, 20);
			this.btnDeleteFeature.Text = "Delete selected - delete unwanted minutiae, cores or deltas. (Ctrl + D)";
			this.btnDeleteFeature.Click += new System.EventHandler(this.btnDeleteSelected_Click);
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
			this.splitContainer1.Panel2.Controls.Add(this.fmView);
			this.splitContainer1.Size = new System.Drawing.Size(944, 518);
			this.splitContainer1.SplitterDistance = 246;
			this.splitContainer1.TabIndex = 8;
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
			this.splitContainer2.Size = new System.Drawing.Size(246, 518);
			this.splitContainer2.SplitterDistance = 271;
			this.splitContainer2.TabIndex = 0;
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
			this.ClientSize = new System.Drawing.Size(944, 542);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.mainMenuStrip);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FMRecord Editor";
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
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
		private System.Windows.Forms.OpenFileDialog fmRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog fmRecordSaveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeFingerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem activeToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pointerToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFeatureToolToolStripMenuItem;
		private Neurotec.Biometrics.Standards.Gui.FMView fmView;
		private System.Windows.Forms.ToolStripMenuItem newFMRecordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFingerViewToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton btnPointerTool;
		private System.Windows.Forms.ToolStripButton btnAddFeatureTool;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton btnDeleteFeature;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItemCbeff;
		private System.Windows.Forms.OpenFileDialog cbeffRecordOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog cbeffRecordSaveFileDialog;
	}
}

