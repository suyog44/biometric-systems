namespace Neurotec.Samples.Controls
{
	partial class DeduplicationPanel
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.pbarProgress = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.gbProperties = new System.Windows.Forms.GroupBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.tbLogFile = new System.Windows.Forms.TextBox();
			this.rtbStatus = new System.Windows.Forms.RichTextBox();
			this.pbStatus = new System.Windows.Forms.PictureBox();
			this.lblRemaining = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.gbProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(6, 32);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(6, 3);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// pbarProgress
			// 
			this.pbarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pbarProgress.Location = new System.Drawing.Point(6, 80);
			this.pbarProgress.Name = "pbarProgress";
			this.pbarProgress.Size = new System.Drawing.Size(758, 23);
			this.pbarProgress.TabIndex = 6;
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.Location = new System.Drawing.Point(390, 64);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(371, 13);
			this.lblProgress.TabIndex = 5;
			this.lblProgress.Text = "progress label";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gbProperties
			// 
			this.gbProperties.Controls.Add(this.btnBrowse);
			this.gbProperties.Controls.Add(this.label5);
			this.gbProperties.Controls.Add(this.tbLogFile);
			this.gbProperties.Location = new System.Drawing.Point(87, 3);
			this.gbProperties.Name = "gbProperties";
			this.gbProperties.Size = new System.Drawing.Size(527, 52);
			this.gbProperties.TabIndex = 3;
			this.gbProperties.TabStop = false;
			this.gbProperties.Text = "Properites";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(482, 17);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(35, 23);
			this.btnBrowse.TabIndex = 8;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(124, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Deduplication results file:";
			// 
			// tbLogFile
			// 
			this.tbLogFile.Location = new System.Drawing.Point(136, 19);
			this.tbLogFile.Name = "tbLogFile";
			this.tbLogFile.Size = new System.Drawing.Size(340, 20);
			this.tbLogFile.TabIndex = 7;
			this.tbLogFile.Text = "results.csv";
			// 
			// rtbStatus
			// 
			this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbStatus.Location = new System.Drawing.Point(67, 109);
			this.rtbStatus.Name = "rtbStatus";
			this.rtbStatus.ReadOnly = true;
			this.rtbStatus.Size = new System.Drawing.Size(697, 242);
			this.rtbStatus.TabIndex = 6;
			this.rtbStatus.Text = "";
			// 
			// pbStatus
			// 
			this.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbStatus.Location = new System.Drawing.Point(9, 109);
			this.pbStatus.Name = "pbStatus";
			this.pbStatus.Size = new System.Drawing.Size(52, 50);
			this.pbStatus.TabIndex = 31;
			this.pbStatus.TabStop = false;
			// 
			// lblRemaining
			// 
			this.lblRemaining.AutoSize = true;
			this.lblRemaining.Location = new System.Drawing.Point(3, 64);
			this.lblRemaining.Name = "lblRemaining";
			this.lblRemaining.Size = new System.Drawing.Size(126, 13);
			this.lblRemaining.TabIndex = 4;
			this.lblRemaining.Text = "Estimated time remaining:";
			// 
			// openFileDialog
			// 
			this.openFileDialog.CheckFileExists = false;
			this.openFileDialog.FileName = "deduplication results.csv";
			// 
			// DeduplicationPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.lblRemaining);
			this.Controls.Add(this.rtbStatus);
			this.Controls.Add(this.pbStatus);
			this.Controls.Add(this.gbProperties);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pbarProgress);
			this.Name = "DeduplicationPanel";
			this.Size = new System.Drawing.Size(764, 354);
			this.Load += new System.EventHandler(this.DeduplicationPanelLoad);
			this.gbProperties.ResumeLayout(false);
			this.gbProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ProgressBar pbarProgress;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.GroupBox gbProperties;
		private System.Windows.Forms.RichTextBox rtbStatus;
		private System.Windows.Forms.PictureBox pbStatus;
		private System.Windows.Forms.Label lblRemaining;
		private System.Windows.Forms.TextBox tbLogFile;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}
