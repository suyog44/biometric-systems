namespace Neurotec.Samples
{
	partial class GeneralizeFinger
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralizeFinger));
			this.lblStatus = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblImageCount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenImages = new System.Windows.Forms.Button();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.fingerView = new Neurotec.Biometrics.Gui.NFingerView();
			this.chbShowBinarizedImage = new System.Windows.Forms.CheckBox();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.Location = new System.Drawing.Point(262, 284);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(436, 23);
			this.lblStatus.TabIndex = 30;
			this.lblStatus.Text = "Status: None";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblStatus.Visible = false;
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
			this.groupBox1.Size = new System.Drawing.Size(695, 54);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Load finger images (Min 3, Max 10)";
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
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveTemplate.Image")));
			this.btnSaveTemplate.Location = new System.Drawing.Point(9, 284);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(105, 23);
			this.btnSaveTemplate.TabIndex = 2;
			this.btnSaveTemplate.Text = "Save Template";
			this.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Multiselect = true;
			// 
			// fingerView
			// 
			this.fingerView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fingerView.BackColor = System.Drawing.SystemColors.Control;
			this.fingerView.BoundingRectColor = System.Drawing.Color.Red;
			this.fingerView.Location = new System.Drawing.Point(3, 114);
			this.fingerView.MinutiaColor = System.Drawing.Color.Red;
			this.fingerView.Name = "fingerView";
			this.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange;
			this.fingerView.ResultImageColor = System.Drawing.Color.Green;
			this.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta;
			this.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta;
			this.fingerView.ShownImage = Neurotec.Biometrics.Gui.ShownImage.Result;
			this.fingerView.SingularPointColor = System.Drawing.Color.Red;
			this.fingerView.Size = new System.Drawing.Size(695, 164);
			this.fingerView.TabIndex = 32;
			this.fingerView.TreeColor = System.Drawing.Color.Crimson;
			this.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay;
			this.fingerView.TreeMinutiaNumberFont = new System.Drawing.Font("Arial", 10F);
			this.fingerView.TreeWidth = 2;
			// 
			// chbShowBinarizedImage
			// 
			this.chbShowBinarizedImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chbShowBinarizedImage.AutoSize = true;
			this.chbShowBinarizedImage.Checked = true;
			this.chbShowBinarizedImage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbShowBinarizedImage.Location = new System.Drawing.Point(120, 288);
			this.chbShowBinarizedImage.Name = "chbShowBinarizedImage";
			this.chbShowBinarizedImage.Size = new System.Drawing.Size(129, 17);
			this.chbShowBinarizedImage.TabIndex = 8;
			this.chbShowBinarizedImage.Text = "Show binarized image";
			this.chbShowBinarizedImage.UseVisualStyleBackColor = true;
			this.chbShowBinarizedImage.CheckedChanged += new System.EventHandler(this.ChbShowBinarizedImageCheckedChanged);
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(423, 282);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(275, 23);
			this.nViewZoomSlider1.TabIndex = 33;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.fingerView;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.Location = new System.Drawing.Point(3, 3);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "Images.WSQ";
			this.licensePanel.RequiredComponents = "Biometrics.FingerExtraction";
			this.licensePanel.Size = new System.Drawing.Size(695, 45);
			this.licensePanel.TabIndex = 31;
			// 
			// GeneralizeFinger
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nViewZoomSlider1);
			this.Controls.Add(this.chbShowBinarizedImage);
			this.Controls.Add(this.btnSaveTemplate);
			this.Controls.Add(this.fingerView);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.groupBox1);
			this.Name = "GeneralizeFinger";
			this.Size = new System.Drawing.Size(701, 310);
			this.Load += new System.EventHandler(this.GeneralizeFingerLoad);
			this.VisibleChanged += new System.EventHandler(this.GeneralizeFingerVisibleChanged);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblImageCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpenImages;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFingerView fingerView;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.CheckBox chbShowBinarizedImage;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
	}
}
