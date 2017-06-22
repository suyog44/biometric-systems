Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Licensing
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [source] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[source] - filename of audio file to extract.")
		Console.WriteLine(Constants.vbTab & "[template] - filename to store sound template.")
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "example: {0} audio.wav template.dat", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Media,Biometrics.VoiceExtraction"

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
					Using voice = New NVoice()
						'Read voice image from file and add it to NFinger object
						voice.FileName = args(0)

						'Read voice from file and add it NSubject
						subject.Voices.Add(voice)

						'Create template from added voice file
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
