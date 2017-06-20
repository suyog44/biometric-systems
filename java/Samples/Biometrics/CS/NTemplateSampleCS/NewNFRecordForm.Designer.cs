namespace Neurotec.Samples
{
	partial class NewNFRecordForm
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
			this.nudWidth = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.nudHeight = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.nudHorzResolution = new System.Windows.Forms.NumericUpDown();
			this.nudVertResolution = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHorzResolution)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVertResolution)).BeginInit();
			this.SuspendLayout();
			// 
			// nudWidth
			// 
			this.nudWidth.Location = new System.Drawing.Point(132, 12);
			this.nudWidth.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(120, 20);
			this.nudWidth.TabIndex = 0;
			this.nudWidth.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(88, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Width:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(85, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height:";
			// 
			// nudHeight
			// 
			this.nudHeight.Location = new System.Drawing.Point(132, 38);
			this.nudHeight.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(120, 20);
			this.nudHeight.TabIndex = 2;
			this.nudHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Horizontal resolution:";
			// 
			// nudHorzResolution
			// 
			this.nudHorzResolution.Location = new System.Drawing.Point(132, 62);
			this.nudHorzResolution.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudHorzResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudHorzResolution.Name = "nudHorzResolution";
			this.nudHorzResolution.Size = new System.Drawing.Size(120, 20);
			this.nudHorzResolution.TabIndex = 4;
			this.nudHorzResolution.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// nudVertResolution
			// 
			this.nudVertResolution.Location = new System.Drawing.Point(132, 88);
			this.nudVertResolution.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudVertResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudVertResolution.Name = "nudVertResolution";
			this.nudVertResolution.Size = new System.Drawing.Size(120, 20);
			this.nudVertResolution.TabIndex = 6;
			this.nudVertResolution.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(33, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Vertical resolution:";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(98, 151);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(179, 151);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// NewNFRecordForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(266, 186);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.nudVertResolution);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.nudHorzResolution);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nudHeight);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudWidth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewNFRecordForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Add NFRecord";
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHorzResolution)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVertResolution)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown nudWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudHorzResolution;
		private System.Windows.Forms.NumericUpDown nudVertResolution;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
	}
}
