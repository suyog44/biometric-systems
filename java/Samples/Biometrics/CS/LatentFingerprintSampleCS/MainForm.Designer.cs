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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.contextZoomMenuRight = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiZoomInRight = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiZoomOutRight = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiZoomOriginalRight = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMatcher = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.contextZoomMenuLeft = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.originalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblLatentResolution = new System.Windows.Forms.Label();
			this.lblLatentSize = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbInvert = new System.Windows.Forms.CheckBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.leftPartPanel = new System.Windows.Forms.Panel();
			this.nfViewLeft = new Neurotec.Biometrics.Gui.NFingerView();
			this.contextMenuLeft = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDeleteFeature = new System.Windows.Forms.ToolStripMenuItem();
			this.tsLeft = new System.Windows.Forms.ToolStrip();
			this.tsbOpenLeft = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveTemplate = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbExtractLeft = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tscbZoomLeft = new System.Windows.Forms.ToolStripComboBox();
			this.tsbLeftZoomIn = new System.Windows.Forms.ToolStripButton();
			this.tsbLeftZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbView = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiViewOriginalLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiRotate90cw = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRotate90ccw = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRotate180 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFlipHorz = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFlipVert = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCropToSel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiInvertMinutiae = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
			this.performBandpassFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lblLeftFilename = new System.Windows.Forms.Label();
			this.rightSidePanel = new System.Windows.Forms.Panel();
			this.nfViewRight = new Neurotec.Biometrics.Gui.NFingerView();
			this.tsRight = new System.Windows.Forms.ToolStrip();
			this.tsbOpenRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tscbZoomRight = new System.Windows.Forms.ToolStripComboBox();
			this.tsbRightZoomIn = new System.Windows.Forms.ToolStripButton();
			this.tsbRightZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSplitButton3 = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiViewOriginalRight = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.lblRightFilename = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblBrightnessB = new System.Windows.Forms.Label();
			this.lblBrightnessG = new System.Windows.Forms.Label();
			this.lblBrightnessR = new System.Windows.Forms.Label();
			this.lblContrastBValue = new System.Windows.Forms.Label();
			this.lblContrastGValue = new System.Windows.Forms.Label();
			this.lblContrastRValue = new System.Windows.Forms.Label();
			this.btnResetAll = new System.Windows.Forms.Button();
			this.btnResetContrast = new System.Windows.Forms.Button();
			this.btnResetBrightness = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.cbGroupContrastSliders = new System.Windows.Forms.CheckBox();
			this.cbGrayscale = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.sliderBrightnessGreen = new Neurotec.Samples.ColorSlider();
			this.sliderContrastBlue = new Neurotec.Samples.ColorSlider();
			this.sliderBrightnessRed = new Neurotec.Samples.ColorSlider();
			this.cbGroupBrightnessSliders = new System.Windows.Forms.CheckBox();
			this.sliderContrastGreen = new Neurotec.Samples.ColorSlider();
			this.sliderBrightnessBlue = new Neurotec.Samples.ColorSlider();
			this.sliderContrastRed = new Neurotec.Samples.ColorSlider();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.rbAddDoubleCoreTool = new System.Windows.Forms.RadioButton();
			this.rbAddDeltaTool = new System.Windows.Forms.RadioButton();
			this.rbAddCoreTool = new System.Windows.Forms.RadioButton();
			this.rbAddBifurcationMinutia = new System.Windows.Forms.RadioButton();
			this.rbAddEndMinutiaTool = new System.Windows.Forms.RadioButton();
			this.rbSelectAreaTool = new System.Windows.Forms.RadioButton();
			this.rbPointerTool = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.saveTemplateDialog = new System.Windows.Forms.SaveFileDialog();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbUseEditedImage = new System.Windows.Forms.RadioButton();
			this.rbUseOriginalImage = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.matchingFarComboBox = new System.Windows.Forms.ComboBox();
			this.lblMatchingScore = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.lblReferenceResolution = new System.Windows.Forms.Label();
			this.lblReferenceSize = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSaveTemplate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSaveLatentImage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSaveReferenceImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExtractionSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
			zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextZoomMenuRight.SuspendLayout();
			this.contextZoomMenuLeft.SuspendLayout();
			this.panel2.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.leftPartPanel.SuspendLayout();
			this.contextMenuLeft.SuspendLayout();
			this.tsLeft.SuspendLayout();
			this.rightSidePanel.SuspendLayout();
			this.tsRight.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel7.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// zoomToolStripMenuItem1
			// 
			zoomToolStripMenuItem1.DropDown = this.contextZoomMenuRight;
			zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
			zoomToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
			zoomToolStripMenuItem1.Text = "&Zoom";
			// 
			// contextZoomMenuRight
			// 
			this.contextZoomMenuRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiZoomInRight,
            this.tsmiZoomOutRight,
            this.tsmiZoomOriginalRight});
			this.contextZoomMenuRight.Name = "contextZoomMenuLeft";
			this.contextZoomMenuRight.OwnerItem = zoomToolStripMenuItem1;
			this.contextZoomMenuRight.Size = new System.Drawing.Size(128, 70);
			// 
			// tsmiZoomInRight
			// 
			this.tsmiZoomInRight.Image = global::Neurotec.Samples.Properties.Resources.ZoomIn;
			this.tsmiZoomInRight.Name = "tsmiZoomInRight";
			this.tsmiZoomInRight.Size = new System.Drawing.Size(127, 22);
			this.tsmiZoomInRight.Text = "Zoom &in";
			this.tsmiZoomInRight.Click += new System.EventHandler(this.TsmiZoomInRightClick);
			// 
			// tsmiZoomOutRight
			// 
			this.tsmiZoomOutRight.Image = global::Neurotec.Samples.Properties.Resources.ZoomOut;
			this.tsmiZoomOutRight.Name = "tsmiZoomOutRight";
			this.tsmiZoomOutRight.Size = new System.Drawing.Size(127, 22);
			this.tsmiZoomOutRight.Text = "Zoom &out";
			this.tsmiZoomOutRight.Click += new System.EventHandler(this.TsmiZoomOutRightClick);
			// 
			// tsmiZoomOriginalRight
			// 
			this.tsmiZoomOriginalRight.Name = "tsmiZoomOriginalRight";
			this.tsmiZoomOriginalRight.Size = new System.Drawing.Size(127, 22);
			this.tsmiZoomOriginalRight.Text = "&Original";
			this.tsmiZoomOriginalRight.Click += new System.EventHandler(this.TsmiZoomOriginalRightClick);
			// 
			// btnMatcher
			// 
			this.btnMatcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMatcher.Enabled = false;
			this.btnMatcher.Location = new System.Drawing.Point(335, 7);
			this.btnMatcher.Name = "btnMatcher";
			this.btnMatcher.Size = new System.Drawing.Size(91, 23);
			this.btnMatcher.TabIndex = 0;
			this.btnMatcher.Text = "&Match";
			this.btnMatcher.UseVisualStyleBackColor = true;
			this.btnMatcher.Click += new System.EventHandler(this.BtnMatcherClick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// contextZoomMenuLeft
			// 
			this.contextZoomMenuLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.originalToolStripMenuItem});
			this.contextZoomMenuLeft.Name = "contextZoomMenuLeft";
			this.contextZoomMenuLeft.OwnerItem = this.zoomToolStripMenuItem2;
			this.contextZoomMenuLeft.Size = new System.Drawing.Size(128, 70);
			// 
			// zoomInToolStripMenuItem
			// 
			this.zoomInToolStripMenuItem.Image = global::Neurotec.Samples.Properties.Resources.ZoomIn;
			this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
			this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.zoomInToolStripMenuItem.Text = "Zoom &in";
			this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.TsmiZoomInLeftClick);
			// 
			// zoomOutToolStripMenuItem
			// 
			this.zoomOutToolStripMenuItem.Image = global::Neurotec.Samples.Properties.Resources.ZoomOut;
			this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
			this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.zoomOutToolStripMenuItem.Text = "Zoom &out";
			this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.TsmiZoomOutLeftClick);
			// 
			// originalToolStripMenuItem
			// 
			this.originalToolStripMenuItem.Name = "originalToolStripMenuItem";
			this.originalToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.originalToolStripMenuItem.Text = "&Original";
			this.originalToolStripMenuItem.Click += new System.EventHandler(this.TsmiZoomOriginalLeftClick);
			// 
			// zoomToolStripMenuItem
			// 
			this.zoomToolStripMenuItem.DropDown = this.contextZoomMenuLeft;
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.zoomToolStripMenuItem.Text = "&Zoom";
			// 
			// zoomToolStripMenuItem2
			// 
			this.zoomToolStripMenuItem2.DropDown = this.contextZoomMenuLeft;
			this.zoomToolStripMenuItem2.Name = "zoomToolStripMenuItem2";
			this.zoomToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
			this.zoomToolStripMenuItem2.Text = "Zoom";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.lblLatentResolution);
			this.panel2.Controls.Add(this.lblLatentSize);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(185, 59);
			this.panel2.TabIndex = 2;
			// 
			// lblLatentResolution
			// 
			this.lblLatentResolution.Location = new System.Drawing.Point(4, 37);
			this.lblLatentResolution.Name = "lblLatentResolution";
			this.lblLatentResolution.Size = new System.Drawing.Size(173, 18);
			this.lblLatentResolution.TabIndex = 3;
			this.lblLatentResolution.Text = "Resolution:";
			// 
			// lblLatentSize
			// 
			this.lblLatentSize.Location = new System.Drawing.Point(4, 22);
			this.lblLatentSize.Name = "lblLatentSize";
			this.lblLatentSize.Size = new System.Drawing.Size(173, 20);
			this.lblLatentSize.TabIndex = 2;
			this.lblLatentSize.Text = "Size:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.label2.Location = new System.Drawing.Point(4, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Latent image";
			// 
			// cbInvert
			// 
			this.cbInvert.AutoSize = true;
			this.cbInvert.Location = new System.Drawing.Point(10, 20);
			this.cbInvert.Name = "cbInvert";
			this.cbInvert.Size = new System.Drawing.Size(52, 17);
			this.cbInvert.TabIndex = 0;
			this.cbInvert.Text = "invert";
			this.cbInvert.UseVisualStyleBackColor = true;
			this.cbInvert.CheckedChanged += new System.EventHandler(this.CbInvertCheckedChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(88, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.leftPartPanel);
			this.splitContainer1.Panel1.Controls.Add(this.lblLeftFilename);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.rightSidePanel);
			this.splitContainer1.Panel2.Controls.Add(this.lblRightFilename);
			this.splitContainer1.Size = new System.Drawing.Size(804, 588);
			this.splitContainer1.SplitterDistance = 390;
			this.splitContainer1.TabIndex = 22;
			// 
			// leftPartPanel
			// 
			this.leftPartPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.leftPartPanel.Controls.Add(this.nfViewLeft);
			this.leftPartPanel.Controls.Add(this.tsLeft);
			this.leftPartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.leftPartPanel.Location = new System.Drawing.Point(0, 17);
			this.leftPartPanel.Name = "leftPartPanel";
			this.leftPartPanel.Size = new System.Drawing.Size(390, 571);
			this.leftPartPanel.TabIndex = 0;
			// 
			// nfViewLeft
			// 
			this.nfViewLeft.AutomaticRotateFlipImage = false;
			this.nfViewLeft.AutoScroll = true;
			this.nfViewLeft.BackColor = System.Drawing.SystemColors.Control;
			this.nfViewLeft.BoundingRectColor = System.Drawing.Color.Red;
			this.nfViewLeft.ContextMenuStrip = this.contextMenuLeft;
			this.nfViewLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nfViewLeft.Location = new System.Drawing.Point(0, 25);
			this.nfViewLeft.MinutiaColor = System.Drawing.Color.Red;
			this.nfViewLeft.Name = "nfViewLeft";
			this.nfViewLeft.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.nfViewLeft.ResultImageColor = System.Drawing.Color.Chartreuse;
			this.nfViewLeft.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.nfViewLeft.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.nfViewLeft.SingularPointColor = System.Drawing.Color.Red;
			this.nfViewLeft.Size = new System.Drawing.Size(386, 542);
			this.nfViewLeft.TabIndex = 0;
			this.nfViewLeft.Text = "nfView1";
			this.nfViewLeft.TreeColor = System.Drawing.Color.Crimson;
			this.nfViewLeft.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.nfViewLeft.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.nfViewLeft.TreeWidth = 3;
			this.nfViewLeft.ZoomToFit = false;
			this.nfViewLeft.SelectedTreeMinutiaIndexChanged += new System.EventHandler(this.NfView1IndexChanged);
			this.nfViewLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.NfViewLeftPaint);
			// 
			// contextMenuLeft
			// 
			this.contextMenuLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem2,
            this.toolStripSeparator4,
            this.tsmiDeleteFeature});
			this.contextMenuLeft.Name = "contextMenuLeft";
			this.contextMenuLeft.Size = new System.Drawing.Size(108, 54);
			this.contextMenuLeft.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuLeftOpening);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(104, 6);
			// 
			// tsmiDeleteFeature
			// 
			this.tsmiDeleteFeature.Name = "tsmiDeleteFeature";
			this.tsmiDeleteFeature.Size = new System.Drawing.Size(107, 22);
			this.tsmiDeleteFeature.Text = "Delete";
			this.tsmiDeleteFeature.Click += new System.EventHandler(this.TsmiDeleteFeatureClick);
			// 
			// tsLeft
			// 
			this.tsLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenLeft,
            this.tsbSaveTemplate,
            this.toolStripSeparator3,
            this.tsbExtractLeft,
            this.toolStripSeparator1,
            this.tscbZoomLeft,
            this.tsbLeftZoomIn,
            this.tsbLeftZoomOut,
            this.toolStripSeparator5,
            this.tsbView,
            this.toolStripSplitButton2});
			this.tsLeft.Location = new System.Drawing.Point(0, 0);
			this.tsLeft.Name = "tsLeft";
			this.tsLeft.Size = new System.Drawing.Size(386, 25);
			this.tsLeft.TabIndex = 1;
			// 
			// tsbOpenLeft
			// 
			this.tsbOpenLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenLeft.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenLeft.Name = "tsbOpenLeft";
			this.tsbOpenLeft.Size = new System.Drawing.Size(23, 22);
			this.tsbOpenLeft.Text = "Open";
			this.tsbOpenLeft.Click += new System.EventHandler(this.BtnLeftClick);
			// 
			// tsbSaveTemplate
			// 
			this.tsbSaveTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.tsbSaveTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveTemplate.Name = "tsbSaveTemplate";
			this.tsbSaveTemplate.Size = new System.Drawing.Size(23, 22);
			this.tsbSaveTemplate.Text = "Save template";
			this.tsbSaveTemplate.Click += new System.EventHandler(this.TsbSaveTemplateClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbExtractLeft
			// 
			this.tsbExtractLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbExtractLeft.Image = global::Neurotec.Samples.Properties.Resources.extract;
			this.tsbExtractLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExtractLeft.Name = "tsbExtractLeft";
			this.tsbExtractLeft.Size = new System.Drawing.Size(23, 22);
			this.tsbExtractLeft.Text = "Extract";
			this.tsbExtractLeft.Click += new System.EventHandler(this.TsbExtractLeftClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tscbZoomLeft
			// 
			this.tscbZoomLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tscbZoomLeft.Name = "tscbZoomLeft";
			this.tscbZoomLeft.Size = new System.Drawing.Size(75, 25);
			this.tscbZoomLeft.SelectedIndexChanged += new System.EventHandler(this.TscbZoomLeftSelectedIndexChanged);
			// 
			// tsbLeftZoomIn
			// 
			this.tsbLeftZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLeftZoomIn.Image = global::Neurotec.Samples.Properties.Resources.ZoomIn;
			this.tsbLeftZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLeftZoomIn.Name = "tsbLeftZoomIn";
			this.tsbLeftZoomIn.Size = new System.Drawing.Size(23, 22);
			this.tsbLeftZoomIn.Text = "Zoom In";
			this.tsbLeftZoomIn.Click += new System.EventHandler(this.TsmiZoomInLeftClick);
			// 
			// tsbLeftZoomOut
			// 
			this.tsbLeftZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLeftZoomOut.Image = global::Neurotec.Samples.Properties.Resources.ZoomOut;
			this.tsbLeftZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLeftZoomOut.Name = "tsbLeftZoomOut";
			this.tsbLeftZoomOut.Size = new System.Drawing.Size(23, 22);
			this.tsbLeftZoomOut.Text = "Zoom Out";
			this.tsbLeftZoomOut.Click += new System.EventHandler(this.TsmiZoomOutLeftClick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbView
			// 
			this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewOriginalLeft,
            this.toolStripMenuItem4,
            this.zoomToolStripMenuItem});
			this.tsbView.Image = ((System.Drawing.Image)(resources.GetObject("tsbView.Image")));
			this.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbView.Name = "tsbView";
			this.tsbView.Size = new System.Drawing.Size(45, 22);
			this.tsbView.Text = "&View";
			// 
			// tsmiViewOriginalLeft
			// 
			this.tsmiViewOriginalLeft.Checked = true;
			this.tsmiViewOriginalLeft.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiViewOriginalLeft.Name = "tsmiViewOriginalLeft";
			this.tsmiViewOriginalLeft.Size = new System.Drawing.Size(116, 22);
			this.tsmiViewOriginalLeft.Text = "&Original";
			this.tsmiViewOriginalLeft.Click += new System.EventHandler(this.TsmiViewOriginalLeftClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(113, 6);
			// 
			// toolStripSplitButton2
			// 
			this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRotate90cw,
            this.tsmiRotate90ccw,
            this.tsmiRotate180,
            this.toolStripMenuItem3,
            this.tsmiFlipHorz,
            this.tsmiFlipVert,
            this.toolStripMenuItem12,
            this.tsmiCropToSel,
            this.toolStripMenuItem13,
            this.tsmiInvertMinutiae,
            this.toolStripMenuItem14,
            this.performBandpassFilteringToolStripMenuItem});
			this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
			this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton2.Name = "toolStripSplitButton2";
			this.toolStripSplitButton2.Size = new System.Drawing.Size(75, 22);
			this.toolStripSplitButton2.Text = "Transform";
			// 
			// tsmiRotate90cw
			// 
			this.tsmiRotate90cw.Name = "tsmiRotate90cw";
			this.tsmiRotate90cw.Size = new System.Drawing.Size(228, 22);
			this.tsmiRotate90cw.Text = "Rotate 90? clockwise";
			this.tsmiRotate90cw.Click += new System.EventHandler(this.TsmiRotate90cwClick);
			// 
			// tsmiRotate90ccw
			// 
			this.tsmiRotate90ccw.Name = "tsmiRotate90ccw";
			this.tsmiRotate90ccw.Size = new System.Drawing.Size(228, 22);
			this.tsmiRotate90ccw.Text = "Rotate 90? counter-clockwise";
			this.tsmiRotate90ccw.Click += new System.EventHandler(this.TsmiRotate90ccwClick);
			// 
			// tsmiRotate180
			// 
			this.tsmiRotate180.Name = "tsmiRotate180";
			this.tsmiRotate180.Size = new System.Drawing.Size(228, 22);
			this.tsmiRotate180.Text = "Rotate 180?";
			this.tsmiRotate180.Click += new System.EventHandler(this.TsmiRotate180Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(225, 6);
			// 
			// tsmiFlipHorz
			// 
			this.tsmiFlipHorz.Name = "tsmiFlipHorz";
			this.tsmiFlipHorz.Size = new System.Drawing.Size(228, 22);
			this.tsmiFlipHorz.Text = "Flip Horizontally";
			this.tsmiFlipHorz.Click += new System.EventHandler(this.TsmiFlipHorzClick);
			// 
			// tsmiFlipVert
			// 
			this.tsmiFlipVert.Name = "tsmiFlipVert";
			this.tsmiFlipVert.Size = new System.Drawing.Size(228, 22);
			this.tsmiFlipVert.Text = "Flip Vertically";
			this.tsmiFlipVert.Click += new System.EventHandler(this.TsmiFlipVertClick);
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			this.toolStripMenuItem12.Size = new System.Drawing.Size(225, 6);
			// 
			// tsmiCropToSel
			// 
			this.tsmiCropToSel.Name = "tsmiCropToSel";
			this.tsmiCropToSel.Size = new System.Drawing.Size(228, 22);
			this.tsmiCropToSel.Text = "Crop to selection";
			this.tsmiCropToSel.Click += new System.EventHandler(this.TsmiCropToSelClick);
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			this.toolStripMenuItem13.Size = new System.Drawing.Size(225, 6);
			// 
			// tsmiInvertMinutiae
			// 
			this.tsmiInvertMinutiae.Name = "tsmiInvertMinutiae";
			this.tsmiInvertMinutiae.Size = new System.Drawing.Size(228, 22);
			this.tsmiInvertMinutiae.Text = "&Invert minutiae";
			this.tsmiInvertMinutiae.Click += new System.EventHandler(this.TsmiInvertMinutiaeClick);
			// 
			// toolStripMenuItem14
			// 
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			this.toolStripMenuItem14.Size = new System.Drawing.Size(225, 6);
			// 
			// performBandpassFilteringToolStripMenuItem
			// 
			this.performBandpassFilteringToolStripMenuItem.Name = "performBandpassFilteringToolStripMenuItem";
			this.performBandpassFilteringToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.performBandpassFilteringToolStripMenuItem.Text = "Perform Bandpass filtering";
			this.performBandpassFilteringToolStripMenuItem.Click += new System.EventHandler(this.TsmiPerformBandpassFilteringClick);
			// 
			// lblLeftFilename
			// 
			this.lblLeftFilename.AutoEllipsis = true;
			this.lblLeftFilename.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblLeftFilename.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblLeftFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblLeftFilename.Location = new System.Drawing.Point(0, 0);
			this.lblLeftFilename.Name = "lblLeftFilename";
			this.lblLeftFilename.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.lblLeftFilename.Size = new System.Drawing.Size(390, 17);
			this.lblLeftFilename.TabIndex = 8;
			this.lblLeftFilename.Text = "Untitled";
			this.lblLeftFilename.UseMnemonic = false;
			// 
			// rightSidePanel
			// 
			this.rightSidePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.rightSidePanel.Controls.Add(this.nfViewRight);
			this.rightSidePanel.Controls.Add(this.tsRight);
			this.rightSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rightSidePanel.Location = new System.Drawing.Point(0, 17);
			this.rightSidePanel.Name = "rightSidePanel";
			this.rightSidePanel.Size = new System.Drawing.Size(410, 571);
			this.rightSidePanel.TabIndex = 0;
			// 
			// nfViewRight
			// 
			this.nfViewRight.AutomaticRotateFlipImage = false;
			this.nfViewRight.AutoScroll = true;
			this.nfViewRight.BackColor = System.Drawing.SystemColors.Control;
			this.nfViewRight.BoundingRectColor = System.Drawing.Color.Red;
			this.nfViewRight.ContextMenuStrip = this.contextZoomMenuRight;
			this.nfViewRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nfViewRight.Location = new System.Drawing.Point(0, 25);
			this.nfViewRight.MatedMinutiaIndex = 1;
			this.nfViewRight.MinutiaColor = System.Drawing.Color.Red;
			this.nfViewRight.Name = "nfViewRight";
			this.nfViewRight.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.nfViewRight.ResultImageColor = System.Drawing.Color.Chartreuse;
			this.nfViewRight.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.nfViewRight.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.nfViewRight.SingularPointColor = System.Drawing.Color.Red;
			this.nfViewRight.Size = new System.Drawing.Size(406, 542);
			this.nfViewRight.TabIndex = 0;
			this.nfViewRight.Text = "nfView1";
			this.nfViewRight.TreeColor = System.Drawing.Color.Crimson;
			this.nfViewRight.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.nfViewRight.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.nfViewRight.TreeWidth = 3;
			this.nfViewRight.ZoomToFit = false;
			this.nfViewRight.SelectedTreeMinutiaIndexChanged += new System.EventHandler(this.NfView2IndexChanged);
			// 
			// tsRight
			// 
			this.tsRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenRight,
            this.toolStripSeparator2,
            this.tscbZoomRight,
            this.tsbRightZoomIn,
            this.tsbRightZoomOut,
            this.toolStripSeparator6,
            this.toolStripSplitButton3});
			this.tsRight.Location = new System.Drawing.Point(0, 0);
			this.tsRight.Name = "tsRight";
			this.tsRight.Size = new System.Drawing.Size(406, 25);
			this.tsRight.TabIndex = 1;
			this.tsRight.Text = "toolStrip3";
			// 
			// tsbOpenRight
			// 
			this.tsbOpenRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenRight.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenRight.Name = "tsbOpenRight";
			this.tsbOpenRight.Size = new System.Drawing.Size(23, 22);
			this.tsbOpenRight.Text = "Open";
			this.tsbOpenRight.Click += new System.EventHandler(this.BtnRightClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tscbZoomRight
			// 
			this.tscbZoomRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tscbZoomRight.Name = "tscbZoomRight";
			this.tscbZoomRight.Size = new System.Drawing.Size(75, 25);
			this.tscbZoomRight.SelectedIndexChanged += new System.EventHandler(this.TscbZoomRightSelectedIndexChanged);
			// 
			// tsbRightZoomIn
			// 
			this.tsbRightZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRightZoomIn.Image = global::Neurotec.Samples.Properties.Resources.ZoomIn;
			this.tsbRightZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRightZoomIn.Name = "tsbRightZoomIn";
			this.tsbRightZoomIn.Size = new System.Drawing.Size(23, 22);
			this.tsbRightZoomIn.Text = "Zoom In";
			this.tsbRightZoomIn.Click += new System.EventHandler(this.TsmiZoomInRightClick);
			// 
			// tsbRightZoomOut
			// 
			this.tsbRightZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRightZoomOut.Image = global::Neurotec.Samples.Properties.Resources.ZoomOut;
			this.tsbRightZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRightZoomOut.Name = "tsbRightZoomOut";
			this.tsbRightZoomOut.Size = new System.Drawing.Size(23, 22);
			this.tsbRightZoomOut.Text = "Zoom Out";
			this.tsbRightZoomOut.Click += new System.EventHandler(this.TsmiZoomOutRightClick);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSplitButton3
			// 
			this.toolStripSplitButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewOriginalRight,
            this.toolStripMenuItem5,
            zoomToolStripMenuItem1});
			this.toolStripSplitButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton3.Image")));
			this.toolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton3.Name = "toolStripSplitButton3";
			this.toolStripSplitButton3.Size = new System.Drawing.Size(45, 22);
			this.toolStripSplitButton3.Text = "&View";
			// 
			// tsmiViewOriginalRight
			// 
			this.tsmiViewOriginalRight.Checked = true;
			this.tsmiViewOriginalRight.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiViewOriginalRight.Name = "tsmiViewOriginalRight";
			this.tsmiViewOriginalRight.Size = new System.Drawing.Size(116, 22);
			this.tsmiViewOriginalRight.Text = "&Original";
			this.tsmiViewOriginalRight.Click += new System.EventHandler(this.TsmiViewOriginalRightClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(113, 6);
			// 
			// lblRightFilename
			// 
			this.lblRightFilename.AutoEllipsis = true;
			this.lblRightFilename.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblRightFilename.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblRightFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblRightFilename.Location = new System.Drawing.Point(0, 0);
			this.lblRightFilename.Name = "lblRightFilename";
			this.lblRightFilename.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.lblRightFilename.Size = new System.Drawing.Size(410, 17);
			this.lblRightFilename.TabIndex = 9;
			this.lblRightFilename.Text = "Untitled";
			this.lblRightFilename.UseMnemonic = false;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.panel1);
			this.panel3.Controls.Add(this.tableLayoutPanel1);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(0, 24);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(88, 647);
			this.panel3.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblBrightnessB);
			this.panel1.Controls.Add(this.lblBrightnessG);
			this.panel1.Controls.Add(this.lblBrightnessR);
			this.panel1.Controls.Add(this.lblContrastBValue);
			this.panel1.Controls.Add(this.lblContrastGValue);
			this.panel1.Controls.Add(this.lblContrastRValue);
			this.panel1.Controls.Add(this.btnResetAll);
			this.panel1.Controls.Add(this.btnResetContrast);
			this.panel1.Controls.Add(this.btnResetBrightness);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.cbGroupContrastSliders);
			this.panel1.Controls.Add(this.cbGrayscale);
			this.panel1.Controls.Add(this.cbInvert);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.sliderBrightnessGreen);
			this.panel1.Controls.Add(this.sliderContrastBlue);
			this.panel1.Controls.Add(this.sliderBrightnessRed);
			this.panel1.Controls.Add(this.cbGroupBrightnessSliders);
			this.panel1.Controls.Add(this.sliderContrastGreen);
			this.panel1.Controls.Add(this.sliderBrightnessBlue);
			this.panel1.Controls.Add(this.sliderContrastRed);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 188);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(84, 452);
			this.panel1.TabIndex = 17;
			// 
			// lblBrightnessB
			// 
			this.lblBrightnessB.Location = new System.Drawing.Point(54, 172);
			this.lblBrightnessB.Name = "lblBrightnessB";
			this.lblBrightnessB.Size = new System.Drawing.Size(30, 21);
			this.lblBrightnessB.TabIndex = 26;
			this.lblBrightnessB.Text = "0";
			this.lblBrightnessB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblBrightnessG
			// 
			this.lblBrightnessG.Location = new System.Drawing.Point(27, 172);
			this.lblBrightnessG.Name = "lblBrightnessG";
			this.lblBrightnessG.Size = new System.Drawing.Size(30, 21);
			this.lblBrightnessG.TabIndex = 25;
			this.lblBrightnessG.Text = "0";
			this.lblBrightnessG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblBrightnessR
			// 
			this.lblBrightnessR.Location = new System.Drawing.Point(-1, 172);
			this.lblBrightnessR.Name = "lblBrightnessR";
			this.lblBrightnessR.Size = new System.Drawing.Size(30, 21);
			this.lblBrightnessR.TabIndex = 24;
			this.lblBrightnessR.Text = "0";
			this.lblBrightnessR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblContrastBValue
			// 
			this.lblContrastBValue.Location = new System.Drawing.Point(53, 353);
			this.lblContrastBValue.Name = "lblContrastBValue";
			this.lblContrastBValue.Size = new System.Drawing.Size(30, 21);
			this.lblContrastBValue.TabIndex = 23;
			this.lblContrastBValue.Text = "0";
			this.lblContrastBValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblContrastGValue
			// 
			this.lblContrastGValue.Location = new System.Drawing.Point(26, 353);
			this.lblContrastGValue.Name = "lblContrastGValue";
			this.lblContrastGValue.Size = new System.Drawing.Size(30, 21);
			this.lblContrastGValue.TabIndex = 22;
			this.lblContrastGValue.Text = "0";
			this.lblContrastGValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblContrastRValue
			// 
			this.lblContrastRValue.Location = new System.Drawing.Point(-2, 353);
			this.lblContrastRValue.Name = "lblContrastRValue";
			this.lblContrastRValue.Size = new System.Drawing.Size(30, 21);
			this.lblContrastRValue.TabIndex = 21;
			this.lblContrastRValue.Text = "0";
			this.lblContrastRValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnResetAll
			// 
			this.btnResetAll.Location = new System.Drawing.Point(4, 424);
			this.btnResetAll.Name = "btnResetAll";
			this.btnResetAll.Size = new System.Drawing.Size(73, 20);
			this.btnResetAll.TabIndex = 20;
			this.btnResetAll.Text = "reset all";
			this.btnResetAll.UseVisualStyleBackColor = true;
			this.btnResetAll.Click += new System.EventHandler(this.BtnResetAllClick);
			// 
			// btnResetContrast
			// 
			this.btnResetContrast.Location = new System.Drawing.Point(27, 225);
			this.btnResetContrast.Name = "btnResetContrast";
			this.btnResetContrast.Size = new System.Drawing.Size(46, 20);
			this.btnResetContrast.TabIndex = 19;
			this.btnResetContrast.Text = "reset";
			this.btnResetContrast.UseVisualStyleBackColor = true;
			this.btnResetContrast.Click += new System.EventHandler(this.BtnResetContrastClick);
			// 
			// btnResetBrightness
			// 
			this.btnResetBrightness.Location = new System.Drawing.Point(29, 43);
			this.btnResetBrightness.Name = "btnResetBrightness";
			this.btnResetBrightness.Size = new System.Drawing.Size(46, 20);
			this.btnResetBrightness.TabIndex = 18;
			this.btnResetBrightness.Text = "reset";
			this.btnResetBrightness.UseVisualStyleBackColor = true;
			this.btnResetBrightness.Click += new System.EventHandler(this.BtnResetBrightnessClick);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 2, 2, 0);
			this.label3.Size = new System.Drawing.Size(84, 17);
			this.label3.TabIndex = 17;
			this.label3.Text = "Colors";
			// 
			// cbGroupContrastSliders
			// 
			this.cbGroupContrastSliders.AutoSize = true;
			this.cbGroupContrastSliders.Checked = true;
			this.cbGroupContrastSliders.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGroupContrastSliders.Location = new System.Drawing.Point(14, 377);
			this.cbGroupContrastSliders.Name = "cbGroupContrastSliders";
			this.cbGroupContrastSliders.Size = new System.Drawing.Size(53, 17);
			this.cbGroupContrastSliders.TabIndex = 15;
			this.cbGroupContrastSliders.Text = "group";
			this.cbGroupContrastSliders.UseVisualStyleBackColor = true;
			// 
			// cbGrayscale
			// 
			this.cbGrayscale.AutoSize = true;
			this.cbGrayscale.Location = new System.Drawing.Point(10, 406);
			this.cbGrayscale.Name = "cbGrayscale";
			this.cbGrayscale.Size = new System.Drawing.Size(58, 17);
			this.cbGrayscale.TabIndex = 5;
			this.cbGrayscale.Text = "to gray";
			this.cbGrayscale.UseVisualStyleBackColor = true;
			this.cbGrayscale.CheckedChanged += new System.EventHandler(this.CbGrayscaleCheckedChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Neurotec.Samples.Properties.Resources.brightness;
			this.pictureBox1.Location = new System.Drawing.Point(4, 43);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(17, 20);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// sliderBrightnessGreen
			// 
			this.sliderBrightnessGreen.BackColor = System.Drawing.Color.Transparent;
			this.sliderBrightnessGreen.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.sliderBrightnessGreen.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.sliderBrightnessGreen.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessGreen.ElapsedInnerColor = System.Drawing.Color.Lime;
			this.sliderBrightnessGreen.ElapsedOuterColor = System.Drawing.Color.Green;
			this.sliderBrightnessGreen.LargeChange = ((uint)(5u));
			this.sliderBrightnessGreen.Location = new System.Drawing.Point(31, 69);
			this.sliderBrightnessGreen.Minimum = -100;
			this.sliderBrightnessGreen.Name = "sliderBrightnessGreen";
			this.sliderBrightnessGreen.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderBrightnessGreen.Size = new System.Drawing.Size(18, 100);
			this.sliderBrightnessGreen.SmallChange = ((uint)(1u));
			this.sliderBrightnessGreen.TabIndex = 9;
			this.sliderBrightnessGreen.Tag = "1";
			this.sliderBrightnessGreen.Text = "colorSlider2";
			this.sliderBrightnessGreen.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessGreen.Value = 0;
			this.sliderBrightnessGreen.ValueChanged += new System.EventHandler(this.BrightnessValueChanged);
			// 
			// sliderContrastBlue
			// 
			this.sliderContrastBlue.BackColor = System.Drawing.Color.Transparent;
			this.sliderContrastBlue.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.sliderContrastBlue.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.sliderContrastBlue.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastBlue.ElapsedInnerColor = System.Drawing.Color.Blue;
			this.sliderContrastBlue.ElapsedOuterColor = System.Drawing.Color.Navy;
			this.sliderContrastBlue.LargeChange = ((uint)(5u));
			this.sliderContrastBlue.Location = new System.Drawing.Point(58, 251);
			this.sliderContrastBlue.Minimum = -100;
			this.sliderContrastBlue.Name = "sliderContrastBlue";
			this.sliderContrastBlue.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderContrastBlue.Size = new System.Drawing.Size(18, 100);
			this.sliderContrastBlue.SmallChange = ((uint)(1u));
			this.sliderContrastBlue.TabIndex = 14;
			this.sliderContrastBlue.Tag = "2";
			this.sliderContrastBlue.Text = "colorSlider3";
			this.sliderContrastBlue.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastBlue.Value = 0;
			this.sliderContrastBlue.ValueChanged += new System.EventHandler(this.ContrastValueChanged);
			// 
			// sliderBrightnessRed
			// 
			this.sliderBrightnessRed.BackColor = System.Drawing.Color.Transparent;
			this.sliderBrightnessRed.BarInnerColor = System.Drawing.Color.Maroon;
			this.sliderBrightnessRed.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.sliderBrightnessRed.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessRed.ElapsedInnerColor = System.Drawing.Color.Red;
			this.sliderBrightnessRed.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.sliderBrightnessRed.LargeChange = ((uint)(5u));
			this.sliderBrightnessRed.Location = new System.Drawing.Point(4, 69);
			this.sliderBrightnessRed.Minimum = -100;
			this.sliderBrightnessRed.Name = "sliderBrightnessRed";
			this.sliderBrightnessRed.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderBrightnessRed.Size = new System.Drawing.Size(18, 100);
			this.sliderBrightnessRed.SmallChange = ((uint)(1u));
			this.sliderBrightnessRed.TabIndex = 8;
			this.sliderBrightnessRed.Tag = "0";
			this.sliderBrightnessRed.Text = "colorSlider1";
			this.sliderBrightnessRed.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessRed.Value = 0;
			this.sliderBrightnessRed.ValueChanged += new System.EventHandler(this.BrightnessValueChanged);
			// 
			// cbGroupBrightnessSliders
			// 
			this.cbGroupBrightnessSliders.AutoSize = true;
			this.cbGroupBrightnessSliders.Checked = true;
			this.cbGroupBrightnessSliders.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGroupBrightnessSliders.Location = new System.Drawing.Point(11, 197);
			this.cbGroupBrightnessSliders.Name = "cbGroupBrightnessSliders";
			this.cbGroupBrightnessSliders.Size = new System.Drawing.Size(53, 17);
			this.cbGroupBrightnessSliders.TabIndex = 11;
			this.cbGroupBrightnessSliders.Text = "group";
			this.cbGroupBrightnessSliders.UseVisualStyleBackColor = true;
			// 
			// sliderContrastGreen
			// 
			this.sliderContrastGreen.BackColor = System.Drawing.Color.Transparent;
			this.sliderContrastGreen.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.sliderContrastGreen.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.sliderContrastGreen.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastGreen.ElapsedInnerColor = System.Drawing.Color.Lime;
			this.sliderContrastGreen.ElapsedOuterColor = System.Drawing.Color.Green;
			this.sliderContrastGreen.LargeChange = ((uint)(5u));
			this.sliderContrastGreen.Location = new System.Drawing.Point(31, 251);
			this.sliderContrastGreen.Minimum = -100;
			this.sliderContrastGreen.Name = "sliderContrastGreen";
			this.sliderContrastGreen.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderContrastGreen.Size = new System.Drawing.Size(18, 100);
			this.sliderContrastGreen.SmallChange = ((uint)(1u));
			this.sliderContrastGreen.TabIndex = 13;
			this.sliderContrastGreen.Tag = "1";
			this.sliderContrastGreen.Text = "colorSlider2";
			this.sliderContrastGreen.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastGreen.Value = 0;
			this.sliderContrastGreen.ValueChanged += new System.EventHandler(this.ContrastValueChanged);
			// 
			// sliderBrightnessBlue
			// 
			this.sliderBrightnessBlue.BackColor = System.Drawing.Color.Transparent;
			this.sliderBrightnessBlue.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.sliderBrightnessBlue.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.sliderBrightnessBlue.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessBlue.ElapsedInnerColor = System.Drawing.Color.Blue;
			this.sliderBrightnessBlue.ElapsedOuterColor = System.Drawing.Color.Navy;
			this.sliderBrightnessBlue.LargeChange = ((uint)(5u));
			this.sliderBrightnessBlue.Location = new System.Drawing.Point(59, 69);
			this.sliderBrightnessBlue.Minimum = -100;
			this.sliderBrightnessBlue.Name = "sliderBrightnessBlue";
			this.sliderBrightnessBlue.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderBrightnessBlue.Size = new System.Drawing.Size(18, 100);
			this.sliderBrightnessBlue.SmallChange = ((uint)(1u));
			this.sliderBrightnessBlue.TabIndex = 10;
			this.sliderBrightnessBlue.Tag = "2";
			this.sliderBrightnessBlue.Text = "colorSlider3";
			this.sliderBrightnessBlue.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderBrightnessBlue.Value = 0;
			this.sliderBrightnessBlue.ValueChanged += new System.EventHandler(this.BrightnessValueChanged);
			// 
			// sliderContrastRed
			// 
			this.sliderContrastRed.BackColor = System.Drawing.Color.Transparent;
			this.sliderContrastRed.BarInnerColor = System.Drawing.Color.Maroon;
			this.sliderContrastRed.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.sliderContrastRed.BorderRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastRed.ElapsedInnerColor = System.Drawing.Color.Red;
			this.sliderContrastRed.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.sliderContrastRed.LargeChange = ((uint)(5u));
			this.sliderContrastRed.Location = new System.Drawing.Point(3, 251);
			this.sliderContrastRed.Minimum = -100;
			this.sliderContrastRed.Name = "sliderContrastRed";
			this.sliderContrastRed.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.sliderContrastRed.Size = new System.Drawing.Size(18, 100);
			this.sliderContrastRed.SmallChange = ((uint)(1u));
			this.sliderContrastRed.TabIndex = 12;
			this.sliderContrastRed.Tag = "0";
			this.sliderContrastRed.Text = "colorSlider1";
			this.sliderContrastRed.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
			this.sliderContrastRed.Value = 0;
			this.sliderContrastRed.ValueChanged += new System.EventHandler(this.ContrastValueChanged);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Neurotec.Samples.Properties.Resources.contrast;
			this.pictureBox2.Location = new System.Drawing.Point(4, 225);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(17, 20);
			this.pictureBox2.TabIndex = 4;
			this.pictureBox2.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.13636F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.86364F));
			this.tableLayoutPanel1.Controls.Add(this.rbAddDoubleCoreTool, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.rbAddDeltaTool, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.rbAddCoreTool, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.rbAddBifurcationMinutia, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.rbAddEndMinutiaTool, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rbSelectAreaTool, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.rbPointerTool, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 17);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(84, 171);
			this.tableLayoutPanel1.TabIndex = 18;
			// 
			// rbAddDoubleCoreTool
			// 
			this.rbAddDoubleCoreTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbAddDoubleCoreTool.AutoSize = true;
			this.rbAddDoubleCoreTool.Image = global::Neurotec.Samples.Properties.Resources.ToolDoubleCore;
			this.rbAddDoubleCoreTool.Location = new System.Drawing.Point(1, 126);
			this.rbAddDoubleCoreTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbAddDoubleCoreTool.Name = "rbAddDoubleCoreTool";
			this.rbAddDoubleCoreTool.Size = new System.Drawing.Size(38, 38);
			this.rbAddDoubleCoreTool.TabIndex = 6;
			this.toolTip1.SetToolTip(this.rbAddDoubleCoreTool, "Add Double Core");
			this.rbAddDoubleCoreTool.UseVisualStyleBackColor = true;
			this.rbAddDoubleCoreTool.CheckedChanged += new System.EventHandler(this.RbAddDoubleCoreToolCheckedChanged);
			// 
			// rbAddDeltaTool
			// 
			this.rbAddDeltaTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbAddDeltaTool.AutoSize = true;
			this.rbAddDeltaTool.Image = global::Neurotec.Samples.Properties.Resources.ToolDelta;
			this.rbAddDeltaTool.Location = new System.Drawing.Point(1, 85);
			this.rbAddDeltaTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbAddDeltaTool.Name = "rbAddDeltaTool";
			this.rbAddDeltaTool.Size = new System.Drawing.Size(38, 38);
			this.rbAddDeltaTool.TabIndex = 5;
			this.toolTip1.SetToolTip(this.rbAddDeltaTool, "Add Delta");
			this.rbAddDeltaTool.UseVisualStyleBackColor = true;
			this.rbAddDeltaTool.CheckedChanged += new System.EventHandler(this.RbAddDeltaToolCheckedChanged);
			// 
			// rbAddCoreTool
			// 
			this.rbAddCoreTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbAddCoreTool.AutoSize = true;
			this.rbAddCoreTool.Image = global::Neurotec.Samples.Properties.Resources.ToolCore;
			this.rbAddCoreTool.Location = new System.Drawing.Point(43, 85);
			this.rbAddCoreTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbAddCoreTool.Name = "rbAddCoreTool";
			this.rbAddCoreTool.Size = new System.Drawing.Size(38, 38);
			this.rbAddCoreTool.TabIndex = 4;
			this.toolTip1.SetToolTip(this.rbAddCoreTool, "Add Core");
			this.rbAddCoreTool.UseVisualStyleBackColor = true;
			this.rbAddCoreTool.CheckedChanged += new System.EventHandler(this.RbAddCoreToolCheckedChanged);
			// 
			// rbAddBifurcationMinutia
			// 
			this.rbAddBifurcationMinutia.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbAddBifurcationMinutia.AutoSize = true;
			this.rbAddBifurcationMinutia.Image = global::Neurotec.Samples.Properties.Resources.ToolMinutiaBifurcation;
			this.rbAddBifurcationMinutia.Location = new System.Drawing.Point(43, 45);
			this.rbAddBifurcationMinutia.Margin = new System.Windows.Forms.Padding(1);
			this.rbAddBifurcationMinutia.Name = "rbAddBifurcationMinutia";
			this.rbAddBifurcationMinutia.Size = new System.Drawing.Size(38, 38);
			this.rbAddBifurcationMinutia.TabIndex = 3;
			this.toolTip1.SetToolTip(this.rbAddBifurcationMinutia, "Add Bifurcation Minutia");
			this.rbAddBifurcationMinutia.UseVisualStyleBackColor = true;
			this.rbAddBifurcationMinutia.CheckedChanged += new System.EventHandler(this.RbAddBifurcationMinutiaCheckedChanged);
			// 
			// rbAddEndMinutiaTool
			// 
			this.rbAddEndMinutiaTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbAddEndMinutiaTool.AutoSize = true;
			this.rbAddEndMinutiaTool.Image = global::Neurotec.Samples.Properties.Resources.ToolMinutiaEnd;
			this.rbAddEndMinutiaTool.Location = new System.Drawing.Point(1, 45);
			this.rbAddEndMinutiaTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbAddEndMinutiaTool.Name = "rbAddEndMinutiaTool";
			this.rbAddEndMinutiaTool.Size = new System.Drawing.Size(38, 38);
			this.rbAddEndMinutiaTool.TabIndex = 2;
			this.toolTip1.SetToolTip(this.rbAddEndMinutiaTool, "Add End Minutia");
			this.rbAddEndMinutiaTool.UseVisualStyleBackColor = true;
			this.rbAddEndMinutiaTool.CheckedChanged += new System.EventHandler(this.RbAddEndMinutiaToolCheckedChanged);
			// 
			// rbSelectAreaTool
			// 
			this.rbSelectAreaTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbSelectAreaTool.AutoSize = true;
			this.rbSelectAreaTool.Image = global::Neurotec.Samples.Properties.Resources.ToolAreaSelect;
			this.rbSelectAreaTool.Location = new System.Drawing.Point(43, 1);
			this.rbSelectAreaTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbSelectAreaTool.Name = "rbSelectAreaTool";
			this.rbSelectAreaTool.Size = new System.Drawing.Size(38, 38);
			this.rbSelectAreaTool.TabIndex = 1;
			this.toolTip1.SetToolTip(this.rbSelectAreaTool, "Area Select Tool");
			this.rbSelectAreaTool.UseVisualStyleBackColor = true;
			this.rbSelectAreaTool.CheckedChanged += new System.EventHandler(this.RbSelectAreaToolCheckedChanged);
			// 
			// rbPointerTool
			// 
			this.rbPointerTool.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbPointerTool.AutoSize = true;
			this.rbPointerTool.Checked = true;
			this.rbPointerTool.Image = global::Neurotec.Samples.Properties.Resources.ToolMoveRotate;
			this.rbPointerTool.Location = new System.Drawing.Point(1, 1);
			this.rbPointerTool.Margin = new System.Windows.Forms.Padding(1);
			this.rbPointerTool.Name = "rbPointerTool";
			this.rbPointerTool.Size = new System.Drawing.Size(38, 38);
			this.rbPointerTool.TabIndex = 0;
			this.rbPointerTool.TabStop = true;
			this.toolTip1.SetToolTip(this.rbPointerTool, "Move/Rotate Tool");
			this.rbPointerTool.UseVisualStyleBackColor = true;
			this.rbPointerTool.CheckedChanged += new System.EventHandler(this.RbPointerToolCheckedChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.label1.Size = new System.Drawing.Size(84, 17);
			this.label1.TabIndex = 7;
			this.label1.Text = "Tools";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem6.Text = "Zoom &in";
			this.toolStripMenuItem6.Click += new System.EventHandler(this.TsmiZoomInRightClick);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem7.Text = "Zoom &out";
			this.toolStripMenuItem7.Click += new System.EventHandler(this.TsmiZoomOutRightClick);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem8.Text = "&Original";
			this.toolStripMenuItem8.Click += new System.EventHandler(this.TsmiZoomOriginalRightClick);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem9.Text = "Zoom &in";
			this.toolStripMenuItem9.Click += new System.EventHandler(this.TsmiZoomInLeftClick);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem10.Text = "Zoom &out";
			this.toolStripMenuItem10.Click += new System.EventHandler(this.TsmiZoomOutLeftClick);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(130, 22);
			this.toolStripMenuItem11.Text = "&Original";
			this.toolStripMenuItem11.Click += new System.EventHandler(this.TsmiZoomOriginalLeftClick);
			// 
			// saveTemplateDialog
			// 
			this.saveTemplateDialog.Title = "Save template file";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.panel7);
			this.panel5.Controls.Add(this.panel6);
			this.panel5.Controls.Add(this.panel2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(88, 612);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(804, 59);
			this.panel5.TabIndex = 23;
			// 
			// panel7
			// 
			this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel7.Controls.Add(this.groupBox2);
			this.panel7.Controls.Add(this.groupBox1);
			this.panel7.Controls.Add(this.btnMatcher);
			this.panel7.Controls.Add(this.lblMatchingScore);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.Location = new System.Drawing.Point(185, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(434, 59);
			this.panel7.TabIndex = 4;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbUseEditedImage);
			this.groupBox2.Controls.Add(this.rbUseOriginalImage);
			this.groupBox2.Location = new System.Drawing.Point(3, 1);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(149, 51);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Working image:";
			// 
			// rbUseEditedImage
			// 
			this.rbUseEditedImage.AutoSize = true;
			this.rbUseEditedImage.Location = new System.Drawing.Point(81, 19);
			this.rbUseEditedImage.Name = "rbUseEditedImage";
			this.rbUseEditedImage.Size = new System.Drawing.Size(55, 17);
			this.rbUseEditedImage.TabIndex = 1;
			this.rbUseEditedImage.Text = "Edited";
			this.rbUseEditedImage.UseVisualStyleBackColor = true;
			// 
			// rbUseOriginalImage
			// 
			this.rbUseOriginalImage.AutoSize = true;
			this.rbUseOriginalImage.Checked = true;
			this.rbUseOriginalImage.Location = new System.Drawing.Point(15, 19);
			this.rbUseOriginalImage.Name = "rbUseOriginalImage";
			this.rbUseOriginalImage.Size = new System.Drawing.Size(60, 17);
			this.rbUseOriginalImage.TabIndex = 0;
			this.rbUseOriginalImage.TabStop = true;
			this.rbUseOriginalImage.Text = "Original";
			this.rbUseOriginalImage.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.matchingFarComboBox);
			this.groupBox1.Location = new System.Drawing.Point(179, 1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(150, 51);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Matching FAR";
			// 
			// matchingFarComboBox
			// 
			this.matchingFarComboBox.FormattingEnabled = true;
			this.matchingFarComboBox.Location = new System.Drawing.Point(6, 21);
			this.matchingFarComboBox.Name = "matchingFarComboBox";
			this.matchingFarComboBox.Size = new System.Drawing.Size(135, 21);
			this.matchingFarComboBox.TabIndex = 19;
			this.matchingFarComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.MatchingFarComboBoxValidating);
			this.matchingFarComboBox.Validated += new System.EventHandler(this.MatchingFarComboBoxValidated);
			// 
			// lblMatchingScore
			// 
			this.lblMatchingScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMatchingScore.Location = new System.Drawing.Point(338, 33);
			this.lblMatchingScore.Name = "lblMatchingScore";
			this.lblMatchingScore.Size = new System.Drawing.Size(89, 16);
			this.lblMatchingScore.TabIndex = 21;
			this.lblMatchingScore.Text = "Score:";
			// 
			// panel6
			// 
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel6.Controls.Add(this.lblReferenceResolution);
			this.panel6.Controls.Add(this.lblReferenceSize);
			this.panel6.Controls.Add(this.label5);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel6.Location = new System.Drawing.Point(619, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(185, 59);
			this.panel6.TabIndex = 3;
			// 
			// lblReferenceResolution
			// 
			this.lblReferenceResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblReferenceResolution.Location = new System.Drawing.Point(3, 37);
			this.lblReferenceResolution.Name = "lblReferenceResolution";
			this.lblReferenceResolution.Size = new System.Drawing.Size(175, 20);
			this.lblReferenceResolution.TabIndex = 6;
			this.lblReferenceResolution.Text = "Resolution:";
			// 
			// lblReferenceSize
			// 
			this.lblReferenceSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblReferenceSize.Location = new System.Drawing.Point(3, 22);
			this.lblReferenceSize.Name = "lblReferenceSize";
			this.lblReferenceSize.Size = new System.Drawing.Size(175, 20);
			this.lblReferenceSize.TabIndex = 5;
			this.lblReferenceSize.Text = "Size:";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.label5.Location = new System.Drawing.Point(1, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Reference image";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(892, 24);
			this.menuStrip1.TabIndex = 24;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSaveTemplate,
            this.toolStripMenuItem1,
            this.tsmiSaveLatentImage,
            this.tsmiSaveReferenceImage,
            this.toolStripMenuItem2,
            this.tsmiFileExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// tsmiSaveTemplate
			// 
			this.tsmiSaveTemplate.Name = "tsmiSaveTemplate";
			this.tsmiSaveTemplate.Size = new System.Drawing.Size(195, 22);
			this.tsmiSaveTemplate.Text = "Save &template...";
			this.tsmiSaveTemplate.Click += new System.EventHandler(this.TsbSaveTemplateClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
			// 
			// tsmiSaveLatentImage
			// 
			this.tsmiSaveLatentImage.Name = "tsmiSaveLatentImage";
			this.tsmiSaveLatentImage.Size = new System.Drawing.Size(195, 22);
			this.tsmiSaveLatentImage.Text = "Save &latent image...";
			this.tsmiSaveLatentImage.Click += new System.EventHandler(this.TsmiSaveLatentImageClick);
			// 
			// tsmiSaveReferenceImage
			// 
			this.tsmiSaveReferenceImage.Name = "tsmiSaveReferenceImage";
			this.tsmiSaveReferenceImage.Size = new System.Drawing.Size(195, 22);
			this.tsmiSaveReferenceImage.Text = "Save &reference image...";
			this.tsmiSaveReferenceImage.Click += new System.EventHandler(this.TsmiSaveReferenceImageClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
			// 
			// tsmiFileExit
			// 
			this.tsmiFileExit.Name = "tsmiFileExit";
			this.tsmiFileExit.Size = new System.Drawing.Size(195, 22);
			this.tsmiFileExit.Text = "&Exit";
			this.tsmiFileExit.Click += new System.EventHandler(this.TsmiFileExitClick);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExtractionSettings});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "&Settings";
			// 
			// tsmiExtractionSettings
			// 
			this.tsmiExtractionSettings.Name = "tsmiExtractionSettings";
			this.tsmiExtractionSettings.Size = new System.Drawing.Size(135, 22);
			this.tsmiExtractionSettings.Text = "&Extraction...";
			this.tsmiExtractionSettings.Click += new System.EventHandler(this.TsmiExtractionSettingsClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpAbout});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// tsmiHelpAbout
			// 
			this.tsmiHelpAbout.Name = "tsmiHelpAbout";
			this.tsmiHelpAbout.Size = new System.Drawing.Size(107, 22);
			this.tsmiHelpAbout.Text = "&About";
			this.tsmiHelpAbout.Click += new System.EventHandler(this.TsmiHelpAboutClick);
			// 
			// saveImageDialog
			// 
			this.saveImageDialog.Title = "Save Image";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 671);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(900, 700);
			this.Name = "MainForm";
			this.Text = "LatentFingerprintSample";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.contextZoomMenuRight.ResumeLayout(false);
			this.contextZoomMenuLeft.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.leftPartPanel.ResumeLayout(false);
			this.leftPartPanel.PerformLayout();
			this.contextMenuLeft.ResumeLayout(false);
			this.tsLeft.ResumeLayout(false);
			this.tsLeft.PerformLayout();
			this.rightSidePanel.ResumeLayout(false);
			this.rightSidePanel.PerformLayout();
			this.tsRight.ResumeLayout(false);
			this.tsRight.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnMatcher;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.CheckBox cbInvert;
		private System.Windows.Forms.ContextMenuStrip contextZoomMenuLeft;
		private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem originalToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel leftPartPanel;
		private Neurotec.Biometrics.Gui.NFingerView nfViewLeft;
		private System.Windows.Forms.ToolStrip tsLeft;
		private System.Windows.Forms.Panel rightSidePanel;
		private Neurotec.Biometrics.Gui.NFingerView nfViewRight;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ToolStripButton tsbOpenLeft;
		private System.Windows.Forms.ToolStripButton tsbExtractLeft;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStrip tsRight;
		private System.Windows.Forms.ToolStripButton tsbOpenRight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ContextMenuStrip contextZoomMenuRight;
		private System.Windows.Forms.ToolStripMenuItem tsmiZoomInRight;
		private System.Windows.Forms.ToolStripMenuItem tsmiZoomOutRight;
		private System.Windows.Forms.ToolStripMenuItem tsmiZoomOriginalRight;
		private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton3;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewOriginalRight;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		private System.Windows.Forms.ToolStripDropDownButton tsbView;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewOriginalLeft;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton2;
		private System.Windows.Forms.ToolStripMenuItem tsmiRotate90cw;
		private System.Windows.Forms.ToolStripMenuItem tsmiRotate90ccw;
		private System.Windows.Forms.ToolStripMenuItem tsmiRotate180;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiFlipHorz;
		private System.Windows.Forms.ToolStripMenuItem tsmiFlipVert;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.CheckBox cbGrayscale;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
		private System.Windows.Forms.ToolStripMenuItem tsmiCropToSel;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
		private ColorSlider sliderBrightnessRed;
		private ColorSlider sliderBrightnessBlue;
		private ColorSlider sliderBrightnessGreen;
		private System.Windows.Forms.CheckBox cbGroupBrightnessSliders;
		private System.Windows.Forms.CheckBox cbGroupContrastSliders;
		private ColorSlider sliderContrastBlue;
		private ColorSlider sliderContrastGreen;
		private ColorSlider sliderContrastRed;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblLatentResolution;
		private System.Windows.Forms.Label lblLatentSize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ContextMenuStrip contextMenuLeft;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFeature;
		private System.Windows.Forms.ToolStripButton tsbSaveTemplate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.SaveFileDialog saveTemplateDialog;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label lblReferenceResolution;
		private System.Windows.Forms.Label lblReferenceSize;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.ComboBox matchingFarComboBox;
		private System.Windows.Forms.Label lblMatchingScore;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiExtractionSettings;
		private System.Windows.Forms.Button btnResetAll;
		private System.Windows.Forms.Button btnResetContrast;
		private System.Windows.Forms.Button btnResetBrightness;
		private System.Windows.Forms.Label lblLeftFilename;
		private System.Windows.Forms.Label lblRightFilename;
		private System.Windows.Forms.ToolStripButton tsbLeftZoomIn;
		private System.Windows.Forms.ToolStripButton tsbLeftZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton tsbRightZoomIn;
		private System.Windows.Forms.ToolStripButton tsbRightZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbUseOriginalImage;
		private System.Windows.Forms.RadioButton rbUseEditedImage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.RadioButton rbPointerTool;
		private System.Windows.Forms.RadioButton rbSelectAreaTool;
		private System.Windows.Forms.RadioButton rbAddDoubleCoreTool;
		private System.Windows.Forms.RadioButton rbAddDeltaTool;
		private System.Windows.Forms.RadioButton rbAddCoreTool;
		private System.Windows.Forms.RadioButton rbAddBifurcationMinutia;
		private System.Windows.Forms.RadioButton rbAddEndMinutiaTool;
		private System.Windows.Forms.Label lblContrastRValue;
		private System.Windows.Forms.Label lblContrastBValue;
		private System.Windows.Forms.Label lblContrastGValue;
		private System.Windows.Forms.Label lblBrightnessB;
		private System.Windows.Forms.Label lblBrightnessG;
		private System.Windows.Forms.Label lblBrightnessR;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveTemplate;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveLatentImage;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveReferenceImage;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
		private System.Windows.Forms.SaveFileDialog saveImageDialog;
		private System.Windows.Forms.ToolStripComboBox tscbZoomLeft;
		private System.Windows.Forms.ToolStripComboBox tscbZoomRight;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
		private System.Windows.Forms.ToolStripMenuItem tsmiInvertMinutiae;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelpAbout;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
		private System.Windows.Forms.ToolStripMenuItem performBandpassFilteringToolStripMenuItem;
	}
}

