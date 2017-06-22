Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class MatchMultipleFaces
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _referenceSubject As NSubject
	Private _multipleFacesSubject As NSubject

#End Region

#Region "Public properties"

	Public Property BiometricClient() As NBiometricClient
		Get
			Return _biometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			_biometricClient = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub ExtractReferenceFace(ByVal imagePath As String)
		Dim face As New NFace() With {.FileName = imagePath}
		_referenceSubject = New NSubject()
		_referenceSubject.Faces.Add(face)
		faceViewReference.Face = face
		_biometricClient.BeginCreateTemplate(_referenceSubject, AddressOf OnReferenceExtractionCompleted, Nothing)
	End Sub

	Private Sub OnReferenceExtractionCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnReferenceExtractionCompleted), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndCreateTemplate(r)
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show("Could not extract template from reference image.")
					_referenceSubject = Nothing
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
				_referenceSubject = Nothing
			End Try
		End If
	End Sub

	Private Sub ExtractMultipleFace(ByVal imagePath As String)
		Dim face As New NFace() With {.FileName = imagePath}
		_multipleFacesSubject = New NSubject()
		_multipleFacesSubject.Faces.Add(face)
		faceViewMultiFace.Face = face
		' Image can have more than one faces
		_multipleFacesSubject.IsMultipleSubjects = True
		_biometricClient.BeginCreateTemplate(_multipleFacesSubject, AddressOf OnMultipleExtractionCompleted, Nothing)
	End Sub

	Private Sub OnMultipleExtractionCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnMultipleExtractionCompleted), r)
		Else
			Dim status As NBiometricStatus = NBiometricStatus.None
			Try
				status = _biometricClient.EndCreateTemplate(r)
				If status = NBiometricStatus.Ok Then
					' Enroll extracted faces
					EnrollMultipleFaceSubject()
				Else
					MessageBox.Show("Could not extract template from multiple face image.")
					_multipleFacesSubject = Nothing
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
				_multipleFacesSubject = Nothing
			End Try
		End If
	End Sub

	Private Sub EnrollMultipleFaceSubject()
		_biometricClient.Clear()
		Dim enrollTask As New NBiometricTask(NBiometricOperations.Enroll)

		' Enroll all faces
		_multipleFacesSubject.Id = "firstSubject"
		enrollTask.Subjects.Add(_multipleFacesSubject)

		Dim tmpSubject As NSubject
		For i As Integer = 0 To _multipleFacesSubject.RelatedSubjects.Count - 1
			tmpSubject = _multipleFacesSubject.RelatedSubjects(i)
			tmpSubject.Id = "relatedSubject" & i.ToString()
			enrollTask.Subjects.Add(tmpSubject)
		Next i
		_biometricClient.BeginPerformTask(enrollTask, AddressOf OnEnrollCompleted, Nothing)
	End Sub

	Private Sub OnEnrollCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnEnrollCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			If task.Status <> NBiometricStatus.Ok Then
				MessageBox.Show(String.Format("Enroll failed: {0}", task.Status))
			ElseIf _referenceSubject IsNot Nothing Then
				MatchFaces()
			End If
		End If
	End Sub

	Private Sub MatchFaces()
		_biometricClient.BeginIdentify(_referenceSubject, AddressOf OnIdentifyCompleted, Nothing)
	End Sub

	Private Sub OnIdentifyCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnIdentifyCompleted), r)
		Else
			Dim multipleFacesCount As Integer = _multipleFacesSubject.Faces.Count + _multipleFacesSubject.RelatedSubjects.Count
			Dim results(multipleFacesCount - 1) As String
			' Get matching scores
			For i As Integer = 0 To _referenceSubject.MatchingResults.Count - 1
				Dim score As Integer = _referenceSubject.MatchingResults(i).Score
				If _referenceSubject.MatchingResults(i).Id = _multipleFacesSubject.Id Then
					results(0) = String.Format("score: {0} (match)", score)
				Else
					For j As Integer = 0 To _multipleFacesSubject.RelatedSubjects.Count - 1
						If _referenceSubject.MatchingResults(i).Id = _multipleFacesSubject.RelatedSubjects(j).Id Then
							results(j + 1) = String.Format("score: {0} (match)", score)
						End If
					Next j
				End If
			Next i

			' All not matched faces have score 0
			For i As Integer = 0 To results.Length - 1
				If results(i) Is Nothing Then
					results(i) = String.Format("score: 0")
				End If
			Next i
			faceViewMultiFace.FaceIds = results
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub TsbOpenReferenceClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenReference.Click
		openImageFileDlg.Filter = NImages.GetOpenFileFilterString(True, True)
		openImageFileDlg.Title = "Open Reference Image"
		If openImageFileDlg.ShowDialog() = DialogResult.OK Then
			Try
				' Set template size (for matching medium is recommended) (optional)
				_biometricClient.FacesTemplateSize = NTemplateSize.Medium
				ExtractReferenceFace(openImageFileDlg.FileName)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub TsbOpenMultifaceImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenMultifaceImage.Click
		openImageFileDlg.Filter = NImages.GetOpenFileFilterString(True, True)
		openImageFileDlg.Title = "Open Reference Image"
		If openImageFileDlg.ShowDialog() = DialogResult.OK Then
			Try
				' Set template size (for enrolling large is recomended) (optional)
				_biometricClient.FacesTemplateSize = NTemplateSize.Large
				ExtractMultipleFace(openImageFileDlg.FileName)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

#End Region
End Class
