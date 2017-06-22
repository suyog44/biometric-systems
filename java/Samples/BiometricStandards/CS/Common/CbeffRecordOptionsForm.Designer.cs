namespace Neurotec.Samples
{
	partial class CbeffRecordOptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CbeffRecordOptionsForm));
			this.gbCommon = new System.Windows.Forms.GroupBox();
			this.rbOwnerType = new System.Windows.Forms.RadioButton();
			this.rbUseFormat = new System.Windows.Forms.RadioButton();
			this.txtBoxFormat = new System.Windows.Forms.TextBox();
			this.lbFormat = new System.Windows.Forms.Label();
			this.lbOwner = new System.Windows.Forms.Label();
			this.lbType = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.fbOwnerType = new System.Windows.Forms.GroupBox();
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.cbOwners = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gbCommon.SuspendLayout();
			this.fbOwnerType.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbCommon
			// 
			this.gbCommon.Controls.Add(this.rbOwnerType);
			this.gbCommon.Controls.Add(this.rbUseFormat);
			this.gbCommon.Location = new System.Drawing.Point(12, 12);
			this.gbCommon.Name = "gbCommon";
			this.gbCommon.Size = new System.Drawing.Size(213, 47);
			this.gbCommon.TabIndex = 1;
			this.gbCommon.TabStop = false;
			this.gbCommon.Text = "Common";
			// 
			// rbOwnerType
			// 
			this.rbOwnerType.AutoSize = true;
			this.rbOwnerType.Checked = true;
			this.rbOwnerType.Location = new System.Drawing.Point(9, 19);
			this.rbOwnerType.Name = "rbOwnerType";
			this.rbOwnerType.Size = new System.Drawing.Size(104, 17);
			this.rbOwnerType.TabIndex = 10;
			this.rbOwnerType.TabStop = true;
			this.rbOwnerType.Text = "Owner and Type";
			this.rbOwnerType.UseVisualStyleBackColor = true;
			this.rbOwnerType.CheckedChanged += new System.EventHandler(this.rbOwnerType_CheckedChanged);
			// 
			// rbUseFormat
			// 
			this.rbUseFormat.AutoSize = true;
			this.rbUseFormat.Location = new System.Drawing.Point(119, 19);
			this.rbUseFormat.Name = "rbUseFormat";
			this.rbUseFormat.Size = new System.Drawing.Size(91, 17);
			this.rbUseFormat.TabIndex = 8;
			this.rbUseFormat.Text = "Patron Format";
			this.rbUseFormat.UseVisualStyleBackColor = true;
			this.rbUseFormat.CheckedChanged += new System.EventHandler(this.rbUseFormat_CheckedChanged);
			// 
			// txtBoxFormat
			// 
			this.txtBoxFormat.Enabled = false;
			this.txtBoxFormat.Location = new System.Drawing.Point(54, 19);
			this.txtBoxFormat.Name = "txtBoxFormat";
			this.txtBoxFormat.Size = new System.Drawing.Size(150, 20);
			this.txtBoxFormat.TabIndex = 9;
			// 
			// lbFormat
			// 
			this.lbFormat.AutoSize = true;
			this.lbFormat.Location = new System.Drawing.Point(6, 22);
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.Size = new System.Drawing.Size(42, 13);
			this.lbFormat.TabIndex = 6;
			this.lbFormat.Text = "Format:";
			// 
			// lbOwner
			// 
			this.lbOwner.AutoSize = true;
			this.lbOwner.Location = new System.Drawing.Point(6, 22);
			this.lbOwner.Name = "lbOwner";
			this.lbOwner.Size = new System.Drawing.Size(41, 13);
			this.lbOwner.TabIndex = 3;
			this.lbOwner.Text = "Owner:";
			// 
			// lbType
			// 
			this.lbType.AutoSize = true;
			this.lbType.Location = new System.Drawing.Point(6, 48);
			this.lbType.Name = "lbType";
			this.lbType.Size = new System.Drawing.Size(34, 13);
			this.lbType.TabIndex = 2;
			this.lbType.Text = "Type:";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(285, 145);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(366, 145);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// fbOwnerType
			// 
			this.fbOwnerType.Controls.Add(this.cbTypes);
			this.fbOwnerType.Controls.Add(this.cbOwners);
			this.fbOwnerType.Controls.Add(this.lbOwner);
			this.fbOwnerType.Controls.Add(this.lbType);
			this.fbOwnerType.Location = new System.Drawing.Point(12, 65);
			this.fbOwnerType.Name = "fbOwnerType";
			this.fbOwnerType.Size = new System.Drawing.Size(429, 74);
			this.fbOwnerType.TabIndex = 10;
			this.fbOwnerType.TabStop = false;
			this.fbOwnerType.Text = "Owner and Type";
			// 
			// cbTypes
			// 
			this.cbTypes.FormattingEnabled = true;
			this.cbTypes.Location = new System.Drawing.Point(53, 45);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(370, 21);
			this.cbTypes.TabIndex = 5;
			// 
			// cbOwners
			// 
			this.cbOwners.FormattingEnabled = true;
			this.cbOwners.Location = new System.Drawing.Point(53, 19);
			this.cbOwners.Name = "cbOwners";
			this.cbOwners.Size = new System.Drawing.Size(370, 21);
			this.cbOwners.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lbFormat);
			this.groupBox1.Controls.Add(this.txtBoxFormat);
			this.groupBox1.Location = new System.Drawing.Point(231, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(210, 47);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patron Format";
			// 
			// CbeffRecordOptionsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(451, 175);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.fbOwnerType);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gbCommon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CbeffRecordOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CbeffRecord Options";
			this.gbCommon.ResumeLayout(false);
			this.gbCommon.PerformLayout();
			this.fbOwnerType.ResumeLayout(false);
			this.fbOwnerType.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbOwner;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RadioButton rbUseFormat;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.RadioButton rbOwnerType;
		private System.Windows.Forms.GroupBox fbOwnerType;
		private System.Windows.Forms.ComboBox cbTypes;
		private System.Windows.Forms.ComboBox cbOwners;
		protected System.Windows.Forms.GroupBox gbCommon;
		protected System.Windows.Forms.TextBox txtBoxFormat;
		protected System.Windows.Forms.GroupBox groupBox1;
	}
}
