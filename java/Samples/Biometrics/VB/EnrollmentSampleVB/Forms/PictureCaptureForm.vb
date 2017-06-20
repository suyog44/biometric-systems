Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports Neurotec.Devices
Imports Neurotec.Images
Imports Neurotec.Media

Namespace Forms
	Partial Public Class PictureCaptureForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

		#End Region

		#Region "Private fields"

		Private _capture As Boolean
		Private _pendingFormat As NMediaFormat

		#End Region

		#Region "Public properties"

		Private privateDeviceManager As NDeviceManager
		Public Property DeviceManager() As NDeviceManager
			Get
				Return privateDeviceManager
			End Get
			Set(ByVal value As NDeviceManager)
				privateDeviceManager = value
			End Set
		End Property

		Private privateImage As NImage
		Public Property Image() As NImage
			Get
				Return privateImage
			End Get
			Set(ByVal value As NImage)
				privateImage = value
			End Set
		End Property

		#End Region

		#Region "Private form events"

		Private Sub PictureCaptureFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			If DeviceManager Is Nothing Then
				Throw New ArgumentNullException("DeviceManager")
			End If

			For Each device As NDevice In DeviceManager.Devices
				If (device.DeviceType And NDeviceType.Camera) = NDeviceType.Camera Then
					cbCameras.Items.Add(device)
				End If
			Next device

			If cbCameras.Items.Count > 0 Then
				cbCameras.SelectedIndex = 0
			End If
		End Sub

		Private Sub PictureCaptureFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			CancelCapture()
		End Sub

		Private Sub BtnCaptureClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture.Click
			Dim camera As NCamera = TryCast(cbCameras.SelectedItem, NCamera)
			If camera IsNot Nothing Then
				If backgroundWorker.IsBusy Then
					_capture = True
				Else
					If camera.IsStillCaptureSupported Then
						AddHandler camera.StillCaptured, AddressOf CameraStillCaptured
					End If
					camera.StartCapturing()
					backgroundWorker.RunWorkerAsync(camera)
				End If
			Else
				Utilities.ShowWarning("Camera not selected")
			End If
		End Sub

		Private Sub CameraStillCaptured(ByVal sender As Object, ByVal e As NCameraStillCapturedEventArgs)
			BeginInvoke(New Action(Of NImage)(AddressOf OnStillCaptured), NImage.FromStream(e.Stream))
		End Sub

		Private Sub CbCamerasSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCameras.SelectedIndexChanged
			CancelCapture()

			Dim camera As NCamera = TryCast(cbCameras.SelectedItem, NCamera)
			If camera Is Nothing Then
				Return
			End If

			cbFormats.Items.Clear()
			cbFormats.BeginUpdate()
			Try
				If camera.IsStillCaptureSupported Then
					AddHandler camera.StillCaptured, AddressOf CameraStillCaptured
				End If
				camera.StartCapturing()

				For Each item As NMediaFormat In camera.GetFormats()
					cbFormats.Items.Add(item)
				Next item

				Dim current As NMediaFormat = camera.GetCurrentFormat()
				If current IsNot Nothing Then
					If cbFormats.Items.Contains(current) Then
						cbFormats.SelectedItem = current
					Else
						cbFormats.Items.Insert(0, current)
						cbFormats.SelectedItem = current
					End If
				End If
			Finally
				cbFormats.EndUpdate()
			End Try

			backgroundWorker.RunWorkerAsync(camera)
		End Sub

		Private Sub ViewImage(ByVal image As Bitmap)
			Dim old As Image = pictureBox.Image
			pictureBox.Image = image
			If old IsNot Nothing Then
				old.Dispose()
			End If
		End Sub

		Private Sub BackgroundWorkerDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles backgroundWorker.DoWork
			Dim camera As NCamera = TryCast(e.Argument, NCamera)
			If camera Is Nothing Then
				Return
			End If

			Try
				Do While Not backgroundWorker.CancellationPending
					If _pendingFormat IsNot Nothing Then
						camera.SetCurrentFormat(_pendingFormat)
						_pendingFormat = Nothing
					End If
					Using frame As NImage = camera.GetFrame()
						If frame Is Nothing Then
							Exit Do
						End If
						BeginInvoke(New Action(Of Bitmap)(AddressOf ViewImage), frame.ToBitmap())
						If _capture Then
							Image = TryCast(frame.Clone(), NImage)
							_capture = False
							Exit Do
						End If
					End Using
				Loop
			Finally
				camera.StopCapturing()
				If camera.IsStillCaptureSupported Then
					RemoveHandler camera.StillCaptured, AddressOf CameraStillCaptured
				End If
			End Try
		End Sub

		Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
			If Image Is Nothing Then
				Utilities.ShowWarning("Image not captured")
				Return
			End If

			CancelCapture()
			DialogResult = System.Windows.Forms.DialogResult.OK
		End Sub

		Private Sub CbFormatsSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbFormats.SelectedIndexChanged
			_pendingFormat = TryCast(cbFormats.SelectedItem, NMediaFormat)
		End Sub

		#End Region

		#Region "Private methods"

		Private Sub CancelCapture()
			If backgroundWorker.IsBusy Then
				backgroundWorker.CancelAsync()
				Do While backgroundWorker.IsBusy
					Thread.Sleep(0)
					Application.DoEvents()
				Loop
			End If
		End Sub

		Private Sub OnStillCaptured(ByVal img As NImage)
			CancelCapture()
			Image = img
		End Sub

		#End Region
	End Class
End Namespace
