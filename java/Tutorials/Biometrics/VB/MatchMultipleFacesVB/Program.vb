Imports System
Imports System.Globalization
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine("	{0} [reference_face_image] [multiple_faces_image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine("	[reference_face_image]  - filename of image with a single (reference) face.")
		Console.WriteLine("	[multiple_faces_image]  - filename of image with multiple faces.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FaceExtraction,Biometrics.FaceMatching"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			' obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient()
				' Create subjects with face object
				Using referenceSubject As NSubject = CreateSubject(args(0), False)
					Using candidateSubject As NSubject = CreateSubject(args(1), True)
						' Create reference subject template
						Dim status As NBiometricStatus = biometricClient.CreateTemplate(referenceSubject)
						If status <> NBiometricStatus.Ok Then
							Console.WriteLine("Template creation was unsuccessful. Status: {0}.", status)
							Return -1
						End If

						' Create candidate subjects templates
						status = biometricClient.CreateTemplate(candidateSubject)
						If status <> NBiometricStatus.Ok Then
							Console.WriteLine("Template creation was unsuccessful. Status: {0}.", status)
							Return -1
						End If

						' Set ids to candidate subjects and related subjects
						Dim i As Integer = 1
						candidateSubject.Id = "GallerySubject_0"
						For Each subject In candidateSubject.RelatedSubjects
							subject.Id = String.Format("GallerySubject_{0}", i)
							i += 1
						Next subject

						' Create enrolment task
						Dim enrollTask As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Enroll, Nothing)

						' Add subject and related subjects to enrollment task
						enrollTask.Subjects.Add(candidateSubject)
						For Each subject In candidateSubject.RelatedSubjects
							enrollTask.Subjects.Add(subject)
						Next subject

						' Enroll candidate subjects and related subject
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
						biometricClient.FacesMatchingSpeed = NMatchingSpeed.Low

						' Identify probe subject
						status = biometricClient.Identify(referenceSubject)
						If status = NBiometricStatus.Ok Then
							For Each matchingResult In referenceSubject.MatchingResults
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
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(Components)
		End Try
	End Function

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal isMultipleSubjects As Boolean) As NSubject
		Dim subject = New NSubject With {.IsMultipleSubjects = isMultipleSubjects}
		Dim face = New NFace With {.FileName = fileName}
		subject.Faces.Add(face)
		Return subject
	End Function
End Class
