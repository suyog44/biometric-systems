Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Samples.Forms

Friend NotInheritable Class Program
	''' <summary>
	''' The main entry point for the application.
	''' </summary>
	Private Sub New()
	End Sub
	<STAThread> _
	Shared Sub Main()
		Const Address As String = "/local"
		Const Port As String = "5000"
		Const Components As String = "Biometrics.FingerExtraction,Biometrics.FingerSegmentation"
		Const OptionalComponents As String = "Biometrics.FingerQualityAssessmentBase,Devices.Cameras"

		Try
			If (Not Licensing.NLicense.ObtainComponents(Address, Port, Components)) Then
				Utilities.ShowWarning("Could not obtain licenses for components: {0}", Components)
				Return
			End If

			Licensing.NLicense.ObtainComponents(Address, Port, OptionalComponents)
		Catch ex As Exception
			Dim message As String = String.Format("Failed to obtain licenses for components." & Constants.vbLf & "Error message: {0}", ex.Message)
			If TypeOf ex Is System.IO.IOException Then
				message &= Constants.vbLf & "(Probably licensing service is not running. Use Activation Wizard to figure it out.)"
			End If
			Utilities.ShowWarning(message)
			Return
		End Try

		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New MainForm())
	End Sub
End Class
