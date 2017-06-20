Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Namespace Forms
	Partial Public Class ExtractionOptionsForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			cbTemplateSize.Items.Add(NTemplateSize.Small)
			cbTemplateSize.Items.Add(NTemplateSize.Medium)
			cbTemplateSize.Items.Add(NTemplateSize.Large)
		End Sub

		#End Region

		#Region "Public properties"

		Private privateBiometricClient As NBiometricClient
		Public Property BiometricClient() As NBiometricClient
			Get
				Return privateBiometricClient
			End Get
			Set(ByVal value As NBiometricClient)
				privateBiometricClient = value
			End Set
		End Property

		#End Region

		#Region "Private form events"

		Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
			BiometricClient.ResetProperty("Fingers.TemplateSize")
			BiometricClient.ResetProperty("Fingers.QualityThreshold")
			BiometricClient.ResetProperty("Fingers.MaximalRotation")
			BiometricClient.ResetProperty("Fingers.FastExtraction")
			LoadSettings()
		End Sub

		Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
			If SaveSettings() Then
				DialogResult = System.Windows.Forms.DialogResult.OK
			End If
		End Sub

		Private Sub OptionsFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			If BiometricClient Is Nothing Then
				Throw New ArgumentException("BiometricClient")
			End If
			LoadSettings()
		End Sub

		#End Region

		#Region "Private methods"

		Private Function SaveSettings() As Boolean
			Try
				BiometricClient.FingersTemplateSize = CType(cbTemplateSize.SelectedItem, NTemplateSize)
				BiometricClient.FingersQualityThreshold = Convert.ToByte(nudQualityThreshold.Value)
				BiometricClient.FingersMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value)
				BiometricClient.FingersFastExtraction = chbFastExtraction.Checked
				Return True
			Catch ex As Exception
				Utilities.ShowError("Failed to set value: {0}", ex.Message)
				Return False
			End Try
		End Function

		Private Sub LoadSettings()
			cbTemplateSize.SelectedItem = BiometricClient.FingersTemplateSize
			nudQualityThreshold.Value = BiometricClient.FingersQualityThreshold
			nudMaximalRotation.Value = Convert.ToDecimal(BiometricClient.FingersMaximalRotation)
			chbFastExtraction.Checked = BiometricClient.FingersFastExtraction
		End Sub

		#End Region
	End Class
End Namespace
