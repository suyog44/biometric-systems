Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing
Imports Neurotec.Media
Imports Neurotec.Images

Friend Class Program
	Private Const MaxFrameCount As Integer = 100
	Private Const AdditionalComponents As String = "Biometrics.FaceSegmentsDetection"

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [-u url]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [-f filename]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "{0} [-d directory]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[-u url] - url to RTSP stream")
		Console.WriteLine(Constants.vbTab & "[-f filename] -  video file containing a face")
		Console.WriteLine(Constants.vbTab & "[-d directory] - directory containing face images")
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Dim components As String = "Biometrics.FaceDetection"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
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

						'Set face template size (optional)
						biometricClient.FacesTemplateSize = NTemplateSize.Medium

						Dim isAdditionalComponentActivated As Boolean = NLicense.IsComponentActivated(AdditionalComponents)
						If isAdditionalComponentActivated Then
							' Set which features should be detected
							biometricClient.FacesDetectBaseFeaturePoints = True
						End If

						' Create NMedia reader or prepare to use a gallery
						Dim reader As NMediaReader = Nothing
						Dim files() As String = Nothing
						Select Case args(0)
							Case "-f"
								reader = New NMediaReader(NMediaSource.FromFile(args(1)), NMediaType.Video, True)
							Case "-u"
								reader = New NMediaReader(NMediaSource.FromUrl(args(1)), NMediaType.Video, True)
							Case "-d"
								files = Directory.GetFiles(args(1))
							Case Else
								Throw New ArgumentException("Unknown input options specified!")
						End Select

						' Set from how many frames to detect
						Dim isReaderUsed As Boolean = reader IsNot Nothing
						Dim maximumFrames = If(isReaderUsed, MaxFrameCount, files.Length)

						' Create Detection task
						Dim task = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject)

						' Start the reader if gallery is not used
						If isReaderUsed Then
							reader.Start()
						End If

						For i As Integer = 0 To maximumFrames - 1
							' Read from reader otherwise create an image from specified gallery
							Dim image = If(isReaderUsed, reader.ReadVideoSample(), NImage.FromFile(files(i)))

							' Image will be null when reader has read all available frames
							If image Is Nothing Then
								Exit For
							End If
							face.Image = image
							biometricClient.PerformTask(task)

							Console.WriteLine(String.Format("[{0}] detection status: {1}", i, task.Status))
							If task.Status = NBiometricStatus.Ok Then
								PrintFaceAttributes(face)
							ElseIf task.Status <> NBiometricStatus.ObjectNotFound Then
								Console.WriteLine("Detection failed! Status: {0}", task.Status)
								If task.Error IsNot Nothing Then
									Throw task.Error
								End If
								Return -1
							End If
							image.Dispose()
						Next i

						If isReaderUsed Then
							reader.Stop()
						End If

						' Reset HasMoreSamples value since we finished loading images
						face.HasMoreSamples = False
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

End Class
