namespace Neurotec.Samples.TenPrintCard
{
	partial class TenPrintCardForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenPrintCardForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbOpenFile = new System.Windows.Forms.ToolStripButton();
			this.tbsScanDefault = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbScan = new System.Windows.Forms.ToolStripMenuItem();
			this.selectDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbOK = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenFile,
            this.tbsScanDefault,
            this.toolStripSeparator1,
            this.tsbOK});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(814, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip";
			// 
			// tsbOpenFile
			// 
			this.tsbOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFile.Image")));
			this.tsbOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpenFile.Name = "tsbOpenFile";
			this.tsbOpenFile.Size = new System.Drawing.Size(23, 22);
			this.tsbOpenFile.Text = "Open image from file";
			this.tsbOpenFile.Click += new System.EventHandler(this.TsbOpenFileClick);
			// 
			// tbsScanDefault
			// 
			this.tbsScanDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbsScanDefault.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbScan,
            this.selectDeviceToolStripMenuItem});
			this.tbsScanDefault.Image = ((System.Drawing.Image)(resources.GetObject("tbsScanDefault.Image")));
			this.tbsScanDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbsScanDefault.Name = "tbsScanDefault";
			this.tbsScanDefault.Size = new System.Drawing.Size(32, 22);
			this.tbsScanDefault.Text = "Acquire image from scanner";
			this.tbsScanDefault.ButtonClick += new System.EventHandler(this.TbsScanDefaultButtonClick);
			// 
			// tsbScan
			// 
			this.tsbScan.Name = "tsbScan";
			this.tsbScan.Size = new System.Drawing.Size(151, 22);
			this.tsbScan.Text = "Scan...";
			this.tsbScan.Click += new System.EventHandler(this.TsbScanClick);
			// 
			// selectDeviceToolStripMenuItem
			// 
			this.selectDeviceToolStripMenuItem.Name = "selectDeviceToolStripMenuItem";
			this.selectDeviceToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.selectDeviceToolStripMenuItem.Text = "Select device...";
			this.selectDeviceToolStripMenuItem.Click += new System.EventHandler(this.SelectDeviceToolStripMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbOK
			// 
			this.tsbOK.Enabled = false;
			this.tsbOK.Image = ((System.Drawing.Image)(resources.GetObject("tsbOK.Image")));
			this.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOK.Name = "tsbOK";
			this.tsbOK.Size = new System.Drawing.Size(113, 22);
			this.tsbOK.Text = "Add fingerprints";
			this.tsbOK.Click += new System.EventHandler(this.TsbOKClick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = resources.GetString("openFileDialog1.Filter");
			this.openFileDialog1.RestoreDirectory = true;
			// 
			// TenPrintCardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(814, 714);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "TenPrintCardForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TenPrint Card";
			this.Load += new System.EventHandler(this.TenPrintCardFormLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenPrintCardFormKeyDown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbOpenFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbOK;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripSplitButton tbsScanDefault;
		private System.Windows.Forms.ToolStripMenuItem tsbScan;
		private System.Windows.Forms.ToolStripMenuItem selectDeviceToolStripMenuItem;

	}
}
