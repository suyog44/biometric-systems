namespace Neurotec.Samples
{
	partial class SegmentIris
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SegmentIris));
			this.btnOpenImage = new System.Windows.Forms.Button();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.gbResults = new System.Windows.Forms.GroupBox();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.irisView2 = new Neurotec.Biometrics.Gui.NIrisView();
			this.tbGrayLevelSpread = new System.Windows.Forms.TextBox();
			this.lblGrayScaleUtilisation = new System.Windows.Forms.Label();
			this.tbInterlace = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.tbSharpness = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbMarginAdequacy = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tbPupilBoundaryCircularity = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tbIrisPupilConcentricity = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tbIrisPupilContrast = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbIrisScleraContrast = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbUsableIrisArea = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbPupilToIrisRatio = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbQuality = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.irisView1 = new Neurotec.Biometrics.Gui.NIrisView();
			this.btnSegmentIris = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.gbResults.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpenImage
			// 
			this.btnOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImage.Image")));
			this.btnOpenImage.Location = new System.Drawing.Point(3, 3);
			this.btnOpenImage.Name = "btnOpenImage";
			this.btnOpenImage.Size = new System.Drawing.Size(92, 23);
			this.btnOpenImage.TabIndex = 9;
			this.btnOpenImage.Text = "Open Image";
			this.btnOpenImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenImage.UseVisualStyleBackColor = true;
			this.btnOpenImage.Click += new System.EventHandler(this.BtnOpenImageClick);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveImage.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveImage.Image")));
			this.btnSaveImage.Location = new System.Drawing.Point(6, 177);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(96, 23);
			this.btnSaveImage.TabIndex = 10;
			this.btnSaveImage.Text = "Save Image";
			this.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.BtnSaveImageClick);
			// 
			// gbResults
			// 
			this.gbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbResults.Controls.Add(this.nViewZoomSlider2);
			this.gbResults.Controls.Add(this.irisView2);
			this.gbResults.Controls.Add(this.tbGrayLevelSpread);
			this.gbResults.Controls.Add(this.btnSaveImage);
			this.gbResults.Controls.Add(this.lblGrayScaleUtilisation);
			this.gbResults.Controls.Add(this.tbInterlace);
			this.gbResults.Controls.Add(this.label12);
			this.gbResults.Controls.Add(this.tbSharpness);
			this.gbResults.Controls.Add(this.label10);
			this.gbResults.Controls.Add(this.tbMarginAdequacy);
			this.gbResults.Controls.Add(this.label9);
			this.gbResults.Controls.Add(this.tbPupilBoundaryCircularity);
			this.gbResults.Controls.Add(this.label8);
			this.gbResults.Controls.Add(this.tbIrisPupilConcentricity);
			this.gbResults.Controls.Add(this.label7);
			this.gbResults.Controls.Add(this.tbIrisPupilContrast);
			this.gbResults.Controls.Add(this.label6);
			this.gbResults.Controls.Add(this.tbIrisScleraContrast);
			this.gbResults.Controls.Add(this.label5);
			this.gbResults.Controls.Add(this.tbUsableIrisArea);
			this.gbResults.Controls.Add(this.label4);
			this.gbResults.Controls.Add(this.tbPupilToIrisRatio);
			this.gbResults.Controls.Add(this.label3);
			this.gbResults.Controls.Add(this.tbQuality);
			this.gbResults.Controls.Add(this.label1);
			this.gbResults.Location = new System.Drawing.Point(3, 32);
			this.gbResults.Name = "gbResults";
			this.gbResults.Size = new System.Drawing.Size(582, 200);
			this.gbResults.TabIndex = 12;
			this.gbResults.TabStop = false;
			this.gbResults.Text = "Results";
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider2.Location = new System.Drawing.Point(327, 177);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider2.TabIndex = 38;
			this.nViewZoomSlider2.Text = "nViewZoomSlider2";
			this.nViewZoomSlider2.View = this.irisView2;
			// 
			// irisView2
			// 
			this.irisView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.irisView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.irisView2.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView2.InnerBoundaryWidth = 2;
			this.irisView2.Iris = null;
			this.irisView2.Location = new System.Drawing.Point(6, 103);
			this.irisView2.Name = "irisView2";
			this.irisView2.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView2.OuterBoundaryWidth = 2;
			this.irisView2.Size = new System.Drawing.Size(570, 70);
			this.irisView2.TabIndex = 11;
			// 
			// tbGrayLevelSpread
			// 
			this.tbGrayLevelSpread.Location = new System.Drawing.Point(173, 77);
			this.tbGrayLevelSpread.Name = "tbGrayLevelSpread";
			this.tbGrayLevelSpread.ReadOnly = true;
			this.tbGrayLevelSpread.Size = new System.Drawing.Size(48, 20);
			this.tbGrayLevelSpread.TabIndex = 37;
			// 
			// lblGrayScaleUtilisation
			// 
			this.lblGrayScaleUtilisation.AutoSize = true;
			this.lblGrayScaleUtilisation.Location = new System.Drawing.Point(66, 80);
			this.lblGrayScaleUtilisation.Name = "lblGrayScaleUtilisation";
			this.lblGrayScaleUtilisation.Size = new System.Drawing.Size(110, 13);
			this.lblGrayScaleUtilisation.TabIndex = 36;
			this.lblGrayScaleUtilisation.Text = "Gray Scale Utilisation:";
			// 
			// tbInterlace
			// 
			this.tbInterlace.Location = new System.Drawing.Point(528, 33);
			this.tbInterlace.Name = "tbInterlace";
			this.tbInterlace.ReadOnly = true;
			this.tbInterlace.Size = new System.Drawing.Size(48, 20);
			this.tbInterlace.TabIndex = 35;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(475, 36);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(57, 13);
			this.label12.TabIndex = 34;
			this.label12.Text = "Interlace:";
			// 
			// tbSharpness
			// 
			this.tbSharpness.Location = new System.Drawing.Point(528, 13);
			this.tbSharpness.Name = "tbSharpness";
			this.tbSharpness.ReadOnly = true;
			this.tbSharpness.Size = new System.Drawing.Size(48, 20);
			this.tbSharpness.TabIndex = 31;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(472, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "Sharpness:";
			// 
			// tbMarginAdequacy
			// 
			this.tbMarginAdequacy.Location = new System.Drawing.Point(528, 54);
			this.tbMarginAdequacy.Name = "tbMarginAdequacy";
			this.tbMarginAdequacy.ReadOnly = true;
			this.tbMarginAdequacy.Size = new System.Drawing.Size(48, 20);
			this.tbMarginAdequacy.TabIndex = 29;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(439, 57);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(93, 13);
			this.label9.TabIndex = 28;
			this.label9.Text = "Margin Adequacy:";
			// 
			// tbPupilBoundaryCircularity
			// 
			this.tbPupilBoundaryCircularity.Location = new System.Drawing.Point(362, 74);
			this.tbPupilBoundaryCircularity.Name = "tbPupilBoundaryCircularity";
			this.tbPupilBoundaryCircularity.ReadOnly = true;
			this.tbPupilBoundaryCircularity.Size = new System.Drawing.Size(48, 20);
			this.tbPupilBoundaryCircularity.TabIndex = 27;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(233, 77);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(129, 13);
			this.label8.TabIndex = 26;
			this.label8.Text = "Pupil Boundary Circularity:";
			// 
			// tbIrisPupilConcentricity
			// 
			this.tbIrisPupilConcentricity.Location = new System.Drawing.Point(362, 54);
			this.tbIrisPupilConcentricity.Name = "tbIrisPupilConcentricity";
			this.tbIrisPupilConcentricity.ReadOnly = true;
			this.tbIrisPupilConcentricity.Size = new System.Drawing.Size(48, 20);
			this.tbIrisPupilConcentricity.TabIndex = 25;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(249, 57);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(113, 13);
			this.label7.TabIndex = 24;
			this.label7.Text = "Iris Pupil Concentricity:";
			// 
			// tbIrisPupilContrast
			// 
			this.tbIrisPupilContrast.Location = new System.Drawing.Point(362, 34);
			this.tbIrisPupilContrast.Name = "tbIrisPupilContrast";
			this.tbIrisPupilContrast.ReadOnly = true;
			this.tbIrisPupilContrast.Size = new System.Drawing.Size(48, 20);
			this.tbIrisPupilContrast.TabIndex = 23;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(271, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Iris Pupil Contrast:";
			// 
			// tbIrisScleraContrast
			// 
			this.tbIrisScleraContrast.Location = new System.Drawing.Point(362, 13);
			this.tbIrisScleraContrast.Name = "tbIrisScleraContrast";
			this.tbIrisScleraContrast.ReadOnly = true;
			this.tbIrisScleraContrast.Size = new System.Drawing.Size(48, 20);
			this.tbIrisScleraContrast.TabIndex = 21;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(264, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 13);
			this.label5.TabIndex = 20;
			this.label5.Text = "Iris Sclera Contrast:";
			// 
			// tbUsableIrisArea
			// 
			this.tbUsableIrisArea.Location = new System.Drawing.Point(173, 57);
			this.tbUsableIrisArea.Name = "tbUsableIrisArea";
			this.tbUsableIrisArea.ReadOnly = true;
			this.tbUsableIrisArea.Size = new System.Drawing.Size(48, 20);
			this.tbUsableIrisArea.TabIndex = 19;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(93, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "Usable Iris Area:";
			// 
			// tbPupilToIrisRatio
			// 
			this.tbPupilToIrisRatio.Location = new System.Drawing.Point(173, 34);
			this.tbPupilToIrisRatio.Name = "tbPupilToIrisRatio";
			this.tbPupilToIrisRatio.ReadOnly = true;
			this.tbPupilToIrisRatio.Size = new System.Drawing.Size(48, 20);
			this.tbPupilToIrisRatio.TabIndex = 17;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(83, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Pupil To Iris Ratio:";
			// 
			// tbQuality
			// 
			this.tbQuality.Location = new System.Drawing.Point(173, 13);
			this.tbQuality.Name = "tbQuality";
			this.tbQuality.ReadOnly = true;
			this.tbQuality.Size = new System.Drawing.Size(48, 20);
			this.tbQuality.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(134, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Quality:";
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(336, 3);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider1.TabIndex = 38;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.irisView1;
			// 
			// irisView1
			// 
			this.irisView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.irisView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.irisView1.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView1.InnerBoundaryWidth = 2;
			this.irisView1.Iris = null;
			this.irisView1.Location = new System.Drawing.Point(3, 32);
			this.irisView1.Name = "irisView1";
			this.irisView1.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView1.OuterBoundaryWidth = 2;
			this.irisView1.Size = new System.Drawing.Size(582, 75);
			this.irisView1.TabIndex = 0;
			// 
			// btnSegmentIris
			// 
			this.btnSegmentIris.Location = new System.Drawing.Point(3, 3);
			this.btnSegmentIris.Name = "btnSegmentIris";
			this.btnSegmentIris.Size = new System.Drawing.Size(92, 23);
			this.btnSegmentIris.TabIndex = 13;
			this.btnSegmentIris.Text = "Segment Iris";
			this.btnSegmentIris.UseVisualStyleBackColor = true;
			this.btnSegmentIris.Click += new System.EventHandler(this.BtnSegmentIrisClick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(3, 51);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.nViewZoomSlider1);
			this.splitContainer1.Panel1.Controls.Add(this.irisView1);
			this.splitContainer1.Panel1.Controls.Add(this.btnOpenImage);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnSegmentIris);
			this.splitContainer1.Panel2.Controls.Add(this.gbResults);
			this.splitContainer1.Size = new System.Drawing.Size(588, 349);
			this.splitContainer1.SplitterDistance = 110;
			this.splitContainer1.TabIndex = 14;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation";
			this.licensePanel.Size = new System.Drawing.Size(594, 45);
			this.licensePanel.TabIndex = 16;
			// 
			// SegmentIris
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.splitContainer1);
			this.Name = "SegmentIris";
			this.Size = new System.Drawing.Size(594, 403);
			this.Load += new System.EventHandler(this.SegmentIrisLoad);
			this.gbResults.ResumeLayout(false);
			this.gbResults.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOpenImage;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.GroupBox gbResults;
		private System.Windows.Forms.Button btnSegmentIris;
		private System.Windows.Forms.TextBox tbQuality;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox tbMarginAdequacy;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbPupilBoundaryCircularity;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbIrisPupilConcentricity;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbIrisPupilContrast;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbIrisScleraContrast;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbUsableIrisArea;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbPupilToIrisRatio;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbInterlace;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbSharpness;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.TextBox tbGrayLevelSpread;
		private System.Windows.Forms.Label lblGrayScaleUtilisation;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NIrisView irisView1;
		private Neurotec.Biometrics.Gui.NIrisView irisView2;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
	}
}
