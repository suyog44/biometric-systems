Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

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
		Dim retry As Boolean
		Dim licenses() As String = {"Biometrics.FingerExtraction", "Biometrics.PalmExtraction", "Biometrics.FaceExtraction", "Biometrics.IrisExtraction", "Biometrics.VoiceExtraction", "Biometrics.FingerMatchingFast", "Biometrics.FingerMatching", "Biometrics.PalmMatchingFast", "Biometrics.PalmMatching", "Biometrics.VoiceMatching", "Biometrics.FaceMatchingFast", "Biometrics.FaceMatching", "Biometrics.IrisMatchingFast", "Biometrics.IrisMatching", "Biometrics.FingerQualityAssessment", "Biometrics.FingerSegmentation", "Biometrics.FingerSegmentsDetection", "Biometrics.PalmSegmentation", "Biometrics.FaceSegmentation", "Biometrics.IrisSegmentation", "Biometrics.VoiceSegmentation", "Biometrics.Standards.Fingers", "Biometrics.Standards.FingerTemplates", "Biometrics.Standards.Faces", "Biometrics.Standards.Irises", "Devices.Cameras", "Devices.FingerScanners", "Devices.IrisScanners", "Devices.PalmScanners", "Devices.Microphones", "Images.WSQ", "Media"}

		Do
			Try
				retry = False
				For Each license As String In licenses
					Licensing.NLicense.ObtainComponents(Address, Port, license)
				Next license
			Catch ex As Exception
				Dim message As String = String.Format("Failed to obtain licenses for components." & Constants.vbLf & "Error message: {0}", ex.Message)
				If TypeOf ex Is System.IO.IOException Then
					message &= Constants.vbLf & "(Probably licensing service is not running. Use Activation Wizard to figure it out.)"
				End If
				If MessageBox.Show(message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry Then
					retry = True
				Else
					retry = False
					Return
				End If
			End Try
		Loop While retry

		Try
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New MainForm())
		Finally
			For Each license As String In licenses
				Licensing.NLicense.ReleaseComponents(license)
			Next license
		End Try
	End Sub
End Class
