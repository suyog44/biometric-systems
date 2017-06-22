namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType1RecordCreateForm
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
			this.tbTransactionType = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.tbDestinationAgency = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbOriginatingAgency = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbTransactionControlId = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cbValidationLevel = new System.Windows.Forms.ComboBox();
			this.newValidationLevelLabel = new System.Windows.Forms.Label();
			this.chbUseNistMinutiaNeighbors = new System.Windows.Forms.CheckBox();
			this.chbUseTwoDigitIdc = new System.Windows.Forms.CheckBox();
			this.chbUseTwoDigitFieldNumber = new System.Windows.Forms.CheckBox();
			this.chbUseTwoDigitFieldNumberType1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(81, 317);
			this.btnOk.TabIndex = 98;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(162, 317);
			this.btnCancel.TabIndex = 99;
			// 
			// nudIdc
			// 
			this.nudIdc.Location = new System.Drawing.Point(12, 25);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 9);
			// 
			// tbTransactionType
			// 
			this.tbTransactionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbTransactionType.Location = new System.Drawing.Point(9, 160);
			this.tbTransactionType.Name = "tbTransactionType";
			this.tbTransactionType.Size = new System.Drawing.Size(226, 20);
			this.tbTransactionType.TabIndex = 9;
			this.tbTransactionType.Validating += new System.ComponentModel.CancelEventHandler(this.TbTransactionTypeValidating);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(9, 144);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(89, 13);
			this.label14.TabIndex = 8;
			this.label14.Text = "Transaction type:";
			// 
			// tbDestinationAgency
			// 
			this.tbDestinationAgency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDestinationAgency.Location = new System.Drawing.Point(9, 201);
			this.tbDestinationAgency.Name = "tbDestinationAgency";
			this.tbDestinationAgency.Size = new System.Drawing.Size(226, 20);
			this.tbDestinationAgency.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 185);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Destination agency:";
			// 
			// tbOriginatingAgency
			// 
			this.tbOriginatingAgency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbOriginatingAgency.Location = new System.Drawing.Point(9, 243);
			this.tbOriginatingAgency.Name = "tbOriginatingAgency";
			this.tbOriginatingAgency.Size = new System.Drawing.Size(226, 20);
			this.tbOriginatingAgency.TabIndex = 13;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(9, 227);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(98, 13);
			this.label10.TabIndex = 12;
			this.label10.Text = "Originating agency:";
			// 
			// tbTransactionControlId
			// 
			this.tbTransactionControlId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbTransactionControlId.Location = new System.Drawing.Point(9, 286);
			this.tbTransactionControlId.Name = "tbTransactionControlId";
			this.tbTransactionControlId.Size = new System.Drawing.Size(226, 20);
			this.tbTransactionControlId.TabIndex = 15;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 270);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(143, 13);
			this.label12.TabIndex = 14;
			this.label12.Text = "Transaction control identifier:";
			// 
			// cbValidationLevel
			// 
			this.cbValidationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbValidationLevel.FormattingEnabled = true;
			this.cbValidationLevel.Location = new System.Drawing.Point(96, 57);
			this.cbValidationLevel.Name = "cbValidationLevel";
			this.cbValidationLevel.Size = new System.Drawing.Size(144, 21);
			this.cbValidationLevel.TabIndex = 3;
			this.cbValidationLevel.SelectedIndexChanged += new System.EventHandler(this.CbValidationLevelSelectedIndexChanged);
			// 
			// newValidationLevelLabel
			// 
			this.newValidationLevelLabel.AutoSize = true;
			this.newValidationLevelLabel.Location = new System.Drawing.Point(9, 60);
			this.newValidationLevelLabel.Name = "newValidationLevelLabel";
			this.newValidationLevelLabel.Size = new System.Drawing.Size(81, 13);
			this.newValidationLevelLabel.TabIndex = 2;
			this.newValidationLevelLabel.Text = "Validation level:";
			// 
			// chbUseNistMinutiaNeighbors
			// 
			this.chbUseNistMinutiaNeighbors.AutoSize = true;
			this.chbUseNistMinutiaNeighbors.Checked = global::Neurotec.Samples.Properties.Settings.Default.NewUseNistMinutiaNeighbors;
			this.chbUseNistMinutiaNeighbors.Location = new System.Drawing.Point(12, 84);
			this.chbUseNistMinutiaNeighbors.Name = "chbUseNistMinutiaNeighbors";
			this.chbUseNistMinutiaNeighbors.Size = new System.Drawing.Size(158, 17);
			this.chbUseNistMinutiaNeighbors.TabIndex = 4;
			this.chbUseNistMinutiaNeighbors.Text = "Use NIST minutia neighbors";
			this.chbUseNistMinutiaNeighbors.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitIdc
			// 
			this.chbUseTwoDigitIdc.AutoSize = true;
			this.chbUseTwoDigitIdc.Location = new System.Drawing.Point(12, 104);
			this.chbUseTwoDigitIdc.Name = "chbUseTwoDigitIdc";
			this.chbUseTwoDigitIdc.Size = new System.Drawing.Size(108, 17);
			this.chbUseTwoDigitIdc.TabIndex = 5;
			this.chbUseTwoDigitIdc.Text = "Use two-digit IDC";
			this.chbUseTwoDigitIdc.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitFieldNumber
			// 
			this.chbUseTwoDigitFieldNumber.AutoSize = true;
			this.chbUseTwoDigitFieldNumber.Location = new System.Drawing.Point(12, 124);
			this.chbUseTwoDigitFieldNumber.Name = "chbUseTwoDigitFieldNumber";
			this.chbUseTwoDigitFieldNumber.Size = new System.Drawing.Size(104, 17);
			this.chbUseTwoDigitFieldNumber.TabIndex = 6;
			this.chbUseTwoDigitFieldNumber.Text = "Use two-digit FN";
			this.chbUseTwoDigitFieldNumber.UseVisualStyleBackColor = true;
			// 
			// chbUseTwoDigitFieldNumberType1
			// 
			this.chbUseTwoDigitFieldNumberType1.AutoSize = true;
			this.chbUseTwoDigitFieldNumberType1.Location = new System.Drawing.Point(122, 124);
			this.chbUseTwoDigitFieldNumberType1.Name = "chbUseTwoDigitFieldNumberType1";
			this.chbUseTwoDigitFieldNumberType1.Size = new System.Drawing.Size(120, 17);
			this.chbUseTwoDigitFieldNumberType1.TabIndex = 7;
			this.chbUseTwoDigitFieldNumberType1.Text = "Use two-digit FN T1";
			this.chbUseTwoDigitFieldNumberType1.UseVisualStyleBackColor = true;
			// 
			// ANType1RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(247, 350);
			this.Controls.Add(this.chbUseTwoDigitFieldNumberType1);
			this.Controls.Add(this.chbUseTwoDigitFieldNumber);
			this.Controls.Add(this.chbUseTwoDigitIdc);
			this.Controls.Add(this.cbValidationLevel);
			this.Controls.Add(this.newValidationLevelLabel);
			this.Controls.Add(this.chbUseNistMinutiaNeighbors);
			this.Controls.Add(this.tbTransactionControlId);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.tbOriginatingAgency);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tbDestinationAgency);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tbTransactionType);
			this.Controls.Add(this.label14);
			this.Name = "ANType1RecordCreateForm";
			this.Text = "New Template";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label14, 0);
			this.Controls.SetChildIndex(this.tbTransactionType, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.tbDestinationAgency, 0);
			this.Controls.SetChildIndex(this.label10, 0);
			this.Controls.SetChildIndex(this.tbOriginatingAgency, 0);
			this.Controls.SetChildIndex(this.label12, 0);
			this.Controls.SetChildIndex(this.tbTransactionControlId, 0);
			this.Controls.SetChildIndex(this.chbUseNistMinutiaNeighbors, 0);
			this.Controls.SetChildIndex(this.newValidationLevelLabel, 0);
			this.Controls.SetChildIndex(this.cbValidationLevel, 0);
			this.Controls.SetChildIndex(this.chbUseTwoDigitIdc, 0);
			this.Controls.SetChildIndex(this.chbUseTwoDigitFieldNumber, 0);
			this.Controls.SetChildIndex(this.chbUseTwoDigitFieldNumberType1, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbTransactionType;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox tbDestinationAgency;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbOriginatingAgency;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbTransactionControlId;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cbValidationLevel;
		private System.Windows.Forms.Label newValidationLevelLabel;
		private System.Windows.Forms.CheckBox chbUseNistMinutiaNeighbors;
		private System.Windows.Forms.CheckBox chbUseTwoDigitIdc;
		private System.Windows.Forms.CheckBox chbUseTwoDigitFieldNumber;
		private System.Windows.Forms.CheckBox chbUseTwoDigitFieldNumberType1;
	}
}
