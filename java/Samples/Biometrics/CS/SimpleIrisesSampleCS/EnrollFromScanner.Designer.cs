namespace Neurotec.Samples
{
	partial class EnrollFromScanner
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollFromScanner));
			this.scannersGroupBox = new System.Windows.Forms.GroupBox();
			this.chbScanAutomatically = new System.Windows.Forms.CheckBox();
			this.btnForce = new System.Windows.Forms.Button();
			this.rbRight = new System.Windows.Forms.RadioButton();
			this.rbLeft = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnScan = new System.Windows.Forms.Button();
			this.lbScanners = new System.Windows.Forms.ListBox();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.irisView = new Neurotec.Biometrics.Gui.NIrisView();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.lblStatus = new System.Windows.Forms.Label();
			this.scannersGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// scannersGroupBox
			// 
			this.scannersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scannersGroupBox.Controls.Add(this.chbScanAutomatically);
			this.scannersGroupBox.Controls.Add(this.btnForce);
			this.scannersGroupBox.Controls.Add(this.rbRight);
			this.scannersGroupBox.Controls.Add(this.rbLeft);
			this.scannersGroupBox.Controls.Add(this.btnCancel);
			this.scannersGroupBox.Controls.Add(this.btnRefresh);
			this.scannersGroupBox.Controls.Add(this.btnScan);
			this.scannersGroupBox.Controls.Add(this.lbScanners);
			this.scannersGroupBox.Location = new System.Drawing.Point(3, 51);
			this.scannersGroupBox.Name = "scannersGroupBox";
			this.scannersGroupBox.Size = new System.Drawing.Size(583, 115);
			this.scannersGroupBox.TabIndex = 12;
			this.scannersGroupBox.TabStop = false;
			this.scannersGroupBox.Text = "Scanners list";
			// 
			// chbScanAutomatically
			// 
			this.chbScanAutomatically.AutoSize = true;
			this.chbScanAutomatically.Checked = true;
			this.chbScanAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbScanAutomatically.Location = new System.Drawing.Point(330, 85);
			this.chbScanAutomatically.Name = "chbScanAutomatically";
			this.chbScanAutomatically.Size = new System.Drawing.Size(115, 17);
			this.chbScanAutomatically.TabIndex = 15;
			this.chbScanAutomatically.Text = "Scan automatically";
			this.chbScanAutomatically.UseVisualStyleBackColor = true;
			// 
			// btnForce
			// 
			this.btnForce.Enabled = false;
			this.btnForce.Location = new System.Drawing.Point(249, 81);
			this.btnForce.Name = "btnForce";
			this.btnForce.Size = new System.Drawing.Size(75, 23);
			this.btnForce.TabIndex = 14;
			this.btnForce.Text = "Force";
			this.btnForce.UseVisualStyleBackColor = true;
			this.btnForce.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// rbRight
			// 
			this.rbRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbRight.AutoSize = true;
			this.rbRight.Location = new System.Drawing.Point(511, 53);
			this.rbRight.Name = "rbRight";
			this.rbRight.Size = new System.Drawing.Size(66, 17);
			this.rbRight.TabIndex = 13;
			this.rbRight.Text = "Right Iris";
			this.rbRight.UseVisualStyleBackColor = true;
			// 
			// rbLeft
			// 
			this.rbLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbLeft.AutoSize = true;
			this.rbLeft.Checked = true;
			this.rbLeft.Location = new System.Drawing.Point(511, 30);
			this.rbLeft.Name = "rbLeft";
			this.rbLeft.Size = new System.Drawing.Size(59, 17);
			this.rbLeft.TabIndex = 12;
			this.rbLeft.TabStop = true;
			this.rbLeft.Text = "Left Iris";
			this.rbLeft.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(168, 81);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(6, 81);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 10;
			this.btnRefresh.Text = "Refresh list";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.BtnRefreshClick);
			// 
			// btnScan
			// 
			this.btnScan.Location = new System.Drawing.Point(87, 81);
			this.btnScan.Name = "btnScan";
			this.btnScan.Size = new System.Drawing.Size(75, 23);
			this.btnScan.TabIndex = 9;
			this.btnScan.Text = "Scan";
			this.btnScan.UseVisualStyleBackColor = true;
			this.btnScan.Click += new System.EventHandler(this.BtnScanClick);
			// 
			// lbScanners
			// 
			this.lbScanners.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbScanners.Location = new System.Drawing.Point(4, 19);
			this.lbScanners.Name = "lbScanners";
			this.lbScanners.Size = new System.Drawing.Size(501, 56);
			this.lbScanners.TabIndex = 6;
			this.lbScanners.SelectedIndexChanged += new System.EventHandler(this.LbScannersSelectedIndexChanged);
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Location = new System.Drawing.Point(490, 330);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(96, 23);
			this.btnSaveTemplate.TabIndex = 16;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveImage.Enabled = false;
			this.btnSaveImage.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveImage.Image")));
			this.btnSaveImage.Location = new System.Drawing.Point(388, 330);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(96, 23);
			this.btnSaveImage.TabIndex = 15;
			this.btnSaveImage.Text = "Save Image";
			this.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.BtnSaveImageClick);
			// 
			// irisView
			// 
			this.irisView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.irisView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.irisView.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView.InnerBoundaryWidth = 2;
			this.irisView.Iris = null;
			this.irisView.Location = new System.Drawing.Point(3, 172);
			this.irisView.Name = "irisView";
			this.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView.OuterBoundaryWidth = 2;
			this.irisView.Size = new System.Drawing.Size(583, 152);
			this.irisView.TabIndex = 13;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Devices.IrisScanners";
			this.licensePanel.Size = new System.Drawing.Size(586, 45);
			this.licensePanel.TabIndex = 14;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(133, 330);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider1.TabIndex = 17;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.irisView;
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(4, 335);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 18;
			// 
			// EnrollFromScanner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.nViewZoomSlider1);
			this.Controls.Add(this.irisView);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.btnSaveImage);
			this.Controls.Add(this.scannersGroupBox);
			this.Name = "EnrollFromScanner";
			this.Size = new System.Drawing.Size(589, 356);
			this.Load += new System.EventHandler(this.EnrollFromScannerLoad);
			this.VisibleChanged += new System.EventHandler(this.EnrollFromScannerVisibleChanged);
			this.scannersGroupBox.ResumeLayout(false);
			this.scannersGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox scannersGroupBox;
		private System.Windows.Forms.RadioButton rbRight;
		private System.Windows.Forms.RadioButton rbLeft;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnScan;
		private System.Windows.Forms.ListBox lbScanners;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NIrisView irisView;
		private System.Windows.Forms.Button btnForce;
		private System.Windows.Forms.CheckBox chbScanAutomatically;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private System.Windows.Forms.Label lblStatus;
	}
}
