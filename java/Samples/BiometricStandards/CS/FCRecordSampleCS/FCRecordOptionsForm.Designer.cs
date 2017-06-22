namespace Neurotec.Samples
{
	partial class FCRecordOptionsForm
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
			this.gbFCRecord = new System.Windows.Forms.GroupBox();
			this.cbSkipFeaturePoints = new System.Windows.Forms.CheckBox();
			this.cbProcessFirstFaceImageOnly = new System.Windows.Forms.CheckBox();
			this.gbFCRecord.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(246, 213);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(165, 213);
			// 
			// gbFCRecord
			// 
			this.gbFCRecord.Controls.Add(this.cbSkipFeaturePoints);
			this.gbFCRecord.Controls.Add(this.cbProcessFirstFaceImageOnly);
			this.gbFCRecord.Location = new System.Drawing.Point(12, 141);
			this.gbFCRecord.Name = "gbFCRecord";
			this.gbFCRecord.Size = new System.Drawing.Size(308, 66);
			this.gbFCRecord.TabIndex = 5;
			this.gbFCRecord.TabStop = false;
			this.gbFCRecord.Text = "FCRecord";
			// 
			// cbSkipFeaturePoints
			// 
			this.cbSkipFeaturePoints.AutoSize = true;
			this.cbSkipFeaturePoints.Location = new System.Drawing.Point(21, 42);
			this.cbSkipFeaturePoints.Name = "cbSkipFeaturePoints";
			this.cbSkipFeaturePoints.Size = new System.Drawing.Size(114, 17);
			this.cbSkipFeaturePoints.TabIndex = 1;
			this.cbSkipFeaturePoints.Text = "Skip feature points";
			this.cbSkipFeaturePoints.UseVisualStyleBackColor = true;
			// 
			// cbProcessFirstFaceImageOnly
			// 
			this.cbProcessFirstFaceImageOnly.AutoSize = true;
			this.cbProcessFirstFaceImageOnly.Location = new System.Drawing.Point(21, 19);
			this.cbProcessFirstFaceImageOnly.Name = "cbProcessFirstFaceImageOnly";
			this.cbProcessFirstFaceImageOnly.Size = new System.Drawing.Size(160, 17);
			this.cbProcessFirstFaceImageOnly.TabIndex = 0;
			this.cbProcessFirstFaceImageOnly.Text = "Process first face image only";
			this.cbProcessFirstFaceImageOnly.UseVisualStyleBackColor = true;
			// 
			// FCRecordOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 248);
			this.Controls.Add(this.gbFCRecord);
			this.Name = "FCRecordOptionsForm";
			this.Text = "FCRecordOptionsForm";
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.gbFCRecord, 0);
			this.gbFCRecord.ResumeLayout(false);
			this.gbFCRecord.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbFCRecord;
		private System.Windows.Forms.CheckBox cbSkipFeaturePoints;
		private System.Windows.Forms.CheckBox cbProcessFirstFaceImageOnly;

	}
}
