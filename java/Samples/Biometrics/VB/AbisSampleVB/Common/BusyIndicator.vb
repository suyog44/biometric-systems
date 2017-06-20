Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class BusyIndicator
	Inherits Control
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True
		timer.Start()
	End Sub

#End Region

#Region "Private fields"

	Private WithEvents timer As Timer
	Private components As IContainer
	Private currentAngle As Integer = 0

#End Region

#Region "Private constants"

	Private Const Radius As Integer = 100
	Private Const CircleRadius As Integer = 33
	Private Const BorderMargin As Integer = 4
	Private Const Center As Integer = BorderMargin + CircleRadius + Radius
	Private Const CombindedSize As Single = BorderMargin + CircleRadius + Radius * 2 + CircleRadius + BorderMargin

#End Region

#Region "Private methods"

	Private Function GetRectangle(ByVal angle As Double) As RectangleF
		Dim x As Double = Center + Radius * Math.Cos(angle)
		Dim y As Double = Center + Radius * Math.Sin(angle)

		Return New RectangleF(CSng(x) - CircleRadius, CSng(y) - CircleRadius, CircleRadius * 2, CircleRadius * 2)
	End Function

	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.timer = New System.Windows.Forms.Timer(Me.components)
		Me.SuspendLayout()
		' 
		' timer
		' 
		Me.timer.Interval = 33
		Me.ResumeLayout(False)
	End Sub

	Private Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles timer.Tick
		currentAngle = (currentAngle + 10) Mod 360
		Invalidate()
	End Sub

#End Region

#Region "Protected methods"

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		MyBase.OnPaint(e)

		Dim g As Graphics = e.Graphics
		g.SmoothingMode = SmoothingMode.AntiAlias

		Dim sz As Size = Me.Size
		Dim zoom As Single = Math.Min(sz.Width / CombindedSize, sz.Height / CombindedSize)
		Dim dx As Single = (zoom * CombindedSize - sz.Width) / 2
		Dim dy As Single = (zoom * CombindedSize - sz.Height) / 2

		Dim m As Matrix = g.Transform
		m.RotateAt(currentAngle, New PointF(sz.Width \ 2, sz.Height \ 2))
		g.Transform = m

		g.TranslateTransform(-dx, -dy)
		g.ScaleTransform(zoom, zoom)

		Dim value As Byte = 0
		For angle As Double = 0 To 2 * Math.PI - 1 Step Math.PI / 4
			value += 25
			Using b = New SolidBrush(Color.FromArgb(value, value, value))
				g.FillEllipse(b, GetRectangle(angle))
			End Using
		Next angle
	End Sub

#End Region
End Class
