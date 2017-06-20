Imports Microsoft.VisualBasic
Imports System

Partial Public Class MainForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.tabControl = New System.Windows.Forms.TabControl
		Me.SuspendLayout()
		'
		'tabControl
		'
		Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tabControl.Location = New System.Drawing.Point(0, 0)
		Me.tabControl.Name = "tabControl"
		Me.tabControl.SelectedIndex = 0
		Me.tabControl.Size = New System.Drawing.Size(1008, 729)
		Me.tabControl.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1008, 729)
		Me.Controls.Add(Me.tabControl)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Simple Faces Sample"
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents tabControl As System.Windows.Forms.TabControl
End Class


