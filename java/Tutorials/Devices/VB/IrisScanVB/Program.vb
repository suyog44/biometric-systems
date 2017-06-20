Imports Microsoft.VisualBasic
Imports System

Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Licensing

Friend Class Program
	Shared Function Main(ByVal args() As String) As Integer
		Const components As String = "Devices.IrisScanners"

		TutorialUtils.PrintTutorialHeader(args)

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", components))
			End If

			Using deviceManager = New NDeviceManager With {.DeviceTypes = NDeviceType.IrisScanner, .AutoPlug = True}
				deviceManager.Initialize()
				Console.WriteLine("device manager created. found scanners: {0}", deviceManager.Devices.Count)

				For Each scanner As NIrisScanner In deviceManager.Devices
					Console.Write("found scanner {0}", scanner.DisplayName)

					Console.Write(vbTab & "capturing right iris: ")
					Using rightIrisBiometric = New NIris()
						rightIrisBiometric.Position = NEPosition.Right
						Dim biometricStatus = scanner.Capture(rightIrisBiometric, -1)
						If biometricStatus <> NBiometricStatus.Ok Then
							Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus)
							Continue For
						End If
						Dim filename As String = String.Format("{0}_iris_right.jpg", scanner.DisplayName)
						rightIrisBiometric.Image.Save(filename)
						Console.WriteLine("done")
					End Using

					Console.Write(vbTab & "capturing left eye: ")
					Using leftIrisBiometric = New NIris()
						leftIrisBiometric.Position = NEPosition.Left
						Dim biometricStatus = scanner.Capture(leftIrisBiometric, -1)
						If biometricStatus <> NBiometricStatus.Ok Then
							Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus)
							Continue For
						End If
						Dim filename As String = String.Format("{0}_iris_left.jpg", scanner.DisplayName)
						leftIrisBiometric.Image.Save(filename)
						Console.WriteLine("done")
					End Using
				Next scanner
			End Using
			Console.WriteLine("done")

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			NLicense.ReleaseComponents(components)
		End Try
	End Function
End Class
