namespace Neurotec.Samples
{
	partial class RecordTypeForm
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
			this.recordTypeLabel = new System.Windows.Forms.Label();
			this.lvRecordType = new System.Windows.Forms.ListView();
			this.recordTypeNumberColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.recordTypeNameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.recordTypeDataTypeColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.recordTypeVersionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnShowFields = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// recordTypeLabel
			// 
			this.recordTypeLabel.AutoSize = true;
			this.recordTypeLabel.Location = new System.Drawing.Point(12, 9);
			this.recordTypeLabel.Name = "recordTypeLabel";
			this.recordTypeLabel.Size = new System.Drawing.Size(68, 13);
			this.recordTypeLabel.TabIndex = 0;
			this.recordTypeLabel.Text = "Record type:";
			// 
			// lvRecordType
			// 
			this.lvRecordType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvRecordType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.recordTypeNumberColumnHeader,
            this.recordTypeNameColumnHeader,
            this.recordTypeDataTypeColumnHeader,
            this.recordTypeVersionColumnHeader});
			this.lvRecordType.FullRowSelect = true;
			this.lvRecordType.GridLines = true;
			this.lvRecordType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvRecordType.HideSelection = false;
			this.lvRecordType.Location = new System.Drawing.Point(12, 25);
			this.lvRecordType.MultiSelect = false;
			this.lvRecordType.Name = "lvRecordType";
			this.lvRecordType.Size = new System.Drawing.Size(508, 319);
			this.lvRecordType.TabIndex = 1;
			this.lvRecordType.UseCompatibleStateImageBehavior = false;
			this.lvRecordType.View = System.Windows.Forms.View.Details;
			this.lvRecordType.SelectedIndexChanged += new System.EventHandler(this.LvRecordTypeSelectedIndexChanged);
			this.lvRecordType.DoubleClick += new System.EventHandler(this.LvRecordTypeDoubleClick);
			// 
			// recordTypeNumberColumnHeader
			// 
			this.recordTypeNumberColumnHeader.Text = "Number";
			// 
			// recordTypeNameColumnHeader
			// 
			this.recordTypeNameColumnHeader.Text = "Name";
			this.recordTypeNameColumnHeader.Width = 250;
			// 
			// recordTypeDataTypeColumnHeader
			// 
			this.recordTypeDataTypeColumnHeader.Text = "Data type";
			this.recordTypeDataTypeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.recordTypeDataTypeColumnHeader.Width = 100;
			// 
			// recordTypeVersionColumnHeader
			// 
			this.recordTypeVersionColumnHeader.Text = "Version";
			this.recordTypeVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(364, 363);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(445, 363);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnShowFields
			// 
			this.btnShowFields.Location = new System.Drawing.Point(12, 363);
			this.btnShowFields.Name = "btnShowFields";
			this.btnShowFields.Size = new System.Drawing.Size(75, 23);
			this.btnShowFields.TabIndex = 2;
			this.btnShowFields.Text = "Show fields";
			this.btnShowFields.UseVisualStyleBackColor = true;
			this.btnShowFields.Click += new System.EventHandler(this.BtnShowFieldsClick);
			// 
			// RecordTypeForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(532, 398);
			this.Controls.Add(this.btnShowFields);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lvRecordType);
			this.Controls.Add(this.recordTypeLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 200);
			this.Name = "RecordTypeForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Record Type";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label recordTypeLabel;
		private System.Windows.Forms.ListView lvRecordType;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader recordTypeNumberColumnHeader;
		private System.Windows.Forms.ColumnHeader recordTypeNameColumnHeader;
		private System.Windows.Forms.ColumnHeader recordTypeDataTypeColumnHeader;
		private System.Windows.Forms.Button btnShowFields;
		private System.Windows.Forms.ColumnHeader recordTypeVersionColumnHeader;
	}
}
