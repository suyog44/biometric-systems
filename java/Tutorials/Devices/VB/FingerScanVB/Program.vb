Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [imageCount]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "imageCount - count of fingerprint images to be scanned")
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const components As String = "Devices.FingerScanners"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", components))
			End If

			Dim imageCount As Integer = Integer.Parse(args(0))
			If imageCount = 0 Then
				Console.WriteLine("no frames will be captured as frame count is not specified")
			End If

			Using deviceManager = New NDeviceManager With {.DeviceTypes = NDeviceType.FingerScanner, .AutoPlug = True}
				deviceManager.Initialize()
				Console.WriteLine("device manager created. found scanners: {0}", deviceManager.Devices.Count)

				For Each scanner As NFScanner In deviceManager.Devices
					Console.WriteLine("found scanner {0}, capturing fingerprints", scanner.DisplayName)

					For i As Integer = 0 To imageCount - 1
						Console.Write(vbTab & "image {0} of {1}. please put your fingerprint on scanner:", i + 1, imageCount)
						Dim filename As String = String.Format("{0}_{1:d4}.jpg", scanner.DisplayName, i)
						Using biometric = New NFinger()
							biometric.Position = NFPosition.Unknown
							Dim biometricStatus = scanner.Capture(biometric, -1)
							If biometricStatus <> NBiometricStatus.Ok Then
								Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus)
								Continue For
							End If
							biometric.Image.Save(filename)
							Console.WriteLine(" image captured")
						End Using
					Next i
				Next scanner
				Console.WriteLine("done")
			End Using
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(components)
		End Try
	End Function
End Class
