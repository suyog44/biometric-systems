Imports Microsoft.VisualBasic
Imports System
Partial Public Class PalmSelector
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

	#Region "Component Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.SuspendLayout()
		' 
		' PalmSelector
		' 
		Me.AllowedPositions = New Neurotec.Biometrics.NFPosition() { Neurotec.Biometrics.NFPosition.RightThumb, Neurotec.Biometrics.NFPosition.RightIndexFinger, Neurotec.Biometrics.NFPosition.RightMiddleFinger, Neurotec.Biometrics.NFPosition.RightRingFinger, Neurotec.Biometrics.NFPosition.RightLittleFinger, Neurotec.Biometrics.NFPosition.LeftThumb, Neurotec.Biometrics.NFPosition.LeftIndexFinger, Neurotec.Biometrics.NFPosition.LeftMiddleFinger, Neurotec.Biometrics.NFPosition.LeftRingFinger, Neurotec.Biometrics.NFPosition.LeftLittle}
		Me.MissingPositions = New Neurotec.Biometrics.NFPosition(){}
		Me.Text = "fc"
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private toolTip As System.Windows.Forms.ToolTip
End Class
