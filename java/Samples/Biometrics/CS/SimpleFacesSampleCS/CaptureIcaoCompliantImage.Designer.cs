namespace Neurotec.Samples
{
	partial class CaptureIcaoCompliantImage
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
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.cbCameras = new System.Windows.Forms.ComboBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnRefreshList = new System.Windows.Forms.Button();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.faceView = new Neurotec.Biometrics.Gui.NFaceView();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnForce = new System.Windows.Forms.Button();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.icaoWarningView = new Neurotec.Samples.IcaoWarningView();
			this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
			this.saveTemplateDialog = new System.Windows.Forms.SaveFileDialog();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.groupBox.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox.Controls.Add(this.btnStop);
			this.groupBox.Controls.Add(this.cbCameras);
			this.groupBox.Controls.Add(this.btnStart);
			this.groupBox.Controls.Add(this.btnRefreshList);
			this.groupBox.Location = new System.Drawing.Point(3, 54);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(783, 72);
			this.groupBox.TabIndex = 27;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Cameras";
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
			// cbCameras
			// 
			this.cbCameras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCameras.FormattingEnabled = true;
			this.cbCameras.Location = new System.Drawing.Point(6, 13);
			this.cbCameras.Name = "cbCameras";
			this.cbCameras.Size = new System.Drawing.Size(766, 21);
			this.cbCameras.TabIndex = 15;
			this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.CbCamerasSelectedIndexChanged);
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
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider2.Location = new System.Drawing.Point(315, 484);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider2.TabIndex = 29;
			this.nViewZoomSlider2.Text = "nViewZoomSlider2";
			this.nViewZoomSlider2.View = null;
			// 
			// faceView
			// 
			this.faceView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceView.Face = null;
			this.faceView.FaceIds = null;
			this.faceView.IcaoArrowsColor = System.Drawing.Color.Red;
			this.faceView.Location = new System.Drawing.Point(209, 3);
			this.faceView.Name = "faceView";
			this.faceView.ShowAge = false;
			this.faceView.ShowEmotions = false;
			this.faceView.ShowExpression = false;
			this.faceView.ShowGender = false;
			this.faceView.ShowIcaoArrows = true;
			this.faceView.ShowProperties = false;
			this.faceView.ShowTokenImageRectangle = true;
			this.faceView.Size = new System.Drawing.Size(571, 340);
			this.faceView.TabIndex = 29;
			this.faceView.TokenImageRectangleColor = System.Drawing.Color.White;
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.Location = new System.Drawing.Point(100, 484);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(209, 23);
			this.lblStatus.TabIndex = 33;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnForce
			// 
			this.btnForce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnForce.Enabled = false;
			this.btnForce.Location = new System.Drawing.Point(3, 484);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(91, 23);
			this.btnForce.TabIndex = 32;
			this.btnForce.Text = "Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveImage.Enabled = false;
			this.btnSaveImage.Location = new System.Drawing.Point(681, 484);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(105, 23);
			this.btnSaveImage.TabIndex = 31;
			this.btnSaveImage.Text = "Save &Image";
			this.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.BtnSaveImageClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.faceView, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.icaoWarningView, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 132);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 346);
			this.tableLayoutPanel1.TabIndex = 34;
			// 
			// icaoWarningView
			// 
			this.icaoWarningView.AutoSize = true;
			this.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.icaoWarningView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange;
			this.icaoWarningView.Location = new System.Drawing.Point(3, 3);
			this.icaoWarningView.MinimumSize = new System.Drawing.Size(200, 0);
			this.icaoWarningView.Name = "icaoWarningView";
			this.icaoWarningView.NoWarningColor = System.Drawing.Color.Green;
			this.icaoWarningView.Size = new System.Drawing.Size(200, 340);
			this.icaoWarningView.TabIndex = 30;
			this.icaoWarningView.WarningColor = System.Drawing.Color.Red;
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveTemplate.Location = new System.Drawing.Point(570, 484);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(105, 23);
			this.btnSaveTemplate.TabIndex = 30;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 3);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceSegmentsDetection,Devices.Cameras";
			this.licensePanel.Size = new System.Drawing.Size(783, 45);
			this.licensePanel.TabIndex = 26;
			// 
			// CaptureIcaoCompliantImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.nViewZoomSlider2);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnForce);
			this.Controls.Add(this.btnSaveImage);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.licensePanel);
			this.Name = "CaptureIcaoCompliantImage";
			this.Size = new System.Drawing.Size(789, 510);
			this.Load += new System.EventHandler(this.CaptureIcaoCompliantImageLoad);
			this.groupBox.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LicensePanel licensePanel;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.ComboBox cbCameras;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnRefreshList;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Neurotec.Biometrics.Gui.NFaceView faceView;
		private System.Windows.Forms.SaveFileDialog saveImageDialog;
		private System.Windows.Forms.SaveFileDialog saveTemplateDialog;
		private IcaoWarningView icaoWarningView;
	}
}
