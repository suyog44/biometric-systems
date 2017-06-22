Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Licensing

Friend NotInheritable Class Program
	''' <summary>
	''' The main entry point for the application.
	''' </summary>
	Private Sub New()
	End Sub
	<STAThread()> _
	Shared Sub Main()
		Const Address As String = "/local"
		Const Port As String = "5000"
		Dim components() As String = {"Biometrics.Standards.Base", "Biometrics.Standards.Fingers", "Biometrics.Standards.FingerTemplates", "Biometrics.Standards.Palms", "Biometrics.Standards.PalmTemplates", "Biometrics.Standards.Irises", "Biometrics.Standards.Faces", "Biometrics.Standards.FingerCardTemplates", "Biometrics.Standards.Other", "Images.LosslessJPEG", "Images.JPEG2000"}

		Try
			Dim obtainedComponents As Integer = 0
			For Each component In components
				If NLicense.ObtainComponents(Address, Port, component) Then
					obtainedComponents += 1
				End If
			Next component
			If obtainedComponents = 0 Then
				Throw New NotActivatedException("No licenses obtained!")
			End If
		Catch ex As Exception
			Dim message As String = String.Format("Failed to obtain licenses for components." & Constants.vbLf & "Error message: {0}", ex.Message)
			If TypeOf ex Is System.IO.IOException Then
				message &= Constants.vbLf & "(Probably licensing service is not running. Use Activation Wizard to figure it out.)"
			End If
			MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return
		End Try

		Try
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New MainForm())
		Finally
			For Each component In components
				NLicense.ReleaseComponents(component)
			Next component
		End Try
	End Sub
End Class
