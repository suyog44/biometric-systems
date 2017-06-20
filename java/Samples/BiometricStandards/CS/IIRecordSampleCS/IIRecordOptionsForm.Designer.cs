namespace Neurotec.Samples
{
	partial class IIRecordOptionsForm
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
			this.gbIIRecord = new System.Windows.Forms.GroupBox();
			this.cbProcessFirstIrisImageOnly = new System.Windows.Forms.CheckBox();
			this.gbIIRecord.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(243, 188);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(162, 188);
			// 
			// gbIIRecord
			// 
			this.gbIIRecord.Controls.Add(this.cbProcessFirstIrisImageOnly);
			this.gbIIRecord.Location = new System.Drawing.Point(12, 136);
			this.gbIIRecord.Name = "gbIIRecord";
			this.gbIIRecord.Size = new System.Drawing.Size(311, 46);
			this.gbIIRecord.TabIndex = 3;
			this.gbIIRecord.TabStop = false;
			this.gbIIRecord.Text = "IIRecord";
			// 
			// cbProcessFirstIrisImageOnly
			// 
			this.cbProcessFirstIrisImageOnly.AutoSize = true;
			this.cbProcessFirstIrisImageOnly.Location = new System.Drawing.Point(9, 19);
			this.cbProcessFirstIrisImageOnly.Name = "cbProcessFirstIrisImageOnly";
			this.cbProcessFirstIrisImageOnly.Size = new System.Drawing.Size(151, 17);
			this.cbProcessFirstIrisImageOnly.TabIndex = 0;
			this.cbProcessFirstIrisImageOnly.Text = "Process first iris image only";
			this.cbProcessFirstIrisImageOnly.UseVisualStyleBackColor = true;
			// 
			// IIRecordOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 221);
			this.Controls.Add(this.gbIIRecord);
			this.Name = "IIRecordOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "IIRecordOptionsForm";
			this.Controls.SetChildIndex(this.gbIIRecord, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.gbIIRecord.ResumeLayout(false);
			this.gbIIRecord.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbIIRecord;
		private System.Windows.Forms.CheckBox cbProcessFirstIrisImageOnly;
	}
}
