Imports Neurotec.IO
Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [ANTemplate] [NTemplate]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[ATemplate] - filename of ANTemplate.")
		Console.WriteLine(Constants.vbTab & "[NTemplate] - filename of NTemplate.")
		Console.WriteLine("")
		Console.WriteLine("examples:")
		Console.WriteLine(Constants.vbTab & "{0} antemplate.data nTemplate.data", TutorialUtils.GetAssemblyName())

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.Standards.FingerTemplates"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 2 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Dim aNTemplateFileName As String = args(0)

			' Creating ANTemplate object from file
			Using anTemplate = New ANTemplate(aNTemplateFileName, ANValidationLevel.Standard)
				' Converting ANTemplate object to NTemplate object
				Using nTemplate As NTemplate = anTemplate.ToNTemplate()
					' Packing NTemplate object
					Dim packedNTemplate() As Byte = nTemplate.Save().ToArray()

					' Storing NTemplate object in file
					File.WriteAllBytes(args(1), packedNTemplate)
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
