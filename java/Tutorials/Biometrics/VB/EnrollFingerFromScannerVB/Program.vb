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
		Console.WriteLine(Constants.vbTab & "{0} [image] [template]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[image]  - image filename to store finger image.")
		Console.WriteLine(Constants.vbTab & "[template] - filename to store finger template.")
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerExtraction,Devices.FingerScanners"

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient With {.UseDeviceManager = True}
				Using deviceManager = biometricClient.DeviceManager
					Using subject = New NSubject()
						Using finger = New NFinger()
							'set type of the device used
							deviceManager.DeviceTypes = NDeviceType.FingerScanner

							'initialize the NDeviceManager
							deviceManager.Initialize()

							Dim i As Integer

							'get count of connected devices
							Dim count As Integer = deviceManager.Devices.Count

							If count > 0 Then
								Console.WriteLine("found {0} finger scanners", count)
							Else
								Console.WriteLine("no finger scanners found, exiting ..." & Constants.vbLf)
								Return -1
							End If

							'list detected scanners
							If count > 1 Then
								Console.WriteLine("Please select finger scanner from the list: ")
							End If
							For i = 0 To count - 1
								Dim device As NDevice = deviceManager.Devices(i)
								Console.WriteLine("{0}) {1}", i + 1, device.DisplayName)
							Next i

							'finger scanner selection by user
							If count > 1 Then
								Console.Write("Please enter finger scanner index: ")
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

							'set the selected finger scanner as NBiometricClient Finger Scanner
							biometricClient.FingerScanner = CType(deviceManager.Devices(i), NFScanner)

							'add NFinger to NSubject
							subject.Fingers.Add(finger)

							'start capturing
							Dim status As NBiometricStatus = biometricClient.Capture(subject)
							If status <> NBiometricStatus.Ok Then
								Console.WriteLine("Failed to capture: " & status)
								Return -1
							End If

							'Set finger template size (recommended, for enroll to database, is large) (optional)
							biometricClient.FingersTemplateSize = NTemplateSize.Large

							'Create template from added finger image
							status = biometricClient.CreateTemplate(subject)
							If status = NBiometricStatus.Ok Then
								Console.WriteLine("Template extracted")

								' save image to file
								Using image = subject.Fingers(0).Image
									image.Save(args(0))
									Console.WriteLine("image saved successfully")
								End Using

								' save template to file
								File.WriteAllBytes(args(1), subject.GetTemplateBuffer().ToArray())
								Console.WriteLine("template saved successfully")
							Else
								Console.WriteLine(String.Format("Extraction failed: {0}", status))
								Return -1
							End If
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
