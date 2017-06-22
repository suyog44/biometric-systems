namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType16RecordCreateForm
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
			this.label10 = new System.Windows.Forms.Label();
			this.tbUdi = new System.Windows.Forms.TextBox();
			this.imageLoader = new Neurotec.Samples.RecordCreateForms.ImageLoaderControl();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(149, 480);
			this.btnOk.TabIndex = 5;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(230, 480);
			this.btnCancel.TabIndex = 6;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(10, 57);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(86, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "User image type:";
			// 
			// tbUit
			// 
			this.tbUdi.Location = new System.Drawing.Point(131, 54);
			this.tbUdi.Name = "tbUit";
			this.tbUdi.Size = new System.Drawing.Size(172, 20);
			this.tbUdi.TabIndex = 3;
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
			this.imageLoader.Location = new System.Drawing.Point(2, 80);
			this.imageLoader.Name = "imageLoader";
			this.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None;
			this.imageLoader.Size = new System.Drawing.Size(311, 389);
			this.imageLoader.Src = "";
			this.imageLoader.TabIndex = 4;
			this.imageLoader.Vll = ((ushort)(0));
			this.imageLoader.Vps = ((ushort)(0));
			// 
			// ANType16RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(315, 513);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tbUdi);
			this.Controls.Add(this.imageLoader);
			this.Name = "ANType16RecordCreateForm";
			this.Text = "Add Type-16 ANRecord";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.imageLoader, 0);
			this.Controls.SetChildIndex(this.tbUdi, 0);
			this.Controls.SetChildIndex(this.label10, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbUdi;
		private ImageLoaderControl imageLoader;
	}
}
