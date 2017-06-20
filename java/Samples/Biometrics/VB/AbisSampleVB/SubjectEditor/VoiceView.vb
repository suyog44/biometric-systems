Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class VoiceView
	Inherits UserControl
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _voice As NVoice

	#End Region

	#Region "Public properties"

	Public Property Voice() As NVoice
		Get
			Return _voice
		End Get
		Set(ByVal value As NVoice)
			_voice = value
			If value IsNot Nothing Then
				lblPhraseId.Text = value.PhraseId.ToString()
				Dim phrase As Phrase = SettingsManager.Phrases.FirstOrDefault(Function(x) x.Id = value.PhraseId)
				lblPhrase.Text = If(phrase IsNot Nothing, phrase.String, "N/A")

				Dim attributes As NSAttributes = _voice.Objects.FirstOrDefault()
				If attributes IsNot Nothing Then
					Dim quality As Byte = attributes.Quality
					Select Case quality
						Case NBiometricTypes.QualityUnknown
							lblQuality.Text = "N/A"
						Case NBiometricTypes.QualityFailed
							lblQuality.Text = "Failed to determine quality"
						Case Else
							lblQuality.Text = attributes.Quality.ToString()
					End Select

					Dim hasTimespan As Boolean = attributes.VoiceStart <> TimeSpan.Zero OrElse attributes.VoiceDuration <> TimeSpan.Zero
					lblStart.Text = If(hasTimespan, attributes.VoiceStart.ToString(), "N/A")
					lblDuration.Text = If(hasTimespan, attributes.VoiceDuration.ToString(), "N/A")
				Else
					lblQuality.Text = "N/A"
					lblDuration.Text = lblQuality.Text
					lblStart.Text = lblDuration.Text
				End If
			Else
				lblQuality.Text = "N/A"
				lblDuration.Text = lblQuality.Text
				lblStart.Text = lblDuration.Text
				lblPhraseId.Text = "-1"
				lblPhrase.Text = "N/A"
			End If
		End Set
	End Property

	#End Region
End Class
