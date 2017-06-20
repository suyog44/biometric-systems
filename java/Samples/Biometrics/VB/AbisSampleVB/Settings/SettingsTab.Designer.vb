Imports Microsoft.VisualBasic
Imports System
Partial Public Class SettingsTab
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
		Me.panelPage = New System.Windows.Forms.Panel
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnDefault = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.listViewPages = New System.Windows.Forms.ListView
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'panelPage
		'
		Me.panelPage.AutoScroll = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.panelPage, 3)
		Me.panelPage.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelPage.Location = New System.Drawing.Point(3, 3)
		Me.panelPage.Name = "panelPage"
		Me.panelPage.Size = New System.Drawing.Size(364, 246)
		Me.panelPage.TabIndex = 1
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 3
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.Controls.Add(Me.panelPage, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnOk, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnDefault, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnCancel, 2, 1)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(150, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(370, 281)
		Me.tableLayoutPanel1.TabIndex = 2
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.Location = New System.Drawing.Point(211, 255)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "&OK"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnDefault
		'
		Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnDefault.Location = New System.Drawing.Point(3, 255)
		Me.btnDefault.Name = "btnDefault"
		Me.btnDefault.Size = New System.Drawing.Size(75, 23)
		Me.btnDefault.TabIndex = 4
		Me.btnDefault.Text = "&Default"
		Me.btnDefault.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.Location = New System.Drawing.Point(292, 255)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'listViewPages
		'
		Me.listViewPages.Dock = System.Windows.Forms.DockStyle.Left
		Me.listViewPages.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.listViewPages.FullRowSelect = True
		Me.listViewPages.GridLines = True
		Me.listViewPages.HideSelection = False
		Me.listViewPages.Location = New System.Drawing.Point(0, 0)
		Me.listViewPages.MultiSelect = False
		Me.listViewPages.Name = "listViewPages"
		Me.listViewPages.Size = New System.Drawing.Size(150, 281)
		Me.listViewPages.TabIndex = 0
		Me.listViewPages.UseCompatibleStateImageBehavior = False
		Me.listViewPages.View = System.Windows.Forms.View.List
		'
		'SettingsTab
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.listViewPages)
		Me.Name = "SettingsTab"
		Me.Size = New System.Drawing.Size(520, 281)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private panelPage As System.Windows.Forms.Panel
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnOk As System.Windows.Forms.Button
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private WithEvents listViewPages As System.Windows.Forms.ListView
	Private WithEvents btnDefault As System.Windows.Forms.Button
End Class
