namespace Neurotec.Samples
{
	partial class ValidateForm
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
			this.progressTitleLabel = new System.Windows.Forms.Label();
			this.progressLabel = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.errorsLabel = new System.Windows.Forms.Label();
			this.errorsSplitContainer = new System.Windows.Forms.SplitContainer();
			this.lbError = new System.Windows.Forms.ListBox();
			this.tbError = new System.Windows.Forms.TextBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.errorsSplitContainer.Panel1.SuspendLayout();
			this.errorsSplitContainer.Panel2.SuspendLayout();
			this.errorsSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressTitleLabel
			// 
			this.progressTitleLabel.AutoSize = true;
			this.progressTitleLabel.Location = new System.Drawing.Point(12, 9);
			this.progressTitleLabel.Name = "progressTitleLabel";
			this.progressTitleLabel.Size = new System.Drawing.Size(51, 13);
			this.progressTitleLabel.TabIndex = 0;
			this.progressTitleLabel.Text = "Progress:";
			// 
			// progressLabel
			// 
			this.progressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressLabel.AutoEllipsis = true;
			this.progressLabel.Location = new System.Drawing.Point(69, 9);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(518, 15);
			this.progressLabel.TabIndex = 1;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(12, 27);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(575, 19);
			this.progressBar.TabIndex = 2;
			// 
			// errorsLabel
			// 
			this.errorsLabel.AutoSize = true;
			this.errorsLabel.Location = new System.Drawing.Point(12, 59);
			this.errorsLabel.Name = "errorsLabel";
			this.errorsLabel.Size = new System.Drawing.Size(37, 13);
			this.errorsLabel.TabIndex = 3;
			this.errorsLabel.Text = "Errors:";
			// 
			// errorsSplitContainer
			// 
			this.errorsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.errorsSplitContainer.Location = new System.Drawing.Point(15, 75);
			this.errorsSplitContainer.Name = "errorsSplitContainer";
			// 
			// errorsSplitContainer.Panel1
			// 
			this.errorsSplitContainer.Panel1.Controls.Add(this.lbError);
			// 
			// errorsSplitContainer.Panel2
			// 
			this.errorsSplitContainer.Panel2.Controls.Add(this.tbError);
			this.errorsSplitContainer.Size = new System.Drawing.Size(572, 191);
			this.errorsSplitContainer.SplitterDistance = 285;
			this.errorsSplitContainer.TabIndex = 4;
			// 
			// lbError
			// 
			this.lbError.DisplayMember = "FileName";
			this.lbError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbError.FormattingEnabled = true;
			this.lbError.HorizontalScrollbar = true;
			this.lbError.Location = new System.Drawing.Point(0, 0);
			this.lbError.Name = "lbError";
			this.lbError.Size = new System.Drawing.Size(285, 186);
			this.lbError.TabIndex = 0;
			this.lbError.SelectedIndexChanged += new System.EventHandler(this.LbErrorSelectedIndexChanged);
			// 
			// tbError
			// 
			this.tbError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbError.Location = new System.Drawing.Point(0, 0);
			this.tbError.Multiline = true;
			this.tbError.Name = "tbError";
			this.tbError.ReadOnly = true;
			this.tbError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbError.Size = new System.Drawing.Size(283, 191);
			this.tbError.TabIndex = 0;
			this.tbError.WordWrap = false;
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Location = new System.Drawing.Point(431, 286);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 5;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.Enabled = false;
			this.btnClose.Location = new System.Drawing.Point(512, 286);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerDoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.WorkerProgressChanged);
			// 
			// ValidateForm
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(599, 321);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.errorsSplitContainer);
			this.Controls.Add(this.errorsLabel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.progressTitleLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ValidateForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Validate";
			this.Shown += new System.EventHandler(this.ValidateFormShown);
			this.errorsSplitContainer.Panel1.ResumeLayout(false);
			this.errorsSplitContainer.Panel2.ResumeLayout(false);
			this.errorsSplitContainer.Panel2.PerformLayout();
			this.errorsSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label progressTitleLabel;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label errorsLabel;
		private System.Windows.Forms.SplitContainer errorsSplitContainer;
		private System.Windows.Forms.ListBox lbError;
		private System.Windows.Forms.TextBox tbError;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnClose;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
	}
}
