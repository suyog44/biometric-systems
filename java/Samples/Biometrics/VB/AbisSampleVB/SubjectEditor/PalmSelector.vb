Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class PalmSelector
	Inherits FingerSelector
	#Region "Private fields"
	''' <summary>
	''' The DesignMode property does not correctly tell you if you are in design mode
	''' see https://connect.microsoft.com/VisualStudio/feedback/details/553305
	''' </summary>
	Protected isConstructorCompleted As Boolean = False
	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		MyBase._allowedPositions.Clear()
		MyBase._allowedPositions.AddRange(New NFPosition() {NFPosition.RightUpperPalm, NFPosition.RightLowerPalm, NFPosition.RightInterdigital, NFPosition.RightHypothenar, NFPosition.RightThenar, NFPosition.RightFullPalm, NFPosition.LeftUpperPalm, NFPosition.LeftLowerPalm, NFPosition.LeftInterdigital, NFPosition.LeftHypothenar, NFPosition.LeftThenar, NFPosition.LeftFullPalm})

		DoubleBuffered = True
		isConstructorCompleted = True
	End Sub

	#End Region

	#Region "Private fields"

	Private preferedPosition As NFPosition = NFPosition.UnknownPalm

	#End Region

	#Region "Public properties"

	<Browsable(False)> _
	Public Overrides Property AllowedPositions() As NFPosition()
		Get
			Return MyBase.AllowedPositions
		End Get
		Set(ByVal value As NFPosition())
			If DesignMode OrElse (Not isConstructorCompleted) Then
				Return
			End If
			MyBase._allowedPositions.Clear()
			If value IsNot Nothing Then
				Dim goodValues = value.Where(Function(x) NBiometricTypes.IsPositionKnown(x) AndAlso NBiometricTypes.IsPositionPalm(x))
				MyBase._allowedPositions.AddRange(goodValues)
			End If
			_painter.Clear()
			OnDataChanged()
			Invalidate()
		End Set
	End Property

	<Browsable(False)> _
	Public Overrides Property IsRolled() As Boolean
		Get
			Return MyBase.IsRolled
		End Get
		Set(ByVal value As Boolean)
		End Set
	End Property

	<Browsable(False)> _
	Public Overrides Property MissingPositions() As NFPosition()
		Get
			Return MyBase.MissingPositions
		End Get
		Set(ByVal value As NFPosition())
		End Set
	End Property

	#End Region

	#Region "Protected methods"

	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		If MyBase._allowHighlight AndAlso e.Location <> _mousePosition Then
			ShowPositionTooltip(e)
		End If

		MyBase.OnMouseMove(e)
	End Sub

	Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
		If e.Button = MouseButtons.Right Then
			Dim hit = GetHitElements(e.Location).ToList()
			If hit.Count >= 2 Then
				Dim index As Integer = hit.IndexOf(preferedPosition)
				If index = -1 Then
					index = 1
				ElseIf index + 1 = hit.Count Then
					index = 0
				Else
					index += 1
				End If

				preferedPosition = hit(index)
				ShowPositionTooltip(e)
			End If
		End If
		MyBase.OnMouseDown(e)
	End Sub

	Protected Overrides Sub OnDataChanged()
		MyBase.OnDataChanged()
	End Sub

	Protected Overrides Function SelectHighlightedElement(ByVal elements As IEnumerable(Of SvgPath)) As SvgPath
		If _allowHighlight Then
			Return elements.Where(Function(x) x.ItemType = ItemType.Item AndAlso x.HitTest(_mousePosition)).OrderByDescending(Function(x) x.Position = preferedPosition).ThenBy(Function(x) x.Position).FirstOrDefault()
		End If
		Return Nothing
	End Function

	#End Region

	#Region "Private methods"

	Private Function GetHitElements(ByVal location As Point) As IEnumerable(Of NFPosition)
		Return GetAvailableElements(False).Where(Function(x) x.HitTest(location)).Select(Function(x) x.Position)
	End Function

	Private Function SortHitElements(ByVal items As IEnumerable(Of NFPosition)) As IEnumerable(Of NFPosition)
		If items.Count() = 0 Then
			Return items
		Else
			Dim list = items.ToList()
			If preferedPosition <> NFPosition.UnknownPalm AndAlso list.Exists(Function(x) x = preferedPosition) Then
				Return list.OrderByDescending(Function(x) x = preferedPosition)
			End If
			Return list
		End If
	End Function

	Private Sub ShowPositionTooltip(ByVal e As MouseEventArgs)
		Dim hit = SortHitElements(GetHitElements(e.Location))
		If hit.Count() = 0 Then
			toolTip.Hide(Me)
		Else
			Dim bestFit As NFPosition = hit.First()
			Dim msg As String = GenerateTooltipMessage(hit, bestFit)
			toolTip.ToolTipTitle = String.Format("Position: {0}", bestFit)
			toolTip.Show(msg, Me, e.Location.X + 15, e.Location.Y + 15)
		End If
	End Sub

	Private Function GenerateTooltipMessage(ByVal hit As IEnumerable(Of NFPosition), ByVal bestFit As NFPosition) As String
		If hit.Count() <= 1 Then
			Return " "
		End If

		Dim result As String = "This is also: "
		For Each item As NFPosition In hit
			If item <> bestFit Then
				result &= item.ToString() & ", "
			End If
		Next item
		result &= Constants.vbLf & "Click right mouse button to show other position"
		Return result
	End Function

	#End Region
End Class
