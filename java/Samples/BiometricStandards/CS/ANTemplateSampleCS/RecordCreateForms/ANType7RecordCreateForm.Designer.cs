namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType7RecordCreateForm
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
			this.isrResolutionEditBox = new Neurotec.Samples.RecordCreateForms.ResolutionEditBox();
			this.label2 = new System.Windows.Forms.Label();
			this.irResolutionEditBox = new Neurotec.Samples.RecordCreateForms.ResolutionEditBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbImageDatePath = new System.Windows.Forms.TextBox();
			this.btnBrowseImageData = new System.Windows.Forms.Button();
			this.imageDataOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(125, 215);
			this.btnOk.TabIndex = 9;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(206, 215);
			this.btnCancel.TabIndex = 10;
			// 
			// isrResolutionEditBox
			// 
			this.isrResolutionEditBox.Location = new System.Drawing.Point(12, 70);
			this.isrResolutionEditBox.Name = "isrResolutionEditBox";
			this.isrResolutionEditBox.PpcmValue = 0;
			this.isrResolutionEditBox.PpiValue = 0;
			this.isrResolutionEditBox.PpmmValue = 0;
			this.isrResolutionEditBox.PpmValue = 0;
			this.isrResolutionEditBox.Size = new System.Drawing.Size(213, 30);
			this.isrResolutionEditBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Image scanning resolution:";
			// 
			// irResolutionEditBox
			// 
			this.irResolutionEditBox.Location = new System.Drawing.Point(12, 117);
			this.irResolutionEditBox.Name = "irResolutionEditBox";
			this.irResolutionEditBox.PpcmValue = 0;
			this.irResolutionEditBox.PpiValue = 0;
			this.irResolutionEditBox.PpmmValue = 0;
			this.irResolutionEditBox.PpmValue = 0;
			this.irResolutionEditBox.Size = new System.Drawing.Size(213, 30);
			this.irResolutionEditBox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Native resolution:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Image data:";
			// 
			// tbImageDatePath
			// 
			this.tbImageDatePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbImageDatePath.Location = new System.Drawing.Point(12, 166);
			this.tbImageDatePath.Name = "tbImageDatePath";
			this.tbImageDatePath.Size = new System.Drawing.Size(186, 20);
			this.tbImageDatePath.TabIndex = 7;
			// 
			// btnBrowseImageData
			// 
			this.btnBrowseImageData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseImageData.Location = new System.Drawing.Point(204, 164);
			this.btnBrowseImageData.Name = "btnBrowseImageData";
			this.btnBrowseImageData.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseImageData.TabIndex = 8;
			this.btnBrowseImageData.Text = "Browse...";
			this.btnBrowseImageData.UseVisualStyleBackColor = true;
			this.btnBrowseImageData.Click += new System.EventHandler(this.BtnImageDataClick);
			// 
			// imageDataOpenFileDialog
			// 
			this.imageDataOpenFileDialog.Filter = "All Files (*.*)|*.*";
			// 
			// ANType7RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(291, 248);
			this.Controls.Add(this.tbImageDatePath);
			this.Controls.Add(this.btnBrowseImageData);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.irResolutionEditBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.isrResolutionEditBox);
			this.Controls.Add(this.label2);
			this.Name = "ANType7RecordCreateForm";
			this.Text = "Add Type-7 ANRecord";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.isrResolutionEditBox, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.irResolutionEditBox, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.btnBrowseImageData, 0);
			this.Controls.SetChildIndex(this.tbImageDatePath, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ResolutionEditBox isrResolutionEditBox;
		private System.Windows.Forms.Label label2;
		private ResolutionEditBox irResolutionEditBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbImageDatePath;
		private System.Windows.Forms.Button btnBrowseImageData;
		private System.Windows.Forms.OpenFileDialog imageDataOpenFileDialog;
	}
}
