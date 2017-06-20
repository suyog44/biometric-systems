namespace Neurotec.Samples
{
	partial class VerifyIris
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyIris));
			this.btnClear = new System.Windows.Forms.Button();
			this.btnOpenImage1 = new System.Windows.Forms.Button();
			this.btnOpenImage2 = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.matchingGroupBox = new System.Windows.Forms.GroupBox();
			this.btnDefault = new System.Windows.Forms.Button();
			this.matchingFarLabel = new System.Windows.Forms.Label();
			this.cbMatchingFAR = new System.Windows.Forms.ComboBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.irisView1 = new Neurotec.Biometrics.Gui.NIrisView();
			this.irisView2 = new Neurotec.Biometrics.Gui.NIrisView();
			this.lblTemplateRight = new System.Windows.Forms.Label();
			this.lblTemplateLeft = new System.Windows.Forms.Label();
			this.templateNameLabel2 = new System.Windows.Forms.Label();
			this.templateNameLabel1 = new System.Windows.Forms.Label();
			this.btnVerify = new System.Windows.Forms.Button();
			this.lblMsg = new System.Windows.Forms.Label();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.matchingGroupBox.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClear
			// 
			this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnClear.Location = new System.Drawing.Point(237, 0);
			this.btnClear.Margin = new System.Windows.Forms.Padding(0);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(108, 23);
			this.btnClear.TabIndex = 25;
			this.btnClear.Text = "Clear Images";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
			// 
			// btnOpenImage1
			// 
			this.btnOpenImage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenImage1.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImage1.Image")));
			this.btnOpenImage1.Location = new System.Drawing.Point(184, 35);
			this.btnOpenImage1.Name = "btnOpenImage1";
			this.btnOpenImage1.Size = new System.Drawing.Size(30, 23);
			this.btnOpenImage1.TabIndex = 21;
			this.btnOpenImage1.UseVisualStyleBackColor = true;
			this.btnOpenImage1.Click += new System.EventHandler(this.BtnOpenImage1Click);
			// 
			// btnOpenImage2
			// 
			this.btnOpenImage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenImage2.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImage2.Image")));
			this.btnOpenImage2.Location = new System.Drawing.Point(368, 35);
			this.btnOpenImage2.Name = "btnOpenImage2";
			this.btnOpenImage2.Size = new System.Drawing.Size(30, 23);
			this.btnOpenImage2.TabIndex = 22;
			this.btnOpenImage2.UseVisualStyleBackColor = true;
			this.btnOpenImage2.Click += new System.EventHandler(this.BtnOpenImage2Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.btnClear, 0, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 277);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(582, 24);
			this.tableLayoutPanel3.TabIndex = 44;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.btnOpenImage1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnOpenImage2, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.matchingGroupBox, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 51);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(582, 61);
			this.tableLayoutPanel2.TabIndex = 43;
			// 
			// matchingGroupBox
			// 
			this.matchingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.matchingGroupBox.Controls.Add(this.btnDefault);
			this.matchingGroupBox.Controls.Add(this.matchingFarLabel);
			this.matchingGroupBox.Controls.Add(this.cbMatchingFAR);
			this.matchingGroupBox.Location = new System.Drawing.Point(220, 3);
			this.matchingGroupBox.MaximumSize = new System.Drawing.Size(600, 200);
			this.matchingGroupBox.Name = "matchingGroupBox";
			this.matchingGroupBox.Size = new System.Drawing.Size(142, 54);
			this.matchingGroupBox.TabIndex = 32;
			this.matchingGroupBox.TabStop = false;
			// 
			// btnDefault
			// 
			this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDefault.Location = new System.Drawing.Point(73, 26);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(63, 23);
			this.btnDefault.TabIndex = 20;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.BtnDefaultClick);
			// 
			// matchingFarLabel
			// 
			this.matchingFarLabel.AutoSize = true;
			this.matchingFarLabel.Location = new System.Drawing.Point(11, 10);
			this.matchingFarLabel.Name = "matchingFarLabel";
			this.matchingFarLabel.Size = new System.Drawing.Size(78, 13);
			this.matchingFarLabel.TabIndex = 18;
			this.matchingFarLabel.Text = "Matching &FAR:";
			// 
			// cbMatchingFAR
			// 
			this.cbMatchingFAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbMatchingFAR.FormattingEnabled = true;
			this.cbMatchingFAR.Location = new System.Drawing.Point(9, 28);
			this.cbMatchingFAR.Name = "cbMatchingFAR";
			this.cbMatchingFAR.Size = new System.Drawing.Size(58, 21);
			this.cbMatchingFAR.TabIndex = 19;
			this.cbMatchingFAR.Leave += new System.EventHandler(this.CbMatchingFARLeave);
			this.cbMatchingFAR.Enter += new System.EventHandler(this.CbMatchingFAREnter);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.irisView1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.irisView2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider2, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 114);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 157);
			this.tableLayoutPanel1.TabIndex = 42;
			// 
			// irisView1
			// 
			this.irisView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.irisView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.irisView1.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView1.InnerBoundaryWidth = 2;
			this.irisView1.Iris = null;
			this.irisView1.Location = new System.Drawing.Point(3, 3);
			this.irisView1.Name = "irisView1";
			this.irisView1.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView1.OuterBoundaryWidth = 2;
			this.irisView1.Size = new System.Drawing.Size(285, 122);
			this.irisView1.TabIndex = 0;
			// 
			// irisView2
			// 
			this.irisView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.irisView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.irisView2.InnerBoundaryColor = System.Drawing.Color.Red;
			this.irisView2.InnerBoundaryWidth = 2;
			this.irisView2.Iris = null;
			this.irisView2.Location = new System.Drawing.Point(294, 3);
			this.irisView2.Name = "irisView2";
			this.irisView2.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView2.OuterBoundaryWidth = 2;
			this.irisView2.Size = new System.Drawing.Size(285, 122);
			this.irisView2.TabIndex = 1;
			// 
			// lblTemplateRight
			// 
			this.lblTemplateRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblTemplateRight.AutoSize = true;
			this.lblTemplateRight.Location = new System.Drawing.Point(120, 328);
			this.lblTemplateRight.Name = "lblTemplateRight";
			this.lblTemplateRight.Size = new System.Drawing.Size(64, 13);
			this.lblTemplateRight.TabIndex = 41;
			this.lblTemplateRight.Text = "template left";
			// 
			// lblTemplateLeft
			// 
			this.lblTemplateLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblTemplateLeft.AutoSize = true;
			this.lblTemplateLeft.Location = new System.Drawing.Point(120, 304);
			this.lblTemplateLeft.Name = "lblTemplateLeft";
			this.lblTemplateLeft.Size = new System.Drawing.Size(64, 13);
			this.lblTemplateLeft.TabIndex = 40;
			this.lblTemplateLeft.Text = "template left";
			// 
			// templateNameLabel2
			// 
			this.templateNameLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateNameLabel2.AutoSize = true;
			this.templateNameLabel2.Location = new System.Drawing.Point(3, 328);
			this.templateNameLabel2.Name = "templateNameLabel2";
			this.templateNameLabel2.Size = new System.Drawing.Size(117, 13);
			this.templateNameLabel2.TabIndex = 39;
			this.templateNameLabel2.Text = "Image or template right:";
			// 
			// templateNameLabel1
			// 
			this.templateNameLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateNameLabel1.AutoSize = true;
			this.templateNameLabel1.Location = new System.Drawing.Point(3, 304);
			this.templateNameLabel1.Name = "templateNameLabel1";
			this.templateNameLabel1.Size = new System.Drawing.Size(111, 13);
			this.templateNameLabel1.TabIndex = 38;
			this.templateNameLabel1.Text = "Image or template left:";
			// 
			// btnVerify
			// 
			this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnVerify.Enabled = false;
			this.btnVerify.Location = new System.Drawing.Point(6, 355);
			this.btnVerify.Name = "btnVerify";
			this.btnVerify.Size = new System.Drawing.Size(121, 23);
			this.btnVerify.TabIndex = 37;
			this.btnVerify.Text = "Verify";
			this.btnVerify.UseVisualStyleBackColor = true;
			this.btnVerify.Click += new System.EventHandler(this.BtnVerifyClick);
			// 
			// lblMsg
			// 
			this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new System.Drawing.Point(3, 388);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(33, 13);
			this.lblMsg.TabIndex = 36;
			this.lblMsg.Text = "score";
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisMatching";
			this.licensePanel.Size = new System.Drawing.Size(585, 45);
			this.licensePanel.TabIndex = 45;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(3, 131);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(285, 23);
			this.nViewZoomSlider1.TabIndex = 2;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.irisView1;
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider2.Location = new System.Drawing.Point(294, 131);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(285, 23);
			this.nViewZoomSlider2.TabIndex = 3;
			this.nViewZoomSlider2.Text = "nViewZoomSlider2";
			this.nViewZoomSlider2.View = this.irisView2;
			// 
			// VerifyIris
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.lblTemplateRight);
			this.Controls.Add(this.lblTemplateLeft);
			this.Controls.Add(this.templateNameLabel2);
			this.Controls.Add(this.templateNameLabel1);
			this.Controls.Add(this.btnVerify);
			this.Controls.Add(this.lblMsg);
			this.Name = "VerifyIris";
			this.Size = new System.Drawing.Size(588, 409);
			this.Load += new System.EventHandler(this.VerifyIrisLoad);
			this.VisibleChanged += new System.EventHandler(this.VerifyIrisVisibleChanged);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.matchingGroupBox.ResumeLayout(false);
			this.matchingGroupBox.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnOpenImage1;
		private System.Windows.Forms.Button btnOpenImage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Label matchingFarLabel;
		private System.Windows.Forms.ComboBox cbMatchingFAR;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblTemplateRight;
		private System.Windows.Forms.Label lblTemplateLeft;
		private System.Windows.Forms.Label templateNameLabel2;
		private System.Windows.Forms.Label templateNameLabel1;
		private System.Windows.Forms.Button btnVerify;
		private System.Windows.Forms.Label lblMsg;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NIrisView irisView1;
		private Neurotec.Biometrics.Gui.NIrisView irisView2;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
	}
}
