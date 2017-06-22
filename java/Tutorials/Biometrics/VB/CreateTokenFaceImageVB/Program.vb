Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [FaceImage] [CreateTokenFaceImage]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[FaceImage] - an image containing frontal face.")
		Console.WriteLine(Constants.vbTab & "[CreateTokenFaceImage] - filename of created token face image.")
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment"

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
					Using face = New NFace()
						'Face image will be read from file
						face.FileName = args(0)
						subject.Faces.Add(face)

						'Create segment detection and check the quality of token face image task
						Dim task = biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.AssessQuality, subject)

						'Perform task
						biometricClient.PerformTask(task)

						Dim status As NBiometricStatus = task.Status
						If status = NBiometricStatus.Ok Then
							Using result = subject.Faces(1)
								Using attributes = result.Objects.First()
									Console.WriteLine("global token face image quality score = {0:f3}. Tested attributes details:", attributes.Quality)
									Console.WriteLine(Constants.vbTab & "sharpness score = {0:f3}", attributes.Sharpness)
									Console.WriteLine(Constants.vbTab & "background uniformity score = {0:f3}", attributes.BackgroundUniformity)
									Console.WriteLine(Constants.vbTab & "grayscale density score = {0:f3}", attributes.GrayscaleDensity)
								End Using
								Using image = result.Image
									image.Save(args(1))
								End Using
							End Using
						Else
							Console.WriteLine("Token Face Image creation failed! Status = {0}", status)
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
