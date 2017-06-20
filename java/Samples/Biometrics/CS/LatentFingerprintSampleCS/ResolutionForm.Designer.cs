namespace Neurotec.Samples
{
	partial class ResolutionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResolutionForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pbFingerprint = new System.Windows.Forms.PictureBox();
			this.nudVertResolution = new System.Windows.Forms.NumericUpDown();
			this.nudHorzResolution = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbCentimeters = new System.Windows.Forms.RadioButton();
			this.rbInches = new System.Windows.Forms.RadioButton();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.nudUnitScale = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVertResolution)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHorzResolution)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudUnitScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoSize = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(541, 224);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.pbFingerprint);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(537, 220);
			this.panel2.TabIndex = 1;
			// 
			// pbFingerprint
			// 
			this.pbFingerprint.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pbFingerprint.Location = new System.Drawing.Point(0, 0);
			this.pbFingerprint.Name = "pbFingerprint";
			this.pbFingerprint.Size = new System.Drawing.Size(196, 184);
			this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbFingerprint.TabIndex = 0;
			this.pbFingerprint.TabStop = false;
			this.pbFingerprint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbFingerprint_MouseMove);
			this.pbFingerprint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFingerprint_MouseDown);
			this.pbFingerprint.Paint += new System.Windows.Forms.PaintEventHandler(this.pbFingerprint_Paint);
			this.pbFingerprint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFingerprint_MouseUp);
			// 
			// nudVertResolution
			// 
			this.nudVertResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudVertResolution.DecimalPlaces = 2;
			this.nudVertResolution.Location = new System.Drawing.Point(133, 45);
			this.nudVertResolution.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.nudVertResolution.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.nudVertResolution.Name = "nudVertResolution";
			this.nudVertResolution.Size = new System.Drawing.Size(98, 20);
			this.nudVertResolution.TabIndex = 3;
			this.nudVertResolution.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudVertResolution.ValueChanged += new System.EventHandler(this.nudVertResolution_ValueChanged);
			// 
			// nudHorzResolution
			// 
			this.nudHorzResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudHorzResolution.DecimalPlaces = 2;
			this.nudHorzResolution.Location = new System.Drawing.Point(133, 19);
			this.nudHorzResolution.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.nudHorzResolution.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.nudHorzResolution.Name = "nudHorzResolution";
			this.nudHorzResolution.Size = new System.Drawing.Size(98, 20);
			this.nudHorzResolution.TabIndex = 1;
			this.nudHorzResolution.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudHorzResolution.ValueChanged += new System.EventHandler(this.nudHorzResolution_ValueChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Horizontal resolution:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Vertical resolution:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.nudVertResolution);
			this.groupBox1.Controls.Add(this.nudHorzResolution);
			this.groupBox1.Location = new System.Drawing.Point(294, 242);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 79);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Resolution";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.rbCentimeters);
			this.groupBox2.Controls.Add(this.rbInches);
			this.groupBox2.Controls.Add(this.pictureBox2);
			this.groupBox2.Controls.Add(this.nudUnitScale);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(12, 242);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(276, 79);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Measure resolution tool";
			// 
			// rbCentimeters
			// 
			this.rbCentimeters.AutoSize = true;
			this.rbCentimeters.Location = new System.Drawing.Point(143, 48);
			this.rbCentimeters.Name = "rbCentimeters";
			this.rbCentimeters.Size = new System.Drawing.Size(86, 17);
			this.rbCentimeters.TabIndex = 5;
			this.rbCentimeters.Text = "Centimeter(s)";
			this.rbCentimeters.UseVisualStyleBackColor = true;
			// 
			// rbInches
			// 
			this.rbInches.AutoSize = true;
			this.rbInches.Checked = true;
			this.rbInches.Location = new System.Drawing.Point(143, 32);
			this.rbInches.Name = "rbInches";
			this.rbInches.Size = new System.Drawing.Size(63, 17);
			this.rbInches.TabIndex = 4;
			this.rbInches.TabStop = true;
			this.rbInches.Text = "Inch(es)";
			this.rbInches.UseVisualStyleBackColor = true;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Neurotec.Samples.Properties.Resources.measureDpiTool;
			this.pictureBox2.Location = new System.Drawing.Point(9, 32);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(32, 33);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			// 
			// nudUnitScale
			// 
			this.nudUnitScale.Location = new System.Drawing.Point(47, 40);
			this.nudUnitScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudUnitScale.Name = "nudUnitScale";
			this.nudUnitScale.Size = new System.Drawing.Size(80, 20);
			this.nudUnitScale.TabIndex = 1;
			this.nudUnitScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(210, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Draw a line on the image which represents:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(478, 335);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(397, 335);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ResolutionForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(565, 370);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(573, 404);
			this.Name = "ResolutionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Resolution";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudVertResolution)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHorzResolution)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudUnitScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pbFingerprint;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown nudVertResolution;
		private System.Windows.Forms.NumericUpDown nudHorzResolution;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown nudUnitScale;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.RadioButton rbCentimeters;
		private System.Windows.Forms.RadioButton rbInches;
	}
}
