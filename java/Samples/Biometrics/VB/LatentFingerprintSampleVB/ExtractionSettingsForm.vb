Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

Partial Public Class ExtractionSettingsForm
	Inherits Form
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Public properties"

	Public Property QualityThreshold() As Byte
		Get
			Return Convert.ToByte(nudThreshold.Value)
		End Get
		Set(ByVal value As Byte)
			nudThreshold.Value = value
		End Set
	End Property

	#End Region

	#Region "Private events"

	Private Sub BtnDefaultThresholdClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefaultThreshold.Click
		nudThreshold.Value = 39
	End Sub

	#End Region
End Class
