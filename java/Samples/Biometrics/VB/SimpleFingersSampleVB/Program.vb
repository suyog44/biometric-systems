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
		Const Components As String = "Biometrics.FingerExtraction,Biometrics.FingerMatching,Devices.FingerScanners,Images.WSQ,Biometrics.FingerSegmentation,Biometrics.FingerQualityAssessmentBase"
		Try
			For Each component As String In Components.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
				NLicense.ObtainComponents(LicensePanel.Address, LicensePanel.Port, component)
			Next component

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New MainForm())
		Catch ex As Exception
			Utils.ShowException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Sub
End Class
