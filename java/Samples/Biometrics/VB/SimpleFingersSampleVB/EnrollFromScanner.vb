Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class EnrollFromScanner
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _deviceManager As NDeviceManager
	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _subjectFinger As NFinger

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

	Private Sub EnableControls(ByVal capturing As Boolean)
		scannersListBox.Enabled = Not capturing
		cancelScanningButton.Enabled = capturing
		scanButton.Enabled = Not capturing
		refreshListButton.Enabled = Not capturing
		Dim fingerStatus = (Not capturing) AndAlso _subjectFinger IsNot Nothing AndAlso _subjectFinger.Status = NBiometricStatus.Ok
		saveImageButton.Enabled = fingerStatus
		chbShowBinarizedImage.Enabled = fingerStatus
		saveTemplateButton.Enabled = (Not capturing) AndAlso _subject IsNot Nothing AndAlso _subject.Status = NBiometricStatus.Ok
		chbScanAutomatically.Enabled = Not capturing
		btnForce.Enabled = capturing AndAlso Not chbScanAutomatically.Checked
	End Sub

	Private Sub OnEnrollCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnEnrollCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			EnableControls(False)
			Dim status As NBiometricStatus = task.Status

			' Check if extraction was canceled
			If status = NBiometricStatus.Canceled Then
				Return
			End If

			If status = NBiometricStatus.Ok Then
				lblQuality.Text = String.Format("Quality: {0}", _subjectFinger.Objects(0).Quality)
				SetShownImage()
			Else
				MessageBox.Show(String.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				_subject = Nothing
				_subjectFinger = Nothing
				EnableControls(False)
			End If
		End If
	End Sub

	Private Sub UpdateScannerList()
		scannersListBox.BeginUpdate()
		Try
			scannersListBox.Items.Clear()
			If _deviceManager IsNot Nothing Then
				For Each item As NDevice In _deviceManager.Devices
					scannersListBox.Items.Add(item)
				Next item
			End If
		Finally
			scannersListBox.EndUpdate()
		End Try
	End Sub

	Private Sub SetShownImage()
		fingerView.ShownImage = If(chbShowBinarizedImage.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

#End Region

#Region "Private form events"

	Private Sub ScanButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles scanButton.Click
		If _biometricClient.FingerScanner Is Nothing Then
			MessageBox.Show("Please select a scanner from the list.")
		Else
			EnableControls(True)
			lblQuality.Text = String.Empty

			' Create a finger
			_subjectFinger = New NFinger()

			' Set Manual capturing mode if not automatic selected
			If Not chbScanAutomatically.Checked Then
				_subjectFinger.CaptureOptions = NBiometricCaptureOptions.Manual
			End If

			' Add finger to the subject and fingerView
			_subject = New NSubject()
			_subject.Fingers.Add(_subjectFinger)
			AddHandler _subjectFinger.PropertyChanged, AddressOf OnAttributesPropertyChanged
			fingerView.Finger = _subjectFinger
			fingerView.ShownImage = ShownImage.Original

			' Begin capturing
			_biometricClient.FingersReturnBinarizedImage = True
			Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.CreateTemplate, _subject)
			_biometricClient.BeginPerformTask(task, AddressOf OnEnrollCompleted, Nothing)
		End If
	End Sub

	Private Sub OnAttributesPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New Action(Of NBiometricStatus)(Function(status) lblQuality.Text = status.ToString()), _subjectFinger.Status)
		End If
	End Sub

	Private Sub CancelScanningButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles cancelScanningButton.Click
		_biometricClient.Cancel()
	End Sub

	Private Sub RefreshListButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles refreshListButton.Click
		UpdateScannerList()
	End Sub

	Private Sub SaveImageButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveImageButton.Click
		If _subjectFinger.Status = NBiometricStatus.Ok Then
			SaveFileDialog.FileName = String.Empty
			SaveFileDialog.Title = "Save Image File"
			SaveFileDialog.Filter = NImages.GetSaveFileFilterString()
			If SaveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					If chbShowBinarizedImage.Checked Then
						_subjectFinger.BinarizedImage.Save(saveFileDialog.FileName)
					Else
						_subjectFinger.Image.Save(SaveFileDialog.FileName)
					End If
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub SaveTemplateButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveTemplateButton.Click
		If _subject.Status = NBiometricStatus.Ok Then
			SaveFileDialog.FileName = String.Empty
			SaveFileDialog.Filter = String.Empty
			SaveFileDialog.Title = "Save Template File"
			If SaveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					' Save template to file
					File.WriteAllBytes(SaveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray())
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub EnrollFromScannerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		_deviceManager = _biometricClient.DeviceManager
		UpdateScannerList()
		SaveFileDialog.Filter = NImages.GetSaveFileFilterString()
	End Sub

	Private Sub ScannersListBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles scannersListBox.SelectedIndexChanged
		_biometricClient.FingerScanner = TryCast(scannersListBox.SelectedItem, NFScanner)
	End Sub

	Private Sub ChbShowBinarizedImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage.CheckedChanged
		SetShownImage()
	End Sub

	Private Sub FingerViewMouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fingerView.MouseClick
		If e.Button = MouseButtons.Right AndAlso chbShowBinarizedImage.Enabled Then
			chbShowBinarizedImage.Checked = Not chbShowBinarizedImage.Checked
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
		_biometricClient.Force()
	End Sub

#End Region

End Class
