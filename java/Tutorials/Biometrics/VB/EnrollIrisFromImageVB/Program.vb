Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[image]    - filename of image.")
		Console.WriteLine(Constants.vbTab & "[template] - filename for template.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.IrisExtraction"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using iris = New NIris()
						' Read iris image from file and add it to NIris object
						iris.FileName = args(0)

						' Read iris image from file and add it NSubject
						subject.Irises.Add(iris)

						' Set iris template size (recommended, for enroll to database, is large) (optional)
						biometricClient.IrisesTemplateSize = NTemplateSize.Large

						' Create template from added iris image
						Dim status = biometricClient.CreateTemplate(subject)
						If status = NBiometricStatus.Ok Then
							Console.WriteLine("Template extracted")

							' save compressed template to file
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("template saved successfully")
						Else
							Console.WriteLine(String.Format("Extraction failed: {0}", status))
							Return -1
						End If
						Console.WriteLine(If(status = NBiometricStatus.Ok, "Template extracted", String.Format("Extraction failed: {0}", status)))
					End Using
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
