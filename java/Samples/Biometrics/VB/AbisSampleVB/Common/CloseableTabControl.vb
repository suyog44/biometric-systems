Imports Microsoft.VisualBasic
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Samples.My

Partial Public Class CloseableTabControl
	Inherits TabControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DrawMode = TabDrawMode.OwnerDrawFixed
		DoubleBuffered = True
		Dim p As Point = Padding
		p.X += 10
		Padding = p
		LastPageIndex = -1
	End Sub

#End Region

#Region "Private fields"

	Private mouseOn As Rectangle

#End Region

#Region "Public properties"

	Private privateLastPageIndex As Integer
	Public Property LastPageIndex() As Integer
		Get
			Return privateLastPageIndex
		End Get
		Set(ByVal value As Integer)
			privateLastPageIndex = value
		End Set
	End Property

#End Region

#Region "Protected methods"

	Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
		Dim g As Graphics = e.Graphics

		Dim r As Rectangle = e.Bounds
		Using b As Brush = New SolidBrush(If(SelectedIndex = e.Index, SystemColors.Window, BackColor))
			g.FillRectangle(b, r)
		End Using

		r = GetTabRect(e.Index)
		Dim page As TabPage = TabPages(e.Index)
		Dim title As String = page.Text
		Dim textSize As SizeF = g.MeasureString(title, e.Font)
		g.DrawString(title, e.Font, Brushes.Black, r)

		Dim closeable As CloseableTabPage = TryCast(page, CloseableTabPage)
		If closeable IsNot Nothing AndAlso closeable.CanClose Then
			r = GetCloseButtonBounds(e.Index)
			If r.Contains(Me.PointToClient(MousePosition)) Then
				g.DrawImage(Resources.closeMouseOn, r)
			Else
				g.DrawImage(Resources.close, r)
			End If
		End If

		MyBase.OnDrawItem(e)
	End Sub

	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		If (Not mouseOn.IsEmpty) AndAlso (Not mouseOn.Contains(e.Location)) Then
			mouseOn = Rectangle.Empty
			Invalidate()
			Return
		ElseIf (Not mouseOn.IsEmpty) AndAlso mouseOn.Contains(e.Location) Then
			Return
		End If

		For i As Integer = 0 To TabPages.Count - 1
			Dim page As CloseableTabPage = TryCast(TabPages(i), CloseableTabPage)
			If page IsNot Nothing AndAlso page.CanClose Then
				Dim r As Rectangle = GetCloseButtonBounds(i)
				If r.Contains(e.Location) Then
					mouseOn = r
					Invalidate()
					Return
				End If
			End If
		Next i
		MyBase.OnMouseMove(e)
	End Sub

	Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
		Dim i As Integer = 0
		Do While i < TabPages.Count
			Dim page As CloseableTabPage = TryCast(TabPages(i), CloseableTabPage)
			If page IsNot Nothing AndAlso page.CanClose Then
				Dim r As Rectangle = GetCloseButtonBounds(i)
				If r.Contains(e.Location) Then
					Dim currentPage As Integer = LastPageIndex
					Dim removedPage As Integer = i
					TabPages.Remove(page)
					If TabPages.Count > 0 Then
						If currentPage = removedPage Then
							SelectedIndex = 0
						Else
							SelectedIndex = If(removedPage < currentPage, currentPage - 1, currentPage)
						End If
					End If
				End If
			End If
			i += 1
		Loop

		MyBase.OnMouseClick(e)
	End Sub

	Protected Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
		Dim control As CloseableTabPage = TryCast(e.Control, CloseableTabPage)
		If control IsNot Nothing AndAlso control.Content IsNot Nothing Then
			control.Content.OnTabAdded()
		End If

		MyBase.OnControlAdded(e)
	End Sub

	Protected Overrides Sub OnControlRemoved(ByVal e As ControlEventArgs)
		Dim control As CloseableTabPage = TryCast(e.Control, CloseableTabPage)
		If control IsNot Nothing AndAlso control.Content IsNot Nothing Then
			control.Content.OnTabClose()
		End If

		If control IsNot Nothing Then
			control.Dispose()
		End If

	End Sub

	Protected Overrides Sub OnDeselecting(ByVal e As TabControlCancelEventArgs)
		If e.TabPageIndex <> -1 Then
			LastPageIndex = e.TabPageIndex
		End If

		MyBase.OnDeselecting(e)
	End Sub

#End Region

#Region "Private methods"

	Private Function GetCloseButtonBounds(ByVal tabIndex As Integer) As Rectangle
		Dim r As Rectangle = GetTabRect(tabIndex)
		Const offset As Integer = 5
		Dim size As Integer = r.Height - offset * 2
		Dim imageLocation As New Rectangle(0, offset, size, size)
		imageLocation.X = r.X + r.Width - size - offset
		imageLocation.Y += r.Y

		Return imageLocation
	End Function

#End Region
End Class
