
Imports Neurotec.Licensing
Imports Microsoft.VisualBasic
Imports System

Imports Neurotec.Devices
Imports Neurotec.Sound

Friend Class Program

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [bufferCount]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(vbTab & "bufferCount - number of sound buffers to capture from each microphone to current directory")
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const components As String = "Devices.Microphones"

		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Try
			If (Not NLicense.ObtainComponents("/local", 5000, components)) Then
				Throw New NotActivatedException(String.Format("Could not obtain licenses for components: {0}", components))
			End If

			Dim bufferCount As Integer = Integer.Parse(args(0))

			If bufferCount = 0 Then
				Console.WriteLine("no sound buffers will be captured as sound buffer count is not specified")
			End If

			Using deviceManager = New NDeviceManager With {.DeviceTypes = NDeviceType.Microphone, .AutoPlug = True}
				deviceManager.Initialize()
				Console.WriteLine("device manager created. found microphones: {0}", deviceManager.Devices.Count)

				For Each microphone As NMicrophone In deviceManager.Devices
					Console.Write("found microphone {0}", microphone.DisplayName)

					microphone.StartCapturing()

					If bufferCount > 0 Then
						Console.WriteLine(", capturing")
						For i As Integer = 0 To bufferCount - 1
							Using soundSample As NSoundBuffer = microphone.GetSoundSample()
								Console.WriteLine("sample buffer received. sample rate: {0}, sample length: {1}", soundSample.SampleRate, soundSample.Length)
							End Using
							Console.Write(" ... ")
						Next i
						Console.Write(" done")
						Console.WriteLine()
					End If
					microphone.StopCapturing()
				Next microphone
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
