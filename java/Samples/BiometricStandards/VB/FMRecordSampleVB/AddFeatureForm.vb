Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class AddFeatureForm
	Inherits Form
	Public Sub New()
		InitializeComponent()

		cbType.Items.Add("End")
		cbType.Items.Add("Bifurcation")
		cbType.Items.Add("Other")

		cbFeature.SelectedIndex = 0
		cbType.SelectedIndex = 0
	End Sub

	Public ReadOnly Property MinutiaType() As BdifFPMinutiaType
		Get
			Select Case cbType.SelectedIndex
				Case 0
					Return BdifFPMinutiaType.End
				Case 1
					Return BdifFPMinutiaType.Bifurcation
				Case 2
					Return BdifFPMinutiaType.Other
				Case Else
					Return BdifFPMinutiaType.Unknown
			End Select
		End Get
	End Property

	Private Sub cbFeature_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbFeature.SelectedIndexChanged
		If cbFeature.SelectedIndex = 0 Then
			cbType.SelectedItem = 0
			cbType.Enabled = True
			labelType.Enabled = True
		Else
			cbType.SelectedText = ""
			cbType.Enabled = False
			labelType.Enabled = False
		End If
	End Sub
End Class
