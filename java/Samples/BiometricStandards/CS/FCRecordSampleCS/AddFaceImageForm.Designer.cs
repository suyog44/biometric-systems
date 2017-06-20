namespace Neurotec.Samples
{
	partial class AddFaceImageForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFaceImageForm));
			this.gbMain = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbImageDataType = new System.Windows.Forms.ComboBox();
			this.cbFaceImageType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbMain
			// 
			this.gbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbMain.Controls.Add(this.label3);
			this.gbMain.Controls.Add(this.cbImageDataType);
			this.gbMain.Controls.Add(this.cbFaceImageType);
			this.gbMain.Controls.Add(this.label2);
			this.gbMain.Location = new System.Drawing.Point(12, 4);
			this.gbMain.Name = "gbMain";
			this.gbMain.Size = new System.Drawing.Size(263, 101);
			this.gbMain.TabIndex = 0;
			this.gbMain.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Face image data type:";
			// 
			// cbImageDataType
			// 
			this.cbImageDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImageDataType.FormattingEnabled = true;
			this.cbImageDataType.Location = new System.Drawing.Point(152, 49);
			this.cbImageDataType.Name = "cbImageDataType";
			this.cbImageDataType.Size = new System.Drawing.Size(105, 21);
			this.cbImageDataType.TabIndex = 4;
			// 
			// cbFaceImageType
			// 
			this.cbFaceImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFaceImageType.FormattingEnabled = true;
			this.cbFaceImageType.Location = new System.Drawing.Point(152, 25);
			this.cbFaceImageType.Name = "cbFaceImageType";
			this.cbFaceImageType.Size = new System.Drawing.Size(105, 21);
			this.cbFaceImageType.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Face image type:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(119, 114);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(200, 114);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// AddFaceImageForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(287, 149);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddFaceImageForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add face from image";
			this.gbMain.ResumeLayout(false);
			this.gbMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Button btnOk;
		protected System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		protected System.Windows.Forms.GroupBox gbMain;
		protected System.Windows.Forms.ComboBox cbFaceImageType;
		protected System.Windows.Forms.ComboBox cbImageDataType;
	}
}
