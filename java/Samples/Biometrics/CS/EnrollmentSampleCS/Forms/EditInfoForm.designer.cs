namespace Neurotec.Samples.Forms
{
	partial class EditInfoForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInfoForm));
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ColumnKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnDown = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnDelete = new System.Windows.Forms.Button();
			this.cbThumbnailField = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnUp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.BackgroundColor = System.Drawing.Color.White;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnKey});
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView.Location = new System.Drawing.Point(35, 0);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(560, 326);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCellValueChanged);
			this.dataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridViewRowsAdded);
			this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCellEndEdit);
			this.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DataGridViewRowsRemoved);
			// 
			// ColumnKey
			// 
			this.ColumnKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnKey.HeaderText = "Key";
			this.ColumnKey.Name = "ColumnKey";
			this.ColumnKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(445, 371);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(526, 371);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnDown
			// 
			this.btnDown.Image = global::Neurotec.Samples.Properties.Resources.ArrowDown;
			this.btnDown.Location = new System.Drawing.Point(0, 36);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(29, 30);
			this.btnDown.TabIndex = 7;
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.BtnDownClick);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Controls.Add(this.cbThumbnailField);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnUp);
			this.panel1.Controls.Add(this.btnDown);
			this.panel1.Controls.Add(this.dataGridView);
			this.panel1.Location = new System.Drawing.Point(3, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(598, 364);
			this.panel1.TabIndex = 8;
			// 
			// btnDelete
			// 
			this.btnDelete.Image = global::Neurotec.Samples.Properties.Resources.Bad;
			this.btnDelete.Location = new System.Drawing.Point(0, 72);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(29, 30);
			this.btnDelete.TabIndex = 10;
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
			// 
			// cbThumbnailField
			// 
			this.cbThumbnailField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbThumbnailField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbThumbnailField.FormattingEnabled = true;
			this.cbThumbnailField.Location = new System.Drawing.Point(137, 338);
			this.cbThumbnailField.Name = "cbThumbnailField";
			this.cbThumbnailField.Size = new System.Drawing.Size(458, 21);
			this.cbThumbnailField.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 341);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Subject image field:";
			// 
			// btnUp
			// 
			this.btnUp.Image = global::Neurotec.Samples.Properties.Resources.ArrowUp;
			this.btnUp.Location = new System.Drawing.Point(0, 0);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(29, 30);
			this.btnUp.TabIndex = 6;
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.BtnUpClick);
			// 
			// EditInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(604, 397);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(620, 400);
			this.Name = "EditInfoForm";
			this.Text = "Edit Info";
			this.Load += new System.EventHandler(this.EditInfoFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbThumbnailField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKey;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnUp;
	}
}
