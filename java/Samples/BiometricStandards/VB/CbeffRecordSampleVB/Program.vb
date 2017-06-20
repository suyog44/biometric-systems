Imports System
Imports System.Windows.Forms
Imports Neurotec.Licensing

Friend NotInheritable Class Program
	Private Sub New()
	End Sub

	Private Shared ReadOnly Components() As String = {"Biometrics.Standards.Base", "Biometrics.Standards.Irises", "Biometrics.Standards.Fingers", "Biometrics.Standards.Faces"}

	<STAThread()> _
	Shared Sub Main()
		Dim obtainedComponentCounter As Integer = 0
		Try
			For Each component As String In Components
				If (NLicense.ObtainComponents("/local", 5000, component)) Then
					obtainedComponentCounter += 1
				End If
			Next component
			If obtainedComponentCounter = 0 Then
				Throw New NotActivatedException("Could not obtain any of components.")
			End If
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return
		End Try

		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)
		Application.Run(New MainForm())

		Try
			For Each component As String In Components
				NLicense.ReleaseComponents(component)
			Next component
		Catch
		End Try
	End Sub
End Class
