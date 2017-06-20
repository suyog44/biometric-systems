Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports Neurotec.Images
Imports Neurotec.Images.Processing

Partial Public Class BandpassFilteringForm
	Inherits Form
	#Region "Private types"

	Private Enum PenType
		Circle
		Rectangle
	End Enum

	#End Region

	#Region "Private fields"

	Private ReadOnly _imageAtt As ImageAttributes
	Private ReadOnly _originalReal As NImage
	Private ReadOnly _originalImaginary As NImage
	Private ReadOnly _imgWidth As Integer
	Private ReadOnly _imgHeight As Integer
	Private ReadOnly _originalWidth As UInteger
	Private ReadOnly _originalHeight As UInteger
	Private ReadOnly _graphics As Graphics
	Private ReadOnly _gr As Graphics
	Private ReadOnly _maskBitmap As Bitmap
	Private ReadOnly _fftBitmap As Bitmap

	Private _result As NImage
	'Bitmap _bm;
	Private _lastX, _lastY As Integer
	Private _penType As PenType
	Private _allowPainting As Boolean

	#End Region

	#Region "Public properties"

	Public ReadOnly Property ResultImage() As NImage
		Get
			Return _result
		End Get
	End Property

	#End Region

	#Region "Public constructor"

	Public Sub New(ByVal image As NImage)
		Dim matrixItems()() As Single = {New Single() {1, 0, 0, 0, 0}, New Single() {0, 0, 0, 0, 0}, New Single() {0, 0, 0, 0, 0}, New Single() {0, 0, 0, 0.2F, 0}, New Single() {0, 0, 0, 0, 1}}
		Dim colorMatrix = New ColorMatrix(matrixItems)

		InitializeComponent()

		penSize.Value = 20
		_penType = PenType.Circle
		radioCircle.Checked = True
		_allowPainting = False

		_imageAtt = New ImageAttributes()
		_imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

		Try
			Dim w As UInteger
			Dim h As UInteger
			Ngip.FFTGetOptimalSize(image, w, h)

			_imgWidth = CInt(Fix(w))
			_imgHeight = CInt(Fix(h))
			_originalWidth = image.Width
			_originalHeight = image.Height
			viewFourierMask.Image = New Bitmap(CInt(Fix(w)), CInt(Fix(h)))
			_gr = Graphics.FromImage(viewFourierMask.Image)
			_maskBitmap = New Bitmap(CInt(Fix(w)), CInt(Fix(h)))
			_graphics = Graphics.FromImage(_maskBitmap)

			Dim original As NImage = NImage.Create(NPixelFormat.Grayscale8U, w, h, 0)
			original.HorzResolution = image.HorzResolution
			original.VertResolution = image.VertResolution
			original.ResolutionIsAspectRatio = False

			_result = NImage.Create(NPixelFormat.Grayscale8U, w, h, 0)
			_result.HorzResolution = original.HorzResolution
			_result.VertResolution = original.VertResolution
			_result.ResolutionIsAspectRatio = False

			NImage.Copy(image, 0, 0, _originalWidth, _originalHeight, original, w \ 2 - _originalWidth \ 2, h \ 2 - _originalHeight \ 2)

			Ngip.FFT(original, _originalReal, _originalImaginary)

			Dim fftImage As NImage = Ngip.CreateMagnitudeFromSpectrum(_originalReal, _originalImaginary)
			_fftBitmap = NImage.FromImage(NPixelFormat.Rgb8U, 0, ShiftFFT(fftImage)).ToBitmap()
		Catch ex As Exception
			Utilities.ShowError("Error creating FFT image: {0}", ex.Message)
			Throw
		End Try

		FillMask(Color.Black)
		UpdateFFT(True)
	End Sub

	Public Overrides NotOverridable Property Text() As String
		Get
			Return MyBase.Text
		End Get
		Set(ByVal value As String)
			MyBase.Text = value
		End Set
	End Property

	#End Region

	#Region "Helper functions"

	Public Sub Invert(ByVal b As Bitmap)
		For x As Integer = 0 To b.Width - 1
			For y As Integer = 0 To b.Height - 1
				Dim myColor As Color = b.GetPixel(x, y)
				Dim myColor2 As Color = Color.FromArgb(myColor.A, 255 - myColor.R, 255 - myColor.G, 255 - myColor.B)
				b.SetPixel(x, y, myColor2)
			Next y
		Next x
	End Sub

	Private Sub FillMask(ByVal color As Color)
		Dim rect = New Rectangle(0, 0, _imgWidth, _imgHeight)
		_graphics.FillRectangle(New SolidBrush(color), rect)
	End Sub

	Private Sub Draw(ByVal e As MouseEventArgs)
		Dim color As Color
		If e.Button = MouseButtons.Left Then
			color = Color.White
		ElseIf e.Button = MouseButtons.Right Then
			color = Color.Black
		Else
			Return
		End If

		Dim pen As Pen
		If _lastX = e.X AndAlso _lastY = e.Y Then
			pen = New Pen(color, 1)
			Dim rect = New Rectangle(e.X - penSize.Value / 2, e.Y - penSize.Value / 2, penSize.Value, penSize.Value)

			If _penType = PenType.Circle Then
				_graphics.DrawEllipse(pen, rect)
				_graphics.FillEllipse(New SolidBrush(color), rect)
			Else
				_graphics.DrawRectangle(pen, rect)
				_graphics.FillRectangle(New SolidBrush(color), rect)
			End If
		Else
			pen = New Pen(color, penSize.Value)

			If _penType = PenType.Circle Then
				pen.EndCap = System.Drawing.Drawing2D.LineCap.Round
				pen.StartCap = System.Drawing.Drawing2D.LineCap.Round
			Else
				pen.EndCap = System.Drawing.Drawing2D.LineCap.Square
				pen.StartCap = System.Drawing.Drawing2D.LineCap.Square
			End If

			_graphics.DrawLine(pen, _lastX, _lastY, e.X, e.Y)
		End If
		pen.Dispose()
	End Sub

	Private Sub UpdateFFT(ByVal ifft As Boolean)
		Dim mask As NImage = Nothing
		Dim resultReal As NImage = Nothing
		Dim resultImaginary As NImage = Nothing
		Dim bmpimg As NImage = Nothing
		Dim tmp As NImage = Nothing

		Try
			If ifft Then
				bmpimg = NImage.FromBitmap(_maskBitmap)
				mask = ShiftFFT(NImage.FromImage(NPixelFormat.Grayscale8U, 0, bmpimg))

				resultReal = NImage.FromImage(_originalReal.PixelFormat, 0, _originalReal)
				resultImaginary = NImage.FromImage(_originalImaginary.PixelFormat, 0, _originalImaginary)

				Ngip.ApplyMaskToSpectrum(resultReal, resultImaginary, mask)

				Ngip.IFFT(resultReal, resultImaginary, tmp)
				If tmp IsNot Nothing Then
					_result = tmp.Crop((tmp.Width - _originalWidth) / 2, (tmp.Height - _originalHeight) / 2, _originalWidth, _originalHeight)
					viewResult.Image = _result.ToBitmap()
					viewResult.Invalidate()
				End If
			End If
			_gr.DrawImage(_fftBitmap, 0, 0, _imgWidth, _imgHeight)
			_gr.DrawImage(_maskBitmap, New Rectangle(0, 0, _imgWidth, _imgHeight), 0.0f, 0.0f, _imgWidth, _imgHeight, GraphicsUnit.Pixel, _imageAtt)
		Catch ex As Exception
			Utilities.ShowError("Error updating FFT image: {0}", ex.Message)
			Return
		Finally
			If tmp IsNot Nothing Then
				tmp.Dispose()
			End If

			If bmpimg IsNot Nothing Then
				bmpimg.Dispose()
			End If

			If resultImaginary IsNot Nothing Then
				resultImaginary.Dispose()
			End If

			If resultReal IsNot Nothing Then
				resultReal.Dispose()
			End If

			If mask IsNot Nothing Then
				mask.Dispose()
			End If
		End Try

		viewFourierMask.Refresh()
	End Sub

	Private Function ShiftFFT(ByVal inp As NImage) As NImage
		Dim shifted = NImage.FromImage(NPixelFormat.Grayscale8U, 0, inp)
		Dim inpCloned = CType(inp.Clone(), NImage)
		Dim cx As UInteger = inp.Width \ 2
		Dim cy As UInteger = inp.Height \ 2

		NImage.Copy(inpCloned, 0, 0, cx, cy, shifted, cx, cy)
		NImage.Copy(inpCloned, cx, cy, cx, cy, shifted, 0, 0)
		NImage.Copy(inpCloned, cx, 0, cx, cy, shifted, 0, cy)
		NImage.Copy(inpCloned, 0, cy, cx, cy, shifted, cx, 0)

		Return shifted
	End Function

	#End Region

	#Region "Mouse events"

	Private Sub viewFourier_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles viewFourierMask.MouseDown
		If e.Button = MouseButtons.Left OrElse e.Button = MouseButtons.Right Then
			_lastX = e.X
			_lastY = e.Y
			_allowPainting = True
		End If
	End Sub

	Private Sub viewFourier_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles viewFourierMask.MouseMove
		If (Not _allowPainting) Then
			Return
		End If
		If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then
			Return
		End If
		Draw(e)
		_lastX = e.X
		_lastY = e.Y
		UpdateFFT(False)
	End Sub

	Private Sub viewFourier_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles viewFourierMask.MouseUp
		If (Not _allowPainting) Then
			Return
		End If
		If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then
			Return
		End If
		Draw(e)
		_lastX = e.X
		_lastY = e.Y
		UpdateFFT(bRealtime.Checked)
		_allowPainting = False
	End Sub
	#End Region

	#Region "Button clicks"
	Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
		FillMask(Color.White)
		UpdateFFT(bRealtime.Checked)
	End Sub

	Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
		FillMask(Color.Black)
		UpdateFFT(bRealtime.Checked)
	End Sub

	Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
		Invert(_maskBitmap)
		UpdateFFT(bRealtime.Checked)
	End Sub

	Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
		UpdateFFT(True)
	End Sub

	Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
		Close()
	End Sub

	Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
		Close()
	End Sub

	Private Sub radioCircle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioCircle.CheckedChanged
		If radioCircle.Checked Then
			_penType = PenType.Circle
		End If
		radioRect.Checked = Not radioCircle.Checked
	End Sub

	Private Sub radioRect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioRect.CheckedChanged
		If radioRect.Checked Then
			_penType = PenType.Rectangle
		End If
		radioCircle.Checked = Not radioRect.Checked
	End Sub

	#End Region
End Class
