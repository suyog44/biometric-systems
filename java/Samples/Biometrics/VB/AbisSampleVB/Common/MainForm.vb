Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Gui
Imports Neurotec.IO
Imports System.Collections.Generic

Partial Public Class MainForm
	Inherits Form
	Implements ITabController
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "ITabController Members"

	Public Sub CreateNewSubjectTab(ByVal subject As NSubject) Implements ITabController.CreateNewSubjectTab
		ShowTab(GetType(EditSubjectTab), True, True, subject)
	End Sub

	Private Function OpenSubjectFunc(ByVal fileName As String, ByVal formatOwner As UShort, ByVal formatType As UShort) As NSubject
		Dim subject As NSubject = NSubject.FromFile(fileName, formatOwner, formatType)
		Dim status As NBiometricStatus = Client.CreateTemplate(subject)
		If status <> NBiometricStatus.Ok AndAlso status <> NBiometricStatus.None Then
			Utilities.ShowError("Failed to process template: {0}", status)
			Return Nothing
		End If
		Return subject
	End Function

	Public Sub OpenSubject() Implements ITabController.OpenSubject
		Using dialog = New OpenSubjectDialog()
			If dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Try
					Dim result As Object = LongActionDialog.ShowDialog(Me, "Opening subject", New Func(Of String, UShort, UShort, NSubject)(AddressOf OpenSubjectFunc), dialog.FileName, dialog.FormatOwner, dialog.FormatType)
					If result IsNot Nothing Then
						ShowTab(GetType(EditSubjectTab), True, True, CType(result, NSubject))
					End If
				Catch ex As Exception
					Utilities.ShowError(ex)
				End Try
			End If
		End Using
	End Sub

	Public Sub ShowSettings(ByVal ParamArray args() As Object) Implements ITabController.ShowSettings
		ShowTab(GetType(SettingsTab), False, True, args)
	End Sub

	Private Sub InitializeClient(ByVal biometricClient As NBiometricClient, ByVal clearDatabase As Boolean)
		SettingsManager.LoadSettings(biometricClient)
		biometricClient.Initialize()
		SettingsManager.LoadPreferedDevices(biometricClient)
		If clearDatabase Then
			Dim status As NBiometricStatus = biometricClient.Clear()
			If status <> NBiometricStatus.Ok Then
				Utilities.ShowInformation("Failed to clear database: {0}", status)
			End If
		End If
	End Sub

	Public Function ShowChangeDatabase() As Boolean Implements ITabController.ShowChangeDatabase
		Dim count As Integer = tabControl.TabPages.Count
		If count > 1 Then
			If (Not Utilities.ShowQuestion(Me, "Changing database will close all currently opened tabs. Do you want to continue?")) Then
				Return False
			End If

			tabControl.SelectedIndex = 0
			For i As Integer = 1 To count - 1
				tabControl.TabPages.RemoveAt(1)
			Next i
		End If

		Using dialog = New ChangeDatabaseDialog()
			If dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Try
					If Client IsNot Nothing Then
						Client.Dispose()
					End If
					Client = dialog.BiometricClient
					Dim operations As NBiometricOperations = Client.LocalOperations
					If (Client.RemoteConnections.Count > 0) Then
						operations = operations Or Client.RemoteConnections(0).Operations
					End If
					tsbGetSubject.Enabled = (operations And NBiometricOperations.Get) = NBiometricOperations.Get
					Return True
				Catch ex As Exception
					Utilities.ShowError(ex)
				End Try
			End If
		End Using
		Return False
	End Function

	Public Sub ShowAbout() Implements ITabController.ShowAbout
		AboutBox.Show(Me)
	End Sub

	Public Sub ShowTab(ByVal tabType As Type, ByVal alwaysCreateNew As Boolean, ByVal canClose As Boolean, ByVal ParamArray args() As Object) Implements ITabController.ShowTab
		If tabType Is Nothing OrElse (Not tabType.IsSubclassOf(GetType(TabPageContentBase))) Then
			Throw New ArgumentException("tabType")
		End If
		If (Not alwaysCreateNew) Then
			Dim tab As CloseableTabPage = tabControl.TabPages.OfType(Of CloseableTabPage)().FirstOrDefault(Function(x) x.Content IsNot Nothing AndAlso x.Content.GetType() Is tabType)
			If tab IsNot Nothing Then
				tab.Content.SetParams(args)
				tabControl.SelectedTab = tab
				Return
			End If
		End If

		Dim content As TabPageContentBase = CType(Activator.CreateInstance(tabType), TabPageContentBase)
		Dim page As CloseableTabPage = New CloseableTabPage With {.Content = content, .CanClose = canClose}
		content.Dock = DockStyle.Fill
		content.TabController = Me
		content.SetParams(args)
		page.DataBindings.Add("Text", content, "TabName")
		tabControl.TabPages.Add(page)
		tabControl.SelectedTab = page
	End Sub

	Public Sub CloseTab(ByVal tab As TabPageContentBase) Implements ITabController.CloseTab
		Dim owner As CloseableTabPage = tabControl.TabPages.OfType(Of CloseableTabPage)().First(Function(x) x.Content Is tab)

		If tabControl.TabPages.Count > tabControl.LastPageIndex Then
			tabControl.SelectedIndex = tabControl.LastPageIndex
		ElseIf tabControl.TabPages.Count > 0 Then
			tabControl.SelectedIndex = 0
		End If
		tabControl.TabPages.Remove(owner)
	End Sub

	Private privateClient As NBiometricClient
	Public Property Client() As NBiometricClient Implements ITabController.Client
		Get
			Return privateClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateClient = value
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub TsbAboutClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbAbout.Click
		ShowAbout()
	End Sub

	Private Sub TsbSettingsClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbSettings.Click
		ShowSettings()
	End Sub

	Private Sub TsbOpenSubjectClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenSubject.Click
		OpenSubject()
	End Sub

	Private Sub TsbNewSubjectClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbNewSubject.Click
		CreateNewSubjectTab(New NSubject())
	End Sub

	Private Sub TbsChangeDatabaseClick(ByVal sender As Object, ByVal e As EventArgs) Handles tbsChangeDatabase.Click
		ShowChangeDatabase()
	End Sub

	Private Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		ShowTab(GetType(StartTab), True, False)
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		tabControl.TabPages.Clear()
		If Client IsNot Nothing Then
			Client.Dispose()
			Client = Nothing
		End If
	End Sub

	Private Sub MainFormShown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		If (Not ShowChangeDatabase()) Then
			Close()
		End If
	End Sub

	Private Sub TsbGetSubjectClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbGetSubject.Click
		Using dialog As GetSubjectDialog = New GetSubjectDialog With {.Client = Me.Client}
			If dialog.ShowDialog() = DialogResult.OK Then
				Try
					CreateNewSubjectTab(RecreateSubject(dialog.Subject))
				Catch ex As Exception
					Utilities.ShowError(ex)
				End Try
			End If
		End Using
	End Sub

#End Region

#Region "Private methods"

	Private Function RecreateSubject(ByVal subject As NSubject) As NSubject
		Dim schema As SampleDbSchema = SettingsManager.CurrentSchema
		Dim hasSchema As Boolean = Not schema.IsEmpty
		Dim galeryRecordCounts() As Integer = Nothing
		Dim resultSubject As NSubject = subject

		If hasSchema Then
			Dim bag As New NPropertyBag()
			subject.CaptureProperties(bag)

			If (Not String.IsNullOrEmpty(schema.EnrollDataName)) AndAlso bag.ContainsKey(schema.EnrollDataName) Then
				Dim templateBuffer As NBuffer = subject.GetTemplateBuffer()
				Dim enrollData As NBuffer = CType(bag(schema.EnrollDataName), NBuffer)
				resultSubject = EnrollDataSerializer.Deserialize(templateBuffer, enrollData, galeryRecordCounts)

				Dim allProperties As List(Of String) = bag.Select(Function(x) x.Key).ToList()
				Dim allowedProperties As List(Of String) = Enumerable.Union(schema.BiographicData.Elements, schema.CustomData.Elements).Select(Function(x) x.Name).ToList()
				For Each propName In allProperties.Where(Function(x) (Not allowedProperties.Contains(x)))
					bag.Remove(propName)
				Next propName

				bag.ApplyTo(resultSubject)
				resultSubject.Id = subject.Id
			End If
			If (Not String.IsNullOrEmpty(schema.GenderDataName)) AndAlso bag.ContainsKey(schema.GenderDataName) Then
				Dim genderString As String = CStr(bag(schema.GenderDataName))
				resultSubject.SetProperty(schema.GenderDataName, System.Enum.Parse(GetType(NGender), genderString))
			End If
		End If

		Return resultSubject
	End Function

#End Region

#Region "Protected methods"
	Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
		MyBase.OnResize(e)

		If WindowState = FormWindowState.Maximized Then
			Update()
		End If
	End Sub
#End Region

End Class
