Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class EnrollFromCamera
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
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
		Dim hasTemplate = (Not capturing) AndAlso _subject IsNot Nothing AndAlso _subject.Status = NBiometricStatus.Ok
		btnSaveImage.Enabled = hasTemplate
		btnSaveTemplate.Enabled = hasTemplate
		btnStart.Enabled = Not capturing
		btnRefreshList.Enabled = Not capturing
		btnStop.Enabled = capturing
		cbCameras.Enabled = Not capturing
		btnStartExtraction.Enabled = capturing AndAlso Not chbCaptureAutomatically.Checked
		chbCaptureAutomatically.Enabled = Not capturing
		chbCheckLiveness.Enabled = Not capturing
	End Sub

	Private Sub OnCapturingCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCapturingCompleted), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndCapture(r)
				' If Stop button was pushed
				If status = NBiometricStatus.Canceled Then
					Return
				End If

				lblStatus.Text = status.ToString()
				If status <> NBiometricStatus.Ok Then
					' Since capture failed start capturing again
					_subject.Faces(0).Image = Nothing
					_biometricClient.BeginCapture(_subject, AddressOf OnCapturingCompleted, Nothing)
				Else
					EnableControls(False)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
				lblStatus.Text = String.Empty
				EnableControls(False)
			End Try
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub EnrollFromCameraLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			Try
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

	Private Sub CbCameraSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCameras.SelectedIndexChanged
		_biometricClient.FaceCaptureDevice = TryCast(cbCameras.SelectedItem, NCamera)
	End Sub

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		If _biometricClient.FaceCaptureDevice Is Nothing Then
			MessageBox.Show("Please select camera from the list")
			Return
		End If
		' Set face capture from stream
		Dim face As New NFace() With {.CaptureOptions = NBiometricCaptureOptions.Stream}
		If Not chbCaptureAutomatically.Checked Then
			face.CaptureOptions = face.CaptureOptions Or NBiometricCaptureOptions.Manual
		End If
		_subject = New NSubject()
		_subject.Faces.Add(face)
		facesView.Face = face

		' Begin capturing faces
		_biometricClient.BeginCapture(_subject, AddressOf OnCapturingCompleted, Nothing)

		lblStatus.Text = String.Empty
		EnableControls(True)
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		_biometricClient.Cancel()
		EnableControls(False)
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If saveTemplateDialog.ShowDialog() = DialogResult.OK Then
			File.WriteAllBytes(saveTemplateDialog.FileName, _subject.GetTemplateBuffer().ToArray())
		End If
	End Sub

	Private Sub BtnStartExtractionClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStartExtraction.Click
		lblStatus.Text = "Extracting ..."
		' Begin extraction
		_biometricClient.ForceStart()
	End Sub

	Private Sub BtnSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
		If saveImageDialog.ShowDialog() = DialogResult.OK Then
			_subject.Faces(0).Image.Save(saveImageDialog.FileName)
		End If
	End Sub

	Private Sub EnrollFromCameraVisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
		If _biometricClient IsNot Nothing Then
			EnableControls(False)
		End If
	End Sub

	Private Sub ChbCheckLivenessCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCheckLiveness.CheckedChanged
		_biometricClient.FacesLivenessMode = If(chbCheckLiveness.Checked, NLivenessMode.PassiveAndActive, NLivenessMode.None)
	End Sub

#End Region

End Class
