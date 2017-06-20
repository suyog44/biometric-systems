Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class TenPrintCardPrintForm
	Inherits PrintPreviewDialog
#Region "Private fields"

	Private ReadOnly _rolledPositions() As NFPosition = {NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle, NFPosition.LeftThumb, NFPosition.LeftIndex, NFPosition.LeftMiddle, NFPosition.LeftRing, NFPosition.LeftLittle}

	Private _painter As FramePainter
	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _images As Dictionary(Of Integer, NImage)

#End Region

#Region "Public constructor"

	Public Sub New(ByVal frameDefinition As String)
		InitializeComponent()
		_painter = New FramePainter(frameDefinition)
		_images = New Dictionary(Of Integer, NImage)()
	End Sub

#End Region

#Region "Public properties"

	Public Property Subject() As NSubject
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

#Region "Events"

	Private Sub TenPrintCardPrintFormShown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
		Dim doc = New PrintDocument()
		AddHandler doc.PrintPage, AddressOf PrintPreviewEvent
		AddHandler doc.BeginPrint, AddressOf BeginPrintEvent
		Document = doc
	End Sub

	Private Sub PrintPreviewEvent(ByVal o As Object, ByVal e As PrintPageEventArgs)
		Dim graphics = e.Graphics
		Dim rect = _painter.GetFrameSplitting()

		graphics.TranslateTransform(13, 25)
		If _subject.Fingers.Count > 0 Then
			graphics.CompositingQuality = CompositingQuality.HighQuality
			graphics.SmoothingMode = SmoothingMode.HighQuality
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic

			Dim subject = MakeSubject()

			SegmentFingers(subject)

			For i As Integer = 1 To 14
				If _images.ContainsKey(i) Then
					Dim currrentRect = rect(i)
					Dim image = _images(i).ToBitmap()
					Dim tmpRect = New Rectangle(currrentRect.X + 2, currrentRect.Y + 2, currrentRect.Width - 4, currrentRect.Height - 4)

					Dim imagePosition = Me.ImagePosition(tmpRect, image)
					graphics.DrawImage(image, imagePosition)
				End If
			Next i
		End If

		_painter.DrawForm(graphics)

	End Sub

	Private Sub BeginPrintEvent(ByVal sender As Object, ByVal e As PrintEventArgs)
		Document.DefaultPageSettings.PrinterResolution = New PrinterResolution With {.Kind = PrinterResolutionKind.Custom, .X = 100, .Y = 100}
		Document.DefaultPageSettings.PaperSize = Document.PrinterSettings.PaperSizes.Cast(Of PaperSize)().Where(Function(x) x.Kind = PaperKind.A4).First()
		If (Not Document.PrintController.IsPreview) Then
			PrintDialog1.Document = Document
			PrintDialog1.AllowSelection = False
			PrintDialog1.AllowSomePages = False
			If PrintDialog1.ShowDialog() <> DialogResult.OK Then
				e.Cancel = True
			End If
		End If
	End Sub

#End Region

#Region "Private methods"

	Private Function MakeSubject() As NSubject
		Dim subject = New NSubject()
		Dim fingers = _subject.Fingers.Where(Function(x) x.Image IsNot Nothing)
		Dim rolled = fingers.Where(Function(x) NBiometricTypes.IsImpressionTypeRolled(x.ImpressionType)).Where(Function(x) NBiometricTypes.IsPositionOneOf(x.Position, _rolledPositions)).Select(Function(x) New NFinger With {.Image = x.Image, .Position = x.Position, .ImpressionType = x.ImpressionType}).GroupBy(Function(x) x.Position).Select(Function(x) x.First())

		For Each finger In rolled
			subject.Fingers.Add(finger)
		Next finger

		Dim thumbPositions = New NFPosition() {NFPosition.LeftThumb, NFPosition.RightThumb, NFPosition.PlainRightThumb, NFPosition.PlainLeftThumb}
		Dim plain = fingers.Where(Function(x) NBiometricTypes.IsImpressionTypePlain(x.ImpressionType)).Where(Function(x) NBiometricTypes.IsPositionFourFingers(x.Position) OrElse NBiometricTypes.IsPositionOneOf(x.Position, thumbPositions)).Select(Function(x) New NFinger With {.Image = x.Image, .Position = x.Position, .ImpressionType = x.ImpressionType}).GroupBy(Function(x) x.Position).Select(Function(x) x.First())

		For Each finger In plain
			subject.Fingers.Add(finger)
		Next finger

		Return subject
	End Function

	Private Sub SegmentFingers(ByVal subject As NSubject)
		_images.Clear()
		Using task = _biometricClient.CreateTask(NBiometricOperations.Segment, subject)
			_biometricClient.PerformTask(task)
			If task.Status <> NBiometricStatus.Ok Then
				Throw New Exception("Segmentation failed")
			End If

			Dim segmentedFingers = subject.Fingers.Where(Function(x) x.ParentObject IsNot Nothing)
			Dim rolled = segmentedFingers.Where(Function(x) NBiometricTypes.IsImpressionTypeRolled(x.ImpressionType))

			Dim rolledPosition = _rolledPositions.ToList()
			For Each finger In rolled
				If rolledPosition.Contains(finger.Position) Then
					Dim index As Integer = rolledPosition.IndexOf(finger.Position) + 1
					_images.Add(index, finger.Image)
				End If
			Next finger

			Dim thumbs = segmentedFingers.Where(Function(x) NBiometricTypes.IsImpressionTypePlain(x.ImpressionType)).Where(Function(x) NBiometricTypes.IsPositionSingleFinger(x.Position) AndAlso x.Position.ToString().Contains("Thumb"))
			Dim leftThumb = thumbs.Where(Function(x) NBiometricTypes.IsPositionLeft(x.Position)).FirstOrDefault()
			If leftThumb IsNot Nothing Then
				_images.Add(12, leftThumb.Image)
			End If

			Dim rightThumb = thumbs.Where(Function(x) NBiometricTypes.IsPositionRight(x.Position)).FirstOrDefault()
			If rightThumb IsNot Nothing Then
				_images.Add(13, rightThumb.Image)
			End If

			Dim fourFingers = subject.Fingers.Where(Function(x) x.ParentObject Is Nothing AndAlso NBiometricTypes.IsPositionFourFingers(x.Position))
			Dim leftFour = fourFingers.Where(Function(x) NBiometricTypes.IsPositionLeft(x.Position)).FirstOrDefault()
			If leftFour IsNot Nothing Then
				_images.Add(11, GetFingerImage(leftFour))
			End If

			Dim rightFour = fourFingers.Where(Function(x) NBiometricTypes.IsPositionRight(x.Position)).FirstOrDefault()
			If rightFour IsNot Nothing Then
				_images.Add(14, GetFingerImage(rightFour))
			End If
		End Using
	End Sub

	Private Function GetFingerImage(ByVal finger As NFinger) As NImage
		Dim rect As Rectangle = finger.Objects.First().BoundingRect
		For i As Integer = 1 To finger.Objects.Count - 1
			rect = Rectangle.Union(rect, finger.Objects(i).BoundingRect)
		Next i

		Dim fingerImage = finger.Image
		If rect.X < 0 Then
			rect.X = 0
		End If
		If rect.Y < 0 Then
			rect.Y = 0
		End If
		If rect.Width + rect.X > fingerImage.Width Then
			rect.Width = CInt(Fix(fingerImage.Width)) - rect.X
		End If
		If rect.Height + rect.Y > fingerImage.Height Then
			rect.Height = CInt(Fix(fingerImage.Height)) - rect.Y
		End If

		Return finger.Image.Crop(CUInt(rect.X), CUInt(rect.Y), CUInt(rect.Width), CUInt(rect.Height))
	End Function

	Private Function ImagePosition(ByVal rect As Rectangle, ByVal image As Image) As Rectangle
		Dim widthRatio As Double = 1.0 * rect.Width / image.Width
		Dim heightRatio As Double = 1.0 * rect.Height / image.Height

		Dim ratio As Double = If(widthRatio < heightRatio, widthRatio, heightRatio)
		Dim width As Integer = CInt(Fix(Math.Truncate(image.Width * ratio)))
		Dim height As Integer = CInt(Fix(Math.Truncate(image.Height * ratio)))

		Dim rectangle = New Rectangle(rect.X, rect.Y, width, height)
		Dim centerWidth As Integer = rect.Width - width
		If centerWidth > 0 Then
			rectangle.X += centerWidth / 2
		End If

		Dim centerHeight As Integer = rect.Height - height
		If centerHeight > 0 Then
			rectangle.Y += centerHeight / 2
		End If

		Return rectangle
	End Function

#End Region

End Class
