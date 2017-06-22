Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [probe image] [one or more gallery images]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.PalmExtraction,Biometrics.PalmMatching"

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			' Obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient()
				' Create probe template
				Using probeSubject As NSubject = CreateSubject(args(0), "ProbeSubject")
					Dim status As NBiometricStatus = biometricClient.CreateTemplate(probeSubject)
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Failed to create probe template. Status: {0}.", status)
						Return -1
					End If

					' Create gallery templates and enroll them
					Dim enrollTask As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Enroll, Nothing)
					For i As Integer = 1 To args.Length - 1
						enrollTask.Subjects.Add(CreateSubject(args(i), String.Format("GallerySubject_{0}", i)))
					Next i
					biometricClient.PerformTask(enrollTask)
					status = enrollTask.Status
					If status <> NBiometricStatus.Ok Then
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status)
						If enrollTask.Error IsNot Nothing Then
							Throw enrollTask.Error
						End If
						Return -1
					End If

					' Set matching threshold
					biometricClient.MatchingThreshold = 48

					' Set matching speed
					biometricClient.PalmsMatchingSpeed = NMatchingSpeed.Low

					' Identify probe subject
					status = biometricClient.Identify(probeSubject)
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
		Dim palm = New NPalm With {.FileName = fileName}
		subject.Palms.Add(palm)
		subject.Id = subjectId
		Return subject
	End Function
End Class
