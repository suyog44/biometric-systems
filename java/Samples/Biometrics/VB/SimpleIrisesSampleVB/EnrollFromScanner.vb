Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
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
	Private _iris As NIris

#End Region	' Private fields

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

#Region "Public methods"

	Public Sub CancelScaning()
		irisView.Iris = Nothing
		_biometricClient.Cancel()
	End Sub

#End Region

#Region "Private methods"

	Private Sub UpdateScannerList()
		lbScanners.BeginUpdate()
		Try
			lbScanners.Items.Clear()
			If _deviceManager IsNot Nothing Then
				For Each item As NDevice In _deviceManager.Devices
					lbScanners.Items.Add(item)
				Next item
			End If
		Finally
			lbScanners.EndUpdate()
		End Try
	End Sub

	Private Sub EnableControls(ByVal capturing As Boolean)
		btnCancel.Enabled = capturing
		btnScan.Enabled = Not capturing
		btnRefresh.Enabled = Not capturing
		rbLeft.Enabled = Not capturing
		rbRight.Enabled = Not capturing
		btnSaveImage.Enabled = (Not capturing) AndAlso _iris IsNot Nothing AndAlso _iris.Status = NBiometricStatus.Ok
		btnSaveTemplate.Enabled = (Not capturing) AndAlso _subject IsNot Nothing AndAlso _subject.Status = NBiometricStatus.Ok
		chbScanAutomatically.Enabled = Not capturing
		btnForce.Enabled = capturing AndAlso Not chbScanAutomatically.Checked
	End Sub

	Private Sub OnCaptureCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCaptureCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			EnableControls(False)
			Dim status As NBiometricStatus = task.Status
			lblStatus.Text = status.ToString()

			' Check if extraction was canceled
			If status = NBiometricStatus.Canceled Then
				Return
			End If
			If status <> NBiometricStatus.Ok Then
				MessageBox.Show(String.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				_subject = Nothing
				_iris = Nothing
				EnableControls(False)
			End If
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnScanClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnScan.Click
		If _biometricClient.IrisScanner Is Nothing Then
			MessageBox.Show("Please select a scanner")
		Else
			EnableControls(True)
			lblStatus.Text = String.Empty

			' Create iris
			_iris = New NIris()

			' Set Manual capturing mode if not automatic selected
			If Not chbScanAutomatically.Checked Then
				_iris.CaptureOptions = NBiometricCaptureOptions.Manual
			End If

			' Add iris to the subject and irisView
			_iris.Position = If(rbRight.Checked, NEPosition.Right, NEPosition.Left)
			_subject = New NSubject()
			_subject.Irises.Add(_iris)
			irisView.Iris = _iris

			' Begin capturing
			Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.CreateTemplate, _subject)
			_biometricClient.BeginPerformTask(task, AddressOf OnCaptureCompleted, Nothing)
		End If
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		CancelScaning()
	End Sub

	Private Sub BtnRefreshClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
		UpdateScannerList()
	End Sub

	Private Sub BtnSaveImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveImage.Click
		If _iris.Status = NBiometricStatus.Ok Then
			saveFileDialog.FileName = String.Empty
			saveFileDialog.Filter = NImages.GetSaveFileFilterString()
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					_iris.Image.Save(saveFileDialog.FileName)
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If _subject.Status = NBiometricStatus.Ok Then
			saveFileDialog.FileName = String.Empty
			saveFileDialog.Filter = String.Empty
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					File.WriteAllBytes(saveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray())
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub EnrollFromScannerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			_deviceManager = _biometricClient.DeviceManager
			UpdateScannerList()
		End If
	End Sub

	Private Sub LbScannersSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbScanners.SelectedIndexChanged
		_biometricClient.IrisScanner = TryCast(lbScanners.SelectedItem, NIrisScanner)
	End Sub

	Private Sub EnrollFromScannerVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
		End If
		If Visible AndAlso _biometricClient IsNot Nothing Then
			EnableControls(False)
			rbLeft.Checked = True
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
		_biometricClient.Force()
	End Sub

#End Region

End Class
