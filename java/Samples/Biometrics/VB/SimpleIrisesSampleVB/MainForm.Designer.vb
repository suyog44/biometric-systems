﻿Imports Microsoft.VisualBasic
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
		Me.TabControl = New System.Windows.Forms.TabControl
		Me.SuspendLayout()
		'
		'TabControl
		'
		Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TabControl.Location = New System.Drawing.Point(0, 0)
		Me.TabControl.Name = "TabControl"
		Me.TabControl.SelectedIndex = 0
		Me.TabControl.Size = New System.Drawing.Size(1008, 729)
		Me.TabControl.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1008, 729)
		Me.Controls.Add(Me.TabControl)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Simple Irises Sample"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents TabControl As System.Windows.Forms.TabControl

#End Region

End Class


