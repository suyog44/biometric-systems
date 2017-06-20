Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Partial Public Class CloseableTabPage
	Inherits TabPage
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _content As TabPageContentBase

	#End Region

	#Region "Public properties"

	Public Property Content() As TabPageContentBase
		Get
			Return _content
		End Get
		Set(ByVal value As TabPageContentBase)
			Controls.Clear()
			_content = value
			If value IsNot Nothing Then
				Controls.Add(value)
			End If
		End Set
	End Property

	Private privateCanClose As Boolean
	<DefaultValue(True)> _
	Public Property CanClose() As Boolean
		Get
			Return privateCanClose
		End Get
		Set(ByVal value As Boolean)
			privateCanClose = value
		End Set
	End Property

	#End Region

	#Region "Public methods"

	Public Function Close() As Boolean
		If (Not CanClose) Then
			Return False
		ElseIf _content IsNot Nothing Then
			_content.OnTabClose()
		End If
		Return True
	End Function

	#End Region

	#Region "Protected methods"

	Protected Overrides Sub OnEnter(ByVal e As EventArgs)
		If _content IsNot Nothing Then
			_content.OnTabEnter()
		End If
		MyBase.OnEnter(e)
	End Sub

	Protected Overrides Sub OnLeave(ByVal e As EventArgs)
		If _content IsNot Nothing Then
			_content.OnTabLeave()
		End If
		MyBase.OnLeave(e)
	End Sub

	#End Region
End Class
