namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType99RecordCreateForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.tbSrc = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tbDataPath = new System.Windows.Forms.TextBox();
			this.btnBrowseData = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.nudBfo = new System.Windows.Forms.NumericUpDown();
			this.nudBft = new System.Windows.Forms.NumericUpDown();
			this.dataOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cbVersion = new System.Windows.Forms.ComboBox();
			this.chlbBiometricType = new System.Windows.Forms.CheckedListBox();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBft)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(208, 305);
			this.btnOk.TabIndex = 15;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(289, 305);
			this.btnCancel.TabIndex = 16;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Source agency:";
			// 
			// tbSrc
			// 
			this.tbSrc.Location = new System.Drawing.Point(135, 59);
			this.tbSrc.Name = "tbSrc";
			this.tbSrc.Size = new System.Drawing.Size(216, 20);
			this.tbSrc.TabIndex = 3;
			this.tbSrc.Validating += new System.ComponentModel.CancelEventHandler(this.TbSrcValidating);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Version:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 117);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Biometric type:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 226);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(117, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Biometric format owner:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 252);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Biometric format type:";
			// 
			// tbDataPath
			// 
			this.tbDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDataPath.Location = new System.Drawing.Point(95, 276);
			this.tbDataPath.Name = "tbDataPath";
			this.tbDataPath.Size = new System.Drawing.Size(191, 20);
			this.tbDataPath.TabIndex = 13;
			// 
			// btnBrowseData
			// 
			this.btnBrowseData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseData.Location = new System.Drawing.Point(289, 273);
			this.btnBrowseData.Name = "btnBrowseData";
			this.btnBrowseData.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseData.TabIndex = 14;
			this.btnBrowseData.Text = "Browse...";
			this.btnBrowseData.UseVisualStyleBackColor = true;
			this.btnBrowseData.Click += new System.EventHandler(this.BtnBrowseDataClick);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 279);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Biometric data:";
			// 
			// nudBfo
			// 
			this.nudBfo.Location = new System.Drawing.Point(135, 224);
			this.nudBfo.Name = "nudBfo";
			this.nudBfo.Size = new System.Drawing.Size(75, 20);
			this.nudBfo.TabIndex = 9;
			// 
			// nudBft
			// 
			this.nudBft.Location = new System.Drawing.Point(135, 250);
			this.nudBft.Name = "nudBft";
			this.nudBft.Size = new System.Drawing.Size(75, 20);
			this.nudBft.TabIndex = 11;
			// 
			// dataOpenFileDialog
			// 
			this.dataOpenFileDialog.Filter = "All Files (*.*)|*.*";
			// 
			// cbVersion
			// 
			this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVersion.FormattingEnabled = true;
			this.cbVersion.Location = new System.Drawing.Point(135, 85);
			this.cbVersion.Name = "cbVersion";
			this.cbVersion.Size = new System.Drawing.Size(216, 21);
			this.cbVersion.TabIndex = 5;
			// 
			// chlbBiometricType
			// 
			this.chlbBiometricType.CheckOnClick = true;
			this.chlbBiometricType.FormattingEnabled = true;
			this.chlbBiometricType.Location = new System.Drawing.Point(135, 117);
			this.chlbBiometricType.Name = "chlbBiometricType";
			this.chlbBiometricType.Size = new System.Drawing.Size(216, 94);
			this.chlbBiometricType.TabIndex = 18;
			this.chlbBiometricType.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChlbBiometricTypeImteCheck);
			// 
			// ANType99RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 338);
			this.Controls.Add(this.chlbBiometricType);
			this.Controls.Add(this.cbVersion);
			this.Controls.Add(this.nudBft);
			this.Controls.Add(this.nudBfo);
			this.Controls.Add(this.tbDataPath);
			this.Controls.Add(this.btnBrowseData);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbSrc);
			this.Name = "ANType99RecordCreateForm";
			this.Text = "Add Type-99 ANRecord";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.tbSrc, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.Controls.SetChildIndex(this.btnBrowseData, 0);
			this.Controls.SetChildIndex(this.tbDataPath, 0);
			this.Controls.SetChildIndex(this.nudBfo, 0);
			this.Controls.SetChildIndex(this.nudBft, 0);
			this.Controls.SetChildIndex(this.cbVersion, 0);
			this.Controls.SetChildIndex(this.chlbBiometricType, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBft)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbSrc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbDataPath;
		private System.Windows.Forms.Button btnBrowseData;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudBfo;
		private System.Windows.Forms.NumericUpDown nudBft;
		private System.Windows.Forms.OpenFileDialog dataOpenFileDialog;
		private System.Windows.Forms.ComboBox cbVersion;
		private System.Windows.Forms.CheckedListBox chlbBiometricType;
	}
}
