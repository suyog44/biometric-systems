namespace Neurotec.Samples
{
	partial class ExtractionSettingsForm
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
			this.ExtractionGroupBox = new System.Windows.Forms.GroupBox();
			this.pnQualityThreshold = new System.Windows.Forms.Panel();
			this.ThresholdLabel = new System.Windows.Forms.Label();
			this.btnDefaultThreshold = new System.Windows.Forms.Button();
			this.nudThreshold = new System.Windows.Forms.NumericUpDown();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.ExtractionGroupBox.SuspendLayout();
			this.pnQualityThreshold.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
			this.SuspendLayout();
			// 
			// ExtractionGroupBox
			// 
			this.ExtractionGroupBox.Controls.Add(this.pnQualityThreshold);
			this.ExtractionGroupBox.Location = new System.Drawing.Point(12, 12);
			this.ExtractionGroupBox.Name = "ExtractionGroupBox";
			this.ExtractionGroupBox.Size = new System.Drawing.Size(235, 58);
			this.ExtractionGroupBox.TabIndex = 11;
			this.ExtractionGroupBox.TabStop = false;
			this.ExtractionGroupBox.Text = "Quality threshold";
			// 
			// pnQualityThreshold
			// 
			this.pnQualityThreshold.Controls.Add(this.ThresholdLabel);
			this.pnQualityThreshold.Controls.Add(this.btnDefaultThreshold);
			this.pnQualityThreshold.Controls.Add(this.nudThreshold);
			this.pnQualityThreshold.Location = new System.Drawing.Point(6, 19);
			this.pnQualityThreshold.Name = "pnQualityThreshold";
			this.pnQualityThreshold.Size = new System.Drawing.Size(223, 29);
			this.pnQualityThreshold.TabIndex = 14;
			// 
			// ThresholdLabel
			// 
			this.ThresholdLabel.AutoSize = true;
			this.ThresholdLabel.Location = new System.Drawing.Point(12, 8);
			this.ThresholdLabel.Name = "ThresholdLabel";
			this.ThresholdLabel.Size = new System.Drawing.Size(57, 13);
			this.ThresholdLabel.TabIndex = 8;
			this.ThresholdLabel.Text = "Threshold:";
			// 
			// btnDefaultThreshold
			// 
			this.btnDefaultThreshold.Location = new System.Drawing.Point(129, 3);
			this.btnDefaultThreshold.Name = "btnDefaultThreshold";
			this.btnDefaultThreshold.Size = new System.Drawing.Size(75, 23);
			this.btnDefaultThreshold.TabIndex = 10;
			this.btnDefaultThreshold.Text = "Default";
			this.btnDefaultThreshold.UseVisualStyleBackColor = true;
			this.btnDefaultThreshold.Click += new System.EventHandler(this.BtnDefaultThresholdClick);
			// 
			// nudThreshold
			// 
			this.nudThreshold.Location = new System.Drawing.Point(75, 5);
			this.nudThreshold.Name = "nudThreshold";
			this.nudThreshold.Size = new System.Drawing.Size(48, 20);
			this.nudThreshold.TabIndex = 9;
			this.nudThreshold.Value = new decimal(new int[] {
            39,
            0,
            0,
            0});
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(174, 76);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(93, 76);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// ExtractionSettingsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(257, 104);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.ExtractionGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExtractionSettingsForm";
			this.ShowInTaskbar = false;
			this.Text = "Extraction Settings";
			this.ExtractionGroupBox.ResumeLayout(false);
			this.pnQualityThreshold.ResumeLayout(false);
			this.pnQualityThreshold.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox ExtractionGroupBox;
		private System.Windows.Forms.Button btnDefaultThreshold;
		private System.Windows.Forms.NumericUpDown nudThreshold;
		private System.Windows.Forms.Label ThresholdLabel;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel pnQualityThreshold;
	}
}
