namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANImageBinaryRecordCreateForm
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
			this.chbIsrFlag = new System.Windows.Forms.CheckBox();
			this.irResolutionEditBox = new Neurotec.Samples.RecordCreateForms.ResolutionEditBox();
			this.label3 = new System.Windows.Forms.Label();
			this.isrResolutionEditBox = new Neurotec.Samples.RecordCreateForms.ResolutionEditBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cbCompressionAlgorithm = new System.Windows.Forms.ComboBox();
			this.rbFromImage = new System.Windows.Forms.RadioButton();
			this.tbImagePath = new System.Windows.Forms.TextBox();
			this.btnBrowseImage = new System.Windows.Forms.Button();
			this.rbFromData = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tbHll = new System.Windows.Forms.TextBox();
			this.tbVll = new System.Windows.Forms.TextBox();
			this.tbVendorCa = new System.Windows.Forms.TextBox();
			this.vendorCaLabel = new System.Windows.Forms.Label();
			this.tbImageDataPath = new System.Windows.Forms.TextBox();
			this.btnBrowseImageData = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.panelFromImage = new System.Windows.Forms.Panel();
			this.panelFromData = new System.Windows.Forms.Panel();
			this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.panelFromImage.SuspendLayout();
			this.panelFromData.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(159, 436);
			this.btnOk.TabIndex = 13;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(240, 436);
			this.btnCancel.TabIndex = 14;
			// 
			// chbIsrFlag
			// 
			this.chbIsrFlag.AutoSize = true;
			this.chbIsrFlag.Location = new System.Drawing.Point(12, 64);
			this.chbIsrFlag.Name = "chbIsrFlag";
			this.chbIsrFlag.Size = new System.Drawing.Size(169, 17);
			this.chbIsrFlag.TabIndex = 4;
			this.chbIsrFlag.Text = "Image scanning resolution flag";
			this.chbIsrFlag.UseVisualStyleBackColor = true;
			// 
			// irResolutionEditBox
			// 
			this.irResolutionEditBox.Location = new System.Drawing.Point(127, 56);
			this.irResolutionEditBox.Name = "irResolutionEditBox";
			this.irResolutionEditBox.PpcmValue = 0;
			this.irResolutionEditBox.PpiValue = 0;
			this.irResolutionEditBox.PpmmValue = 0;
			this.irResolutionEditBox.PpmValue = 0;
			this.irResolutionEditBox.Size = new System.Drawing.Size(148, 30);
			this.irResolutionEditBox.TabIndex = 5;
			this.irResolutionEditBox.Validating += new System.ComponentModel.CancelEventHandler(this.NativeScanningResolutionEditBoxValidating);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Image resolution:";
			// 
			// isrResolutionEditBox
			// 
			this.isrResolutionEditBox.Location = new System.Drawing.Point(15, 104);
			this.isrResolutionEditBox.Name = "isrResolutionEditBox";
			this.isrResolutionEditBox.PpcmValue = 0;
			this.isrResolutionEditBox.PpiValue = 0;
			this.isrResolutionEditBox.PpmmValue = 0;
			this.isrResolutionEditBox.PpmValue = 0;
			this.isrResolutionEditBox.Size = new System.Drawing.Size(145, 30);
			this.isrResolutionEditBox.TabIndex = 6;
			this.isrResolutionEditBox.Validating += new System.ComponentModel.CancelEventHandler(this.ImageScanningResolutionEditBoxValidating);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Image scanning resolution:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 137);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(115, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Compression algorithm:";
			// 
			// cbCompressionAlgorithm
			// 
			this.cbCompressionAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCompressionAlgorithm.FormattingEnabled = true;
			this.cbCompressionAlgorithm.Location = new System.Drawing.Point(138, 134);
			this.cbCompressionAlgorithm.Name = "cbCompressionAlgorithm";
			this.cbCompressionAlgorithm.Size = new System.Drawing.Size(172, 21);
			this.cbCompressionAlgorithm.TabIndex = 8;
			this.cbCompressionAlgorithm.SelectedIndexChanged += new System.EventHandler(this.CbCompressionAlgorithmSelectedIndexChanged);
			// 
			// rbFromImage
			// 
			this.rbFromImage.AutoSize = true;
			this.rbFromImage.Checked = true;
			this.rbFromImage.Location = new System.Drawing.Point(12, 162);
			this.rbFromImage.Name = "rbFromImage";
			this.rbFromImage.Size = new System.Drawing.Size(82, 17);
			this.rbFromImage.TabIndex = 9;
			this.rbFromImage.TabStop = true;
			this.rbFromImage.Text = "From image:";
			this.rbFromImage.UseVisualStyleBackColor = true;
			this.rbFromImage.CheckedChanged += new System.EventHandler(this.RbFromImageCheckedChanged);
			// 
			// tbImagePath
			// 
			this.tbImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbImagePath.Location = new System.Drawing.Point(5, 3);
			this.tbImagePath.Name = "tbImagePath";
			this.tbImagePath.Size = new System.Drawing.Size(212, 20);
			this.tbImagePath.TabIndex = 0;
			// 
			// btnBrowseImage
			// 
			this.btnBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImage.Location = new System.Drawing.Point(223, 0);
			this.btnBrowseImage.Name = "btnBrowseImage";
			this.btnBrowseImage.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseImage.TabIndex = 1;
			this.btnBrowseImage.Text = "Browse...";
			this.btnBrowseImage.UseVisualStyleBackColor = true;
			this.btnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImageClick);
			// 
			// rbFromData
			// 
			this.rbFromData.AutoSize = true;
			this.rbFromData.Location = new System.Drawing.Point(12, 228);
			this.rbFromData.Name = "rbFromData";
			this.rbFromData.Size = new System.Drawing.Size(75, 17);
			this.rbFromData.TabIndex = 11;
			this.rbFromData.Text = "From data:";
			this.rbFromData.UseVisualStyleBackColor = true;
			this.rbFromData.CheckedChanged += new System.EventHandler(this.RbFromDataCheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(108, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Horizontal line length:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 35);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Vertical line length:";
			// 
			// tbHll
			// 
			this.tbHll.Location = new System.Drawing.Point(126, 6);
			this.tbHll.Name = "tbHll";
			this.tbHll.Size = new System.Drawing.Size(100, 20);
			this.tbHll.TabIndex = 1;
			this.tbHll.Text = "0";
			this.tbHll.Validating += new System.ComponentModel.CancelEventHandler(this.TbHllValidating);
			// 
			// tbVll
			// 
			this.tbVll.Location = new System.Drawing.Point(126, 32);
			this.tbVll.Name = "tbVll";
			this.tbVll.Size = new System.Drawing.Size(100, 20);
			this.tbVll.TabIndex = 3;
			this.tbVll.Text = "0";
			this.tbVll.Validating += new System.ComponentModel.CancelEventHandler(this.TbVllValidating);
			// 
			// tbVendorCa
			// 
			this.tbVendorCa.Location = new System.Drawing.Point(126, 86);
			this.tbVendorCa.Name = "tbVendorCa";
			this.tbVendorCa.Size = new System.Drawing.Size(100, 20);
			this.tbVendorCa.TabIndex = 7;
			this.tbVendorCa.Text = "0";
			this.tbVendorCa.Validating += new System.ComponentModel.CancelEventHandler(this.TbVendorCaValidating);
			// 
			// vendorCaLabel
			// 
			this.vendorCaLabel.AutoSize = true;
			this.vendorCaLabel.Location = new System.Drawing.Point(5, 89);
			this.vendorCaLabel.Name = "vendorCaLabel";
			this.vendorCaLabel.Size = new System.Drawing.Size(61, 13);
			this.vendorCaLabel.TabIndex = 6;
			this.vendorCaLabel.Text = "Vendor CA:";
			// 
			// tbImageDataPath
			// 
			this.tbImageDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbImageDataPath.Location = new System.Drawing.Point(8, 128);
			this.tbImageDataPath.Name = "tbImageDataPath";
			this.tbImageDataPath.Size = new System.Drawing.Size(209, 20);
			this.tbImageDataPath.TabIndex = 9;
			// 
			// btnBrowseImageData
			// 
			this.btnBrowseImageData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImageData.Location = new System.Drawing.Point(223, 128);
			this.btnBrowseImageData.Name = "btnBrowseImageData";
			this.btnBrowseImageData.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseImageData.TabIndex = 10;
			this.btnBrowseImageData.Text = "Browse...";
			this.btnBrowseImageData.UseVisualStyleBackColor = true;
			this.btnBrowseImageData.Click += new System.EventHandler(this.BrowseImageDataClick);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 112);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 13);
			this.label8.TabIndex = 8;
			this.label8.Text = "Image data file:";
			// 
			// panelFromImage
			// 
			this.panelFromImage.Controls.Add(this.tbImagePath);
			this.panelFromImage.Controls.Add(this.btnBrowseImage);
			this.panelFromImage.Location = new System.Drawing.Point(12, 185);
			this.panelFromImage.Name = "panelFromImage";
			this.panelFromImage.Size = new System.Drawing.Size(301, 37);
			this.panelFromImage.TabIndex = 10;
			// 
			// panelFromData
			// 
			this.panelFromData.Controls.Add(this.label5);
			this.panelFromData.Controls.Add(this.label3);
			this.panelFromData.Controls.Add(this.irResolutionEditBox);
			this.panelFromData.Controls.Add(this.label6);
			this.panelFromData.Controls.Add(this.btnBrowseImageData);
			this.panelFromData.Controls.Add(this.tbHll);
			this.panelFromData.Controls.Add(this.label8);
			this.panelFromData.Controls.Add(this.tbVll);
			this.panelFromData.Controls.Add(this.tbImageDataPath);
			this.panelFromData.Controls.Add(this.vendorCaLabel);
			this.panelFromData.Controls.Add(this.tbVendorCa);
			this.panelFromData.Location = new System.Drawing.Point(12, 251);
			this.panelFromData.Name = "panelFromData";
			this.panelFromData.Size = new System.Drawing.Size(301, 167);
			this.panelFromData.TabIndex = 12;
			// 
			// imageOpenFileDialog
			// 
			this.imageOpenFileDialog.Filter = "All Files (*.*)|*.*";
			this.imageOpenFileDialog.Title = "Open Image";
			// 
			// ANImageBinaryRecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 469);
			this.Controls.Add(this.panelFromImage);
			this.Controls.Add(this.rbFromImage);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panelFromData);
			this.Controls.Add(this.isrResolutionEditBox);
			this.Controls.Add(this.rbFromData);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chbIsrFlag);
			this.Controls.Add(this.cbCompressionAlgorithm);
			this.Name = "ANImageBinaryRecordCreateForm";
			this.Text = "ANImageBinaryRecordCreateForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ANImageBinaryRecordCreateFormClosing);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.cbCompressionAlgorithm, 0);
			this.Controls.SetChildIndex(this.chbIsrFlag, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.rbFromData, 0);
			this.Controls.SetChildIndex(this.isrResolutionEditBox, 0);
			this.Controls.SetChildIndex(this.panelFromData, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.rbFromImage, 0);
			this.Controls.SetChildIndex(this.panelFromImage, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.panelFromImage.ResumeLayout(false);
			this.panelFromImage.PerformLayout();
			this.panelFromData.ResumeLayout(false);
			this.panelFromData.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chbIsrFlag;
		private ResolutionEditBox irResolutionEditBox;
		private System.Windows.Forms.Label label3;
		private ResolutionEditBox isrResolutionEditBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbImagePath;
		private System.Windows.Forms.Button btnBrowseImage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbHll;
		private System.Windows.Forms.TextBox tbVll;
		private System.Windows.Forms.TextBox tbImageDataPath;
		private System.Windows.Forms.Button btnBrowseImageData;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
		protected System.Windows.Forms.TextBox tbVendorCa;
		protected System.Windows.Forms.Label vendorCaLabel;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.RadioButton rbFromImage;
		protected System.Windows.Forms.RadioButton rbFromData;
		protected System.Windows.Forms.Panel panelFromImage;
		protected System.Windows.Forms.Panel panelFromData;
		private System.Windows.Forms.ComboBox cbCompressionAlgorithm;
	}
}
