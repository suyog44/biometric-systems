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
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [image] [template] [-u url](optional)", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [image] [template] [-f filename](optional)", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[image]  - image filename to store face image.")
		Console.WriteLine(Constants.vbTab & "[template] - filename to store face template.")
		Console.WriteLine(Constants.vbTab & "[-u url] - (optional) url to RTSP stream")
		Console.WriteLine(Constants.vbTab & "[-f filename] - (optional) video file containing a face")
		Console.WriteLine(Constants.vbTab & "If url(-u) or filename(-f) attribute is not specified first attached camera will be used")
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceExtraction,Devices.Cameras"
		Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 2 AndAlso args.Length <> 4 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", components))
			End If
			If NLicense.ObtainComponents("/local", 5000, AdditionalComponents) Then
				components &= "," & AdditionalComponents
			End If

			Using biometricClient = New NBiometricClient With {.UseDeviceManager = True}
				Using deviceManager = biometricClient.DeviceManager
					Using subject = New NSubject()
						Using face = New NFace()
							' Set type of the device used
							deviceManager.DeviceTypes = NDeviceType.Camera

							' Initialize the NDeviceManager
							deviceManager.Initialize()

							' Create camera from filename or RTSP stram if attached
							Dim camera As NCamera
							If args.Length = 4 Then
								camera = CType(ConnectDevice(deviceManager, args(3), args(2).Equals("-u")), NCamera)

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

							' Set the selected camera as NBiometricClient Face Capturing Device
							biometricClient.FaceCaptureDevice = camera

							Console.WriteLine("Capturing from {0}. Please turn camera to face.", biometricClient.FaceCaptureDevice.DisplayName)

							' Define that the face source will be a stream
							face.CaptureOptions = NBiometricCaptureOptions.Stream

							' Add NFace to NSubject
							subject.Faces.Add(face)

							' Set face template size (recommended, for enroll to database, is large) (optional)
							biometricClient.FacesTemplateSize = NTemplateSize.Large

							' Detect all faces features
							Dim isAdditionalComponentActivated As Boolean = NLicense.IsComponentActivated(AdditionalComponents)
							biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated

							' Start capturing
							Dim status As NBiometricStatus = biometricClient.Capture(subject)
							If status = NBiometricStatus.Ok Then
								Console.WriteLine("Capturing succeeded")
							Else
								Console.WriteLine("Failed to capture: {0}", status)
								Return -1
							End If

							' Get face detection details if face was detected
							For Each nface In subject.Faces
								For Each attributes In nface.Objects
									Console.WriteLine("face:")
									Console.WriteLine(Constants.vbTab & "location = ({0}, {1}), width = {2}, height = {3}", attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height)
									If attributes.RightEyeCenter.Confidence > 0 OrElse attributes.LeftEyeCenter.Confidence > 0 Then
										Console.WriteLine(Constants.vbTab & "found eyes:")
										If attributes.RightEyeCenter.Confidence > 0 Then
											Console.WriteLine(Constants.vbTab + Constants.vbTab & "Right: location = ({0}, {1}), confidence = {2}", attributes.RightEyeCenter.X, attributes.RightEyeCenter.Y, attributes.RightEyeCenter.Confidence)
										End If
										If attributes.LeftEyeCenter.Confidence > 0 Then
											Console.WriteLine(Constants.vbTab + Constants.vbTab & "Left: location = ({0}, {1}), confidence = {2}", attributes.LeftEyeCenter.X, attributes.LeftEyeCenter.Y, attributes.LeftEyeCenter.Confidence)
										End If
									End If
									If isAdditionalComponentActivated AndAlso attributes.NoseTip.Confidence > 0 Then
										Console.WriteLine(Constants.vbTab & "found nose:")
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "location = ({0}, {1}), confidence = {2}", attributes.NoseTip.X, attributes.NoseTip.Y, attributes.NoseTip.Confidence)
									End If
									If isAdditionalComponentActivated AndAlso attributes.MouthCenter.Confidence > 0 Then
										Console.WriteLine(Constants.vbTab & "found mouth:")
										Console.WriteLine(Constants.vbTab + Constants.vbTab & "location = ({0}, {1}), confidence = {2}", attributes.MouthCenter.X, attributes.MouthCenter.Y, attributes.MouthCenter.Confidence)
									End If
								Next attributes
							Next nface

							' Save image to file
							Using image = subject.Faces(0).Image
								image.Save(args(0))
								Console.WriteLine("image saved successfully")
							End Using

							' Save template to file
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("template saved successfully")
						End Using
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
