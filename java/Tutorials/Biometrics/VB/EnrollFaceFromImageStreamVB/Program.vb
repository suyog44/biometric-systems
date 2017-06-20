Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing
Imports Neurotec.Media
Imports Neurotec.Images

Friend Class Program
	Private Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"
	Private Const MaxFrameCount As Integer = 100

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [output template] [-u url]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [output template] [-f filename]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [output template] [-d directory]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[-u url] - url to RTSP stream")
		Console.WriteLine(Constants.vbTab & "[-f filename] -  video file containing a face")
		Console.WriteLine(Constants.vbTab & "[-d directory] - directory containing face images")

		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "example: {0} template.dat -f video.avi", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "example: {0} template.dat -u rtsp://camera_url", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "example: {0} template.dat -d C:" & Constants.vbTab & "emplates", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceExtraction"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length <> 3 Then
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
					Using face = New NFace With {.HasMoreSamples = True}
						subject.Faces.Add(face)

						'Set face template size (recommended, for enroll to database, is large) (optional)
						biometricClient.FacesTemplateSize = NTemplateSize.Large

						' Detect all faces features
						Dim isAdditionalComponentActivated As Boolean = NLicense.IsComponentActivated(AdditionalComponents)
						biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated

						' Create NMedia reader or prepare to use a gallery
						Dim reader As NMediaReader = Nothing
						Dim files() As String = Nothing
						Select Case args(1)
							Case "-f"
								reader = New NMediaReader(NMediaSource.FromFile(args(2)), NMediaType.Video, True)
							Case "-u"
								reader = New NMediaReader(NMediaSource.FromUrl(args(2)), NMediaType.Video, True)
							Case "-d"
								files = Directory.GetFiles(args(2))
							Case Else
								Throw New ArgumentException("Unknown input options specified!")
						End Select

						' Start extraction from stream
						Dim isReaderUsed = reader IsNot Nothing
						Dim i As Integer = 0
						Dim status = NBiometricStatus.None
						Dim image As NImage = Nothing
						Dim task = biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject)

						If isReaderUsed Then
							reader.Start()
						End If
						Do While status = NBiometricStatus.None
							If isReaderUsed Then
								image = reader.ReadVideoSample()
							Else
								image = If(i < files.Length, NImage.FromFile(files(i)), Nothing)
								i += 1
							End If
							If image Is Nothing Then
								Exit Do
							End If
							face.Image = image
							biometricClient.PerformTask(task)
							If task.Error IsNot Nothing Then
								Throw task.Error
							End If
							status = task.Status
							image.Dispose()
						Loop
						If isReaderUsed Then
							reader.Stop()
						End If

						' Reset HasMoreSamples value since we finished loading images
						face.HasMoreSamples = False

						' If loading was finished because MeadiaReaded had no more images we have to
						' finalize extraction by performing task after setting HasMoreSamples to false
						If image Is Nothing Then
							biometricClient.PerformTask(task)
							status = task.Status
						End If

						' Return extraction results
						If status = NBiometricStatus.Ok Then
							' Get detection details if the face was detected
							For Each attributes In face.Objects
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

							Console.WriteLine("Template extracted")
							' Save compressed template to file
							File.WriteAllBytes(args(0), subject.GetTemplateBuffer().ToArray())
							Console.WriteLine("Template saved successfully")
						Else
							Console.WriteLine("Extraction failed: {0}", status)
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
End Class
