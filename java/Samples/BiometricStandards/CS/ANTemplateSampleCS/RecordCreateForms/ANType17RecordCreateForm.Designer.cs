namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType17RecordCreateForm
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
			this.imageLoader = new Neurotec.Samples.RecordCreateForms.ImageLoaderControl();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(154, 457);
			this.btnOk.TabIndex = 3;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(235, 457);
			this.btnCancel.TabIndex = 4;
			// 
			// imageLoader
			// 
			this.imageLoader.Bpx = ((byte)(0));
			this.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified;
			this.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None;
			this.imageLoader.HasBpx = true;
			this.imageLoader.HasColorspace = true;
			this.imageLoader.Hll = ((ushort)(0));
			this.imageLoader.Hps = ((ushort)(0));
			this.imageLoader.Location = new System.Drawing.Point(5, 51);
			this.imageLoader.Name = "imageLoader";
			this.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None;
			this.imageLoader.Size = new System.Drawing.Size(311, 389);
			this.imageLoader.Src = "";
			this.imageLoader.TabIndex = 2;
			this.imageLoader.Vll = ((ushort)(0));
			this.imageLoader.Vps = ((ushort)(0));
			// 
			// ANType17RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 490);
			this.Controls.Add(this.imageLoader);
			this.Name = "ANType17RecordCreateForm";
			this.Text = "Add Type-17 ANRecord";
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.imageLoader, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ImageLoaderControl imageLoader;
	}
}
