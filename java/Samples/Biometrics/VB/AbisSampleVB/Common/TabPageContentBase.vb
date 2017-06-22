Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Neurotec.Biometrics.Client
Imports System

Partial Public Class TabPageContentBase
	Inherits UserControl
	Implements INotifyPropertyChanged
	#Region "Public properties"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _tabName As String = "Tab"

	#End Region

	#Region "Public properties"

	Private privateTabController As ITabController
	Public Property TabController() As ITabController
		Get
			Return privateTabController
		End Get
		Set(ByVal value As ITabController)
			privateTabController = value
		End Set
	End Property

	Public Property TabName() As String
		Get
			Return _tabName
		End Get
		Set(ByVal value As String)
			If _tabName <> value Then
				_tabName = value
				RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("TabName"))
			End If
		End Set
	End Property

	#End Region

	#Region "Public virtual methods"

	Public Overridable Sub OnTabAdded()
	End Sub

	Public Overridable Sub OnTabEnter()
	End Sub

	Public Overridable Sub OnTabLeave()
	End Sub

	Public Overridable Sub OnTabClose()
	End Sub

	Public Overridable Sub SetParams(ParamArray ByVal parameters() As Object)
	End Sub

	#End Region

	#Region "INotifyPropertyChanged Members"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	#End Region
End Class
