namespace Neurotec.Samples
{
	partial class OptionsForm
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
			this.cbValidationLevel = new System.Windows.Forms.ComboBox();
			this.openValidationLevelLabel = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chbUseTwoDigitIdc = new System.Windows.Forms.CheckBox();
			this.chbLeaveInvalidUnvalidated = new System.Windows.Forms.CheckBox();
			this.chbRecover = new System.Windows.Forms.CheckBox();
			this.chbMergeDuplicateFields = new System.Windows.Forms.CheckBox();
			this.chbUseNistMinutiaeNeighbors = new System.Windows.Forms.CheckBox();
			this.chbNonStrinctRead = new System.Windows.Forms.CheckBox();
			this.chbUseTwoDigitFieldNumber = new System.Windows.Forms.CheckBox();
			this.chbUseTwoDigitFieldNumberType1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cbValidationLevel
			// 
			this.cbValidationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbValidationLevel.FormattingEnabled = true;
			this.cbValidationLevel.Location = new System.Drawing.Point(90, 6);
			this.cbValidationLevel.Name = "cbValidationLevel";
			this.cbValidationLevel.Size = new System.Drawing.Size(121, 21);
			this.cbValidationLevel.TabIndex = 1;
			// 
			// openValidationLevelLabel
			// 
			this.openValidationLevelLabel.AutoSize = true;
			this.openValidationLevelLabel.Location = new System.Drawing.Point(3, 9);
			this.openValidationLevelLabel.Name = "openValidationLevelLabel";
			this.openValidationLevelLabel.Size = new System.Drawing.Size(81, 13);
			this.openValidationLevelLabel.TabIndex = 0;
			this.openValidationLevelLabel.Text = "Validation level:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(167, 129);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(248, 129);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitIdc
			// 
			this.chbUseTwoDigitIdc.AutoSize = true;
			this.chbUseTwoDigitIdc.Location = new System.Drawing.Point(6, 79);
			this.chbUseTwoDigitIdc.Name = "chbUseTwoDigitIdc";
			this.chbUseTwoDigitIdc.Size = new System.Drawing.Size(108, 17);
			this.chbUseTwoDigitIdc.TabIndex = 4;
			this.chbUseTwoDigitIdc.Text = "Use two-digit IDC";
			this.chbUseTwoDigitIdc.UseVisualStyleBackColor = true;
			// 
			// chbLeaveInvalidUnvalidated
			// 
			this.chbLeaveInvalidUnvalidated.AutoSize = true;
			this.chbLeaveInvalidUnvalidated.Location = new System.Drawing.Point(139, 56);
			this.chbLeaveInvalidUnvalidated.Name = "chbLeaveInvalidUnvalidated";
			this.chbLeaveInvalidUnvalidated.Size = new System.Drawing.Size(185, 17);
			this.chbLeaveInvalidUnvalidated.TabIndex = 7;
			this.chbLeaveInvalidUnvalidated.Text = "Leave invalid records unvalidated";
			this.chbLeaveInvalidUnvalidated.UseVisualStyleBackColor = true;
			// 
			// chbRecover
			// 
			this.chbRecover.AutoSize = true;
			this.chbRecover.Location = new System.Drawing.Point(139, 79);
			this.chbRecover.Name = "chbRecover";
			this.chbRecover.Size = new System.Drawing.Size(145, 17);
			this.chbRecover.TabIndex = 8;
			this.chbRecover.Text = "Recover from binary data";
			this.chbRecover.UseVisualStyleBackColor = true;
			// 
			// chbMergeDuplicateFields
			// 
			this.chbMergeDuplicateFields.AutoSize = true;
			this.chbMergeDuplicateFields.Location = new System.Drawing.Point(6, 56);
			this.chbMergeDuplicateFields.Name = "chbMergeDuplicateFields";
			this.chbMergeDuplicateFields.Size = new System.Drawing.Size(129, 17);
			this.chbMergeDuplicateFields.TabIndex = 3;
			this.chbMergeDuplicateFields.Text = "Merge duplicate fields";
			this.chbMergeDuplicateFields.UseVisualStyleBackColor = true;
			// 
			// chbUseNistMinutiaeNeighbors
			// 
			this.chbUseNistMinutiaeNeighbors.AutoSize = true;
			this.chbUseNistMinutiaeNeighbors.Location = new System.Drawing.Point(139, 33);
			this.chbUseNistMinutiaeNeighbors.Name = "chbUseNistMinutiaeNeighbors";
			this.chbUseNistMinutiaeNeighbors.Size = new System.Drawing.Size(158, 17);
			this.chbUseNistMinutiaeNeighbors.TabIndex = 6;
			this.chbUseNistMinutiaeNeighbors.Text = "Use NIST minutia neighbors";
			this.chbUseNistMinutiaeNeighbors.UseVisualStyleBackColor = true;
			// 
			// chbNonStrinctRead
			// 
			this.chbNonStrinctRead.AutoSize = true;
			this.chbNonStrinctRead.Location = new System.Drawing.Point(6, 33);
			this.chbNonStrinctRead.Name = "chbNonStrinctRead";
			this.chbNonStrinctRead.Size = new System.Drawing.Size(95, 17);
			this.chbNonStrinctRead.TabIndex = 2;
			this.chbNonStrinctRead.Text = "Non-strict read";
			this.chbNonStrinctRead.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitFieldNumber
			// 
			this.chbUseTwoDigitFieldNumber.AutoSize = true;
			this.chbUseTwoDigitFieldNumber.Location = new System.Drawing.Point(6, 102);
			this.chbUseTwoDigitFieldNumber.Name = "chbUseTwoDigitFieldNumber";
			this.chbUseTwoDigitFieldNumber.Size = new System.Drawing.Size(104, 17);
			this.chbUseTwoDigitFieldNumber.TabIndex = 5;
			this.chbUseTwoDigitFieldNumber.Text = "Use two-digit FN";
			this.chbUseTwoDigitFieldNumber.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitFieldNumberType1
			// 
			this.chbUseTwoDigitFieldNumberType1.AutoSize = true;
			this.chbUseTwoDigitFieldNumberType1.Location = new System.Drawing.Point(138, 102);
			this.chbUseTwoDigitFieldNumberType1.Name = "chbUseTwoDigitFieldNumberType1";
			this.chbUseTwoDigitFieldNumberType1.Size = new System.Drawing.Size(120, 17);
			this.chbUseTwoDigitFieldNumberType1.TabIndex = 9;
			this.chbUseTwoDigitFieldNumberType1.Text = "Use two-digit FN T1";
			this.chbUseTwoDigitFieldNumberType1.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(335, 160);
			this.Controls.Add(this.chbUseTwoDigitFieldNumberType1);
			this.Controls.Add(this.chbUseTwoDigitFieldNumber);
			this.Controls.Add(this.chbUseTwoDigitIdc);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.chbLeaveInvalidUnvalidated);
			this.Controls.Add(this.chbRecover);
			this.Controls.Add(this.chbMergeDuplicateFields);
			this.Controls.Add(this.chbUseNistMinutiaeNeighbors);
			this.Controls.Add(this.chbNonStrinctRead);
			this.Controls.Add(this.cbValidationLevel);
			this.Controls.Add(this.openValidationLevelLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "OptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.OpenOptionsFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chbLeaveInvalidUnvalidated;
		private System.Windows.Forms.CheckBox chbRecover;
		private System.Windows.Forms.CheckBox chbMergeDuplicateFields;
		private System.Windows.Forms.CheckBox chbUseNistMinutiaeNeighbors;
		private System.Windows.Forms.CheckBox chbNonStrinctRead;
		private System.Windows.Forms.ComboBox cbValidationLevel;
		private System.Windows.Forms.Label openValidationLevelLabel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chbUseTwoDigitIdc;
		private System.Windows.Forms.CheckBox chbUseTwoDigitFieldNumber;
		private System.Windows.Forms.CheckBox chbUseTwoDigitFieldNumberType1;

	}
}
