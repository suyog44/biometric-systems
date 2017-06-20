Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Partial Public Class EditSubjectTab
	Inherits Neurotec.Samples.TabPageContentBase
	Implements IPageController
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		TabName = "Subject"
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject
	Private _client As NBiometricClient
	Private _pages As New List(Of PageBase)()
	Private _currentPage As PageBase = Nothing

#End Region

#Region "Public methods"

	Public Overrides Sub OnTabAdded()
		Dim types As NBiometricType = NBiometricType.None
		If LicensingTools.CanCreateFingerTemplate(_client.LocalOperations) Then
			types = types Or NBiometricType.Finger
		End If
		If LicensingTools.CanCreateFaceTemplate(_client.LocalOperations) Then
			types = types Or NBiometricType.Face
		End If
		If LicensingTools.CanCreateIrisTemplate(_client.LocalOperations) Then
			types = types Or NBiometricType.Iris
		End If
		If LicensingTools.CanCreatePalmTemplate(_client.LocalOperations) Then
			types = types Or NBiometricType.Palm
		End If
		If LicensingTools.CanCreateVoiceTemplate(_client.LocalOperations) Then
			types = types Or NBiometricType.Voice
		End If
		subjectTree.AllowNew = types

		NavigateToStartPage()
		AddHandler subjectTree.PropertyChanged, AddressOf SubjectTreePropertyChanged
		MyBase.OnTabAdded()
	End Sub

	Public Overrides Sub OnTabLeave()
		NavigateToStartPage()
		MyBase.OnTabLeave()
	End Sub

	Public Overrides Sub SetParams(ByVal ParamArray parameters() As Object)
		If parameters Is Nothing OrElse parameters.Length <> 1 Then
			Throw New ArgumentException("parameters")
		End If
		_subject = CType(parameters(0), NSubject)
		_client = TabController.Client
		subjectTree.Subject = _subject

		DataBindings.Clear()
		Dim binding = DataBindings.Add("TabName", _subject, "Id")
		AddHandler binding.Format, AddressOf OnIdChanged
	End Sub

	Private Sub OnIdChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ConvertEventArgs)
		Dim value As String = If(e.Value Is Nothing, "Subject", String.Format("Subject: {0}", e.Value))
		If value.Length > 30 Then
			value = value.Substring(0, 30) & "..."
		End If
		e.Value = value
	End Sub

	Public Overrides Sub OnTabClose()
		DataBindings.Clear()
		RemoveHandler subjectTree.PropertyChanged, AddressOf SubjectTreePropertyChanged
		subjectTree.Subject = Nothing

		If _currentPage IsNot Nothing Then
			_currentPage.OnNavigatingFrom()
			_currentPage.NavigationParam = Nothing
		End If
		pagePanel.Controls.Clear()
		For Each item In _pages
			item.Dispose()
		Next item
		_pages.Clear()
		If _subject IsNot Nothing Then
			_subject.Dispose()
			_subject = Nothing
		End If
	End Sub

#End Region

#Region "Private methods"

	Private Sub SubjectTreePropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "SelectedItem" Then
			If IsHandleCreated Then
				BeginInvoke(New MethodInvoker(AddressOf OnSubjectTreeSelectedItemChanged))
			End If
		End If
	End Sub

	Private Sub OnSubjectTreeSelectedItemChanged()
		Dim selected = subjectTree.SelectedItem
		If selected Is Nothing OrElse selected.IsSubjectNode Then
			NavigateToStartPage()
		Else
			If selected.IsBiometricNode Then
				NavigateToPage(GetType(BiometricPreviewPage), selected)
			Else
				Dim pageType As Type = Nothing
				Select Case selected.BiometricType
					Case NBiometricType.Face
						pageType = GetType(CaptureFacePage)
					Case NBiometricType.Finger
						pageType = GetType(CaptureFingersPage)
					Case NBiometricType.Iris
						pageType = GetType(CaptureIrisPage)
					Case NBiometricType.Palm
						pageType = GetType(CapturePalmsPage)
					Case NBiometricType.Voice
						pageType = GetType(CaptureVoicePage)
					Case Else
						Throw New NotImplementedException()
				End Select

				NavigateToPage(pageType, selected, _subject, _client)
			End If
		End If
	End Sub

	Public Sub NavigateToPage(ByVal pageType As Type, ByVal navigationParam As Object, ByVal ParamArray args() As Object) Implements IPageController.NavigateToPage
		If _currentPage IsNot Nothing AndAlso _currentPage.GetType() Is pageType AndAlso _currentPage.NavigationParam Is navigationParam Then
			' Already in this page
			Return
		End If

		If _currentPage IsNot Nothing Then
			_currentPage.OnNavigatingFrom()
			_currentPage.NavigationParam = Nothing
			_currentPage = Nothing
			pagePanel.Controls.Clear()
		End If

		If args Is Nothing OrElse args.Length = 0 Then
			args = New Object() {navigationParam}
		End If

		Dim page As PageBase = _pages.FirstOrDefault(Function(x) x.GetType() Is pageType)
		If page Is Nothing Then
			Dim pp As Object = Activator.CreateInstance(pageType)
			page = CType(Activator.CreateInstance(pageType), PageBase)
			page.Dock = DockStyle.Fill
			page.PageController = Me
			_pages.Add(page)
		End If

		_currentPage = page
		page.OnNavigatedTo(args)
		page.NavigationParam = navigationParam
		pagePanel.Controls.Add(page)

		subjectTree.SelectedItem = subjectTree.GetNodeFor(navigationParam)
	End Sub

	Public Sub NavigateToStartPage() Implements IPageController.NavigateToStartPage
		NavigateToPage(GetType(SubjectStartPage), _subject)
	End Sub

#End Region

	Public Shadows Property TabController() As ITabController Implements IPageController.TabController
		Get
			Return MyBase.TabController
		End Get
		Set(ByVal value As ITabController)
			MyBase.TabController = value
		End Set
	End Property
End Class
