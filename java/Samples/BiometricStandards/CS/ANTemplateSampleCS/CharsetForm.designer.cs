namespace Neurotec.Samples
{
	partial class CharsetForm
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
			this.standardCharsetsLabel = new System.Windows.Forms.Label();
			this.lbStandardCharsets = new System.Windows.Forms.ListView();
			this.charsetIndexColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.charsetNameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.charsetVersionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rbStandardCharset = new System.Windows.Forms.RadioButton();
			this.userDefinedCharsetsLabel = new System.Windows.Forms.Label();
			this.rbUserDefinedCharset = new System.Windows.Forms.RadioButton();
			this.tbUserDefinedCharsetIndex = new System.Windows.Forms.TextBox();
			this.lblUserDefinedCharsetIndicies = new System.Windows.Forms.Label();
			this.userDefinedCharsetIndexLabel = new System.Windows.Forms.Label();
			this.userDefinedCharsetNameLabel = new System.Windows.Forms.Label();
			this.tbUserDefinedCharsetName = new System.Windows.Forms.TextBox();
			this.charsetVersionLabel = new System.Windows.Forms.Label();
			this.tbCharsetVersion = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// standardCharsetsLabel
			// 
			this.standardCharsetsLabel.AutoSize = true;
			this.standardCharsetsLabel.Location = new System.Drawing.Point(12, 9);
			this.standardCharsetsLabel.Name = "standardCharsetsLabel";
			this.standardCharsetsLabel.Size = new System.Drawing.Size(96, 13);
			this.standardCharsetsLabel.TabIndex = 1;
			this.standardCharsetsLabel.Text = "Standard charsets:";
			// 
			// lbStandardCharsets
			// 
			this.lbStandardCharsets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbStandardCharsets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.charsetIndexColumnHeader,
            this.charsetNameColumnHeader,
            this.charsetVersionColumnHeader});
			this.lbStandardCharsets.FullRowSelect = true;
			this.lbStandardCharsets.GridLines = true;
			this.lbStandardCharsets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lbStandardCharsets.HideSelection = false;
			this.lbStandardCharsets.Location = new System.Drawing.Point(12, 25);
			this.lbStandardCharsets.MultiSelect = false;
			this.lbStandardCharsets.Name = "lbStandardCharsets";
			this.lbStandardCharsets.Size = new System.Drawing.Size(398, 120);
			this.lbStandardCharsets.TabIndex = 2;
			this.lbStandardCharsets.UseCompatibleStateImageBehavior = false;
			this.lbStandardCharsets.View = System.Windows.Forms.View.Details;
			this.lbStandardCharsets.SelectedIndexChanged += new System.EventHandler(this.LvStandardCharsetSelectedIndexChanged);
			this.lbStandardCharsets.DoubleClick += new System.EventHandler(this.LvStandardCharsetDoubleClick);
			// 
			// charsetIndexColumnHeader
			// 
			this.charsetIndexColumnHeader.Text = "Index";
			// 
			// charsetNameColumnHeader
			// 
			this.charsetNameColumnHeader.Text = "Name";
			this.charsetNameColumnHeader.Width = 250;
			// 
			// charsetVersionColumnHeader
			// 
			this.charsetVersionColumnHeader.Text = "Version";
			this.charsetVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(254, 231);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 12;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(335, 231);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// rbStandardCharset
			// 
			this.rbStandardCharset.AutoCheck = false;
			this.rbStandardCharset.AutoSize = true;
			this.rbStandardCharset.Location = new System.Drawing.Point(15, 7);
			this.rbStandardCharset.Name = "rbStandardCharset";
			this.rbStandardCharset.Size = new System.Drawing.Size(109, 17);
			this.rbStandardCharset.TabIndex = 0;
			this.rbStandardCharset.TabStop = true;
			this.rbStandardCharset.Text = "Standard charset:";
			this.rbStandardCharset.UseVisualStyleBackColor = true;
			this.rbStandardCharset.Click += new System.EventHandler(this.RbStandardCharsetClick);
			// 
			// userDefinedCharsetsLabel
			// 
			this.userDefinedCharsetsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userDefinedCharsetsLabel.AutoSize = true;
			this.userDefinedCharsetsLabel.Location = new System.Drawing.Point(12, 157);
			this.userDefinedCharsetsLabel.Name = "userDefinedCharsetsLabel";
			this.userDefinedCharsetsLabel.Size = new System.Drawing.Size(113, 13);
			this.userDefinedCharsetsLabel.TabIndex = 4;
			this.userDefinedCharsetsLabel.Text = "User defined charsets:";
			// 
			// rbUserDefinedCharset
			// 
			this.rbUserDefinedCharset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbUserDefinedCharset.AutoCheck = false;
			this.rbUserDefinedCharset.AutoSize = true;
			this.rbUserDefinedCharset.Location = new System.Drawing.Point(15, 155);
			this.rbUserDefinedCharset.Name = "rbUserDefinedCharset";
			this.rbUserDefinedCharset.Size = new System.Drawing.Size(126, 17);
			this.rbUserDefinedCharset.TabIndex = 3;
			this.rbUserDefinedCharset.TabStop = true;
			this.rbUserDefinedCharset.Text = "User defined charset:";
			this.rbUserDefinedCharset.UseVisualStyleBackColor = true;
			this.rbUserDefinedCharset.Click += new System.EventHandler(this.RbUserDefinedCharsetClick);
			// 
			// tbUserDefinedCharsetIndex
			// 
			this.tbUserDefinedCharsetIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbUserDefinedCharsetIndex.Location = new System.Drawing.Point(15, 194);
			this.tbUserDefinedCharsetIndex.Name = "tbUserDefinedCharsetIndex";
			this.tbUserDefinedCharsetIndex.Size = new System.Drawing.Size(100, 20);
			this.tbUserDefinedCharsetIndex.TabIndex = 7;
			// 
			// lblUserDefinedCharsetIndicies
			// 
			this.lblUserDefinedCharsetIndicies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblUserDefinedCharsetIndicies.AutoSize = true;
			this.lblUserDefinedCharsetIndicies.Location = new System.Drawing.Point(143, 157);
			this.lblUserDefinedCharsetIndicies.Name = "lblUserDefinedCharsetIndicies";
			this.lblUserDefinedCharsetIndicies.Size = new System.Drawing.Size(34, 13);
			this.lblUserDefinedCharsetIndicies.TabIndex = 5;
			this.lblUserDefinedCharsetIndicies.Text = "(0 - 0)";
			// 
			// userDefinedCharsetIndexLabel
			// 
			this.userDefinedCharsetIndexLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userDefinedCharsetIndexLabel.AutoSize = true;
			this.userDefinedCharsetIndexLabel.Location = new System.Drawing.Point(12, 178);
			this.userDefinedCharsetIndexLabel.Name = "userDefinedCharsetIndexLabel";
			this.userDefinedCharsetIndexLabel.Size = new System.Drawing.Size(36, 13);
			this.userDefinedCharsetIndexLabel.TabIndex = 6;
			this.userDefinedCharsetIndexLabel.Text = "Index:";
			// 
			// userDefinedCharsetNameLabel
			// 
			this.userDefinedCharsetNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userDefinedCharsetNameLabel.AutoSize = true;
			this.userDefinedCharsetNameLabel.Location = new System.Drawing.Point(118, 178);
			this.userDefinedCharsetNameLabel.Name = "userDefinedCharsetNameLabel";
			this.userDefinedCharsetNameLabel.Size = new System.Drawing.Size(38, 13);
			this.userDefinedCharsetNameLabel.TabIndex = 8;
			this.userDefinedCharsetNameLabel.Text = "Name:";
			// 
			// tbUserDefinedCharsetName
			// 
			this.tbUserDefinedCharsetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbUserDefinedCharsetName.Location = new System.Drawing.Point(121, 194);
			this.tbUserDefinedCharsetName.Name = "tbUserDefinedCharsetName";
			this.tbUserDefinedCharsetName.Size = new System.Drawing.Size(100, 20);
			this.tbUserDefinedCharsetName.TabIndex = 9;
			// 
			// charsetVersionLabel
			// 
			this.charsetVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.charsetVersionLabel.AutoSize = true;
			this.charsetVersionLabel.Location = new System.Drawing.Point(307, 178);
			this.charsetVersionLabel.Name = "charsetVersionLabel";
			this.charsetVersionLabel.Size = new System.Drawing.Size(45, 13);
			this.charsetVersionLabel.TabIndex = 10;
			this.charsetVersionLabel.Text = "Version:";
			// 
			// tbCharsetVersion
			// 
			this.tbCharsetVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbCharsetVersion.Location = new System.Drawing.Point(310, 194);
			this.tbCharsetVersion.Name = "tbCharsetVersion";
			this.tbCharsetVersion.Size = new System.Drawing.Size(100, 20);
			this.tbCharsetVersion.TabIndex = 11;
			// 
			// CharsetForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(422, 266);
			this.Controls.Add(this.tbCharsetVersion);
			this.Controls.Add(this.charsetVersionLabel);
			this.Controls.Add(this.tbUserDefinedCharsetName);
			this.Controls.Add(this.userDefinedCharsetNameLabel);
			this.Controls.Add(this.userDefinedCharsetIndexLabel);
			this.Controls.Add(this.lblUserDefinedCharsetIndicies);
			this.Controls.Add(this.tbUserDefinedCharsetIndex);
			this.Controls.Add(this.rbUserDefinedCharset);
			this.Controls.Add(this.userDefinedCharsetsLabel);
			this.Controls.Add(this.rbStandardCharset);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lbStandardCharsets);
			this.Controls.Add(this.standardCharsetsLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "CharsetForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Charset";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharsetFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label standardCharsetsLabel;
		private System.Windows.Forms.ListView lbStandardCharsets;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader charsetIndexColumnHeader;
		private System.Windows.Forms.ColumnHeader charsetNameColumnHeader;
		private System.Windows.Forms.RadioButton rbStandardCharset;
		private System.Windows.Forms.Label userDefinedCharsetsLabel;
		private System.Windows.Forms.RadioButton rbUserDefinedCharset;
		private System.Windows.Forms.TextBox tbUserDefinedCharsetIndex;
		private System.Windows.Forms.Label lblUserDefinedCharsetIndicies;
		private System.Windows.Forms.ColumnHeader charsetVersionColumnHeader;
		private System.Windows.Forms.Label userDefinedCharsetIndexLabel;
		private System.Windows.Forms.Label userDefinedCharsetNameLabel;
		private System.Windows.Forms.TextBox tbUserDefinedCharsetName;
		private System.Windows.Forms.Label charsetVersionLabel;
		private System.Windows.Forms.TextBox tbCharsetVersion;
	}
}
