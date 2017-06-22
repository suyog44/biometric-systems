Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images
Imports System.Threading

Namespace Forms
	Partial Public Class FingerCaptureForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
			Dim tool = New NFingerView.SegmentManipulationTool()
			AddHandler tool.SegmentManipulationEnded, AddressOf OnSegmentManipulationEnded
			fingerView.ActiveTool = tool
		End Sub

		#End Region

		#Region "Private fields"

		Private _biometricClient As NBiometricClient
		Private _subject As NSubject
		Private _captureList As List(Of NFinger)
		Private _current As NFinger

		Private _isProcessing As Boolean
'INSTANT VB TODO TASK: There is no VB.NET equivalent to 'volatile':
'ORIGINAL LINE: private volatile bool _isCapturing;
		Private _isCapturing As Boolean

		Private Function IsBusy() As Boolean
			Return Thread.VolatileRead(_isProcessing) OrElse Thread.VolatileRead(_isCapturing)
		End Function

		Private Sub CancelAndWait()
			If IsBusy() Then
				If _isCapturing Then
					_biometricClient.Cancel()
				End If
				Do While IsBusy()
					Application.DoEvents()
				Loop
			End If
		End Sub

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

		Public Property Subject() As NSubject
			Get
				Return _subject
			End Get
			Set(ByVal value As NSubject)
				_subject = value
			End Set
		End Property

		#End Region

		#Region "Private form events"

		Private Sub CaptureFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			If _biometricClient IsNot Nothing AndAlso _subject IsNot Nothing Then
				fSelector.MissingPositions = _subject.MissingFingers.ToArray()
				SetStatus(String.Empty)

				_captureList = _subject.Fingers.Where(Function(x) x.ParentObject Is Nothing).ToList()
				For Each item In _captureList
					Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(item.ImpressionType)
					Dim text As String = String.Format("{0}{1}", PositionToString(item.Position),If(isRolled, "(rolled)", String.Empty))
					Dim lvi = lvQueue.Items.Add(text)
					If IsCreateTemplateDone(item) Then
						lvi.ForeColor = Color.Green
					End If
				Next item

				If (Not NextTask()) Then
					SetError("Failed to start capturing")
				End If
			End If
		End Sub

		Private Sub CaptureFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			CancelAndWait()
			If _current IsNot Nothing AndAlso (Not IsCreateTemplateDone(_current)) Then
				_current.Image = Nothing
			End If
		End Sub

		Private Sub BtnPreviousClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrevious.Click
			Dim index As Integer = _captureList.IndexOf(_current) - 1
			If index >= 0 Then
				StartTask(_captureList(index))
			End If
		End Sub

		Private Sub BtnNextClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
			NextTask()
		End Sub

		Private Sub BtnRescanClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRescan.Click
			_current.Image = Nothing
			Dim index As Integer = _captureList.IndexOf(_current)
			lvQueue.Items(index).ForeColor = Color.Black

			StartTask(_current)
		End Sub

		Private Sub BtnAcceptClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAccept.Click
			If _current IsNot Nothing Then
				Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, _subject)
				task.Biometric = _current
				Thread.VolatileWrite(_isProcessing, True)
				_biometricClient.BeginPerformTask(task, AddressOf OnCreateTemplateCompleted, Nothing)
				SetStatus("Extracting record(s). Please wait ...")
			End If
			EnableControls()
		End Sub

		Private Sub LvQueueSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvQueue.SelectedIndexChanged
			Dim selected As ListView.SelectedIndexCollection = lvQueue.SelectedIndices
			If selected.Count > 0 Then
				Dim current As Integer = _captureList.IndexOf(_current)
				Dim index As Integer = selected(0)
				If index <> current Then
					StartTask(_captureList(index))
				End If
			End If
		End Sub

		Private Sub OnCreateTemplateCompleted(ByVal r As IAsyncResult)
			If InvokeRequired Then
				BeginInvoke(New AsyncCallback(AddressOf OnCreateTemplateCompleted), r)
			Else
				Dim status As NBiometricStatus = NBiometricStatus.None
				Try
					_biometricClient.EndPerformTask(r)

					status = _current.Status
					If status = NBiometricStatus.Ok Then
						Dim index As Integer = _captureList.IndexOf(_current)
						lvQueue.Items(index).ForeColor = Color.Green
						SetStatus(Color.Green, Color.White, "Create template completed successfully")
					Else
						SetError("Create template failed, status = {0}", EnumToString(status))
					End If
				Catch ex As Exception
					Utilities.ShowError(ex)
					SetError("Create template failed: {0}", ex.Message)
				Finally
					Thread.VolatileWrite(_isProcessing, False)
					EnableControls()
				End Try

				If status = NBiometricStatus.Ok Then
					NextTask()
				End If
			End If
		End Sub

		Private Sub OnSegmentManipulationEnded(ByVal sender As Object, ByVal e As EventArgs)
			If _current IsNot Nothing AndAlso _biometricClient.FingersCalculateNfiq Then
				Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.AssessQuality, _subject)
				task.Biometric = _current
				Thread.VolatileWrite(_isProcessing, True)
				_biometricClient.BeginPerformTask(task, AddressOf OnAssessQualityCompleted, Nothing)
				EnableControls()
			End If
		End Sub

		Private Sub OnAssessQualityCompleted(ByVal result As IAsyncResult)
			If InvokeRequired Then
				BeginInvoke(New AsyncCallback(AddressOf OnAssessQualityCompleted), result)
			Else
				Try
					_biometricClient.EndPerformTask(result)
					SetStatus(Color.Green, Color.White, "Successfully captured {0}. Adjust segment(s) if needed and press accept button", PositionToString(_current.Position))
				Catch ex As Exception
					SetError("Assess quality failed: {0}", ex.Message)
					Utilities.ShowError(ex)
				Finally
					Thread.VolatileWrite(_isProcessing, False)
					EnableControls()
				End Try
			End If
		End Sub

		Private Sub OnPropertyChanged(ByVal sender As Object, ByVal args As PropertyChangedEventArgs)
			If args.PropertyName = "Status" Then
				BeginInvoke(New Action(Of NBiometricStatus)(AddressOf OnStatusChanged), _current.Status)
			End If
		End Sub
		Private Sub OnStatusChanged(ByVal status As Object)
			Dim statusColor As Color = If(status = NBiometricStatus.Ok OrElse status = NBiometricStatus.None, Color.Green, Color.Red)
			lblStatus.Text = String.Format("Status: {0}", EnumToString(status))
			lblStatus.ForeColor = statusColor
		End Sub

		Private Sub OnCaptureCompleted(ByVal result As IAsyncResult)
			If InvokeRequired Then
				BeginInvoke(New AsyncCallback(AddressOf OnCaptureCompleted), result)
			Else
				Try
					_biometricClient.EndPerformTask(result)

					Dim status As NBiometricStatus = _current.Status
					If status = NBiometricStatus.Ok Then
						SetStatus(Color.Green, Color.White, "Successfully captured {0}. Adjust segment(s) if needed and press accept button", PositionToString(_current.Position))
					Else
						SetError("Operation failed, status = {0}", EnumToString(status))
					End If
				Catch ex As Exception
					SetError(ex.Message)
					Utilities.ShowError(ex)
				Finally
					_isCapturing = False
					EnableControls()
					RemoveHandler _current.PropertyChanged, AddressOf OnPropertyChanged
				End Try
			End If
		End Sub

		#End Region

		#Region "Private methods"

		Private Sub SetStatus(ByVal backColor As Color, ByVal foreColor As Color, ByVal format As String, ParamArray ByVal args() As Object)
			lblInfo.BackColor = backColor
			lblInfo.ForeColor = foreColor
			lblInfo.Text = String.Format(format, args)
		End Sub

		Private Sub SetStatus(ByVal format As String, ParamArray ByVal args() As Object)
			SetStatus(SystemColors.Control, Color.Black, format, args)
		End Sub

		Private Sub SetError(ByVal format As String, ParamArray ByVal args() As Object)
			SetStatus(Color.Red, Color.White, format, args)
		End Sub

		Private Sub FinishTask()
			CancelAndWait()
			If _current IsNot Nothing Then
				If IsCaptureDone(_current) AndAlso (Not IsCreateTemplateDone(_current)) Then
					_current.Image = Nothing ' Reset partial progress
				End If
			End If
		End Sub

		Private Function NextTask() As Boolean
			If _captureList IsNot Nothing AndAlso _captureList.Count > 0 Then
				Dim index As Integer = 0
				If _current IsNot Nothing Then
					index = _captureList.IndexOf(_current) + 1
				End If

				If index <> _captureList.Count Then
					StartTask(_captureList(index))
					Return True
				End If
			End If

			Return False
		End Function

		Private Sub StartTask(ByVal task As NFinger)
			If _current IsNot Nothing Then
				If IsCaptureDone(_current) AndAlso (Not IsCreateTemplateDone(_current)) Then
					If (Not Utilities.ShowQuestion(Me, "Records not extracted from this image! Press 'Yes' to continue anyway, press 'No' and then Accept button to extract records")) Then
						toolTip.Show("Accept image and extract recors", btnAccept)
						Return
					End If
				End If
			End If

			DisableNavigation()
			FinishTask()

			fingerView.Finger = Nothing
			_current = task
			fSelector.SelectedPosition = task.Position
			Dim isRolled As Boolean = NBiometricTypes.IsImpressionTypeRolled(_current.ImpressionType)
			fSelector.IsRolled = isRolled
			SetStatus(If(isRolled, "Please roll {0} finger on scanner", "Please place {0} on scanner"), PositionToString(task.Position))

			Dim index As Integer = _captureList.IndexOf(_current)
			lvQueue.Items(index).Selected = True

			fingerView.Finger = _current
			If (Not IsCreateTemplateDone(_current)) Then
				Dim biometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.Segment Or NBiometricOperations.AssessQuality, _subject)
				biometricTask.Biometric = _current
				AddHandler _current.PropertyChanged, AddressOf OnPropertyChanged
				_isCapturing = True
				_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCaptureCompleted, Nothing)
			Else
				SetStatus(Color.Green, Color.White, "Record(s) successfully extracted")
			End If
			EnableControls()
		End Sub

		Private Function IsCaptureDone(ByVal finger As NFinger) As Boolean
			Return (Not _isCapturing) AndAlso finger.Image IsNot Nothing
		End Function

		Private Function IsCreateTemplateDone(ByVal finger As NFinger) As Boolean
			If finger Is Nothing OrElse (Not IsCaptureDone(finger)) OrElse finger.Status <> NBiometricStatus.Ok Then
				Return False
			Else
				Dim attributes() As NFAttributes = finger.Objects.ToArray()
				If (Not attributes.All(Function(x) x.Status = NBiometricStatus.Ok OrElse x.Status = NBiometricStatus.ObjectMissing)) Then
					Return False
				End If

				Dim children() As NFinger = attributes.Select(Function(x) CType(x.Child, NFinger)).Where(Function(x) x IsNot Nothing).ToArray()
				If children.Length = 0 Then
					Return attributes.All(Function(x) x.Template IsNot Nothing)
				Else
					Return children.All(Function(x) IsCreateTemplateDone(x))
				End If
			End If
		End Function

		Private Sub EnableControls()
			If _captureList IsNot Nothing AndAlso _captureList.Count > 0 Then
				Dim isProcessing As Boolean = Thread.VolatileRead(_isProcessing)
				Dim isCapturing As Boolean = Thread.VolatileRead(_isCapturing)
				Dim tool = TryCast(fingerView.ActiveTool, NFingerView.SegmentManipulationTool)
				If tool IsNot Nothing Then
					tool.AllowManipulations = (Not isProcessing) AndAlso Not isCapturing AndAlso _current IsNot Nothing AndAlso (_current.Status = NBiometricStatus.Ok Or IsCaptureDone(_current))
					fingerView.Invalidate()
				End If
				btnPrevious.Enabled = _current IsNot _captureList.First() AndAlso Not isProcessing
				btnNext.Enabled = _current IsNot _captureList.Last() AndAlso Not isProcessing
				btnAccept.Enabled = (Not isCapturing) AndAlso (Not isProcessing) AndAlso IsCaptureDone(_current) AndAlso Not IsCreateTemplateDone(_current)
				btnRescan.Enabled = (Not isCapturing) AndAlso (Not isProcessing) AndAlso IsCaptureDone(_current)
				lvQueue.Enabled = True
			End If
			lblStatus.Visible = _isCapturing
		End Sub

		Private Sub DisableNavigation()
			btnNext.Enabled = False
			btnPrevious.Enabled = False
			btnRescan.Enabled = False
			btnAccept.Enabled = False
			lvQueue.Enabled = False
		End Sub

		#End Region

		#Region "Private static methods"

		Private Shared Function PositionToString(ByVal value As NFPosition) As String
			Select Case value
				Case NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainThumbs
					Return EnumToString(value).Replace("Plain ", String.Empty)
				Case Else
					Return EnumToString(value)
			End Select
		End Function

		Private Shared Function EnumToString(ByVal value As System.Enum) As String
			Dim sb As New StringBuilder()
			For Each c As Char In value.ToString()
				If Char.IsUpper(c) Then
					sb.Append(" "c)
				End If
				sb.Append(c)
			Next c
			Return sb.ToString().Trim()
		End Function

		#End Region
	End Class
End Namespace
