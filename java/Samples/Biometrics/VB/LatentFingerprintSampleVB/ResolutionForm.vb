Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class ResolutionForm
	Inherits Form
	#Region "Private Variables"

	Private _startPt As Point
	Private _endPt As Point
	Private _dragging As Boolean

	Private Const EndMarkerSize As Integer = 4

	#End Region ' Private Variables

	#Region "Constructor"
	Public Sub New()
		InitializeComponent()
	End Sub
	#End Region

	#Region "Public Properties"
	Public Property HorzResolution() As Single
		Get
			Return Convert.ToSingle(nudHorzResolution.Value)
		End Get
		Set(ByVal value As Single)
			Dim val As Decimal = Convert.ToDecimal(value)
			If val < nudHorzResolution.Minimum Then
				val = nudHorzResolution.Minimum
			ElseIf val > nudHorzResolution.Maximum Then
				val = nudHorzResolution.Maximum
			End If
			nudHorzResolution.Value = val
		End Set
	End Property

	Public Property VertResolution() As Single
		Get
			Return Convert.ToSingle(nudVertResolution.Value)
		End Get
		Set(ByVal value As Single)
			Dim val As Decimal = Convert.ToDecimal(value)
			If val < nudVertResolution.Minimum Then
				val = nudVertResolution.Minimum
			ElseIf val > nudVertResolution.Maximum Then
				val = nudVertResolution.Maximum
			End If
			nudVertResolution.Value = val
		End Set
	End Property

	Public WriteOnly Property FingerImage() As Bitmap
		Set(ByVal value As Bitmap)
			pbFingerprint.Image = value
		End Set
	End Property

	#End Region ' Public Properties

	#Region "Event Handling"

	Private Function CalculateDistance() As Single
		Return Convert.ToSingle(Math.Sqrt((_startPt.X - _endPt.X) * CDbl(_startPt.X - _endPt.X) + (_startPt.Y - _endPt.Y) * CDbl(_startPt.Y - _endPt.Y)))
	End Function

	Private Sub pbFingerprint_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbFingerprint.MouseDown
		If e.Button = MouseButtons.Left Then
			_startPt = e.Location
			_endPt = Point.Empty
			pbFingerprint.Cursor = Cursors.Cross
			_dragging = True
		End If
	End Sub

	Private Sub pbFingerprint_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbFingerprint.MouseMove
		If e.Button = MouseButtons.Left AndAlso _dragging Then
			_endPt = e.Location
			pbFingerprint.Invalidate()
		End If
	End Sub

	Private Sub pbFingerprint_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbFingerprint.MouseUp
		If e.Button = MouseButtons.Left AndAlso _dragging Then
			_endPt = e.Location
			Cursor = Cursors.Arrow
			_dragging = False

			Dim scaleToInch As Single = 1.0f
			If rbCentimeters.Checked Then
				scaleToInch = 1.0f / 2.54f
			End If

			Dim distance As Single = CalculateDistance()
			If distance > 50.0 Then
				nudHorzResolution.Value = Convert.ToDecimal(distance / (Convert.ToSingle(nudUnitScale.Value) * scaleToInch))
				nudVertResolution.Value = nudHorzResolution.Value
			Else
				Utilities.ShowInformation("Please draw a longer line segment.")
			End If

			_startPt = Point.Empty
			_endPt = Point.Empty
			pbFingerprint.Invalidate()
		End If
	End Sub

	Private Sub DrawSelectionLine(ByVal g As Graphics, ByVal pen As Pen)
		' draw pointer markers
		g.DrawLine(pen, _startPt.X - EndMarkerSize, _startPt.Y, _startPt.X + EndMarkerSize, _startPt.Y)
		g.DrawLine(pen, _startPt.X, _startPt.Y - EndMarkerSize, _startPt.X, _startPt.Y + EndMarkerSize)
		g.DrawLine(pen, _endPt.X - EndMarkerSize, _endPt.Y, _endPt.X + EndMarkerSize, _endPt.Y)
		g.DrawLine(pen, _endPt.X, _endPt.Y - EndMarkerSize, _endPt.X, _endPt.Y + EndMarkerSize)
		' draw line between starting and ending points
		g.DrawLine(pen, _startPt, _endPt)
	End Sub

	Private Sub pbFingerprint_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pbFingerprint.Paint
		If (Not _startPt.IsEmpty) AndAlso (Not _endPt.IsEmpty) Then
			Dim g As Graphics = e.Graphics

			Dim smoothingMode As System.Drawing.Drawing2D.SmoothingMode = g.SmoothingMode
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality

			Using pen = New Pen(Color.White, 3)
				DrawSelectionLine(g, pen)
			End Using

			Using pen = New Pen(Color.Green, 1)
				DrawSelectionLine(g, pen)
			End Using

			g.SmoothingMode = smoothingMode
		End If
	End Sub

	Private Sub nudHorzResolution_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudHorzResolution.ValueChanged
		errorProvider1.SetError(nudHorzResolution,If(nudHorzResolution.Value < 250D, "Current resolution is lower than recommended minimum.", ""))
	End Sub

	Private Sub nudVertResolution_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudVertResolution.ValueChanged
		errorProvider1.SetError(nudVertResolution,If(nudVertResolution.Value < 250D, "Current resolution is lower than recommended minimum.", ""))
	End Sub

	#End Region ' Event Handling
End Class
