namespace Neurotec.Samples
{
	partial class IdentifyIris
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdentifyIris));
			this.label2 = new System.Windows.Forms.Label();
			this.lblTemplatesCount = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.btnIdentify = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.irisView = new Neurotec.Biometrics.Gui.NIrisView();
			this.lblFileForIdentification = new System.Windows.Forms.Label();
			this.btnOpenImage = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOpenTemplates = new System.Windows.Forms.Button();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.matchingGroupBox = new System.Windows.Forms.GroupBox();
			this.btnDefault = new System.Windows.Forms.Button();
			this.cbMatchingFar = new System.Windows.Forms.ComboBox();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.listView = new System.Windows.Forms.ListView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.matchingGroupBox.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 1;
			// 
			// lblTemplatesCount
			// 
			this.lblTemplatesCount.AutoSize = true;
			this.lblTemplatesCount.Location = new System.Drawing.Point(142, 34);
			this.lblTemplatesCount.Name = "lblTemplatesCount";
			this.lblTemplatesCount.Size = new System.Drawing.Size(13, 13);
			this.lblTemplatesCount.TabIndex = 7;
			this.lblTemplatesCount.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Templates loaded:";
			// 
			// btnIdentify
			// 
			this.btnIdentify.Enabled = false;
			this.btnIdentify.Location = new System.Drawing.Point(9, 19);
			this.btnIdentify.Name = "btnIdentify";
			this.btnIdentify.Size = new System.Drawing.Size(92, 23);
			this.btnIdentify.TabIndex = 0;
			this.btnIdentify.Text = "&Identify";
			this.btnIdentify.UseVisualStyleBackColor = true;
			this.btnIdentify.Click += new System.EventHandler(this.BtnIdentifyClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.nViewZoomSlider1);
			this.groupBox2.Controls.Add(this.irisView);
			this.groupBox2.Controls.Add(this.lblFileForIdentification);
			this.groupBox2.Controls.Add(this.btnOpenImage);
			this.groupBox2.Location = new System.Drawing.Point(3, 114);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(547, 161);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Image / template for identification";
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
			this.irisView.Location = new System.Drawing.Point(6, 19);
			this.irisView.Name = "irisView";
			this.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView.OuterBoundaryWidth = 2;
			this.irisView.Size = new System.Drawing.Size(535, 94);
			this.irisView.TabIndex = 11;
			// 
			// lblFileForIdentification
			// 
			this.lblFileForIdentification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFileForIdentification.Location = new System.Drawing.Point(6, 145);
			this.lblFileForIdentification.Name = "lblFileForIdentification";
			this.lblFileForIdentification.Size = new System.Drawing.Size(434, 13);
			this.lblFileForIdentification.TabIndex = 10;
			this.lblFileForIdentification.Text = "file for identification";
			// 
			// btnOpenImage
			// 
			this.btnOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenImage.Image")));
			this.btnOpenImage.Location = new System.Drawing.Point(6, 119);
			this.btnOpenImage.Name = "btnOpenImage";
			this.btnOpenImage.Size = new System.Drawing.Size(92, 23);
			this.btnOpenImage.TabIndex = 8;
			this.btnOpenImage.Text = "Open";
			this.btnOpenImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenImage.UseVisualStyleBackColor = true;
			this.btnOpenImage.Click += new System.EventHandler(this.BtnOpenImageClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.lblTemplatesCount);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnOpenTemplates);
			this.groupBox1.Location = new System.Drawing.Point(3, 51);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(544, 57);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Templates loading";
			// 
			// btnOpenTemplates
			// 
			this.btnOpenTemplates.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTemplates.Image")));
			this.btnOpenTemplates.Location = new System.Drawing.Point(6, 24);
			this.btnOpenTemplates.Name = "btnOpenTemplates";
			this.btnOpenTemplates.Size = new System.Drawing.Size(30, 23);
			this.btnOpenTemplates.TabIndex = 5;
			this.btnOpenTemplates.UseVisualStyleBackColor = true;
			this.btnOpenTemplates.Click += new System.EventHandler(this.BtnOpenTemplatesClick);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Score";
			this.columnHeader2.Width = 100;
			// 
			// matchingGroupBox
			// 
			this.matchingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.matchingGroupBox.Controls.Add(this.btnDefault);
			this.matchingGroupBox.Controls.Add(this.cbMatchingFar);
			this.matchingGroupBox.Location = new System.Drawing.Point(380, 14);
			this.matchingGroupBox.Name = "matchingGroupBox";
			this.matchingGroupBox.Size = new System.Drawing.Size(158, 45);
			this.matchingGroupBox.TabIndex = 21;
			this.matchingGroupBox.TabStop = false;
			this.matchingGroupBox.Text = "Matching FAR";
			// 
			// btnDefault
			// 
			this.btnDefault.Location = new System.Drawing.Point(89, 17);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(63, 23);
			this.btnDefault.TabIndex = 20;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.BtnDefaultClick);
			// 
			// cbMatchingFar
			// 
			this.cbMatchingFar.FormattingEnabled = true;
			this.cbMatchingFar.Location = new System.Drawing.Point(9, 19);
			this.cbMatchingFar.Name = "cbMatchingFar";
			this.cbMatchingFar.Size = new System.Drawing.Size(73, 21);
			this.cbMatchingFar.TabIndex = 19;
			this.cbMatchingFar.Leave += new System.EventHandler(this.CbMatchingFarLeave);
			this.cbMatchingFar.Enter += new System.EventHandler(this.CbMatchingFarEnter);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ID";
			this.columnHeader1.Width = 300;
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listView.Location = new System.Drawing.Point(9, 65);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(529, 127);
			this.listView.TabIndex = 2;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.matchingGroupBox);
			this.groupBox3.Controls.Add(this.listView);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.btnIdentify);
			this.groupBox3.Location = new System.Drawing.Point(3, 281);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(544, 198);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Identification";
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(292, 119);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider1.TabIndex = 8;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.irisView;
			// 
			// licensePanel
			// 
			this.licensePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisMatching";
			this.licensePanel.Size = new System.Drawing.Size(550, 45);
			this.licensePanel.TabIndex = 15;
			// 
			// IdentifyIris
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Name = "IdentifyIris";
			this.Size = new System.Drawing.Size(550, 482);
			this.Load += new System.EventHandler(this.IdentifyIrisLoad);
			this.VisibleChanged += new System.EventHandler(this.IdentifyIrisVisibleChanged);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.matchingGroupBox.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblTemplatesCount;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnIdentify;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblFileForIdentification;
		private System.Windows.Forms.Button btnOpenImage;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOpenTemplates;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.ComboBox cbMatchingFar;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.GroupBox groupBox3;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NIrisView irisView;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
	}
}
