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
		Console.WriteLine(Constants.vbTab & "[image] - filename of image.")
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceDetection,Biometrics.FaceExtraction"
		Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", components))
			End If
			If NLicense.ObtainComponents("/local", 5000, AdditionalComponents) Then
				components &= "," & AdditionalComponents
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using face = New NFace()
						' Read face image from file and add it to NFace object
						face.FileName = args(0)

						' Read face image from file and add it NSubject
						subject.Faces.Add(face)

						' Detect multiple faces
						subject.IsMultipleSubjects = True

						Dim isAdditionalComponentActivated As Boolean = NLicense.IsComponentActivated(AdditionalComponents)
						If isAdditionalComponentActivated Then
							' Set which features should be detected
							biometricClient.FacesDetectBaseFeaturePoints = True
							biometricClient.FacesDetectAllFeaturePoints = True
							biometricClient.FacesRecognizeEmotion = True
							biometricClient.FacesRecognizeExpression = True
							biometricClient.FacesDetectProperties = True
							biometricClient.FacesDetermineGender = True
							biometricClient.FacesDetermineAge = True
						End If

						' Set template size
						biometricClient.FacesTemplateSize = NTemplateSize.Medium

						' Create segment detection task
						Dim task = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject)

						' Perform task
						biometricClient.PerformTask(task)
						Dim status As NBiometricStatus = task.Status

						' Get detection details of the extracted face
						If status = NBiometricStatus.Ok Then
							For Each attributes In face.Objects
								Console.WriteLine(Constants.vbTab & "location = ({0}, {1}), width = {2}, height = {3}", attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height)

								PrintNleFeaturePoint("LeftEyeCenter", attributes.LeftEyeCenter)
								PrintNleFeaturePoint("RightEyeCenter", attributes.RightEyeCenter)

								If isAdditionalComponentActivated Then
									PrintNleFeaturePoint("MouthCenter", attributes.MouthCenter)
									PrintNleFeaturePoint("NoseTip", attributes.NoseTip)
									Console.WriteLine()
									For Each featurePoint In attributes.FeaturePoints
										PrintBaseFeaturePoint(featurePoint)
									Next featurePoint

									Console.WriteLine()
									If attributes.Age = 254 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Age not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Age: {0}", attributes.Age)
									End If
									If attributes.GenderConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Gender not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Gender: {0}, Confidence: {1}", attributes.Gender, attributes.GenderConfidence)
									End If
									If attributes.ExpressionConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Expression not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Expression: {0}, Confidence: {1}", attributes.Expression, attributes.ExpressionConfidence)
									End If
									If attributes.BlinkConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Blink not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Blink: {0}, Confidence: {1}", (attributes.Properties And NLProperties.Blink) = NLProperties.Blink, attributes.BlinkConfidence)
									End If
									If attributes.MouthOpenConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Mouth open not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Mouth open: {0}, Confidence: {1}", (attributes.Properties And NLProperties.MouthOpen) = NLProperties.MouthOpen, attributes.MouthOpenConfidence)
									End If
									If attributes.GlassesConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Glasses not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Glasses: {0}, Confidence: {1}", (attributes.Properties And NLProperties.Glasses) = NLProperties.Glasses, attributes.GlassesConfidence)
									End If
									If attributes.DarkGlassesConfidence = 255 Then
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Dark glasses not detected")
									Else
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "Dark glasses: {0}, Confidence: {1}", (attributes.Properties And NLProperties.DarkGlasses) = NLProperties.DarkGlasses, attributes.DarkGlassesConfidence)
									End If

									Console.WriteLine()
								End If
							Next attributes
						Else
							Console.WriteLine("Face detection failed! Status = {0}", status)
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
			NLicense.ReleaseComponents(components)
		End Try
	End Function

	Private Shared Sub PrintNleFeaturePoint(ByVal name As String, ByVal point As NLFeaturePoint)
		If point.Confidence = 0 Then
			Console.WriteLine(Constants.vbTab + Constants.vbTab & "{0} feature unavailable. confidence: 0", name)
			Return
		End If
		Console.WriteLine(Constants.vbTab + Constants.vbTab & "{0} feature found. X: {1}, Y: {2}, confidence: {3}", name, point.X, point.Y, point.Confidence)
	End Sub

	Private Shared Sub PrintBaseFeaturePoint(ByVal point As NLFeaturePoint)
		If point.Confidence = 0 Then
			Console.WriteLine(Constants.vbTab + Constants.vbTab & "Base feature point unavailable. confidence: 0")
			Return
		End If
		Console.WriteLine(Constants.vbTab + Constants.vbTab & "Base feature point found. X: {0}, Y: {1}, confidence: {2}, Code: {3}", point.X, point.Y, point.Confidence, point.Code)
	End Sub
End Class
