namespace Neurotec.Samples.Controls
{
	partial class TestSpeedPanel
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
			this.tbDBSize = new System.Windows.Forms.TextBox();
			this.lblTemplatesOnAcc = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbSpeed = new System.Windows.Forms.TextBox();
			this.gbProperties = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.nudMaxCount = new System.Windows.Forms.NumericUpDown();
			this.gbResults = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.rtbStatus = new System.Windows.Forms.RichTextBox();
			this.lblTemplateInfo = new System.Windows.Forms.Label();
			this.pbStatus = new System.Windows.Forms.PictureBox();
			this.tbTaskCount = new System.Windows.Forms.TextBox();
			this.lblCount = new System.Windows.Forms.Label();
			this.lblRemaining = new System.Windows.Forms.Label();
			this.gbProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxCount)).BeginInit();
			this.gbResults.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// pbarProgress
			// 
			this.pbarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pbarProgress.Location = new System.Drawing.Point(3, 95);
			this.pbarProgress.Name = "pbarProgress";
			this.pbarProgress.Size = new System.Drawing.Size(604, 23);
			this.pbarProgress.TabIndex = 5;
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
			// tbDBSize
			// 
			this.tbDBSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDBSize.Location = new System.Drawing.Point(363, 3);
			this.tbDBSize.Name = "tbDBSize";
			this.tbDBSize.ReadOnly = true;
			this.tbDBSize.Size = new System.Drawing.Size(261, 20);
			this.tbDBSize.TabIndex = 3;
			this.tbDBSize.Text = "N/A";
			// 
			// lblTemplatesOnAcc
			// 
			this.lblTemplatesOnAcc.AutoSize = true;
			this.lblTemplatesOnAcc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTemplatesOnAcc.Location = new System.Drawing.Point(227, 5);
			this.lblTemplatesOnAcc.Name = "lblTemplatesOnAcc";
			this.lblTemplatesOnAcc.Size = new System.Drawing.Size(130, 21);
			this.lblTemplatesOnAcc.TabIndex = 2;
			this.lblTemplatesOnAcc.Text = "Templates on accelerator:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 21);
			this.label5.TabIndex = 4;
			this.label5.Text = "Time elapsed:";
			// 
			// tbTime
			// 
			this.tbTime.Location = new System.Drawing.Point(112, 3);
			this.tbTime.Name = "tbTime";
			this.tbTime.ReadOnly = true;
			this.tableLayoutPanel1.SetRowSpan(this.tbTime, 2);
			this.tbTime.Size = new System.Drawing.Size(109, 20);
			this.tbTime.TabIndex = 5;
			this.tbTime.Text = "N/A";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(227, 31);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(130, 21);
			this.label6.TabIndex = 6;
			this.label6.Text = "Speed:";
			// 
			// tbSpeed
			// 
			this.tbSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbSpeed.Location = new System.Drawing.Point(363, 29);
			this.tbSpeed.Name = "tbSpeed";
			this.tbSpeed.ReadOnly = true;
			this.tbSpeed.Size = new System.Drawing.Size(261, 20);
			this.tbSpeed.TabIndex = 7;
			this.tbSpeed.Text = "N/A";
			// 
			// gbProperties
			// 
			this.gbProperties.Controls.Add(this.label9);
			this.gbProperties.Controls.Add(this.label8);
			this.gbProperties.Controls.Add(this.label7);
			this.gbProperties.Controls.Add(this.nudMaxCount);
			this.gbProperties.Location = new System.Drawing.Point(84, 3);
			this.gbProperties.Name = "gbProperties";
			this.gbProperties.Size = new System.Drawing.Size(287, 70);
			this.gbProperties.TabIndex = 3;
			this.gbProperties.TabStop = false;
			this.gbProperties.Text = "Properites";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 49);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(271, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "* - all templates should be able to fit into memory at once";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(258, 25);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(11, 13);
			this.label8.TabIndex = 6;
			this.label8.Text = "*";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(146, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Maximum templates to match:";
			// 
			// nudMaxCount
			// 
			this.nudMaxCount.Location = new System.Drawing.Point(161, 20);
			this.nudMaxCount.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
			this.nudMaxCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.nudMaxCount.Name = "nudMaxCount";
			this.nudMaxCount.Size = new System.Drawing.Size(94, 20);
			this.nudMaxCount.TabIndex = 5;
			this.nudMaxCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// gbResults
			// 
			this.gbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbResults.Controls.Add(this.tableLayoutPanel1);
			this.gbResults.Location = new System.Drawing.Point(3, 124);
			this.gbResults.Name = "gbResults";
			this.gbResults.Size = new System.Drawing.Size(604, 198);
			this.gbResults.TabIndex = 0;
			this.gbResults.TabStop = false;
			this.gbResults.Text = "Results";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tbDBSize, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.rtbStatus, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblTemplatesOnAcc, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.lblTemplateInfo, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.pbStatus, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.tbTime, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbSpeed, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbTaskCount, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 179);
			this.tableLayoutPanel1.TabIndex = 17;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Templates matched:";
			// 
			// rtbStatus
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.rtbStatus, 3);
			this.rtbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbStatus.Location = new System.Drawing.Point(112, 68);
			this.rtbStatus.Name = "rtbStatus";
			this.rtbStatus.ReadOnly = true;
			this.rtbStatus.Size = new System.Drawing.Size(512, 108);
			this.rtbStatus.TabIndex = 8;
			this.rtbStatus.Text = "";
			// 
			// lblTemplateInfo
			// 
			this.lblTemplateInfo.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblTemplateInfo, 4);
			this.lblTemplateInfo.Location = new System.Drawing.Point(3, 52);
			this.lblTemplateInfo.Name = "lblTemplateInfo";
			this.lblTemplateInfo.Size = new System.Drawing.Size(336, 13);
			this.lblTemplateInfo.TabIndex = 16;
			this.lblTemplateInfo.Text = "* - server template count is assumed to be equal to DB template count";
			// 
			// pbStatus
			// 
			this.pbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbStatus.Location = new System.Drawing.Point(54, 68);
			this.pbStatus.Name = "pbStatus";
			this.pbStatus.Size = new System.Drawing.Size(52, 50);
			this.pbStatus.TabIndex = 15;
			this.pbStatus.TabStop = false;
			// 
			// tbTaskCount
			// 
			this.tbTaskCount.Location = new System.Drawing.Point(112, 29);
			this.tbTaskCount.Name = "tbTaskCount";
			this.tbTaskCount.ReadOnly = true;
			this.tableLayoutPanel1.SetRowSpan(this.tbTaskCount, 2);
			this.tbTaskCount.Size = new System.Drawing.Size(109, 20);
			this.tbTaskCount.TabIndex = 1;
			this.tbTaskCount.Text = "N/A";
			// 
			// lblCount
			// 
			this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCount.Location = new System.Drawing.Point(377, 76);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(230, 13);
			this.lblCount.TabIndex = 4;
			this.lblCount.Text = "progress label";
			this.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblRemaining
			// 
			this.lblRemaining.AutoSize = true;
			this.lblRemaining.Location = new System.Drawing.Point(3, 76);
			this.lblRemaining.Name = "lblRemaining";
			this.lblRemaining.Size = new System.Drawing.Size(126, 13);
			this.lblRemaining.TabIndex = 6;
			this.lblRemaining.Text = "Estimated time remaining:";
			// 
			// TestSpeedPanel
			// 
			this.AccessibleDescription = "";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.lblRemaining);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.gbResults);
			this.Controls.Add(this.gbProperties);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pbarProgress);
			this.Name = "TestSpeedPanel";
			this.Size = new System.Drawing.Size(610, 325);
			this.Load += new System.EventHandler(this.TestSpeedPanelLoad);
			this.gbProperties.ResumeLayout(false);
			this.gbProperties.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxCount)).EndInit();
			this.gbResults.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbarProgress;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbDBSize;
		private System.Windows.Forms.Label lblTemplatesOnAcc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbSpeed;
		private System.Windows.Forms.PictureBox pbStatus;
		private System.Windows.Forms.GroupBox gbProperties;
		private System.Windows.Forms.GroupBox gbResults;
		private System.Windows.Forms.RichTextBox rtbStatus;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.TextBox tbTaskCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudMaxCount;
		private System.Windows.Forms.Label lblTemplateInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblRemaining;
	}
}
