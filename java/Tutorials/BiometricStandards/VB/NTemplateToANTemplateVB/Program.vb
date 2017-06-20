Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [NTemplate] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[NTemplate]     - filename of NTemplate.")
		Console.WriteLine(Constants.vbTab & "[ANTemplate]    - filename of ANTemplate.")
		Console.WriteLine(Constants.vbTab & "[Tot] - specifies type of transaction.")
		Console.WriteLine(Constants.vbTab & "[Dai] - specifies destination agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Ori] - specifies originating agency identifier.")
		Console.WriteLine(Constants.vbTab & "[Tcn] - specifies transaction control number.")
		Console.WriteLine("")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		' Depending on NTemplate contents choose the licenses: if you will have only finger templates in NTemplate - leave finger templates license only.
		Const Components As String = "Biometrics.Standards.FingerTemplates,Biometrics.Standards.PalmTemplates"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 6 Then
			Return Usage()
		End If

		If args(0) = "/?" OrElse args(0) = "help" Then
			Return Usage()
		End If

		Try
			Dim nTemplateFileName As String = args(0)

			Dim tot As String = args(2)	' type of transaction
			Dim dai As String = args(3)	' destination agency identifier
			Dim ori As String = args(4)	' originating agency identifier
			Dim tcn As String = args(5)	' transaction control number

			If (tot.Length < 3) OrElse (tot.Length > 4) Then
				Console.WriteLine("Tot parameter should be 3 or 4 characters length.")
				Return -1
			End If

			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim packedNTemplate() As Byte = File.ReadAllBytes(nTemplateFileName)

			' Creating NTemplate object from packed NTemplate
			Using nTemplate = New NTemplate(packedNTemplate)
				' Creating ANTemplate object from NTemplate object
				Using tempANTemplate = New ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, True, nTemplate)
					' Storing ANTemplate object in file
					tempANTemplate.Save(args(1))
					Console.WriteLine("Program produced file: " & args(1))
				End Using
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function
End Class
