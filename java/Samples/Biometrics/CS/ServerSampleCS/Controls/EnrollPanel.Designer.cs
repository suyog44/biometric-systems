namespace Neurotec.Samples.Controls
{
	partial class EnrollPanel
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
			this.pbarProgress = new System.Windows.Forms.ProgressBar();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbResults = new System.Windows.Forms.GroupBox();
			this.rtbStatus = new System.Windows.Forms.RichTextBox();
			this.tbTaskCount = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pbStatus = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.lblProgress = new System.Windows.Forms.Label();
			this.gbProperties = new System.Windows.Forms.GroupBox();
			this.nudBunchSize = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.lblRemaining = new System.Windows.Forms.Label();
			this.gbResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
			this.gbProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBunchSize)).BeginInit();
			this.SuspendLayout();
			// 
			// pbarProgress
			// 
			this.pbarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pbarProgress.Location = new System.Drawing.Point(6, 78);
			this.pbarProgress.Name = "pbarProgress";
			this.pbarProgress.Size = new System.Drawing.Size(707, 23);
			this.pbarProgress.TabIndex = 4;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(3, 3);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(3, 32);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// gbResults
			// 
			this.gbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbResults.Controls.Add(this.rtbStatus);
			this.gbResults.Controls.Add(this.tbTaskCount);
			this.gbResults.Controls.Add(this.label1);
			this.gbResults.Controls.Add(this.pbStatus);
			this.gbResults.Controls.Add(this.label5);
			this.gbResults.Controls.Add(this.tbTime);
			this.gbResults.Location = new System.Drawing.Point(3, 107);
			this.gbResults.Name = "gbResults";
			this.gbResults.Size = new System.Drawing.Size(707, 209);
			this.gbResults.TabIndex = 6;
			this.gbResults.TabStop = false;
			this.gbResults.Text = "Results";
			// 
			// rtbStatus
			// 
			this.rtbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbStatus.Location = new System.Drawing.Point(68, 51);
			this.rtbStatus.Name = "rtbStatus";
			this.rtbStatus.ReadOnly = true;
			this.rtbStatus.Size = new System.Drawing.Size(631, 152);
			this.rtbStatus.TabIndex = 4;
			this.rtbStatus.Text = "";
			// 
			// tbTaskCount
			// 
			this.tbTaskCount.Location = new System.Drawing.Point(111, 19);
			this.tbTaskCount.Name = "tbTaskCount";
			this.tbTaskCount.ReadOnly = true;
			this.tbTaskCount.Size = new System.Drawing.Size(109, 20);
			this.tbTaskCount.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Templates to enroll:";
			// 
			// pbStatus
			// 
			this.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbStatus.Location = new System.Drawing.Point(10, 51);
			this.pbStatus.Name = "pbStatus";
			this.pbStatus.Size = new System.Drawing.Size(52, 50);
			this.pbStatus.TabIndex = 15;
			this.pbStatus.TabStop = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(237, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Time elapsed:";
			// 
			// tbTime
			// 
			this.tbTime.Location = new System.Drawing.Point(316, 19);
			this.tbTime.Name = "tbTime";
			this.tbTime.ReadOnly = true;
			this.tbTime.Size = new System.Drawing.Size(136, 20);
			this.tbTime.TabIndex = 3;
			this.tbTime.Text = "N/A";
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.Location = new System.Drawing.Point(343, 62);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(367, 13);
			this.lblProgress.TabIndex = 5;
			this.lblProgress.Text = "progress";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// gbProperties
			// 
			this.gbProperties.Controls.Add(this.nudBunchSize);
			this.gbProperties.Controls.Add(this.label4);
			this.gbProperties.Location = new System.Drawing.Point(84, 3);
			this.gbProperties.Name = "gbProperties";
			this.gbProperties.Size = new System.Drawing.Size(371, 52);
			this.gbProperties.TabIndex = 2;
			this.gbProperties.TabStop = false;
			this.gbProperties.Text = "Properties";
			// 
			// nudBunchSize
			// 
			this.nudBunchSize.Location = new System.Drawing.Point(101, 19);
			this.nudBunchSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudBunchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudBunchSize.Name = "nudBunchSize";
			this.nudBunchSize.Size = new System.Drawing.Size(128, 20);
			this.nudBunchSize.TabIndex = 1;
			this.nudBunchSize.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Bunch size:";
			// 
			// lblRemaining
			// 
			this.lblRemaining.AutoSize = true;
			this.lblRemaining.Location = new System.Drawing.Point(0, 62);
			this.lblRemaining.Name = "lblRemaining";
			this.lblRemaining.Size = new System.Drawing.Size(126, 13);
			this.lblRemaining.TabIndex = 3;
			this.lblRemaining.Text = "Estimated time remaining:";
			// 
			// EnrollPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.lblRemaining);
			this.Controls.Add(this.gbProperties);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.gbResults);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pbarProgress);
			this.Name = "EnrollPanel";
			this.Size = new System.Drawing.Size(713, 319);
			this.Load += new System.EventHandler(this.EnrollPanelLoad);
			this.gbResults.ResumeLayout(false);
			this.gbResults.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
			this.gbProperties.ResumeLayout(false);
			this.gbProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBunchSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbarProgress;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbResults;
		private System.Windows.Forms.RichTextBox rtbStatus;
		private System.Windows.Forms.TextBox tbTaskCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pbStatus;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.GroupBox gbProperties;
		private System.Windows.Forms.Label lblRemaining;
		private System.Windows.Forms.NumericUpDown nudBunchSize;
		private System.Windows.Forms.Label label4;
	}
}
