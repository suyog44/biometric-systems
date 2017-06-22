Imports Microsoft.VisualBasic
Imports System

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "image - image of fingerprint to be evaluated")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerQualityAssessmentBase"

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
				Using subject = New NSubject()
					Using finger = New NFinger()
						' Read finger image from file and add it to NFinger object
						finger.FileName = args(0)

						' Read finger image from file and add it NSubject
						subject.Fingers.Add(finger)

						' Set parameter to calculate NFIQ value
						biometricClient.FingersCalculateNfiq = True

						' Create Quality Assesment task
						Dim task As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.AssessQuality, subject)

						'Perform task
						biometricClient.PerformTask(task)
						Dim status As NBiometricStatus = task.Status
						If status = NBiometricStatus.Ok Then
							Console.WriteLine("Finger quality is: {0}", subject.Fingers(0).Objects(0).NfiqQuality)
						Else
							Console.WriteLine("Quality assesment failed: {0}", status)
							If task.Error IsNot Nothing Then
								Throw task.Error
							End If
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
