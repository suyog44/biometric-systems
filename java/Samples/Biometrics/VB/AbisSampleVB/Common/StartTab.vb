Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics

Partial Public Class StartTab
	Inherits Neurotec.Samples.TabPageContentBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		TabName = "Start page"
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnNewSubjectClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
		TabController.CreateNewSubjectTab(New NSubject())
	End Sub

	Private Sub BtnAboutClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAbout.Click
		TabController.ShowAbout()
	End Sub

	Private Sub BtnSettingsClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSettings.Click
		TabController.ShowSettings()
	End Sub

	Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
		TabController.OpenSubject()
	End Sub

	Private Sub BtnChangeDbClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeDb.Click
		TabController.ShowChangeDatabase()
	End Sub

#End Region
End Class
