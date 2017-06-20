Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports Neurotec.Biometrics

Partial Public Class FingerSelector
	Inherits Control
	#Region "Public types"

	Public Class FingerClickArgs
		Inherits MouseEventArgs
		#Region "Public properties"

		Private privatePosition As NFPosition
		Public Property Position() As NFPosition
			Get
				Return privatePosition
			End Get
			Set(ByVal value As NFPosition)
				privatePosition = value
			End Set
		End Property
		Private privatePositionPart As NFPosition
		Public Property PositionPart() As NFPosition
			Get
				Return privatePositionPart
			End Get
			Set(ByVal value As NFPosition)
				privatePositionPart = value
			End Set
		End Property

		#End Region

		#Region "Public constructors"

		Public Sub New(ByVal pos As NFPosition, ByVal e As MouseEventArgs)
			Me.New(pos, NFPosition.Unknown, e)
		End Sub

		Public Sub New(ByVal pos As NFPosition, ByVal part As NFPosition, ByVal e As MouseEventArgs)
			MyBase.New(e.Button, e.Clicks, e.X, e.Y, e.Delta)
			Position = pos
			PositionPart = part
		End Sub

		#End Region
	End Class

	#End Region

	#Region "Protected types"

	Protected Enum ItemType
		None = 0
		Item
		ItemPart
		Fingernails
		PalmCreases
		Rotation
	End Enum

	Protected NotInheritable Class SvgPath
		#Region "Public constructor"

		Public Sub New()
			Scale = 1
			Position = NFPosition.Unknown
		End Sub

		#End Region

		#Region "Private fields"

		Private _path As GraphicsPath
		Private _region As Region

		#End Region

		#Region "Public properties"

		Private privateItemType As ItemType
		Public Property ItemType() As ItemType
			Get
				Return privateItemType
			End Get
			Set(ByVal value As ItemType)
				privateItemType = value
			End Set
		End Property
		Private privateId As String
		Public Property Id() As String
			Get
				Return privateId
			End Get
			Set(ByVal value As String)
				privateId = value
			End Set
		End Property
		Private privateFill As Boolean
		Public Property Fill() As Boolean
			Get
				Return privateFill
			End Get
			Set(ByVal value As Boolean)
				privateFill = value
			End Set
		End Property
		Private privateFillColor As Color
		Public Property FillColor() As Color
			Get
				Return privateFillColor
			End Get
			Set(ByVal value As Color)
				privateFillColor = value
			End Set
		End Property
		Private privatePosition As NFPosition
		Public Property Position() As NFPosition
			Get
				Return privatePosition
			End Get
			Set(ByVal value As NFPosition)
				privatePosition = value
			End Set
		End Property
		Private privateScale As Single
		Public Property Scale() As Single
			Get
				Return privateScale
			End Get
			Set(ByVal value As Single)
				privateScale = value
			End Set
		End Property
		Public Property Path() As GraphicsPath
			Get
				Return _path
			End Get
			Set(ByVal value As GraphicsPath)
				_path = value
				_region = New Region(_path)
			End Set
		End Property
		Private privateStrokeAlpha As Single
		Public Property StrokeAlpha() As Single
			Get
				Return privateStrokeAlpha
			End Get
			Set(ByVal value As Single)
				privateStrokeAlpha = value
			End Set
		End Property

		#End Region

		#Region "Public methods"

		Public Sub DrawElement(ByVal g As Graphics)
			Using p As New Pen(Color.FromArgb(CInt(Fix(StrokeAlpha * 255)), Color.Black))
				g.DrawPath(p, _path)
				If (Not Fill) Then
					Return
				End If
				Using b As Brush = New SolidBrush(FillColor)
					g.FillPath(b, _path)
				End Using
			End Using
		End Sub

		Public Function HitTest(ByVal point As Point) As Boolean
			If _region Is Nothing Then
				Return False
			End If
			Return _region.IsVisible(point.X / Scale, point.Y / Scale)
		End Function

		Public Overrides Function ToString() As String
			Return String.Format("Id={0}, Type={1}, Position={2}", Id, ItemType, Position)
		End Function

		#End Region
	End Class

	Protected Class SvgPainter
		#Region "Private fields"

		Private _width, _height As Integer
		Private ReadOnly _paths As List(Of SvgPath)

		#End Region

		#Region "Public constructor"

		Public Sub New(ByVal handsString As String)
			_paths = New List(Of SvgPath)()
			Elements = New List(Of SvgPath)()

			ParsePaths(handsString)

			For Each item As SvgPath In _paths.Where(Function(x) x.Position <> NFPosition.Unknown)
				If item.ItemType = ItemType.Rotation Then
					item.Fill = True
					item.FillColor = Color.GreenYellow
					item.StrokeAlpha = 1
				Else
					item.FillColor = Color.Transparent
				End If
			Next item
		End Sub

		#End Region

		#Region "Private methods"

		Private Shared Function ToAbsolute(ByVal absolute As PointF, ByVal reliative As PointF) As PointF
			Return PointF.Add(absolute, New SizeF(reliative.X, reliative.Y))
		End Function

		Private Function ParsePath(ByVal value As String) As GraphicsPath
			Dim gp As New GraphicsPath()
			Dim vals() As String = value.Split(New Char() { ","c, " "c }, StringSplitOptions.RemoveEmptyEntries)
			Dim pnts(3) As PointF
			Dim endPoint As New PointF()
			Dim j As Integer = 0
			Dim relative As Boolean = False
			Do While j < vals.Length
				If vals(j) = "m" OrElse vals(j) = "M" Then 'move
					gp.StartFigure()
					endPoint = New PointF(Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 2), System.Globalization.CultureInfo.InvariantCulture))
					j += 3
				ElseIf vals(j) = "l" OrElse vals(j) = "L" Then 'draw line
					relative = Char.IsLower(vals(j).Chars(0))
					Dim point As New PointF(Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 2), System.Globalization.CultureInfo.InvariantCulture))
					j += 3
					If relative Then
						point = ToAbsolute(endPoint, point)
					End If
					gp.AddLine(endPoint, point)
					endPoint = point
				ElseIf vals(j) = "z" OrElse vals(j) = "Z" Then 'end
					gp.CloseFigure()
					j += 1
				ElseIf vals(j) = "c" OrElse vals(j) = "C" Then 'curve
					relative = Char.IsLower(vals(j).Chars(0))
					j += 1

					Dim p1 As New PointF(Single.Parse(vals(j), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture))
					j += 2
					Dim p2 As New PointF(Single.Parse(vals(j), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture))
					j += 2
					Dim p3 As New PointF(Single.Parse(vals(j), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture))
					j += 2
					Dim p0 As PointF = endPoint
					If relative Then
						p1 = ToAbsolute(endPoint, p1)
						p2 = ToAbsolute(endPoint, p2)
						p3 = ToAbsolute(endPoint, p3)
					End If

					gp.AddBezier(p0, p1, p2, p3)
					endPoint = p3
				End If
			Loop
			Return gp
		End Function

		Private Sub ParsePaths(ByVal xmlString As String)
			Dim xml As XmlReader = New XmlTextReader(New StringReader(xmlString))
			Do While xml.Read()
				If xml.NodeType = XmlNodeType.Element Then
					If xml.Name = "svg" Then
						If (Not xml.MoveToAttribute("width")) Then
							Throw New Exception("width attribute not found")
						End If
						_width = Integer.Parse(xml.Value)
						If _width = 0 Then
							Throw New Exception("width attribute is invalid")
						End If
						If (Not xml.MoveToAttribute("height")) Then
							Throw New Exception("height attribute not found")
						End If
						_height = Integer.Parse(xml.Value)
						If _height = 0 Then
							Throw New Exception("height attribute is invalid")
						End If
					ElseIf xml.Name = "path" Then
						Dim count As Integer = xml.AttributeCount
						Dim shape As New SvgPath()

						_paths.Add(shape)
						For i As Integer = 0 To count - 1
							xml.MoveToAttribute(i)
							Select Case xml.Name
								Case "d"
										Dim id As String = xml.GetAttribute("id")
										If id IsNot Nothing AndAlso id.EndsWith("Rotate") Then
											id = 123.ToString()
										End If
										shape.Path = ParsePath(xml.Value)
										Exit Select
								Case "position"
										shape.Position = CType(System.Enum.Parse(GetType(NFPosition), xml.Value), NFPosition)
										shape.Fill = True
										If shape.Position = NFPosition.PlainLeftFourFingers OrElse shape.Position = NFPosition.PlainRightFourFingers OrElse shape.Position = NFPosition.PlainThumbs Then
											shape.FillColor = Color.Transparent
										End If
										Elements.Add(shape)
										Exit Select
								Case "id"
										shape.Id = xml.Value
										Exit Select
								Case "style"
										Dim vals() As String = xml.Value.Split(";"c)
										For Each t As String In vals
											Dim item As String = t
											If item.StartsWith("fill:") Then
												item = item.Replace("fill:", "")
												shape.FillColor = Color.Transparent
												If item <> "none" Then
													shape.FillColor = ColorTranslator.FromHtml(item)
												End If
												Continue For
											End If
											If item.StartsWith("stroke-opacity:") Then
												item = item.Replace("stroke-opacity:", "")
												shape.StrokeAlpha = Single.Parse(item, System.Globalization.CultureInfo.InvariantCulture)
												Continue For
											End If
										Next t
										Exit Select
								Case "group"
										shape.ItemType = CType(System.Enum.Parse(GetType(ItemType), xml.Value), ItemType)
										Exit Select
							End Select
						Next i
					End If
				End If
			Loop
			xml.Close()
		End Sub

		#End Region

		#Region "Public methods"

		Public Sub Paint(ByVal g As Graphics, ByVal clientSize As Size)
			g.SmoothingMode = SmoothingMode.HighQuality
			Dim m As Matrix = g.Transform

			Dim scale As Single = Math.Min(CSng(clientSize.Width) / _width, CSng(clientSize.Height) / _height)
			g.ScaleTransform(scale, scale)

			For Each item As SvgPath In _paths
				item.Scale = scale
				If item.ItemType <> ItemType.Rotation Then
					If item.ItemType = ItemType.PalmCreases Then
						If ShowPalmCreases Then
							item.DrawElement(g)
						End If
					ElseIf item.ItemType = ItemType.Fingernails Then
						If ShowFingerNails Then
							item.DrawElement(g)
						End If
					Else
						item.DrawElement(g)
					End If
				End If
			Next item

			g.Transform = m
		End Sub

		Public Sub PaintRotateForFinger(ByVal g As Graphics, ByVal clientSize As Size, ByVal position As NFPosition, ByVal angle As Single)
			Dim path As SvgPath = _paths.FirstOrDefault(Function(x) x.Position = position AndAlso x.ItemType = ItemType.Rotation)
			If path Is Nothing Then
				Return
			End If

			g.SmoothingMode = SmoothingMode.HighQuality
			Dim m As Matrix = g.Transform

			Dim scale As Single = Math.Min(CSng(clientSize.Width) / _width, CSng(clientSize.Height) / _height)
			m.Scale(scale, scale)

			Dim reg As New Region(path.Path)
			Dim bounds As RectangleF = reg.GetBounds(g)
			Dim rotateAt As New PointF(bounds.X + bounds.Width / 2.0F, bounds.Y + bounds.Height / 2.0F)
			m.RotateAt(angle, rotateAt)
			g.Transform = m

			path.DrawElement(g)
		End Sub

		Public Sub Clear()
			For Each item In Elements
				If item.ItemType = ItemType.ItemPart OrElse item.ItemType = ItemType.Item Then
					item.FillColor = Color.Transparent
				End If
			Next item
		End Sub

		#End Region

		#Region "Public properties"

		Private privateElements As List(Of SvgPath)
		Public Property Elements() As List(Of SvgPath)
			Get
				Return privateElements
			End Get
			Private Set(ByVal value As List(Of SvgPath))
				privateElements = value
			End Set
		End Property

		Private privateShowPalmCreases As Boolean
		Public Property ShowPalmCreases() As Boolean
			Get
				Return privateShowPalmCreases
			End Get
			Set(ByVal value As Boolean)
				privateShowPalmCreases = value
			End Set
		End Property
		Private privateShowFingerNails As Boolean
		Public Property ShowFingerNails() As Boolean
			Get
				Return privateShowFingerNails
			End Get
			Set(ByVal value As Boolean)
				privateShowFingerNails = value
			End Set
		End Property

		#End Region
	End Class

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True

		_painter = New SvgPainter(Encoding.UTF8.GetString(My.Resources.Hands))
		_missingPositions = New List(Of NFPosition)()
		_allowedPositions = New List(Of NFPosition)()
		For i As Integer = CInt(Fix(NFPosition.RightThumb)) To CInt(Fix(NFPosition.LeftLittle))
			_allowedPositions.Add(CType(i, NFPosition))
		Next i
	End Sub

	#End Region

	#Region "Protected fields"

	Protected ReadOnly _painter As SvgPainter
	Protected _selectedPosition As NFPosition = NFPosition.Unknown
	Protected ReadOnly _missingPositions As List(Of NFPosition)
	Protected ReadOnly _allowedPositions As List(Of NFPosition)
	Protected _mousePosition As New Point(0, 0)
	Protected _allowHighlight As Boolean = True
	Protected _isRolled As Boolean = False
	Protected _rollStarted As Boolean = False
	Protected _rollAngle As Single = 0
	Private _highlightColor As Color = Color.LightBlue
	Private _highligthPartColor As Color = Color.LightCyan
	Private _missingFingerColor As Color = Color.DarkRed
	Private _selectedFingerColor As Color = Color.Green

	#End Region

	#Region "Public properties"

	<Category("Appearance")> _
	Public Property ShowFingerNails() As Boolean
		Get
			Return _painter.ShowFingerNails
		End Get
		Set(ByVal value As Boolean)
			_painter.ShowFingerNails = value
		End Set
	End Property

	<Category("Appearance")> _
	Public Property ShowPalmCreases() As Boolean
		Get
			Return _painter.ShowPalmCreases
		End Get
		Set(ByVal value As Boolean)
			_painter.ShowPalmCreases = value
		End Set
	End Property

	<Browsable(False)> _
	Public Overridable Property SelectedPosition() As NFPosition
		Get
			Return _selectedPosition
		End Get
		Set(ByVal value As NFPosition)
			If _selectedPosition <> value Then
				_selectedPosition = value
				OnDataChanged()
			End If
		End Set
	End Property

	<Browsable(False)> _
	Public Overridable Property MissingPositions() As NFPosition()
		Get
			Return _missingPositions.ToArray()
		End Get
		Set(ByVal value As NFPosition())
			_missingPositions.Clear()
			If value IsNot Nothing Then
				_missingPositions.AddRange(value)
			End If
			_painter.Clear()
			OnDataChanged()
			Invalidate()
		End Set
	End Property

	<Browsable(False)> _
	Public Overridable Property AllowedPositions() As NFPosition()
		Get
			Return _allowedPositions.ToArray()
		End Get
		Set(ByVal value As NFPosition())
			If DesignMode Then
				Return
			End If
			_allowedPositions.Clear()
			If value IsNot Nothing Then
				_allowedPositions.AddRange(value.Where(Function(x) NBiometricTypes.IsPositionKnown(x) AndAlso (Not NBiometricTypes.IsPositionPalm(x))))
			End If
			_painter.Clear()
			OnDataChanged()
			Invalidate()
		End Set
	End Property

	<Category("Behavior")> _
	Public Property AllowHighlight() As Boolean
		Get
			Return _allowHighlight
		End Get
		Set(ByVal value As Boolean)
			If _allowHighlight <> value Then
				_allowHighlight = value
				OnDataChanged()
			End If
		End Set
	End Property

	<Browsable(False)> _
	Public Overridable Property IsRolled() As Boolean
		Get
			Return _isRolled
		End Get
		Set(ByVal value As Boolean)
			If _isRolled <> value Then
				_isRolled = value
				OnDataChanged()
			End If
		End Set
	End Property

	#End Region

	#Region "Public events"

	Public Event FingerClick As EventHandler(Of FingerClickArgs)

	#End Region

	#Region "Protected methods"

	Protected Overridable Function GetAvailableElements(ByVal getParts As Boolean) As IEnumerable(Of SvgPath)
		If DesignMode Then
			Return New SvgPath(){}
		End If

		Dim items = _painter.Elements.Where(Function(x) (x.ItemType = ItemType.Item OrElse x.ItemType = ItemType.ItemPart) AndAlso x.Position <> NFPosition.Unknown AndAlso _allowedPositions.Contains(x.Position))
		If getParts Then
			Dim parts = New List(Of SvgPath)()
			For Each item In items
				parts.AddRange(Me.GetParts(item))
			Next item
			Return Enumerable.Union(items, parts)
		End If
		Return items
	End Function

	Protected Overridable Function GetParts(ByVal item As SvgPath) As IEnumerable(Of SvgPath)
		If (Not NBiometricTypes.IsPositionSingleFinger(item.Position)) AndAlso NBiometricTypes.IsPositionFinger(item.Position) Then
			Dim availableParts = NBiometricTypes.GetPositionAvailableParts(item.Position, Nothing)
			Return _painter.Elements.Where(Function(x) x.ItemType = ItemType.ItemPart AndAlso availableParts.Contains(x.Position))
		End If
		Return New SvgPath(){}
	End Function

	Protected Overridable Function SelectHighlightedElement(ByVal elements As IEnumerable(Of SvgPath)) As SvgPath
		If _allowHighlight Then
			Return elements.Where(Function(x) x.ItemType = ItemType.Item AndAlso x.HitTest(_mousePosition)).OrderBy(Function(x) x.Position).FirstOrDefault()
		End If
		Return Nothing
	End Function

	Protected Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles timer.Tick
		If InvokeRequired Then
			BeginInvoke(New EventHandler(AddressOf TimerTick), sender, e)
		Else
			_rollAngle = (_rollAngle + 15) Mod 360
			Invalidate()
		End If
	End Sub

	Protected Overridable Sub OnDataChanged()
		If DesignMode Then
			Return
		End If

		Dim needsRepaint As Boolean = False
		Dim elements As IEnumerable(Of SvgPath) = GetAvailableElements(True)
		Dim first As SvgPath = SelectHighlightedElement(elements)
		Dim firstPart As SvgPath = Nothing
		If first IsNot Nothing Then
			firstPart = GetParts(first).Where(Function(x) x.HitTest(_mousePosition)).FirstOrDefault()
		End If

		For Each item As SvgPath In elements
			Dim c As Color = Color.Transparent
			If item Is first Then
				c = _highlightColor
			ElseIf item Is firstPart Then
				c = _highligthPartColor
			ElseIf _missingPositions.Contains(item.Position) Then
				c = _missingFingerColor
			ElseIf item.Position = _selectedPosition Then
				c = _selectedFingerColor
			End If
			If item.FillColor <> c Then
				item.FillColor = c
				needsRepaint = True
			End If
		Next item

		If IsRolled AndAlso NBiometricTypes.IsPositionSingleFinger(_selectedPosition) Then
			If (Not _rollStarted) Then
				_rollStarted = True
				timer.Start()
			End If
			needsRepaint = True
		ElseIf _rollStarted Then
			timer.Stop()
			_rollStarted = False
			needsRepaint = True
		End If

		If needsRepaint Then
			Invalidate()
		End If
	End Sub

	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		If _allowHighlight Then
			_mousePosition = e.Location

			OnDataChanged()
		End If

		MyBase.OnMouseMove(e)
	End Sub

	Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
		If DesignMode Then
			Return
		End If
		If FingerClickEvent IsNot Nothing AndAlso e.Button = MouseButtons.Left Then
			Dim onClick As EventHandler(Of FingerClickArgs) = FingerClickEvent

			Dim elements As IEnumerable(Of SvgPath) = GetAvailableElements(False)
			Dim first As SvgPath = SelectHighlightedElement(elements)
			Dim firstPart As SvgPath = Nothing
			If first IsNot Nothing Then
				firstPart = GetParts(first).Where(Function(x) x.HitTest(e.Location)).FirstOrDefault()

				Dim args As New FingerClickArgs(first.Position, e)
				If firstPart IsNot Nothing Then
					args.PositionPart = firstPart.Position
				End If
				onClick(Me, args)
			End If
		End If

		MyBase.OnMouseClick(e)
	End Sub

	Protected Overrides Sub OnResize(ByVal e As EventArgs)
		Invalidate()

		MyBase.OnResize(e)
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		If _painter IsNot Nothing Then
			_painter.Paint(e.Graphics, ClientSize)

			If (Not DesignMode) AndAlso _isRolled AndAlso NBiometricTypes.IsPositionSingleFinger(_selectedPosition) Then
				_painter.PaintRotateForFinger(e.Graphics, ClientSize, _selectedPosition, _rollAngle)
			End If
		End If

		MyBase.OnPaint(e)
	End Sub

	#End Region
End Class
