Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureDeviceForm
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
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' customizeFormatButton
		' 
		Me.customizeFormatButton.Enabled = True
		' 
		' CaptureDeviceForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.ClientSize = New System.Drawing.Size(664, 544)
		Me.Name = "CaptureDeviceForm"
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region
End Class
