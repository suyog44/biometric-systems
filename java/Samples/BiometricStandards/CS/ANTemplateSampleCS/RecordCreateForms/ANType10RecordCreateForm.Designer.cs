namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType10RecordCreateForm
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
			this.imageTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.smtTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(149, 517);
			this.btnOk.TabIndex = 7;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(230, 517);
			this.btnCancel.TabIndex = 8;
			// 
			// imageLoader
			// 
			this.imageLoader.Bpx = ((byte)(0));
			this.imageLoader.Colorspace = Neurotec.Biometrics.Standards.ANImageColorSpace.Unspecified;
			this.imageLoader.CompressionAlgorithm = Neurotec.Biometrics.Standards.ANImageCompressionAlgorithm.None;
			this.imageLoader.HasBpx = false;
			this.imageLoader.HasColorspace = true;
			this.imageLoader.Hll = ((ushort)(0));
			this.imageLoader.Hps = ((ushort)(0));
			this.imageLoader.Location = new System.Drawing.Point(-2, 111);
			this.imageLoader.Name = "imageLoader";
			this.imageLoader.ScaleUnits = Neurotec.Biometrics.Standards.BdifScaleUnits.None;
			this.imageLoader.Size = new System.Drawing.Size(311, 389);
			this.imageLoader.Src = "";
			this.imageLoader.TabIndex = 6;
			this.imageLoader.Vll = ((ushort)(0));
			this.imageLoader.Vps = ((ushort)(0));
			// 
			// imageTypeComboBox
			// 
			this.imageTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.imageTypeComboBox.FormattingEnabled = true;
			this.imageTypeComboBox.Location = new System.Drawing.Point(127, 58);
			this.imageTypeComboBox.Name = "imageTypeComboBox";
			this.imageTypeComboBox.Size = new System.Drawing.Size(172, 21);
			this.imageTypeComboBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Image type:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 88);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(28, 13);
			this.label10.TabIndex = 4;
			this.label10.Text = "Smt:";
			// 
			// smtTextBox
			// 
			this.smtTextBox.Location = new System.Drawing.Point(127, 85);
			this.smtTextBox.Name = "smtTextBox";
			this.smtTextBox.Size = new System.Drawing.Size(172, 20);
			this.smtTextBox.TabIndex = 5;
			// 
			// ANType10RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(315, 550);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.smtTextBox);
			this.Controls.Add(this.imageTypeComboBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.imageLoader);
			this.Name = "ANType10RecordCreateForm";
			this.Text = "Add Type-10 ANRecord";
			this.Controls.SetChildIndex(this.imageLoader, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.imageTypeComboBox, 0);
			this.Controls.SetChildIndex(this.smtTextBox, 0);
			this.Controls.SetChildIndex(this.label10, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ImageLoaderControl imageLoader;
		private System.Windows.Forms.ComboBox imageTypeComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox smtTextBox;
	}
}
