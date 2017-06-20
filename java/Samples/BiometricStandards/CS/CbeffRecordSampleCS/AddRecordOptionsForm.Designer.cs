namespace Neurotec.Samples
{
	partial class AddRecordOptionsForm
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbStandard = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbCommon
			// 
			this.gbCommon.Location = new System.Drawing.Point(83, 12);
			// 
			// txtBoxFormat
			// 
			this.txtBoxFormat.Size = new System.Drawing.Size(79, 20);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(302, 12);
			this.groupBox1.Size = new System.Drawing.Size(139, 47);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbStandard);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(65, 47);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Standard";
			// 
			// cbStandard
			// 
			this.cbStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbStandard.FormattingEnabled = true;
			this.cbStandard.Location = new System.Drawing.Point(7, 19);
			this.cbStandard.Name = "cbStandard";
			this.cbStandard.Size = new System.Drawing.Size(52, 21);
			this.cbStandard.TabIndex = 0;
			// 
			// AddRecordOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(449, 175);
			this.Controls.Add(this.groupBox2);
			this.Name = "AddRecordOptionsForm";
			this.Controls.SetChildIndex(this.gbCommon, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cbStandard;

	}
}
