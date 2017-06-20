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
			if (disposing)
			{
				if (_template != null) _template.Dispose();
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
			this.fingerView = new Neurotec.Biometrics.Gui.NFingerView();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFingersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addIrisesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addPalmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addVoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.addFingersFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFacesFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addIrisesFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addPalmsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addVoicesFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.addFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addIrisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addPalmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addVoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.addFingerFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFaceFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addIrisFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addPalmFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addVoiceFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.saveItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nfRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nTemplateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nlRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.neRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nfTemplateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nlTemplateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.neTemplateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nTemplateOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nfTemplateOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nlTemplateOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.neTemplateOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nfRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nlRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.neRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nsTemplateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nsRecordSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.nsRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nsTemplateOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nViewZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.leftSplitContainer.Panel1.SuspendLayout();
			this.leftSplitContainer.Panel2.SuspendLayout();
			this.leftSplitContainer.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.mainSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
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
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(243, 301);
			this.propertyGrid.TabIndex = 0;
			this.propertyGrid.ToolbarVisible = false;
			// 
			// fingerView
			// 
			this.fingerView.AutoScroll = true;
			this.fingerView.BackColor = System.Drawing.SystemColors.Control;
			this.fingerView.BoundingRectColor = System.Drawing.Color.Red;
			this.fingerView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fingerView.Location = new System.Drawing.Point(3, 3);
			this.fingerView.MinutiaColor = System.Drawing.Color.Red;
			this.fingerView.Name = "fingerView";
			this.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.fingerView.ResultImageColor = System.Drawing.Color.Green;
			this.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.fingerView.SingularPointColor = System.Drawing.Color.Red;
			this.fingerView.Size = new System.Drawing.Size(476, 454);
			this.fingerView.TabIndex = 0;
			this.fingerView.TreeColor = System.Drawing.Color.Crimson;
			this.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.fingerView.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.fingerView.TreeWidth = 2;
			this.fingerView.ZoomToFit = false;
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
			this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.saveToolStripMenuItem.Text = "&Save...";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// fileToolStripSeparator1
			// 
			this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
			this.fileToolStripSeparator1.Size = new System.Drawing.Size(152, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFingersToolStripMenuItem,
            this.addFacesToolStripMenuItem,
            this.addIrisesToolStripMenuItem,
            this.addPalmsToolStripMenuItem,
            this.addVoicesToolStripMenuItem,
            this.editToolStripSeparator1,
            this.addFingersFromFileToolStripMenuItem,
            this.addFacesFromFileToolStripMenuItem,
            this.addIrisesFromFileToolStripMenuItem,
            this.addPalmsFromFileToolStripMenuItem,
            this.addVoicesFromFileToolStripMenuItem,
            this.editToolStripSeparator2,
            this.addFingerToolStripMenuItem,
            this.addFaceToolStripMenuItem,
            this.addIrisToolStripMenuItem,
            this.addPalmToolStripMenuItem,
            this.addVoiceToolStripMenuItem,
            this.editToolStripSeparator3,
            this.addFingerFromFileToolStripMenuItem,
            this.addFaceFromFileToolStripMenuItem,
            this.addIrisFromFileToolStripMenuItem,
            this.addPalmFromFileToolStripMenuItem,
            this.addVoiceFromFileToolStripMenuItem,
            this.editToolStripSeparator4,
            this.removeToolStripMenuItem,
            this.editToolStripSeparator5,
            this.saveItemToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
			// 
			// addFingersToolStripMenuItem
			// 
			this.addFingersToolStripMenuItem.Name = "addFingersToolStripMenuItem";
			this.addFingersToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFingersToolStripMenuItem.Text = "Add fingers";
			this.addFingersToolStripMenuItem.Click += new System.EventHandler(this.addFingersToolStripMenuItem_Click);
			// 
			// addFacesToolStripMenuItem
			// 
			this.addFacesToolStripMenuItem.Name = "addFacesToolStripMenuItem";
			this.addFacesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFacesToolStripMenuItem.Text = "Add faces";
			this.addFacesToolStripMenuItem.Click += new System.EventHandler(this.addFacesToolStripMenuItem_Click);
			// 
			// addIrisesToolStripMenuItem
			// 
			this.addIrisesToolStripMenuItem.Name = "addIrisesToolStripMenuItem";
			this.addIrisesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addIrisesToolStripMenuItem.Text = "Add irises";
			this.addIrisesToolStripMenuItem.Click += new System.EventHandler(this.addIrisesToolStripMenuItem_Click);
			// 
			// addPalmsToolStripMenuItem
			// 
			this.addPalmsToolStripMenuItem.Name = "addPalmsToolStripMenuItem";
			this.addPalmsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addPalmsToolStripMenuItem.Text = "Add palms";
			this.addPalmsToolStripMenuItem.Click += new System.EventHandler(this.addPalmsToolStripMenuItem_Click);
			// 
			// addVoicesToolStripMenuItem
			// 
			this.addVoicesToolStripMenuItem.Name = "addVoicesToolStripMenuItem";
			this.addVoicesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addVoicesToolStripMenuItem.Text = "Add voices";
			this.addVoicesToolStripMenuItem.Click += new System.EventHandler(this.addVoicesToolStripMenuItem_Click);
			// 
			// editToolStripSeparator1
			// 
			this.editToolStripSeparator1.Name = "editToolStripSeparator1";
			this.editToolStripSeparator1.Size = new System.Drawing.Size(189, 6);
			// 
			// addFingersFromFileToolStripMenuItem
			// 
			this.addFingersFromFileToolStripMenuItem.Name = "addFingersFromFileToolStripMenuItem";
			this.addFingersFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFingersFromFileToolStripMenuItem.Text = "Add fingers from file...";
			this.addFingersFromFileToolStripMenuItem.Click += new System.EventHandler(this.addFingersFromFileToolStripMenuItem_Click);
			// 
			// addFacesFromFileToolStripMenuItem
			// 
			this.addFacesFromFileToolStripMenuItem.Name = "addFacesFromFileToolStripMenuItem";
			this.addFacesFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFacesFromFileToolStripMenuItem.Text = "Add faces from file...";
			this.addFacesFromFileToolStripMenuItem.Click += new System.EventHandler(this.addFacesFromFileToolStripMenuItem_Click);
			// 
			// addIrisesFromFileToolStripMenuItem
			// 
			this.addIrisesFromFileToolStripMenuItem.Name = "addIrisesFromFileToolStripMenuItem";
			this.addIrisesFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addIrisesFromFileToolStripMenuItem.Text = "Add irises from file...";
			this.addIrisesFromFileToolStripMenuItem.Click += new System.EventHandler(this.addIrisesFromFileToolStripMenuItem_Click);
			// 
			// addPalmsFromFileToolStripMenuItem
			// 
			this.addPalmsFromFileToolStripMenuItem.Name = "addPalmsFromFileToolStripMenuItem";
			this.addPalmsFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addPalmsFromFileToolStripMenuItem.Text = "Add palms from file...";
			this.addPalmsFromFileToolStripMenuItem.Click += new System.EventHandler(this.addPalmsFromFileToolStripMenuItem_Click);
			// 
			// addVoicesFromFileToolStripMenuItem
			// 
			this.addVoicesFromFileToolStripMenuItem.Name = "addVoicesFromFileToolStripMenuItem";
			this.addVoicesFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addVoicesFromFileToolStripMenuItem.Text = "Add voices from file...";
			this.addVoicesFromFileToolStripMenuItem.Click += new System.EventHandler(this.addVoicesFromFileToolStripMenuItem_Click);
			// 
			// editToolStripSeparator2
			// 
			this.editToolStripSeparator2.Name = "editToolStripSeparator2";
			this.editToolStripSeparator2.Size = new System.Drawing.Size(189, 6);
			// 
			// addFingerToolStripMenuItem
			// 
			this.addFingerToolStripMenuItem.Name = "addFingerToolStripMenuItem";
			this.addFingerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFingerToolStripMenuItem.Text = "Add finger...";
			this.addFingerToolStripMenuItem.Click += new System.EventHandler(this.addFingerToolStripMenuItem_Click);
			// 
			// addFaceToolStripMenuItem
			// 
			this.addFaceToolStripMenuItem.Name = "addFaceToolStripMenuItem";
			this.addFaceToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFaceToolStripMenuItem.Text = "Add face...";
			this.addFaceToolStripMenuItem.Click += new System.EventHandler(this.addFaceToolStripMenuItem_Click);
			// 
			// addIrisToolStripMenuItem
			// 
			this.addIrisToolStripMenuItem.Name = "addIrisToolStripMenuItem";
			this.addIrisToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addIrisToolStripMenuItem.Text = "Add iris...";
			this.addIrisToolStripMenuItem.Click += new System.EventHandler(this.addIrisToolStripMenuItem_Click);
			// 
			// addPalmToolStripMenuItem
			// 
			this.addPalmToolStripMenuItem.Name = "addPalmToolStripMenuItem";
			this.addPalmToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addPalmToolStripMenuItem.Text = "Add palm...";
			this.addPalmToolStripMenuItem.Click += new System.EventHandler(this.addPalmToolStripMenuItem_Click);
			// 
			// addVoiceToolStripMenuItem
			// 
			this.addVoiceToolStripMenuItem.Name = "addVoiceToolStripMenuItem";
			this.addVoiceToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addVoiceToolStripMenuItem.Text = "Add voice...";
			this.addVoiceToolStripMenuItem.Click += new System.EventHandler(this.addVoiceToolStripMenuItem_Click);
			// 
			// editToolStripSeparator3
			// 
			this.editToolStripSeparator3.Name = "editToolStripSeparator3";
			this.editToolStripSeparator3.Size = new System.Drawing.Size(189, 6);
			// 
			// addFingerFromFileToolStripMenuItem
			// 
			this.addFingerFromFileToolStripMenuItem.Name = "addFingerFromFileToolStripMenuItem";
			this.addFingerFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFingerFromFileToolStripMenuItem.Text = "Add finger from file...";
			this.addFingerFromFileToolStripMenuItem.Click += new System.EventHandler(this.addFingerFromFileToolStripMenuItem_Click);
			// 
			// addFaceFromFileToolStripMenuItem
			// 
			this.addFaceFromFileToolStripMenuItem.Name = "addFaceFromFileToolStripMenuItem";
			this.addFaceFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addFaceFromFileToolStripMenuItem.Text = "Add face from file...";
			this.addFaceFromFileToolStripMenuItem.Click += new System.EventHandler(this.addFaceFromFileToolStripMenuItem_Click);
			// 
			// addIrisFromFileToolStripMenuItem
			// 
			this.addIrisFromFileToolStripMenuItem.Name = "addIrisFromFileToolStripMenuItem";
			this.addIrisFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addIrisFromFileToolStripMenuItem.Text = "Add iris from file...";
			this.addIrisFromFileToolStripMenuItem.Click += new System.EventHandler(this.addIrisFromFileToolStripMenuItem_Click);
			// 
			// addPalmFromFileToolStripMenuItem
			// 
			this.addPalmFromFileToolStripMenuItem.Name = "addPalmFromFileToolStripMenuItem";
			this.addPalmFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addPalmFromFileToolStripMenuItem.Text = "Add palm from file...";
			this.addPalmFromFileToolStripMenuItem.Click += new System.EventHandler(this.addPalmFromFileToolStripMenuItem_Click);
			// 
			// addVoiceFromFileToolStripMenuItem
			// 
			this.addVoiceFromFileToolStripMenuItem.Name = "addVoiceFromFileToolStripMenuItem";
			this.addVoiceFromFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addVoiceFromFileToolStripMenuItem.Text = "Add voice from file...";
			this.addVoiceFromFileToolStripMenuItem.Click += new System.EventHandler(this.addVoiceFromFileToolStripMenuItem_Click);
			// 
			// editToolStripSeparator4
			// 
			this.editToolStripSeparator4.Name = "editToolStripSeparator4";
			this.editToolStripSeparator4.Size = new System.Drawing.Size(189, 6);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.removeToolStripMenuItem.Text = "&Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// editToolStripSeparator5
			// 
			this.editToolStripSeparator5.Name = "editToolStripSeparator5";
			this.editToolStripSeparator5.Size = new System.Drawing.Size(189, 6);
			// 
			// saveItemToolStripMenuItem
			// 
			this.saveItemToolStripMenuItem.Name = "saveItemToolStripMenuItem";
			this.saveItemToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.saveItemToolStripMenuItem.Text = "&Save item...";
			this.saveItemToolStripMenuItem.Click += new System.EventHandler(this.saveItemToolStripMenuItem_Click);
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
			// nfRecordSaveFileDialog
			// 
			this.nfRecordSaveFileDialog.Filter = "NFRecord Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nTemplateSaveFileDialog
			// 
			this.nTemplateSaveFileDialog.Filter = "NTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nlRecordSaveFileDialog
			// 
			this.nlRecordSaveFileDialog.Filter = "NLRecord Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// neRecordSaveFileDialog
			// 
			this.neRecordSaveFileDialog.Filter = "NERecord Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nfTemplateSaveFileDialog
			// 
			this.nfTemplateSaveFileDialog.Filter = "NFTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nlTemplateSaveFileDialog
			// 
			this.nlTemplateSaveFileDialog.Filter = "NLTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// neTemplateSaveFileDialog
			// 
			this.neTemplateSaveFileDialog.Filter = "NETemplate Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nTemplateOpenFileDialog
			// 
			this.nTemplateOpenFileDialog.Filter = resources.GetString("nTemplateOpenFileDialog.Filter");
			// 
			// nfTemplateOpenFileDialog
			// 
			this.nfTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFTemplate Files (*.dat)|*.dat|NFRecord Files (" +
				"*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nlTemplateOpenFileDialog
			// 
			this.nlTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NLTemplate Files (*.dat)|*.dat|NLRecord Files (" +
				"*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// neTemplateOpenFileDialog
			// 
			this.neTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NETemplate Files (*.dat)|*.dat|NERecord Files (" +
				"*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nfRecordOpenFileDialog
			// 
			this.nfRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// nlRecordOpenFileDialog
			// 
			this.nlRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NLRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// neRecordOpenFileDialog
			// 
			this.neRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NERecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// nsTemplateSaveFileDialog
			// 
			this.nsTemplateSaveFileDialog.Filter = "NSTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nsRecordSaveFileDialog
			// 
			this.nsRecordSaveFileDialog.Filter = "NSRecord Files (*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nsRecordOpenFileDialog
			// 
			this.nsRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NERecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// nsTemplateOpenFileDialog
			// 
			this.nsTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFTemplate Files (*.dat)|*.dat|NFRecord Files (" +
				"*.dat)|*.dat|All Files (*.*)|*.*";
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider.Location = new System.Drawing.Point(3, 463);
			this.nViewZoomSlider.Name = "nViewZoomSlider1";
			this.nViewZoomSlider.Size = new System.Drawing.Size(253, 23);
			this.nViewZoomSlider.TabIndex = 1;
			this.nViewZoomSlider.Text = "nViewZoomSlider";
			this.nViewZoomSlider.View = this.fingerView;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.fingerView, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 489);
			this.tableLayoutPanel1.TabIndex = 2;
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
			this.Text = "NTemplate Editor";
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			this.mainSplitContainer.ResumeLayout(false);
			this.leftSplitContainer.Panel1.ResumeLayout(false);
			this.leftSplitContainer.Panel2.ResumeLayout(false);
			this.leftSplitContainer.ResumeLayout(false);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
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
		private System.Windows.Forms.SaveFileDialog nfRecordSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog nTemplateSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog nlRecordSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog neRecordSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog nfTemplateSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog nlTemplateSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog neTemplateSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog nTemplateOpenFileDialog;
		private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog nfTemplateOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog nlTemplateOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog neTemplateOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog nfRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog nlRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog neRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
		private System.Windows.Forms.SaveFileDialog nsTemplateSaveFileDialog;
		private System.Windows.Forms.SaveFileDialog nsRecordSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog nsRecordOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog nsTemplateOpenFileDialog;
		private Neurotec.Biometrics.Gui.NFingerView fingerView;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFingersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFacesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addIrisesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addPalmsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addVoicesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator editToolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem addFingersFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFacesFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addIrisesFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addPalmsFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addVoicesFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator editToolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem addFingerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addIrisToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addPalmToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addVoiceToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator editToolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem addFingerFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFaceFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addIrisFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addPalmFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addVoiceFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator editToolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator editToolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem saveItemToolStripMenuItem;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}

