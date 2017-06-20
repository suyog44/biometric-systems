namespace Neurotec.Samples
{
	partial class DetectFaces
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectFaces));
			this.tsbDetectFacialFeatures = new System.Windows.Forms.ToolStripButton();
			this.cbRollAngle = new System.Windows.Forms.ToolStripComboBox();
			this.tlsLabel = new System.Windows.Forms.ToolStripLabel();
			this.tsbOpenImage = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.cbYawAngle = new System.Windows.Forms.ToolStripComboBox();
			this.openFaceImageDlg = new System.Windows.Forms.OpenFileDialog();
			this.facesView = new Neurotec.Biometrics.Gui.NFaceView();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsbDetectFacialFeatures
			// 
			this.tsbDetectFacialFeatures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbDetectFacialFeatures.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbDetectFacialFeatures.Image = ((System.Drawing.Image)(resources.GetObject("tsbDetectFacialFeatures.Image")));
			this.tsbDetectFacialFeatures.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDetectFacialFeatures.Name = "tsbDetectFacialFeatures";
			this.tsbDetectFacialFeatures.Size = new System.Drawing.Size(134, 22);
			this.tsbDetectFacialFeatures.Text = "&Detect Facial Features";
			this.tsbDetectFacialFeatures.Click += new System.EventHandler(this.TsbDetectFacialFeaturesClick);
			// 
			// cbRollAngle
			// 
			this.cbRollAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRollAngle.Name = "cbRollAngle";
			this.cbRollAngle.Size = new System.Drawing.Size(121, 25);
			this.cbRollAngle.SelectedIndexChanged += new System.EventHandler(this.CbRollAngleSelectedIndexChanged);
			// 
			// tlsLabel
			// 
			this.tlsLabel.Name = "tlsLabel";
			this.tlsLabel.Size = new System.Drawing.Size(136, 22);
			this.tlsLabel.Text = "Max roll angle deviation:";
			// 
			// tsbOpenImage
			// 
			this.tsbOpenImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenImage.Name = "tsbOpenImage";
			this.tsbOpenImage.Size = new System.Drawing.Size(101, 22);
			this.tsbOpenImage.Text = "&Open image...";
			this.tsbOpenImage.Click += new System.EventHandler(this.TsbOpenImageClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenImage,
            this.tlsLabel,
            this.cbRollAngle,
            this.toolStripLabel1,
            this.cbYawAngle,
            this.tsbDetectFacialFeatures});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(792, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(140, 22);
			this.toolStripLabel1.Text = "Max yaw angle deviation:";
			// 
			// cbYawAngle
			// 
			this.cbYawAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbYawAngle.Name = "cbYawAngle";
			this.cbYawAngle.Size = new System.Drawing.Size(121, 25);
			this.cbYawAngle.SelectedIndexChanged += new System.EventHandler(this.CbYawAngleSelectedIndexChanged);
			// 
			// facesView
			// 
			this.facesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.facesView.BaseFeaturePointSize = 4;
			this.facesView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.facesView.Face = null;
			this.facesView.FaceIds = null;
			this.facesView.Location = new System.Drawing.Point(0, 79);
			this.facesView.Name = "facesView";
			this.facesView.Size = new System.Drawing.Size(792, 261);
			this.facesView.TabIndex = 2;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(3, 346);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(273, 23);
			this.nViewZoomSlider1.TabIndex = 5;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.facesView;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 25);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "Biometrics.FaceSegmentsDetection";
			this.licensePanel.RequiredComponents = "Biometrics.FaceDetection";
			this.licensePanel.Size = new System.Drawing.Size(792, 45);
			this.licensePanel.TabIndex = 4;
			// 
			// DetectFaces
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nViewZoomSlider1);
			this.Controls.Add(this.facesView);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "DetectFaces";
			this.Size = new System.Drawing.Size(792, 372);
			this.Load += new System.EventHandler(this.DetectFacesLoad);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripButton tsbDetectFacialFeatures;
		private System.Windows.Forms.ToolStripComboBox cbRollAngle;
		private System.Windows.Forms.ToolStripLabel tlsLabel;
		private System.Windows.Forms.ToolStripButton tsbOpenImage;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.OpenFileDialog openFaceImageDlg;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox cbYawAngle;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView facesView;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
	}
}
