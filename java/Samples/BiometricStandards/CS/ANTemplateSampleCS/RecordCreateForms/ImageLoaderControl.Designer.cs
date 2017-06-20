namespace Neurotec.Samples.RecordCreateForms
{
	partial class ImageLoaderControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.rbFromData = new System.Windows.Forms.RadioButton();
			this.rbFromImage = new System.Windows.Forms.RadioButton();
			this.panelFromImage = new System.Windows.Forms.Panel();
			this.tbImagePath = new System.Windows.Forms.TextBox();
			this.btnBrowseImage = new System.Windows.Forms.Button();
			this.imageDataOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.panelFromData = new System.Windows.Forms.Panel();
			this.colorspaceLabel = new System.Windows.Forms.Label();
			this.cbColorSpace = new System.Windows.Forms.ComboBox();
			this.nudBpx = new System.Windows.Forms.NumericUpDown();
			this.bpxLabel = new System.Windows.Forms.Label();
			this.nudVps = new System.Windows.Forms.NumericUpDown();
			this.nudHps = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.nudVll = new System.Windows.Forms.NumericUpDown();
			this.nudHll = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnBrowseImageData = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.imageDataPathTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbScaleUnits = new System.Windows.Forms.ComboBox();
			this.cbCompressionAlgorithm = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbSrc = new System.Windows.Forms.TextBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.panelFromImage.SuspendLayout();
			this.panelFromData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBpx)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVps)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHps)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// rbFromData
			// 
			this.rbFromData.AutoSize = true;
			this.rbFromData.Location = new System.Drawing.Point(6, 153);
			this.rbFromData.Name = "rbFromData";
			this.rbFromData.Size = new System.Drawing.Size(75, 17);
			this.rbFromData.TabIndex = 8;
			this.rbFromData.Text = "From data:";
			this.rbFromData.UseVisualStyleBackColor = true;
			this.rbFromData.CheckedChanged += new System.EventHandler(this.RbFromDataCheckedChanged);
			// 
			// rbFromImage
			// 
			this.rbFromImage.AutoSize = true;
			this.rbFromImage.Checked = true;
			this.rbFromImage.Location = new System.Drawing.Point(6, 87);
			this.rbFromImage.Name = "rbFromImage";
			this.rbFromImage.Size = new System.Drawing.Size(82, 17);
			this.rbFromImage.TabIndex = 6;
			this.rbFromImage.TabStop = true;
			this.rbFromImage.Text = "From image:";
			this.rbFromImage.UseVisualStyleBackColor = true;
			this.rbFromImage.CheckedChanged += new System.EventHandler(this.RbFomImageCheckedChanged);
			// 
			// panelFromImage
			// 
			this.panelFromImage.Controls.Add(this.tbImagePath);
			this.panelFromImage.Controls.Add(this.btnBrowseImage);
			this.panelFromImage.Location = new System.Drawing.Point(6, 110);
			this.panelFromImage.Name = "panelFromImage";
			this.panelFromImage.Size = new System.Drawing.Size(298, 37);
			this.panelFromImage.TabIndex = 7;
			// 
			// tbImagePath
			// 
			this.tbImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbImagePath.Location = new System.Drawing.Point(5, 3);
			this.tbImagePath.Name = "tbImagePath";
			this.tbImagePath.Size = new System.Drawing.Size(209, 20);
			this.tbImagePath.TabIndex = 0;
			// 
			// btnBrowseImage
			// 
			this.btnBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImage.Location = new System.Drawing.Point(220, 1);
			this.btnBrowseImage.Name = "btnBrowseImage";
			this.btnBrowseImage.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseImage.TabIndex = 1;
			this.btnBrowseImage.Text = "Browse...";
			this.btnBrowseImage.UseVisualStyleBackColor = true;
			this.btnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImageClick);
			// 
			// imageDataOpenFileDialog
			// 
			this.imageDataOpenFileDialog.Filter = "All Files (*.*)|*.*";
			// 
			// imageOpenFileDialog
			// 
			this.imageOpenFileDialog.Filter = "All Files (*.*)|*.*";
			this.imageOpenFileDialog.Title = "Open Image";
			// 
			// panelFromData
			// 
			this.panelFromData.Controls.Add(this.colorspaceLabel);
			this.panelFromData.Controls.Add(this.cbColorSpace);
			this.panelFromData.Controls.Add(this.nudBpx);
			this.panelFromData.Controls.Add(this.bpxLabel);
			this.panelFromData.Controls.Add(this.nudVps);
			this.panelFromData.Controls.Add(this.nudHps);
			this.panelFromData.Controls.Add(this.label3);
			this.panelFromData.Controls.Add(this.label4);
			this.panelFromData.Controls.Add(this.nudVll);
			this.panelFromData.Controls.Add(this.nudHll);
			this.panelFromData.Controls.Add(this.label5);
			this.panelFromData.Controls.Add(this.label6);
			this.panelFromData.Controls.Add(this.btnBrowseImageData);
			this.panelFromData.Controls.Add(this.label8);
			this.panelFromData.Controls.Add(this.imageDataPathTextBox);
			this.panelFromData.Location = new System.Drawing.Point(6, 176);
			this.panelFromData.Name = "panelFromData";
			this.panelFromData.Size = new System.Drawing.Size(298, 206);
			this.panelFromData.TabIndex = 9;
			// 
			// colorspaceLabel
			// 
			this.colorspaceLabel.AutoSize = true;
			this.colorspaceLabel.Location = new System.Drawing.Point(5, 138);
			this.colorspaceLabel.Name = "colorspaceLabel";
			this.colorspaceLabel.Size = new System.Drawing.Size(63, 13);
			this.colorspaceLabel.TabIndex = 10;
			this.colorspaceLabel.Text = "Colorspace:";
			// 
			// cbColorSpace
			// 
			this.cbColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbColorSpace.FormattingEnabled = true;
			this.cbColorSpace.Location = new System.Drawing.Point(126, 135);
			this.cbColorSpace.Name = "cbColorSpace";
			this.cbColorSpace.Size = new System.Drawing.Size(169, 21);
			this.cbColorSpace.TabIndex = 11;
			// 
			// nudBpx
			// 
			this.nudBpx.Location = new System.Drawing.Point(126, 109);
			this.nudBpx.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudBpx.Name = "nudBpx";
			this.nudBpx.Size = new System.Drawing.Size(100, 20);
			this.nudBpx.TabIndex = 9;
			// 
			// bpxLabel
			// 
			this.bpxLabel.AutoSize = true;
			this.bpxLabel.Location = new System.Drawing.Point(5, 112);
			this.bpxLabel.Name = "bpxLabel";
			this.bpxLabel.Size = new System.Drawing.Size(69, 13);
			this.bpxLabel.TabIndex = 8;
			this.bpxLabel.Text = "Bits per pixel:";
			// 
			// nudVps
			// 
			this.nudVps.Location = new System.Drawing.Point(126, 83);
			this.nudVps.Name = "nudVps";
			this.nudVps.Size = new System.Drawing.Size(100, 20);
			this.nudVps.TabIndex = 7;
			// 
			// nudHps
			// 
			this.nudHps.Location = new System.Drawing.Point(126, 57);
			this.nudHps.Name = "nudHps";
			this.nudHps.Size = new System.Drawing.Size(100, 20);
			this.nudHps.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(109, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Horizontal pixel scale:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(97, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Vertical pixel scale:";
			// 
			// nudVll
			// 
			this.nudVll.Location = new System.Drawing.Point(126, 32);
			this.nudVll.Name = "nudVll";
			this.nudVll.Size = new System.Drawing.Size(100, 20);
			this.nudVll.TabIndex = 3;
			// 
			// nudHll
			// 
			this.nudHll.Location = new System.Drawing.Point(126, 6);
			this.nudHll.Name = "nudHll";
			this.nudHll.Size = new System.Drawing.Size(100, 20);
			this.nudHll.TabIndex = 1;
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
			// btnBrowseImageData
			// 
			this.btnBrowseImageData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImageData.Location = new System.Drawing.Point(220, 178);
			this.btnBrowseImageData.Name = "btnBrowseImageData";
			this.btnBrowseImageData.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseImageData.TabIndex = 14;
			this.btnBrowseImageData.Text = "Browse...";
			this.btnBrowseImageData.UseVisualStyleBackColor = true;
			this.btnBrowseImageData.Click += new System.EventHandler(this.BtnBrowseImageDataClick);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 164);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(79, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "Image data file:";
			// 
			// imageDataPathTextBox
			// 
			this.imageDataPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.imageDataPathTextBox.Location = new System.Drawing.Point(8, 180);
			this.imageDataPathTextBox.Name = "imageDataPathTextBox";
			this.imageDataPathTextBox.Size = new System.Drawing.Size(206, 20);
			this.imageDataPathTextBox.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Scale units:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(115, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Compression algorithm:";
			// 
			// cbScaleUnits
			// 
			this.cbScaleUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbScaleUnits.FormattingEnabled = true;
			this.cbScaleUnits.Location = new System.Drawing.Point(129, 27);
			this.cbScaleUnits.Name = "cbScaleUnits";
			this.cbScaleUnits.Size = new System.Drawing.Size(172, 21);
			this.cbScaleUnits.TabIndex = 3;
			// 
			// cbCompressionAlgorithm
			// 
			this.cbCompressionAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCompressionAlgorithm.FormattingEnabled = true;
			this.cbCompressionAlgorithm.Location = new System.Drawing.Point(129, 54);
			this.cbCompressionAlgorithm.Name = "cbCompressionAlgorithm";
			this.cbCompressionAlgorithm.Size = new System.Drawing.Size(172, 21);
			this.cbCompressionAlgorithm.TabIndex = 5;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 4);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(82, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Source agency:";
			// 
			// tbSrc
			// 
			this.tbSrc.Location = new System.Drawing.Point(129, 1);
			this.tbSrc.Name = "tbSrc";
			this.tbSrc.Size = new System.Drawing.Size(172, 20);
			this.tbSrc.TabIndex = 1;
			this.tbSrc.Validating += new System.ComponentModel.CancelEventHandler(this.TbSrcValidating);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ImageLoaderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tbSrc);
			this.Controls.Add(this.cbCompressionAlgorithm);
			this.Controls.Add(this.cbScaleUnits);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panelFromData);
			this.Controls.Add(this.panelFromImage);
			this.Controls.Add(this.rbFromImage);
			this.Controls.Add(this.rbFromData);
			this.Name = "ImageLoaderControl";
			this.Size = new System.Drawing.Size(311, 387);
			this.panelFromImage.ResumeLayout(false);
			this.panelFromImage.PerformLayout();
			this.panelFromData.ResumeLayout(false);
			this.panelFromData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBpx)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVps)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHps)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbFromData;
		private System.Windows.Forms.RadioButton rbFromImage;
		private System.Windows.Forms.Panel panelFromImage;
		private System.Windows.Forms.TextBox tbImagePath;
		private System.Windows.Forms.Button btnBrowseImage;
		private System.Windows.Forms.OpenFileDialog imageDataOpenFileDialog;
		private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
		private System.Windows.Forms.Panel panelFromData;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnBrowseImageData;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox imageDataPathTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbScaleUnits;
		private System.Windows.Forms.ComboBox cbCompressionAlgorithm;
		private System.Windows.Forms.NumericUpDown nudVps;
		private System.Windows.Forms.NumericUpDown nudHps;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nudVll;
		private System.Windows.Forms.NumericUpDown nudHll;
		protected System.Windows.Forms.Label colorspaceLabel;
		protected System.Windows.Forms.ComboBox cbColorSpace;
		protected System.Windows.Forms.NumericUpDown nudBpx;
		protected System.Windows.Forms.Label bpxLabel;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbSrc;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}
