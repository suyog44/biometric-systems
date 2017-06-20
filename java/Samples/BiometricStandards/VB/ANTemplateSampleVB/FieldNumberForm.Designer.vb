Imports Microsoft.VisualBasic
Imports System
Partial Public Class FieldNumberForm
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
		Me.standardFieldsLabel = New System.Windows.Forms.Label()
		Me.lvStandardField = New System.Windows.Forms.ListView()
		Me.fieldNumberColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.fieldNameColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.fieldVersionColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.isFieldMantadoryColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.rbStandardField = New System.Windows.Forms.RadioButton()
		Me.userDefinedFieldsLabel = New System.Windows.Forms.Label()
		Me.rbUserDefinedField = New System.Windows.Forms.RadioButton()
		Me.tbUserDefinedField = New System.Windows.Forms.TextBox()
		Me.userDefinedFieldNumbersLabel = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		' 
		' standardFieldsLabel
		' 
		Me.standardFieldsLabel.AutoSize = True
		Me.standardFieldsLabel.Location = New System.Drawing.Point(12, 9)
		Me.standardFieldsLabel.Name = "standardFieldsLabel"
		Me.standardFieldsLabel.Size = New System.Drawing.Size(80, 13)
		Me.standardFieldsLabel.TabIndex = 1
		Me.standardFieldsLabel.Text = "Standard fields:"
		' 
		' lvStandardField
		' 
		Me.lvStandardField.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.lvStandardField.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.fieldNumberColumnHeader, Me.fieldNameColumnHeader, Me.fieldVersionColumnHeader, Me.isFieldMantadoryColumnHeader})
		Me.lvStandardField.FullRowSelect = True
		Me.lvStandardField.GridLines = True
		Me.lvStandardField.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.lvStandardField.HideSelection = False
		Me.lvStandardField.Location = New System.Drawing.Point(12, 25)
		Me.lvStandardField.MultiSelect = False
		Me.lvStandardField.Name = "lvStandardField"
		Me.lvStandardField.Size = New System.Drawing.Size(461, 187)
		Me.lvStandardField.TabIndex = 2
		Me.lvStandardField.UseCompatibleStateImageBehavior = False
		Me.lvStandardField.View = System.Windows.Forms.View.Details
'		Me.lvStandardField.SelectedIndexChanged += New System.EventHandler(Me.LvStandardFieldSelectedIndexChanged);
'		Me.lvStandardField.DoubleClick += New System.EventHandler(Me.LvStandardFieldDoubleClick);
		' 
		' fieldNumberColumnHeader
		' 
		Me.fieldNumberColumnHeader.Text = "Number"
		' 
		' fieldNameColumnHeader
		' 
		Me.fieldNameColumnHeader.Text = "Name"
		Me.fieldNameColumnHeader.Width = 250
		' 
		' fieldVersionColumnHeader
		' 
		Me.fieldVersionColumnHeader.Text = "Version"
		Me.fieldVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		' 
		' isFieldMantadoryColumnHeader
		' 
		Me.isFieldMantadoryColumnHeader.Text = "Mandatory"
		Me.isFieldMantadoryColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.isFieldMantadoryColumnHeader.Width = 70
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(317, 286)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 7
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(398, 286)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 8
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' rbStandardField
		' 
		Me.rbStandardField.AutoCheck = False
		Me.rbStandardField.AutoSize = True
		Me.rbStandardField.Location = New System.Drawing.Point(15, 7)
		Me.rbStandardField.Name = "rbStandardField"
		Me.rbStandardField.Size = New System.Drawing.Size(93, 17)
		Me.rbStandardField.TabIndex = 0
		Me.rbStandardField.TabStop = True
		Me.rbStandardField.Text = "Standard field:"
		Me.rbStandardField.UseVisualStyleBackColor = True
'		Me.rbStandardField.Click += New System.EventHandler(Me.RbStandardFieldClick);
		' 
		' userDefinedFieldsLabel
		' 
		Me.userDefinedFieldsLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.userDefinedFieldsLabel.AutoSize = True
		Me.userDefinedFieldsLabel.Location = New System.Drawing.Point(12, 228)
		Me.userDefinedFieldsLabel.Name = "userDefinedFieldsLabel"
		Me.userDefinedFieldsLabel.Size = New System.Drawing.Size(97, 13)
		Me.userDefinedFieldsLabel.TabIndex = 4
		Me.userDefinedFieldsLabel.Text = "User defined fields:"
		' 
		' rbUserDefinedField
		' 
		Me.rbUserDefinedField.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.rbUserDefinedField.AutoCheck = False
		Me.rbUserDefinedField.AutoSize = True
		Me.rbUserDefinedField.Location = New System.Drawing.Point(15, 226)
		Me.rbUserDefinedField.Name = "rbUserDefinedField"
		Me.rbUserDefinedField.Size = New System.Drawing.Size(110, 17)
		Me.rbUserDefinedField.TabIndex = 3
		Me.rbUserDefinedField.TabStop = True
		Me.rbUserDefinedField.Text = "User defined field:"
		Me.rbUserDefinedField.UseVisualStyleBackColor = True
'		Me.rbUserDefinedField.Click += New System.EventHandler(Me.RbUserDefinedFieldClick);
		' 
		' tbUserDefinedField
		' 
		Me.tbUserDefinedField.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.tbUserDefinedField.Location = New System.Drawing.Point(15, 249)
		Me.tbUserDefinedField.Name = "tbUserDefinedField"
		Me.tbUserDefinedField.Size = New System.Drawing.Size(100, 20)
		Me.tbUserDefinedField.TabIndex = 6
		' 
		' userDefinedFieldNumbersLabel
		' 
		Me.userDefinedFieldNumbersLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
		Me.userDefinedFieldNumbersLabel.AutoSize = True
		Me.userDefinedFieldNumbersLabel.Location = New System.Drawing.Point(143, 228)
		Me.userDefinedFieldNumbersLabel.Name = "userDefinedFieldNumbersLabel"
		Me.userDefinedFieldNumbersLabel.Size = New System.Drawing.Size(34, 13)
		Me.userDefinedFieldNumbersLabel.TabIndex = 5
		Me.userDefinedFieldNumbersLabel.Text = "(0 - 0)"
		' 
		' FieldNumberForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(485, 321)
		Me.Controls.Add(Me.userDefinedFieldNumbersLabel)
		Me.Controls.Add(Me.tbUserDefinedField)
		Me.Controls.Add(Me.rbUserDefinedField)
		Me.Controls.Add(Me.userDefinedFieldsLabel)
		Me.Controls.Add(Me.rbStandardField)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.lvStandardField)
		Me.Controls.Add(Me.standardFieldsLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(300, 300)
		Me.Name = "FieldNumberForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Field Number"
'		Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.FieldNumberFormClosing);
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private standardFieldsLabel As System.Windows.Forms.Label
	Private WithEvents lvStandardField As System.Windows.Forms.ListView
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private fieldNumberColumnHeader As System.Windows.Forms.ColumnHeader
	Private fieldNameColumnHeader As System.Windows.Forms.ColumnHeader
	Private isFieldMantadoryColumnHeader As System.Windows.Forms.ColumnHeader
	Private WithEvents rbStandardField As System.Windows.Forms.RadioButton
	Private userDefinedFieldsLabel As System.Windows.Forms.Label
	Private WithEvents rbUserDefinedField As System.Windows.Forms.RadioButton
	Private tbUserDefinedField As System.Windows.Forms.TextBox
	Private userDefinedFieldNumbersLabel As System.Windows.Forms.Label
	Private fieldVersionColumnHeader As System.Windows.Forms.ColumnHeader
End Class