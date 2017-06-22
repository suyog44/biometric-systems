
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
		Const Components As String = "Biometrics.FaceExtraction,Biometrics.FaceMatching,Biometrics.FaceDetection,Devices.Cameras,Biometrics.FaceSegmentsDetection"
		Try
			For Each component As String In Components.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
				NLicense.ObtainComponents(Address, Port, component)
			Next

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
