namespace Neurotec.Biometrics.Common
{
	partial class BdifOptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BdifOptionsForm));
			this.gbMain = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbVersion = new System.Windows.Forms.ComboBox();
			this.cbBiometricStandard = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lbVersion = new System.Windows.Forms.Label();
			this.cbDoNotCheckCbeffProductId = new System.Windows.Forms.CheckBox();
			this.cbNoStrictRead = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.gbMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbMain
			// 
			this.gbMain.Controls.Add(this.tableLayoutPanel1);
			this.gbMain.Location = new System.Drawing.Point(12, 12);
			this.gbMain.Name = "gbMain";
			this.gbMain.Size = new System.Drawing.Size(311, 121);
			this.gbMain.TabIndex = 0;
			this.gbMain.TabStop = false;
			this.gbMain.Text = "Common";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.cbVersion, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbBiometricStandard, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbVersion, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbDoNotCheckCbeffProductId, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.cbNoStrictRead, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(305, 102);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// cbVersion
			// 
			this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVersion.FormattingEnabled = true;
			this.cbVersion.Location = new System.Drawing.Point(106, 30);
			this.cbVersion.Name = "cbVersion";
			this.cbVersion.Size = new System.Drawing.Size(195, 21);
			this.cbVersion.TabIndex = 7;
			// 
			// cbBiometricStandard
			// 
			this.cbBiometricStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbBiometricStandard.FormattingEnabled = true;
			this.cbBiometricStandard.Location = new System.Drawing.Point(106, 3);
			this.cbBiometricStandard.Name = "cbBiometricStandard";
			this.cbBiometricStandard.Size = new System.Drawing.Size(88, 21);
			this.cbBiometricStandard.TabIndex = 1;
			this.cbBiometricStandard.SelectedIndexChanged += new System.EventHandler(this.cbBiometricStandard_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Biometric standard:";
			// 
			// lbVersion
			// 
			this.lbVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lbVersion.AutoSize = true;
			this.lbVersion.Location = new System.Drawing.Point(55, 34);
			this.lbVersion.Name = "lbVersion";
			this.lbVersion.Size = new System.Drawing.Size(45, 13);
			this.lbVersion.TabIndex = 6;
			this.lbVersion.Text = "Version:";
			// 
			// cbDoNotCheckCbeffProductId
			// 
			this.cbDoNotCheckCbeffProductId.AutoSize = true;
			this.cbDoNotCheckCbeffProductId.Location = new System.Drawing.Point(106, 57);
			this.cbDoNotCheckCbeffProductId.Name = "cbDoNotCheckCbeffProductId";
			this.cbDoNotCheckCbeffProductId.Size = new System.Drawing.Size(177, 17);
			this.cbDoNotCheckCbeffProductId.TabIndex = 4;
			this.cbDoNotCheckCbeffProductId.Text = "Do not check CBEFF product id";
			this.cbDoNotCheckCbeffProductId.UseVisualStyleBackColor = true;
			// 
			// cbNoStrictRead
			// 
			this.cbNoStrictRead.AutoSize = true;
			this.cbNoStrictRead.Location = new System.Drawing.Point(106, 80);
			this.cbNoStrictRead.Name = "cbNoStrictRead";
			this.cbNoStrictRead.Size = new System.Drawing.Size(95, 17);
			this.cbNoStrictRead.TabIndex = 5;
			this.cbNoStrictRead.Text = "Non-strict read";
			this.cbNoStrictRead.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(245, 149);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(164, 149);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// BdifOptionsForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(332, 182);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gbMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BdifOptionsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BdifOptionsForm";
			this.gbMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbMain;
		private System.Windows.Forms.ComboBox cbBiometricStandard;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbDoNotCheckCbeffProductId;
		private System.Windows.Forms.CheckBox cbNoStrictRead;
		protected System.Windows.Forms.Button btnCancel;
		protected System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ComboBox cbVersion;
		private System.Windows.Forms.Label lbVersion;
	}
}
