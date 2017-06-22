Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class CaptureIcaoCompliantImage
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _face As NFace
	Private _segmentedFace As NFace
	Private _deviceManager As NDeviceManager

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

	Private Sub UpdateCameraList()
		cbCameras.BeginUpdate()
		Try
			cbCameras.Items.Clear()
			For Each device As NDevice In _deviceManager.Devices
				cbCameras.Items.Add(device)
			Next device

			If _biometricClient.FaceCaptureDevice Is Nothing AndAlso cbCameras.Items.Count > 0 Then
				cbCameras.SelectedIndex = 0
				Return
			End If

			If _biometricClient.FaceCaptureDevice IsNot Nothing Then
				cbCameras.SelectedIndex = cbCameras.Items.IndexOf(_biometricClient.FaceCaptureDevice)
			End If
		Finally
			cbCameras.EndUpdate()
		End Try
	End Sub

	Private Sub EnableControls(ByVal capturing As Boolean)
		Dim hasTemplate As Boolean = (Not capturing) AndAlso _subject IsNot Nothing AndAlso _subject.Status = NBiometricStatus.Ok
		btnSaveImage.Enabled = hasTemplate
		btnSaveTemplate.Enabled = hasTemplate
		btnStart.Enabled = Not capturing
		btnRefreshList.Enabled = Not capturing
		btnStop.Enabled = capturing
		cbCameras.Enabled = Not capturing
		btnForce.Enabled = capturing
	End Sub

	Private Sub OnCapturingCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCapturingCompleted), r)
		Else
			Try
				Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
				Dim status As NBiometricStatus = task.Status

				If task.Error IsNot Nothing Then
					Utils.ShowException(task.Error)
				End If
				If status = NBiometricStatus.Ok Then
					_segmentedFace = _subject.Faces(1)
					faceView.Face = _segmentedFace
					icaoWarningView.Face = _segmentedFace
				End If

				lblStatus.Text = status.ToString()
				lblStatus.ForeColor = If(status = NBiometricStatus.Ok, Color.Green, Color.Red)
				EnableControls(False)
			Catch ex As Exception
				Utils.ShowException(ex)
				lblStatus.Text = String.Empty
				EnableControls(False)
			End Try
		End If
	End Sub

#End Region

	Private Sub CaptureIcaoCompliantImageLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			Try
				nViewZoomSlider2.View = faceView
				lblStatus.Text = String.Empty
				_deviceManager = _biometricClient.DeviceManager
				saveImageDialog.Filter = NImages.GetSaveFileFilterString()
				UpdateCameraList()
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub BtnRefreshListClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefreshList.Click
		UpdateCameraList()
	End Sub

	Private Sub CbCamerasSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCameras.SelectedIndexChanged
		_biometricClient.FaceCaptureDevice = TryCast(cbCameras.SelectedItem, NCamera)
	End Sub

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		If _biometricClient.FaceCaptureDevice Is Nothing Then
			MessageBox.Show("Please select camera from the list")
			Return
		End If

		' Set face capture from stream
		_face = New NFace With {.CaptureOptions = NBiometricCaptureOptions.Stream}
		_subject = New NSubject()
		_subject.Faces.Add(_face)
		faceView.Face = _face
		IcaoWarningView.Face = _face

		_biometricClient.FacesCheckIcaoCompliance = True

		Dim task = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate, _subject)
		_biometricClient.BeginPerformTask(task, AddressOf OnCapturingCompleted, Nothing)

		lblStatus.Text = String.Empty
		EnableControls(True)
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		_biometricClient.Cancel()
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If saveTemplateDialog.ShowDialog() = DialogResult.OK Then
			File.WriteAllBytes(saveTemplateDialog.FileName, _subject.GetTemplateBuffer().ToArray())
		End If
	End Sub

	Private Sub BtnSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
		If saveImageDialog.ShowDialog() = DialogResult.OK Then
			_segmentedFace.Image.Save(saveImageDialog.FileName)
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnForce.Click
		_biometricClient.Force()
	End Sub
End Class
