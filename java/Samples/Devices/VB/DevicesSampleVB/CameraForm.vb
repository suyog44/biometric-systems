
Imports System
Imports Neurotec.Devices
Imports Neurotec.Images
Imports System.Drawing
Imports Neurotec.Gui
Imports System.Windows.Forms

Partial Public Class CameraForm
	Inherits CaptureDeviceForm
#Region "Private fields"

	Private _cameraStatus As NCameraStatus = NCameraStatus.None
	Private _focusRegion As Nullable(Of RectangleF)
	Private _focusPen As Pen = Pens.White

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		OnDeviceChanged()
		OnCameraStatusChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Sub OnCameraStatusChanged()
		If _cameraStatus = NCameraStatus.None Then
			cameraStatusLabel.Text = Nothing
		Else
			cameraStatusLabel.Text = _cameraStatus.ToString()
		End If
	End Sub

#End Region

#Region "Protected methods"

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NCamera).IsAssignableFrom(value)
	End Function

	Protected Overrides Sub OnDeviceChanged()
		MyBase.OnDeviceChanged()
		Dim camera As NCamera = CType(Device, NCamera)
		EnableForcedCapture = camera IsNot Nothing AndAlso camera.IsStillCaptureSupported
		focusButton.Visible = camera IsNot Nothing AndAlso camera.IsFocusSupported
		resetFocusButton.Visible = focusButton.Visible
		clickToFocusCheckBox.Visible = camera IsNot Nothing AndAlso camera.IsFocusRegionSupported
	End Sub

	Protected Overrides Sub OnStatusChanged()
		MyBase.OnStatusChanged()
		focusButton.Enabled = IsCapturing
		resetFocusButton.Enabled = focusButton.Enabled
		clickToFocusCheckBox.Enabled = IsCapturing
	End Sub

	Protected Overrides Sub OnStartingCapture()
		MyBase.OnStartingCapture()
		Dim camera As NCamera = CType(Device, NCamera)
		AddHandler camera.StillCaptured, AddressOf Device_StillCaptured
	End Sub

	Protected Overrides Sub OnFinishingCapture()
		Dim camera As NCamera = CType(Device, NCamera)
		RemoveHandler camera.StillCaptured, AddressOf Device_StillCaptured
		MyBase.OnFinishingCapture()
	End Sub

	Protected Overrides Function OnObtainSample() As Boolean
		Dim camera As NCamera = CType(Device, NCamera)
		Using image As NImage = camera.GetFrame()
			If image IsNot Nothing Then
				Dim focusRegion As Nullable(Of RectangleF) = camera.FocusRegion
				SyncLock _statusLock
					_focusRegion = focusRegion
				End SyncLock
				OnImage(image, Nothing, Nothing, False)
				Return True
			Else
				Return False
			End If
		End Using
	End Function

	Protected Overrides Sub OnCaptureFinished()
		SyncLock _statusLock
			_focusRegion = Nothing
		End SyncLock
		MyBase.OnCaptureFinished()
	End Sub

#End Region

#Region "Public properties"

	Public Property ClickToFocus() As Boolean
		Get
			Return clickToFocusCheckBox.Checked AndAlso clickToFocusCheckBox.Visible
		End Get
		Set(ByVal value As Boolean)
			clickToFocusCheckBox.Checked = value
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub Device_StillCaptured(ByVal sender As Object, ByVal e As NCameraStillCapturedEventArgs)
		If (Not HasFinal) Then
			Using image As NImage = NImage.FromStream(e.Stream)
				OnImage(image, Nothing, Nothing, True)
			End Using
			BeginInvoke(New MethodInvoker(AddressOf OnCancelCapture))
		End If
	End Sub

#End Region

	Private Sub pictureBox_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pictureBox.Paint
		Dim focusRegion? As RectangleF
		SyncLock _statusLock
			focusRegion = _focusRegion
		End SyncLock
		If focusRegion.HasValue Then
			Dim area As Rectangle = GetPictureArea()
			e.Graphics.DrawRectangle(_focusPen, area.X + focusRegion.Value.X * area.Width, area.Y + focusRegion.Value.Y * area.Height, focusRegion.Value.Width * area.Width, focusRegion.Value.Height * area.Height)
		End If
	End Sub

	Private Delegate Sub OnPictureBoxClickedDelegate(ByVal e As MouseEventArgs, ByVal area As Rectangle, ByVal clickToFocus As Boolean)
	Private Sub OnPictureBoxClicked(ByVal e As MouseEventArgs, ByVal area As Rectangle, ByVal clickToFocus As Boolean)
		Dim camera As NCamera = CType(Device, NCamera)
		If camera IsNot Nothing AndAlso camera.IsFocusRegionSupported Then
			Dim focusRegion? As RectangleF
			SyncLock _statusLock
				focusRegion = _focusRegion
			End SyncLock
			Dim w As Single = 0.1F
			Dim h As Single = 0.1F
			If focusRegion.HasValue Then
				w = focusRegion.Value.Width
				h = focusRegion.Value.Height
			End If
			camera.FocusRegion = New RectangleF((e.X - area.X) / CSng(area.Width) - w / 2, (e.Y - area.Y) / CSng(area.Height) - h / 2, w, h)
			If clickToFocus AndAlso camera.IsFocusSupported Then
				_cameraStatus = camera.Focus()
				BeginInvoke(New MethodInvoker(AddressOf OnCameraStatusChanged))
			End If
		End If
	End Sub

	Private Sub focusButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles focusButton.Click
		NGui.InvokeAsync(AddressOf AnonymousMethod1)
	End Sub
	Private Sub AnonymousMethod1()
		Dim camera As NCamera = CType(Device, NCamera)
		_cameraStatus = camera.Focus()
		BeginInvoke(New MethodInvoker(AddressOf OnCameraStatusChanged))
	End Sub

	Private Sub resetFocusButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles resetFocusButton.Click
		NGui.InvokeAsync(AddressOf AnonymousMethod2)
	End Sub
	Private Sub AnonymousMethod2()
		Dim camera As NCamera = CType(Device, NCamera)
		camera.ResetFocus()
		_cameraStatus = NCameraStatus.None
		BeginInvoke(New MethodInvoker(AddressOf OnCameraStatusChanged))
	End Sub

	Private Sub forceButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles forceButton.Click
		NGui.InvokeAsync(AddressOf AnonymousMethod3)
	End Sub
	Private Sub AnonymousMethod3()
		Dim camera As NCamera = CType(Device, NCamera)
		_cameraStatus = camera.CaptureStill()
		BeginInvoke(New MethodInvoker(AddressOf OnCameraStatusChanged))
	End Sub
End Class
