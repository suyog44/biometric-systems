Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [reference image] [candidate image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerExtraction,Biometrics.FingerMatching"

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
				' Create subjects with finger object
				Using referenceSubject As NSubject = CreateSubject(args(0), args(0))
					Using candidateSubject As NSubject = CreateSubject(args(1), args(1))
						' Set matching threshold
						biometricClient.MatchingThreshold = 48

						' Set matching speed
						biometricClient.FingersMatchingSpeed = NMatchingSpeed.Low

						' Verify subjects
						Dim status As NBiometricStatus = biometricClient.Verify(referenceSubject, candidateSubject)
						If status = NBiometricStatus.Ok OrElse status = NBiometricStatus.MatchNotFound Then
							Dim score As Integer = referenceSubject.MatchingResults(0).Score
							Console.Write("image scored {0}, verification.. ", score)
							Console.WriteLine(If(status = NBiometricStatus.Ok, "succeeded", "failed"))
						Else
							Console.Write("Verification failed. Status: {0}", status)
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
		Dim subject = New NSubject With {.Id = subjectId}
		Dim finger = New NFinger With {.FileName = fileName}
		subject.Fingers.Add(finger)
		Return subject
	End Function
End Class
