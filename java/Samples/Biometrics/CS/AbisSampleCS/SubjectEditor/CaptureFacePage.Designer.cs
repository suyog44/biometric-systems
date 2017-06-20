namespace Neurotec.Samples
{
	partial class CaptureFacePage
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
			this.btnForceStart = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnForceEnd = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnFinish = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.generalizationView = new Neurotec.Samples.Common.GeneralizeProgressView();
			this.faceView = new Neurotec.Biometrics.Gui.NFaceView();
			this.btnRepeat = new System.Windows.Forms.Button();
			this.gbCaptureOptions = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnCapture = new System.Windows.Forms.Button();
			this.rbFromCamera = new System.Windows.Forms.RadioButton();
			this.chbWithGeneralization = new System.Windows.Forms.CheckBox();
			this.rbFromVideo = new System.Windows.Forms.RadioButton();
			this.rbFromFile = new System.Windows.Forms.RadioButton();
			this.chbManual = new System.Windows.Forms.CheckBox();
			this.chbStream = new System.Windows.Forms.CheckBox();
			this.chbCheckIcaoCompliance = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.busyIndicator = new Neurotec.Samples.BusyIndicator();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.chbMirrorHorizontally = new System.Windows.Forms.CheckBox();
			this.horizontalZoomSlider = new Neurotec.Gui.NViewZoomSlider();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.icaoWarningView = new Neurotec.Samples.IcaoWarningView();
			this.subjectTreeControl = new Neurotec.Samples.SubjectTreeControl();
			this.tableLayoutPanel3.SuspendLayout();
			this.gbCaptureOptions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnForceStart
			// 
			this.btnForceStart.Location = new System.Drawing.Point(418, 630);
			this.btnForceStart.Name = "btnForceStart";
			this.btnForceStart.Size = new System.Drawing.Size(75, 23);
			this.btnForceStart.TabIndex = 3;
			this.btnForceStart.Text = "&Start";
			this.btnForceStart.UseVisualStyleBackColor = true;
			this.btnForceStart.Click += new System.EventHandler(this.BtnForceStartClick);
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
			this.lblStatus.Size = new System.Drawing.Size(967, 20);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "Extraction status: None";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnForceEnd
			// 
			this.btnForceEnd.Location = new System.Drawing.Point(499, 630);
			this.btnForceEnd.Name = "btnForceEnd";
			this.btnForceEnd.Size = new System.Drawing.Size(75, 23);
			this.btnForceEnd.TabIndex = 4;
			this.btnForceEnd.Text = "&End";
			this.btnForceEnd.UseVisualStyleBackColor = true;
			this.btnForceEnd.Click += new System.EventHandler(this.BtnForceEndClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(340, 630);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnFinish
			// 
			this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFinish.Location = new System.Drawing.Point(921, 659);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 10;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 6;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.btnForceEnd, 3, 5);
			this.tableLayoutPanel3.Controls.Add(this.btnForceStart, 2, 5);
			this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 5);
			this.tableLayoutPanel3.Controls.Add(this.generalizationView, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.btnRepeat, 4, 5);
			this.tableLayoutPanel3.Controls.Add(this.btnFinish, 5, 6);
			this.tableLayoutPanel3.Controls.Add(this.gbCaptureOptions, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 7;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(999, 694);
			this.tableLayoutPanel3.TabIndex = 10;
			// 
			// generalizationView
			// 
			this.generalizationView.AutoSize = true;
			this.generalizationView.Biometrics = null;
			this.tableLayoutPanel3.SetColumnSpan(this.generalizationView, 5);
			this.generalizationView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.generalizationView.EnableMouseSelection = true;
			this.generalizationView.Generalized = null;
			this.generalizationView.IcaoView = null;
			this.generalizationView.Location = new System.Drawing.Point(3, 563);
			this.generalizationView.Name = "generalizationView";
			this.generalizationView.Selected = null;
			this.generalizationView.Size = new System.Drawing.Size(656, 35);
			this.generalizationView.StatusText = "Status";
			this.generalizationView.TabIndex = 13;
			this.generalizationView.View = this.faceView;
			// 
			// faceView
			// 
			this.faceView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.faceView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceView.Face = null;
			this.faceView.FaceIds = null;
			this.faceView.IcaoArrowsColor = System.Drawing.Color.Red;
			this.faceView.Location = new System.Drawing.Point(209, 3);
			this.faceView.Name = "faceView";
			this.tableLayoutPanel5.SetRowSpan(this.faceView, 2);
			this.faceView.ShowIcaoArrows = true;
			this.faceView.ShowTokenImageRectangle = true;
			this.faceView.Size = new System.Drawing.Size(781, 448);
			this.faceView.TabIndex = 6;
			this.faceView.TokenImageRectangleColor = System.Drawing.Color.White;
			// 
			// btnRepeat
			// 
			this.btnRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRepeat.Image = global::Neurotec.Samples.Properties.Resources.Repeat;
			this.btnRepeat.Location = new System.Drawing.Point(580, 630);
			this.btnRepeat.Name = "btnRepeat";
			this.btnRepeat.Size = new System.Drawing.Size(79, 23);
			this.btnRepeat.TabIndex = 18;
			this.btnRepeat.Text = "&Repeat";
			this.btnRepeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRepeat.UseVisualStyleBackColor = true;
			this.btnRepeat.Click += new System.EventHandler(this.BtnRepeatClick);
			// 
			// gbCaptureOptions
			// 
			this.gbCaptureOptions.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.gbCaptureOptions, 6);
			this.gbCaptureOptions.Controls.Add(this.tableLayoutPanel1);
			this.gbCaptureOptions.Location = new System.Drawing.Point(3, 3);
			this.gbCaptureOptions.Name = "gbCaptureOptions";
			this.gbCaptureOptions.Size = new System.Drawing.Size(388, 94);
			this.gbCaptureOptions.TabIndex = 19;
			this.gbCaptureOptions.TabStop = false;
			this.gbCaptureOptions.Text = "Capture options";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.btnCapture, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.rbFromCamera, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.chbWithGeneralization, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.rbFromVideo, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.rbFromFile, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.chbManual, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.chbStream, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.chbCheckIcaoCompliance, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 75);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnCapture
			// 
			this.btnCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCapture.Location = new System.Drawing.Point(104, 49);
			this.btnCapture.Name = "btnCapture";
			this.btnCapture.Size = new System.Drawing.Size(75, 23);
			this.btnCapture.TabIndex = 9;
			this.btnCapture.Text = "Capture";
			this.btnCapture.UseVisualStyleBackColor = true;
			this.btnCapture.Click += new System.EventHandler(this.BtnCaptureClick);
			// 
			// rbFromCamera
			// 
			this.rbFromCamera.AutoSize = true;
			this.rbFromCamera.Checked = true;
			this.rbFromCamera.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rbFromCamera.Location = new System.Drawing.Point(3, 3);
			this.rbFromCamera.Name = "rbFromCamera";
			this.rbFromCamera.Size = new System.Drawing.Size(95, 17);
			this.rbFromCamera.TabIndex = 6;
			this.rbFromCamera.TabStop = true;
			this.rbFromCamera.Text = "From camera";
			this.rbFromCamera.UseVisualStyleBackColor = true;
			this.rbFromCamera.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// chbWithGeneralization
			// 
			this.chbWithGeneralization.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbWithGeneralization, 2);
			this.chbWithGeneralization.Location = new System.Drawing.Point(104, 26);
			this.chbWithGeneralization.Name = "chbWithGeneralization";
			this.chbWithGeneralization.Size = new System.Drawing.Size(116, 17);
			this.chbWithGeneralization.TabIndex = 12;
			this.chbWithGeneralization.Text = "With generalization";
			this.chbWithGeneralization.UseVisualStyleBackColor = true;
			// 
			// rbFromVideo
			// 
			this.rbFromVideo.AutoSize = true;
			this.rbFromVideo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rbFromVideo.Location = new System.Drawing.Point(3, 49);
			this.rbFromVideo.Name = "rbFromVideo";
			this.rbFromVideo.Size = new System.Drawing.Size(95, 23);
			this.rbFromVideo.TabIndex = 8;
			this.rbFromVideo.Text = "From video file";
			this.rbFromVideo.UseVisualStyleBackColor = true;
			this.rbFromVideo.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// rbFromFile
			// 
			this.rbFromFile.AutoSize = true;
			this.rbFromFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rbFromFile.Location = new System.Drawing.Point(3, 26);
			this.rbFromFile.Name = "rbFromFile";
			this.rbFromFile.Size = new System.Drawing.Size(95, 17);
			this.rbFromFile.TabIndex = 7;
			this.rbFromFile.Text = "From image file";
			this.rbFromFile.UseVisualStyleBackColor = true;
			this.rbFromFile.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// chbManual
			// 
			this.chbManual.AutoSize = true;
			this.chbManual.Location = new System.Drawing.Point(318, 3);
			this.chbManual.Name = "chbManual";
			this.chbManual.Size = new System.Drawing.Size(61, 17);
			this.chbManual.TabIndex = 1;
			this.chbManual.Text = "Manual";
			this.chbManual.UseVisualStyleBackColor = true;
			// 
			// chbStream
			// 
			this.chbStream.AutoSize = true;
			this.chbStream.Checked = true;
			this.chbStream.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbStream.Location = new System.Drawing.Point(253, 3);
			this.chbStream.Name = "chbStream";
			this.chbStream.Size = new System.Drawing.Size(59, 17);
			this.chbStream.TabIndex = 0;
			this.chbStream.Text = "Stream";
			this.chbStream.UseVisualStyleBackColor = true;
			// 
			// chbCheckIcaoCompliance
			// 
			this.chbCheckIcaoCompliance.AutoSize = true;
			this.chbCheckIcaoCompliance.Checked = true;
			this.chbCheckIcaoCompliance.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbCheckIcaoCompliance.Location = new System.Drawing.Point(104, 3);
			this.chbCheckIcaoCompliance.Name = "chbCheckIcaoCompliance";
			this.chbCheckIcaoCompliance.Size = new System.Drawing.Size(143, 17);
			this.chbCheckIcaoCompliance.TabIndex = 13;
			this.chbCheckIcaoCompliance.Text = "Check ICAO Compliance";
			this.chbCheckIcaoCompliance.UseVisualStyleBackColor = true;
			this.chbCheckIcaoCompliance.CheckedChanged += new System.EventHandler(this.ChbCheckIcaoComplianceCheckedChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel2, 6);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.busyIndicator, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblStatus, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 604);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(993, 20);
			this.tableLayoutPanel2.TabIndex = 20;
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
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel4, 5);
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.chbMirrorHorizontally, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.horizontalZoomSlider, 1, 0);
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 659);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(364, 32);
			this.tableLayoutPanel4.TabIndex = 21;
			// 
			// chbMirrorHorizontally
			// 
			this.chbMirrorHorizontally.AutoSize = true;
			this.chbMirrorHorizontally.Checked = true;
			this.chbMirrorHorizontally.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbMirrorHorizontally.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chbMirrorHorizontally.Location = new System.Drawing.Point(3, 2);
			this.chbMirrorHorizontally.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.chbMirrorHorizontally.Name = "chbMirrorHorizontally";
			this.chbMirrorHorizontally.Size = new System.Drawing.Size(77, 27);
			this.chbMirrorHorizontally.TabIndex = 21;
			this.chbMirrorHorizontally.Text = "Mirror view";
			this.chbMirrorHorizontally.UseVisualStyleBackColor = true;
			this.chbMirrorHorizontally.CheckedChanged += new System.EventHandler(this.ChbMirrorHorizontallyCheckedChanged);
			// 
			// horizontalZoomSlider
			// 
			this.horizontalZoomSlider.Location = new System.Drawing.Point(86, 3);
			this.horizontalZoomSlider.Name = "horizontalZoomSlider";
			this.horizontalZoomSlider.Size = new System.Drawing.Size(275, 26);
			this.horizontalZoomSlider.TabIndex = 11;
			this.horizontalZoomSlider.View = this.faceView;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 2;
			this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel5, 6);
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.icaoWarningView, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.subjectTreeControl, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.faceView, 1, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 103);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.Size = new System.Drawing.Size(993, 454);
			this.tableLayoutPanel5.TabIndex = 22;
			// 
			// icaoWarningView
			// 
			this.icaoWarningView.AutoScroll = true;
			this.icaoWarningView.AutoSize = true;
			this.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.icaoWarningView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange;
			this.icaoWarningView.Location = new System.Drawing.Point(3, 3);
			this.icaoWarningView.MinimumSize = new System.Drawing.Size(200, 0);
			this.icaoWarningView.Name = "icaoWarningView";
			this.icaoWarningView.NoWarningColor = System.Drawing.Color.Green;
			this.icaoWarningView.Size = new System.Drawing.Size(200, 392);
			this.icaoWarningView.TabIndex = 25;
			this.icaoWarningView.Visible = false;
			this.icaoWarningView.WarningColor = System.Drawing.Color.Red;
			// 
			// subjectTreeControl
			// 
			this.subjectTreeControl.AllowNew = Neurotec.Biometrics.NBiometricType.None;
			this.subjectTreeControl.AllowRemove = false;
			this.subjectTreeControl.AutoSize = true;
			this.subjectTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.subjectTreeControl.Location = new System.Drawing.Point(3, 401);
			this.subjectTreeControl.MinimumSize = new System.Drawing.Size(200, 50);
			this.subjectTreeControl.Name = "subjectTreeControl";
			this.subjectTreeControl.SelectedItem = null;
			this.subjectTreeControl.ShowBiometricsOnly = true;
			this.subjectTreeControl.ShownTypes = ((Neurotec.Biometrics.NBiometricType)(((((Neurotec.Biometrics.NBiometricType.Face | Neurotec.Biometrics.NBiometricType.Voice)
						| Neurotec.Biometrics.NBiometricType.Fingerprint)
						| Neurotec.Biometrics.NBiometricType.Iris)
						| Neurotec.Biometrics.NBiometricType.PalmPrint)));
			this.subjectTreeControl.Size = new System.Drawing.Size(200, 50);
			this.subjectTreeControl.Subject = null;
			this.subjectTreeControl.TabIndex = 24;
			this.subjectTreeControl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.OnSubjectTreeControlPropertyChanged);
			// 
			// CaptureFacePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Name = "CaptureFacePage";
			this.Size = new System.Drawing.Size(999, 694);
			this.Load += new System.EventHandler(this.CaptureFacePageLoad);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.gbCaptureOptions.ResumeLayout(false);
			this.gbCaptureOptions.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnForceStart;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnForceEnd;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnRepeat;
		private Neurotec.Biometrics.Gui.NFaceView faceView;
		private Neurotec.Samples.Common.GeneralizeProgressView generalizationView;
		private Neurotec.Gui.NViewZoomSlider horizontalZoomSlider;
		private System.Windows.Forms.GroupBox gbCaptureOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnCapture;
		private System.Windows.Forms.RadioButton rbFromCamera;
		private System.Windows.Forms.CheckBox chbWithGeneralization;
		private System.Windows.Forms.RadioButton rbFromVideo;
		private System.Windows.Forms.CheckBox chbManual;
		private System.Windows.Forms.RadioButton rbFromFile;
		private System.Windows.Forms.CheckBox chbStream;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private BusyIndicator busyIndicator;
		private System.Windows.Forms.CheckBox chbMirrorHorizontally;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.CheckBox chbCheckIcaoCompliance;
		private IcaoWarningView icaoWarningView;
		private SubjectTreeControl subjectTreeControl;
	}
}
