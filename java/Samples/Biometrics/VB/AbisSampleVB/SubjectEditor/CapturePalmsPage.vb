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
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images

Partial Public Class CapturePalmsPage
	Inherits PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		If (Not DesignMode) Then
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If

		AddHandler btnOpen.Click, AddressOf BtnOpenClick
		AddHandler palmSelector.FingerClick, AddressOf PalmSelectorFingerClick
		AddHandler rbFile.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbScanner.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler cbSelectedPosition.SelectedIndexChanged, AddressOf CbSelectedPositionSelectedIndexChanged
		AddHandler btnCapture.Click, AddressOf BtnCaptureClick
		AddHandler btnCancel.Click, AddressOf BtnCancelClick
		AddHandler palmsTree.PropertyChanged, AddressOf PalmTreePropertyChanged
		AddHandler btnFinish.Click, AddressOf BtnFinishClick
		AddHandler chbShowReturned.CheckedChanged, AddressOf ChbShowReturnedCheckedChanged
		AddHandler btnForce.Click, AddressOf BtnForceClick
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject
	Private _biometricClient As NBiometricClient
	Private _isIdle As New ManualResetEvent(True)
	Private _currentBiometric As NPalm
	Private _newSubject As NSubject
	Private _sessionId As Integer = -1
	Private _nowCapturing() As NPalm
	Private _titlePrefix As String = String.Empty

#End Region

#Region "Public overrided methods"

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

		_sessionId = If(_subject.Palms.Count > 0, _subject.Palms.Max(Function(x) x.SessionId) + 1, 0)
		_newSubject = New NSubject()
		palmsTree.Subject = _newSubject
		lblStatus.Text = String.Empty
		lblStatus.Visible = False
		chbShowReturned.Visible = _biometricClient.PalmsReturnBinarizedImage
		chbShowReturned.Checked = False
		OnPalmScannerChanged()
		ToggleRadioButtons()

		generalizeProgressView.Clear()
		generalizeProgressView.Visible = False

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		If IsBusy() Then
			LongActionDialog.ShowDialog(Me, "Finishing current action ... ", AddressOf CancelAndWait)
		End If
		RemoveHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		palmsTree.Subject = Nothing

		Dim palms = _newSubject.Palms.ToArray()
		_newSubject.Palms.Clear()
		For Each item In palms
			_subject.Palms.Add(item)
		Next item

		palmView.Finger = Nothing
		_newSubject = Nothing
		_subject = Nothing
		_biometricClient = Nothing

		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim generalize As Boolean = chbWithGeneralization.Checked
		Dim sessionId As Integer = -1
		If generalize Then
			sessionId = _sessionId
			_sessionId = _sessionId + 1
		End If
		Dim files As New List(Of String)()
		Dim count As Integer = If(generalize, SettingsManager.PalmsGeneralizationRecordCount, 1)

		_nowCapturing = Nothing
		generalizeProgressView.Clear()
		generalizeProgressView.Visible = generalize

		Do While count > files.Count
			openFileDialog.Title = If(generalize, String.Format("Open image ({0} of {1})", files.Count + 1, count), "Open image")
			If openFileDialog.ShowDialog() <> DialogResult.OK Then
				Return
			End If
			files.Add(openFileDialog.FileName)
		Loop

		_nowCapturing = New NPalm(count - 1) {}
		For i As Integer = 0 To count - 1
			Dim palm As NPalm = New NPalm With {.SessionId = sessionId, .FileName = files(i), .Position = palmSelector.SelectedPosition, .ImpressionType = CType(cbImpression.SelectedItem, NFImpressionType)}
			_newSubject.Palms.Add(palm)
			_nowCapturing(i) = palm
		Next i

		Dim first As NPalm = _nowCapturing.First()
		If generalize Then
			generalizeProgressView.Biometrics = _nowCapturing
			generalizeProgressView.Selected = first
		End If
		palmView.Finger = first
		palmsTree.UpdateTree()
		palmsTree.SelectedItem = palmsTree.GetBiometricNode(first)

		Dim biometricTask = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, _newSubject)
		SetIsBusy(True)
		_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCreateTemplateCompleted, Nothing)
		SetStatusText(Color.Orange, "Extracting template. Please wait ...")
		EnableControls()
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
			If status <> NBiometricStatus.Ok Then
				For Each item In SubjectUtils.FlattenPalms(_nowCapturing)
					_newSubject.Palms.Remove(item)
				Next item
			End If
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateOnCreateTemplateCompleted), status)
	End Sub
	Private Sub UpdateOnCreateTemplateCompleted(ByVal taskStatus As Object)
		If IsPageShown Then
			palmsTree.UpdateTree()
			generalizeProgressView.EnableMouseSelection = True
			If taskStatus <> NBiometricStatus.Ok Then
				palmsTree.SelectedItem = Nothing
				palmView.Finger = _nowCapturing.FirstOrDefault()
				SetStatusText(Color.Red, "Extraction failed: {0}", taskStatus)
			Else
				SetStatusText(Color.Green, "Extraction completed successfully")
				If chbWithGeneralization.Checked Then
					Dim node = palmsTree.GetBiometricNode(_nowCapturing.First())
					If node IsNot Nothing Then
						Dim generalized = node.GetAllGeneralized()
						generalizeProgressView.Generalized = generalized
						generalizeProgressView.Selected = generalized.FirstOrDefault()
						generalizeProgressView.Visible = node.IsGeneralizedNode
					End If
				End If
			End If
			_nowCapturing = Nothing
			EnableControls()
		End If
	End Sub

	Private Sub PalmSelectorFingerClick(ByVal sender As Object, ByVal e As FingerSelector.FingerClickArgs)
		palmSelector.SelectedPosition = e.Position
		cbSelectedPosition.SelectedItem = e.Position
	End Sub

	Private Sub RadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (CType(sender, RadioButton)).Checked Then
			ToggleRadioButtons()
		End If
	End Sub

	Private Sub CbSelectedPositionSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim selected As Object = cbSelectedPosition.SelectedItem
		palmSelector.SelectedPosition = If(selected IsNot Nothing, CType(selected, NFPosition), NFPosition.UnknownPalm)
	End Sub

	Private Sub BtnCaptureClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim generalize As Boolean = chbWithGeneralization.Checked
		Dim sessionId As Integer = -1
		If generalize Then
			sessionId = _sessionId
			_sessionId = _sessionId + 1
		End If
		Dim count As Integer = If(generalize, SettingsManager.PalmsGeneralizationRecordCount, 1)
		Dim manual As Boolean = Not chbCaptureAutomatically.Checked

		_nowCapturing = Nothing
		generalizeProgressView.Clear()

		_nowCapturing = New NPalm(count - 1) {}
		For i As Integer = 0 To count - 1
			Dim palm As NPalm = New NPalm With {.CaptureOptions = If(manual, NBiometricCaptureOptions.Manual, NBiometricCaptureOptions.None), .SessionId = sessionId, .Position = palmSelector.SelectedPosition, .ImpressionType = CType(cbImpression.SelectedItem, NFImpressionType)}
			_nowCapturing(i) = palm
			_newSubject.Palms.Add(palm)
		Next i
		palmsTree.UpdateTree()
		palmsTree.SelectedItem = palmsTree.GetBiometricNode(_nowCapturing.First())
		SetStatusText(Color.Orange, "Starting capturing from scanner ...")
		Dim biometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.CreateTemplate, _newSubject)
		SetIsBusy(True)
		_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCaptureCompleted, Nothing)
		EnableControls()
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs)
		_biometricClient.Cancel()
		btnCancel.Enabled = False
	End Sub

	Private Sub OnCaptureCompleted(ByVal r As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		Try
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			status = task.Status
			If task.Error IsNot Nothing Then
				Utilities.ShowError(task.Error)
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			If status <> NBiometricStatus.Ok Then
				For Each item In SubjectUtils.FlattenPalms(_nowCapturing)
					_newSubject.Palms.Remove(item)
				Next item
			End If
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateOnCaptureCompleted), status)
	End Sub
	Private Sub UpdateOnCaptureCompleted(ByVal taskStatus As Object)
		If IsPageShown Then
			generalizeProgressView.EnableMouseSelection = True
			If taskStatus = NBiometricStatus.Ok Then
				SetStatusText(Color.Green, "Palms captured successfully")
			Else
				If taskStatus = NBiometricStatus.Canceled Then
					lblStatus.Visible = False
				Else
					SetStatusText(Color.Red, "Extraction completed successfully: {0}", taskStatus)
				End If
			End If
			palmsTree.UpdateTree()
			Dim selected = palmsTree.SelectedItem
			If selected IsNot Nothing AndAlso selected.IsGeneralizedNode Then
				Dim generalized = selected.GetAllGeneralized()
				generalizeProgressView.Biometrics = selected.Items
				generalizeProgressView.Generalized = generalized
				generalizeProgressView.Selected = If(generalized.FirstOrDefault(), selected.Items.First())
			Else
				generalizeProgressView.Clear()
				generalizeProgressView.Visible = False
				If selected IsNot Nothing Then
					palmView.Finger = CType(selected.Items.First(), NPalm)
				End If
			End If
			_nowCapturing = Nothing
			EnableControls()
		End If
	End Sub

	Private Sub PalmTreePropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "SelectedItem" Then
			BeginInvoke(New MethodInvoker(AddressOf OnPalmTreeSelectedItemChanged))
		End If
	End Sub
	Private Sub OnPalmTreeSelectedItemChanged()
		Dim selection = palmsTree.SelectedItem
		Dim first As NPalm = If(selection IsNot Nothing, CType(selection.Items.First(), NPalm), Nothing)
		generalizeProgressView.Clear()
		If selection IsNot Nothing AndAlso selection.IsGeneralizedNode Then
			Dim generalized = selection.GetAllGeneralized()
			generalizeProgressView.Biometrics = selection.Items
			generalizeProgressView.Generalized = generalized
			generalizeProgressView.Selected = If(generalized.FirstOrDefault(), first)
			generalizeProgressView.Visible = True
		Else
			generalizeProgressView.Visible = False
		End If
		palmView.Finger = first
		chbShowReturned.Enabled = first IsNot Nothing AndAlso first.BinarizedImage IsNot Nothing
		lblStatus.Visible = lblStatus.Visible AndAlso IsBusy()
		If chbShowReturned.Checked AndAlso (first Is Nothing OrElse first.BinarizedImage Is Nothing) Then
			chbShowReturned.Checked = False
		End If
	End Sub

	Private Sub BtnFinishClick(ByVal sender As Object, ByVal e As EventArgs)
		PageController.NavigateToStartPage()
	End Sub

#End Region

#Region "Private methods"

	Private Sub SetStatusText(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		lblStatus.BackColor = backColor
		lblStatus.Text = String.Format(format, args)
		lblStatus.Visible = True
	End Sub

	Private Sub ToggleRadioButtons()
		cbImpression.Items.Clear()
		palmSelector.SelectedPosition = NFPosition.UnknownPalm
		palmSelector.AllowedPositions = Nothing
		cbSelectedPosition.Items.Clear()
		If rbFile.Checked Then
			Dim values() As NFImpressionType = CType(System.Enum.GetValues(GetType(NFImpressionType)), NFImpressionType())
			For Each item As NFImpressionType In values.Where(Function(x) NBiometricTypes.IsImpressionTypePalm(x))
				cbImpression.Items.Add(item)
			Next item
			cbImpression.SelectedIndex = 0
			cbImpression.Enabled = True

			Dim allowedPositions() As NFPosition = {NFPosition.LeftFullPalm, NFPosition.RightFullPalm, NFPosition.LeftUpperPalm, NFPosition.RightUpperPalm, NFPosition.LeftInterdigital, NFPosition.RightInterdigital, NFPosition.LeftHypothenar, NFPosition.RightHypothenar, NFPosition.LeftLowerPalm, NFPosition.RightLowerPalm, NFPosition.LeftThenar, NFPosition.RightThenar}

			palmSelector.AllowedPositions = allowedPositions
			For Each item In allowedPositions
				cbSelectedPosition.Items.Add(item)
			Next item
			cbSelectedPosition.SelectedIndex = 0
		Else
			Dim device = _biometricClient.PalmScanner
			cbImpression.Items.Add(device.GetSupportedImpressionTypes().First(Function(x) NBiometricTypes.IsImpressionTypePalm(x)))
			cbImpression.SelectedIndex = 0

			Dim supportedPositions = device.GetSupportedPositions().Where(Function(x) NBiometricTypes.IsPositionPalm(x)).ToArray()
			palmSelector.AllowedPositions = supportedPositions
			For Each item In supportedPositions
				cbSelectedPosition.Items.Add(item)
			Next item
		End If
		cbSelectedPosition.SelectedIndex = 0
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

	Private Sub EnableControls()
		Dim isIdle As Boolean = Not IsBusy()
		rbScanner.Enabled = _biometricClient.PalmScanner IsNot Nothing AndAlso _biometricClient.PalmScanner.IsAvailable
		gbSource.Enabled = isIdle
		gbOptions.Enabled = isIdle
		palmSelector.AllowHighlight = isIdle
		cbSelectedPosition.Enabled = isIdle
		palmsTree.Enabled = isIdle
		btnCapture.Enabled = isIdle OrElse rbScanner.Checked
		btnCapture.Visible = rbScanner.Checked AndAlso isIdle
		btnCancel.Visible = rbScanner.Checked
		btnCancel.Enabled = Not isIdle
		btnOpen.Enabled = isIdle AndAlso rbFile.Checked
		btnOpen.Visible = rbFile.Checked
		palmsTree.Enabled = isIdle
		chbShowReturned.Enabled = isIdle
		chbCaptureAutomatically.Enabled = isIdle AndAlso rbScanner.Checked
		btnForce.Visible = rbScanner.Checked
		btnForce.Enabled = rbScanner.Checked AndAlso Not isIdle
		busyIndicator.Visible = Not isIdle
	End Sub

	Private Sub OnPalmPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnPalmStatusChanged), _currentBiometric.Status)
		End If
	End Sub

	Private Sub OnPalmStatusChanged(ByVal status As Object)
		Dim backColor As Color = If(status = NBiometricStatus.Ok OrElse status = NBiometricStatus.None, Color.Green, Color.Red)
		SetStatusText(backColor, "{0}Status: {1}", _titlePrefix, status)
	End Sub

	Private Sub ChbShowReturnedCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		palmView.ShownImage = If(chbShowReturned.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

	Private Sub OnPalmScannerChanged()
		Dim device = _biometricClient.PalmScanner
		If device Is Nothing OrElse (Not device.IsAvailable) Then
			rbFile.Checked = True
			rbScanner.Text = "Scanner (Not connected)"
		Else
			rbScanner.Text = String.Format("Scanner ({0})", device.DisplayName)
		End If
		EnableControls()
	End Sub

	Private Sub OnBiometricClientPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "PalmScanner" Then
			BeginInvoke(New Action(AddressOf InvokeOnPalmScannerChanged))
		ElseIf e.PropertyName = "CurrentBiometric" Then
			BeginInvoke(New Action(Of NPalm)(AddressOf OnCurrentBiometricChanged), _biometricClient.CurrentBiometric)
		End If
	End Sub

	Private Sub InvokeOnPalmScannerChanged()
		If IsPageShown Then
			OnPalmScannerChanged()
		End If
	End Sub

	Private Sub OnCurrentBiometricChanged(ByVal current As Object)
		If _currentBiometric IsNot Nothing Then
			RemoveHandler _currentBiometric.PropertyChanged, AddressOf OnPalmPropertyChanged
			_currentBiometric = Nothing
		End If
		If current IsNot Nothing Then
			Dim withGeneralization As Boolean = chbWithGeneralization.Checked
			Dim node = palmsTree.GetBiometricNode(current)
			If withGeneralization Then
				generalizeProgressView.Biometrics = node.Items
				generalizeProgressView.Generalized = node.GetAllGeneralized()
				generalizeProgressView.Selected = current
				_titlePrefix = String.Format("Capturing {0} ({1} of {2}). ", current.Position, Array.IndexOf(node.Items, current) + 1, SettingsManager.PalmsGeneralizationRecordCount)
			End If
			palmView.Finger = current
			_currentBiometric = current
			AddHandler _currentBiometric.PropertyChanged, AddressOf OnPalmPropertyChanged
		Else
			generalizeProgressView.Clear()
			lblStatus.Visible = False
			_titlePrefix = String.Empty
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As Object, ByVal e As EventArgs)
		_biometricClient.Force()
	End Sub

#End Region
End Class
