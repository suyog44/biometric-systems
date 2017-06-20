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
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class CaptureFingersPage
	Inherits PageBase
#Region "Private types"

	Private Class Scenario
#Region "Public static readonly fields"

		Public Shared ReadOnly UnknownPlainFinger As Scenario
		Public Shared ReadOnly UnknownRolledFinger As Scenario
		Public Shared ReadOnly AllPlainFingers As Scenario
		Public Shared ReadOnly AllRolledFingers As Scenario
		Public Shared ReadOnly Slaps As Scenario
		Public Shared ReadOnly SlapsSeparateThumbs As Scenario
		Public Shared ReadOnly RolledPlusSlaps As Scenario
		Public Shared ReadOnly RolledPlusSlapsSeparateThumbs As Scenario

#End Region

#Region "Private types"

		Private Structure Tuple
			Private privatePosition As NFPosition
			Public Property Position() As NFPosition
				Get
					Return privatePosition
				End Get
				Set(ByVal value As NFPosition)
					privatePosition = value
				End Set
			End Property
			Private privateImpressionType As NFImpressionType
			Public Property ImpressionType() As NFImpressionType
				Get
					Return privateImpressionType
				End Get
				Set(ByVal value As NFImpressionType)
					privateImpressionType = value
				End Set
			End Property
		End Structure

#End Region

#Region "Private fields"

		Private Shared ReadOnly _scenarios() As Scenario

#End Region

#Region "Private constructor"

		Private Sub New(ByVal name As String)
			Me.Name = name
		End Sub

#End Region

#Region "Static constructor"

		Shared Sub New()
			Dim plainFingers() As NFPosition = New NFPosition() {NFPosition.LeftLittle, NFPosition.LeftRing, NFPosition.LeftMiddle, NFPosition.LeftIndex, NFPosition.LeftThumb, NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle}
			Dim slaps() As NFPosition = New NFPosition() {NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainThumbs}
			Dim slapsSeparatteThumbs() As NFPosition = New NFPosition() {NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainLeftThumb, NFPosition.PlainRightThumb}
			Dim TempScenario As Scenario = New Scenario("Unknown plain finger") With {.Items = New Tuple() {New Tuple()}, .IsUnknownFingers = True}
			UnknownPlainFinger = New Scenario("Unknown plain finger") With {.Items = New Tuple() {New Tuple()}, .IsUnknownFingers = True}
			UnknownRolledFinger = New Scenario("Unknown rolled finger") With {.Items = New Tuple() {New Tuple With {.ImpressionType = NFImpressionType.LiveScanRolled}}, .HasRolled = True, .IsUnknownFingers = True}
			AllPlainFingers = New Scenario("All plain fingers") With {.Items = plainFingers.Select(Function(x) New Tuple With {.Position = x})}
			AllRolledFingers = New Scenario("All rolled fingers") With {.Items = plainFingers.Select(Function(x) New Tuple With {.Position = x, .ImpressionType = NFImpressionType.LiveScanRolled}), .HasRolled = True}
			Scenario.Slaps = New Scenario("4-4-2") With {.Items = slaps.Select(Function(x) New Tuple With {.Position = x}), .HasSlaps = True}
			SlapsSeparateThumbs = New Scenario("4-4-1-1") With {.Items = slapsSeparatteThumbs.Select(Function(x) New Tuple With {.Position = x}), .HasSlaps = True}
			RolledPlusSlaps = New Scenario("Rolled fingers + 4-4-2") With {.Items = Enumerable.Union(AllRolledFingers.Items, Scenario.Slaps.Items), .HasSlaps = True, .HasRolled = True}
			RolledPlusSlapsSeparateThumbs = New Scenario("Rolled fingers + 4-4-1-1") With {.Items = Enumerable.Union(AllRolledFingers.Items, SlapsSeparateThumbs.Items), .HasSlaps = True, .HasRolled = True}

			_scenarios = New Scenario() {UnknownPlainFinger, UnknownRolledFinger, AllPlainFingers, AllRolledFingers, Scenario.Slaps, SlapsSeparateThumbs, RolledPlusSlaps, RolledPlusSlapsSeparateThumbs}
		End Sub

#End Region

#Region "Public static methods"

		Public Shared Function GetAvailableScenarios() As IEnumerable(Of Scenario)
			Return _scenarios
		End Function

#End Region

#Region "Private properties"

		Private privateItems As IEnumerable(Of Tuple)
		Private Property Items() As IEnumerable(Of Tuple)
			Get
				Return privateItems
			End Get
			Set(ByVal value As IEnumerable(Of Tuple))
				privateItems = value
			End Set
		End Property

#End Region

#Region "Public properties"

		Private privateHasRolled As Boolean
		Public Property HasRolled() As Boolean
			Get
				Return privateHasRolled
			End Get
			Private Set(ByVal value As Boolean)
				privateHasRolled = value
			End Set
		End Property
		Private privateHasSlaps As Boolean
		Public Property HasSlaps() As Boolean
			Get
				Return privateHasSlaps
			End Get
			Private Set(ByVal value As Boolean)
				privateHasSlaps = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Private Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateIsUnknownFingers As Boolean
		Public Property IsUnknownFingers() As Boolean
			Get
				Return privateIsUnknownFingers
			End Get
			Private Set(ByVal value As Boolean)
				privateIsUnknownFingers = value
			End Set
		End Property

#End Region

#Region "Public methods"

		Public Overrides Function ToString() As String
			Return Name
		End Function

		Public Function GetPositions() As IEnumerable(Of NFPosition)
			Return Items.Select(Function(x) x.Position).Distinct()
		End Function

		Public Function GetFingers(ByVal sessionId As Integer, ByVal generalizationCount As Integer) As IEnumerable(Of NFinger)
			Dim results = New List(Of NFinger)()
			For Each item In Items
				For i As Integer = 0 To generalizationCount - 1
					results.Add(New NFinger With {.Position = item.Position, .ImpressionType = item.ImpressionType, .SessionId = sessionId})
				Next i
			Next item
			Return results
		End Function

		Public Function GetFingers() As IEnumerable(Of NFinger)
			Return GetFingers(-1, 1)
		End Function

#End Region
	End Class

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		If (Not DesignMode) Then
			OpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If

		AddHandler cbScenario.SelectedIndexChanged, AddressOf CbScenarioSelectedIndexChanged
		AddHandler btnCancel.Click, AddressOf BtnCancelClick
		AddHandler btnOpenImage.Click, AddressOf BtnOpenImageClick
		AddHandler btnStart.Click, AddressOf BtnStartClick
		AddHandler rbTenPrintCard.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbFiles.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbScanner.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler btnCancel.Click, AddressOf BtnCancelClick
		AddHandler fingerSelector.FingerClick, AddressOf FingerSelectorFingerClick
		AddHandler contextMnuStrip.ItemClicked, AddressOf ContextMenuStripItemClicked
		AddHandler fingersTree.PropertyChanged, AddressOf FingersTreePropertyChanged
		AddHandler btnRepeat.Click, AddressOf BtnRepeatClick
		AddHandler btnSkip.Click, AddressOf BtnSkipClick
		AddHandler bntFinish.Click, AddressOf BntFinishClick
		AddHandler chbShowReturned.CheckedChanged, AddressOf ChbShowReturnedCheckedChanged
		AddHandler btnForce.Click, AddressOf BtnForceClick

	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _isIdle As New ManualResetEvent(True)
	Private _newSubject As NSubject
	Private _currentBiometric As NFinger = Nothing
	Private _captureNeedsAction As Boolean = False
	Private _sessionId As Integer = -1
	Private _nowCapturing() As NFinger = Nothing
	Private _titlePrefix As String = String.Empty

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
		_sessionId = If(_subject.Fingers.Count > 0, _subject.Fingers.Max(Function(x) x.SessionId) + 1, 0)

		_biometricClient.CurrentBiometricCompletedTimeout = -1
		AddHandler _biometricClient.CurrentBiometricCompleted, AddressOf BiometricClientCurrentBiometricCompleted
		AddHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged

		_newSubject = New NSubject()
		CopyMissingFingerPositions(_newSubject, _subject)
		fingersTree.Subject = _newSubject

		FingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray()
		lblStatus.Text = String.Empty
		lblStatus.Visible = False
		chbShowReturned.Visible = _biometricClient.FingersReturnBinarizedImage
		chbShowReturned.Checked = False
		GeneralizeProgressView.Visible = False
		AddHandler GeneralizeProgressView.PropertyChanged, AddressOf OnGeneralizeProgressViewPropertyChanged

		OnFingerScannerChanged()
		OnRadioButtonToggle()

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		If IsBusy() Then
			LongActionDialog.ShowDialog(Me, "Finish current action ...", AddressOf CancelAndWait)
		End If

		_nowCapturing = Nothing
		RemoveHandler GeneralizeProgressView.PropertyChanged, AddressOf OnGeneralizeProgressViewPropertyChanged
		RemoveHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		RemoveHandler _biometricClient.CurrentBiometricCompleted, AddressOf BiometricClientCurrentBiometricCompleted
		_biometricClient.CurrentBiometricCompletedTimeout = 0
		Dim fingers = _newSubject.Fingers.ToArray()
		_newSubject.Fingers.Clear()
		For Each item In fingers
			_subject.Fingers.Add(item)
		Next item
		CopyMissingFingerPositions(_subject, _newSubject)

		_newSubject = Nothing
		fingersTree.Subject = Nothing
		fingerView.Finger = Nothing
		_biometricClient = Nothing

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

	Private Sub UpdateShowReturned()
		Dim ridge As NFrictionRidge = fingerView.Finger
		chbShowReturned.Enabled = ridge IsNot Nothing AndAlso ridge.BinarizedImage IsNot Nothing
		If Not chbShowReturned.Enabled AndAlso chbShowReturned.Checked Then
			chbShowReturned.Checked = False
		End If
	End Sub

	Private Sub CopyMissingFingerPositions(ByVal dst As NSubject, ByVal src As NSubject)
		dst.MissingFingers.Clear()
		For Each item In src.MissingFingers
			dst.MissingFingers.Add(item)
		Next item
	End Sub

	Private Sub EnableControls()
		Dim fromFile As Boolean = rbFiles.Checked
		Dim isBusy As Boolean = Me.IsBusy()
		Dim isTenPrint As Boolean = rbTenPrintCard.Checked

		btnStart.Visible = Not fromFile
		btnOpenImage.Enabled = fromFile AndAlso Not isBusy
		Dim selected As Scenario = CType(cbScenario.SelectedItem, Scenario)
		btnSkip.Enabled = (Not fromFile) AndAlso (selected IsNot Nothing) AndAlso (Not selected.IsUnknownFingers) AndAlso isBusy
		btnRepeat.Enabled = (Not fromFile) AndAlso isBusy
		btnStart.Enabled = (Not fromFile) AndAlso Not isBusy
		btnCancel.Enabled = (Not fromFile) AndAlso isBusy
		FingerSelector.AllowHighlight = (Not isBusy) AndAlso Not isTenPrint
		rbScanner.Enabled = _biometricClient IsNot Nothing AndAlso _biometricClient.FingerScanner IsNot Nothing
		cbScenario.Enabled = Not isBusy
		cbImpression.Enabled = (Not isBusy) AndAlso fromFile
		fingersTree.Enabled = Not isBusy
		panelNavigations.Visible = (Not fromFile) AndAlso Not isTenPrint
		btnOpenImage.Visible = fromFile
		chbCaptureAutomatically.Enabled = (Not fromFile) AndAlso (Not isTenPrint) AndAlso Not isBusy
		chbWithGeneralization.Enabled = (Not isTenPrint) AndAlso Not isBusy
		btnForce.Visible = Not fromFile
		btnForce.Enabled = isBusy AndAlso Not chbCaptureAutomatically.Checked
		gbSource.Enabled = Not isBusy
		chbShowReturned.Enabled = chbShowReturned.Enabled AndAlso Not isBusy

		busyIndicator.Visible = isBusy
	End Sub

	Private Sub ListSupportedScenarios()
		If _biometricClient Is Nothing Then
			Return
		End If

		Try
			cbScenario.BeginUpdate()

			Dim selected = CType(cbScenario.SelectedItem, Scenario)
			Dim supportedScenarios As IEnumerable(Of Scenario) = Scenario.GetAvailableScenarios()
			If rbTenPrintCard.Checked Then
				supportedScenarios = New Scenario() {Scenario.RolledPlusSlapsSeparateThumbs}
			ElseIf Not rbFiles.Checked Then
				Dim scanner As NFScanner = _biometricClient.FingerScanner
				Dim impressions = scanner.GetSupportedImpressionTypes()
				Dim positions = scanner.GetSupportedPositions()
				Dim supportsRolled As Boolean = Array.Exists(impressions, Function(x) NBiometricTypes.IsImpressionTypeRolled(x))
				Dim supportsSlaps As Boolean = Array.Exists(positions, Function(x) NBiometricTypes.IsPositionFourFingers(x))

				If (Not supportsRolled) Then
					supportedScenarios = supportedScenarios.Where(Function(x) (Not x.HasRolled))
				End If
				If (Not supportsSlaps) Then
					supportedScenarios = supportedScenarios.Where(Function(x) (Not x.HasSlaps))
				End If
			End If

			cbScenario.Items.Clear()
			For Each item In supportedScenarios
				cbScenario.Items.Add(item)
			Next item

			cbScenario.SelectedItem = selected
		Finally
			cbScenario.EndUpdate()
		End Try
	End Sub

	Private Sub OnRadioButtonToggle()
		ListSupportedScenarios()
		If cbScenario.SelectedIndex = -1 AndAlso cbScenario.Items.Count > 0 Then
			cbScenario.SelectedIndex = 0
		End If
		EnableControls()
		ShowHint()
	End Sub

	Private Sub ShowHint()
		lblNote.Visible = fingerSelector.Visible AndAlso Not rbTenPrintCard.Checked
		If rbFiles.Checked Then
			lblNote.Text = "Hint: Click on finger to select it or mark as missing"
		Else
			lblNote.Text = "Hint: Click on finger to mark it as missing"
		End If
	End Sub

	Private Sub UpdateImpressionTypes(ByVal position As NFPosition, ByVal isRolled As Boolean)
		Try
			Dim impressions = If(rbScanner.Checked, _biometricClient.FingerScanner.GetSupportedImpressionTypes(), CType(System.Enum.GetValues(GetType(NFImpressionType)), NFImpressionType()))
			Dim valid = impressions.Where(Function(x) NBiometricTypes.IsImpressionTypeRolled(x) = isRolled AndAlso NBiometricTypes.IsPositionCompatibleWith(position, x)).Distinct()
			cbImpression.BeginUpdate()
			cbImpression.Items.Clear()
			For Each item In valid
				cbImpression.Items.Add(item)
			Next item
			If cbImpression.Items.Count > 0 Then
				cbImpression.SelectedIndex = 0
			End If
		Finally
			cbImpression.EndUpdate()
		End Try
	End Sub

	Private Sub OnSelectedPositionChanged(ByVal sc As Scenario, ByVal position As NFPosition)
		Dim isRolled As Boolean = sc.HasRolled AndAlso NBiometricTypes.IsPositionTheFinger(position)
		OnSelectedPositionChanged(sc, position, isRolled)
	End Sub

	Private Sub OnSelectedPositionChanged(ByVal sc As Scenario, ByVal position As NFPosition, ByVal isRolled As Boolean)
		If position = NFPosition.Unknown AndAlso rbFiles.Checked Then
			position = sc.GetPositions().First()
			isRolled = sc.HasRolled AndAlso NBiometricTypes.IsPositionTheFinger(position)
		End If
		fingerSelector.SelectedPosition = position
		fingerSelector.IsRolled = isRolled
		UpdateImpressionTypes(position, isRolled AndAlso sc.HasRolled)
	End Sub

	Private Sub SetLabelText(ByVal lbl As Label, ByVal backColor As Color, ByVal text As String)
		lbl.Text = text
		lbl.BackColor = backColor
		lbl.Visible = True
	End Sub

	Private Sub SetStatusText(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		SetLabelText(lblStatus, backColor, String.Format(format, args))
	End Sub

	Private Sub OnFingerScannerChanged()
		Dim device = _biometricClient.FingerScanner
		If device Is Nothing OrElse (Not device.IsAvailable) Then
			If rbScanner.Checked Then
				rbFiles.Checked = True
			End If
			rbScanner.Text = "Scanner (Not connected)"
		Else
			rbScanner.Text = String.Format("Scanner ({0})", device.DisplayName)
		End If
		EnableControls()
	End Sub

#End Region

#Region "Private events"

	Private Sub OnGeneralizeProgressViewPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Selected" Then
			UpdateShowReturned()
		End If
	End Sub

	Private Sub BiometricClientCurrentBiometricCompleted(ByVal sender As Object, ByVal e As EventArgs)
		Dim status As NBiometricStatus = _biometricClient.CurrentBiometric.Status
		If status = NBiometricStatus.Ok OrElse status = NBiometricStatus.SpoofDetected OrElse status = NBiometricStatus.SourceError OrElse status = NBiometricStatus.CaptureError Then
			_biometricClient.Force()
		Else
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateCaptureFailedStatus), status)
			Dim delayedInvoke As System.Threading.Timer = New System.Threading.Timer(New TimerCallback(AddressOf TimerTick), Nothing, 3000, Timeout.Infinite)
		End If
	End Sub

	Private Sub UpdateCaptureFailedStatus(ByVal status As NBiometricStatus)
		_captureNeedsAction = True
		SetStatusText(Color.Red, "Capturing failed: {0}. Trying again ...", status)
	End Sub

	Private Sub TimerTick(ByVal obj As Object)
		If IsHandleCreated Then
			BeginInvoke(New Action(AddressOf RepeatCapture))
		End If
	End Sub

	Private Sub RepeatCapture()
		If IsPageShown AndAlso _captureNeedsAction Then
			_biometricClient.Repeat()
			_captureNeedsAction = False
		End If
	End Sub

	Private Sub CbScenarioSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim selected As Scenario = CType(cbScenario.SelectedItem, Scenario)
		fingerSelector.SelectedPosition = NFPosition.Unknown
		fingerSelector.IsRolled = False
		fingerSelector.Visible = Not selected.IsUnknownFingers
		fingerSelector.AllowedPositions = selected.GetPositions().ToArray()
		If rbFiles.Checked Then
			Dim position As NFPosition = fingerSelector.AllowedPositions.FirstOrDefault()
			OnSelectedPositionChanged(selected, position)
		Else
			UpdateImpressionTypes(NFPosition.Unknown, False)
		End If
		ShowHint()
		EnableControls()
	End Sub

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs)
		If rbScanner.Checked Then
			Dim generalize As Boolean = chbWithGeneralization.Checked
			Dim sessionId As Integer = -1
			If generalize Then
				sessionId = _sessionId
				_sessionId = _sessionId + 1
			End If
			Dim count As Integer = If(generalize, SettingsManager.FingersGeneralizationRecordCount, 1)
			Dim manual As Boolean = Not chbCaptureAutomatically.Checked

			_nowCapturing = Nothing
			generalizeProgressView.Clear()
			generalizeProgressView.EnableMouseSelection = False

			Dim sc As Scenario = CType(cbScenario.SelectedItem, Scenario)
			_nowCapturing = sc.GetFingers(sessionId, count).Where(Function(x) (Not _newSubject.MissingFingers.Contains(x.Position))).ToArray()
			For Each finger In _nowCapturing
				If manual Then
					finger.CaptureOptions = NBiometricCaptureOptions.Manual
				End If
				_newSubject.Fingers.Add(finger)
			Next finger

			Dim operations As NBiometricOperations = NBiometricOperations.Capture Or NBiometricOperations.CreateTemplate
			If _biometricClient.FingersCalculateNfiq Then
				operations = operations Or NBiometricOperations.AssessQuality
			End If
			Dim biometricTask = _biometricClient.CreateTask(operations, _newSubject)
			SetIsBusy(True)
			_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCaptureCompleted, Nothing)
			SetStatusText(Color.Orange, "Starting capturing from scanner ...")
			EnableControls()
		ElseIf rbTenPrintCard.Checked Then
			Using dialog = New TenPrintCardForm()
				dialog.BiometricClient = _biometricClient
				If dialog.ShowDialog() = DialogResult.OK Then
					Dim subject As NSubject = dialog.Result
					If subject IsNot Nothing Then
						Dim fingers() As NFinger = subject.Fingers.ToArray()
						subject.Fingers.Clear()

						For Each item In fingers
							_newSubject.Fingers.Add(item)
						Next item
					End If
				End If
			End Using
		End If
	End Sub

	Private Sub OnCaptureCompleted(ByVal r As IAsyncResult)
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
				' Remove fingers where failed
				Dim grouped = SubjectUtils.GetFingersGeneralizationGroups(_nowCapturing).ToArray()
				Dim failedGroups = grouped.Where(Function(g) g.FirstOrDefault(Function(x) x.Status <> NBiometricStatus.Ok) IsNot Nothing)
				For Each group In failedGroups
					For Each item In SubjectUtils.FlattenFingers(group)
						_newSubject.Fingers.Remove(item)
					Next item
				Next group
			End If
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateOnCaptureCompleted), status)
	End Sub
	Private Sub UpdateOnCaptureCompleted(ByVal taskStatus As Object)
		If IsPageShown Then
			GeneralizeProgressView.EnableMouseSelection = True
			If taskStatus = NBiometricStatus.Ok Then
				SetStatusText(Color.Green, "Fingers captured successfully")
			Else
				If taskStatus = NBiometricStatus.Canceled Then
					lblStatus.Visible = False
				Else
					SetStatusText(Color.Red, "Capture failed: {0}", taskStatus)
				End If
				fingerSelector.SelectedPosition = NFPosition.Unknown
			End If
			fingersTree.UpdateTree()
			Dim selected = fingersTree.SelectedItem
			If selected IsNot Nothing AndAlso selected.IsGeneralizedNode Then
				Dim generalized = selected.GetAllGeneralized()
				GeneralizeProgressView.Biometrics = selected.Items
				GeneralizeProgressView.Generalized = generalized
				GeneralizeProgressView.Selected = If(generalized.FirstOrDefault(), selected.Items.First())
				GeneralizeProgressView.Visible = True
			Else
				GeneralizeProgressView.Clear()
				GeneralizeProgressView.Visible = False
				If selected IsNot Nothing Then
					fingerView.Finger = CType(selected.Items.First(), NFinger)
				End If
			End If
			_nowCapturing = Nothing
			UpdateShowReturned()
			EnableControls()
		End If
	End Sub

	Private Sub OnBiometricClientPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "CurrentBiometric" Then
			BeginInvoke(New Action(Of NFinger)(AddressOf OnCurrentBiometricChanged), _biometricClient.CurrentBiometric)
		ElseIf e.PropertyName = "FingerScanner" Then
			BeginInvoke(New Action(AddressOf InvokeOnCurrentScannerChanged))
		End If
	End Sub
	Private Sub OnCurrentBiometricChanged(ByVal current As Object)
		If _currentBiometric IsNot Nothing Then
			RemoveHandler _currentBiometric.PropertyChanged, AddressOf CurrentBiometricPropertyChanged
			_currentBiometric = Nothing
		End If
		If current IsNot Nothing Then
			Dim withGeneralization As Boolean = chbWithGeneralization.Checked
			Dim node = fingersTree.GetBiometricNode(current)
			OnSelectedPositionChanged(CType(cbScenario.SelectedItem, Scenario), current.Position)
			fingersTree.SelectedItem = node
			If withGeneralization Then
				GeneralizeProgressView.Biometrics = node.Items
				GeneralizeProgressView.Generalized = node.GetAllGeneralized()
				GeneralizeProgressView.Selected = current
				GeneralizeProgressView.Visible = True
				_titlePrefix = String.Format("Capturing {0} ({1} of {2}). ", current.Position, Array.IndexOf(node.Items, current) + 1, SettingsManager.FingersGeneralizationRecordCount)
			End If
			fingerView.Finger = current
			_currentBiometric = current
			AddHandler _currentBiometric.PropertyChanged, AddressOf CurrentBiometricPropertyChanged
		Else
			GeneralizeProgressView.Clear()
			lblStatus.Visible = False
			_titlePrefix = String.Empty
		End If
	End Sub
	Private Sub InvokeOnCurrentScannerChanged()
		If IsPageShown Then
			OnFingerScannerChanged()
		End If
	End Sub

	Private Sub CurrentBiometricPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnCurrentBiometricStatusChanged), _currentBiometric.Status)
		End If
	End Sub
	Private Sub OnCurrentBiometricStatusChanged(ByVal status As Object)
		Dim backColor As Color = If(status = NBiometricStatus.Ok OrElse status = NBiometricStatus.None, Color.Green, Color.Red)
		SetStatusText(backColor, "{0}Status: {1}", _titlePrefix, status)
	End Sub

	Private Sub BtnOpenImageClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim generalize As Boolean = chbWithGeneralization.Checked
		Dim sessionId As Integer = -1
		If generalize Then
			sessionId = _sessionId
			_sessionId = _sessionId + 1
		End If
		Dim files As New List(Of String)()
		Dim count As Integer = If(generalize, SettingsManager.FingersGeneralizationRecordCount, 1)

		_nowCapturing = Nothing
		generalizeProgressView.Clear()

		Do While count > files.Count
			openFileDialog.Title = If(generalize, String.Format("Open image ({0} of {1})", files.Count + 1, count), "Open image")
			If openFileDialog.ShowDialog() <> DialogResult.OK Then
				Return
			End If
			files.Add(openFileDialog.FileName)
		Loop

		_nowCapturing = New NFinger(count - 1) {}
		For i As Integer = 0 To count - 1
			Dim finger As NFinger = New NFinger With {.SessionId = sessionId, .FileName = files(i), .Position = fingerSelector.SelectedPosition, .ImpressionType = CType(cbImpression.SelectedItem, NFImpressionType)}
			_newSubject.Fingers.Add(finger)
			_nowCapturing(i) = finger
		Next i

		Dim first As NFinger = _nowCapturing.First()
		If generalize Then
			generalizeProgressView.Biometrics = _nowCapturing
			generalizeProgressView.Selected = first
		End If
		fingerView.Finger = first
		fingersTree.UpdateTree()
		fingersTree.SelectedItem = fingersTree.GetBiometricNode(first)

		Dim operations As NBiometricOperations = NBiometricOperations.CreateTemplate
		If _biometricClient.FingersCalculateNfiq Then
			operations = operations Or NBiometricOperations.AssessQuality
		End If
		Dim biometricTask = _biometricClient.CreateTask(operations, _newSubject)
		SetIsBusy(True)
		_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCreateTemplateCompleted, Nothing)
		SetStatusText(Color.Orange, "Extracting template. Please wait ...")
		EnableControls()
	End Sub

	Private Sub OnCreateTemplateCompleted(ByVal result As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		Try
			Dim biometricTask = _biometricClient.EndPerformTask(result)
			status = biometricTask.Status
			If biometricTask.Error IsNot Nothing Then
				Utilities.ShowError(biometricTask.Error)
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			If status <> NBiometricStatus.Ok Then
				Dim allItems = SubjectUtils.FlattenFingers(_nowCapturing)
				For Each item In allItems
					_newSubject.Fingers.Remove(item)
				Next item
			End If
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateOnCrateTemplateCompleted), status)
	End Sub
	Private Sub UpdateOnCrateTemplateCompleted(ByVal taskStatus As Object)
		If IsPageShown Then
			fingersTree.UpdateTree()
			GeneralizeProgressView.EnableMouseSelection = True
			If taskStatus <> NBiometricStatus.Ok Then
				fingersTree.SelectedItem = Nothing
				fingerView.Finger = _nowCapturing.FirstOrDefault()
				SetStatusText(Color.Red, "Extraction failed: {0}", taskStatus)
			Else
				SetStatusText(Color.Green, "Finger extraction completed successfully")
				If chbWithGeneralization.Checked Then
					Dim node = fingersTree.GetBiometricNode(_nowCapturing.First())
					If node IsNot Nothing Then
						Dim generalized = node.GetAllGeneralized()
						generalizeProgressView.Generalized = generalized
						generalizeProgressView.Selected = generalized.FirstOrDefault()
					End If
				End If
			End If
			_nowCapturing = Nothing
			EnableControls()
			UpdateShowReturned()
		End If
	End Sub

	Private Sub RadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (CType(sender, RadioButton)).Checked Then
			OnRadioButtonToggle()
		End If
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs)
		_captureNeedsAction = False
		_biometricClient.Cancel()
		btnCancel.Enabled = False
	End Sub

	Private Sub FingerSelectorFingerClick(ByVal sender As Object, ByVal e As FingerSelector.FingerClickArgs)
		If rbFiles.Checked Then
			Dim position As NFPosition = e.Position
			Dim part As NFPosition = If(e.PositionPart = NFPosition.Unknown, position, e.PositionPart)

			Dim isMissing As Boolean = _newSubject.MissingFingers.Contains(position) OrElse _newSubject.MissingFingers.Contains(part)
			Dim isSelected As Boolean = fingerSelector.SelectedPosition = position

			tsmiSelect.Text = String.Format("Select {0}", position)
			tsmiSelect.Tag = position
			tsmiMissing.Text = String.Format("Mark {0} as {1}", part, If(isMissing, "not missing", "missing"))
			tsmiMissing.Tag = part
			Dim location = fingerSelector.PointToScreen(e.Location)
			contextMnuStrip.Show(location)
		Else
			Dim position As NFPosition = e.Position
			If (Not NBiometricTypes.IsPositionSingleFinger(position)) Then
				position = e.PositionPart
			End If

			Dim isMissing As Boolean = _newSubject.MissingFingers.Contains(position)
			If isMissing Then
				_newSubject.MissingFingers.Remove(position)
			Else
				_newSubject.MissingFingers.Add(position)
			End If
			fingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray()
		End If
	End Sub

	Private Sub ContextMenuStripItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
		Dim position As NFPosition = CType(e.ClickedItem.Tag, NFPosition)
		If e.ClickedItem Is tsmiMissing Then
			Dim isMissing As Boolean = _newSubject.MissingFingers.Contains(position)
			If isMissing Then
				_newSubject.MissingFingers.Remove(position)
			Else
				_newSubject.MissingFingers.Add(position)
			End If
			fingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray()
		ElseIf e.ClickedItem Is tsmiSelect Then
			OnSelectedPositionChanged(CType(cbScenario.SelectedItem, Scenario), position)
		End If
	End Sub

	Private Sub FingersTreePropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "SelectedItem" Then
			BeginInvoke(New Action(AddressOf OnFingerTreeSelectedItemChanged))
		End If
	End Sub
	Private Sub OnFingerTreeSelectedItemChanged()
		Dim selection = fingersTree.SelectedItem
		Dim first As NFinger = If(selection IsNot Nothing, CType(selection.Items.FirstOrDefault(), NFinger), Nothing)
		Dim withGeneralization As Boolean = chbWithGeneralization.Checked
		Dim position As NFPosition = If(first IsNot Nothing, first.Position, NFPosition.Unknown)
		Dim impression As NFImpressionType = If(first IsNot Nothing, first.ImpressionType, NFImpressionType.LiveScanPlain)
		GeneralizeProgressView.Clear()
		If selection IsNot Nothing AndAlso selection.IsGeneralizedNode Then
			Dim generalized = selection.GetAllGeneralized()
			GeneralizeProgressView.Biometrics = selection.Items
			GeneralizeProgressView.Generalized = generalized
			GeneralizeProgressView.Selected = If(generalized.FirstOrDefault(), first)
			GeneralizeProgressView.Visible = True
		Else
			GeneralizeProgressView.Visible = False
		End If
		fingerView.Finger = first
		OnSelectedPositionChanged(CType(cbScenario.SelectedItem, Scenario), position, NBiometricTypes.IsImpressionTypeRolled(impression))
		lblStatus.Visible = lblStatus.Visible AndAlso IsBusy()
		UpdateShowReturned()
	End Sub

	Private Sub BtnRepeatClick(ByVal sender As Object, ByVal e As EventArgs)
		_captureNeedsAction = False
		_biometricClient.Repeat()
	End Sub

	Private Sub BtnSkipClick(ByVal sender As Object, ByVal e As EventArgs)
		_captureNeedsAction = False
		_biometricClient.Skip()
	End Sub

	Private Sub BntFinishClick(ByVal sender As Object, ByVal e As EventArgs)
		PageController.NavigateToStartPage()
	End Sub

	Private Sub ChbShowReturnedCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		fingerView.ShownImage = If(chbShowReturned.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

	Private Sub BtnForceClick(ByVal sender As Object, ByVal e As EventArgs)
		_biometricClient.Force()
	End Sub

#End Region
End Class
