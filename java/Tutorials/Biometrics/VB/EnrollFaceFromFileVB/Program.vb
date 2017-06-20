Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [input file] [output template] [still image or video file]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[input file]  - image filename or video file with face.")
		Console.WriteLine(Constants.vbTab & "[template] - filename to store face template.")
		Console.WriteLine(Constants.vbTab & "[still image or video file] - specifies that passed source parameter is image (value: 0) or video (value: 1)")
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "example: {0} image.jpg template.dat 0", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "example: {0} video.avi template.dat 1", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceExtraction"
		Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 3 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", components))
			End If
			If NLicense.ObtainComponents("/local", 5000, AdditionalComponents) Then
				components &= "," & AdditionalComponents
			End If

			Dim isVideo As Boolean = False

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using face = New NFace()
						' Read face image from file and add it to NFace object
						face.FileName = args(0)

						If args.Length > 2 Then
							isVideo = args(2) = "1"
						End If

						'define that the face source will be a stream
						face.CaptureOptions = If(isVideo, NBiometricCaptureOptions.Stream, NBiometricCaptureOptions.None)

						' Read face image from file and add it NSubject
						subject.Faces.Add(face)

						'Set face template size (recommended, for enroll to database, is large) (optional)
						biometricClient.FacesTemplateSize = NTemplateSize.Large

						' Detect all faces features
						Dim isAdditionalComponentActivated As Boolean = NLicense.IsComponentActivated(AdditionalComponents)
						biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated

						' Create template from added face image
						Dim status = biometricClient.CreateTemplate(subject)
						If status = NBiometricStatus.Ok Then
							Console.WriteLine("Template extracted")
							' Save compressed template to file
							File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("template saved successfully")
						Else
							Console.WriteLine("Extraction failed: {0}", status)
							Return -1
						End If

						' Get detection details if the face was detected
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
End Class
