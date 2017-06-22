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
	<STAThread> _
	Shared Sub Main()
		Const Components As String = "Biometrics.FingerExtraction,Images.Processing.FFT"

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Utilities.ShowWarning("Could not obtain licenses for any of components: {0}", Components)
				Return
			End If

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New MainForm())
		Catch ex As Exception
			MessageBox.Show(String.Format("Unhandled exception. Details: {0}", ex), "LatentFingerprintSample", MessageBoxButtons.OK, MessageBoxIcon.Error)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Sub
End Class
