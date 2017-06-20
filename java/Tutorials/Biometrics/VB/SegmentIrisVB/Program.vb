Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [input image] [output image]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation"

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
					Using iris = New NIris()
						' Read iris image from file and add it to NIris object
						iris.FileName = args(0)

						' Set iris image type;
						iris.ImageType = NEImageType.CroppedAndMasked

						' Read iris image from file and add it NSubject
						subject.Irises.Add(iris)

						' Create segmentation task
						Dim task As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Segment, subject)

						' Perform task
						biometricClient.PerformTask(task)
						Dim status As NBiometricStatus = task.Status
						If status = NBiometricStatus.Ok Then
							For Each attributes In subject.Irises(0).Objects
								Console.WriteLine("overall quality" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.Quality)
								Console.WriteLine("GrayScaleUtilisation" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.GrayScaleUtilisation)
								Console.WriteLine("Interlace" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.Interlace)
								Console.WriteLine("IrisPupilConcentricity" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.IrisPupilConcentricity)
								Console.WriteLine("IrisPupilContrast" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.IrisPupilContrast)
								Console.WriteLine("IrisRadius" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.IrisRadius)
								Console.WriteLine("IrisScleraContrast" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.IrisScleraContrast)
								Console.WriteLine("MarginAdequacy" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.MarginAdequacy)
								Console.WriteLine("PupilBoundaryCircularity" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.PupilBoundaryCircularity)
								Console.WriteLine("PupilToIrisRatio" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.PupilToIrisRatio)
								Console.WriteLine("Sharpness" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.Sharpness)
								Console.WriteLine("UsableIrisArea" & Constants.vbTab & "{0}" & Constants.vbLf, attributes.UsableIrisArea)
							Next attributes
							Using image = subject.Irises(1).Image
								image.Save(args(1))
							End Using
						Else
							Console.WriteLine("Segmentation failed. Status {0}", status)
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
