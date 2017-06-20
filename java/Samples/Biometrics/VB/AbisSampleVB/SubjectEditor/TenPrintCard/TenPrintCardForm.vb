Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports Neurotec.Samples.TenPrintCardForm

Partial Public Class TenPrintCardForm
	Inherits Form
#Region "Private fields"

	Private ReadOnly _painter As FramePainter
	Private _biometricClient As NBiometricClient
	Private _subject As NSubject

#End Region

#Region "Public properties"

	Public Property Result() As NSubject
		Get
			Return _subject
		End Get
		Set(ByVal value As NSubject)
			_subject = value
		End Set
	End Property

	Public Property BiometricClient() As NBiometricClient
		Get
			Return _biometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			_biometricClient = value
		End Set
	End Property

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		_painter = New FramePainter(My.Resources.TenPrintCard)
		_painter.Visible = False

		Controls.Add(_painter)
	End Sub

#End Region

#Region "Private methods"

	Private Sub SetImage(ByVal img As NImage)
		_painter.SetImage(img)
		tsbOK.Enabled = img IsNot Nothing
	End Sub

	Private Function MakeSubject() As NSubject
		Dim srcimg As Dictionary(Of Integer, NImage) = _painter.GetFramedFingerprints()
		Dim subject As New NSubject()
		Dim finger As NFinger
		Dim positions() As NFPosition = {NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle, NFPosition.LeftThumb, NFPosition.LeftIndex, NFPosition.LeftMiddle, NFPosition.LeftRing, NFPosition.LeftLittle}

		For i As Integer = 0 To positions.Length - 1
			Try
				finger = New NFinger With {.Position = positions(i), .ImpressionType = NFImpressionType.NonliveScanRolled, .Image = srcimg(i + 1)}
				subject.Fingers.Add(finger)
			Catch
			End Try
		Next i

		finger = New NFinger With {.Position = NFPosition.PlainLeftFourFingers, .ImpressionType = NFImpressionType.NonliveScanPlain, .Image = srcimg(11)}
		subject.Fingers.Add(finger)

		finger = New NFinger With {.Position = NFPosition.LeftThumb, .ImpressionType = NFImpressionType.NonliveScanPlain, .Image = srcimg(12)}
		subject.Fingers.Add(finger)

		finger = New NFinger With {.Position = NFPosition.RightThumb, .ImpressionType = NFImpressionType.NonliveScanPlain, .Image = srcimg(13)}
		subject.Fingers.Add(finger)

		finger = New NFinger With {.Position = NFPosition.PlainRightFourFingers, .ImpressionType = NFImpressionType.NonliveScanPlain, .Image = srcimg(14)}
		subject.Fingers.Add(finger)

		For Each nImage As KeyValuePair(Of Integer, NImage) In srcimg
			nImage.Value.Dispose()
		Next nImage

		Return subject
	End Function

	Private Sub TsbOpenFileClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenFile.Click
		If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			_painter.Visible = True
			_painter.Dock = DockStyle.Fill
			Dim img As NImage = NImage.FromFile(openFileDialog1.FileName)
			SetImage(img)
		End If
	End Sub

	Private Sub DoScan()
		Dim scanner As New NPhotoScannerForm(Me)
		Dim img As NImage = scanner.Scan()
		If img IsNot Nothing Then
			_painter.Visible = True
			_painter.Dock = DockStyle.Fill
			SetImage(img)
		End If
	End Sub

	Private Sub TsbScanClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbScan.Click
		DoScan()
	End Sub

	Private Sub TbsScanDefaultButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles tbsScanDefault.ButtonClick
		DoScan()
	End Sub

	Private Sub SelectDeviceToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles selectDeviceToolStripMenuItem.Click
		Dim scanner As New NPhotoScannerForm(Me)
		scanner.SelectDevice()
	End Sub

	Private Sub TsbOKClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOK.Click
		Try
			Dim operations As NBiometricOperations = NBiometricOperations.CreateTemplate
			If _biometricClient.FingersCalculateNfiq Then
				operations = operations Or NBiometricOperations.AssessQuality
			End If
			Dim task As NBiometricTask

			_subject = MakeSubject()
			task = _biometricClient.CreateTask(operations, _subject)
			LongActionDialog.ShowDialog(Me, "Creating template ...", New Action(Of NBiometricTask)(AddressOf _biometricClient.PerformTask), task)

			Dim status As NBiometricStatus = task.Status
			If task.Error IsNot Nothing Then
				Utilities.ShowError("Error while segmenting images: {0}.", task.Error)
				Return
			End If
			If status <> NBiometricStatus.Ok Then
				Dim failedList As New StringBuilder()
				Dim failed As New List(Of NFinger)(_subject.Fingers.Where(Function(x) x.Status <> NBiometricStatus.Ok OrElse x.Position = NFPosition.Unknown))
				For Each item In failed
					_subject.Fingers.Remove(item)
				Next item
				failedList.AppendLine("Failed to extract the following fingerprints:")
				For Each item As NFinger In failed.Where(Function(x) x.Status <> NBiometricStatus.Ok)
					failedList.AppendFormat("- {0}" & Constants.vbLf, item.Position)
				Next item
				Utilities.ShowInformation(failedList.ToString())
			End If
			DialogResult = System.Windows.Forms.DialogResult.OK
		Catch ex As Exception
			Utilities.ShowError("Error while segmenting images: {0}.", ex.Message)
		End Try
	End Sub

	Private Sub TenPrintCardFormKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		Select Case e.KeyCode
			Case Keys.Escape
				DialogResult = System.Windows.Forms.DialogResult.Cancel
			Case Keys.Return
				DialogResult = System.Windows.Forms.DialogResult.OK
		End Select
	End Sub

	Private Sub TenPrintCardFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If Environment.OSVersion.Platform <> PlatformID.Win32Windows Then
			toolStrip1.Items.Remove(tsbScan)
			toolStrip1.Items.Remove(selectDeviceToolStripMenuItem)
			toolStrip1.Items.Remove(tbsScanDefault)
		End If
	End Sub

#End Region
End Class
