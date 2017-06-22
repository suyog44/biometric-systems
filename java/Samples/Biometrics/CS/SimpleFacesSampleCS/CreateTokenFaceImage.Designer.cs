namespace Neurotec.Samples
{
	partial class CreateTokenFaceImage
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpenImage = new System.Windows.Forms.ToolStripButton();
			this.openImageFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.faceViewToken = new Neurotec.Biometrics.Gui.NFaceView();
			this.lbQuality = new System.Windows.Forms.Label();
			this.lbSharpness = new System.Windows.Forms.Label();
			this.lbUniformity = new System.Windows.Forms.Label();
			this.lbDensity = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.faceViewOriginal = new Neurotec.Biometrics.Gui.NFaceView();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.toolStrip1.SuspendLayout();
			this.faceViewToken.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenImage});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(570, 25);
			this.toolStrip1.TabIndex = 18;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.Click += new System.EventHandler(this.TsbOpenOriginalClick);
			// 
			// tsbOpenImage
			// 
			this.tsbOpenImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenImage.Name = "tsbOpenImage";
			this.tsbOpenImage.Size = new System.Drawing.Size(92, 22);
			this.tsbOpenImage.Text = "Open Image";
			this.tsbOpenImage.Click += new System.EventHandler(this.TsbOpenOriginalClick);
			// 
			// openImageFileDlg
			// 
			this.openImageFileDlg.Title = "Open Face Image";
			// 
			// faceViewToken
			// 
			this.faceViewToken.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceViewToken.Controls.Add(this.lbQuality);
			this.faceViewToken.Controls.Add(this.lbSharpness);
			this.faceViewToken.Controls.Add(this.lbUniformity);
			this.faceViewToken.Controls.Add(this.lbDensity);
			this.faceViewToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceViewToken.Face = null;
			this.faceViewToken.FaceIds = null;
			this.faceViewToken.FaceRectangleColor = System.Drawing.Color.Transparent;
			this.faceViewToken.Location = new System.Drawing.Point(285, 23);
			this.faceViewToken.Name = "faceViewToken";
			this.faceViewToken.ShowBaseFeaturePoints = false;
			this.faceViewToken.ShowEmotions = false;
			this.faceViewToken.ShowExpression = false;
			this.faceViewToken.ShowEyes = false;
			this.faceViewToken.ShowFaceConfidence = false;
			this.faceViewToken.ShowGender = false;
			this.faceViewToken.ShowMouth = false;
			this.faceViewToken.ShowNose = false;
			this.faceViewToken.ShowProperties = false;
			this.faceViewToken.Size = new System.Drawing.Size(276, 274);
			this.faceViewToken.TabIndex = 2;
			// 
			// lbQuality
			// 
			this.lbQuality.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lbQuality.AutoSize = true;
			this.lbQuality.Location = new System.Drawing.Point(71, 211);
			this.lbQuality.Name = "lbQuality";
			this.lbQuality.Size = new System.Drawing.Size(42, 13);
			this.lbQuality.TabIndex = 3;
			this.lbQuality.Text = "Quality:";
			this.lbQuality.Visible = false;
			// 
			// lbSharpness
			// 
			this.lbSharpness.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lbSharpness.AutoSize = true;
			this.lbSharpness.Location = new System.Drawing.Point(71, 224);
			this.lbSharpness.Name = "lbSharpness";
			this.lbSharpness.Size = new System.Drawing.Size(89, 13);
			this.lbSharpness.TabIndex = 2;
			this.lbSharpness.Text = "Sharpness score:";
			this.lbSharpness.Visible = false;
			// 
			// lbUniformity
			// 
			this.lbUniformity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lbUniformity.AutoSize = true;
			this.lbUniformity.Location = new System.Drawing.Point(71, 237);
			this.lbUniformity.Name = "lbUniformity";
			this.lbUniformity.Size = new System.Drawing.Size(144, 13);
			this.lbUniformity.TabIndex = 1;
			this.lbUniformity.Text = "Background uniformity score:";
			this.lbUniformity.Visible = false;
			// 
			// lbDensity
			// 
			this.lbDensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lbDensity.AutoSize = true;
			this.lbDensity.Location = new System.Drawing.Point(71, 250);
			this.lbDensity.Name = "lbDensity";
			this.lbDensity.Size = new System.Drawing.Size(122, 13);
			this.lbDensity.TabIndex = 0;
			this.lbDensity.Text = "Grayscale density score:";
			this.lbDensity.Visible = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(276, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Original face image";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// faceViewOriginal
			// 
			this.faceViewOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceViewOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceViewOriginal.Face = null;
			this.faceViewOriginal.FaceIds = null;
			this.faceViewOriginal.FaceRectangleColor = System.Drawing.Color.Transparent;
			this.faceViewOriginal.Location = new System.Drawing.Point(3, 23);
			this.faceViewOriginal.Name = "faceViewOriginal";
			this.faceViewOriginal.ShowBaseFeaturePoints = false;
			this.faceViewOriginal.ShowEmotions = false;
			this.faceViewOriginal.ShowExpression = false;
			this.faceViewOriginal.ShowEyes = false;
			this.faceViewOriginal.ShowFaceConfidence = false;
			this.faceViewOriginal.ShowGender = false;
			this.faceViewOriginal.ShowMouth = false;
			this.faceViewOriginal.ShowNose = false;
			this.faceViewOriginal.ShowProperties = false;
			this.faceViewOriginal.Size = new System.Drawing.Size(276, 274);
			this.faceViewOriginal.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(285, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(276, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Token face image";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveImage.Enabled = false;
			this.btnSaveImage.Image = global::Neurotec.Samples.Properties.Resources.saveHS;
			this.btnSaveImage.Location = new System.Drawing.Point(440, 414);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(127, 23);
			this.btnSaveImage.TabIndex = 20;
			this.btnSaveImage.Text = "&Save token image";
			this.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider1, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.faceViewToken, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.faceViewOriginal, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 79);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 329);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider1.Location = new System.Drawing.Point(285, 303);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(276, 23);
			this.nViewZoomSlider1.TabIndex = 8;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.faceViewToken;
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider2.Location = new System.Drawing.Point(3, 303);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(276, 23);
			this.nViewZoomSlider2.TabIndex = 8;
			this.nViewZoomSlider2.Text = "s";
			this.nViewZoomSlider2.View = this.faceViewOriginal;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 28);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssess" +
				"ment";
			this.licensePanel.Size = new System.Drawing.Size(570, 45);
			this.licensePanel.TabIndex = 19;
			// 
			// CreateTokenFaceImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btnSaveImage);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.licensePanel);
			this.Name = "CreateTokenFaceImage";
			this.Size = new System.Drawing.Size(570, 440);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.faceViewToken.ResumeLayout(false);
			this.faceViewToken.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpenImage;
		private System.Windows.Forms.OpenFileDialog openImageFileDlg;
		private Neurotec.Biometrics.Gui.NFaceView faceViewToken;
		private LicensePanel licensePanel;
		private System.Windows.Forms.Label label1;
		private Neurotec.Biometrics.Gui.NFaceView faceViewOriginal;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Label lbQuality;
		private System.Windows.Forms.Label lbSharpness;
		private System.Windows.Forms.Label lbUniformity;
		private System.Windows.Forms.Label lbDensity;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;

	}
}
