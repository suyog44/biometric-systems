Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Licensing
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Friend NotInheritable Class Program
	Private Sub New()
	End Sub
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [reference voice file] [candidate voice file]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.VoiceExtraction,Biometrics.VoiceMatching"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using client = New NBiometricClient()
				Using referenceSubject = CreateSubject(args(0), "ReferenceSubject")
					Using candidateSubject = CreateSubject(args(1), "CandidateSubject")
						' creating reference subject template
						Dim status As NBiometricStatus = client.CreateTemplate(referenceSubject)
						If status <> NBiometricStatus.Ok Then
							Console.WriteLine("Reference template creation failed! status: {0}", status)
							Return -1
						End If

						' creating candidate subject template
						status = client.CreateTemplate(candidateSubject)
						If status <> NBiometricStatus.Ok Then
							Console.WriteLine("Candidate template creation failed! status: {0}", status)
							Return -1
						End If

						' verifying subjects
						status = client.Verify(referenceSubject, candidateSubject)
						If status = NBiometricStatus.Ok OrElse status = NBiometricStatus.MatchNotFound Then
							' matching score
							Dim score As Integer = referenceSubject.MatchingResults(0).Score
							Console.Write("Matching score: {0}, verification ", score)
							Console.WriteLine(If(status = NBiometricStatus.Ok, "succeeded", "failed"))
						Else
							Console.WriteLine("Verification failed! status: {0}", status)
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

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal subjectId As String) As NSubject
		Dim subject = New NSubject()
		Dim voice = New NVoice With {.FileName = fileName}
		subject.Voices.Add(voice)
		subject.Id = subjectId
		Return subject
	End Function
End Class
