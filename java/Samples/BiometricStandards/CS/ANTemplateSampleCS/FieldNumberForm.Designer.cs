namespace Neurotec.Samples
{
	partial class FieldNumberForm
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
			this.standardFieldsLabel = new System.Windows.Forms.Label();
			this.lvStandardField = new System.Windows.Forms.ListView();
			this.fieldNumberColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.fieldNameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.fieldVersionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.isFieldMantadoryColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rbStandardField = new System.Windows.Forms.RadioButton();
			this.userDefinedFieldsLabel = new System.Windows.Forms.Label();
			this.rbUserDefinedField = new System.Windows.Forms.RadioButton();
			this.tbUserDefinedField = new System.Windows.Forms.TextBox();
			this.userDefinedFieldNumbersLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// standardFieldsLabel
			// 
			this.standardFieldsLabel.AutoSize = true;
			this.standardFieldsLabel.Location = new System.Drawing.Point(12, 9);
			this.standardFieldsLabel.Name = "standardFieldsLabel";
			this.standardFieldsLabel.Size = new System.Drawing.Size(80, 13);
			this.standardFieldsLabel.TabIndex = 1;
			this.standardFieldsLabel.Text = "Standard fields:";
			// 
			// lvStandardField
			// 
			this.lvStandardField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvStandardField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fieldNumberColumnHeader,
            this.fieldNameColumnHeader,
            this.fieldVersionColumnHeader,
            this.isFieldMantadoryColumnHeader});
			this.lvStandardField.FullRowSelect = true;
			this.lvStandardField.GridLines = true;
			this.lvStandardField.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvStandardField.HideSelection = false;
			this.lvStandardField.Location = new System.Drawing.Point(12, 25);
			this.lvStandardField.MultiSelect = false;
			this.lvStandardField.Name = "lvStandardField";
			this.lvStandardField.Size = new System.Drawing.Size(461, 187);
			this.lvStandardField.TabIndex = 2;
			this.lvStandardField.UseCompatibleStateImageBehavior = false;
			this.lvStandardField.View = System.Windows.Forms.View.Details;
			this.lvStandardField.SelectedIndexChanged += new System.EventHandler(this.LvStandardFieldSelectedIndexChanged);
			this.lvStandardField.DoubleClick += new System.EventHandler(this.LvStandardFieldDoubleClick);
			// 
			// fieldNumberColumnHeader
			// 
			this.fieldNumberColumnHeader.Text = "Number";
			// 
			// fieldNameColumnHeader
			// 
			this.fieldNameColumnHeader.Text = "Name";
			this.fieldNameColumnHeader.Width = 250;
			// 
			// fieldVersionColumnHeader
			// 
			this.fieldVersionColumnHeader.Text = "Version";
			this.fieldVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// isFieldMantadoryColumnHeader
			// 
			this.isFieldMantadoryColumnHeader.Text = "Mandatory";
			this.isFieldMantadoryColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.isFieldMantadoryColumnHeader.Width = 70;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(317, 286);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(398, 286);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// rbStandardField
			// 
			this.rbStandardField.AutoCheck = false;
			this.rbStandardField.AutoSize = true;
			this.rbStandardField.Location = new System.Drawing.Point(15, 7);
			this.rbStandardField.Name = "rbStandardField";
			this.rbStandardField.Size = new System.Drawing.Size(93, 17);
			this.rbStandardField.TabIndex = 0;
			this.rbStandardField.TabStop = true;
			this.rbStandardField.Text = "Standard field:";
			this.rbStandardField.UseVisualStyleBackColor = true;
			this.rbStandardField.Click += new System.EventHandler(this.RbStandardFieldClick);
			// 
			// userDefinedFieldsLabel
			// 
			this.userDefinedFieldsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userDefinedFieldsLabel.AutoSize = true;
			this.userDefinedFieldsLabel.Location = new System.Drawing.Point(12, 228);
			this.userDefinedFieldsLabel.Name = "userDefinedFieldsLabel";
			this.userDefinedFieldsLabel.Size = new System.Drawing.Size(97, 13);
			this.userDefinedFieldsLabel.TabIndex = 4;
			this.userDefinedFieldsLabel.Text = "User defined fields:";
			// 
			// rbUserDefinedField
			// 
			this.rbUserDefinedField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbUserDefinedField.AutoCheck = false;
			this.rbUserDefinedField.AutoSize = true;
			this.rbUserDefinedField.Location = new System.Drawing.Point(15, 226);
			this.rbUserDefinedField.Name = "rbUserDefinedField";
			this.rbUserDefinedField.Size = new System.Drawing.Size(110, 17);
			this.rbUserDefinedField.TabIndex = 3;
			this.rbUserDefinedField.TabStop = true;
			this.rbUserDefinedField.Text = "User defined field:";
			this.rbUserDefinedField.UseVisualStyleBackColor = true;
			this.rbUserDefinedField.Click += new System.EventHandler(this.RbUserDefinedFieldClick);
			// 
			// tbUserDefinedField
			// 
			this.tbUserDefinedField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbUserDefinedField.Location = new System.Drawing.Point(15, 249);
			this.tbUserDefinedField.Name = "tbUserDefinedField";
			this.tbUserDefinedField.Size = new System.Drawing.Size(100, 20);
			this.tbUserDefinedField.TabIndex = 6;
			// 
			// userDefinedFieldNumbersLabel
			// 
			this.userDefinedFieldNumbersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userDefinedFieldNumbersLabel.AutoSize = true;
			this.userDefinedFieldNumbersLabel.Location = new System.Drawing.Point(143, 228);
			this.userDefinedFieldNumbersLabel.Name = "userDefinedFieldNumbersLabel";
			this.userDefinedFieldNumbersLabel.Size = new System.Drawing.Size(34, 13);
			this.userDefinedFieldNumbersLabel.TabIndex = 5;
			this.userDefinedFieldNumbersLabel.Text = "(0 - 0)";
			// 
			// FieldNumberForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(485, 321);
			this.Controls.Add(this.userDefinedFieldNumbersLabel);
			this.Controls.Add(this.tbUserDefinedField);
			this.Controls.Add(this.rbUserDefinedField);
			this.Controls.Add(this.userDefinedFieldsLabel);
			this.Controls.Add(this.rbStandardField);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lvStandardField);
			this.Controls.Add(this.standardFieldsLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "FieldNumberForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Field Number";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FieldNumberFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label standardFieldsLabel;
		private System.Windows.Forms.ListView lvStandardField;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader fieldNumberColumnHeader;
		private System.Windows.Forms.ColumnHeader fieldNameColumnHeader;
		private System.Windows.Forms.ColumnHeader isFieldMantadoryColumnHeader;
		private System.Windows.Forms.RadioButton rbStandardField;
		private System.Windows.Forms.Label userDefinedFieldsLabel;
		private System.Windows.Forms.RadioButton rbUserDefinedField;
		private System.Windows.Forms.TextBox tbUserDefinedField;
		private System.Windows.Forms.Label userDefinedFieldNumbersLabel;
		private System.Windows.Forms.ColumnHeader fieldVersionColumnHeader;
	}
}
