namespace Neurotec.Samples
{
	partial class MatchingResultView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.lblDetails = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// linkLabel
			// 
			this.linkLabel.AutoSize = true;
			this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel.LinkColor = System.Drawing.Color.Black;
			this.linkLabel.Location = new System.Drawing.Point(3, 0);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(105, 16);
			this.linkLabel.TabIndex = 0;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "Matched with {0}";
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
			// 
			// lblDetails
			// 
			this.lblDetails.AutoSize = true;
			this.lblDetails.Location = new System.Drawing.Point(10, 25);
			this.lblDetails.Name = "lblDetails";
			this.lblDetails.Size = new System.Drawing.Size(84, 13);
			this.lblDetails.TabIndex = 1;
			this.lblDetails.Text = "Matching details";
			// 
			// MatchingResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.lblDetails);
			this.Controls.Add(this.linkLabel);
			this.Name = "MatchingResultView";
			this.Size = new System.Drawing.Size(188, 45);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Label lblDetails;

	}
}
