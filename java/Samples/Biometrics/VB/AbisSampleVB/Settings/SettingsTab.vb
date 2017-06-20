Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Partial Public Class SettingsTab
	Inherits TabPageContentBase
	Implements IPageController
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		TabName = "Settings"
	End Sub

#End Region

#Region "Private fields"

	Private _pages As New List(Of SettingsPageBase)()
	Private _currentPage As SettingsPageBase = Nothing

#End Region

#Region "Public properties"

	Public Shadows Property TabController() As ITabController Implements IPageController.TabController
		Get
			Return MyBase.TabController
		End Get
		Set(ByVal value As ITabController)
			MyBase.TabController = value
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Overrides Sub OnTabAdded()
		If listViewPages.SelectedItems.Count = 0 Then
			NavigateToStartPage()
		End If

		MyBase.OnTabAdded()
	End Sub

	Public Overrides Sub SetParams(ByVal ParamArray parameters() As Object)
		If parameters IsNot Nothing AndAlso parameters.Length = 1 Then
			Try
				Dim param As String = parameters(0).ToString()
				Dim type As Type = type.GetType(param)
				NavigateToPage(type, TabController.Client)
			Catch
			End Try
		End If

		MyBase.SetParams(parameters)
	End Sub

	Public Sub NavigateToPage(ByVal pageType As Type, ByVal navigationParam As Object, ByVal ParamArray arguments() As Object) Implements IPageController.NavigateToPage
		If pageType Is Nothing OrElse (Not pageType.IsSubclassOf(GetType(SettingsPageBase))) Then
			Throw New ArgumentException("pageType")
		End If
		If _currentPage IsNot Nothing AndAlso _currentPage.GetType() Is pageType Then
			Return
		End If

		If _currentPage IsNot Nothing Then

			_currentPage.OnNavigatingFrom()
			_currentPage.NavigationParam = Nothing
			_currentPage = Nothing
			panelPage.Controls.Clear()
		End If

		If arguments Is Nothing OrElse arguments.Length = 0 Then
			arguments = New Object() {navigationParam}
		End If
		Dim page As SettingsPageBase = _pages.FirstOrDefault(Function(x) x.GetType() Is pageType)
		If page Is Nothing Then
			page = CType(Activator.CreateInstance(pageType), SettingsPageBase)
			page.PageController = Me
			_pages.Add(page)
		End If

		_currentPage = page
		panelPage.Controls.Add(page)

		page.NavigationParam = navigationParam
		page.OnNavigatedTo(arguments)

		For Each item As ListViewItem In listViewPages.Items
			If item.Tag Is pageType Then
				item.Selected = True
				Exit For
			End If
		Next item
	End Sub

	Public Sub NavigateToStartPage() Implements IPageController.NavigateToStartPage
		NavigateToPage(GetType(GeneralSettingsPage), TabController.Client)
	End Sub

#End Region

#Region "Private form events"

	Private Sub ListViewPagesSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listViewPages.SelectedIndexChanged
		Dim selected = listViewPages.SelectedItems
		Dim selectedItem As ListViewItem = Nothing
		If selected.Count > 0 Then
			selectedItem = listViewPages.SelectedItems(0)
			NavigateToPage(CType(selectedItem.Tag, Type), TabController.Client)
		End If
	End Sub

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		If _currentPage IsNot Nothing Then
			_currentPage.SaveSettings()
		End If
		SettingsManager.SaveSettings(TabController.Client)
		TabController.CloseTab(Me)
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		SettingsManager.LoadSettings(TabController.Client)
		TabController.CloseTab(Me)
	End Sub

	Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
		If _currentPage IsNot Nothing Then
			_currentPage.DefaultSettings()
		End If
	End Sub

	Private Sub SettingsTabLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			listViewPages.Items.Add(New ListViewItem("General") With {.Tag = GetType(GeneralSettingsPage)})
			If LicensingTools.CanCreateFingerTemplate(TabController().Client.LocalOperations) Then
				listViewPages.Items.Add(New ListViewItem("Fingers") With {.Tag = GetType(FingersSettingsPage)})
			End If
			If LicensingTools.CanCreateFaceTemplate(TabController().Client.LocalOperations) Then
				listViewPages.Items.Add(New ListViewItem("Faces") With {.Tag = GetType(FacesSettingsPage)})
			End If
			If LicensingTools.CanCreateIrisTemplate(TabController().Client.LocalOperations) Then
				listViewPages.Items.Add(New ListViewItem("Irises") With {.Tag = GetType(IrisesSettingsPage)})
			End If
			If LicensingTools.CanCreatePalmTemplate(TabController().Client.LocalOperations) Then
				listViewPages.Items.Add(New ListViewItem("Palms") With {.Tag = GetType(PalmsSettingsPage)})
			End If
			If LicensingTools.CanCreateVoiceTemplate(TabController().Client.LocalOperations) Then
				listViewPages.Items.Add(New ListViewItem("Voices") With {.Tag = GetType(VoicesSettingsPage)})
			End If
		End If
	End Sub

#End Region
End Class
