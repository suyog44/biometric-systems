Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports Neurotec.Biometrics

Namespace Controls
	Public NotInheritable Partial Class FingerSelector
		Inherits Panel
		#Region "Nested types"

		Public Class FingerClickArgs
			Inherits MouseEventArgs
			#Region "Private fields"

			Private ReadOnly _position As NFPosition

			#End Region

			#Region "Public properties"

			Public ReadOnly Property Position() As NFPosition
				Get
					Return _position
				End Get
			End Property

			#End Region

			#Region "Public constructor"

			Public Sub New(ByVal position As NFPosition, ByVal e As MouseEventArgs)
				MyBase.New(e.Button, e.Clicks, e.X, e.Y, e.Delta)
				_position = position
			End Sub

			#End Region
		End Class

		Private NotInheritable Class SvgPath
			#Region "Private fields"

			Private _path As GraphicsPath
			Private _strokeAlpha As Single
			Private _region As Region
			Private _fill As Boolean
			Private _fillColor As Color = Color.Transparent
			Private _position As NFPosition = CType(-1, NFPosition)
			Private _id As String
			Private _scale As Single = 1.0f

			#End Region

			#Region "Public properties"

			Public Property Id() As String
				Get
					Return _id
				End Get
				Set(ByVal value As String)
					_id = value
				End Set
			End Property

			Public Property Fill() As Boolean
				Get
					Return _fill
				End Get
				Set(ByVal value As Boolean)
					_fill = value
				End Set
			End Property

			Public Property FillColor() As Color
				Get
					Return _fillColor
				End Get
				Set(ByVal value As Color)
					_fillColor = value
				End Set
			End Property

			Public Property Position() As NFPosition
				Get
					Return _position
				End Get
				Set(ByVal value As NFPosition)
					_position = value
				End Set
			End Property

			Public Property Scale() As Single
				Get
					Return _scale
				End Get
				Set(ByVal value As Single)
					_scale = value
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

			Public Property StrokeAlpha() As Single
				Get
					Return _strokeAlpha
				End Get
				Set(ByVal value As Single)
					_strokeAlpha = value
				End Set
			End Property

			#End Region

			#Region "Public methods"

			Public Sub DrawElement(ByVal g As Graphics)
				Using p As New Pen(Color.FromArgb(CInt(Fix(_strokeAlpha * 255)), Color.Black))
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

			#End Region
		End Class

		Private Class SvgPainter
			#Region "Private fields"

			Private _width, _height As Integer
			Private ReadOnly _paths As List(Of SvgPath)
			Private ReadOnly _elements As Dictionary(Of NFPosition, SvgPath)

			#End Region

			#Region "Public constructor"

			Public Sub New(ByVal handsString As String)
				_paths = New List(Of SvgPath)()
				_elements = New Dictionary(Of NFPosition, SvgPath)()

				ParsePaths(handsString)

				For Each item As SvgPath In _paths
					If (Not item.Id.EndsWith("Rotate")) Then
						Continue For
					End If
					item.Fill = True
					item.FillColor = Color.GreenYellow
					item.StrokeAlpha = 1
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
				Dim j As Integer = 0, k As Integer = 0
				Dim relative As Boolean = False
				Do While j < vals.Length
					If vals(j) = "m" OrElse vals(j) = "M" Then 'move
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
					Else
						'still curve
						Dim point As New PointF(Single.Parse(vals(j), System.Globalization.CultureInfo.InvariantCulture), Single.Parse(vals(j + 1), System.Globalization.CultureInfo.InvariantCulture))
						If relative Then
							point = ToAbsolute(endPoint, point)
						End If
						Select Case k
							Case 0
								pnts(0) = endPoint
								pnts(1) = point
								k = 2
							Case 2
								pnts(k) = point
								k += 1
							Case 3
								pnts(3) = point
								endPoint = point
								k = 0
								gp.AddBezier(pnts(0), pnts(1), pnts(2), pnts(3))
						End Select
						j += 2
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
											_elements.Add(shape.Position, shape)
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
					If (Not item.Id.EndsWith("Rotate")) Then
						item.DrawElement(g)
					End If
				Next item

				g.Transform = m
			End Sub

			Public Sub PaintRotateForFinger(ByVal g As Graphics, ByVal clientSize As Size, ByVal position As NFPosition, ByVal angle As Single)
				If (Not _elements.ContainsKey(position)) Then
					Return
				End If

				Dim path As SvgPath = Nothing
				For Each item As SvgPath In _paths
					If item.Id = String.Format("{0}Rotate", position).Replace("Finger", String.Empty) Then
						path = item
						Exit For
					End If
				Next item

				If path Is Nothing Then
					Return
				End If

				g.SmoothingMode = SmoothingMode.HighQuality
				Dim m As Matrix = g.Transform

				Dim scale As Single = Math.Min(CSng(clientSize.Width) / _width, CSng(clientSize.Height) / _height)
				m.Scale(scale, scale)

				Dim reg As New Region(path.Path)
				Dim bounds As RectangleF = reg.GetBounds(g)
				Dim rotateAt As New PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2)
				m.RotateAt(angle, rotateAt)
				g.Transform = m

				path.DrawElement(g)
			End Sub

			#End Region

			#Region "Public properties"

			Public ReadOnly Property Elements() As Dictionary(Of NFPosition, SvgPath)
				Get
					Return _elements
				End Get
			End Property

			#End Region
		End Class

		#End Region

		#Region "Private fields"

		Private ReadOnly _painter As SvgPainter
		Private _selectedPosition As NFPosition = NFPosition.Unknown
		Private ReadOnly _missingPositions As New List(Of NFPosition)()
		Private _mousePosition As New Point(0, 0)
		Private _allowHighlight As Boolean = True
		Private _isRolled As Boolean
		Private _rollStarted As Boolean
		Private _rollAngle As Single

		#End Region

		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
			DoubleBuffered = True

			_painter = New SvgPainter(Encoding.UTF8.GetString(My.Resources.TwoHands))
		End Sub

		#End Region

		#Region "Public properties"

		<Browsable(False)> _
		Public Property IsRolled() As Boolean
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

		<Browsable(False)> _
		Public Property SelectedPosition() As NFPosition
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
		Public Property MissingPositions() As NFPosition()
			Get
				Return _missingPositions.ToArray()
			End Get
			Set(ByVal value As NFPosition())
				_missingPositions.Clear()
				If value IsNot Nothing Then
					For Each item As NFPosition In value
						If NBiometricTypes.IsPositionSingleFinger(item) Then
							_missingPositions.Add(item)
						End If
					Next item
				End If
				OnDataChanged()
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

		#End Region

		#Region "Public events"

		Public Event FingerClick As EventHandler(Of FingerClickArgs)

		#End Region

		#Region "Private methods"

		Private Sub OnDataChanged()
			Dim needsRepaint As Boolean = False
			For Each item As SvgPath In _painter.Elements.Values
				Dim color As Color = Color.Transparent
				If _allowHighlight AndAlso item.HitTest(_mousePosition) AndAlso NBiometricTypes.IsPositionSingleFinger(item.Position) Then
					color = Color.DarkRed
				ElseIf _missingPositions.Contains(item.Position) Then
					color = Color.Red
				ElseIf item.Position = _selectedPosition Then
					color = Color.Green
				End If
				If item.FillColor <> color Then
					item.FillColor = color
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

		#End Region

		#Region "Private form events"

		Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
			If _painter IsNot Nothing Then
				_painter.Paint(e.Graphics, ClientSize)

				If _isRolled AndAlso NBiometricTypes.IsPositionSingleFinger(_selectedPosition) Then
					_painter.PaintRotateForFinger(e.Graphics, ClientSize, _selectedPosition, _rollAngle)
				End If
			End If

			MyBase.OnPaint(e)
		End Sub

		Protected Overrides Sub OnResize(ByVal e As EventArgs)
			Invalidate()

			MyBase.OnResize(e)
		End Sub

		Private Sub FingerSelectorMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
			If _allowHighlight Then
				_mousePosition = e.Location

				OnDataChanged()
			End If
		End Sub

		Private Sub FingerSelectorMouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseClick
			If FingerClickEvent IsNot Nothing Then
				Dim click As EventHandler(Of FingerClickArgs) = FingerClickEvent
				Dim p As Point = e.Location

				For Each item As SvgPath In _painter.Elements.Values
					If NBiometricTypes.IsPositionSingleFinger(item.Position) AndAlso item.HitTest(p) Then
						click(Me, New FingerClickArgs(item.Position, e))
						Return
					End If
				Next item
			End If
		End Sub

		Private Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles timer.Tick
			If InvokeRequired Then
				BeginInvoke(New EventHandler(AddressOf TimerTick), sender, e)
			Else
				_rollAngle = (_rollAngle + 15) Mod 360
				Invalidate()
			End If
		End Sub

		#End Region
	End Class
End Namespace
