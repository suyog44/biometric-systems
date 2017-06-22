Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class CreateTokenFaceImage
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _tokenFace As NFace

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

	Private Sub CreateImage(ByVal imagePath As String)
		Dim face = New NFace With {.FileName = imagePath}
		_subject = New NSubject()
		_subject.Faces.Add(face)
		faceViewOriginal.Face = face
		Dim task = _biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.AssessQuality, _subject)
		_biometricClient.BeginPerformTask(task, AddressOf OnImageCreated, Nothing)
	End Sub

	Private Sub OnImageCreated(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnImageCreated), r)
		Else
			Dim task = _biometricClient.EndPerformTask(r)
			If task.Error Is Nothing Then
				Dim status As NBiometricStatus = task.Status
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show(String.Format("Could not create token face image! Status: {0}", status))
					_subject = Nothing
				Else
					_tokenFace = _subject.Faces(1)
					faceViewToken.Face = _tokenFace
					btnSaveImage.Enabled = True
					ShowTokenAttributes()
				End If
			Else
				Utils.ShowException(task.Error)
				_subject = Nothing
			End If
		End If
	End Sub

	Private Sub ShowTokenAttributes()
		Dim attributes = _tokenFace.Objects(0)
		lbQuality.Text = String.Format("Quality: {0}", attributes.Quality)
		lbSharpness.Text = String.Format("Sharpness score: {0}", attributes.Sharpness)
		lbUniformity.Text = String.Format("Background uniformity score: {0}", attributes.BackgroundUniformity)
		lbDensity.Text = String.Format("Grayscale density score: {0}", attributes.GrayscaleDensity)
		ShowAttributeLabels(True)
	End Sub

	Private Sub ShowAttributeLabels(ByVal show As Boolean)
		lbQuality.Visible = show
		lbSharpness.Visible = show
		lbUniformity.Visible = show
		lbDensity.Visible = show
	End Sub

#End Region

#Region "Private form events"

	Private Sub TsbOpenOriginalClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenImage.Click
		faceViewToken.Face = Nothing
		faceViewOriginal.Face = Nothing
		_tokenFace = Nothing
		btnSaveImage.Enabled = False
		ShowAttributeLabels(False)

		openImageFileDlg.Filter = NImages.GetOpenFileFilterString(True, True)
		openImageFileDlg.Title = "Open Face Image"
		If openImageFileDlg.ShowDialog() = DialogResult.OK Then
			Try
				CreateImage(openImageFileDlg.FileName)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub BtnSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
		If _tokenFace Is Nothing Then
			Return
		End If

		saveFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		If saveFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				_tokenFace.Image.Save(saveFileDialog.FileName)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

#End Region

End Class
