Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports System.Xml
Imports Neurotec.Images
Imports Neurotec.Samples.My

Friend NotInheritable Class FramePainter
	Inherits Control
#Region "Nested types"

	Private Enum MouseCapturing
		NotCapturing
		Moving
		Resizing
	End Enum

	Private Enum Position
		Left = 1
		Right = 2
		Top = 4
		Bottom = 8
		Center = 16
	End Enum

	Private Class FrameDefinition
#Region "Public types"

		Public Class Text
#Region "Public Constructor"

			Public Sub New(ByVal text As String, ByVal size As Integer, ByVal alignment As Position, ByVal drawCheckBox As Boolean, ByVal isBold As Boolean)
				Me.Value = text
				Me.Size = size
				Me.Alignment = alignment
				Me.DrawCheckBox = drawCheckBox
				Me.IsBold = isBold
			End Sub

#End Region

#Region "Public Properties"

			Private privateValue As String
			Public Property Value() As String
				Get
					Return privateValue
				End Get
				Private Set(ByVal value As String)
					privateValue = value
				End Set
			End Property
			Private privateSize As Integer
			Public Property Size() As Integer
				Get
					Return privateSize
				End Get
				Private Set(ByVal value As Integer)
					privateSize = value
				End Set
			End Property
			Private privateAlignment As Position
			Public Property Alignment() As Position
				Get
					Return privateAlignment
				End Get
				Private Set(ByVal value As Position)
					privateAlignment = value
				End Set
			End Property
			Private privateDrawCheckBox As Boolean
			Public Property DrawCheckBox() As Boolean
				Get
					Return privateDrawCheckBox
				End Get
				Private Set(ByVal value As Boolean)
					privateDrawCheckBox = value
				End Set
			End Property
			Private privateIsBold As Boolean
			Public Property IsBold() As Boolean
				Get
					Return privateIsBold
				End Get
				Private Set(ByVal value As Boolean)
					privateIsBold = value
				End Set
			End Property

#End Region
		End Class

		Public Class Block
#Region "Public Consnstructor"

			Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal isFingerBlock As Boolean)
				Me.X = x
				Me.Y = y
				Me.Width = width
				Me.Height = height
				Me.Cells = New List(Of Cell)()
				Me.Frames = New List(Of FrameInfo)()
				Me.IsFingerBlock = isFingerBlock
			End Sub

#End Region

#Region "Public properties"

			Private privateX As Integer
			Public Property X() As Integer
				Get
					Return privateX
				End Get
				Private Set(ByVal value As Integer)
					privateX = value
				End Set
			End Property
			Private privateY As Integer
			Public Property Y() As Integer
				Get
					Return privateY
				End Get
				Private Set(ByVal value As Integer)
					privateY = value
				End Set
			End Property
			Private privateWidth As Integer
			Public Property Width() As Integer
				Get
					Return privateWidth
				End Get
				Private Set(ByVal value As Integer)
					privateWidth = value
				End Set
			End Property
			Private privateHeight As Integer
			Public Property Height() As Integer
				Get
					Return privateHeight
				End Get
				Private Set(ByVal value As Integer)
					privateHeight = value
				End Set
			End Property
			Private privateIsFingerBlock As Boolean
			Public Property IsFingerBlock() As Boolean
				Get
					Return privateIsFingerBlock
				End Get
				Private Set(ByVal value As Boolean)
					privateIsFingerBlock = value
				End Set
			End Property
			Private privateCells As List(Of Cell)
			Public Property Cells() As List(Of Cell)
				Get
					Return privateCells
				End Get
				Private Set(ByVal value As List(Of Cell))
					privateCells = value
				End Set
			End Property
			Private privateFrames As List(Of FrameInfo)
			Public Property Frames() As List(Of FrameInfo)
				Get
					Return privateFrames
				End Get
				Private Set(ByVal value As List(Of FrameInfo))
					privateFrames = value
				End Set
			End Property

#End Region
		End Class

		Public Class Cell
#Region "Public Contructor"

			Public Sub New(ByVal width As Integer, ByVal height As Integer, ByVal drawRect As Boolean, ByVal fingerNumber As Integer)
				Me.Width = width
				Me.Height = height
				Me.DrawRect = drawRect
				Me.FingerNumber = fingerNumber
				Me.TextLines = New List(Of Text)()
			End Sub

#End Region

#Region "Public properties"

			Private privateWidth As Integer
			Public Property Width() As Integer
				Get
					Return privateWidth
				End Get
				Private Set(ByVal value As Integer)
					privateWidth = value
				End Set
			End Property
			Private privateHeight As Integer
			Public Property Height() As Integer
				Get
					Return privateHeight
				End Get
				Private Set(ByVal value As Integer)
					privateHeight = value
				End Set
			End Property
			Private privateDrawRect As Boolean
			Public Property DrawRect() As Boolean
				Get
					Return privateDrawRect
				End Get
				Private Set(ByVal value As Boolean)
					privateDrawRect = value
				End Set
			End Property
			Private privateFingerNumber As Integer
			Public Property FingerNumber() As Integer
				Get
					Return privateFingerNumber
				End Get
				Private Set(ByVal value As Integer)
					privateFingerNumber = value
				End Set
			End Property
			Private privateTextLines As List(Of Text)
			Public Property TextLines() As List(Of Text)
				Get
					Return privateTextLines
				End Get
				Private Set(ByVal value As List(Of Text))
					privateTextLines = value
				End Set
			End Property

#End Region
		End Class

		Public Class FrameInfo
#Region "Public Constructor"

			Public Sub New(ByVal size As Integer, ByVal positions As Position)
				Me.Size = size
				Me.Positions = positions
			End Sub

#End Region

#Region "Public properties"

			Private privateSize As Integer
			Public Property Size() As Integer
				Get
					Return privateSize
				End Get
				Private Set(ByVal value As Integer)
					privateSize = value
				End Set
			End Property
			Private privatePositions As Position
			Public Property Positions() As Position
				Get
					Return privatePositions
				End Get
				Private Set(ByVal value As Position)
					privatePositions = value
				End Set
			End Property

#End Region

		End Class

#End Region

#Region "Public constructor"

		Public Sub New()
			Me.Blocks = New List(Of Block)()
			Me.Color = Color.Red
		End Sub

#End Region

#Region "Public properties"

		Private privateBlocks As List(Of Block)
		Public Property Blocks() As List(Of Block)
			Get
				Return privateBlocks
			End Get
			Private Set(ByVal value As List(Of Block))
				privateBlocks = value
			End Set
		End Property
		Private privateColor As Color
		Public Property Color() As Color
			Get
				Return privateColor
			End Get
			Set(ByVal value As Color)
				privateColor = value
			End Set
		End Property

#End Region

	End Class

#End Region

#Region "Private fields"

	Private ReadOnly _frames As FrameDefinition

	Private _x, _y As Integer
	Private _width As Integer
	Private _height As Integer
	Private _aspect As Double

	Private _image As NImage
	Private _imageSmall As Image

	Private _imageRect As Rectangle
	Private _currScale As Double = 1

	Private _mouseDownX, _mouseDownY As Integer
	Private _mouseCapturing As MouseCapturing

#End Region

#Region "Public constructor"

	Public Sub New(ByVal frameDefinition As String)
		_frames = New FrameDefinition()

		Try
			ParseFrameDefinition(frameDefinition)
		Catch ex As Exception
			Throw New Exception("Error parsing frame settings." & Constants.vbLf & "Error message: " & ex.Message)
		End Try

		Dim block = _frames.Blocks.Where(Function(x) x.IsFingerBlock).FirstOrDefault()
		If block IsNot Nothing Then
			_aspect = CDbl(block.Width) / CDbl(block.Height)
		End If

		SetStyle(ControlStyles.DoubleBuffer, True)
		BackColor = Color.FromKnownColor(KnownColor.White)

		AddHandler MouseUp, AddressOf OnMouseUp
		AddHandler MouseMove, AddressOf OnMouseMove
		AddHandler MouseDown, AddressOf OnMouseDown
		AddHandler SizeChanged, AddressOf OnSizeChanged
	End Sub

#End Region

#Region "Protected methods"

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If _image IsNot Nothing Then
			_image.Dispose()
			_image = Nothing
		End If

		If _imageSmall IsNot Nothing Then
			_imageSmall.Dispose()
			_imageSmall = Nothing
		End If
	End Sub

	Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
		MyBase.OnPaint(pe)

		If _image Is Nothing Then
			Return
		End If

		DrawFrame(pe.Graphics)
	End Sub

	Protected Overrides Sub OnPaintBackground(ByVal pe As PaintEventArgs)
		Using brush As New SolidBrush(BackColor)
			pe.Graphics.FillRectangle(brush, 0, 0, Width, Height)
			DrawImage(pe.Graphics)
		End Using
	End Sub

#End Region

#Region "Public methods"

	Public Sub SetImage(ByVal img As NImage)
		If _image IsNot Nothing Then
			_image.Dispose()
			_image = Nothing
		End If

		If _imageSmall IsNot Nothing Then
			_imageSmall.Dispose()
			_imageSmall = Nothing
		End If

		_image = img
		OnSizeChanged(Nothing, Nothing)
		SetDefaultFramePosition()

		Refresh()
	End Sub

	Public Function GetFramedFingerprints() As Dictionary(Of Integer, NImage)
		Dim splitting As Dictionary(Of Integer, Rectangle) = GetFrameSplitting()
		Dim images As New Dictionary(Of Integer, NImage)()

		For Each key As Integer In splitting.Keys
			Dim rect As Rectangle = splitting(key)

			Dim img As NImage = _image.Crop(CUInt(rect.Left), CUInt(rect.Top), CUInt(rect.Width), CUInt(rect.Height))
			images.Add(key, img)
		Next key

		Return images
	End Function

	Public Function GetFrameSplitting() As Dictionary(Of Integer, Rectangle)
		Dim scale As Double = 1 / _currScale
		Dim x As Integer = CInt(Fix(Math.Round((_x - _imageRect.X) * scale)))
		Dim y As Integer = CInt(Fix(Math.Round((_y - _imageRect.Y) * scale)))
		Dim width As Integer = CInt(Fix(Math.Round(_width * scale)))
		Dim height As Integer = CInt(Fix(Math.Round(_height * scale)))

		Dim areaList = GetFingerRectangles(New Rectangle(x, y, width, height))

		Return areaList
	End Function

	Public Sub DrawForm(ByVal g As Graphics)
		Dim pen = New Pen(_frames.Color)
		For Each block In _frames.Blocks
			Dim currentWidth As Integer = 0, currentHeight As Integer = 0
			For Each cell In block.Cells
				Dim cellRect = New Rectangle(block.X + currentWidth, block.Y + currentHeight, cell.Width, cell.Height)
				If cell.DrawRect Then
					g.DrawRectangle(pen, cellRect)
				End If

				Dim brush = New SolidBrush(_frames.Color)
				Dim endPoint = New Point()
				Dim tmpAlignment As Position = 0
				For Each t In cell.TextLines.OrderBy(Function(x) x.Alignment)
					If tmpAlignment <> t.Alignment Then
						endPoint = New Point()
					End If
					Dim textStyle = If(t.IsBold, FontStyle.Bold, FontStyle.Regular)
					Dim testFont = New Font("Arial", t.Size, textStyle)

					Dim point = GetAlignmentPoint(g, cellRect, t, testFont, endPoint)
					g.DrawString(t.Value, testFont, brush, point)
					tmpAlignment = t.Alignment

					If t.DrawCheckBox Then
						DrawCheckBox(g, pen, cellRect, point, endPoint)
					End If
				Next t
				' do not increment height if line contains more than one cell
				Dim height = If(cell.Width + currentWidth = block.Width, cell.Height, 0)
				currentHeight = If(cell.Height + currentHeight <= block.Height, currentHeight + height, 0)
				currentWidth = If(cell.Width + currentWidth < block.Width, currentWidth + cell.Width, 0)
			Next cell
			DrawBlockFrames(g, block)
		Next block
	End Sub

#End Region

#Region "Private methods"

	Private Sub DrawCheckBox(ByVal g As Graphics, ByVal pen As Pen, ByVal rect As Rectangle, ByVal startPoint As Point, ByVal endPoint As Point)
		Const squareWidth As Integer = 16
		Dim textCenter As Integer = startPoint.X - rect.X + (endPoint.X - startPoint.X) / 2
		Dim x As Integer = rect.X + textCenter - squareWidth \ 2
		Dim y As Integer = rect.Y + 6
		g.DrawRectangle(pen, x, y, squareWidth, squareWidth)
	End Sub

	Private Sub DrawBlockFrames(ByVal g As Graphics, ByVal block As FrameDefinition.Block)
		For Each frame In block.Frames
			Dim pen = New Pen(_frames.Color, frame.Size)
			If (frame.Positions And Position.Bottom) <> 0 Then
				g.DrawLine(pen, block.X, block.Y + block.Height, block.X + block.Width, block.Y + block.Height)
			End If
			If (frame.Positions And Position.Top) <> 0 Then
				g.DrawLine(pen, block.X, block.Y, block.X + block.Width, block.Y)
			End If
			If (frame.Positions And Position.Left) <> 0 Then
				g.DrawLine(pen, block.X, block.Y, block.X, block.Y + block.Height)
			End If
			If (frame.Positions And Position.Right) <> 0 Then
				g.DrawLine(pen, block.X + block.Width, block.Y, block.X + block.Width, block.Y + block.Height)
			End If
		Next frame
	End Sub

	Private Function GetAlignmentPoint(ByVal g As Graphics, ByVal rect As Rectangle, ByVal text As FrameDefinition.Text, ByVal font As Font, ByRef endPoint As Point) As Point
		Dim size = g.MeasureString(text.Value, font)
		Dim stringWidth = CInt(Fix(size.Width))
		Dim stringHeight = CInt(Fix(size.Height))
		Dim point = New Point()

		Dim centerIncluded As Boolean = (text.Alignment And Position.Center) <> 0
		Dim topBottomIncluded As Boolean = (text.Alignment And (Position.Bottom Or Position.Top)) <> 0
		Dim leftRightIncluded As Boolean = (text.Alignment And (Position.Left Or Position.Right)) <> 0

		' vertical
		If (text.Alignment And Position.Top) <> 0 Then
			point.Y = If(endPoint.Y = 0 OrElse (Not centerIncluded), rect.Y + 2, endPoint.Y)
			endPoint.Y = point.Y + stringHeight
		ElseIf (text.Alignment And Position.Bottom) <> 0 Then
			endPoint.Y = If(endPoint.Y = 0, rect.Y + rect.Height - 4, endPoint.Y - 1)
			point.Y = endPoint.Y - stringHeight
		ElseIf centerIncluded Then
			point.Y = If(endPoint.Y = 0 OrElse leftRightIncluded, rect.Y + (rect.Height - stringHeight) / 2, endPoint.Y + 1)
			endPoint.Y = point.Y + stringHeight
		End If
		' horizontal
		If (text.Alignment And Position.Left) <> 0 Then
			point.X = If(endPoint.X = 0, rect.X + 4, endPoint.X + 1)
			endPoint.X = point.X + stringWidth
		ElseIf (text.Alignment And Position.Right) <> 0 Then
			endPoint.X = If(endPoint.X = 0, rect.X + Width - 2, endPoint.X - 1)
			point.X = endPoint.X - stringWidth
		ElseIf centerIncluded Then
			point.X = If(endPoint.X = 0 OrElse topBottomIncluded, rect.X + (rect.Width - stringWidth) / 2, endPoint.X + 1)
			endPoint.X = point.X + stringWidth
		End If

		Return point
	End Function

	Private Sub SetDefaultFramePosition()
		If _image Is Nothing Then
			Return
		End If

		_x = _imageRect.X
		_y = _imageRect.Y
		_width = _imageRect.Width
		_height = _imageRect.Height
		If CDbl(_width) / _height > _aspect Then
			_width = CInt(Fix(Math.Round(_height * _aspect)))
			_x += (_imageRect.Width - _width) / 2
		Else
			_height = CInt(Fix(Math.Round(_width / _aspect)))
			_y += (_imageRect.Height - _height) / 2
		End If
	End Sub

	Private Function InFrame(ByVal p As Point) As Boolean
		Return (p.X < _width + _x) AndAlso (p.Y < _height + _y) AndAlso (p.X > _x) AndAlso (p.Y > _y)
	End Function

	Private Function InSizeFrame(ByVal p As Point) As Boolean
		Return (p.X < _width + _x + 5) AndAlso (p.Y < _height + _y + 5) AndAlso (p.X > _width + _x - 5) AndAlso (p.Y > _height + _y - 5)
	End Function

	Private Overloads Sub OnMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
		If InSizeFrame(e.Location) OrElse _mouseCapturing = MouseCapturing.Resizing Then
			Cursor = Cursors.SizeNWSE
		Else
			If InFrame(e.Location) OrElse _mouseCapturing = MouseCapturing.Moving Then
				Cursor = Cursors.SizeAll
			Else
				Cursor = Cursors.Default
			End If
		End If

		Select Case _mouseCapturing
			Case MouseCapturing.Resizing
				If e.X - _mouseDownX > 250 Then
					_width = e.X - _mouseDownX
					_height = CInt(Fix(Math.Round(_width / _aspect)))
				End If
				Refresh()
			Case MouseCapturing.Moving
				_x = e.X - _mouseDownX
				_y = e.Y - _mouseDownY
				Refresh()
		End Select
	End Sub

	Private Overloads Sub OnMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If InSizeFrame(e.Location) Then
			_mouseDownX = e.X - _width
			_mouseDownY = e.Y - _height
			Capture = True
			_mouseCapturing = MouseCapturing.Resizing
		Else
			If InFrame(e.Location) Then
				_mouseDownX = e.X - _x
				_mouseDownY = e.Y - _y
				Capture = True
				_mouseCapturing = MouseCapturing.Moving
			End If
		End If
	End Sub

	Private Overloads Sub OnMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
		If _mouseCapturing = MouseCapturing.NotCapturing Then
			Return
		End If
		Capture = False
		_mouseCapturing = MouseCapturing.NotCapturing
	End Sub

	Private Sub ChangeScale(ByVal newScale As Double)
		_x = CInt(Fix(Math.Round(_x * newScale)))
		_y = CInt(Fix(Math.Round(_y * newScale)))
		_width = CInt(Fix(Math.Round(_width * newScale)))
		_height = CInt(Fix(Math.Round(_height * newScale)))
	End Sub

	Private Overloads Sub OnSizeChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (Width = 0) OrElse (Height = 0) OrElse (_image Is Nothing) Then
			If _imageSmall IsNot Nothing Then
				_imageSmall.Dispose()
			End If
			_imageSmall = Nothing
			Refresh()
			Return
		End If

		Dim w, h As Integer

		If _image.Height > _image.Width Then
			h = Height
			w = CInt(Fix((h * _image.Width) / _image.Height))
		Else
			w = Width
			h = CInt(Fix((w * _image.Height) / _image.Width))
		End If

		If _imageSmall IsNot Nothing Then
			_imageSmall.Dispose()
		End If
		_imageSmall = Nothing

		If w <= 0 OrElse h <= 0 Then
			Refresh()
			Return
		End If

		_x -= _imageRect.X
		_y -= _imageRect.Y

		_imageRect = New Rectangle(Width \ 2 - w \ 2, Height \ 2 - h \ 2, w, h)
		_imageSmall = New Bitmap(w, h)

		Using g As Graphics = Graphics.FromImage(_imageSmall)
			g.InterpolationMode = InterpolationMode.HighQualityBicubic
			Using bmp As Bitmap = _image.ToBitmap()
				g.DrawImage(bmp, 0, 0, w, h)
			End Using
		End Using

		ChangeScale(1 / _currScale)
		_currScale = CDbl(w) / _image.Width
		ChangeScale(_currScale)

		_x += _imageRect.X
		_y += _imageRect.Y

		Refresh()
	End Sub

	Private Sub DrawImage(ByVal g As Graphics)
		If _imageSmall IsNot Nothing Then
			g.DrawImage(_imageSmall, _imageRect)
		End If
	End Sub

	Private Sub ParseFrameDefinition(ByVal frameDefinition As String)
		Dim stringReader As New StringReader(frameDefinition)
		Dim reader As XmlReader = XmlReader.Create(stringReader)

		reader.Read()

		Dim c = reader.GetAttribute("color")
		If c IsNot Nothing Then
			_frames.Color = Color.FromName(c)
		End If

		Do While reader.Read()
			If (reader.NodeType = XmlNodeType.Element) AndAlso (reader.Name = "block") Then
				Dim x As Integer = If(reader.GetAttribute("x") IsNot Nothing, Integer.Parse(reader.GetAttribute("x")), 0)
				Dim y As Integer = If(reader.GetAttribute("y") IsNot Nothing, Integer.Parse(reader.GetAttribute("y")), 0)
				Dim height As Integer = If(reader.GetAttribute("height") IsNot Nothing, Integer.Parse(reader.GetAttribute("height")), 0)
				Dim width As Integer = If(reader.GetAttribute("width") IsNot Nothing, Integer.Parse(reader.GetAttribute("width")), 0)
				Dim isFingerBlock As Boolean = If(reader.GetAttribute("isFingerBlock") IsNot Nothing, Boolean.Parse(reader.GetAttribute("isFingerBlock")), False)

				Dim block = New FrameDefinition.Block(x, y, width, height, isFingerBlock)
				_frames.Blocks.Add(block)
				ReadCellInformation(reader, block)
			End If
		Loop

		reader.Close()
	End Sub

	Private Function ParseAlignment(ByVal alignment As String) As Position
		Dim values = alignment.Split(","c)
		Dim align As Position = 0
		For Each value In values
			align = align Or CType(System.Enum.Parse(GetType(Position), value, True), Position)
		Next value
		Return align
	End Function

	Private Sub ReadTextInformation(ByVal reader As XmlReader, ByVal cell As FrameDefinition.Cell)
		If reader.ReadToDescendant("text") Then
			Do
				If (reader.NodeType = XmlNodeType.Element) AndAlso (reader.Name = "text") Then
					Dim size As Integer = Integer.Parse(reader.GetAttribute("size"))
					Dim value As String = reader.GetAttribute("value").Replace("\n", Environment.NewLine)
					Dim alignment As Position = ParseAlignment(reader.GetAttribute("alignment"))
					Dim drawSquare As Boolean = If(reader.GetAttribute("square") IsNot Nothing, Boolean.Parse(reader.GetAttribute("square")), False)
					Dim isBold As Boolean = If(reader.GetAttribute("bold") IsNot Nothing, Boolean.Parse(reader.GetAttribute("bold")), False)
					cell.TextLines.Add(New FrameDefinition.Text(value, size, alignment, drawSquare, isBold))
				End If
			Loop While reader.Read() AndAlso (reader.NodeType <> XmlNodeType.EndElement)
		End If
	End Sub

	Private Sub ReadCellInformation(ByVal reader As XmlReader, ByVal block As FrameDefinition.Block)
		If reader.ReadToDescendant("cell") OrElse reader.ReadToDescendant("frame") Then
			Do
				If (reader.NodeType = XmlNodeType.Element) AndAlso (reader.Name = "cell") Then
					Dim h As Integer = If(reader.GetAttribute("height") IsNot Nothing, Integer.Parse(reader.GetAttribute("height")), block.Height)
					Dim w As Integer = If(reader.GetAttribute("width") IsNot Nothing, Integer.Parse(reader.GetAttribute("width")), block.Width)
					Dim drawRect As Boolean = If(reader.GetAttribute("drawRect") IsNot Nothing, Boolean.Parse(reader.GetAttribute("drawRect")), True)
					Dim fingerNo As Integer = If(reader.GetAttribute("fingerNo") IsNot Nothing, Integer.Parse(reader.GetAttribute("fingerNo")), -1)
					Dim cell = New FrameDefinition.Cell(w, h, drawRect, fingerNo)
					block.Cells.Add(cell)
					ReadTextInformation(reader, cell)
				End If
				If (reader.NodeType = XmlNodeType.Element) AndAlso (reader.Name = "frame") Then
					Dim size As Integer = Integer.Parse(reader.GetAttribute("size"))
					Dim alignment As Position = ParseAlignment(reader.GetAttribute("positions"))
					block.Frames.Add(New FrameDefinition.FrameInfo(size, alignment))
				End If
			Loop While reader.Read() AndAlso (reader.NodeType <> XmlNodeType.EndElement)
		End If
	End Sub

	Private Sub DrawFrame(ByVal g As Graphics)
		If _width <= 0 OrElse _height <= 0 Then
			Return
		End If

		Dim p = New Pen(_frames.Color, 2)

		g.TranslateTransform(_x, _y)
		g.SmoothingMode = SmoothingMode.AntiAlias

		g.DrawRectangle(p, 0, 0, _width, _height)
		g.FillRectangle(Brushes.White, _width - 3, _height - 3, 5, 5)
		g.DrawRectangle(p, _width - 3, _height - 3, 5, 5)

		Dim rect = GetFingerRectangles(New Rectangle(0, 0, _width, _height))
		For i = 1 To 14
			g.DrawRectangle(p, rect(i))
		Next i
	End Sub

	Private Function GetFingerRectangles(ByVal fingerFramePos As Rectangle) As Dictionary(Of Integer, Rectangle)
		Dim areaList = New Dictionary(Of Integer, Rectangle)()

		Dim block = _frames.Blocks.First(Function(b) b.IsFingerBlock)
		Dim widthRatio = If(fingerFramePos.Width <> 0, fingerFramePos.Width / CDbl(block.Width), 1)
		Dim heightRatio = If(fingerFramePos.Height <> 0, fingerFramePos.Height / CDbl(block.Height), 1)

		Dim currentWidth As Integer = 0, currentHeight As Integer = 0
		For Each cell In block.Cells
			Dim cellX As Integer = CInt(Fix((block.X + currentWidth) * widthRatio)) + fingerFramePos.X
			Dim cellY As Integer = CInt(Fix((block.Y + currentHeight) * heightRatio)) + fingerFramePos.Y
			Dim cellW As Integer = CInt(Fix(widthRatio * cell.Width))
			Dim cellH As Integer = CInt(Fix(heightRatio * cell.Height))
			If cellX < 0 Then
				cellX = 0
			End If
			If cellY < 0 Then
				cellY = 0
			End If
			If _image IsNot Nothing Then
				If cellX + cellW > _image.Width Then
					cellW = CInt(Fix(_image.Width)) - cellX
				End If
				If cellY + cellH > _image.Height Then
					cellH = CInt(Fix(_image.Height)) - cellY
				End If
			End If
			areaList.Add(cell.FingerNumber, New Rectangle(cellX, cellY, cellW, cellH))

			' do not increment height if line contains more than one cell
			Dim tmpHeight = If(cell.Width + currentWidth = block.Width, cell.Height, 0)
			currentHeight = If(cell.Height + currentHeight <= block.Height, currentHeight + tmpHeight, 0)
			currentWidth = If(cell.Width + currentWidth < block.Width, currentWidth + cell.Width, 0)
		Next cell

		Return areaList
	End Function

#End Region

End Class
