Imports Microsoft.VisualBasic
Imports Neurotec.Biometrics.Client
Imports System

Partial Public Class SettingsPageBase
	Inherits Neurotec.Samples.PageBase
	#Region "Public methods"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	Private privateClient As NBiometricClient
	Protected Property Client() As NBiometricClient
		Get
			Return privateClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateClient = value
		End Set
	End Property

	#Region "Public methods"

	Public Overrides Sub OnNavigatedTo(ParamArray ByVal args() As Object)
		If args Is Nothing OrElse args.Length <> 1 Then
			Throw New ArgumentException("args")
		End If
		Client = CType(args(0), NBiometricClient)
		If Client Is Nothing Then
			Throw New ArgumentException()
		End If

		LoadSettings()
		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		SaveSettings()
		Client = Nothing

		MyBase.OnNavigatingFrom()
	End Sub

	Public Overridable Sub LoadSettings()
	End Sub

	Public Overridable Sub SaveSettings()
	End Sub

	Public Overridable Sub DefaultSettings()
		LoadSettings()
	End Sub

	#End Region
End Class
