Imports Microsoft.VisualBasic
Imports System
Partial Public Class FieldForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.editSubFieldButton = New System.Windows.Forms.Button()
		Me.removeSubFieldButton = New System.Windows.Forms.Button()
		Me.insertSubFieldButton = New System.Windows.Forms.Button()
		Me.addSubFieldButton = New System.Windows.Forms.Button()
		Me.subFieldsLabel = New System.Windows.Forms.Label()
		Me.subFieldListBox = New System.Windows.Forms.ListBox()
		Me.editItemButton = New System.Windows.Forms.Button()
		Me.removeItemButton = New System.Windows.Forms.Button()
		Me.insertItemButton = New System.Windows.Forms.Button()
		Me.addItemButton = New System.Windows.Forms.Button()
		Me.itemsLabel = New System.Windows.Forms.Label()
		Me.itemListBox = New System.Windows.Forms.ListBox()
		Me.closeButton = New System.Windows.Forms.Button()
		Me.valueLabel = New System.Windows.Forms.Label()
		Me.fieldValueLabel = New System.Windows.Forms.Label()
		Me.editFieldButton = New System.Windows.Forms.Button()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.SuspendLayout()
		' 
		' splitContainer
		' 
		Me.splitContainer.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.splitContainer.Location = New System.Drawing.Point(12, 43)
		Me.splitContainer.Name = "splitContainer"
		' 
		' splitContainer.Panel1
		' 
		Me.splitContainer.Panel1.Controls.Add(Me.editSubFieldButton)
		Me.splitContainer.Panel1.Controls.Add(Me.removeSubFieldButton)
		Me.splitContainer.Panel1.Controls.Add(Me.insertSubFieldButton)
		Me.splitContainer.Panel1.Controls.Add(Me.addSubFieldButton)
		Me.splitContainer.Panel1.Controls.Add(Me.subFieldsLabel)
		Me.splitContainer.Panel1.Controls.Add(Me.subFieldListBox)
		' 
		' splitContainer.Panel2
		' 
		Me.splitContainer.Panel2.Controls.Add(Me.editItemButton)
		Me.splitContainer.Panel2.Controls.Add(Me.removeItemButton)
		Me.splitContainer.Panel2.Controls.Add(Me.insertItemButton)
		Me.splitContainer.Panel2.Controls.Add(Me.addItemButton)
		Me.splitContainer.Panel2.Controls.Add(Me.itemsLabel)
		Me.splitContainer.Panel2.Controls.Add(Me.itemListBox)
		Me.splitContainer.Size = New System.Drawing.Size(638, 253)
		Me.splitContainer.SplitterDistance = 296
		Me.splitContainer.TabIndex = 3
		' 
		' editSubFieldButton
		' 
		Me.editSubFieldButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.editSubFieldButton.Location = New System.Drawing.Point(204, 221)
		Me.editSubFieldButton.Name = "editSubFieldButton"
		Me.editSubFieldButton.Size = New System.Drawing.Size(60, 23)
		Me.editSubFieldButton.TabIndex = 5
		Me.editSubFieldButton.Text = "Edi&t"
		Me.editSubFieldButton.UseVisualStyleBackColor = True
'		Me.editSubFieldButton.Click += New System.EventHandler(Me.EditSubFieldButtonClick);
		' 
		' removeSubFieldButton
		' 
		Me.removeSubFieldButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.removeSubFieldButton.Location = New System.Drawing.Point(138, 221)
		Me.removeSubFieldButton.Name = "removeSubFieldButton"
		Me.removeSubFieldButton.Size = New System.Drawing.Size(60, 23)
		Me.removeSubFieldButton.TabIndex = 4
		Me.removeSubFieldButton.Text = "Re&move"
		Me.removeSubFieldButton.UseVisualStyleBackColor = True
'		Me.removeSubFieldButton.Click += New System.EventHandler(Me.RemoveSubFieldButtonClick);
		' 
		' insertSubFieldButton
		' 
		Me.insertSubFieldButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.insertSubFieldButton.Location = New System.Drawing.Point(72, 221)
		Me.insertSubFieldButton.Name = "insertSubFieldButton"
		Me.insertSubFieldButton.Size = New System.Drawing.Size(60, 23)
		Me.insertSubFieldButton.TabIndex = 3
		Me.insertSubFieldButton.Text = "I&nsert"
		Me.insertSubFieldButton.UseVisualStyleBackColor = True
'		Me.insertSubFieldButton.Click += New System.EventHandler(Me.InsertSubFieldButtonClick);
		' 
		' addSubFieldButton
		' 
		Me.addSubFieldButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.addSubFieldButton.Location = New System.Drawing.Point(6, 221)
		Me.addSubFieldButton.Name = "addSubFieldButton"
		Me.addSubFieldButton.Size = New System.Drawing.Size(60, 23)
		Me.addSubFieldButton.TabIndex = 2
		Me.addSubFieldButton.Text = "A&dd"
		Me.addSubFieldButton.UseVisualStyleBackColor = True
'		Me.addSubFieldButton.Click += New System.EventHandler(Me.AddSubFieldButtonClick);
		' 
		' subFieldsLabel
		' 
		Me.subFieldsLabel.AutoSize = True
		Me.subFieldsLabel.Location = New System.Drawing.Point(3, 0)
		Me.subFieldsLabel.Name = "subFieldsLabel"
		Me.subFieldsLabel.Size = New System.Drawing.Size(53, 13)
		Me.subFieldsLabel.TabIndex = 0
		Me.subFieldsLabel.Text = "Subfields:"
		' 
		' subFieldListBox
		' 
		Me.subFieldListBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.subFieldListBox.FormattingEnabled = True
		Me.subFieldListBox.HorizontalScrollbar = True
		Me.subFieldListBox.Location = New System.Drawing.Point(0, 16)
		Me.subFieldListBox.Name = "subFieldListBox"
		Me.subFieldListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.subFieldListBox.Size = New System.Drawing.Size(294, 186)
		Me.subFieldListBox.TabIndex = 1
'		Me.subFieldListBox.SelectedIndexChanged += New System.EventHandler(Me.SubFieldListBoxSelectedIndexChanged);
'		Me.subFieldListBox.DoubleClick += New System.EventHandler(Me.SubFieldListBoxDoubleClick);
		' 
		' editItemButton
		' 
		Me.editItemButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.editItemButton.Location = New System.Drawing.Point(204, 221)
		Me.editItemButton.Name = "editItemButton"
		Me.editItemButton.Size = New System.Drawing.Size(60, 23)
		Me.editItemButton.TabIndex = 5
		Me.editItemButton.Text = "&Edit"
		Me.editItemButton.UseVisualStyleBackColor = True
'		Me.editItemButton.Click += New System.EventHandler(Me.EditItemButtonClick);
		' 
		' removeItemButton
		' 
		Me.removeItemButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.removeItemButton.Location = New System.Drawing.Point(138, 221)
		Me.removeItemButton.Name = "removeItemButton"
		Me.removeItemButton.Size = New System.Drawing.Size(60, 23)
		Me.removeItemButton.TabIndex = 4
		Me.removeItemButton.Text = "&Remove"
		Me.removeItemButton.UseVisualStyleBackColor = True
'		Me.removeItemButton.Click += New System.EventHandler(Me.RemoveItemButtonClick);
		' 
		' insertItemButton
		' 
		Me.insertItemButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.insertItemButton.Location = New System.Drawing.Point(72, 221)
		Me.insertItemButton.Name = "insertItemButton"
		Me.insertItemButton.Size = New System.Drawing.Size(60, 23)
		Me.insertItemButton.TabIndex = 3
		Me.insertItemButton.Text = "&Insert"
		Me.insertItemButton.UseVisualStyleBackColor = True
'		Me.insertItemButton.Click += New System.EventHandler(Me.InsertItemButtonClick);
		' 
		' addItemButton
		' 
		Me.addItemButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.addItemButton.Location = New System.Drawing.Point(6, 221)
		Me.addItemButton.Name = "addItemButton"
		Me.addItemButton.Size = New System.Drawing.Size(60, 23)
		Me.addItemButton.TabIndex = 2
		Me.addItemButton.Text = "&Add"
		Me.addItemButton.UseVisualStyleBackColor = True
'		Me.addItemButton.Click += New System.EventHandler(Me.AddItemButtonClick);
		' 
		' itemsLabel
		' 
		Me.itemsLabel.AutoSize = True
		Me.itemsLabel.Location = New System.Drawing.Point(3, 0)
		Me.itemsLabel.Name = "itemsLabel"
		Me.itemsLabel.Size = New System.Drawing.Size(35, 13)
		Me.itemsLabel.TabIndex = 0
		Me.itemsLabel.Text = "Items:"
		' 
		' itemListBox
		' 
		Me.itemListBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.itemListBox.FormattingEnabled = True
		Me.itemListBox.HorizontalScrollbar = True
		Me.itemListBox.Location = New System.Drawing.Point(3, 16)
		Me.itemListBox.Name = "itemListBox"
		Me.itemListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.itemListBox.Size = New System.Drawing.Size(335, 186)
		Me.itemListBox.TabIndex = 1
'		Me.itemListBox.SelectedIndexChanged += New System.EventHandler(Me.ItemListBoxSelectedIndexChanged);
'		Me.itemListBox.DoubleClick += New System.EventHandler(Me.ItemListBoxDoubleClick);
		' 
		' closeButton
		' 
		Me.closeButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.closeButton.Location = New System.Drawing.Point(575, 316)
		Me.closeButton.Name = "closeButton"
		Me.closeButton.Size = New System.Drawing.Size(75, 23)
		Me.closeButton.TabIndex = 4
		Me.closeButton.Text = "Close"
		Me.closeButton.UseVisualStyleBackColor = True
		' 
		' valueLabel
		' 
		Me.valueLabel.AutoSize = True
		Me.valueLabel.Location = New System.Drawing.Point(12, 17)
		Me.valueLabel.Name = "valueLabel"
		Me.valueLabel.Size = New System.Drawing.Size(37, 13)
		Me.valueLabel.TabIndex = 0
		Me.valueLabel.Text = "&Value:"
		' 
		' fieldValueLabel
		' 
		Me.fieldValueLabel.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.fieldValueLabel.AutoEllipsis = True
		Me.fieldValueLabel.Location = New System.Drawing.Point(55, 17)
		Me.fieldValueLabel.Name = "fieldValueLabel"
		Me.fieldValueLabel.Size = New System.Drawing.Size(529, 13)
		Me.fieldValueLabel.TabIndex = 2
'		Me.fieldValueLabel.DoubleClick += New System.EventHandler(Me.FieldValueLabelDoubleClick);
		' 
		' editFieldButton
		' 
		Me.editFieldButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.editFieldButton.Location = New System.Drawing.Point(590, 12)
		Me.editFieldButton.Name = "editFieldButton"
		Me.editFieldButton.Size = New System.Drawing.Size(60, 23)
		Me.editFieldButton.TabIndex = 1
		Me.editFieldButton.Text = "Edit"
		Me.editFieldButton.UseVisualStyleBackColor = True
'		Me.editFieldButton.Click += New System.EventHandler(Me.EditFieldButtonClick);
		' 
		' FieldForm
		' 
		Me.AcceptButton = Me.closeButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.closeButton
		Me.ClientSize = New System.Drawing.Size(662, 351)
		Me.Controls.Add(Me.editFieldButton)
		Me.Controls.Add(Me.fieldValueLabel)
		Me.Controls.Add(Me.valueLabel)
		Me.Controls.Add(Me.closeButton)
		Me.Controls.Add(Me.splitContainer)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(600, 300)
		Me.Name = "FieldForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Edit Field"
'		Me.Shown += New System.EventHandler(Me.FieldFormShown);
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel1.PerformLayout()
		Me.splitContainer.Panel2.ResumeLayout(False)
		Me.splitContainer.Panel2.PerformLayout()
		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private splitContainer As System.Windows.Forms.SplitContainer
	Private closeButton As System.Windows.Forms.Button
	Private WithEvents subFieldListBox As System.Windows.Forms.ListBox
	Private WithEvents itemListBox As System.Windows.Forms.ListBox
	Private subFieldsLabel As System.Windows.Forms.Label
	Private itemsLabel As System.Windows.Forms.Label
	Private WithEvents removeSubFieldButton As System.Windows.Forms.Button
	Private WithEvents insertSubFieldButton As System.Windows.Forms.Button
	Private WithEvents addSubFieldButton As System.Windows.Forms.Button
	Private WithEvents editItemButton As System.Windows.Forms.Button
	Private WithEvents removeItemButton As System.Windows.Forms.Button
	Private WithEvents insertItemButton As System.Windows.Forms.Button
	Private WithEvents addItemButton As System.Windows.Forms.Button
	Private WithEvents editSubFieldButton As System.Windows.Forms.Button
	Private valueLabel As System.Windows.Forms.Label
	Private WithEvents fieldValueLabel As System.Windows.Forms.Label
	Private WithEvents editFieldButton As System.Windows.Forms.Button
End Class