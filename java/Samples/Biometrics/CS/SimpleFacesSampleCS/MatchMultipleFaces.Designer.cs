namespace Neurotec.Samples
{
	partial class MatchMultipleFaces
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
			this.faceViewReference = new Neurotec.Biometrics.Gui.NFaceView();
			this.label1 = new System.Windows.Forms.Label();
			this.faceViewMultiFace = new Neurotec.Biometrics.Gui.NFaceView();
			this.label2 = new System.Windows.Forms.Label();
			this.tsbOpenReference = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpenMultifaceImage = new System.Windows.Forms.ToolStripButton();
			this.openImageFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// faceViewReference
			// 
			this.faceViewReference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceViewReference.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceViewReference.Face = null;
			this.faceViewReference.FaceIds = null;
			this.faceViewReference.IcaoArrowsColor = System.Drawing.Color.Red;
			this.faceViewReference.Location = new System.Drawing.Point(3, 23);
			this.faceViewReference.Name = "faceViewReference";
			this.faceViewReference.ShowIcaoArrows = true;
			this.faceViewReference.ShowTokenImageRectangle = true;
			this.faceViewReference.Size = new System.Drawing.Size(279, 259);
			this.faceViewReference.TabIndex = 1;
			this.faceViewReference.TokenImageRectangleColor = System.Drawing.Color.White;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(279, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Reference Image";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// faceViewMultiFace
			// 
			this.faceViewMultiFace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceViewMultiFace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceViewMultiFace.Face = null;
			this.faceViewMultiFace.FaceIds = null;
			this.faceViewMultiFace.IcaoArrowsColor = System.Drawing.Color.Red;
			this.faceViewMultiFace.Location = new System.Drawing.Point(288, 23);
			this.faceViewMultiFace.Name = "faceViewMultiFace";
			this.faceViewMultiFace.ShowFaceConfidence = false;
			this.faceViewMultiFace.ShowIcaoArrows = true;
			this.faceViewMultiFace.ShowTokenImageRectangle = true;
			this.faceViewMultiFace.Size = new System.Drawing.Size(279, 259);
			this.faceViewMultiFace.TabIndex = 2;
			this.faceViewMultiFace.TokenImageRectangleColor = System.Drawing.Color.White;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(288, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(279, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Multiple Face Image";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsbOpenReference
			// 
			this.tsbOpenReference.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenReference.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenReference.Name = "tsbOpenReference";
			this.tsbOpenReference.Size = new System.Drawing.Size(156, 22);
			this.tsbOpenReference.Text = "Open Reference Image...";
			this.tsbOpenReference.Click += new System.EventHandler(this.TsbOpenReferenceClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenReference,
            this.tsbOpenMultifaceImage});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(570, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbOpenMultifaceImage
			// 
			this.tsbOpenMultifaceImage.Image = global::Neurotec.Samples.Properties.Resources.openfolderHS;
			this.tsbOpenMultifaceImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenMultifaceImage.Name = "tsbOpenMultifaceImage";
			this.tsbOpenMultifaceImage.Size = new System.Drawing.Size(154, 22);
			this.tsbOpenMultifaceImage.Text = "Open Multiface Image...";
			this.tsbOpenMultifaceImage.Click += new System.EventHandler(this.TsbOpenMultifaceImageClick);
			// 
			// openImageFileDlg
			// 
			this.openImageFileDlg.Title = "Open Face Image";
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider2.Location = new System.Drawing.Point(3, 288);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(279, 26);
			this.nViewZoomSlider2.TabIndex = 6;
			this.nViewZoomSlider2.Text = "nViewZoomSlider2";
			this.nViewZoomSlider2.View = this.faceViewReference;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider1.Location = new System.Drawing.Point(288, 288);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(279, 26);
			this.nViewZoomSlider1.TabIndex = 7;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.faceViewMultiFace;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider1, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.faceViewMultiFace, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.faceViewReference, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 79);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 317);
			this.tableLayoutPanel1.TabIndex = 17;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 28);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceMatching";
			this.licensePanel.Size = new System.Drawing.Size(570, 45);
			this.licensePanel.TabIndex = 16;
			// 
			// MatchMultipleFaces
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.toolStrip1);
			this.Name = "MatchMultipleFaces";
			this.Size = new System.Drawing.Size(570, 399);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripButton tsbOpenReference;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpenMultifaceImage;
		private System.Windows.Forms.OpenFileDialog openImageFileDlg;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView faceViewReference;
		private Neurotec.Biometrics.Gui.NFaceView faceViewMultiFace;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
