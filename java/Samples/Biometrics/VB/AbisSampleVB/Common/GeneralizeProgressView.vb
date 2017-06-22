Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Gui

Partial Public Class GeneralizeProgressView
	Inherits UserControl
	Implements INotifyPropertyChanged
#Region "Private types"

	Protected Class ItemStatus
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
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
		Private privateColor As Color
		Public Property Color() As Color
			Get
				Return privateColor
			End Get
			Set(ByVal value As Color)
				privateColor = value
			End Set
		End Property
		Private privateBiometric As NBiometric
		Public Property Biometric() As NBiometric
			Get
				Return privateBiometric
			End Get
			Set(ByVal value As NBiometric)
				privateBiometric = value
			End Set
		End Property
		Private privateHitBox As RectangleF
		Public Property HitBox() As RectangleF
			Get
				Return privateHitBox
			End Get
			Set(ByVal value As RectangleF)
				privateHitBox = value
			End Set
		End Property
		Private privateSelected As Boolean
		Public Property Selected() As Boolean
			Get
				Return privateSelected
			End Get
			Set(ByVal value As Boolean)
				privateSelected = value
			End Set
		End Property

		Public Function HitTest(ByVal p As Point) As Boolean
			If HitBox <> RectangleF.Empty Then
				Return HitBox.X <= p.X AndAlso p.X <= HitBox.X + HitBox.Width AndAlso HitBox.Y <= p.Y AndAlso p.Y <= HitBox.Y + HitBox.Height
			End If
			Return False
		End Function
	End Class

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Protected _view As NView = Nothing
	Protected _icaoView As IcaoWarningView = Nothing
	Protected _biometrics() As NBiometric = Nothing
	Protected _selected As NBiometric = Nothing
	Protected _generalized() As NBiometric = Nothing
	Protected _enableMouseSelection As Boolean = True
	Protected _drawings As New List(Of ItemStatus)()
	Private _redraw As Boolean = False

#End Region

#Region "Public properties"

	Public Property View() As NView
		Get
			Return _view
		End Get
		Set(ByVal value As NView)
			SetProperty(_view, value, "View")
		End Set
	End Property

	Public Property IcaoView() As IcaoWarningView
		Get
			Return _icaoView
		End Get
		Set(ByVal value As IcaoWarningView)
			SetProperty(_icaoView, value, "IcaoView")
		End Set
	End Property

	Public Property Selected() As NBiometric
		Get
			Return _selected
		End Get
		Set(ByVal value As NBiometric)
			Dim newValue As Boolean = _selected IsNot value
			_selected = value
			SetBiometricToView(_view, value)
			For Each dr As ItemStatus In _drawings
				dr.Selected = Object.Equals(dr.Biometric, value)
			Next dr
			panelPaint.Invalidate()
			If newValue Then
				OnPropertyChanged("Selected")
			End If
		End Set
	End Property

	Public Property Biometrics() As NBiometric()
		Get
			Return _biometrics
		End Get
		Set(ByVal value As NBiometric())
			If _biometrics IsNot value Then
				If _biometrics IsNot Nothing Then
					For Each item In _biometrics
						RemoveHandler item.PropertyChanged, AddressOf BiometricPropertyChanged
					Next item
				End If
				_biometrics = value
				If value IsNot Nothing Then
					For Each item In _biometrics
						AddHandler item.PropertyChanged, AddressOf BiometricPropertyChanged
					Next item
				End If
				OnDataChanged()
				OnPropertyChanged("Biometrics")
			End If
		End Set
	End Property

	Public Property Generalized() As NBiometric()
		Get
			Return _generalized
		End Get
		Set(ByVal value As NBiometric())
			If _generalized IsNot value Then
				If _generalized IsNot Nothing Then
					For Each item In _generalized
						RemoveHandler item.PropertyChanged, AddressOf BiometricPropertyChanged
					Next item
				End If
				_generalized = value
				If _generalized IsNot Nothing Then
					For Each item In _generalized
						AddHandler item.PropertyChanged, AddressOf BiometricPropertyChanged
					Next item
				End If
				OnDataChanged()
				OnPropertyChanged("Generalized")
			End If
		End Set
	End Property

	Public Property StatusText() As String
		Get
			Return lblStatus.Text
		End Get
		Set(ByVal value As String)
			If lblStatus.Text <> value Then
				lblStatus.Text = value
				OnPropertyChanged("StatusText")
			End If
		End Set
	End Property

	Public Property EnableMouseSelection() As Boolean
		Get
			Return _enableMouseSelection
		End Get
		Set(ByVal value As Boolean)
			_enableMouseSelection = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub OnDataChanged()
		Dim i As Integer

		_drawings.Clear()
		If _biometrics IsNot Nothing Then
			i = 1
			For Each item In _biometrics
				_drawings.Add(New ItemStatus With {.Text = i.ToString(), .Biometric = item, .Color = Color.Orange, .Fill = False})
				i += 1
			Next item
		End If
		If _generalized IsNot Nothing Then
			i = 0
			For Each item In _generalized
				_drawings.Add(New ItemStatus With {.Text = If(i = 0, "Generalized:", String.Empty), .Biometric = item, .Color = Color.Orange, .Fill = False})
				i += 1
			Next item
		End If

		UpdateBiometricsStatus()
		_redraw = True
	End Sub

	Private Sub BiometricPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "Status" Then
			BeginInvoke(New MethodInvoker(AddressOf UpdateBiometricsStatus))
		End If
	End Sub

	Private Sub UpdateBiometricsStatus()
		For Each item In _drawings
			Dim biometric = item.Biometric
			item.Color = Color.Orange
			item.Fill = False
			If biometric IsNot Nothing Then
				Select Case biometric.Status
					Case NBiometricStatus.Ok
						item.Color = Color.DarkGreen
						item.Fill = True
					Case NBiometricStatus.None
						item.Fill = False
						item.Color = Color.Orange
					Case Else
						item.Color = Color.Red
						item.Fill = True
						Exit Select
				End Select
			End If
		Next item
		panelPaint.Invalidate()
	End Sub

	Private Sub PanelPaintPaint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles panelPaint.Paint
		Const Margin As Integer = 2
		Dim sz As Size = panelPaint.Size
		sz.Width = sz.Width - Margin * 2
		sz.Height = sz.Height - Margin * 2

		Dim g As Graphics = e.Graphics
		g.SmoothingMode = SmoothingMode.AntiAlias

		If _drawings.Count > 0 Then
			Dim text As String = "Az"
			Dim defaultTextSize As SizeF = g.MeasureString(text, Font)
			Dim textSize As SizeF = defaultTextSize
			Dim bubleDiameter As Single = textSize.Height - Margin * 2
			Dim totalWidth As Single = 0
			For Each item In _drawings
				totalWidth += 2 * Margin + g.MeasureString(item.Text, Font).Width + Margin + bubleDiameter + 2 * Margin
			Next item
			Dim offsetX As Single = (sz.Width - totalWidth) / 2
			Dim offsetY As Single = (sz.Height - textSize.Height) / 2

			Dim m = g.Transform
			m.Translate(offsetX, offsetY)
			g.Transform = m

			Dim offset As Single = 2 * Margin
			For Each item In _drawings
				Dim hitBox As New RectangleF(offsetX + offset, offsetY, 0, 0)
				If item.Text <> String.Empty Then
					textSize = g.MeasureString(item.Text, Font)
					g.DrawString(item.Text, Font, Brushes.Black, offset, 0)
				Else
					textSize = New SizeF(0, defaultTextSize.Height)
				End If
				hitBox.Width = textSize.Width + Margin
				offset += hitBox.Width
				If item.Fill Then
					Using b = New SolidBrush(item.Color)
						g.FillEllipse(b, offset, Margin, bubleDiameter, bubleDiameter)
					End Using
				Else
					Using p = New Pen(item.Color)
						g.DrawEllipse(p, offset, Margin, bubleDiameter, bubleDiameter)
					End Using
				End If
				If item.Selected Then
					Using p = New Pen(Color.CadetBlue, 2)
						g.DrawEllipse(p, offset, Margin, bubleDiameter, bubleDiameter)
					End Using
				End If
				offset += bubleDiameter + 2 * Margin
				hitBox.Width += bubleDiameter
				hitBox.Height = textSize.Height
				item.HitBox = hitBox
			Next item
		End If

		MyBase.OnPaint(e)

		If _redraw Then
			_redraw = False
			ResizeForAutoSize()
		End If
	End Sub

	Private Sub PanelPaintMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelPaint.MouseMove
		Dim hit As Boolean = False
		If _enableMouseSelection Then
			For Each item In _drawings
				If item.HitTest(e.Location) Then
					hit = True
				End If
			Next item
			If hit Then
				Cursor = Cursors.Hand
			End If
		End If
		If (Not hit) Then
			Cursor = Cursors.Default
		End If
	End Sub

	Private Sub PanelPaintMouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelPaint.MouseClick
		If _enableMouseSelection Then
			For Each item In _drawings
				If item.HitTest(e.Location) Then
					Selected = item.Biometric
					Exit For
				End If
			Next item
		End If
	End Sub

	Private Sub ResizeForAutoSize()
		If AutoSize Then
			Me.SetBoundsCore(Left, Top, Width, Height, BoundsSpecified.Size)
		End If
	End Sub

#End Region

#Region "Protected methods"

	Protected Overridable Sub SetBiometricToView(ByVal view As NView, ByVal biometric As NBiometric)
		If view IsNot Nothing Then
			If TypeOf view Is NFaceView Then
				CType(view, NFaceView).Face = TryCast(biometric, NFace)
				If _icaoView IsNot Nothing Then
					_icaoView.Face = TryCast(biometric, NFace)
				End If
			ElseIf TypeOf view Is NFingerView Then
				CType(view, NFingerView).Finger = TryCast(biometric, NFrictionRidge)
			End If
		End If
	End Sub

	Protected Overrides Sub SetBoundsCore(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal specified As BoundsSpecified)
		If Me.AutoSize AndAlso (specified And BoundsSpecified.Size) <> 0 Then
			Dim size As Size = GetPreferredSize(New Size(width, height))

			width = size.Width
			height = size.Height
		End If

		MyBase.SetBoundsCore(x, y, width, height, specified)
	End Sub

	Protected Function SetProperty(Of T)(ByRef value As T, ByVal newValue As T, ByVal propertyName As String) As Boolean
		If (Not Object.Equals(value, newValue)) Then
			value = newValue
			OnPropertyChanged(propertyName)
			Return True
		End If
		Return False
	End Function

	Protected Sub OnPropertyChanged(ByVal propertyName As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
	End Sub

#End Region

#Region "Public methods"

	Public Sub Clear()
		Biometrics = Nothing
		Generalized = Nothing
		Selected = Nothing
		StatusText = String.Empty
		_drawings.Clear()
	End Sub

	Public Overrides Function GetPreferredSize(ByVal proposedSize As Size) As Size
		Dim sz As Size = lblStatus.Size
		If _drawings.Count > 0 Then
			sz.Height += CInt(Fix(_drawings.Max(Function(x) x.HitBox.Height))) + 8
			sz.Width = Math.Max(CInt(Fix(_drawings.Last().HitBox.Right)), sz.Width)
		Else
			sz.Width = Math.Max(50, sz.Width)
			sz.Height = 35
		End If
		Return sz
	End Function

#End Region

#Region "INotifyPropertyChanged Members"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region
End Class

Public Class DoubleBufferedPanel
	Inherits Panel
	Public Sub New()
		DoubleBuffered = True
	End Sub
End Class
