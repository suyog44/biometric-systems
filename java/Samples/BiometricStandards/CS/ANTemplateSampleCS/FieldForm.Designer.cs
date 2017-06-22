namespace Neurotec.Samples
{
	partial class FieldForm
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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.editSubFieldButton = new System.Windows.Forms.Button();
			this.removeSubFieldButton = new System.Windows.Forms.Button();
			this.insertSubFieldButton = new System.Windows.Forms.Button();
			this.addSubFieldButton = new System.Windows.Forms.Button();
			this.subFieldsLabel = new System.Windows.Forms.Label();
			this.subFieldListBox = new System.Windows.Forms.ListBox();
			this.editItemButton = new System.Windows.Forms.Button();
			this.removeItemButton = new System.Windows.Forms.Button();
			this.insertItemButton = new System.Windows.Forms.Button();
			this.addItemButton = new System.Windows.Forms.Button();
			this.itemsLabel = new System.Windows.Forms.Label();
			this.itemListBox = new System.Windows.Forms.ListBox();
			this.closeButton = new System.Windows.Forms.Button();
			this.valueLabel = new System.Windows.Forms.Label();
			this.fieldValueLabel = new System.Windows.Forms.Label();
			this.editFieldButton = new System.Windows.Forms.Button();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(12, 43);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.editSubFieldButton);
			this.splitContainer.Panel1.Controls.Add(this.removeSubFieldButton);
			this.splitContainer.Panel1.Controls.Add(this.insertSubFieldButton);
			this.splitContainer.Panel1.Controls.Add(this.addSubFieldButton);
			this.splitContainer.Panel1.Controls.Add(this.subFieldsLabel);
			this.splitContainer.Panel1.Controls.Add(this.subFieldListBox);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.editItemButton);
			this.splitContainer.Panel2.Controls.Add(this.removeItemButton);
			this.splitContainer.Panel2.Controls.Add(this.insertItemButton);
			this.splitContainer.Panel2.Controls.Add(this.addItemButton);
			this.splitContainer.Panel2.Controls.Add(this.itemsLabel);
			this.splitContainer.Panel2.Controls.Add(this.itemListBox);
			this.splitContainer.Size = new System.Drawing.Size(638, 253);
			this.splitContainer.SplitterDistance = 296;
			this.splitContainer.TabIndex = 3;
			// 
			// editSubFieldButton
			// 
			this.editSubFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.editSubFieldButton.Location = new System.Drawing.Point(204, 221);
			this.editSubFieldButton.Name = "editSubFieldButton";
			this.editSubFieldButton.Size = new System.Drawing.Size(60, 23);
			this.editSubFieldButton.TabIndex = 5;
			this.editSubFieldButton.Text = "Edi&t";
			this.editSubFieldButton.UseVisualStyleBackColor = true;
			this.editSubFieldButton.Click += new System.EventHandler(this.EditSubFieldButtonClick);
			// 
			// removeSubFieldButton
			// 
			this.removeSubFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removeSubFieldButton.Location = new System.Drawing.Point(138, 221);
			this.removeSubFieldButton.Name = "removeSubFieldButton";
			this.removeSubFieldButton.Size = new System.Drawing.Size(60, 23);
			this.removeSubFieldButton.TabIndex = 4;
			this.removeSubFieldButton.Text = "Re&move";
			this.removeSubFieldButton.UseVisualStyleBackColor = true;
			this.removeSubFieldButton.Click += new System.EventHandler(this.RemoveSubFieldButtonClick);
			// 
			// insertSubFieldButton
			// 
			this.insertSubFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.insertSubFieldButton.Location = new System.Drawing.Point(72, 221);
			this.insertSubFieldButton.Name = "insertSubFieldButton";
			this.insertSubFieldButton.Size = new System.Drawing.Size(60, 23);
			this.insertSubFieldButton.TabIndex = 3;
			this.insertSubFieldButton.Text = "I&nsert";
			this.insertSubFieldButton.UseVisualStyleBackColor = true;
			this.insertSubFieldButton.Click += new System.EventHandler(this.InsertSubFieldButtonClick);
			// 
			// addSubFieldButton
			// 
			this.addSubFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addSubFieldButton.Location = new System.Drawing.Point(6, 221);
			this.addSubFieldButton.Name = "addSubFieldButton";
			this.addSubFieldButton.Size = new System.Drawing.Size(60, 23);
			this.addSubFieldButton.TabIndex = 2;
			this.addSubFieldButton.Text = "A&dd";
			this.addSubFieldButton.UseVisualStyleBackColor = true;
			this.addSubFieldButton.Click += new System.EventHandler(this.AddSubFieldButtonClick);
			// 
			// subFieldsLabel
			// 
			this.subFieldsLabel.AutoSize = true;
			this.subFieldsLabel.Location = new System.Drawing.Point(3, 0);
			this.subFieldsLabel.Name = "subFieldsLabel";
			this.subFieldsLabel.Size = new System.Drawing.Size(53, 13);
			this.subFieldsLabel.TabIndex = 0;
			this.subFieldsLabel.Text = "Subfields:";
			// 
			// subFieldListBox
			// 
			this.subFieldListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.subFieldListBox.FormattingEnabled = true;
			this.subFieldListBox.HorizontalScrollbar = true;
			this.subFieldListBox.Location = new System.Drawing.Point(0, 16);
			this.subFieldListBox.Name = "subFieldListBox";
			this.subFieldListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.subFieldListBox.Size = new System.Drawing.Size(294, 186);
			this.subFieldListBox.TabIndex = 1;
			this.subFieldListBox.SelectedIndexChanged += new System.EventHandler(this.SubFieldListBoxSelectedIndexChanged);
			this.subFieldListBox.DoubleClick += new System.EventHandler(this.SubFieldListBoxDoubleClick);
			// 
			// editItemButton
			// 
			this.editItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.editItemButton.Location = new System.Drawing.Point(204, 221);
			this.editItemButton.Name = "editItemButton";
			this.editItemButton.Size = new System.Drawing.Size(60, 23);
			this.editItemButton.TabIndex = 5;
			this.editItemButton.Text = "&Edit";
			this.editItemButton.UseVisualStyleBackColor = true;
			this.editItemButton.Click += new System.EventHandler(this.EditItemButtonClick);
			// 
			// removeItemButton
			// 
			this.removeItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removeItemButton.Location = new System.Drawing.Point(138, 221);
			this.removeItemButton.Name = "removeItemButton";
			this.removeItemButton.Size = new System.Drawing.Size(60, 23);
			this.removeItemButton.TabIndex = 4;
			this.removeItemButton.Text = "&Remove";
			this.removeItemButton.UseVisualStyleBackColor = true;
			this.removeItemButton.Click += new System.EventHandler(this.RemoveItemButtonClick);
			// 
			// insertItemButton
			// 
			this.insertItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.insertItemButton.Location = new System.Drawing.Point(72, 221);
			this.insertItemButton.Name = "insertItemButton";
			this.insertItemButton.Size = new System.Drawing.Size(60, 23);
			this.insertItemButton.TabIndex = 3;
			this.insertItemButton.Text = "&Insert";
			this.insertItemButton.UseVisualStyleBackColor = true;
			this.insertItemButton.Click += new System.EventHandler(this.InsertItemButtonClick);
			// 
			// addItemButton
			// 
			this.addItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addItemButton.Location = new System.Drawing.Point(6, 221);
			this.addItemButton.Name = "addItemButton";
			this.addItemButton.Size = new System.Drawing.Size(60, 23);
			this.addItemButton.TabIndex = 2;
			this.addItemButton.Text = "&Add";
			this.addItemButton.UseVisualStyleBackColor = true;
			this.addItemButton.Click += new System.EventHandler(this.AddItemButtonClick);
			// 
			// itemsLabel
			// 
			this.itemsLabel.AutoSize = true;
			this.itemsLabel.Location = new System.Drawing.Point(3, 0);
			this.itemsLabel.Name = "itemsLabel";
			this.itemsLabel.Size = new System.Drawing.Size(35, 13);
			this.itemsLabel.TabIndex = 0;
			this.itemsLabel.Text = "Items:";
			// 
			// itemListBox
			// 
			this.itemListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.itemListBox.FormattingEnabled = true;
			this.itemListBox.HorizontalScrollbar = true;
			this.itemListBox.Location = new System.Drawing.Point(3, 16);
			this.itemListBox.Name = "itemListBox";
			this.itemListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.itemListBox.Size = new System.Drawing.Size(335, 186);
			this.itemListBox.TabIndex = 1;
			this.itemListBox.SelectedIndexChanged += new System.EventHandler(this.ItemListBoxSelectedIndexChanged);
			this.itemListBox.DoubleClick += new System.EventHandler(this.ItemListBoxDoubleClick);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.closeButton.Location = new System.Drawing.Point(575, 316);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 4;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// valueLabel
			// 
			this.valueLabel.AutoSize = true;
			this.valueLabel.Location = new System.Drawing.Point(12, 17);
			this.valueLabel.Name = "valueLabel";
			this.valueLabel.Size = new System.Drawing.Size(37, 13);
			this.valueLabel.TabIndex = 0;
			this.valueLabel.Text = "&Value:";
			// 
			// fieldValueLabel
			// 
			this.fieldValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fieldValueLabel.AutoEllipsis = true;
			this.fieldValueLabel.Location = new System.Drawing.Point(55, 17);
			this.fieldValueLabel.Name = "fieldValueLabel";
			this.fieldValueLabel.Size = new System.Drawing.Size(529, 13);
			this.fieldValueLabel.TabIndex = 2;
			this.fieldValueLabel.DoubleClick += new System.EventHandler(this.FieldValueLabelDoubleClick);
			// 
			// editFieldButton
			// 
			this.editFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editFieldButton.Location = new System.Drawing.Point(590, 12);
			this.editFieldButton.Name = "editFieldButton";
			this.editFieldButton.Size = new System.Drawing.Size(60, 23);
			this.editFieldButton.TabIndex = 1;
			this.editFieldButton.Text = "Edit";
			this.editFieldButton.UseVisualStyleBackColor = true;
			this.editFieldButton.Click += new System.EventHandler(this.EditFieldButtonClick);
			// 
			// FieldForm
			// 
			this.AcceptButton = this.closeButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(662, 351);
			this.Controls.Add(this.editFieldButton);
			this.Controls.Add(this.fieldValueLabel);
			this.Controls.Add(this.valueLabel);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.splitContainer);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 300);
			this.Name = "FieldForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Field";
			this.Shown += new System.EventHandler(this.FieldFormShown);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.ListBox subFieldListBox;
		private System.Windows.Forms.ListBox itemListBox;
		private System.Windows.Forms.Label subFieldsLabel;
		private System.Windows.Forms.Label itemsLabel;
		private System.Windows.Forms.Button removeSubFieldButton;
		private System.Windows.Forms.Button insertSubFieldButton;
		private System.Windows.Forms.Button addSubFieldButton;
		private System.Windows.Forms.Button editItemButton;
		private System.Windows.Forms.Button removeItemButton;
		private System.Windows.Forms.Button insertItemButton;
		private System.Windows.Forms.Button addItemButton;
		private System.Windows.Forms.Button editSubFieldButton;
		private System.Windows.Forms.Label valueLabel;
		private System.Windows.Forms.Label fieldValueLabel;
		private System.Windows.Forms.Button editFieldButton;
	}
}
