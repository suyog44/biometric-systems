namespace Neurotec.Samples
{
	partial class DababaseOperationTab
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.lblStatus = new System.Windows.Forms.Label();
			this.rtbError = new System.Windows.Forms.RichTextBox();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.AutoScroll = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.progressBar1, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.lblStatus, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.rtbError, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel, 0, 2);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(455, 312);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 289);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Operation is in progress:";
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(129, 292);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(323, 17);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 2;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.BackColor = System.Drawing.Color.Orange;
			this.tableLayoutPanel.SetColumnSpan(this.lblStatus, 2);
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(3, 0);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.lblStatus.Size = new System.Drawing.Size(449, 22);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "Operation in progress. Please wait ...";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rtbError
			// 
			this.rtbError.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tableLayoutPanel.SetColumnSpan(this.rtbError, 2);
			this.rtbError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbError.ForeColor = System.Drawing.Color.Red;
			this.rtbError.Location = new System.Drawing.Point(3, 25);
			this.rtbError.Name = "rtbError";
			this.rtbError.ReadOnly = true;
			this.rtbError.Size = new System.Drawing.Size(449, 172);
			this.rtbError.TabIndex = 6;
			this.rtbError.Text = "";
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.AutoScroll = true;
			this.tableLayoutPanel.SetColumnSpan(this.flowLayoutPanel, 2);
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(3, 203);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(449, 83);
			this.flowLayoutPanel.TabIndex = 7;
			this.flowLayoutPanel.WrapContents = false;
			// 
			// DababaseOperationTab
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "DababaseOperationTab";
			this.Size = new System.Drawing.Size(455, 312);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.RichTextBox rtbError;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
	}
}
