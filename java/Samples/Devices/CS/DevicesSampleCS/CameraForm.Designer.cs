namespace Neurotec.Samples
{
	partial class CameraForm
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
			this.focusButton = new System.Windows.Forms.Button();
			this.resetFocusButton = new System.Windows.Forms.Button();
			this.clickToFocusCheckBox = new System.Windows.Forms.CheckBox();
			this.cameraStatusLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			// 
			// forceButton
			// 
			this.forceButton.Text = "&Capture";
			this.forceButton.Click += new System.EventHandler(this.forceButton_Click);
			// 
			// focusButton
			// 
			this.focusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.focusButton.Location = new System.Drawing.Point(577, 416);
			this.focusButton.Name = "focusButton";
			this.focusButton.Size = new System.Drawing.Size(75, 23);
			this.focusButton.TabIndex = 12;
			this.focusButton.Text = "&Focus";
			this.focusButton.UseVisualStyleBackColor = true;
			this.focusButton.Click += new System.EventHandler(this.focusButton_Click);
			// 
			// resetFocusButton
			// 
			this.resetFocusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.resetFocusButton.Location = new System.Drawing.Point(577, 445);
			this.resetFocusButton.Name = "resetFocusButton";
			this.resetFocusButton.Size = new System.Drawing.Size(75, 23);
			this.resetFocusButton.TabIndex = 13;
			this.resetFocusButton.Text = "&Reset";
			this.resetFocusButton.UseVisualStyleBackColor = true;
			this.resetFocusButton.Click += new System.EventHandler(this.resetFocusButton_Click);
			// 
			// clickToFocusCheckBox
			// 
			this.clickToFocusCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clickToFocusCheckBox.AutoSize = true;
			this.clickToFocusCheckBox.Checked = true;
			this.clickToFocusCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.clickToFocusCheckBox.Location = new System.Drawing.Point(577, 474);
			this.clickToFocusCheckBox.Name = "clickToFocusCheckBox";
			this.clickToFocusCheckBox.Size = new System.Drawing.Size(55, 17);
			this.clickToFocusCheckBox.TabIndex = 14;
			this.clickToFocusCheckBox.Text = "Focus";
			this.clickToFocusCheckBox.UseVisualStyleBackColor = true;
			// 
			// cameraStatusLabel
			// 
			this.cameraStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cameraStatusLabel.AutoSize = true;
			this.cameraStatusLabel.Location = new System.Drawing.Point(574, 493);
			this.cameraStatusLabel.Name = "cameraStatusLabel";
			this.cameraStatusLabel.Size = new System.Drawing.Size(98, 13);
			this.cameraStatusLabel.TabIndex = 15;
			this.cameraStatusLabel.Text = "cameraStatusLabel";
			// 
			// CameraForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 544);
			this.Controls.Add(this.focusButton);
			this.Controls.Add(this.clickToFocusCheckBox);
			this.Controls.Add(this.cameraStatusLabel);
			this.Controls.Add(this.resetFocusButton);
			this.Name = "CameraForm";
			this.Text = "CameraForm";
			this.Controls.SetChildIndex(this.resetFocusButton, 0);
			this.Controls.SetChildIndex(this.cameraStatusLabel, 0);
			this.Controls.SetChildIndex(this.clickToFocusCheckBox, 0);
			this.Controls.SetChildIndex(this.focusButton, 0);
			this.Controls.SetChildIndex(this.pictureBox, 0);
			this.Controls.SetChildIndex(this.forceButton, 0);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button focusButton;
		private System.Windows.Forms.Button resetFocusButton;
		private System.Windows.Forms.CheckBox clickToFocusCheckBox;
		private System.Windows.Forms.Label cameraStatusLabel;
	}
}
