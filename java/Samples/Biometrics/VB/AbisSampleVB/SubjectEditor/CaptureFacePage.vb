Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Threading
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class CaptureFacePage
	Inherits PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		AddHandler rbFromVideo.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbFromFile.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbFromCamera.CheckedChanged, AddressOf RadioButtonCheckedChanged
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject
	Private _biometricClient As NBiometricClient
	Private _isIdle As New ManualResetEvent(True)
	Private _currentBiometric As NFace = Nothing
	Private _newSubject As NSubject
	Private _isExtractStarted As Boolean = False
	Private _sessionId As Integer = -1
	Private _titlePrefix As String = String.Empty

#End Region

#Region "Public overridden methods"

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		If args Is Nothing OrElse args.Length <> 2 Then
			Throw New ArgumentException("args")
		End If

		_subject = CType(args(0), NSubject)
		_biometricClient = CType(args(1), NBiometricClient)
		If _subject Is Nothing OrElse _biometricClient Is Nothing Then
			Throw New ArgumentException("args")
		End If

		_sessionId = If(_subject.Faces.Count > 0, _subject.Faces.Max(Function(x) x.SessionId) + 1, 0)
		_newSubject = New NSubject()
		AddHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		_biometricClient.CurrentBiometricCompletedTimeout = 5000
		AddHandler _biometricClient.CurrentBiometricCompleted, AddressOf OnCurrentBiometricCompleted

		subjectTreeControl.Visible = False
		icaoWarningView.Visible = False
		OnFaceCaptureDeviceChanged()
		ToggleRadioButton()
		lblStatus.Visible = False
		generalizationView.Visible = False

		Dim mirrorHorizontally As Boolean = SettingsManager.FacesMirrorHorizontally
		chbMirrorHorizontally.Checked = mirrorHorizontally
		faceView.MirrorHorizontally = mirrorHorizontally

		subjectTreeControl.Subject = _newSubject

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		If IsBusy() Then
			LongActionDialog.ShowDialog(Me, "Finish current action ...", AddressOf CancelAndWait)
		End If
		RemoveHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		RemoveHandler _biometricClient.CurrentBiometricCompleted, AddressOf OnCurrentBiometricCompleted
		_biometricClient.CurrentBiometricCompletedTimeout = 0
		subjectTreeControl.SelectedItem = Nothing
		subjectTreeControl.Subject = Nothing
		faceView.Face = Nothing
		icaoWarningView.Face = Nothing
		If _newSubject.Status = NBiometricStatus.Ok Then
			Dim faces = _newSubject.Faces.ToArray()
			_newSubject.Clear()
			For Each face In faces
				_subject.Faces.Add(face)
			Next face
		End If
		_newSubject = Nothing
		_subject = Nothing
		_biometricClient = Nothing

		SettingsManager.FacesMirrorHorizontally = chbMirrorHorizontally.Checked

		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private methods"

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

	Private Sub SetStatusText(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		lblStatus.Text = String.Format(format, args)
		lblStatus.BackColor = backColor
		lblStatus.Visible = True
	End Sub

	Private Sub EnableControls()
		Dim canCancel As Boolean = rbFromCamera.Checked OrElse rbFromVideo.Checked
		Dim isManual As Boolean = chbManual.Checked
		Dim isStream As Boolean = chbStream.Checked
		Dim isIdle As Boolean = Not IsBusy()
		Dim isLocalCreate = (_biometricClient.LocalOperations And NBiometricOperations.CreateTemplate) = NBiometricOperations.CreateTemplate
		Dim checkIcao As Boolean = chbCheckIcaoCompliance.Checked
		chbStream.Enabled = (Not rbFromFile.Checked) AndAlso isLocalCreate
		chbStream.Checked = chbStream.Checked AndAlso isLocalCreate
		chbManual.Enabled = Not rbFromFile.Checked
		chbCheckIcaoCompliance.Enabled = isIdle
		rbFromCamera.Enabled = _biometricClient.FaceCaptureDevice IsNot Nothing
		gbCaptureOptions.Enabled = isIdle
		btnCancel.Enabled = (Not isIdle) AndAlso canCancel
		btnForceStart.Enabled = (Not isIdle) AndAlso ((isManual AndAlso (Not _isExtractStarted)) OrElse checkIcao)
		btnForceEnd.Enabled = False
		btnForceStart.Visible = canCancel
		btnForceEnd.Visible = btnForceStart.Visible
		btnCancel.Visible = btnForceEnd.Visible
		btnRepeat.Enabled = False
		btnRepeat.Visible = chbWithGeneralization.Checked AndAlso btnCancel.Visible

		Dim boldStart As Boolean = btnForceStart.Enabled AndAlso isManual
		Dim boldFinish As Boolean = isIdle AndAlso _newSubject.Status = NBiometricStatus.Ok
		btnForceStart.Font = New Font(btnForceStart.Font, If(boldStart, FontStyle.Bold, FontStyle.Regular))
		btnForceStart.Text = If(checkIcao AndAlso (Not isManual), "Force", "Start")
		btnFinish.Font = New Font(btnFinish.Font, If(boldFinish, FontStyle.Bold, FontStyle.Regular))

		busyIndicator.Visible = Not isIdle
	End Sub

	Private Sub ToggleRadioButton()
		EnableControls()
	End Sub

	Private Sub OnFacePropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnFaceStatusChanged), (CType(sender, NBiometric)).Status)
		End If
	End Sub

	Private Sub OnFaceStatusChanged(ByVal status As NBiometricStatus)
		Dim format As String = If(_isExtractStarted, "Extracion status: {0}", "Detection status: {0}")
		Dim backColor As Color = If(status = NBiometricStatus.Ok OrElse status = NBiometricStatus.None, Color.Green, Color.Red)
		SetStatusText(backColor, _titlePrefix & format, status)
	End Sub

	Private Sub OnFaceCaptureDeviceChanged()
		Dim device = _biometricClient.FaceCaptureDevice
		If device Is Nothing OrElse (Not device.IsAvailable) Then
			If rbFromCamera.Checked Then
				rbFromFile.Checked = True
			End If
			rbFromCamera.Text = "From camera (Not connected)"
		Else
			rbFromCamera.Text = String.Format("From camera ({0})", device.DisplayName)
		End If
		EnableControls()
	End Sub

	Private Sub OnBiometricClientPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "FaceCaptureDevice" Then
			BeginInvoke(New Action(AddressOf InvokeOnFaceCaptureDeviceChanged))
		ElseIf e.PropertyName = "CurrentBiometric" Then
			BeginInvoke(New Action(Of NBiometric)(AddressOf OnCurrentBiometricChanged), (CType(sender, NBiometricClient)).CurrentBiometric)
		End If
	End Sub
	Private Sub InvokeOnFaceCaptureDeviceChanged()
		If IsPageShown Then
			OnFaceCaptureDeviceChanged()
		End If
	End Sub
	Private Sub OnCurrentBiometricChanged(ByVal x As Object)
		If IsPageShown Then
			Dim face As NFace = CType(x, NFace)
			If face IsNot Nothing Then
				faceView.Face = face
			End If
			If face IsNot Nothing AndAlso chbCheckIcaoCompliance.Checked Then
				icaoWarningView.Face = face
			End If
			If chbWithGeneralization.Checked AndAlso face IsNot Nothing Then
				Dim index As Integer = _newSubject.Faces.IndexOf(face)
				generalizationView.Selected = x
				_titlePrefix = String.Format("Capturing face {0} of {1}. ", index + 1, SettingsManager.FacesGeneralizationRecordCount)
			End If
			If rbFromCamera.Checked OrElse rbFromVideo.Checked Then
				_isExtractStarted = Not chbManual.Checked
				If _currentBiometric IsNot Nothing Then
					RemoveHandler _currentBiometric.PropertyChanged, AddressOf OnFacePropertyChanged
				End If
				_currentBiometric = face
				If _currentBiometric IsNot Nothing Then
					AddHandler _currentBiometric.PropertyChanged, AddressOf OnFacePropertyChanged
				End If
			End If
			EnableControls()
		End If
	End Sub

	Private Sub UpdateWithTaskResult(ByVal status As NBiometricStatus)
		If IsPageShown Then
			PrepareViews(False, chbCheckIcaoCompliance.Checked, status = NBiometricStatus.Ok)

			Dim withGeneralization As Boolean = chbWithGeneralization.Checked
			Dim backColor As Color = If(status = NBiometricStatus.Ok, Color.Green, Color.Red)
			SetStatusText(backColor, "Extraction status: {0}", If(status = NBiometricStatus.Timeout, "Liveness check failed", status.ToString()))
			If withGeneralization AndAlso status = NBiometricStatus.Ok Then
				Dim generalized As NFace = _newSubject.Faces.Last()
				generalizationView.Generalized = New NFace() {generalized}
				generalizationView.Selected = generalized
			End If
			generalizationView.EnableMouseSelection = True
			EnableControls()
		End If
	End Sub

	Private Sub OnCurrentBiometricCompleted(ByVal sender As Object, ByVal e As EventArgs)
		Dim face As NFace = CType(_biometricClient.CurrentBiometric, NFace)
		Dim status As NBiometricStatus = face.Status
		If status = NBiometricStatus.Ok Then
			Dim attributes = face.Objects.FirstOrDefault()
			Dim child = If(attributes IsNot Nothing, attributes.Child, Nothing)
			If child IsNot Nothing AndAlso child.Status <> NBiometricStatus.Ok Then
				status = child.Status
			End If
		End If

		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnCurrentBiometricCompletedInternal), status)
	End Sub

	Private Sub OnCurrentBiometricCompletedInternal(ByVal status As Object)
		Dim allowRepeat As Boolean = btnRepeat.Visible AndAlso status <> NBiometricStatus.Ok
		If (Not allowRepeat) Then
			_biometricClient.Force()
		Else
			SetStatusText(Color.Red, _titlePrefix & "Extracion status: {0}", status)
		End If
		btnRepeat.Enabled = allowRepeat
	End Sub

	Private Sub PrepareViews(ByVal isCapturing As Boolean, ByVal checkIcao As Boolean)
		PrepareViews(isCapturing, checkIcao, True)
	End Sub

	Private Sub PrepareViews(ByVal isCapturing As Boolean, ByVal checkIcao As Boolean, ByVal isOk As Boolean)
		icaoWarningView.Visible = checkIcao
		If isCapturing Then
			faceView.ShowAge = Not checkIcao
			faceView.ShowEmotions = Not checkIcao
			faceView.ShowExpression = Not checkIcao
			faceView.ShowGender = Not checkIcao
			faceView.ShowProperties = Not checkIcao
			faceView.ShowIcaoArrows = True
			subjectTreeControl.Visible = False
		Else
			faceView.ShowAge = True
			faceView.ShowEmotions = True
			faceView.ShowExpression = True
			faceView.ShowGender = True
			faceView.ShowProperties = True
			faceView.ShowIcaoArrows = False
			subjectTreeControl.Visible = checkIcao AndAlso isOk
			If checkIcao Then
				subjectTreeControl.UpdateTree()
				Dim node = subjectTreeControl.Nodes.First()
				subjectTreeControl.SelectedItem = If(node.GetChildren().FirstOrDefault(), node)
			End If
		End If
		faceView.Invalidate()
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnForceStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnForceStart.Click
		If (Not chbCheckIcaoCompliance.Checked) Then
			_isExtractStarted = True
			btnForceEnd.Enabled = chbStream.Checked
			btnForceStart.Enabled = False
		ElseIf chbManual.Checked Then
			btnForceStart.Text = "Force"
			btnForceStart.Font = New Font(btnFinish.Font, FontStyle.Regular)
		End If
		_biometricClient.Force()
	End Sub

	Private Sub BtnForceEndClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnForceEnd.Click
		btnForceEnd.Enabled = False
		_isExtractStarted = False
		_biometricClient.Force()
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		_biometricClient.Cancel()
	End Sub

	Private Sub RadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (CType(sender, RadioButton)).Checked Then
			ToggleRadioButton()
		End If
	End Sub

	Private Sub BtnFinishClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
		PageController.NavigateToStartPage()
	End Sub

	Private Sub BtnCaptureClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture.Click
		Dim generalize As Boolean = chbWithGeneralization.Checked
		Dim fromFile As Boolean = rbFromFile.Checked
		Dim fromCamera As Boolean = rbFromCamera.Checked
		Dim checkIcao As Boolean = chbCheckIcaoCompliance.Checked
		Dim count As Integer = If(generalize, SettingsManager.FacesGeneralizationRecordCount, 1)
		Dim options As NBiometricCaptureOptions = NBiometricCaptureOptions.None
		If chbManual.Checked Then
			options = options Or NBiometricCaptureOptions.Manual
		End If
		If chbStream.Checked Then
			options = options Or NBiometricCaptureOptions.Stream
		End If

		lblStatus.Visible = False
		_titlePrefix = String.Empty
		_newSubject.Clear()
		faceView.Face = Nothing
		generalizationView.Clear()
		generalizationView.EnableMouseSelection = False
		generalizationView.Visible = generalize

		Dim selectedFiles As New List(Of String)()
		Dim title As String = If(fromFile, "Select image", "Select video file")
		Dim titleFormat As String = If(fromFile, "Select face image ({0} out of {1})", "Select video file ({0} out of {1})")
		Dim fileFilter As String = If(fromFile, NImages.GetOpenFileFilterString(True, True), Nothing)
		If rbFromFile.Checked OrElse rbFromVideo.Checked Then
			openFileDialog.FileName = Nothing
			openFileDialog.Filter = fileFilter
			openFileDialog.Title = title
			Do While selectedFiles.Count < count
				If generalize Then
					openFileDialog.Title = String.Format(titleFormat, selectedFiles.Count + 1, count)
				End If
				If openFileDialog.ShowDialog() <> DialogResult.OK Then
					Return
				End If
				selectedFiles.Add(openFileDialog.FileName)
			Loop
		End If

		Dim id As Integer = If(generalize, _sessionId, -1)
		For i As Integer = 0 To count - 1
			Dim face As NFace = New NFace With {.SessionId = id, .FileName = If((Not fromCamera), selectedFiles(i), Nothing), .CaptureOptions = options}
			_newSubject.Faces.Add(face)
		Next i
		faceView.Face = _newSubject.Faces.First()

		If generalize Then
			generalizationView.Biometrics = _newSubject.Faces.ToArray()
			generalizationView.Selected = _newSubject.Faces.First()
		End If

		icaoWarningView.Face = _newSubject.Faces.First()

		_biometricClient.FacesCheckIcaoCompliance = checkIcao
		Dim operations As NBiometricOperations = If(fromFile, NBiometricOperations.CreateTemplate, NBiometricOperations.Capture Or NBiometricOperations.CreateTemplate)
		If checkIcao Then
			operations = operations Or NBiometricOperations.Segment
		End If

		Dim biometricTask As NBiometricTask = _biometricClient.CreateTask(operations, _newSubject)
		SetStatusText(Color.Orange, If(fromFile, "Extracting template ...", "Starting capturing ..."))
		SetIsBusy(True)
		_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCreateTemplateCompleted, Nothing)

		EnableControls()
		PrepareViews(True, checkIcao)
	End Sub

	Private Sub OnCreateTemplateCompleted(ByVal r As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		Try
			Dim biometricTask = _biometricClient.EndPerformTask(r)
			status = biometricTask.Status
			If biometricTask.Error IsNot Nothing Then
				Utilities.ShowError(biometricTask.Error)
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			SetIsBusy(False)
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateWithTaskResult), status)
		End Try
	End Sub

	Private Sub BtnRepeatClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRepeat.Click
		_biometricClient.Repeat()
	End Sub

	Private Sub CaptureFacePageLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		chbStream.Checked = (_biometricClient.LocalOperations And NBiometricOperations.CreateTemplate) <> 0
	End Sub

	Private Sub ChbMirrorHorizontallyCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbMirrorHorizontally.CheckedChanged
		faceView.MirrorHorizontally = chbMirrorHorizontally.Checked
		faceView.Invalidate()
	End Sub

	Private Sub ChbCheckIcaoComplianceCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCheckIcaoCompliance.CheckedChanged
		If chbCheckIcaoCompliance.Checked Then
			chbStream.Checked = chbStream.Enabled
			chbManual.Checked = False
		End If
	End Sub

	Private Sub SubjectTreeControlPropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles subjectTreeControl.PropertyChanged
		If subjectTreeControl.Visible AndAlso e.PropertyName = "SelectedItem" Then
			Dim selected = subjectTreeControl.SelectedItem
			If selected IsNot Nothing Then
				If generalizationView.Visible Then
					generalizationView.Biometrics = selected.Items
					generalizationView.Generalized = selected.GetAllGeneralized()
					generalizationView.Selected = selected.AllItems.First()
				Else
					faceView.Face = TryCast(selected.Items.First(), NFace)
					icaoWarningView.Face = faceView.Face
				End If
			End If
		End If
	End Sub

#End Region
End Class
