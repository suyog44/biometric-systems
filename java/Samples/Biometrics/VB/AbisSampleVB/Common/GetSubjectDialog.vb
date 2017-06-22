Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Partial Public Class GetSubjectDialog
	Inherits Form
#Region "Public constructor"
	Public Sub New()
		InitializeComponent()
	End Sub
#End Region

#Region "Public properties"

	Private privateSubject As NSubject
	Public Property Subject() As NSubject
		Get
			Return privateSubject
		End Get
		Set(ByVal value As NSubject)
			privateSubject = value
		End Set
	End Property

	Private privateClient As NBiometricClient
	Public Property Client() As NBiometricClient
		Get
			Return privateClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateClient = value
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		Try
			Dim subj As NSubject = New NSubject With {.Id = tbId.Text}

			Dim status As NBiometricStatus = Client.Get(subj)
			If status <> NBiometricStatus.Ok Then
				Utilities.ShowInformation("Failed to retrieve subject. Status: {0}", status)
			Else
				Subject = subj
				DialogResult = System.Windows.Forms.DialogResult.OK
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub GetSubjectDialogLoad(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		If Client Is Nothing Then
			Throw New ArgumentNullException()
		End If

		Try
			Dim operations As NBiometricOperations = Client.LocalOperations
			If Client.RemoteConnections.Count > 0 Then
				operations = operations Or Client.RemoteConnections(0).Operations
			End If

			If (operations And NBiometricOperations.ListIds) = NBiometricOperations.ListIds Then
				tbId.AutoCompleteCustomSource.AddRange(Client.ListIds())
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub TbIdKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tbId.KeyDown
		If e.KeyCode = Keys.Enter Then
			btnOk.PerformClick()
		End If
	End Sub

#End Region
End Class
