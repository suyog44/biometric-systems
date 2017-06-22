Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class MatchingResultView
	Inherits UserControl
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _linkEnabled As Boolean = True
	Private _matchingResult As NMatchingResult
	Private _matchingThreshold As Integer

	#End Region

#Region "Public properties"

	Public Property MatchingThreshold() As Integer
		Get
			Return _matchingThreshold
		End Get
		Set(ByVal value As Integer)
			_matchingThreshold = value
		End Set
	End Property

	Public Property LinkEnabled() As Boolean
		Get
			Return _linkEnabled
		End Get
		Set(ByVal value As Boolean)
			_linkEnabled = value
			linkLabel.Enabled = value
			linkLabel.ForeColor = If(value, Color.Green, Color.Black)
			linkLabel.LinkBehavior = If(value, LinkBehavior.SystemDefault, LinkBehavior.NeverUnderline)
		End Set
	End Property

	Public Property Result() As NMatchingResult
		Get
			Return _matchingResult
		End Get
		Set(ByVal value As NMatchingResult)
			_matchingResult = value
			linkLabel.Text = String.Empty
			lblDetails.Text = String.Empty
			If _matchingResult IsNot Nothing Then
				linkLabel.Text = String.Format("Matched with '{0}', score  = {1}", _matchingResult.Id, _matchingResult.Score)
				lblDetails.Text = MatchingResultToString(_matchingResult)
			End If
		End Set
	End Property

#End Region

	#Region "Public events"

	Public Event LinkActivated As EventHandler

	#End Region

	#Region "Protected methods"

	Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
		_matchingResult = Nothing
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#End Region

	#Region "Private methods"

	Private Function MatchingResultToString(ByVal result As NMatchingResult) As String
		Dim index As Integer
		Dim details As NMatchingDetails = result.MatchingDetails

		Dim builder As New StringBuilder()
		If details IsNot Nothing Then
			Dim belowThreshold = " (Below matching threshold)"
			If (details.BiometricType And NBiometricType.Face) = NBiometricType.Face Then
				builder.AppendFormat("Face match details: score = {0}{1}", details.FacesScore, Environment.NewLine)
				index = 0
				For Each faceDetails As NLMatchingDetails In details.Faces
					If faceDetails.MatchedIndex <> -1 Then
						builder.AppendFormat("    face index {0}: matched with index {1}, score = {2}{3};{4}", index, faceDetails.MatchedIndex, faceDetails.Score, If(faceDetails.Score < MatchingThreshold, belowThreshold, String.Empty), Environment.NewLine)
						index += 1
					Else
						builder.AppendFormat("    face index {0}: doesn't match{1}", index, Environment.NewLine)
						index += 1
					End If
				Next faceDetails
			End If
			If (details.BiometricType And NBiometricType.Finger) = NBiometricType.Finger Then
				builder.AppendFormat("Fingerprint match details: score = {0}{1}", details.FingersScore, Environment.NewLine)
				index = 0
				For Each fngrDetails As NFMatchingDetails In details.Fingers
					If fngrDetails.MatchedIndex <> -1 Then
						builder.AppendFormat("    fingerprint index {0}: matched with index {1}, score = {2}{3};{4}", index, fngrDetails.MatchedIndex, fngrDetails.Score, If(fngrDetails.Score < MatchingThreshold, belowThreshold, String.Empty), Environment.NewLine)
						index += 1
					Else
						builder.AppendFormat("    fingerprint index: {0}: doesn't match{1}", index, Environment.NewLine)
						index += 1
					End If
				Next fngrDetails
			End If
			If (details.BiometricType And NBiometricType.Iris) = NBiometricType.Iris Then
				builder.AppendFormat("Irises match details: score = {0}{1}", details.IrisesScore, Environment.NewLine)
				index = 0
				For Each irisesDetails As NEMatchingDetails In details.Irises
					If irisesDetails.MatchedIndex <> -1 Then
						builder.AppendFormat("    irises index: {0}: matched with index {1}, score = {2}{3};{4}", index, irisesDetails.MatchedIndex, irisesDetails.Score, If(irisesDetails.Score < MatchingThreshold, belowThreshold, String.Empty), Environment.NewLine)
						index += 1
					Else
						builder.AppendFormat("    irises index: {0}: doesn't match{1}", index, Environment.NewLine)
						index += 1
					End If
				Next irisesDetails
			End If
			If (details.BiometricType And NBiometricType.Palm) = NBiometricType.Palm Then
				builder.AppendFormat("Palmprint match details: score = {0}{1}", details.PalmsScore, Environment.NewLine)
				index = 0
				For Each fngrDetails As NFMatchingDetails In details.Palms
					If fngrDetails.MatchedIndex <> -1 Then
						builder.AppendFormat("    palmprint index {0}: matched with index {1}, score = {2}{3};{4}", index, fngrDetails.MatchedIndex, fngrDetails.Score, If(fngrDetails.Score < MatchingThreshold, belowThreshold, String.Empty), Environment.NewLine)
						index += 1
					Else
						builder.AppendFormat("    palmprint index: {0}: doesn't match{1}", index, Environment.NewLine)
						index += 1
					End If
				Next fngrDetails
			End If
			If (details.BiometricType And NBiometricType.Voice) = NBiometricType.Voice Then
				builder.AppendFormat("Voice match details: score = {0}{1}", details.VoicesScore, Environment.NewLine)
				index = 0
				For Each voicesDetails As NSMatchingDetails In details.Voices
					If voicesDetails.MatchedIndex <> -1 Then
						builder.AppendFormat("    voices index {0}: matched with index {1}, score = {2}{3};{4}", index, voicesDetails.MatchedIndex, voicesDetails.Score, If(voicesDetails.Score < MatchingThreshold, belowThreshold, String.Empty), Environment.NewLine)
						index += 1
					Else
						builder.AppendFormat("    voices index {0}: doesn't match{1}", index, Environment.NewLine)
						index += 1
					End If
				Next voicesDetails
			End If

			If (details.BiometricType And (NBiometricType.Finger Or NBiometricType.Palm Or NBiometricType.Iris Or NBiometricType.Voice Or NBiometricType.Face)) = NBiometricType.None Then
				builder.AppendFormat(" score = {0}", details.Score)
			End If
		End If

		Return builder.ToString()
	End Function

	Private Sub LinkLabelLinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles linkLabel.LinkClicked
		RaiseEvent LinkActivated(Me, EventArgs.Empty)
	End Sub

	#End Region
End Class
