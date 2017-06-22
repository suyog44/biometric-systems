Imports Microsoft.VisualBasic
Imports System
Partial Public Class RecordTypeForm
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
		Me.recordTypeLabel = New System.Windows.Forms.Label()
		Me.lvRecordType = New System.Windows.Forms.ListView()
		Me.recordTypeNumberColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.recordTypeNameColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.recordTypeDataTypeColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.recordTypeVersionColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnShowFields = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' recordTypeLabel
		' 
		Me.recordTypeLabel.AutoSize = True
		Me.recordTypeLabel.Location = New System.Drawing.Point(12, 9)
		Me.recordTypeLabel.Name = "recordTypeLabel"
		Me.recordTypeLabel.Size = New System.Drawing.Size(68, 13)
		Me.recordTypeLabel.TabIndex = 0
		Me.recordTypeLabel.Text = "Record type:"
		' 
		' lvRecordType
		' 
		Me.lvRecordType.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.lvRecordType.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.recordTypeNumberColumnHeader, Me.recordTypeNameColumnHeader, Me.recordTypeDataTypeColumnHeader, Me.recordTypeVersionColumnHeader})
		Me.lvRecordType.FullRowSelect = True
		Me.lvRecordType.GridLines = True
		Me.lvRecordType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.lvRecordType.HideSelection = False
		Me.lvRecordType.Location = New System.Drawing.Point(12, 25)
		Me.lvRecordType.MultiSelect = False
		Me.lvRecordType.Name = "lvRecordType"
		Me.lvRecordType.Size = New System.Drawing.Size(508, 319)
		Me.lvRecordType.TabIndex = 1
		Me.lvRecordType.UseCompatibleStateImageBehavior = False
		Me.lvRecordType.View = System.Windows.Forms.View.Details
'		Me.lvRecordType.SelectedIndexChanged += New System.EventHandler(Me.LvRecordTypeSelectedIndexChanged);
'		Me.lvRecordType.DoubleClick += New System.EventHandler(Me.LvRecordTypeDoubleClick);
		' 
		' recordTypeNumberColumnHeader
		' 
		Me.recordTypeNumberColumnHeader.Text = "Number"
		' 
		' recordTypeNameColumnHeader
		' 
		Me.recordTypeNameColumnHeader.Text = "Name"
		Me.recordTypeNameColumnHeader.Width = 250
		' 
		' recordTypeDataTypeColumnHeader
		' 
		Me.recordTypeDataTypeColumnHeader.Text = "Data type"
		Me.recordTypeDataTypeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.recordTypeDataTypeColumnHeader.Width = 100
		' 
		' recordTypeVersionColumnHeader
		' 
		Me.recordTypeVersionColumnHeader.Text = "Version"
		Me.recordTypeVersionColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(364, 363)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 3
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(445, 363)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' btnShowFields
		' 
		Me.btnShowFields.Location = New System.Drawing.Point(12, 363)
		Me.btnShowFields.Name = "btnShowFields"
		Me.btnShowFields.Size = New System.Drawing.Size(75, 23)
		Me.btnShowFields.TabIndex = 2
		Me.btnShowFields.Text = "Show fields"
		Me.btnShowFields.UseVisualStyleBackColor = True
'		Me.btnShowFields.Click += New System.EventHandler(Me.BtnShowFieldsClick);
		' 
		' RecordTypeForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(532, 398)
		Me.Controls.Add(Me.btnShowFields)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.lvRecordType)
		Me.Controls.Add(Me.recordTypeLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(300, 200)
		Me.Name = "RecordTypeForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Record Type"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private recordTypeLabel As System.Windows.Forms.Label
	Private WithEvents lvRecordType As System.Windows.Forms.ListView
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private recordTypeNumberColumnHeader As System.Windows.Forms.ColumnHeader
	Private recordTypeNameColumnHeader As System.Windows.Forms.ColumnHeader
	Private recordTypeDataTypeColumnHeader As System.Windows.Forms.ColumnHeader
	Private WithEvents btnShowFields As System.Windows.Forms.Button
	Private recordTypeVersionColumnHeader As System.Windows.Forms.ColumnHeader
End Class