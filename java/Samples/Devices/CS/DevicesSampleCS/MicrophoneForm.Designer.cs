namespace Neurotec.Samples
{
	sealed partial class MicrophoneForm
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
			this.soundLevelProgressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// soundLevelProgressBar
			// 
			this.soundLevelProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.soundLevelProgressBar.Location = new System.Drawing.Point(146, 140);
			this.soundLevelProgressBar.Name = "soundLevelProgressBar";
			this.soundLevelProgressBar.Size = new System.Drawing.Size(363, 20);
			this.soundLevelProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.soundLevelProgressBar.TabIndex = 11;
			// 
			// MicrophoneForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 544);
			this.Controls.Add(this.soundLevelProgressBar);
			this.Name = "MicrophoneForm";
			this.Text = "MicrophoneForm";
			this.Controls.SetChildIndex(this.soundLevelProgressBar, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar soundLevelProgressBar;
	}
}
