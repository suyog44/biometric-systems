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
		Const Components As String = "Biometrics.Standards.Faces"

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				MessageBox.Show(String.Format("Could not obtain licenses for components: {0}", Components), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Return
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return
		End Try

		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New MainForm())

		Try
			NLicense.ReleaseComponents(Components)
		Catch
		End Try
	End Sub
End Class
