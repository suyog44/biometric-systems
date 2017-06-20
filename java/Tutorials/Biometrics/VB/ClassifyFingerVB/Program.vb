Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Linq
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "image - image of fingerprint to be classified")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerSegmentsDetection"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Try
			' Obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient()
				' Create subject
				Using subject As NSubject = CreateSubject(args(0), NFPosition.Unknown)
					' Set paramter to classfiy fingeprint
					biometricClient.FingersDeterminePatternClass = True

					' Create task
					Dim task As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject)

					' Perform task
					biometricClient.PerformTask(task)
					If task.Status = NBiometricStatus.Ok Then
						Using result = subject.Fingers.Last()
							Console.WriteLine("Fingerprint pattern class is ""{0}"", confidence {1:f2}", result.Objects(0).PatternClass, result.Objects(0).PatternClassConfidence)
						End Using
					Else
						Console.WriteLine("Calssification failed. Status: {0}", task.Status)
						If task.Error IsNot Nothing Then
							Throw task.Error
						End If
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

	Private Shared Function CreateSubject(ByVal fileName As String, ByVal position As NFPosition) As NSubject
		Dim subject = New NSubject With {.Id = fileName}
		Dim finger = New NFinger With {.FileName = fileName, .Position = position}
		subject.Fingers.Add(finger)
		Return subject
	End Function
End Class
