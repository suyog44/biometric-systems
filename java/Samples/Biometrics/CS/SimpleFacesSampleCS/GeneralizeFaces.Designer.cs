namespace Neurotec.Samples
{
	partial class GeneralizeFaces
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralizeFaces));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblImageCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenImages = new System.Windows.Forms.Button();
			this.faceView = new Neurotec.Biometrics.Gui.NFaceView();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveTemplateDialog = new System.Windows.Forms.SaveFileDialog();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.lblImageCount);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnOpenImages);
			this.groupBox1.Location = new System.Drawing.Point(3, 54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(514, 54);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Load face images";
			// 
			// lblImageCount
			// 
			this.lblImageCount.AutoSize = true;
			this.lblImageCount.Location = new System.Drawing.Point(142, 24);
			this.lblImageCount.Name = "lblImageCount";
			this.lblImageCount.Size = new System.Drawing.Size(13, 13);
			this.lblImageCount.TabIndex = 10;
			this.lblImageCount.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Images loaded:";
			// 
			// btnOpenImages
			// 
			this.btnOpenImages.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImages.Image")));
			this.btnOpenImages.Location = new System.Drawing.Point(6, 19);
			this.btnOpenImages.Name = "btnOpenImages";
			this.btnOpenImages.Size = new System.Drawing.Size(30, 23);
			this.btnOpenImages.TabIndex = 8;
			this.btnOpenImages.UseVisualStyleBackColor = true;
			this.btnOpenImages.Click += new System.EventHandler(this.BtnOpenImagesClick);
			// 
			// faceView
			// 
			this.faceView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.faceView.Face = null;
			this.faceView.FaceIds = null;
			this.faceView.Location = new System.Drawing.Point(3, 114);
			this.faceView.Name = "faceView";
			this.faceView.Size = new System.Drawing.Size(514, 116);
			this.faceView.TabIndex = 19;
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.Location = new System.Drawing.Point(114, 233);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(181, 23);
			this.lblStatus.TabIndex = 26;
			this.lblStatus.Text = "Status: None";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblStatus.Visible = false;
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveTemplate.Location = new System.Drawing.Point(3, 233);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(105, 23);
			this.btnSaveTemplate.TabIndex = 25;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Multiselect = true;
			this.openFileDialog.Title = "Select multiple images for generalization";
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(244, 233);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(273, 23);
			this.nViewZoomSlider1.TabIndex = 27;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.faceView;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 3);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction";
			this.licensePanel.Size = new System.Drawing.Size(517, 45);
			this.licensePanel.TabIndex = 17;
			// 
			// GeneralizeFaces
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nViewZoomSlider1);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.faceView);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.licensePanel);
			this.Name = "GeneralizeFaces";
			this.Size = new System.Drawing.Size(520, 259);
			this.Load += new System.EventHandler(this.GeneralizeFacesLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LicensePanel licensePanel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblImageCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpenImages;
		private Neurotec.Biometrics.Gui.NFaceView faceView;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveTemplateDialog;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;

	}
}
