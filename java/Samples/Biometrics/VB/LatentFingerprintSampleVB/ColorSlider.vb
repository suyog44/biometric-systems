Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Encapsulates control that visualy displays certain integer value and allows user to change it within desired range. It imitates <see cref="System.Windows.Forms.TrackBar"/> as far as mouse usage is concerned.
''' </summary>
<ToolboxBitmap(GetType(TrackBar)), DefaultEvent("Scroll"), DefaultProperty("BarInnerColor")> _
Partial Public Class ColorSlider
	Inherits Control
#Region "Events"

	''' <summary>
	''' Fires when Slider position has changed
	''' </summary>
	<Description("Event fires when the Value property changes"), Category("Action")> _
	Public Event ValueChanged As EventHandler

	''' <summary>
	''' Fires when user scrolls the Slider
	''' </summary>
	<Description("Event fires when the Slider position is changed"), Category("Behavior")> _
	Public Event Scroll As ScrollEventHandler

#End Region

#Region "Properties"

	Private thumbRect_Renamed As Rectangle 'bounding rectangle of thumb area
	''' <summary>
	''' Gets the thumb rect. Usefull to determine bounding rectangle when creating custom thumb shape.
	''' </summary>
	''' <value>The thumb rect.</value>
	<Browsable(False)> _
	Public ReadOnly Property ThumbRect() As Rectangle
		Get
			Return thumbRect_Renamed
		End Get
	End Property

	Private barRect As Rectangle 'bounding rectangle of bar area
	Private barHalfRect As Rectangle
	Private thumbHalfRect As Rectangle
	Private elapsedRect As Rectangle 'bounding rectangle of elapsed area

	Private thumbSize_Renamed As Integer = 15
	''' <summary>
	''' Gets or sets the size of the thumb.
	''' </summary>
	''' <value>The size of the thumb.</value>
	''' <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is lower than zero or grather than half of appropiate dimension</exception>
	<Description("Set Slider thumb size"), Category("ColorSlider"), DefaultValue(15)> _
	Public Property ThumbSize() As Integer
		Get
			Return thumbSize_Renamed
		End Get
		Set(ByVal value As Integer)
			Dim orientationSize As Integer = ClientRectangle.Height
			If (barOrientation = Orientation.Horizontal) Then orientationSize = ClientRectangle.Width
			If value > 0 And value < orientationSize Then
				thumbSize_Renamed = value
			Else
				Throw New ArgumentOutOfRangeException("TrackSize has to be greather than zero and lower than half of Slider width")
			End If
			Invalidate()
		End Set
	End Property

	Private thumbCustomShape_Renamed As GraphicsPath = Nothing
	''' <summary>
	''' Gets or sets the thumb custom shape. Use ThumbRect property to determine bounding rectangle.
	''' </summary>
	''' <value>The thumb custom shape. null means default shape</value>
	<Description("Set Slider's thumb's custom shape"), Category("ColorSlider"), Browsable(False), DefaultValue(GetType(GraphicsPath), "null")> _
	Public Property ThumbCustomShape() As GraphicsPath
		Get
			Return thumbCustomShape_Renamed
		End Get
		Set(ByVal value As GraphicsPath)
			thumbCustomShape_Renamed = value
			Dim orientationSize As Single = value.GetBounds().Height
			If (barOrientation = Orientation.Horizontal) Then orientationSize = value.GetBounds().Width
			thumbSize_Renamed = CInt(orientationSize) + 1
			Invalidate()
		End Set
	End Property

	Private thumbRoundRectSize_Renamed As New Size(8, 8)
	''' <summary>
	''' Gets or sets the size of the thumb round rectangle edges.
	''' </summary>
	''' <value>The size of the thumb round rectangle edges.</value>
	<Description("Set Slider's thumb round rect size"), Category("ColorSlider"), DefaultValue(GetType(Size), "8; 8")> _
	Public Property ThumbRoundRectSize() As Size
		Get
			Return thumbRoundRectSize_Renamed
		End Get
		Set(ByVal value As Size)
			Dim h As Integer = value.Height, w As Integer = value.Width
			If h <= 0 Then
				h = 1
			End If
			If w <= 0 Then
				w = 1
			End If
			thumbRoundRectSize_Renamed = New Size(w, h)
			Invalidate()
		End Set
	End Property

	Private borderRoundRectSize_Renamed As New Size(8, 8)
	''' <summary>
	''' Gets or sets the size of the border round rect.
	''' </summary>
	''' <value>The size of the border round rect.</value>
	<Description("Set Slider's border round rect size"), Category("ColorSlider"), DefaultValue(GetType(Size), "8; 8")> _
	Public Property BorderRoundRectSize() As Size
		Get
			Return borderRoundRectSize_Renamed
		End Get
		Set(ByVal value As Size)
			Dim h As Integer = value.Height, w As Integer = value.Width
			If h <= 0 Then
				h = 1
			End If
			If w <= 0 Then
				w = 1
			End If
			borderRoundRectSize_Renamed = New Size(w, h)
			Invalidate()
		End Set
	End Property

	Private barOrientation As Orientation = Orientation.Horizontal
	''' <summary>
	''' Gets or sets the orientation of Slider.
	''' </summary>
	''' <value>The orientation.</value>
	<Description("Set Slider orientation"), Category("ColorSlider"), DefaultValue(Orientation.Horizontal)> _
	Public Property Orientation() As Orientation
		Get
			Return barOrientation
		End Get
		Set(ByVal value As Orientation)
			If barOrientation <> value Then
				barOrientation = value
				Dim temp As Integer = Width
				Width = Height
				Height = temp
				If thumbCustomShape_Renamed IsNot Nothing Then
					Dim orientationSize As Single = thumbCustomShape_Renamed.GetBounds().Height
					If barOrientation = Orientation.Horizontal Then orientationSize = thumbCustomShape_Renamed.GetBounds().Width
					thumbSize_Renamed = CInt(barOrientation) + 1
				End If
				Invalidate()
			End If
		End Set
	End Property

	Private trackerValue As Integer = 50
	''' <summary>
	''' Gets or sets the value of Slider.
	''' </summary>
	''' <value>The value.</value>
	''' <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
	<Description("Set Slider value"), Category("ColorSlider"), DefaultValue(50)> _
	Public Property Value() As Integer
		Get
			Return trackerValue
		End Get
		Set(ByVal value As Integer)
			If value >= barMinimum And value <= barMaximum Then
				If trackerValue <> value Then
					trackerValue = value
					RaiseEvent ValueChanged(Me, New EventArgs())
					Refresh()
				End If
			Else
				Throw New ArgumentOutOfRangeException("Value is outside appropriate range (min, max)")
			End If
		End Set
	End Property

	Private barMinimum As Integer = 0
	''' <summary>
	''' Gets or sets the minimum value.
	''' </summary>
	''' <value>The minimum value.</value>
	''' <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when minimal value is greather than maximal one</exception>
	<Description("Set Slider minimal point"), Category("ColorSlider"), DefaultValue(0)> _
	Public Property Minimum() As Integer
		Get
			Return barMinimum
		End Get
		Set(ByVal value As Integer)
			If value < barMaximum Then
				barMinimum = value
				If trackerValue < barMinimum Then
					trackerValue = barMinimum
					RaiseEvent ValueChanged(Me, New EventArgs())
				End If
				Invalidate()
			Else
				Throw New ArgumentOutOfRangeException("Minimal value is greather than maximal one")
			End If
		End Set
	End Property

	Private barMaximum As Integer = 100
	''' <summary>
	''' Gets or sets the maximum value.
	''' </summary>
	''' <value>The maximum value.</value>
	''' <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when maximal value is lower than minimal one</exception>
	<Description("Set Slider maximal point"), Category("ColorSlider"), DefaultValue(100)> _
	Public Property Maximum() As Integer
		Get
			Return barMaximum
		End Get
		Set(ByVal value As Integer)
			If value > barMinimum Then
				barMaximum = value
				If trackerValue > barMaximum Then
					trackerValue = barMaximum
					RaiseEvent ValueChanged(Me, New EventArgs())
				End If
				Invalidate()
			Else
				Throw New ArgumentOutOfRangeException("Maximal value is lower than minimal one")
			End If
		End Set
	End Property

	Private smallChange_Renamed As UInteger = 1
	''' <summary>
	''' Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed
	''' </summary>
	''' <value>The small change value.</value>
	<Description("Set trackbar's small change"), Category("ColorSlider"), DefaultValue(1)> _
	Public Property SmallChange() As UInteger
		Get
			Return smallChange_Renamed
		End Get
		Set(ByVal value As UInteger)
			smallChange_Renamed = value
		End Set
	End Property

	Private largeChange_Renamed As UInteger = 5

	''' <summary>
	''' Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed
	''' </summary>
	''' <value>The large change value.</value>
	<Description("Set trackbar's large change"), Category("ColorSlider"), DefaultValue(5)> _
	Public Property LargeChange() As UInteger
		Get
			Return largeChange_Renamed
		End Get
		Set(ByVal value As UInteger)
			largeChange_Renamed = value
		End Set
	End Property

	Private drawFocusRectangle_Renamed As Boolean = True
	''' <summary>
	''' Gets or sets a value indicating whether to draw focus rectangle.
	''' </summary>
	''' <value><c>true</c> if focus rectangle should be drawn; otherwise, <c>false</c>.</value>
	<Description("Set whether to draw focus rectangle"), Category("ColorSlider"), DefaultValue(True)> _
	Public Property DrawFocusRectangle() As Boolean
		Get
			Return drawFocusRectangle_Renamed
		End Get
		Set(ByVal value As Boolean)
			drawFocusRectangle_Renamed = value
			Invalidate()
		End Set
	End Property

	Private drawSemitransparentThumb_Renamed As Boolean = True
	''' <summary>
	''' Gets or sets a value indicating whether to draw semitransparent thumb.
	''' </summary>
	''' <value><c>true</c> if semitransparent thumb should be drawn; otherwise, <c>false</c>.</value>
	<Description("Set whether to draw semitransparent thumb"), Category("ColorSlider"), DefaultValue(True)> _
	Public Property DrawSemitransparentThumb() As Boolean
		Get
			Return drawSemitransparentThumb_Renamed
		End Get
		Set(ByVal value As Boolean)
			drawSemitransparentThumb_Renamed = value
			Invalidate()
		End Set
	End Property

	Private mouseEffects_Renamed As Boolean = True
	''' <summary>
	''' Gets or sets whether mouse entry and exit actions have impact on how control look.
	''' </summary>
	''' <value><c>true</c> if mouse entry and exit actions have impact on how control look; otherwise, <c>false</c>.</value>
	<Description("Set whether mouse entry and exit actions have impact on how control look"), Category("ColorSlider"), DefaultValue(True)> _
	Public Property MouseEffects() As Boolean
		Get
			Return mouseEffects_Renamed
		End Get
		Set(ByVal value As Boolean)
			mouseEffects_Renamed = value
			Invalidate()
		End Set
	End Property

	Private mouseWheelBarPartitions_Renamed As Integer = 10
	''' <summary>
	''' Gets or sets the mouse wheel bar partitions.
	''' </summary>
	''' <value>The mouse wheel bar partitions.</value>
	''' <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value isn't greather than zero</exception>
	<Description("Set to how many parts is bar divided when using mouse wheel"), Category("ColorSlider"), DefaultValue(10)> _
	Public Property MouseWheelBarPartitions() As Integer
		Get
			Return mouseWheelBarPartitions_Renamed
		End Get
		Set(ByVal value As Integer)
			If value > 0 Then
				mouseWheelBarPartitions_Renamed = value
			Else
				Throw New ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero")
			End If
		End Set
	End Property

	Private thumbImage_Renamed As Image = Nothing
	''' <summary>
	''' Gets or sets the Image use to render the thumb.
	''' </summary>
	''' <value>the thumb Image</value>  
	Public Property ThumbImage() As Image
		Get
			Return thumbImage_Renamed
		End Get
		Set(ByVal value As Image)
			If value IsNot Nothing Then
				thumbImage_Renamed = value
			End If
		End Set
	End Property

	Private thumbOuterColor_Renamed As Color = Color.White
	''' <summary>
	''' Gets or sets the thumb outer color .
	''' </summary>
	''' <value>The thumb outer color.</value>
	<Description("Set Slider thumb outer color"), Category("ColorSlider"), DefaultValue(GetType(Color), "White")> _
	Public Property ThumbOuterColor() As Color
		Get
			Return thumbOuterColor_Renamed
		End Get
		Set(ByVal value As Color)
			thumbOuterColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private thumbInnerColor_Renamed As Color = Color.Gainsboro
	''' <summary>
	''' Gets or sets the inner color of the thumb.
	''' </summary>
	''' <value>The inner color of the thumb.</value>
	<Description("Set Slider thumb inner color"), Category("ColorSlider"), DefaultValue(GetType(Color), "Gainsboro")> _
	Public Property ThumbInnerColor() As Color
		Get
			Return thumbInnerColor_Renamed
		End Get
		Set(ByVal value As Color)
			thumbInnerColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private thumbPenColor_Renamed As Color = Color.Silver
	''' <summary>
	''' Gets or sets the color of the thumb pen.
	''' </summary>
	''' <value>The color of the thumb pen.</value>
	<Description("Set Slider thumb pen color"), Category("ColorSlider"), DefaultValue(GetType(Color), "Silver")> _
	Public Property ThumbPenColor() As Color
		Get
			Return thumbPenColor_Renamed
		End Get
		Set(ByVal value As Color)
			thumbPenColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private barOuterColor_Renamed As Color = Color.SkyBlue
	''' <summary>
	''' Gets or sets the outer color of the bar.
	''' </summary>
	''' <value>The outer color of the bar.</value>
	<Description("Set Slider bar outer color"), Category("ColorSlider"), DefaultValue(GetType(Color), "SkyBlue")> _
	Public Property BarOuterColor() As Color
		Get
			Return barOuterColor_Renamed
		End Get
		Set(ByVal value As Color)
			barOuterColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private barInnerColor_Renamed As Color = Color.DarkSlateBlue
	''' <summary>
	''' Gets or sets the inner color of the bar.
	''' </summary>
	''' <value>The inner color of the bar.</value>
	<Description("Set Slider bar inner color"), Category("ColorSlider"), DefaultValue(GetType(Color), "DarkSlateBlue")> _
	Public Property BarInnerColor() As Color
		Get
			Return barInnerColor_Renamed
		End Get
		Set(ByVal value As Color)
			barInnerColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private barPenColor_Renamed As Color = Color.Gainsboro
	''' <summary>
	''' Gets or sets the color of the bar pen.
	''' </summary>
	''' <value>The color of the bar pen.</value>
	<Description("Set Slider bar pen color"), Category("ColorSlider"), DefaultValue(GetType(Color), "Gainsboro")> _
	Public Property BarPenColor() As Color
		Get
			Return barPenColor_Renamed
		End Get
		Set(ByVal value As Color)
			barPenColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private elapsedOuterColor_Renamed As Color = Color.DarkGreen
	''' <summary>
	''' Gets or sets the outer color of the elapsed.
	''' </summary>
	''' <value>The outer color of the elapsed.</value>
	<Description("Set Slider's elapsed part outer color"), Category("ColorSlider"), DefaultValue(GetType(Color), "DarkGreen")> _
	Public Property ElapsedOuterColor() As Color
		Get
			Return elapsedOuterColor_Renamed
		End Get
		Set(ByVal value As Color)
			elapsedOuterColor_Renamed = value
			Invalidate()
		End Set
	End Property

	Private elapsedInnerColor_Renamed As Color = Color.Chartreuse
	''' <summary>
	''' Gets or sets the inner color of the elapsed.
	''' </summary>
	''' <value>The inner color of the elapsed.</value>
	<Description("Set Slider's elapsed part inner color"), Category("ColorSlider"), DefaultValue(GetType(Color), "Chartreuse")> _
	Public Property ElapsedInnerColor() As Color
		Get
			Return elapsedInnerColor_Renamed
		End Get
		Set(ByVal value As Color)
			elapsedInnerColor_Renamed = value
			Invalidate()
		End Set
	End Property

#End Region

#Region "Color schemas"

	'define own color schemas
	Private aColorSchema(,) As Color = {{Color.White, Color.Gainsboro, Color.Silver, Color.SkyBlue, Color.DarkSlateBlue, Color.Gainsboro, Color.DarkGreen, Color.Chartreuse}, {Color.White, Color.Gainsboro, Color.Silver, Color.Red, Color.DarkRed, Color.Gainsboro, Color.Coral, Color.LightCoral}, {Color.White, Color.Gainsboro, Color.Silver, Color.GreenYellow, Color.Yellow, Color.Gold, Color.Orange, Color.OrangeRed}, {Color.White, Color.Gainsboro, Color.Silver, Color.Red, Color.Crimson, Color.Gainsboro, Color.DarkViolet, Color.Violet}}

	Public Enum ColorSchemas
		PerlBlueGreen
		PerlRedCoral
		PerlGold
		PerlRoyalColors
	End Enum

	Private colorSchema_Renamed As ColorSchemas = ColorSchemas.PerlBlueGreen
	''' <summary>
	''' Sets color schema. Color generalization / fast color changing. Has no effect when slider colors are changed manually after schema was applied.
	''' </summary>
	''' <value>New color schema value</value>
	<Description("Set Slider color schema. Has no effect when slider colors are changed manually after schema was applied."), Category("ColorSlider"), DefaultValue(GetType(ColorSchemas), "PerlBlueGreen")> _
	Public Property ColorSchema() As ColorSchemas
		Get
			Return colorSchema_Renamed
		End Get
		Set(ByVal value As ColorSchemas)
			colorSchema_Renamed = value
			Dim sn As Byte = CByte(value)
			thumbOuterColor_Renamed = aColorSchema(sn, 0)
			thumbInnerColor_Renamed = aColorSchema(sn, 1)
			thumbPenColor_Renamed = aColorSchema(sn, 2)
			barOuterColor_Renamed = aColorSchema(sn, 3)
			barInnerColor_Renamed = aColorSchema(sn, 4)
			barPenColor_Renamed = aColorSchema(sn, 5)
			elapsedOuterColor_Renamed = aColorSchema(sn, 6)
			elapsedInnerColor_Renamed = aColorSchema(sn, 7)

			Invalidate()
		End Set
	End Property

#End Region

#Region "Constructors"

	''' <summary>
	''' Initializes a new instance of the <see cref="ColorSlider"/> class.
	''' </summary>
	''' <param name="min">The minimum value.</param>
	''' <param name="max">The maximum value.</param>
	''' <param name="value">The current value.</param>
	Public Sub New(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer)
		InitializeComponent()
		SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.Selectable Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserMouse Or ControlStyles.UserPaint, True)
		BackColor = Color.Transparent

		Minimum = min
		Maximum = max
		Me.Value = value
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="ColorSlider"/> class.
	''' </summary>
	Public Sub New()
		Me.New(0, 100, 50)
	End Sub

#End Region

#Region "Paint"

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		If (Not Enabled) Then
			Dim desaturatedColors() As Color = DesaturateColors(thumbOuterColor_Renamed, thumbInnerColor_Renamed, thumbPenColor_Renamed, barOuterColor_Renamed, barInnerColor_Renamed, barPenColor_Renamed, elapsedOuterColor_Renamed, elapsedInnerColor_Renamed)
			DrawColorSlider(e, desaturatedColors(0), desaturatedColors(1), desaturatedColors(2), desaturatedColors(3), desaturatedColors(4), desaturatedColors(5), desaturatedColors(6), desaturatedColors(7))
		Else
			If mouseEffects_Renamed AndAlso mouseInRegion Then
				Dim lightenedColors() As Color = LightenColors(thumbOuterColor_Renamed, thumbInnerColor_Renamed, thumbPenColor_Renamed, barOuterColor_Renamed, barInnerColor_Renamed, barPenColor_Renamed, elapsedOuterColor_Renamed, elapsedInnerColor_Renamed)
				DrawColorSlider(e, lightenedColors(0), lightenedColors(1), lightenedColors(2), lightenedColors(3), lightenedColors(4), lightenedColors(5), lightenedColors(6), lightenedColors(7))
			Else
				DrawColorSlider(e, thumbOuterColor_Renamed, thumbInnerColor_Renamed, thumbPenColor_Renamed, barOuterColor_Renamed, barInnerColor_Renamed, barPenColor_Renamed, elapsedOuterColor_Renamed, elapsedInnerColor_Renamed)
			End If
		End If
	End Sub

	''' <summary>
	''' Draws the colorslider control using passed colors.
	''' </summary>
	''' <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
	''' <param name="thumbOuterColorPaint">The thumb outer color paint.</param>
	''' <param name="thumbInnerColorPaint">The thumb inner color paint.</param>
	''' <param name="thumbPenColorPaint">The thumb pen color paint.</param>
	''' <param name="barOuterColorPaint">The bar outer color paint.</param>
	''' <param name="barInnerColorPaint">The bar inner color paint.</param>
	''' <param name="barPenColorPaint">The bar pen color paint.</param>
	''' <param name="elapsedOuterColorPaint">The elapsed outer color paint.</param>
	''' <param name="elapsedInnerColorPaint">The elapsed inner color paint.</param>
	Private Sub DrawColorSlider(ByVal e As PaintEventArgs, ByVal thumbOuterColorPaint As Color, ByVal thumbInnerColorPaint As Color, ByVal thumbPenColorPaint As Color, ByVal barOuterColorPaint As Color, ByVal barInnerColorPaint As Color, ByVal barPenColorPaint As Color, ByVal elapsedOuterColorPaint As Color, ByVal elapsedInnerColorPaint As Color)
		Try
			'set up thumbRect aproprietly
			If barOrientation = Orientation.Horizontal Then
				Dim TrackX As Integer = CInt(((trackerValue - barMinimum) * (ClientRectangle.Width - thumbSize_Renamed)) / (barMaximum - barMinimum))
				thumbRect_Renamed = New Rectangle(TrackX, 1, thumbSize_Renamed - 1, ClientRectangle.Height - 3)
			Else
				Dim TrackY As Integer = CInt(((trackerValue - barMinimum) * (ClientRectangle.Height - thumbSize_Renamed)) / (barMaximum - barMinimum))
				thumbRect_Renamed = New Rectangle(1, TrackY, ClientRectangle.Width - 3, thumbSize_Renamed - 1)
			End If

			'adjust drawing rects
			barRect = ClientRectangle
			thumbHalfRect = thumbRect_Renamed
			Dim gradientOrientation As LinearGradientMode
			If barOrientation = Orientation.Horizontal Then
				barRect.Inflate(-1, -barRect.Height \ 3)
				barHalfRect = barRect
				barHalfRect.Height /= 2
				gradientOrientation = LinearGradientMode.Vertical
				thumbHalfRect.Height /= 2
				elapsedRect = barRect
				elapsedRect.Width = CInt(thumbRect_Renamed.Left + thumbSize_Renamed / 2)
			Else
				barRect.Inflate(-barRect.Width \ 3, -1)
				barHalfRect = barRect
				barHalfRect.Width /= 2
				gradientOrientation = LinearGradientMode.Horizontal
				thumbHalfRect.Width /= 2
				elapsedRect = barRect
				elapsedRect.Height = CInt(thumbRect_Renamed.Top + thumbSize_Renamed / 2)
			End If
			'get thumb shape path
			Dim thumbPath As GraphicsPath
			If thumbCustomShape_Renamed Is Nothing Then
				thumbPath = CreateRoundRectPath(thumbRect_Renamed, thumbRoundRectSize_Renamed)
			Else
				thumbPath = thumbCustomShape_Renamed
				Dim m As New Matrix()
				m.Translate(thumbRect_Renamed.Left - thumbPath.GetBounds().Left, thumbRect_Renamed.Top - thumbPath.GetBounds().Top)
				thumbPath.Transform(m)
			End If

			'draw bar
			Using lgbBar As New LinearGradientBrush(barHalfRect, barOuterColorPaint, barInnerColorPaint, gradientOrientation)
				lgbBar.WrapMode = WrapMode.TileFlipXY
				e.Graphics.FillRectangle(lgbBar, barRect)
				'draw elapsed bar
				Using lgbElapsed As New LinearGradientBrush(barHalfRect, elapsedOuterColorPaint, elapsedInnerColorPaint, gradientOrientation)
					lgbElapsed.WrapMode = WrapMode.TileFlipXY
					If Capture AndAlso drawSemitransparentThumb_Renamed Then
						Dim elapsedReg As New Region(elapsedRect)
						elapsedReg.Exclude(thumbPath)
						e.Graphics.FillRegion(lgbElapsed, elapsedReg)
					Else
						e.Graphics.FillRectangle(lgbElapsed, elapsedRect)
					End If
				End Using
				'draw bar band                    
				Using barPen As New Pen(barPenColorPaint, 0.5F)
					e.Graphics.DrawRectangle(barPen, barRect)
				End Using
			End Using

			'draw thumb
			Dim newthumbOuterColorPaint As Color = thumbOuterColorPaint, newthumbInnerColorPaint As Color = thumbInnerColorPaint
			If Capture AndAlso drawSemitransparentThumb_Renamed Then
				newthumbOuterColorPaint = Color.FromArgb(175, thumbOuterColorPaint)
				newthumbInnerColorPaint = Color.FromArgb(175, thumbInnerColorPaint)
			End If
			Using lgbThumb As New LinearGradientBrush(thumbHalfRect, newthumbOuterColorPaint, newthumbInnerColorPaint, gradientOrientation)
				lgbThumb.WrapMode = WrapMode.TileFlipXY
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
				'draw thumb band
				Dim newThumbPenColor As Color = thumbPenColorPaint
				If mouseEffects_Renamed AndAlso (Capture OrElse mouseInThumbRegion) Then
					newThumbPenColor = ControlPaint.Dark(newThumbPenColor)
				End If
				Using thumbPen As New Pen(newThumbPenColor)
					If thumbImage_Renamed IsNot Nothing Then
						e.Graphics.DrawImage(thumbImage_Renamed, thumbRect_Renamed)
					Else
						e.Graphics.FillPath(lgbThumb, thumbPath)
						e.Graphics.DrawPath(thumbPen, thumbPath)
					End If
				End Using
				'gp.Dispose();                    
				'                    if (Capture || mouseInThumbRegion)
				'						using (LinearGradientBrush lgbThumb2 = new LinearGradientBrush(thumbHalfRect, Color.FromArgb(150, Color.Blue), Color.Transparent, gradientOrientation))
				'						{
				'							lgbThumb2.WrapMode = WrapMode.TileFlipXY;
				'							e.Graphics.FillPath(lgbThumb2, gp);
				'						}
			End Using

			'draw focusing rectangle
			If Focused And drawFocusRectangle_Renamed Then
				Using p As New Pen(Color.FromArgb(200, barPenColorPaint))
					p.DashStyle = DashStyle.Dot
					Dim r As Rectangle = ClientRectangle
					r.Width -= 2
					r.Height -= 1
					r.X += 1
					'ControlPaint.DrawFocusRectangle(e.Graphics, r);                        
					Using gpBorder As GraphicsPath = CreateRoundRectPath(r, borderRoundRectSize_Renamed)
						e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
						e.Graphics.DrawPath(p, gpBorder)
					End Using
				End Using
			End If
		Catch Err As Exception
			Console.WriteLine("DrawBackGround Error in " & Name & ":" & Err.Message)
		Finally
		End Try
	End Sub

#End Region

#Region "Overided events"

	Private mouseInRegion As Boolean = False
	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged"></see> event.
	''' </summary>
	''' <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnEnabledChanged(ByVal e As EventArgs)
		MyBase.OnEnabledChanged(e)
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"></see> event.
	''' </summary>
	''' <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
		MyBase.OnMouseEnter(e)
		mouseInRegion = True
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"></see> event.
	''' </summary>
	''' <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
		MyBase.OnMouseLeave(e)
		mouseInRegion = False
		mouseInThumbRegion = False
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
		MyBase.OnMouseDown(e)
		If e.Button = MouseButtons.Left Then
			Capture = True
			RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.ThumbTrack, trackerValue))
			RaiseEvent ValueChanged(Me, New EventArgs())
			OnMouseMove(e)
		End If
		Invalidate()
	End Sub

	Private mouseInThumbRegion As Boolean = False

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		MyBase.OnMouseMove(e)
		mouseInThumbRegion = IsPointInRect(e.Location, thumbRect_Renamed)
		If Capture And e.Button = MouseButtons.Left Then
			Dim oldValue As Integer = trackerValue

			Dim [set] As ScrollEventType = ScrollEventType.ThumbPosition
			Dim pt As Point = e.Location
			Dim p As Integer = CInt(IIf(barOrientation = Orientation.Horizontal, pt.X, pt.Y))
			Dim margin As Integer = thumbSize_Renamed >> 1
			p -= margin
			Dim coef As Single = CSng(barMaximum - barMinimum) / CSng(CSng(IIf(barOrientation = Orientation.Horizontal, ClientSize.Width, ClientSize.Height)) - 2 * margin)
			trackerValue = CInt(p * coef + barMinimum)

			If trackerValue <= barMinimum Then
				trackerValue = barMinimum
				[set] = ScrollEventType.First
			ElseIf trackerValue >= barMaximum Then
				trackerValue = barMaximum
				[set] = ScrollEventType.Last
			End If

			RaiseEvent Scroll(Me, New ScrollEventArgs([set], trackerValue))
			RaiseEvent ValueChanged(Me, New EventArgs())
		End If
		Refresh()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
		MyBase.OnMouseUp(e)
		Capture = False
		mouseInThumbRegion = IsPointInRect(e.Location, thumbRect_Renamed)
		RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.EndScroll, trackerValue))
		RaiseEvent ValueChanged(Me, New EventArgs())
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnMouseWheel(ByVal e As MouseEventArgs)
		MyBase.OnMouseWheel(e)
		Dim v As Integer = CInt(e.Delta / 120 * (barMaximum - barMinimum) / mouseWheelBarPartitions_Renamed)
		SetProperValue(Value - v)
		RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.EndScroll, trackerValue))
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"></see> event.
	''' </summary>
	''' <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
		MyBase.OnGotFocus(e)
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"></see> event.
	''' </summary>
	''' <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnLostFocus(ByVal e As EventArgs)
		MyBase.OnLostFocus(e)
		Invalidate()
	End Sub

	''' <summary>
	''' Raises the <see cref="E:System.Windows.Forms.Control.KeyUp"></see> event.
	''' </summary>
	''' <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
	Protected Overrides Sub OnKeyUp(ByVal e As KeyEventArgs)
		MyBase.OnKeyUp(e)
		Select Case e.KeyCode
			Case Keys.Down, Keys.Left
				SetProperValue(Value - CInt(Fix(smallChange_Renamed)))
				RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.SmallDecrement, Value))
			Case Keys.Up, Keys.Right
				SetProperValue(Value + CInt(Fix(smallChange_Renamed)))
				RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.SmallIncrement, Value))
			Case Keys.Home
				Value = barMinimum
			Case Keys.End
				Value = barMaximum
			Case Keys.PageDown
				SetProperValue(Value - CInt(Fix(largeChange_Renamed)))
				RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.LargeDecrement, Value))
			Case Keys.PageUp
				SetProperValue(Value + CInt(Fix(largeChange_Renamed)))
				RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.LargeIncrement, Value))
		End Select
		If ScrollEvent IsNot Nothing AndAlso Value = barMinimum Then
			RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.First, Value))
		End If
		If ScrollEvent IsNot Nothing AndAlso Value = barMaximum Then
			RaiseEvent Scroll(Me, New ScrollEventArgs(ScrollEventType.Last, Value))
		End If
		Dim pt As Point = PointToClient(Cursor.Position)
		OnMouseMove(New MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0))
	End Sub

	''' <summary>
	''' Processes a dialog key.
	''' </summary>
	''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key to process.</param>
	''' <returns>
	''' true if the key was processed by the control; otherwise, false.
	''' </returns>
	Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
		If keyData = Keys.Tab Or ModifierKeys = Keys.Shift Then
			Return MyBase.ProcessDialogKey(keyData)
		Else
			OnKeyDown(New KeyEventArgs(keyData))
			Return True
		End If
	End Function

#End Region

#Region "Help routines"

	''' <summary>
	''' Creates the round rect path.
	''' </summary>
	''' <param name="rect">The rectangle on which graphics path will be spanned.</param>
	''' <param name="size">The size of rounded rectangle edges.</param>
	''' <returns></returns>
	Public Shared Function CreateRoundRectPath(ByVal rect As Rectangle, ByVal size As Size) As GraphicsPath
		Dim gp As New GraphicsPath()
		gp.AddLine(rect.Left + size.Width \ 2, rect.Top, rect.Right - size.Width \ 2, rect.Top)
		gp.AddArc(rect.Right - size.Width, rect.Top, size.Width, size.Height, 270, 90)

		gp.AddLine(rect.Right, rect.Top + size.Height \ 2, rect.Right, rect.Bottom - size.Width \ 2)
		gp.AddArc(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height, 0, 90)

		gp.AddLine(rect.Right - size.Width \ 2, rect.Bottom, rect.Left + size.Width \ 2, rect.Bottom)
		gp.AddArc(rect.Left, rect.Bottom - size.Height, size.Width, size.Height, 90, 90)

		gp.AddLine(rect.Left, rect.Bottom - size.Height \ 2, rect.Left, rect.Top + size.Height \ 2)
		gp.AddArc(rect.Left, rect.Top, size.Width, size.Height, 180, 90)
		Return gp
	End Function

	''' <summary>
	''' Desaturates colors from given array.
	''' </summary>
	''' <param name="colorsToDesaturate">The colors to be desaturated.</param>
	''' <returns></returns>
	Public Shared Function DesaturateColors(ByVal ParamArray colorsToDesaturate() As Color) As Color()
		Dim colorsToReturn(colorsToDesaturate.Length - 1) As Color
		For i As Integer = 0 To colorsToDesaturate.Length - 1
			'use NTSC weighted avarage
			Dim gray As Integer = CInt(Fix(colorsToDesaturate(i).R * 0.3 + colorsToDesaturate(i).G * 0.6 + colorsToDesaturate(i).B * 0.1))
			colorsToReturn(i) = Color.FromArgb(-&H10101 * (255 - gray) - 1)
		Next i
		Return colorsToReturn
	End Function

	''' <summary>
	''' Lightens colors from given array.
	''' </summary>
	''' <param name="colorsToLighten">The colors to lighten.</param>
	''' <returns></returns>
	Public Shared Function LightenColors(ByVal ParamArray colorsToLighten() As Color) As Color()
		Dim colorsToReturn(colorsToLighten.Length - 1) As Color
		For i As Integer = 0 To colorsToLighten.Length - 1
			colorsToReturn(i) = ControlPaint.Light(colorsToLighten(i))
		Next i
		Return colorsToReturn
	End Function

	''' <summary>
	''' Sets the trackbar value so that it wont exceed allowed range.
	''' </summary>
	''' <param name="val">The value.</param>
	Private Sub SetProperValue(ByVal val As Integer)
		If val < barMinimum Then
			Value = barMinimum
		ElseIf val > barMaximum Then
			Value = barMaximum
		Else
			Value = val
		End If
	End Sub

	''' <summary>
	''' Determines whether rectangle contains given point.
	''' </summary>
	''' <param name="pt">The point to test.</param>
	''' <param name="rect">The base rectangle.</param>
	''' <returns>
	'''     <c>true</c> if rectangle contains given point; otherwise, <c>false</c>.
	''' </returns>
	Private Shared Function IsPointInRect(ByVal pt As Point, ByVal rect As Rectangle) As Boolean
		If pt.X > rect.Left And pt.X < rect.Right And pt.Y > rect.Top And pt.Y < rect.Bottom Then
			Return True
		Else
			Return False
		End If
	End Function

#End Region
End Class
