namespace Neurotec.Samples
{
	partial class IdentifyFace
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdentifyFace));
			this.label2 = new System.Windows.Forms.Label();
			this.templatesCountLabel = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.identifyButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.faceView = new Neurotec.Biometrics.Gui.NFaceView();
			this.fileForIdentificationLabel = new System.Windows.Forms.Label();
			this.openImageButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.openTemplatesButton = new System.Windows.Forms.Button();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.matchingGroupBox = new System.Windows.Forms.GroupBox();
			this.defaultMatchingFARButton = new System.Windows.Forms.Button();
			this.matchingFarComboBox = new System.Windows.Forms.ComboBox();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.listView = new System.Windows.Forms.ListView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
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
			// templatesCountLabel
			// 
			this.templatesCountLabel.AutoSize = true;
			this.templatesCountLabel.Location = new System.Drawing.Point(142, 34);
			this.templatesCountLabel.Name = "templatesCountLabel";
			this.templatesCountLabel.Size = new System.Drawing.Size(13, 13);
			this.templatesCountLabel.TabIndex = 7;
			this.templatesCountLabel.Text = "0";
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
			// identifyButton
			// 
			this.identifyButton.Enabled = false;
			this.identifyButton.Location = new System.Drawing.Point(9, 25);
			this.identifyButton.Name = "identifyButton";
			this.identifyButton.Size = new System.Drawing.Size(92, 23);
			this.identifyButton.TabIndex = 0;
			this.identifyButton.Text = "&Identify";
			this.identifyButton.UseVisualStyleBackColor = true;
			this.identifyButton.Click += new System.EventHandler(this.IdentifyButtonClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.nViewZoomSlider1);
			this.groupBox2.Controls.Add(this.faceView);
			this.groupBox2.Controls.Add(this.fileForIdentificationLabel);
			this.groupBox2.Controls.Add(this.openImageButton);
			this.groupBox2.Location = new System.Drawing.Point(3, 109);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(461, 101);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Image / template for identification";
			// 
			// faceView
			// 
			this.faceView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.faceView.Face = null;
			this.faceView.FaceIds = null;
			this.faceView.Location = new System.Drawing.Point(9, 16);
			this.faceView.Name = "faceView";
			this.faceView.Size = new System.Drawing.Size(446, 50);
			this.faceView.TabIndex = 15;
			// 
			// fileForIdentificationLabel
			// 
			this.fileForIdentificationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fileForIdentificationLabel.Location = new System.Drawing.Point(104, 77);
			this.fileForIdentificationLabel.Name = "fileForIdentificationLabel";
			this.fileForIdentificationLabel.Size = new System.Drawing.Size(348, 13);
			this.fileForIdentificationLabel.TabIndex = 10;
			// 
			// openImageButton
			// 
			this.openImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.openImageButton.Image = ((System.Drawing.Image)(resources.GetObject("openImageButton.Image")));
			this.openImageButton.Location = new System.Drawing.Point(9, 72);
			this.openImageButton.Name = "openImageButton";
			this.openImageButton.Size = new System.Drawing.Size(89, 23);
			this.openImageButton.TabIndex = 8;
			this.openImageButton.Text = "Open";
			this.openImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.openImageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.toolTip.SetToolTip(this.openImageButton, "Open face image or template for identification");
			this.openImageButton.UseVisualStyleBackColor = true;
			this.openImageButton.Click += new System.EventHandler(this.OpenImageButtonClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.templatesCountLabel);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.openTemplatesButton);
			this.groupBox1.Location = new System.Drawing.Point(3, 46);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(461, 57);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Templates loading";
			// 
			// openTemplatesButton
			// 
			this.openTemplatesButton.Image = ((System.Drawing.Image)(resources.GetObject("openTemplatesButton.Image")));
			this.openTemplatesButton.Location = new System.Drawing.Point(6, 24);
			this.openTemplatesButton.Name = "openTemplatesButton";
			this.openTemplatesButton.Size = new System.Drawing.Size(30, 23);
			this.openTemplatesButton.TabIndex = 5;
			this.toolTip.SetToolTip(this.openTemplatesButton, "Open templates files (*.dat) ");
			this.openTemplatesButton.UseVisualStyleBackColor = true;
			this.openTemplatesButton.Click += new System.EventHandler(this.OpenTemplatesButtonClick);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Score";
			this.columnHeader2.Width = 100;
			// 
			// matchingGroupBox
			// 
			this.matchingGroupBox.Controls.Add(this.defaultMatchingFARButton);
			this.matchingGroupBox.Controls.Add(this.matchingFarComboBox);
			this.matchingGroupBox.Location = new System.Drawing.Point(112, 8);
			this.matchingGroupBox.Name = "matchingGroupBox";
			this.matchingGroupBox.Size = new System.Drawing.Size(159, 43);
			this.matchingGroupBox.TabIndex = 21;
			this.matchingGroupBox.TabStop = false;
			this.matchingGroupBox.Text = "Matching FAR";
			// 
			// defaultMatchingFARButton
			// 
			this.defaultMatchingFARButton.Location = new System.Drawing.Point(88, 17);
			this.defaultMatchingFARButton.Name = "defaultMatchingFARButton";
			this.defaultMatchingFARButton.Size = new System.Drawing.Size(63, 23);
			this.defaultMatchingFARButton.TabIndex = 20;
			this.defaultMatchingFARButton.Text = "Default";
			this.defaultMatchingFARButton.UseVisualStyleBackColor = true;
			this.defaultMatchingFARButton.Click += new System.EventHandler(this.DefaultMatchingFARButtonClick);
			// 
			// matchingFarComboBox
			// 
			this.matchingFarComboBox.FormattingEnabled = true;
			this.matchingFarComboBox.Location = new System.Drawing.Point(9, 19);
			this.matchingFarComboBox.Name = "matchingFarComboBox";
			this.matchingFarComboBox.Size = new System.Drawing.Size(73, 21);
			this.matchingFarComboBox.TabIndex = 19;
			this.matchingFarComboBox.Leave += new System.EventHandler(this.MatchingFarComboBoxLeave);
			this.matchingFarComboBox.Enter += new System.EventHandler(this.MatchingFarComboBoxEnter);
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
			this.listView.Location = new System.Drawing.Point(6, 53);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(452, 133);
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
			this.groupBox3.Controls.Add(this.identifyButton);
			this.groupBox3.Location = new System.Drawing.Point(0, 216);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(464, 192);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Identification";
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(182, 72);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(273, 23);
			this.nViewZoomSlider1.TabIndex = 16;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.faceView;
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
			this.licensePanel.Size = new System.Drawing.Size(461, 45);
			this.licensePanel.TabIndex = 15;
			// 
			// IdentifyFace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Name = "IdentifyFace";
			this.Size = new System.Drawing.Size(467, 411);
			this.Load += new System.EventHandler(this.IdentifyFaceLoad);
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
		private System.Windows.Forms.Label templatesCountLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button identifyButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label fileForIdentificationLabel;
		private System.Windows.Forms.Button openImageButton;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button openTemplatesButton;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Button defaultMatchingFARButton;
		private System.Windows.Forms.ComboBox matchingFarComboBox;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.GroupBox groupBox3;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NFaceView faceView;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
	}
}
