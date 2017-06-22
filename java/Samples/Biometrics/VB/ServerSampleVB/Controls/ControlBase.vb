Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Neurotec.Biometrics.Client

Partial Public Class ControlBase
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Public properties"

	Private privateTemplateLoader As ITemplateLoader
	Public Overridable Property TemplateLoader() As ITemplateLoader
		Get
			Return privateTemplateLoader
		End Get
		Set(ByVal value As ITemplateLoader)
			privateTemplateLoader = value
		End Set
	End Property

	Private privateAccelerator As AcceleratorConnection
	Public Overridable Property Accelerator() As AcceleratorConnection
		Get
			Return privateAccelerator
		End Get
		Set(ByVal value As AcceleratorConnection)
			privateAccelerator = value
		End Set
	End Property

	Private privateBiometricClient As NBiometricClient
	Public Overridable Property BiometricClient() As NBiometricClient
		Get
			Return privateBiometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateBiometricClient = value
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Overridable ReadOnly Property IsBusy() As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable Sub Cancel()
	End Sub

	Public Overridable Function GetTitle() As String
		Return String.Empty
	End Function

	Public Function GetTemplateCount() As Integer
		Dim args As RunWorkerCompletedEventArgs = LongTaskForm.RunLongTask("Calculating template count", AddressOf GetTemplateCountDoWork, Nothing)
		Return CInt(Fix(args.Result))
	End Function

#End Region

#Region "Private methods"

	Private Sub GetTemplateCountDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
		If TemplateLoader IsNot Nothing Then
			e.Result = TemplateLoader.TemplateCount
		Else
			e.Result = -1
		End If
	End Sub

#End Region
End Class
