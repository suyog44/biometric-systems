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
			this.btnOpen = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.irisView = new Neurotec.Biometrics.Gui.NIrisView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblQuality = new System.Windows.Forms.Label();
			this.btnSaveTemplate = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.licensePanel = new Neurotec.Samples.LicensePanel();
			this.nViewZoomSlider1 = new Neurotec.Gui.NViewZoomSlider();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOpen.Location = new System.Drawing.Point(3, 3);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(93, 23);
			this.btnOpen.TabIndex = 3;
			this.btnOpen.Tag = "Open";
			this.btnOpen.Text = "Open Image";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.btnOpen);
			this.panel1.Location = new System.Drawing.Point(0, 51);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(497, 35);
			this.panel1.TabIndex = 5;
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
			this.irisView.Location = new System.Drawing.Point(3, 84);
			this.irisView.Name = "irisView";
			this.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow;
			this.irisView.OuterBoundaryWidth = 2;
			this.irisView.Size = new System.Drawing.Size(494, 245);
			this.irisView.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.nViewZoomSlider1);
			this.panel2.Controls.Add(this.lblQuality);
			this.panel2.Controls.Add(this.btnSaveTemplate);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 335);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(497, 46);
			this.panel2.TabIndex = 7;
			// 
			// lblQuality
			// 
			this.lblQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblQuality.Location = new System.Drawing.Point(315, 11);
			this.lblQuality.Name = "lblQuality";
			this.lblQuality.Size = new System.Drawing.Size(170, 23);
			this.lblQuality.TabIndex = 1;
			// 
			// btnSaveTemplate
			// 
			this.btnSaveTemplate.Enabled = false;
			this.btnSaveTemplate.Location = new System.Drawing.Point(3, 11);
			this.btnSaveTemplate.Name = "btnSaveTemplate";
			this.btnSaveTemplate.Size = new System.Drawing.Size(96, 23);
			this.btnSaveTemplate.TabIndex = 6;
			this.btnSaveTemplate.Text = "&Save Template";
			this.btnSaveTemplate.UseVisualStyleBackColor = true;
			this.btnSaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// licensePanel
			// 
			this.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.licensePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.licensePanel.Location = new System.Drawing.Point(0, 0);
			this.licensePanel.Name = "licensePanel";
			this.licensePanel.OptionalComponents = "";
			this.licensePanel.RequiredComponents = "Biometrics.IrisExtraction";
			this.licensePanel.Size = new System.Drawing.Size(497, 45);
			this.licensePanel.TabIndex = 8;
			// 
			// nViewZoomSlider1
			// 
			this.nViewZoomSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nViewZoomSlider1.Location = new System.Drawing.Point(245, 11);
			this.nViewZoomSlider1.Name = "nViewZoomSlider1";
			this.nViewZoomSlider1.Size = new System.Drawing.Size(249, 23);
			this.nViewZoomSlider1.TabIndex = 7;
			this.nViewZoomSlider1.Text = "nViewZoomSlider1";
			this.nViewZoomSlider1.View = this.irisView;
			// 
			// EnrollFromImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.irisView);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.licensePanel);
			this.Controls.Add(this.panel2);
			this.Name = "EnrollFromImage";
			this.Size = new System.Drawing.Size(497, 381);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblQuality;
		private System.Windows.Forms.Button btnSaveTemplate;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private LicensePanel licensePanel;
		private Neurotec.Biometrics.Gui.NIrisView irisView;
		private Neurotec.Gui.NViewZoomSlider nViewZoomSlider1;
	}
}
