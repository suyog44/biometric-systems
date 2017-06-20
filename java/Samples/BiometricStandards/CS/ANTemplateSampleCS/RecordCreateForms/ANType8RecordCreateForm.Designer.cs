namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType8RecordCreateForm
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
			this.rbFromVectors = new System.Windows.Forms.RadioButton();
			this.fromVectorsPanel = new System.Windows.Forms.Panel();
			this.btnEditVectors = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.resolutioEditBox = new Neurotec.Samples.RecordCreateForms.ResolutionEditBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbSignature = new System.Windows.Forms.ComboBox();
			this.panelFromData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.fromVectorsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbVendorCa
			// 
			this.tbVendorCa.Enabled = false;
			// 
			// vendorCaLabel
			// 
			this.vendorCaLabel.Enabled = false;
			// 
			// label4
			// 
			this.label4.Size = new System.Drawing.Size(125, 13);
			this.label4.Text = "Signature representation:";
			// 
			// rbFromImage
			// 
			this.rbFromImage.Location = new System.Drawing.Point(12, 190);
			// 
			// rbFromData
			// 
			this.rbFromData.Location = new System.Drawing.Point(12, 256);
			// 
			// panelFromImage
			// 
			this.panelFromImage.Location = new System.Drawing.Point(12, 213);
			// 
			// panelFromData
			// 
			this.panelFromData.Location = new System.Drawing.Point(12, 279);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(159, 546);
			this.btnOk.TabIndex = 15;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(240, 546);
			this.btnCancel.TabIndex = 16;
			// 
			// rbFromVectors
			// 
			this.rbFromVectors.AutoSize = true;
			this.rbFromVectors.Location = new System.Drawing.Point(12, 452);
			this.rbFromVectors.Name = "rbFromVectors";
			this.rbFromVectors.Size = new System.Drawing.Size(89, 17);
			this.rbFromVectors.TabIndex = 13;
			this.rbFromVectors.TabStop = true;
			this.rbFromVectors.Text = "From vectors:";
			this.rbFromVectors.UseVisualStyleBackColor = true;
			this.rbFromVectors.CheckedChanged += new System.EventHandler(this.FromVectorsRadioButtonCheckedChanged);
			// 
			// fromVectorsPanel
			// 
			this.fromVectorsPanel.Controls.Add(this.btnEditVectors);
			this.fromVectorsPanel.Controls.Add(this.label9);
			this.fromVectorsPanel.Controls.Add(this.resolutioEditBox);
			this.fromVectorsPanel.Controls.Add(this.label10);
			this.fromVectorsPanel.Location = new System.Drawing.Point(12, 475);
			this.fromVectorsPanel.Name = "fromVectorsPanel";
			this.fromVectorsPanel.Size = new System.Drawing.Size(301, 61);
			this.fromVectorsPanel.TabIndex = 14;
			// 
			// btnEditVectors
			// 
			this.btnEditVectors.Location = new System.Drawing.Point(126, 29);
			this.btnEditVectors.Name = "btnEditVectors";
			this.btnEditVectors.Size = new System.Drawing.Size(75, 23);
			this.btnEditVectors.TabIndex = 3;
			this.btnEditVectors.Text = "Edit...";
			this.btnEditVectors.UseVisualStyleBackColor = true;
			this.btnEditVectors.Click += new System.EventHandler(this.BtnEditVectorsClick);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(5, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(87, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "Image resolution:";
			// 
			// resolutioEditBox
			// 
			this.resolutioEditBox.Location = new System.Drawing.Point(127, 1);
			this.resolutioEditBox.Name = "resolutioEditBox";
			this.resolutioEditBox.PpcmValue = 0;
			this.resolutioEditBox.PpiValue = 0;
			this.resolutioEditBox.PpmmValue = 0;
			this.resolutioEditBox.PpmValue = 0;
			this.resolutioEditBox.Size = new System.Drawing.Size(148, 30);
			this.resolutioEditBox.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(5, 34);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "Vectors:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 164);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Signature:";
			// 
			// cbSignature
			// 
			this.cbSignature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSignature.FormattingEnabled = true;
			this.cbSignature.Location = new System.Drawing.Point(138, 161);
			this.cbSignature.Name = "cbSignature";
			this.cbSignature.Size = new System.Drawing.Size(172, 21);
			this.cbSignature.TabIndex = 18;
			// 
			// ANType8RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 579);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cbSignature);
			this.Controls.Add(this.fromVectorsPanel);
			this.Controls.Add(this.rbFromVectors);
			this.Name = "ANType8RecordCreateForm";
			this.Text = "Add Type-8 ANRecord";
			this.Controls.SetChildIndex(this.rbFromVectors, 0);
			this.Controls.SetChildIndex(this.fromVectorsPanel, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.panelFromImage, 0);
			this.Controls.SetChildIndex(this.panelFromData, 0);
			this.Controls.SetChildIndex(this.rbFromData, 0);
			this.Controls.SetChildIndex(this.rbFromImage, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.cbSignature, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.panelFromData.ResumeLayout(false);
			this.panelFromData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.fromVectorsPanel.ResumeLayout(false);
			this.fromVectorsPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbFromVectors;
		private System.Windows.Forms.Panel fromVectorsPanel;
		private System.Windows.Forms.Button btnEditVectors;
		private System.Windows.Forms.Label label9;
		private ResolutionEditBox resolutioEditBox;
		protected System.Windows.Forms.Label label10;
		protected System.Windows.Forms.Label label7;
		protected System.Windows.Forms.ComboBox cbSignature;
	}
}
