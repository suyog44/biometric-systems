Imports Microsoft.VisualBasic
Imports System
Imports System.IO

Imports Neurotec.Licensing
Imports Neurotec.Devices
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image1] [image2] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(Constants.vbTab & "[image1]    - image filename to store left iris image.")
		Console.WriteLine(Constants.vbTab & "[image2]    - image filename to store right iris image.")
		Console.WriteLine(Constants.vbTab & "[template]  - filename to store template.")

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.IrisExtraction,Devices.IrisScanners"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 3 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient With {.UseDeviceManager = True}
				Using deviceManager = biometricClient.DeviceManager
					Using subject = New NSubject()
						Using leftIris = New NIris()
							Using rightIris = New NIris()
								'set type of the device used
								deviceManager.DeviceTypes = NDeviceType.IrisScanner

								'initialize the NDeviceManager
								deviceManager.Initialize()

								Dim i As Integer

								'get count of connected devices
								Dim count As Integer = deviceManager.Devices.Count

								If count > 0 Then
									Console.WriteLine("found {0} iris scanners", count)
								Else
									Console.WriteLine("no iris scanners found, exiting ..." & Constants.vbLf)
									Return -1
								End If

								'list detected iris scanners
								If count > 1 Then
									Console.WriteLine("Please select iris scanners from the list: ")
								End If
								For i = 0 To count - 1
									Dim device As NDevice = deviceManager.Devices(i)
									Console.WriteLine("{0}) {1}", i + 1, device.DisplayName)
								Next i

								'iris scanners selection by user
								If count > 1 Then
									Console.Write("Please enter iris scanners index: ")
									Dim line As String = Console.ReadLine()
									If line Is Nothing Then
										Throw New ApplicationException("Nothing read from standard input")
									End If
									i = Integer.Parse(line)
									If i > count OrElse i < 1 Then
										Console.WriteLine("Incorrect index provided, exiting ...")
										Return -1
									End If
								End If
								i -= 1

								'set the selected camera as NBiometricClient Iris Scanner
								biometricClient.IrisScanner = CType(deviceManager.Devices(i), NIrisScanner)

								Console.WriteLine("started capturing left iris from {0} ..", biometricClient.IrisScanner.DisplayName)

								' Set NIris position
								leftIris.Position = NEPosition.Left

								' add NIris to NSubject
								subject.Irises.Add(leftIris)

								' Capture left iris
								Dim status As NBiometricStatus = biometricClient.Capture(subject)
								Console.WriteLine(If(status = NBiometricStatus.Ok, "Captured", String.Format("Capturing failed: {0}", status)))

								Console.WriteLine("started capturing right iris from {0} ..", biometricClient.IrisScanner.DisplayName)

								' Set NIris position
								rightIris.Position = NEPosition.Right

								' add NIris to NSubject
								subject.Irises.Add(rightIris)

								' Capture right iris
								status = biometricClient.Capture(subject)
								Console.WriteLine(If(status = NBiometricStatus.Ok, "Captured", String.Format("Capturing failed: {0}", status)))

								'Set iris template size (recommended, for enroll to database, is large) (optional)
								biometricClient.IrisesTemplateSize = NTemplateSize.Large

								'Create template from added iris image
								status = biometricClient.CreateTemplate(subject)
								Console.WriteLine(If(status = NBiometricStatus.Ok, "Template extracted", String.Format("Extraction failed: {0}", status)))

								' save first iris image to file
								Using image = subject.Irises(0).Image
									image.Save(args(0))
									Console.WriteLine("image saved successfully")
								End Using

								' save second iris image to file
								Using image = subject.Irises(1).Image
									image.Save(args(1))
									Console.WriteLine("image saved successfully")
								End Using

								' save template to file
								File.WriteAllBytes(args(2), subject.GetTemplateBuffer().ToArray())
								Console.WriteLine("template saved successfully")

							End Using
						End Using
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
