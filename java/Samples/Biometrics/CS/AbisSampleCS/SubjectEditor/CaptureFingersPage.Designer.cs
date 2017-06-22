namespace Neurotec.Samples
{
	partial class CaptureFingersPage
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
			this.fingerSelector = new Neurotec.Samples.FingerSelector();
			this.gbOptions = new System.Windows.Forms.GroupBox();
			this.chbWithGeneralization = new System.Windows.Forms.CheckBox();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbImpression = new System.Windows.Forms.ComboBox();
			this.cbScenario = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.gbSource = new System.Windows.Forms.GroupBox();
			this.rbTenPrintCard = new System.Windows.Forms.RadioButton();
			this.rbFiles = new System.Windows.Forms.RadioButton();
			this.rbScanner = new System.Windows.Forms.RadioButton();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnSkip = new System.Windows.Forms.Button();
			this.btnRepeat = new System.Windows.Forms.Button();
			this.btnOpenImage = new System.Windows.Forms.Button();
			this.fingerView = new Neurotec.Biometrics.Gui.NFingerView();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.fingersTree = new Neurotec.Samples.SubjectTreeControl();
			this.lblNote = new System.Windows.Forms.Label();
			this.horizontalZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMissing = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panelNavigations = new System.Windows.Forms.TableLayoutPanel();
			this.btnForce = new System.Windows.Forms.Button();
			this.bntFinish = new System.Windows.Forms.Button();
			this.chbShowReturned = new System.Windows.Forms.CheckBox();
			this.generalizeProgressView = new Neurotec.Samples.Common.GeneralizeProgressView();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.busyIndicator = new Neurotec.Samples.BusyIndicator();
			this.lblStatus = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.gbOptions.SuspendLayout();
			this.gbSource.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panelNavigations.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// fingerSelector
			// 
			this.fingerSelector.AllowedPositions = new Neurotec.Biometrics.NFPosition[] {
        Neurotec.Biometrics.NFPosition.RightThumb,
        Neurotec.Biometrics.NFPosition.RightIndexFinger,
        Neurotec.Biometrics.NFPosition.RightMiddleFinger,
        Neurotec.Biometrics.NFPosition.RightRingFinger,
        Neurotec.Biometrics.NFPosition.RightLittleFinger,
        Neurotec.Biometrics.NFPosition.LeftThumb,
        Neurotec.Biometrics.NFPosition.LeftIndexFinger,
        Neurotec.Biometrics.NFPosition.LeftMiddleFinger,
        Neurotec.Biometrics.NFPosition.LeftRingFinger,
        Neurotec.Biometrics.NFPosition.LeftLittle};
			this.fingerSelector.AllowHighlight = true;
			this.tableLayoutPanel1.SetColumnSpan(this.fingerSelector, 2);
			this.fingerSelector.IsRolled = false;
			this.fingerSelector.Location = new System.Drawing.Point(3, 3);
			this.fingerSelector.MissingPositions = new Neurotec.Biometrics.NFPosition[0];
			this.fingerSelector.Name = "fingerSelector";
			this.fingerSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown;
			this.fingerSelector.ShowFingerNails = true;
			this.fingerSelector.ShowPalmCreases = false;
			this.fingerSelector.Size = new System.Drawing.Size(240, 119);
			this.fingerSelector.TabIndex = 0;
			this.fingerSelector.Text = "fingerSelector1";
			this.fingerSelector.FingerClick += new System.EventHandler<Neurotec.Samples.FingerSelector.FingerClickArgs>(this.FingerSelectorFingerClick);
			// 
			// gbOptions
			// 
			this.gbOptions.Controls.Add(this.chbWithGeneralization);
			this.gbOptions.Controls.Add(this.chbCaptureAutomatically);
			this.gbOptions.Controls.Add(this.label2);
			this.gbOptions.Controls.Add(this.cbImpression);
			this.gbOptions.Controls.Add(this.cbScenario);
			this.gbOptions.Controls.Add(this.label1);
			this.gbOptions.Location = new System.Drawing.Point(255, 3);
			this.gbOptions.Name = "gbOptions";
			this.gbOptions.Size = new System.Drawing.Size(324, 85);
			this.gbOptions.TabIndex = 13;
			this.gbOptions.TabStop = false;
			this.gbOptions.Text = "Options";
			// 
			// chbWithGeneralization
			// 
			this.chbWithGeneralization.AutoSize = true;
			this.chbWithGeneralization.Location = new System.Drawing.Point(207, 64);
			this.chbWithGeneralization.Name = "chbWithGeneralization";
			this.chbWithGeneralization.Size = new System.Drawing.Size(116, 17);
			this.chbWithGeneralization.TabIndex = 6;
			this.chbWithGeneralization.Text = "With generalization";
			this.chbWithGeneralization.UseVisualStyleBackColor = true;
			// 
			// chbCaptureAutomatically
			// 
			this.chbCaptureAutomatically.AutoSize = true;
			this.chbCaptureAutomatically.Checked = true;
			this.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(73, 64);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 5;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Impression";
			// 
			// cbImpression
			// 
			this.cbImpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbImpression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImpression.Enabled = false;
			this.cbImpression.FormattingEnabled = true;
			this.cbImpression.Location = new System.Drawing.Point(73, 37);
			this.cbImpression.MaxDropDownItems = 20;
			this.cbImpression.Name = "cbImpression";
			this.cbImpression.Size = new System.Drawing.Size(245, 21);
			this.cbImpression.TabIndex = 3;
			// 
			// cbScenario
			// 
			this.cbScenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScenario.FormattingEnabled = true;
			this.cbScenario.Location = new System.Drawing.Point(73, 10);
			this.cbScenario.Name = "cbScenario";
			this.cbScenario.Size = new System.Drawing.Size(245, 21);
			this.cbScenario.TabIndex = 2;
			this.cbScenario.SelectedIndexChanged += new System.EventHandler(this.CbScenarioSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Scenario";
			// 
			// gbSource
			// 
			this.gbSource.AutoSize = true;
			this.gbSource.Controls.Add(this.rbTenPrintCard);
			this.gbSource.Controls.Add(this.rbFiles);
			this.gbSource.Controls.Add(this.rbScanner);
			this.gbSource.Location = new System.Drawing.Point(3, 3);
			this.gbSource.MaximumSize = new System.Drawing.Size(0, 85);
			this.gbSource.MinimumSize = new System.Drawing.Size(246, 0);
			this.gbSource.Name = "gbSource";
			this.gbSource.Size = new System.Drawing.Size(246, 85);
			this.gbSource.TabIndex = 14;
			this.gbSource.TabStop = false;
			this.gbSource.Text = "Source";
			// 
			// rbTenPrintCard
			// 
			this.rbTenPrintCard.AutoSize = true;
			this.rbTenPrintCard.Location = new System.Drawing.Point(6, 53);
			this.rbTenPrintCard.Name = "rbTenPrintCard";
			this.rbTenPrintCard.Size = new System.Drawing.Size(91, 17);
			this.rbTenPrintCard.TabIndex = 2;
			this.rbTenPrintCard.Text = "Ten print card";
			this.rbTenPrintCard.UseVisualStyleBackColor = true;
			this.rbTenPrintCard.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbFiles
			// 
			this.rbFiles.AutoSize = true;
			this.rbFiles.Location = new System.Drawing.Point(6, 33);
			this.rbFiles.Name = "rbFiles";
			this.rbFiles.Size = new System.Drawing.Size(41, 17);
			this.rbFiles.TabIndex = 1;
			this.rbFiles.Text = "File";
			this.rbFiles.UseVisualStyleBackColor = true;
			this.rbFiles.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbScanner
			// 
			this.rbScanner.AutoSize = true;
			this.rbScanner.Checked = true;
			this.rbScanner.Location = new System.Drawing.Point(6, 13);
			this.rbScanner.Name = "rbScanner";
			this.rbScanner.Size = new System.Drawing.Size(65, 17);
			this.rbScanner.TabIndex = 0;
			this.rbScanner.TabStop = true;
			this.rbScanner.Text = "Scanner";
			this.rbScanner.UseVisualStyleBackColor = true;
			this.rbScanner.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.Location = new System.Drawing.Point(3, 141);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(109, 23);
			this.btnStart.TabIndex = 15;
			this.btnStart.Text = "&Start capturing";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// btnSkip
			// 
			this.btnSkip.Image = global::Neurotec.Samples.Properties.Resources.GoToNext;
			this.btnSkip.Location = new System.Drawing.Point(265, 3);
			this.btnSkip.Name = "btnSkip";
			this.btnSkip.Size = new System.Drawing.Size(90, 23);
			this.btnSkip.TabIndex = 16;
			this.btnSkip.Text = "&Next";
			this.btnSkip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSkip.UseVisualStyleBackColor = true;
			this.btnSkip.Click += new System.EventHandler(this.BtnSkipClick);
			// 
			// btnRepeat
			// 
			this.btnRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRepeat.Image = global::Neurotec.Samples.Properties.Resources.Repeat;
			this.btnRepeat.Location = new System.Drawing.Point(145, 3);
			this.btnRepeat.Name = "btnRepeat";
			this.btnRepeat.Size = new System.Drawing.Size(114, 23);
			this.btnRepeat.TabIndex = 17;
			this.btnRepeat.Text = "&Repeat";
			this.btnRepeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRepeat.UseVisualStyleBackColor = true;
			this.btnRepeat.Click += new System.EventHandler(this.BtnRepeatClick);
			// 
			// btnOpenImage
			// 
			this.btnOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOpenImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.btnOpenImage.Location = new System.Drawing.Point(3, 170);
			this.btnOpenImage.Name = "btnOpenImage";
			this.btnOpenImage.Size = new System.Drawing.Size(109, 23);
			this.btnOpenImage.TabIndex = 18;
			this.btnOpenImage.Text = "&Open image";
			this.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenImage.UseVisualStyleBackColor = true;
			this.btnOpenImage.Click += new System.EventHandler(this.BtnOpenImageClick);
			// 
			// fingerView
			// 
			this.fingerView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fingerView.BackColor = System.Drawing.SystemColors.Control;
			this.fingerView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.fingerView.BoundingRectColor = System.Drawing.Color.Red;
			this.tableLayoutPanel2.SetColumnSpan(this.fingerView, 3);
			this.fingerView.ForeColor = System.Drawing.Color.Red;
			this.fingerView.Location = new System.Drawing.Point(3, 3);
			this.fingerView.MinutiaColor = System.Drawing.Color.Red;
			this.fingerView.Name = "fingerView";
			this.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.fingerView.ResultImageColor = System.Drawing.Color.Green;
			this.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.fingerView.SingularPointColor = System.Drawing.Color.Red;
			this.fingerView.Size = new System.Drawing.Size(724, 378);
			this.fingerView.TabIndex = 19;
			this.fingerView.TreeColor = System.Drawing.Color.Crimson;
			this.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.fingerView.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.fingerView.TreeWidth = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(457, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 23);
			this.btnCancel.TabIndex = 20;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.fingerSelector, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.fingersTree, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.lblNote, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnOpenImage, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnStart, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 97);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 519);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// fingersTree
			// 
			this.fingersTree.AllowNew = Neurotec.Biometrics.NBiometricType.None;
			this.fingersTree.AllowRemove = true;
			this.tableLayoutPanel1.SetColumnSpan(this.fingersTree, 2);
			this.fingersTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fingersTree.Location = new System.Drawing.Point(3, 199);
			this.fingersTree.Name = "fingersTree";
			this.fingersTree.SelectedItem = null;
			this.fingersTree.ShowBiometricsOnly = true;
			this.fingersTree.ShownTypes = Neurotec.Biometrics.NBiometricType.Fingerprint;
			this.fingersTree.Size = new System.Drawing.Size(240, 317);
			this.fingersTree.Subject = null;
			this.fingersTree.TabIndex = 21;
			this.fingersTree.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.FingersTreePropertyChanged);
			// 
			// lblNote
			// 
			this.lblNote.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblNote, 2);
			this.lblNote.Location = new System.Drawing.Point(3, 125);
			this.lblNote.Name = "lblNote";
			this.lblNote.Size = new System.Drawing.Size(33, 13);
			this.lblNote.TabIndex = 22;
			this.lblNote.Text = "Note:";
			// 
			// horizontalZoomSlider
			// 
			this.horizontalZoomSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.horizontalZoomSlider.Location = new System.Drawing.Point(3, 492);
			this.horizontalZoomSlider.Name = "horizontalZoomSlider";
			this.horizontalZoomSlider.Size = new System.Drawing.Size(259, 24);
			this.horizontalZoomSlider.TabIndex = 22;
			this.horizontalZoomSlider.View = this.fingerView;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelect,
            this.tsmiMissing});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(160, 48);
			this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStripItemClicked);
			// 
			// tsmiSelect
			// 
			this.tsmiSelect.Name = "tsmiSelect";
			this.tsmiSelect.Size = new System.Drawing.Size(159, 22);
			this.tsmiSelect.Text = "Select";
			// 
			// tsmiMissing
			// 
			this.tsmiMissing.Name = "tsmiMissing";
			this.tsmiMissing.Size = new System.Drawing.Size(159, 22);
			this.tsmiMissing.Text = "Mark as missing";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Controls.Add(this.horizontalZoomSlider, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.fingerView, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.panelNavigations, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.bntFinish, 2, 5);
			this.tableLayoutPanel2.Controls.Add(this.chbShowReturned, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.generalizeProgressView, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 3);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(252, 97);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(730, 519);
			this.tableLayoutPanel2.TabIndex = 24;
			// 
			// panelNavigations
			// 
			this.panelNavigations.ColumnCount = 4;
			this.tableLayoutPanel2.SetColumnSpan(this.panelNavigations, 3);
			this.panelNavigations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.36306F));
			this.panelNavigations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panelNavigations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panelNavigations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.63694F));
			this.panelNavigations.Controls.Add(this.btnRepeat, 0, 0);
			this.panelNavigations.Controls.Add(this.btnSkip, 1, 0);
			this.panelNavigations.Controls.Add(this.btnCancel, 3, 0);
			this.panelNavigations.Controls.Add(this.btnForce, 2, 0);
			this.panelNavigations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelNavigations.Location = new System.Drawing.Point(3, 454);
			this.panelNavigations.Name = "panelNavigations";
			this.panelNavigations.RowCount = 1;
			this.panelNavigations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panelNavigations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.panelNavigations.Size = new System.Drawing.Size(724, 32);
			this.panelNavigations.TabIndex = 25;
			// 
			// btnForce
			// 
			this.btnForce.Location = new System.Drawing.Point(361, 3);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(90, 23);
			this.btnForce.TabIndex = 21;
			this.btnForce.Text = "&Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// bntFinish
			// 
			this.bntFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bntFinish.Location = new System.Drawing.Point(652, 492);
			this.bntFinish.Name = "bntFinish";
			this.bntFinish.Size = new System.Drawing.Size(75, 24);
			this.bntFinish.TabIndex = 30;
			this.bntFinish.Text = "&Finish";
			this.bntFinish.UseVisualStyleBackColor = true;
			this.bntFinish.Click += new System.EventHandler(this.BntFinishClick);
			// 
			// chbShowReturned
			// 
			this.chbShowReturned.AutoSize = true;
			this.chbShowReturned.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbShowReturned.Enabled = false;
			this.chbShowReturned.Location = new System.Drawing.Point(268, 492);
			this.chbShowReturned.Name = "chbShowReturned";
			this.chbShowReturned.Size = new System.Drawing.Size(378, 24);
			this.chbShowReturned.TabIndex = 31;
			this.chbShowReturned.Text = "Show binarized image";
			this.chbShowReturned.UseVisualStyleBackColor = true;
			this.chbShowReturned.CheckedChanged += new System.EventHandler(this.ChbShowReturnedCheckedChanged);
			// 
			// generalizeProgressView
			// 
			this.generalizeProgressView.AutoSize = true;
			this.generalizeProgressView.Biometrics = null;
			this.tableLayoutPanel2.SetColumnSpan(this.generalizeProgressView, 3);
			this.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.generalizeProgressView.EnableMouseSelection = false;
			this.generalizeProgressView.Generalized = null;
			this.generalizeProgressView.Location = new System.Drawing.Point(3, 387);
			this.generalizeProgressView.Name = "generalizeProgressView";
			this.generalizeProgressView.Selected = null;
			this.generalizeProgressView.Size = new System.Drawing.Size(724, 35);
			this.generalizeProgressView.StatusText = "Generalize progress control";
			this.generalizeProgressView.TabIndex = 32;
			this.generalizeProgressView.View = this.fingerView;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel4, 3);
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.busyIndicator, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.lblStatus, 1, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 428);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(724, 20);
			this.tableLayoutPanel4.TabIndex = 33;
			// 
			// busyIndicator
			// 
			this.busyIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.busyIndicator.Location = new System.Drawing.Point(3, 3);
			this.busyIndicator.Name = "busyIndicator";
			this.busyIndicator.Size = new System.Drawing.Size(14, 14);
			this.busyIndicator.TabIndex = 0;
			this.busyIndicator.Visible = false;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.BackColor = System.Drawing.Color.Red;
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(23, 0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(698, 20);
			this.lblStatus.TabIndex = 24;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.gbSource, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.gbOptions, 1, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(976, 94);
			this.tableLayoutPanel3.TabIndex = 25;
			// 
			// CaptureFingersPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CaptureFingersPage";
			this.Size = new System.Drawing.Size(985, 619);
			this.gbOptions.ResumeLayout(false);
			this.gbOptions.PerformLayout();
			this.gbSource.ResumeLayout(false);
			this.gbSource.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.panelNavigations.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Neurotec.Samples.FingerSelector fingerSelector;
		private System.Windows.Forms.GroupBox gbOptions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbImpression;
		private System.Windows.Forms.ComboBox cbScenario;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbSource;
		private System.Windows.Forms.RadioButton rbFiles;
		private System.Windows.Forms.RadioButton rbScanner;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnSkip;
		private System.Windows.Forms.Button btnRepeat;
		private System.Windows.Forms.Button btnOpenImage;
		private Neurotec.Biometrics.Gui.NFingerView fingerView;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private SubjectTreeControl fingersTree;
		private Neurotec.Gui.NViewZoomSlider horizontalZoomSlider;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiMissing;
		private System.Windows.Forms.Label lblNote;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button bntFinish;
		private System.Windows.Forms.TableLayoutPanel panelNavigations;
		private System.Windows.Forms.CheckBox chbShowReturned;
		private System.Windows.Forms.RadioButton rbTenPrintCard;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.CheckBox chbWithGeneralization;
		private Neurotec.Samples.Common.GeneralizeProgressView generalizeProgressView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private BusyIndicator busyIndicator;
	}
}
