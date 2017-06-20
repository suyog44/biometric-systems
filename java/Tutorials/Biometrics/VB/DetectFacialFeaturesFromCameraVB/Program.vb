Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.ComponentModel
Imports Neurotec.Devices
Imports Neurotec.Licensing
Imports Neurotec.Plugins

Friend Class Program
	Private Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [FaceTemplate] [FaceImage] [TokenFaceImage]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [FaceTemplate] [FaceImage] [TokenFaceImage] [-u url](optional)", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [FaceTemplate] [FaceImage] [TokenFaceImage] [-f filename](optional)", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[FaceTemplate] - filename for template.")
		Console.WriteLine(Constants.vbTab & "[FaceImage] - filename for face image.")
		Console.WriteLine(Constants.vbTab & "[TokenFaceImage] - filename for token face image.")
		Console.WriteLine(Constants.vbTab & "[-u url] - (optional) url to RTSP stream")
		Console.WriteLine(Constants.vbTab & "[-f filename] - (optional) video file containing a face")
		Console.WriteLine(Constants.vbTab & "If url(-u) or filename(-f) attribute is not specified first attached camera will be used")
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceDetection,Biometrics.FaceExtraction,Devices.Cameras," & "Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 3 AndAlso args.Length <> 5 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", components))
			End If
			If NLicense.ObtainComponents("/local", 5000, AdditionalComponents) Then
				components &= "," & AdditionalComponents
			End If

			Using biometricClient = New NBiometricClient With {.UseDeviceManager = True, .BiometricTypes = NBiometricType.Face}
				Using subject = New NSubject()
					Using face = New NFace With {.CaptureOptions = NBiometricCaptureOptions.Stream}
						biometricClient.Initialize()

						' Create camera from filename or RTSP stram if attached
						Dim camera As NCamera
						Dim deviceManager = biometricClient.DeviceManager
						If args.Length = 5 Then
							camera = CType(ConnectDevice(deviceManager, args(4), args(3).Equals("-u")), NCamera)

						Else
							' Get count of connected devices
							Dim count As Integer = deviceManager.Devices.Count

							If count = 0 Then
								Console.WriteLine("no cameras found, exiting ..." & Constants.vbLf)
								Return -1
							End If

							'select the first available camera
							camera = CType(deviceManager.Devices(0), NCamera)
						End If
						biometricClient.FaceCaptureDevice = camera

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

						biometricClient.FacesTemplateSize = NTemplateSize.Large

						subject.Faces.Add(face)

						Dim task = biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.DetectSegments Or NBiometricOperations.Segment Or NBiometricOperations.AssessQuality, subject)

						Console.Write("Starting to capture. Please look into the camera... ")
						biometricClient.PerformTask(task)
						Console.WriteLine("Done.")

						If task.Status = NBiometricStatus.Ok Then
							' print face attributes
							PrintFaceAttributes(face)
							' template
							File.WriteAllBytes(args(0), subject.GetTemplateBuffer().ToArray())
							' original face
							face.Image.Save(args(1))
							' token face image
							subject.Faces(1).Image.Save(args(2))
						Else
							Console.WriteLine("Capturing failed! Status: {0}", task.Status)
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

	Private Shared Sub PrintFaceAttributes(ByVal face As NFace)
		For Each attributes In face.Objects
			Console.WriteLine(Constants.vbTab & "location = ({0}, {1}), width = {2}, height = {3}", attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height)

			PrintNleFeaturePoint("LeftEyeCenter", attributes.LeftEyeCenter)
			PrintNleFeaturePoint("RightEyeCenter", attributes.RightEyeCenter)

			If NLicense.IsComponentActivated(AdditionalComponents) Then
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
			End If
			Console.WriteLine()
		Next attributes
	End Sub

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

	Private Shared Function ConnectDevice(ByVal deviceManager As NDeviceManager, ByVal url As String, ByVal isUrl As Boolean) As NDevice
		Dim plugin As NPlugin = NDeviceManager.PluginManager.Plugins("Media")
		If plugin.State = NPluginState.Plugged AndAlso NDeviceManager.IsConnectToDeviceSupported(plugin) Then
			Dim parameters() As NParameterDescriptor = NDeviceManager.GetConnectToDeviceParameters(plugin)
			Dim bag = New NParameterBag(parameters)
			If isUrl Then
				bag.SetProperty("DisplayName", "IP Camera")
				bag.SetProperty("Url", url)
			Else
				bag.SetProperty("DisplayName", "Video file")
				bag.SetProperty("FileName", url)
			End If
			Return deviceManager.ConnectToDevice(plugin, bag.ToPropertyBag())
		End If
		Throw New Exception("Failed to connect specified device!")
	End Function
End Class
