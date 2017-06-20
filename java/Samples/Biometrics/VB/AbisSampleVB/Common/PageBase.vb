Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Partial Public Class PageBase
	Inherits UserControl
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Public virtual methods"

	Public Overridable Sub OnNavigatedTo(ParamArray ByVal args() As Object)
		IsPageShown = True
	End Sub

	Public Overridable Sub OnNavigatingFrom()
		IsPageShown = False
	End Sub

	#End Region

	#Region "Public proeprties"

	Private privatePageController As IPageController
	Public Property PageController() As IPageController
		Get
			Return privatePageController
		End Get
		Set(ByVal value As IPageController)
			privatePageController = value
		End Set
	End Property
	Private privateNavigationParam As Object
	Public Property NavigationParam() As Object
		Get
			Return privateNavigationParam
		End Get
		Set(ByVal value As Object)
			privateNavigationParam = value
		End Set
	End Property
	Private privatePageName As String
	Public Property PageName() As String
		Get
			Return privatePageName
		End Get
		Set(ByVal value As String)
			privatePageName = value
		End Set
	End Property
	Private privateIsPageShown As Boolean
	Public Property IsPageShown() As Boolean
		Get
			Return privateIsPageShown
		End Get
		Set(ByVal value As Boolean)
			privateIsPageShown = value
		End Set
	End Property

	#End Region
End Class
