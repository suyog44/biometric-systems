Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Licensing
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Friend NotInheritable Class Program
	Private Sub New()
	End Sub
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [input image] [output template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.PalmExtraction"

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
					Using palm = New NPalm()
						'Read palm image from file and add it to NPalm object
						palm.FileName = args(0)

						'Read palm image from file and add it NSubject
						subject.Palms.Add(palm)

						'Set palm template size (recommended, for enroll to database, is large) (optional)
						biometricClient.PalmsTemplateSize = NTemplateSize.Large

						'Create template from added finger image
						Dim status = biometricClient.CreateTemplate(subject)
						If status = NBiometricStatus.Ok Then
							Console.WriteLine("Template extracted")

							' save compressed template to file
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("template saved successfully")
						Else
							Console.WriteLine("Extraction failed: {0}", status)
							Return -1
						End If
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
