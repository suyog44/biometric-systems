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
		Console.WriteLine(Constants.vbTab & "{0} [probe voice] [one or more gallery voices]", TutorialUtils.GetAssemblyName())
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
			' obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using client = New NBiometricClient()
				Using probeSubject = CreateSubject(args(0), "ProbeSubject")
					' creating  probe subjects template
					Dim status As NBiometricStatus = client.CreateTemplate(probeSubject)
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Failed to create probe template. Status: {0}.", status)
						Return -1
					End If

					' creating task for enrollment
					Dim task As NBiometricTask = client.CreateTask(NBiometricOperations.Enroll, Nothing)
					For i As Integer = 1 To args.Length - 1
						task.Subjects.Add(CreateSubject(args(i), String.Format("GallerySubject_{0}", i)))
					Next i

					' perform enrollment task
					client.PerformTask(task)
					status = task.Status
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status)
						If task.Error IsNot Nothing Then
							Throw task.Error
						End If
						Return -1
					End If

					' Set matching threshold
					client.MatchingThreshold = 48

					' Identify probe subject
					status = client.Identify(probeSubject)

					If status = NBiometricStatus.Ok Then
						For Each matchingResult In probeSubject.MatchingResults
							Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score)
						Next matchingResult
					ElseIf status = NBiometricStatus.MatchNotFound Then
						Console.WriteLine("Match not found!")
					Else
						Console.WriteLine("Matching failed! Status: {0}", status)
						Return -1
					End If
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
