namespace Neurotec.Samples
{
	partial class VersionForm
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
			this.versionsLabel = new System.Windows.Forms.Label();
			this.lbVersions = new System.Windows.Forms.ListView();
			this.versionValueColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.versionNameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// versionsLabel
			// 
			this.versionsLabel.AutoSize = true;
			this.versionsLabel.Location = new System.Drawing.Point(12, 9);
			this.versionsLabel.Name = "versionsLabel";
			this.versionsLabel.Size = new System.Drawing.Size(50, 13);
			this.versionsLabel.TabIndex = 0;
			this.versionsLabel.Text = "Versions:";
			// 
			// lbVersions
			// 
			this.lbVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.versionValueColumnHeader,
            this.versionNameColumnHeader});
			this.lbVersions.FullRowSelect = true;
			this.lbVersions.GridLines = true;
			this.lbVersions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lbVersions.HideSelection = false;
			this.lbVersions.Location = new System.Drawing.Point(12, 25);
			this.lbVersions.MultiSelect = false;
			this.lbVersions.Name = "lbVersions";
			this.lbVersions.Size = new System.Drawing.Size(347, 123);
			this.lbVersions.TabIndex = 1;
			this.lbVersions.UseCompatibleStateImageBehavior = false;
			this.lbVersions.View = System.Windows.Forms.View.Details;
			this.lbVersions.SelectedIndexChanged += new System.EventHandler(this.LvVersionsSelectedIndexChanged);
			this.lbVersions.DoubleClick += new System.EventHandler(this.LvVersionsDoubleClick);
			// 
			// versionValueColumnHeader
			// 
			this.versionValueColumnHeader.Text = "Value";
			// 
			// versionNameColumnHeader
			// 
			this.versionNameColumnHeader.Text = "Name";
			this.versionNameColumnHeader.Width = 250;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(203, 165);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(284, 165);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// VersionForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(371, 200);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lbVersions);
			this.Controls.Add(this.versionsLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "VersionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Version";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label versionsLabel;
		private System.Windows.Forms.ListView lbVersions;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader versionValueColumnHeader;
		private System.Windows.Forms.ColumnHeader versionNameColumnHeader;
	}
}
