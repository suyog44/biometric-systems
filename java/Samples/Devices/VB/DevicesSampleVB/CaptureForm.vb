Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports System.Threading
Imports Neurotec.Devices
Imports Neurotec.Images
Imports System.Diagnostics
Imports Neurotec.Media

Partial Public Class CaptureForm
	Inherits Form
#Region "Private fields"

	Private _autoCaptureStart As Boolean
	Private _device As NDevice
	Private _gatherImages As Boolean
	Private _forceCapture As Boolean
	Private _isCapturing As Boolean
	Private _fps As Integer
	Private _bitmap, _finalBitmap As Bitmap
	Private _userStatus, _finalUserStatus As String
	Private _sw As Stopwatch
	Private _timestamps As Queue(Of TimeSpan)
	Private _lastReportTime As TimeSpan = TimeSpan.Zero
	Private _imagesPath As String
	Private _imageCount As Integer = 0

#End Region

#Region "Protected fields"

	Protected ReadOnly _statusLock As Object = New Object()

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Protected methods"

	Protected Overridable Sub OnDeviceChanged()
		If _device Is Nothing Then
			Text = "No device"
		Else
			Text = _device.DisplayName
		End If
		OnStatusChanged()
	End Sub

	Protected Overridable Sub OnStatusChanged()
		SyncLock _statusLock
			Dim theBitmap As Bitmap
			Dim theUserStatus As String
			statusTextBox.Clear()
			If _isCapturing Then
				statusTextBox.AppendText(String.Format("Capturing ({0} fps)", _fps))
				theBitmap = _bitmap
				theUserStatus = _userStatus
			Else
				statusTextBox.AppendText(String.Format("Finished"))
				theBitmap = _finalBitmap
				theUserStatus = _finalUserStatus
			End If
			If pictureBox.Image IsNot theBitmap Then
				If pictureBox.Image IsNot Nothing Then
					pictureBox.Image.Dispose()
					pictureBox.Image = Nothing
				End If
				pictureBox.Image = theBitmap
			End If
			If theBitmap IsNot Nothing Then
				statusTextBox.AppendText(String.Format(" ({0}x{1}, {2}x{3} ppi)", theBitmap.Width, theBitmap.Height, theBitmap.HorizontalResolution, theBitmap.VerticalResolution))
			End If
			If theUserStatus IsNot Nothing Then
				statusTextBox.AppendText(": ")
				statusTextBox.AppendText(theUserStatus)
			End If
			statusTextBox.AppendText(Environment.NewLine)
			forceButton.Enabled = _isCapturing
			closeButton.Text = If(_isCapturing, "Cancel", "Close")
			closeButton.DialogResult = If(_isCapturing, System.Windows.Forms.DialogResult.Cancel, System.Windows.Forms.DialogResult.OK)
			AcceptButton = If(_isCapturing, Nothing, closeButton)
		End SyncLock
	End Sub

	Protected Sub CheckIsBusy()
		If backgroundWorker.IsBusy Then
			Throw New InvalidOperationException("Capturing is running")
		End If
	End Sub

	Protected Overridable Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return True
	End Function

	Protected Overridable Sub OnCaptureStarted()
		_isCapturing = True
		If InvokeRequired Then
			BeginInvoke(New MethodInvoker(AddressOf OnStatusChanged))
		Else
			OnStatusChanged()
		End If
	End Sub

	Protected Overridable Sub OnCaptureFinished()
		_isCapturing = False
		If InvokeRequired Then
			BeginInvoke(New MethodInvoker(AddressOf OnStatusChanged))
		Else
			OnStatusChanged()
		End If
	End Sub

	Protected Function OnImage(ByVal image As NImage, ByVal userStatus As String, ByVal imageName As String, ByVal isFinal As Boolean) As Boolean
		SyncLock _statusLock
			If (Not isFinal) Then
				Dim elapsed As TimeSpan = _sw.Elapsed
				_timestamps.Enqueue(elapsed)
				If (elapsed.Subtract(_lastReportTime)).TotalSeconds >= 0.3 Then
					Dim s As Double
					Do
						s = (elapsed - _timestamps.Peek()).TotalSeconds
						If _timestamps.Count <= 1 OrElse s <= 1 Then
							Exit Do
						End If
						_timestamps.Dequeue()
					Loop
					_fps = If(s > Double.Epsilon, CInt(Fix(Math.Round(_timestamps.Count / s))), 0)
					_lastReportTime = elapsed
				End If
			End If
			If _gatherImages AndAlso image IsNot Nothing Then
				Dim str As String = Nothing
				If imageName IsNot Nothing Then
					str = String.Concat("_", imageName)
				End If
				image.Save(Path.Combine(_imagesPath, String.Format("{0}{1}.png", IIf(isFinal, "Final", (_imageCount).ToString("D8")), str)))
				_imageCount += 1
			End If
			If isFinal Then
				If image Is Nothing Then
					_finalBitmap = Nothing
				Else
					_finalBitmap = image.ToBitmap()
				End If
				_finalUserStatus = userStatus
			Else
				If image Is Nothing Then
					_bitmap = Nothing
				Else
					_bitmap = image.ToBitmap()
				End If
				_userStatus = userStatus
			End If
		End SyncLock
		BeginInvoke(New MethodInvoker(AddressOf OnStatusChanged))
		Return _forceCapture
	End Function

	Protected Sub WriteParameter(ByVal writer As XmlWriter, ByVal key As String, ByVal parameter As Object)
		writer.WriteStartElement("Parameter")
		writer.WriteAttributeString("Name", key)
		writer.WriteString(parameter.ToString())
		writer.WriteEndElement()
	End Sub

	Protected Overridable Sub OnWriteScanParameters(ByVal writer As XmlWriter)
	End Sub

	Protected Overridable Sub OnCancelCapture()
		backgroundWorker.CancelAsync()
	End Sub

	Protected Overridable Sub OnCapture()
		Throw New NotImplementedException()
	End Sub

	Private Delegate Sub AddMediaFormatsHandler(ByVal mediaFormats As IEnumerable(Of NMediaFormat), ByVal currentFormat As NMediaFormat)
	Private _suppressMediaFormatEvents As Boolean

	Protected Sub AddMediaFormats(ByVal mediaFormats As IEnumerable(Of NMediaFormat), ByVal currentFormat As NMediaFormat)
		If InvokeRequired Then
			BeginInvoke(New AddMediaFormatsHandler(AddressOf AddMediaFormats), mediaFormats, currentFormat)
		Else
			If mediaFormats Is Nothing Then
				Throw New ArgumentNullException("mediaFormats")
			End If
			_suppressMediaFormatEvents = True
			formatsComboBox.BeginUpdate()
			For Each mediaFormat As NMediaFormat In mediaFormats
				formatsComboBox.Items.Add(mediaFormat)
			Next mediaFormat
			If currentFormat IsNot Nothing Then
				Dim currentIndex As Integer = formatsComboBox.Items.IndexOf(currentFormat)
				formatsComboBox.SelectedIndex = currentIndex
			End If
			formatsComboBox.EndUpdate()
			_suppressMediaFormatEvents = False
		End If
	End Sub

	Protected Overridable Sub OnMediaFormatChanged(ByVal mediaFormat As NMediaFormat)
	End Sub

	Protected Function GetPictureArea() As Rectangle
		Dim bmp As Bitmap = _finalBitmap
		If _finalBitmap Is Nothing Then
			bmp = _bitmap
		End If

		Dim frameWidth As Integer = 0
		Dim frameHeight As Integer = 0
		If bmp IsNot Nothing Then
			frameWidth = bmp.Width
			frameHeight = bmp.Height
		End If
		Dim cs As Size = pictureBox.ClientSize
		Dim zoom As Single = 1
		If frameWidth <> 0 AndAlso frameHeight <> 0 Then
			zoom = Math.Min(cs.Width / CSng(frameWidth), cs.Height / CSng(frameHeight))
		End If
		Dim sx As Single = frameWidth * zoom
		Dim sy As Single = frameHeight * zoom
		Return New Rectangle(CInt(Fix(Math.Round((cs.Width - sx) / 2))), CInt(Fix(Math.Round((cs.Height - sy) / 2))), CInt(Fix(Math.Round(sx))), CInt(Fix(Math.Round(sy))))
	End Function

#End Region

#Region "Protected properties"

	Protected ReadOnly Property IsCapturing() As Boolean
		Get
			Return _isCapturing
		End Get
	End Property

	Protected ReadOnly Property HasFinal() As Boolean
		Get
			Return _finalBitmap IsNot Nothing
		End Get
	End Property

	Protected Property AutoCaptureStart() As Boolean
		Get
			Return _autoCaptureStart
		End Get
		Set(ByVal value As Boolean)
			_autoCaptureStart = value
		End Set
	End Property

	Protected Property EnableForcedCapture() As Boolean
		Get
			Return forceButton.Visible
		End Get
		Set(ByVal value As Boolean)
			forceButton.Visible = value
		End Set
	End Property

	Protected ReadOnly Property IsCancellationPending() As Boolean
		Get
			Return backgroundWorker.CancellationPending
		End Get
	End Property

#End Region

#Region "Public methods"

	Public Sub WaitForCaptureToFinish()
		Do While backgroundWorker.IsBusy
			Thread.Sleep(0)
			Application.DoEvents()
		Loop
	End Sub

#End Region

#Region "Public properties"

	Public Property Device() As NDevice
		Get
			Return _device
		End Get
		Set(ByVal value As NDevice)
			If _device IsNot value Then
				If _device IsNot Nothing AndAlso (Not IsValidDeviceType(_device.GetType())) Then
					Throw New ArgumentException("Invalid NDevice type")
				End If
				CheckIsBusy()
				_device = value
				OnDeviceChanged()
			End If
		End Set
	End Property

	Public Property GatherImages() As Boolean
		Get
			Return _gatherImages
		End Get
		Set(ByVal value As Boolean)
			If _gatherImages <> value Then
				CheckIsBusy()
				_gatherImages = value
			End If
		End Set
	End Property

#End Region

	Private Sub CaptureForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		If _device Is Nothing Then
			Return
		End If

		If _gatherImages Then
			_imagesPath = Path.Combine(String.Format("{0}_{1}", Device.Make, Device.Model), Guid.NewGuid().ToString())
			Directory.CreateDirectory(_imagesPath)
			_imageCount = 0

			Dim writer As XmlWriter = XmlWriter.Create(Path.Combine(_imagesPath, "ScanInfo.xml"))
			writer.WriteStartElement("Scan")
			OnWriteScanParameters(writer)
			writer.Close()
		End If
		If (Not _autoCaptureStart) Then
			OnCaptureStarted()
		End If
		backgroundWorker.RunWorkerAsync()
	End Sub

	Private Sub CaptureForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If (Not backgroundWorker.IsBusy) Then
			Return
		End If

		OnCancelCapture()
		WaitForCaptureToFinish()
	End Sub

	Private Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles backgroundWorker.DoWork
		_timestamps = New Queue(Of TimeSpan)()
		_sw = Stopwatch.StartNew()
		OnCapture()
	End Sub

	Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
		If (Not _autoCaptureStart) Then
			OnCaptureFinished()
		End If
		If e.Error Is Nothing Then
			Return
		End If

		statusTextBox.AppendText("Error: ")
		statusTextBox.AppendText(e.Error.ToString())
	End Sub

	Private Sub forceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles forceButton.Click
		_forceCapture = True
	End Sub

	Private Sub closeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles closeButton.Click
		Close()
	End Sub

	Private Sub formatsComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles formatsComboBox.SelectedIndexChanged
		If _suppressMediaFormatEvents Then
			Return
		End If
		Dim mediaFormat As NMediaFormat = Nothing
		If formatsComboBox.SelectedIndex >= 0 Then
			mediaFormat = CType(formatsComboBox.Items(formatsComboBox.SelectedIndex), NMediaFormat)
		End If
		OnMediaFormatChanged(mediaFormat)
	End Sub

	Private Sub customizeFormatButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles customizeFormatButton.Click
		Dim selectedFormat As NMediaFormat = TryCast(formatsComboBox.SelectedItem, NMediaFormat)
		If selectedFormat Is Nothing Then
			Dim device As NDevice = Me.Device
			If (device.DeviceType And NDeviceType.Camera) = NDeviceType.Camera Then
				selectedFormat = New NVideoFormat()
			ElseIf (device.DeviceType And NDeviceType.Microphone) = NDeviceType.Microphone Then
				selectedFormat = New NAudioFormat()
			Else
				Throw New NotImplementedException()
			End If
		End If
		Dim customFormat As NMediaFormat = CustomizeFormatForm.CustomizeFormat(selectedFormat)
		If customFormat IsNot Nothing Then
			Dim index As Integer = formatsComboBox.Items.IndexOf(customFormat)
			If index = -1 Then
				formatsComboBox.Items.Add(customFormat)
			End If
			formatsComboBox.SelectedItem = customFormat
		End If
	End Sub
End Class
