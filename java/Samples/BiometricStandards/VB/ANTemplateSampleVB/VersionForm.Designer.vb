Imports Microsoft.VisualBasic
Imports System
Partial Public Class VersionForm
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
		Me.versionsLabel = New System.Windows.Forms.Label()
		Me.lbVersions = New System.Windows.Forms.ListView()
		Me.versionValueColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.versionNameColumnHeader = New System.Windows.Forms.ColumnHeader()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' versionsLabel
		' 
		Me.versionsLabel.AutoSize = True
		Me.versionsLabel.Location = New System.Drawing.Point(12, 9)
		Me.versionsLabel.Name = "versionsLabel"
		Me.versionsLabel.Size = New System.Drawing.Size(50, 13)
		Me.versionsLabel.TabIndex = 0
		Me.versionsLabel.Text = "Versions:"
		' 
		' lbVersions
		' 
		Me.lbVersions.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.lbVersions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.versionValueColumnHeader, Me.versionNameColumnHeader})
		Me.lbVersions.FullRowSelect = True
		Me.lbVersions.GridLines = True
		Me.lbVersions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.lbVersions.HideSelection = False
		Me.lbVersions.Location = New System.Drawing.Point(12, 25)
		Me.lbVersions.MultiSelect = False
		Me.lbVersions.Name = "lbVersions"
		Me.lbVersions.Size = New System.Drawing.Size(347, 123)
		Me.lbVersions.TabIndex = 1
		Me.lbVersions.UseCompatibleStateImageBehavior = False
		Me.lbVersions.View = System.Windows.Forms.View.Details
'		Me.lbVersions.SelectedIndexChanged += New System.EventHandler(Me.LvVersionsSelectedIndexChanged);
'		Me.lbVersions.DoubleClick += New System.EventHandler(Me.LvVersionsDoubleClick);
		' 
		' versionValueColumnHeader
		' 
		Me.versionValueColumnHeader.Text = "Value"
		' 
		' versionNameColumnHeader
		' 
		Me.versionNameColumnHeader.Text = "Name"
		Me.versionNameColumnHeader.Width = 250
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(203, 165)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(284, 165)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' VersionForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(371, 200)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.lbVersions)
		Me.Controls.Add(Me.versionsLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(200, 200)
		Me.Name = "VersionForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Version"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private versionsLabel As System.Windows.Forms.Label
	Private WithEvents lbVersions As System.Windows.Forms.ListView
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private versionValueColumnHeader As System.Windows.Forms.ColumnHeader
	Private versionNameColumnHeader As System.Windows.Forms.ColumnHeader
End Class