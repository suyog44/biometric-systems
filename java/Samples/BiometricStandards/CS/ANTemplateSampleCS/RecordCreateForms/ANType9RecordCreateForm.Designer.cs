namespace Neurotec.Samples.RecordCreateForms
{
	partial class ANType9RecordCreateForm
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
			this.chbFmtFlag = new System.Windows.Forms.CheckBox();
			this.rbFromNFRecord = new System.Windows.Forms.RadioButton();
			this.fromNFRecordPanel = new System.Windows.Forms.Panel();
			this.tbNFRecordPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseNFRecord = new System.Windows.Forms.Button();
			this.rbCreateEmpty = new System.Windows.Forms.RadioButton();
			this.createEmptyPanel = new System.Windows.Forms.Panel();
			this.chbHasRidgeCountsIndicator = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbImpressionType = new System.Windows.Forms.ComboBox();
			this.chbContainsRidgeCounts = new System.Windows.Forms.CheckBox();
			this.chbHasMinutiae = new System.Windows.Forms.CheckBox();
			this.nfRecordOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).BeginInit();
			this.fromNFRecordPanel.SuspendLayout();
			this.createEmptyPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.btnOk.Location = new System.Drawing.Point(168, 276);
			this.btnOk.TabIndex = 7;
			// 
			// cancelButton
			// 
			this.btnCancel.Location = new System.Drawing.Point(249, 276);
			this.btnCancel.TabIndex = 8;
			// 
			// chbFmtFlag
			// 
			this.chbFmtFlag.AutoSize = true;
			this.chbFmtFlag.Checked = true;
			this.chbFmtFlag.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbFmtFlag.Location = new System.Drawing.Point(12, 53);
			this.chbFmtFlag.Name = "chbFmtFlag";
			this.chbFmtFlag.Size = new System.Drawing.Size(146, 17);
			this.chbFmtFlag.TabIndex = 2;
			this.chbFmtFlag.Text = "Minutia format is standard";
			this.chbFmtFlag.UseVisualStyleBackColor = true;
			// 
			// rbFromNFRecord
			// 
			this.rbFromNFRecord.AutoSize = true;
			this.rbFromNFRecord.Checked = true;
			this.rbFromNFRecord.Location = new System.Drawing.Point(12, 76);
			this.rbFromNFRecord.Name = "rbFromNFRecord";
			this.rbFromNFRecord.Size = new System.Drawing.Size(103, 17);
			this.rbFromNFRecord.TabIndex = 3;
			this.rbFromNFRecord.TabStop = true;
			this.rbFromNFRecord.Text = "From NFRecord:";
			this.rbFromNFRecord.UseVisualStyleBackColor = true;
			this.rbFromNFRecord.CheckedChanged += new System.EventHandler(this.RbFromNFRecordCheckedChanged);
			// 
			// fromNFRecordPanel
			// 
			this.fromNFRecordPanel.Controls.Add(this.tbNFRecordPath);
			this.fromNFRecordPanel.Controls.Add(this.label2);
			this.fromNFRecordPanel.Controls.Add(this.btnBrowseNFRecord);
			this.fromNFRecordPanel.Location = new System.Drawing.Point(12, 99);
			this.fromNFRecordPanel.Name = "fromNFRecordPanel";
			this.fromNFRecordPanel.Size = new System.Drawing.Size(312, 33);
			this.fromNFRecordPanel.TabIndex = 4;
			// 
			// tbNFRecordPath
			// 
			this.tbNFRecordPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbNFRecordPath.Location = new System.Drawing.Point(84, 3);
			this.tbNFRecordPath.Name = "tbNFRecordPath";
			this.tbNFRecordPath.Size = new System.Drawing.Size(144, 20);
			this.tbNFRecordPath.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "NFRecord file:";
			// 
			// btnBrowseNFRecord
			// 
			this.btnBrowseNFRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseNFRecord.Location = new System.Drawing.Point(234, 1);
			this.btnBrowseNFRecord.Name = "btnBrowseNFRecord";
			this.btnBrowseNFRecord.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseNFRecord.TabIndex = 2;
			this.btnBrowseNFRecord.Text = "Browse...";
			this.btnBrowseNFRecord.UseVisualStyleBackColor = true;
			this.btnBrowseNFRecord.Click += new System.EventHandler(this.BtnBrowseNFRecordClick);
			// 
			// rbCreateEmpty
			// 
			this.rbCreateEmpty.AutoSize = true;
			this.rbCreateEmpty.Location = new System.Drawing.Point(12, 138);
			this.rbCreateEmpty.Name = "rbCreateEmpty";
			this.rbCreateEmpty.Size = new System.Drawing.Size(90, 17);
			this.rbCreateEmpty.TabIndex = 5;
			this.rbCreateEmpty.Text = "Create empty:";
			this.rbCreateEmpty.UseVisualStyleBackColor = true;
			this.rbCreateEmpty.CheckedChanged += new System.EventHandler(this.RbCreateEmptyCheckedChanged);
			// 
			// createEmptyPanel
			// 
			this.createEmptyPanel.Controls.Add(this.chbHasRidgeCountsIndicator);
			this.createEmptyPanel.Controls.Add(this.label3);
			this.createEmptyPanel.Controls.Add(this.cbImpressionType);
			this.createEmptyPanel.Controls.Add(this.chbContainsRidgeCounts);
			this.createEmptyPanel.Controls.Add(this.chbHasMinutiae);
			this.createEmptyPanel.Location = new System.Drawing.Point(12, 161);
			this.createEmptyPanel.Name = "createEmptyPanel";
			this.createEmptyPanel.Size = new System.Drawing.Size(312, 109);
			this.createEmptyPanel.TabIndex = 6;
			// 
			// chbHasRidgeCountsIndicator
			// 
			this.chbHasRidgeCountsIndicator.AutoSize = true;
			this.chbHasRidgeCountsIndicator.Checked = true;
			this.chbHasRidgeCountsIndicator.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbHasRidgeCountsIndicator.Location = new System.Drawing.Point(0, 53);
			this.chbHasRidgeCountsIndicator.Name = "chbHasRidgeCountsIndicator";
			this.chbHasRidgeCountsIndicator.Size = new System.Drawing.Size(149, 17);
			this.chbHasRidgeCountsIndicator.TabIndex = 4;
			this.chbHasRidgeCountsIndicator.Text = "Has ridge counts indicator";
			this.chbHasRidgeCountsIndicator.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Impression type:";
			// 
			// cbImpressionType
			// 
			this.cbImpressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImpressionType.FormattingEnabled = true;
			this.cbImpressionType.Location = new System.Drawing.Point(89, 4);
			this.cbImpressionType.Name = "cbImpressionType";
			this.cbImpressionType.Size = new System.Drawing.Size(220, 21);
			this.cbImpressionType.TabIndex = 1;
			// 
			// chbContainsRidgeCounts
			// 
			this.chbContainsRidgeCounts.AutoSize = true;
			this.chbContainsRidgeCounts.Location = new System.Drawing.Point(18, 76);
			this.chbContainsRidgeCounts.Name = "chbContainsRidgeCounts";
			this.chbContainsRidgeCounts.Size = new System.Drawing.Size(128, 17);
			this.chbContainsRidgeCounts.TabIndex = 3;
			this.chbContainsRidgeCounts.Text = "Contains ridge counts";
			this.chbContainsRidgeCounts.UseVisualStyleBackColor = true;
			// 
			// chbHasMinutiae
			// 
			this.chbHasMinutiae.AutoSize = true;
			this.chbHasMinutiae.Checked = true;
			this.chbHasMinutiae.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbHasMinutiae.Location = new System.Drawing.Point(0, 31);
			this.chbHasMinutiae.Name = "chbHasMinutiae";
			this.chbHasMinutiae.Size = new System.Drawing.Size(153, 17);
			this.chbHasMinutiae.TabIndex = 2;
			this.chbHasMinutiae.Text = "Contains standard minutiae";
			this.chbHasMinutiae.UseVisualStyleBackColor = true;
			// 
			// nfRecordOpenFileDialog
			// 
			this.nfRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFRecord Files (*.dat)|*.dat|All Files (*.*)|*." +
				"*";
			// 
			// ANType9RecordCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 309);
			this.Controls.Add(this.createEmptyPanel);
			this.Controls.Add(this.rbCreateEmpty);
			this.Controls.Add(this.chbFmtFlag);
			this.Controls.Add(this.rbFromNFRecord);
			this.Controls.Add(this.fromNFRecordPanel);
			this.Name = "ANType9RecordCreateForm";
			this.Text = "Add Type-9 ANRecord";
			this.Load += new System.EventHandler(this.ANType9RecordCreateFormLoad);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.fromNFRecordPanel, 0);
			this.Controls.SetChildIndex(this.rbFromNFRecord, 0);
			this.Controls.SetChildIndex(this.chbFmtFlag, 0);
			this.Controls.SetChildIndex(this.nudIdc, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.rbCreateEmpty, 0);
			this.Controls.SetChildIndex(this.createEmptyPanel, 0);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdc)).EndInit();
			this.fromNFRecordPanel.ResumeLayout(false);
			this.fromNFRecordPanel.PerformLayout();
			this.createEmptyPanel.ResumeLayout(false);
			this.createEmptyPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chbFmtFlag;
		private System.Windows.Forms.RadioButton rbFromNFRecord;
		private System.Windows.Forms.Panel fromNFRecordPanel;
		private System.Windows.Forms.TextBox tbNFRecordPath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseNFRecord;
		private System.Windows.Forms.RadioButton rbCreateEmpty;
		private System.Windows.Forms.Panel createEmptyPanel;
		private System.Windows.Forms.CheckBox chbHasMinutiae;
		private System.Windows.Forms.CheckBox chbContainsRidgeCounts;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbImpressionType;
		private System.Windows.Forms.OpenFileDialog nfRecordOpenFileDialog;
		private System.Windows.Forms.CheckBox chbHasRidgeCountsIndicator;
	}
}
