namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType13RecordCreateForm
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
			this.cbFpImpressionType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.imageLoader = new Neurotec.Samples.RecordCreateForms.ImageLoaderControl();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(155, 479);
			this.btnOk.TabIndex = 5;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(236, 479);
			this.btnCancel.TabIndex = 6;
			// 
			// cbFpImpressionType
			// 
			this.cbFpImpressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFpImpressionType.FormattingEnabled = true;
			this.cbFpImpressionType.Location = new System.Drawing.Point(132, 52);
			this.cbFpImpressionType.Name = "cbFpImpressionType";
			this.cbFpImpressionType.Size = new System.Drawing.Size(172, 21);
			this.cbFpImpressionType.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "FP Impression type:";
			// 
			// imageLoader
			// 
			this.imageLoader.Bpx = ((byte)(0));
			this.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified;
			this.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None;
			this.imageLoader.HasBpx = true;
			this.imageLoader.HasColorspace = false;
			this.imageLoader.Hll = ((ushort)(0));
			this.imageLoader.Hps = ((ushort)(0));
			this.imageLoader.Location = new System.Drawing.Point(3, 79);
			this.imageLoader.Name = "imageLoader";
			this.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None;
			this.imageLoader.Size = new System.Drawing.Size(311, 389);
			this.imageLoader.Src = "";
			this.imageLoader.TabIndex = 4;
			this.imageLoader.Vll = ((ushort)(0));
			this.imageLoader.Vps = ((ushort)(0));
			// 
			// ANType13RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(321, 512);
			this.Controls.Add(this.cbFpImpressionType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.imageLoader);
			this.Name = "ANType13RecordCreateForm";
			this.Text = "Add Type-13 ANRecord";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.imageLoader, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.cbFpImpressionType, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbFpImpressionType;
		private System.Windows.Forms.Label label2;
		private ImageLoaderControl imageLoader;
	}
}
