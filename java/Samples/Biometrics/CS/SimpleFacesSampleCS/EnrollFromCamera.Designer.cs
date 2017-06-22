namespace Neurotec.Samples
{
	partial class EnrollFromCamera
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.cbCameras = new System.Windows.Forms.ComboBox();
			this.btnRefreshList = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.chbCaptureAutomatically = new System.Windows.Forms.CheckBox();
			this.btnStartExtraction = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.saveTemplateDialog = new System.Windows.Forms.SaveFileDialog();
			this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
			this.facesView = new Neurotec.Biometrics.Gui.NFaceView();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.chbCheckLiveness = new System.Windows.Forms.CheckBox();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveTemplate.Location = new System.Drawing.Point(445, 337);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(105, 23);
			this.btnSaveTemplate.TabIndex = 9;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveImage.Enabled = false;
			this.btnSaveImage.Location = new System.Drawing.Point(556, 337);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(105, 23);
			this.btnSaveImage.TabIndex = 10;
			this.btnSaveImage.Text = "Save &Image";
			this.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.BtnSaveImageClick);
			// 
			// cbCameras
			// 
			this.cbCameras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCameras.FormattingEnabled = true;
			this.cbCameras.Location = new System.Drawing.Point(6, 13);
			this.cbCameras.Name = "cbCameras";
			this.cbCameras.Size = new System.Drawing.Size(641, 21);
			this.cbCameras.TabIndex = 15;
			this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.CbCameraSelectedIndexChanged);
			// 
			// btnRefreshList
			// 
			this.btnRefreshList.Location = new System.Drawing.Point(6, 40);
			this.btnRefreshList.Name = "btnRefreshList";
			this.btnRefreshList.Size = new System.Drawing.Size(75, 21);
			this.btnRefreshList.TabIndex = 17;
			this.btnRefreshList.Text = "Refresh list";
			this.btnRefreshList.UseVisualStyleBackColor = true;
			this.btnRefreshList.Click += new System.EventHandler(this.BtnRefreshListClick);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(87, 40);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(86, 21);
			this.btnStart.TabIndex = 18;
			this.btnStart.Text = "Start capturing";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(179, 40);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(74, 21);
			this.btnStop.TabIndex = 19;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// groupBox
			// 
			this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox.Controls.Add(this.chbCheckLiveness);
			this.groupBox.Controls.Add(this.chbCaptureAutomatically);
			this.groupBox.Controls.Add(this.btnStop);
			this.groupBox.Controls.Add(this.cbCameras);
			this.groupBox.Controls.Add(this.btnStart);
			this.groupBox.Controls.Add(this.btnRefreshList);
			this.groupBox.Location = new System.Drawing.Point(3, 51);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(658, 72);
			this.groupBox.TabIndex = 20;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Cameras";
			// 
			// chbCaptureAutomatically
			// 
			this.chbCaptureAutomatically.AutoSize = true;
			this.chbCaptureAutomatically.Location = new System.Drawing.Point(259, 43);
			this.chbCaptureAutomatically.Name = "chbCaptureAutomatically";
			this.chbCaptureAutomatically.Size = new System.Drawing.Size(127, 17);
			this.chbCaptureAutomatically.TabIndex = 20;
			this.chbCaptureAutomatically.Text = "Capture automatically";
			this.chbCaptureAutomatically.UseVisualStyleBackColor = true;
			// 
			// btnStartExtraction
			// 
			this.btnStartExtraction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStartExtraction.Enabled = false;
			this.btnStartExtraction.Location = new System.Drawing.Point(3, 337);
			this.btnStartExtraction.Name = "btnStartExtraction";
			this.btnStartExtraction.Size = new System.Drawing.Size(91, 23);
			this.btnStartExtraction.TabIndex = 23;
			this.btnStartExtraction.Text = "Start extraction";
			this.btnStartExtraction.UseVisualStyleBackColor = true;
			this.btnStartExtraction.Click += new System.EventHandler(this.BtnStartExtractionClick);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.Location = new System.Drawing.Point(100, 337);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(226, 23);
			this.lblStatus.TabIndex = 24;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// facesView
			// 
			this.facesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.facesView.Face = null;
			this.facesView.FaceIds = null;
			this.facesView.Location = new System.Drawing.Point(3, 129);
			this.facesView.Name = "facesView";
			this.facesView.Size = new System.Drawing.Size(658, 202);
			this.facesView.TabIndex = 13;
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider2.Location = new System.Drawing.Point(190, 337);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider2.TabIndex = 7;
			this.nViewZoomSlider2.Text = "nViewZoomSlider2";
			this.nViewZoomSlider2.View = this.facesView;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Devices.Cameras";
			this.licensePanel.Size = new System.Drawing.Size(664, 45);
			this.licensePanel.TabIndex = 25;
			// 
			// chbCheckLiveness
			// 
			this.chbCheckLiveness.AutoSize = true;
			this.chbCheckLiveness.Location = new System.Drawing.Point(392, 43);
			this.chbCheckLiveness.Name = "chbCheckLiveness";
			this.chbCheckLiveness.Size = new System.Drawing.Size(98, 17);
			this.chbCheckLiveness.TabIndex = 21;
			this.chbCheckLiveness.Text = "Check liveness";
			this.chbCheckLiveness.UseVisualStyleBackColor = true;
			this.chbCheckLiveness.CheckedChanged += new System.EventHandler(this.ChbCheckLivenessCheckedChanged);
			// 
			// EnrollFromCamera
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nViewZoomSlider2);
			this.Controls.Add(this.facesView);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnStartExtraction);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.btnSaveImage);
			this.Controls.Add(this.btnSaveTemplate);
			this.Name = "EnrollFromCamera";
			this.Size = new System.Drawing.Size(664, 363);
			this.Load += new System.EventHandler(this.EnrollFromCameraLoad);
			this.VisibleChanged += new System.EventHandler(this.EnrollFromCameraVisibleChanged);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.ComboBox cbCameras;
		private System.Windows.Forms.Button btnRefreshList;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Button btnStartExtraction;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.SaveFileDialog saveTemplateDialog;
		private System.Windows.Forms.SaveFileDialog saveImageDialog;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView facesView;
		private System.Windows.Forms.CheckBox chbCaptureAutomatically;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
		private System.Windows.Forms.CheckBox chbCheckLiveness;
	}
}
