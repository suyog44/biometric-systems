namespace Neurotec.Samples
{
	partial class CaptureForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureForm));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.statusTextBox = new System.Windows.Forms.TextBox();
			this.forceButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.formatsComboBox = new System.Windows.Forms.ComboBox();
			this.customizeFormatButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(640, 371);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// statusTextBox
			// 
			this.statusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.statusTextBox.Location = new System.Drawing.Point(12, 416);
			this.statusTextBox.Multiline = true;
			this.statusTextBox.Name = "statusTextBox";
			this.statusTextBox.ReadOnly = true;
			this.statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.statusTextBox.Size = new System.Drawing.Size(556, 116);
			this.statusTextBox.TabIndex = 3;
			// 
			// forceButton
			// 
			this.forceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.forceButton.Location = new System.Drawing.Point(577, 387);
			this.forceButton.Name = "forceButton";
			this.forceButton.Size = new System.Drawing.Size(75, 23);
			this.forceButton.TabIndex = 9;
			this.forceButton.Text = "&Force";
			this.forceButton.UseVisualStyleBackColor = true;
			this.forceButton.Click += new System.EventHandler(this.forceButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.closeButton.Location = new System.Drawing.Point(577, 509);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 8;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// formatsComboBox
			// 
			this.formatsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.formatsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.formatsComboBox.FormattingEnabled = true;
			this.formatsComboBox.Location = new System.Drawing.Point(12, 389);
			this.formatsComboBox.Name = "formatsComboBox";
			this.formatsComboBox.Size = new System.Drawing.Size(478, 21);
			this.formatsComboBox.TabIndex = 10;
			this.formatsComboBox.SelectedIndexChanged += new System.EventHandler(this.formatsComboBox_SelectedIndexChanged);
			// 
			// customizeFormatButton
			// 
			this.customizeFormatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.customizeFormatButton.Enabled = false;
			this.customizeFormatButton.Location = new System.Drawing.Point(496, 387);
			this.customizeFormatButton.Name = "customizeFormatButton";
			this.customizeFormatButton.Size = new System.Drawing.Size(75, 23);
			this.customizeFormatButton.TabIndex = 11;
			this.customizeFormatButton.Text = "Customize...";
			this.customizeFormatButton.UseVisualStyleBackColor = true;
			this.customizeFormatButton.Click += new System.EventHandler(this.customizeFormatButton_Click);
			// 
			// CaptureForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(664, 544);
			this.Controls.Add(this.customizeFormatButton);
			this.Controls.Add(this.formatsComboBox);
			this.Controls.Add(this.forceButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.statusTextBox);
			this.Controls.Add(this.pictureBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CaptureForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CaptureForm";
			this.Shown += new System.EventHandler(this.CaptureForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox statusTextBox;
		private System.Windows.Forms.Button closeButton;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ComboBox formatsComboBox;
		protected System.Windows.Forms.PictureBox pictureBox;
		protected System.Windows.Forms.Button forceButton;
		protected System.Windows.Forms.Button customizeFormatButton;
	}
}
