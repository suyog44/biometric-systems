namespace Neurotec.Samples
{
	partial class EnrollFromImage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollFromImage));
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpenImage = new System.Windows.Forms.ToolStripButton();
			this.tlsLabel = new System.Windows.Forms.ToolStripLabel();
			this.cbRollAngle = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.cbYawAngle = new System.Windows.Forms.ToolStripComboBox();
			this.tsbExtract = new System.Windows.Forms.ToolStripButton();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.facesView = new Neurotec.Biometrics.Gui.NFaceView();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.lblStatus = new System.Windows.Forms.Label();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveTemplate.Location = new System.Drawing.Point(0, 242);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(105, 23);
			this.btnSaveTemplate.TabIndex = 5;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenImage,
            this.tlsLabel,
            this.cbRollAngle,
            this.toolStripLabel1,
            this.cbYawAngle,
            this.tsbExtract});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(743, 23);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbOpenImage
			// 
			this.tsbOpenImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenImage.Name = "tsbOpenImage";
			this.tsbOpenImage.Size = new System.Drawing.Size(101, 20);
			this.tsbOpenImage.Text = "&Open image...";
			this.tsbOpenImage.Click += new System.EventHandler(this.TsbOpenImageClick);
			// 
			// tlsLabel
			// 
			this.tlsLabel.Name = "tlsLabel";
			this.tlsLabel.Size = new System.Drawing.Size(136, 15);
			this.tlsLabel.Text = "Max roll angle deviation:";
			// 
			// cbRollAngle
			// 
			this.cbRollAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRollAngle.Name = "cbRollAngle";
			this.cbRollAngle.Size = new System.Drawing.Size(121, 23);
			this.cbRollAngle.SelectedIndexChanged += new System.EventHandler(this.CbRollAngleSelectedIndexChanged);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(140, 15);
			this.toolStripLabel1.Text = "Max yaw angle deviation:";
			// 
			// cbYawAngle
			// 
			this.cbYawAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbYawAngle.Name = "cbYawAngle";
			this.cbYawAngle.Size = new System.Drawing.Size(121, 23);
			this.cbYawAngle.SelectedIndexChanged += new System.EventHandler(this.CbYawAngleSelectedIndexChanged);
			// 
			// tsbExtract
			// 
			this.tsbExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbExtract.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbExtract.Image = ((System.Drawing.Image)(resources.GetObject("tsbExtract.Image")));
			this.tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbExtract.Name = "tsbExtract";
			this.tsbExtract.Size = new System.Drawing.Size(105, 19);
			this.tsbExtract.Text = "&Extract template";
			this.tsbExtract.Click += new System.EventHandler(this.TsbExtractClick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			// 
			// facesView
			// 
			this.facesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.facesView.BaseFeaturePointSize = 4;
			this.facesView.Face = null;
			this.facesView.FaceIds = null;
			this.facesView.Location = new System.Drawing.Point(0, 77);
			this.facesView.Name = "facesView";
			this.facesView.Size = new System.Drawing.Size(740, 159);
			this.facesView.TabIndex = 3;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(467, 242);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(273, 23);
			this.nViewZoomSlider1.TabIndex = 10;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.facesView;
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(111, 247);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 11;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 26);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "Biometrics.FaceSegmentsDetection";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction";
			this.licensePanel.Size = new System.Drawing.Size(737, 45);
			this.licensePanel.TabIndex = 9;
			// 
			// EnrollFromImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.nViewZoomSlider1);
			this.Controls.Add(this.facesView);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.btnSaveTemplate);
			this.Name = "EnrollFromImage";
			this.Size = new System.Drawing.Size(743, 268);
			this.Load += new System.EventHandler(this.EnrollFromImageLoad);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpenImage;
		private System.Windows.Forms.ToolStripLabel tlsLabel;
		private System.Windows.Forms.ToolStripComboBox cbRollAngle;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox cbYawAngle;
		private System.Windows.Forms.ToolStripButton tsbExtract;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView facesView;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private System.Windows.Forms.Label lblStatus;
	}
}
