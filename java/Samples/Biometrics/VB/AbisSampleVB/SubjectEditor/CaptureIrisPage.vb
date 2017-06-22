Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class CaptureIrisPage
	Inherits Neurotec.Samples.PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		If (Not DesignMode) Then
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If

		AddHandler rbFile.CheckedChanged, AddressOf RbFileCheckedChanged
		AddHandler rbIrisScanner.CheckedChanged, AddressOf RbIrisScannerCheckedChanged
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _isIdle As New ManualResetEvent(True)
	Private _iris As NIris = Nothing
	Private _newSubject As NSubject = Nothing

#End Region

#Region "Public methods"

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		If args Is Nothing OrElse args.Length <> 2 Then
			Throw New ArgumentException("args")
		End If

		_subject = CType(args(0), NSubject)
		_biometricClient = CType(args(1), NBiometricClient)
		If _subject Is Nothing OrElse _biometricClient Is Nothing Then
			Throw New ArgumentException("args")
		End If
		AddHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged

		_newSubject = New NSubject()
		_iris = New NIris()
		_newSubject.Irises.Add(_iris)
		irisView.Iris = _iris
		lblStatus.Visible = False

		OnIrisScannerChanged()
		ToggleRadioButton()

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		If IsBusy() Then
			LongActionDialog.ShowDialog(Me, "Finishing current action ...", AddressOf CancelAndWait)
		End If
		RemoveHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		If _iris.Status = NBiometricStatus.Ok Then
			Dim irises = _newSubject.Irises.ToArray()
			_newSubject.Irises.Clear()
			For Each item As NIris In irises
				_subject.Irises.Add(item)
			Next item
		End If
		_newSubject = Nothing
		_iris = Nothing
		irisView.Iris = Nothing
		_subject = Nothing
		_biometricClient = Nothing

		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private methods"

	Private Sub OnIrisScannerChanged()
		Dim device = _biometricClient.IrisScanner
		If device Is Nothing OrElse Not device.IsAvailable Then
			rbFile.Checked = True
			rbIrisScanner.Text = "Scanner (Not connected)"
		Else
			rbIrisScanner.Text = String.Format("Scanner ({0})", device.DisplayName)
		End If
		EnableControls()
	End Sub

	Private Sub SetIsBusy(ByVal value As Boolean)
		If value Then
			_isIdle.Reset()
		Else
			_isIdle.Set()
		End If
	End Sub

	Private Function IsBusy() As Boolean
		Return Not _isIdle.WaitOne(0)
	End Function

	Private Sub CancelAndWait()
		If IsBusy() Then
			_biometricClient.Cancel()
			_isIdle.WaitOne()
		End If
	End Sub

	Private Sub ToggleRadioButton()
		cbPosition.BeginUpdate()
		Try
			cbPosition.Items.Clear()
			If rbFile.Checked Then
				cbPosition.Items.Add(NEPosition.Left)
				cbPosition.Items.Add(NEPosition.Right)
				cbPosition.Items.Add(NEPosition.Unknown)
			Else
				Try
					For Each item In _biometricClient.IrisScanner.GetSupportedPositions()
						cbPosition.Items.Add(item)
					Next item
				Catch ex As Exception
					cbPosition.Items.Add(NEPosition.Unknown)
					Utilities.ShowError(ex)
				End Try
			End If
			cbPosition.SelectedIndex = 0
		Finally
			cbPosition.EndUpdate()
		End Try
		EnableControls()
	End Sub

	Private Sub EnableControls()
		Dim isBusy As Boolean = Me.IsBusy()
		gbCaptureOptions.Enabled = Not isBusy
		btnCancel.Enabled = isBusy AndAlso rbIrisScanner.Checked
		btnCapture.Visible = rbIrisScanner.Checked
		btnForce.Visible = rbIrisScanner.Checked
		btnForce.Enabled = isBusy AndAlso Not chbCaptureAutomatically.Checked
		chbCaptureAutomatically.Visible = rbIrisScanner.Checked
		chbCaptureAutomatically.Enabled = Not isBusy
		btnOpen.Visible = rbFile.Checked
		rbIrisScanner.Enabled = _biometricClient.IrisScanner IsNot Nothing AndAlso _biometricClient.IrisScanner.IsAvailable
		If (Not btnCapture.Visible AndAlso Not btnForce.Visible) Then
			tableLayoutPanel2.SetCellPosition(btnOpen, tableLayoutPanel2.GetCellPosition(btnCapture))
		End If

		Dim boldFinish As Boolean = Not isBusy AndAlso _iris Is Nothing AndAlso _iris.Status = NBiometricStatus.Ok
		btnFinish.Font = New Font(btnFinish.Font, If(boldFinish, FontStyle.Bold, FontStyle.Regular))

		busyIndicator.Visible = isBusy
	End Sub

	Private Sub SetStatusText(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		lblStatus.Text = String.Format(format, args)
		lblStatus.BackColor = backColor
		lblStatus.Visible = True
	End Sub

	Private Sub OnBiometricClientPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "IrisScanner" Then
			BeginInvoke(New MethodInvoker(AddressOf InvokeOnIrisScannerChanged))
		End If
	End Sub

	Private Sub InvokeOnIrisScannerChanged()
		If IsPageShown Then
			OnIrisScannerChanged()
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub OnIrisPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnStatusChanged), _iris.Status)
		End If
	End Sub

	Private Sub OnStatusChanged(ByVal status As NBiometricStatus)
		Dim backColor As Color = If(status = NBiometricStatus.Ok OrElse status = NBiometricStatus.None, Color.Green, Color.Red)
		SetStatusText(backColor, "Status: {0}", status)
	End Sub

	Private Sub OnCaptureCompleted(ByVal r As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		RemoveHandler _iris.PropertyChanged, AddressOf OnIrisPropertyChanged
		Try
			status = _biometricClient.EndCreateTemplate(r)
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateStatus), status)
	End Sub

	Private Sub UpdateStatus(ByVal status As NBiometricStatus)
		If IsPageShown Then
			Dim backColor As Color = If(status = NBiometricStatus.Ok, Color.Green, Color.Red)
			SetStatusText(backColor, "Extraction status: {0}", status)
			EnableControls()
		End If
	End Sub

	Private Sub OnCreateTemplateCompleted(ByVal r As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		Try
			status = _biometricClient.EndCreateTemplate(r)
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateStatus), status)
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		_biometricClient.Cancel()
	End Sub

	Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			_iris.Image = Nothing
			_iris.Position = CType(cbPosition.SelectedItem, NEPosition)
			_iris.FileName = openFileDialog.FileName
			_iris.CaptureOptions = NBiometricCaptureOptions.None
			SetStatusText(Color.Green, "Extracting template ...")
			SetIsBusy(True)
			_biometricClient.BeginCreateTemplate(_newSubject, AddressOf OnCreateTemplateCompleted, Nothing)
			EnableControls()
		End If
	End Sub

	Private Sub BtnCaptureClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture.Click
		_iris.Image = Nothing
		_iris.FileName = Nothing
		_iris.Position = CType(cbPosition.SelectedItem, NEPosition)
		_iris.CaptureOptions = If(Not chbCaptureAutomatically.Checked, NBiometricCaptureOptions.Manual, NBiometricCaptureOptions.None)
		AddHandler _iris.PropertyChanged, AddressOf OnIrisPropertyChanged
		SetStatusText(Color.Orange, "Starting capture from camera ...")
		SetIsBusy(True)
		_biometricClient.BeginCreateTemplate(_newSubject, AddressOf OnCaptureCompleted, Nothing)
		EnableControls()
	End Sub

	Private Sub BtnFinishClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
		PageController.NavigateToStartPage()
	End Sub

	Private Sub RbIrisScannerCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If rbIrisScanner.Checked Then
			ToggleRadioButton()
		End If
	End Sub

	Private Sub RbFileCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If rbFile.Checked Then
			ToggleRadioButton()
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
		_biometricClient.Force()
	End Sub

#End Region
End Class
