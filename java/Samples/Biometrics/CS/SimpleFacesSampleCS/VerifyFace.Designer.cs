namespace Neurotec.Samples
{
	partial class VerifyFace
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyFace));
			this.clearImagesButton = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.openImageButton1 = new System.Windows.Forms.Button();
			this.openImageButton2 = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.matchingGroupBox = new System.Windows.Forms.GroupBox();
			this.defaultButton = new System.Windows.Forms.Button();
			this.matchingFarLabel = new System.Windows.Forms.Label();
			this.matchingFarComboBox = new System.Windows.Forms.ComboBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.faceView1 = new Neurotec.Biometrics.Gui.NFaceView();
			this.faceView2 = new Neurotec.Biometrics.Gui.NFaceView();
			this.nViewZoomSlider2 = new Neurotec.Gui.NViewZoomSlider();
			this.templateRightLabel = new System.Windows.Forms.Label();
			this.templateLeftLabel = new System.Windows.Forms.Label();
			this.templateNameLabel2 = new System.Windows.Forms.Label();
			this.templateNameLabel1 = new System.Windows.Forms.Label();
			this.verifyButton = new System.Windows.Forms.Button();
			this.msgLabel = new System.Windows.Forms.Label();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.matchingGroupBox.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// clearImagesButton
			// 
			this.clearImagesButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.clearImagesButton.Location = new System.Drawing.Point(232, 0);
			this.clearImagesButton.Margin = new System.Windows.Forms.Padding(0);
			this.clearImagesButton.Name = "clearImagesButton";
			this.clearImagesButton.Size = new System.Drawing.Size(108, 23);
			this.clearImagesButton.TabIndex = 25;
			this.clearImagesButton.Text = "Clear Images";
			this.clearImagesButton.UseVisualStyleBackColor = true;
			this.clearImagesButton.Click += new System.EventHandler(this.ClearImagesButtonClick);
			// 
			// openImageButton1
			// 
			this.openImageButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.openImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("openImageButton1.Image")));
			this.openImageButton1.Location = new System.Drawing.Point(177, 35);
			this.openImageButton1.Name = "openImageButton1";
			this.openImageButton1.Size = new System.Drawing.Size(30, 23);
			this.openImageButton1.TabIndex = 21;
			this.toolTip.SetToolTip(this.openImageButton1, "Open first fingerprint image or template (*.dat) file");
			this.openImageButton1.UseVisualStyleBackColor = true;
			this.openImageButton1.Click += new System.EventHandler(this.OpenImageButton1Click);
			// 
			// openImageButton2
			// 
			this.openImageButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.openImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("openImageButton2.Image")));
			this.openImageButton2.Location = new System.Drawing.Point(364, 35);
			this.openImageButton2.Name = "openImageButton2";
			this.openImageButton2.Size = new System.Drawing.Size(30, 23);
			this.openImageButton2.TabIndex = 22;
			this.toolTip.SetToolTip(this.openImageButton2, "Open second fingerprint image or template (*.dat) file");
			this.openImageButton2.UseVisualStyleBackColor = true;
			this.openImageButton2.Click += new System.EventHandler(this.OpenImageButton2Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.clearImagesButton, 0, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 240);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(572, 24);
			this.tableLayoutPanel3.TabIndex = 44;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.openImageButton1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.openImageButton2, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.matchingGroupBox, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 51);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(572, 61);
			this.tableLayoutPanel2.TabIndex = 43;
			// 
			// matchingGroupBox
			// 
			this.matchingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.matchingGroupBox.Controls.Add(this.defaultButton);
			this.matchingGroupBox.Controls.Add(this.matchingFarLabel);
			this.matchingGroupBox.Controls.Add(this.matchingFarComboBox);
			this.matchingGroupBox.Location = new System.Drawing.Point(214, 3);
			this.matchingGroupBox.MaximumSize = new System.Drawing.Size(600, 200);
			this.matchingGroupBox.Name = "matchingGroupBox";
			this.matchingGroupBox.Size = new System.Drawing.Size(142, 54);
			this.matchingGroupBox.TabIndex = 32;
			this.matchingGroupBox.TabStop = false;
			// 
			// defaultButton
			// 
			this.defaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.defaultButton.Location = new System.Drawing.Point(73, 25);
			this.defaultButton.Name = "defaultButton";
			this.defaultButton.Size = new System.Drawing.Size(63, 23);
			this.defaultButton.TabIndex = 20;
			this.defaultButton.Text = "Default";
			this.defaultButton.UseVisualStyleBackColor = true;
			this.defaultButton.Click += new System.EventHandler(this.DefaultButtonClick);
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
			// matchingFarComboBox
			// 
			this.matchingFarComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.matchingFarComboBox.FormattingEnabled = true;
			this.matchingFarComboBox.Location = new System.Drawing.Point(9, 28);
			this.matchingFarComboBox.Name = "matchingFarComboBox";
			this.matchingFarComboBox.Size = new System.Drawing.Size(58, 21);
			this.matchingFarComboBox.TabIndex = 19;
			this.matchingFarComboBox.Leave += new System.EventHandler(this.MatchingFarComboBoxLeave);
			this.matchingFarComboBox.Enter += new System.EventHandler(this.MatchingFarComboBoxEnter);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.faceView2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.faceView1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.nViewZoomSlider2, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 114);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 123);
			this.tableLayoutPanel1.TabIndex = 42;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider1.Location = new System.Drawing.Point(3, 97);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(280, 23);
			this.nViewZoomSlider1.TabIndex = 7;
			this.nViewZoomSlider1.Text = "nViewZoomSlider2";
			this.nViewZoomSlider1.View = this.faceView1;
			// 
			// faceView1
			// 
			this.faceView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceView1.Face = null;
			this.faceView1.FaceIds = null;
			this.faceView1.Location = new System.Drawing.Point(3, 3);
			this.faceView1.Name = "faceView1";
			this.faceView1.Size = new System.Drawing.Size(280, 88);
			this.faceView1.TabIndex = 3;
			// 
			// faceView2
			// 
			this.faceView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.faceView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.faceView2.Face = null;
			this.faceView2.FaceIds = null;
			this.faceView2.Location = new System.Drawing.Point(289, 3);
			this.faceView2.Name = "faceView2";
			this.faceView2.Size = new System.Drawing.Size(280, 88);
			this.faceView2.TabIndex = 4;
			// 
			// nViewZoomSlider2
			// 
			this.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nViewZoomSlider2.Location = new System.Drawing.Point(289, 97);
			this.nViewZoomSlider2.Name = "nViewZoomSlider2";
			this.nViewZoomSlider2.Size = new System.Drawing.Size(280, 23);
			this.nViewZoomSlider2.TabIndex = 6;
			this.nViewZoomSlider2.Text = "nViewZoomSlider1";
			this.nViewZoomSlider2.View = this.faceView2;
			// 
			// templateRightLabel
			// 
			this.templateRightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateRightLabel.AutoSize = true;
			this.templateRightLabel.Location = new System.Drawing.Point(120, 291);
			this.templateRightLabel.Name = "templateRightLabel";
			this.templateRightLabel.Size = new System.Drawing.Size(64, 13);
			this.templateRightLabel.TabIndex = 41;
			this.templateRightLabel.Text = "template left";
			// 
			// templateLeftLabel
			// 
			this.templateLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateLeftLabel.AutoSize = true;
			this.templateLeftLabel.Location = new System.Drawing.Point(120, 267);
			this.templateLeftLabel.Name = "templateLeftLabel";
			this.templateLeftLabel.Size = new System.Drawing.Size(64, 13);
			this.templateLeftLabel.TabIndex = 40;
			this.templateLeftLabel.Text = "template left";
			// 
			// templateNameLabel2
			// 
			this.templateNameLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateNameLabel2.AutoSize = true;
			this.templateNameLabel2.Location = new System.Drawing.Point(3, 291);
			this.templateNameLabel2.Name = "templateNameLabel2";
			this.templateNameLabel2.Size = new System.Drawing.Size(117, 13);
			this.templateNameLabel2.TabIndex = 39;
			this.templateNameLabel2.Text = "Image or template right:";
			// 
			// templateNameLabel1
			// 
			this.templateNameLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.templateNameLabel1.AutoSize = true;
			this.templateNameLabel1.Location = new System.Drawing.Point(3, 267);
			this.templateNameLabel1.Name = "templateNameLabel1";
			this.templateNameLabel1.Size = new System.Drawing.Size(111, 13);
			this.templateNameLabel1.TabIndex = 38;
			this.templateNameLabel1.Text = "Image or template left:";
			// 
			// verifyButton
			// 
			this.verifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.verifyButton.Enabled = false;
			this.verifyButton.Location = new System.Drawing.Point(6, 318);
			this.verifyButton.Name = "verifyButton";
			this.verifyButton.Size = new System.Drawing.Size(121, 23);
			this.verifyButton.TabIndex = 37;
			this.verifyButton.Text = "Verify";
			this.verifyButton.UseVisualStyleBackColor = true;
			this.verifyButton.Click += new System.EventHandler(this.VerifyButtonClick);
			// 
			// msgLabel
			// 
			this.msgLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.msgLabel.AutoSize = true;
			this.msgLabel.Location = new System.Drawing.Point(3, 351);
			this.msgLabel.Name = "msgLabel";
			this.msgLabel.Size = new System.Drawing.Size(33, 13);
			this.msgLabel.TabIndex = 36;
			this.msgLabel.Text = "score";
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(3, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceMatching";
			this.licensePanel.Size = new System.Drawing.Size(569, 45);
			this.licensePanel.TabIndex = 45;
			// 
			// VerifyFace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.templateRightLabel);
			this.Controls.Add(this.templateLeftLabel);
			this.Controls.Add(this.templateNameLabel2);
			this.Controls.Add(this.templateNameLabel1);
			this.Controls.Add(this.verifyButton);
			this.Controls.Add(this.msgLabel);
			this.Name = "VerifyFace";
			this.Size = new System.Drawing.Size(572, 367);
			this.Load += new System.EventHandler(this.VerifyFaceLoad);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.matchingGroupBox.ResumeLayout(false);
			this.matchingGroupBox.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button clearImagesButton;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button openImageButton1;
		private System.Windows.Forms.Button openImageButton2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Button defaultButton;
		private System.Windows.Forms.Label matchingFarLabel;
		private System.Windows.Forms.ComboBox matchingFarComboBox;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label templateRightLabel;
		private System.Windows.Forms.Label templateLeftLabel;
		private System.Windows.Forms.Label templateNameLabel2;
		private System.Windows.Forms.Label templateNameLabel1;
		private System.Windows.Forms.Button verifyButton;
		private System.Windows.Forms.Label msgLabel;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView faceView1;
		private Neurotec.Biometrics.Gui.NFaceView faceView2;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider2;
	}
}
