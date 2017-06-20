namespace Neurotec.Samples
{
	partial class RawFaceImageOptionsForm
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.cbImageColorSpace = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tbWidth = new System.Windows.Forms.TextBox();
			this.tbHeight = new System.Windows.Forms.TextBox();
			this.tbVendorImageColorSpace = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.gbMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(127, 211);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(208, 211);
			// 
			// gbMain
			// 
			this.gbMain.Controls.Add(this.tbVendorImageColorSpace);
			this.gbMain.Controls.Add(this.tbHeight);
			this.gbMain.Controls.Add(this.tbWidth);
			this.gbMain.Controls.Add(this.label6);
			this.gbMain.Controls.Add(this.label5);
			this.gbMain.Controls.Add(this.label4);
			this.gbMain.Controls.Add(this.label1);
			this.gbMain.Controls.Add(this.cbImageColorSpace);
			this.gbMain.Size = new System.Drawing.Size(268, 201);
			this.gbMain.Controls.SetChildIndex(this.cbFaceImageType, 0);
			this.gbMain.Controls.SetChildIndex(this.cbImageDataType, 0);
			this.gbMain.Controls.SetChildIndex(this.cbImageColorSpace, 0);
			this.gbMain.Controls.SetChildIndex(this.label1, 0);
			this.gbMain.Controls.SetChildIndex(this.label4, 0);
			this.gbMain.Controls.SetChildIndex(this.label5, 0);
			this.gbMain.Controls.SetChildIndex(this.label6, 0);
			this.gbMain.Controls.SetChildIndex(this.tbWidth, 0);
			this.gbMain.Controls.SetChildIndex(this.tbHeight, 0);
			this.gbMain.Controls.SetChildIndex(this.tbVendorImageColorSpace, 0);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Face image color space:";
			// 
			// cbImageColorSpace
			// 
			this.cbImageColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImageColorSpace.FormattingEnabled = true;
			this.cbImageColorSpace.Location = new System.Drawing.Point(152, 73);
			this.cbImageColorSpace.Name = "cbImageColorSpace";
			this.cbImageColorSpace.Size = new System.Drawing.Size(105, 21);
			this.cbImageColorSpace.TabIndex = 6;
			this.cbImageColorSpace.SelectedIndexChanged += new System.EventHandler(this.cbImageColorSpace_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Width:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Height:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 148);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(133, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Vendor image color space:";
			// 
			// tbWidth
			// 
			this.errorProvider.SetIconAlignment(this.tbWidth, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.tbWidth.Location = new System.Drawing.Point(152, 100);
			this.tbWidth.Name = "tbWidth";
			this.tbWidth.Size = new System.Drawing.Size(105, 20);
			this.tbWidth.TabIndex = 11;
			this.tbWidth.Text = "0";
			this.tbWidth.Validating += new System.ComponentModel.CancelEventHandler(this.tbWidthHeight_Validating);
			// 
			// tbHeight
			// 
			this.errorProvider.SetIconAlignment(this.tbHeight, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.tbHeight.Location = new System.Drawing.Point(152, 122);
			this.tbHeight.Name = "tbHeight";
			this.tbHeight.Size = new System.Drawing.Size(105, 20);
			this.tbHeight.TabIndex = 12;
			this.tbHeight.Text = "0";
			this.tbHeight.Validating += new System.ComponentModel.CancelEventHandler(this.tbWidthHeight_Validating);
			// 
			// tbVendorImageColorSpace
			// 
			this.errorProvider.SetIconAlignment(this.tbVendorImageColorSpace, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
			this.tbVendorImageColorSpace.Location = new System.Drawing.Point(152, 145);
			this.tbVendorImageColorSpace.Name = "tbVendorImageColorSpace";
			this.tbVendorImageColorSpace.Size = new System.Drawing.Size(105, 20);
			this.tbVendorImageColorSpace.TabIndex = 13;
			this.tbVendorImageColorSpace.Text = "0";
			this.tbVendorImageColorSpace.Validating += new System.ComponentModel.CancelEventHandler(this.tbVendorImageColorSpace_Validating);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// RawFaceImageOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(295, 246);
			this.Name = "RawFaceImageOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RawFaceImageOptionsForm";
			this.gbMain.ResumeLayout(false);
			this.gbMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbImageColorSpace;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbVendorImageColorSpace;
		private System.Windows.Forms.TextBox tbHeight;
		private System.Windows.Forms.TextBox tbWidth;
		private System.Windows.Forms.ErrorProvider errorProvider;
	}
}
