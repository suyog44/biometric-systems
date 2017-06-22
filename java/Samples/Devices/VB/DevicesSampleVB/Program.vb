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
        Dim components() As String = {"Biometrics.FingerDetection", "Biometrics.PalmDetection", "Devices.FingerScanners", "Devices.PalmScanners", "Devices.Cameras", "Biometrics.IrisDetection", "Devices.IrisScanners", "Devices.Microphones"}

		Try
			Dim anyObtained As Boolean = False
			For Each component As String In components
				anyObtained = anyObtained Or NLicense.ObtainComponents("/local", 5000, component)
			Next component
			If (Not anyObtained) Then
				MessageBox.Show(String.Format("Could not obtain licenses for any of components: {0}", components))
				Return
			End If

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New MainForm())
		Catch ex As Exception
			MessageBox.Show(ex.ToString())
		Finally
			For Each component As String In components
				NLicense.ReleaseComponents(component)
			Next component
		End Try
	End Sub
End Class
