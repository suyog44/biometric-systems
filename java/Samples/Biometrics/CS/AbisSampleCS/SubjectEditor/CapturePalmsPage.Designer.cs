namespace Neurotec.Samples
{
	partial class CapturePalmsPage
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
			this.palmSelector = new Neurotec.Samples.PalmSelector();
			this.gbSource = new System.Windows.Forms.GroupBox();
			this.rbFile = new System.Windows.Forms.RadioButton();
			this.rbScanner = new System.Windows.Forms.RadioButton();
			this.palmView = new Neurotec.Biometrics.Gui.NFingerView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnCapture = new System.Windows.Forms.Button();
			this.cbSelectedPosition = new System.Windows.Forms.ComboBox();
			this.gbOptions = new System.Windows.Forms.GroupBox();
			this.chbWithGeneralization = new System.Windows.Forms.CheckBox();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbImpression = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.palmsTree = new Neurotec.Samples.SubjectTreeControl();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnForce = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnFinish = new System.Windows.Forms.Button();
			this.horizontalZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.chbShowReturned = new System.Windows.Forms.CheckBox();
			this.generalizeProgressView = new Neurotec.Samples.Common.GeneralizeProgressView();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.busyIndicator = new Neurotec.Samples.BusyIndicator();
			this.lblStatus = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.gbSource.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.gbOptions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// palmSelector
			// 
			this.palmSelector.AllowedPositions = new Neurotec.Biometrics.NFPosition[] {
        Neurotec.Biometrics.NFPosition.RightUpperPalm,
        Neurotec.Biometrics.NFPosition.RightLowerPalm,
        Neurotec.Biometrics.NFPosition.RightInterdigital,
        Neurotec.Biometrics.NFPosition.RightHypothenar,
        Neurotec.Biometrics.NFPosition.RightThenar,
        Neurotec.Biometrics.NFPosition.RightFullPalm,
        Neurotec.Biometrics.NFPosition.LeftUpperPalm,
        Neurotec.Biometrics.NFPosition.LeftLowerPalm,
        Neurotec.Biometrics.NFPosition.LeftInterdigital,
        Neurotec.Biometrics.NFPosition.LeftHypothenar,
        Neurotec.Biometrics.NFPosition.LeftThenar,
        Neurotec.Biometrics.NFPosition.LeftFullPalm};
			this.palmSelector.AllowHighlight = true;
			this.palmSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.palmSelector, 2);
			this.palmSelector.IsRolled = false;
			this.palmSelector.Location = new System.Drawing.Point(3, 170);
			this.palmSelector.MissingPositions = new Neurotec.Biometrics.NFPosition[0];
			this.palmSelector.Name = "palmSelector";
			this.palmSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown;
			this.palmSelector.ShowFingerNails = false;
			this.palmSelector.ShowPalmCreases = true;
			this.palmSelector.Size = new System.Drawing.Size(217, 93);
			this.palmSelector.TabIndex = 0;
			this.palmSelector.Text = "palmSelector";
			this.palmSelector.FingerClick += new System.EventHandler<Neurotec.Samples.FingerSelector.FingerClickArgs>(this.PalmSelectorFingerClick);
			// 
			// gbSource
			// 
			this.gbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.gbSource, 2);
			this.gbSource.Controls.Add(this.rbFile);
			this.gbSource.Controls.Add(this.rbScanner);
			this.gbSource.Location = new System.Drawing.Point(3, 3);
			this.gbSource.Name = "gbSource";
			this.gbSource.Size = new System.Drawing.Size(217, 72);
			this.gbSource.TabIndex = 1;
			this.gbSource.TabStop = false;
			this.gbSource.Text = "Source";
			// 
			// rbFile
			// 
			this.rbFile.AutoSize = true;
			this.rbFile.Location = new System.Drawing.Point(13, 42);
			this.rbFile.Name = "rbFile";
			this.rbFile.Size = new System.Drawing.Size(41, 17);
			this.rbFile.TabIndex = 1;
			this.rbFile.Text = "File";
			this.rbFile.UseVisualStyleBackColor = true;
			this.rbFile.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbScanner
			// 
			this.rbScanner.AutoSize = true;
			this.rbScanner.Checked = true;
			this.rbScanner.Location = new System.Drawing.Point(13, 19);
			this.rbScanner.Name = "rbScanner";
			this.rbScanner.Size = new System.Drawing.Size(65, 17);
			this.rbScanner.TabIndex = 0;
			this.rbScanner.TabStop = true;
			this.rbScanner.Text = "Scanner";
			this.rbScanner.UseVisualStyleBackColor = true;
			this.rbScanner.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// palmView
			// 
			this.palmView.BackColor = System.Drawing.SystemColors.Control;
			this.palmView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.palmView.BoundingRectColor = System.Drawing.Color.Red;
			this.tableLayoutPanel1.SetColumnSpan(this.palmView, 3);
			this.palmView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.palmView.Location = new System.Drawing.Point(3, 3);
			this.palmView.MinutiaColor = System.Drawing.Color.Red;
			this.palmView.Name = "palmView";
			this.palmView.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.palmView.ResultImageColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
			this.palmView.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.palmView.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.palmView.SingularPointColor = System.Drawing.Color.Red;
			this.palmView.Size = new System.Drawing.Size(672, 338);
			this.palmView.TabIndex = 2;
			this.palmView.TreeColor = System.Drawing.Color.Crimson;
			this.palmView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.palmView.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.palmView.TreeWidth = 2;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(905, 446);
			this.splitContainer1.SplitterDistance = 223;
			this.splitContainer1.TabIndex = 3;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.btnCapture, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.cbSelectedPosition, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.palmSelector, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.gbOptions, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.gbSource, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.palmsTree, 0, 8);
			this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.btnForce, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.btnOpen, 0, 7);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 9;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(223, 446);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// btnCapture
			// 
			this.btnCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCapture.Location = new System.Drawing.Point(3, 296);
			this.btnCapture.Name = "btnCapture";
			this.btnCapture.Size = new System.Drawing.Size(91, 23);
			this.btnCapture.TabIndex = 5;
			this.btnCapture.Text = "&Capture";
			this.btnCapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCapture.UseVisualStyleBackColor = true;
			this.btnCapture.Click += new System.EventHandler(this.BtnCaptureClick);
			// 
			// cbSelectedPosition
			// 
			this.cbSelectedPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbSelectedPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSelectedPosition.Enabled = false;
			this.cbSelectedPosition.FormattingEnabled = true;
			this.cbSelectedPosition.Location = new System.Drawing.Point(100, 269);
			this.cbSelectedPosition.MaxDropDownItems = 20;
			this.cbSelectedPosition.Name = "cbSelectedPosition";
			this.cbSelectedPosition.Size = new System.Drawing.Size(120, 21);
			this.cbSelectedPosition.TabIndex = 5;
			this.cbSelectedPosition.SelectedIndexChanged += new System.EventHandler(this.CbSelectedPositionSelectedIndexChanged);
			// 
			// gbOptions
			// 
			this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.SetColumnSpan(this.gbOptions, 2);
			this.gbOptions.Controls.Add(this.chbWithGeneralization);
			this.gbOptions.Controls.Add(this.chbCaptureAutomatically);
			this.gbOptions.Controls.Add(this.label2);
			this.gbOptions.Controls.Add(this.cbImpression);
			this.gbOptions.Location = new System.Drawing.Point(3, 81);
			this.gbOptions.Name = "gbOptions";
			this.gbOptions.Size = new System.Drawing.Size(217, 83);
			this.gbOptions.TabIndex = 15;
			this.gbOptions.TabStop = false;
			this.gbOptions.Text = "Options";
			// 
			// chbWithGeneralization
			// 
			this.chbWithGeneralization.AutoSize = true;
			this.chbWithGeneralization.Location = new System.Drawing.Point(9, 63);
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
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(9, 40);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 5;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
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
			this.cbImpression.Location = new System.Drawing.Point(69, 13);
			this.cbImpression.MaxDropDownItems = 20;
			this.cbImpression.Name = "cbImpression";
			this.cbImpression.Size = new System.Drawing.Size(142, 21);
			this.cbImpression.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 266);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 27);
			this.label1.TabIndex = 16;
			this.label1.Text = "Selected position:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// palmsTree
			// 
			this.palmsTree.AllowNew = Neurotec.Biometrics.NBiometricType.None;
			this.palmsTree.AllowRemove = true;
			this.tableLayoutPanel2.SetColumnSpan(this.palmsTree, 2);
			this.palmsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.palmsTree.Location = new System.Drawing.Point(3, 383);
			this.palmsTree.Name = "palmsTree";
			this.palmsTree.SelectedItem = null;
			this.palmsTree.ShowBiometricsOnly = true;
			this.palmsTree.ShownTypes = Neurotec.Biometrics.NBiometricType.PalmPrint;
			this.palmsTree.Size = new System.Drawing.Size(217, 60);
			this.palmsTree.Subject = null;
			this.palmsTree.TabIndex = 17;
			this.palmsTree.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.PalmTreePropertyChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(100, 296);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(91, 23);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnForce
			// 
			this.btnForce.Location = new System.Drawing.Point(3, 325);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(91, 23);
			this.btnForce.TabIndex = 20;
			this.btnForce.Text = "&Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// btnOpen
			// 
			this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOpen.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.btnOpen.Location = new System.Drawing.Point(3, 354);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(91, 23);
			this.btnOpen.TabIndex = 18;
			this.btnOpen.Text = "Open";
			this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.19643F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.80357F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.palmView, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnFinish, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.horizontalZoomSlider, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.chbShowReturned, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.generalizeProgressView, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 446);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnFinish
			// 
			this.btnFinish.Location = new System.Drawing.Point(599, 414);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 4;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// horizontalZoomSlider
			// 
			this.horizontalZoomSlider.Location = new System.Drawing.Point(3, 414);
			this.horizontalZoomSlider.Name = "horizontalZoomSlider";
			this.horizontalZoomSlider.Size = new System.Drawing.Size(257, 29);
			this.horizontalZoomSlider.TabIndex = 26;
			this.horizontalZoomSlider.View = this.palmView;
			// 
			// chbShowReturned
			// 
			this.chbShowReturned.AutoSize = true;
			this.chbShowReturned.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbShowReturned.Location = new System.Drawing.Point(266, 414);
			this.chbShowReturned.Name = "chbShowReturned";
			this.chbShowReturned.Size = new System.Drawing.Size(327, 29);
			this.chbShowReturned.TabIndex = 27;
			this.chbShowReturned.Text = "Show binarized image";
			this.chbShowReturned.UseVisualStyleBackColor = true;
			this.chbShowReturned.CheckedChanged += new System.EventHandler(this.ChbShowReturnedCheckedChanged);
			// 
			// generalizeProgressView
			// 
			this.generalizeProgressView.AutoSize = true;
			this.generalizeProgressView.Biometrics = null;
			this.tableLayoutPanel1.SetColumnSpan(this.generalizeProgressView, 3);
			this.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.generalizeProgressView.EnableMouseSelection = false;
			this.generalizeProgressView.Generalized = null;
			this.generalizeProgressView.Location = new System.Drawing.Point(3, 347);
			this.generalizeProgressView.Name = "generalizeProgressView";
			this.generalizeProgressView.Selected = null;
			this.generalizeProgressView.Size = new System.Drawing.Size(672, 35);
			this.generalizeProgressView.StatusText = "Generalization status";
			this.generalizeProgressView.TabIndex = 28;
			this.generalizeProgressView.View = this.palmView;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 3);
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.busyIndicator, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblStatus, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 388);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(672, 20);
			this.tableLayoutPanel3.TabIndex = 29;
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
			this.lblStatus.Size = new System.Drawing.Size(646, 20);
			this.lblStatus.TabIndex = 3;
			this.lblStatus.Text = "Status:";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CapturePalmsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.splitContainer1);
			this.Name = "CapturePalmsPage";
			this.Size = new System.Drawing.Size(905, 446);
			this.gbSource.ResumeLayout(false);
			this.gbSource.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.gbOptions.ResumeLayout(false);
			this.gbOptions.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private PalmSelector palmSelector;
		private System.Windows.Forms.GroupBox gbSource;
		private System.Windows.Forms.RadioButton rbFile;
		private System.Windows.Forms.RadioButton rbScanner;
		private Neurotec.Biometrics.Gui.NFingerView palmView;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnCapture;
		private System.Windows.Forms.ComboBox cbSelectedPosition;
		private System.Windows.Forms.GroupBox gbOptions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbImpression;
		private System.Windows.Forms.Label label1;
		private SubjectTreeControl palmsTree;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Neurotec.Gui.NViewZoomSlider horizontalZoomSlider;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.CheckBox chbShowReturned;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.CheckBox chbWithGeneralization;
		private Neurotec.Samples.Common.GeneralizeProgressView generalizeProgressView;
		private BusyIndicator busyIndicator;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	}
}
