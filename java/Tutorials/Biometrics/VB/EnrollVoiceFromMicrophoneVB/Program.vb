Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Licensing
Imports Neurotec.Devices
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports System.IO

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [template] [voice]", TutorialUtils.GetAssemblyName())
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "[template] - filename to store sound template.")
		Console.WriteLine(Constants.vbTab & "[voice] - filename to store voice audio file.")
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "example: {0} template.dat voice.wav", TutorialUtils.GetAssemblyName())
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Devices.Microphones,Biometrics.VoiceExtraction"

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
						Using voice = New NVoice()
							'set type of the device used
							deviceManager.DeviceTypes = NDeviceType.Microphone

							'initialize the NDeviceManager
							deviceManager.Initialize()

							Dim i As Integer

							'get count of connected devices
							Dim count As Integer = deviceManager.Devices.Count

							If count > 0 Then
								Console.WriteLine("found {0} microphones", count)
							Else
								Console.WriteLine("no microphones found, exiting ..." & Constants.vbLf)
								Return -1
							End If

							'list detected microphones
							If count > 1 Then
								Console.WriteLine("Please select microphones from the list: ")
							End If
							For i = 0 To count - 1
								Dim device As NDevice = deviceManager.Devices(i)
								Console.WriteLine("{0}) {1}", i + 1, device.DisplayName)
							Next i

							'microphones selection by user
							If count > 1 Then
								Console.Write("Please enter microphones index: ")
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

							'set the selected microphones as NBiometricClient Voice Capturing Device
							biometricClient.VoiceCaptureDevice = CType(deviceManager.Devices(i), NMicrophone)

							Console.WriteLine("recording from {0}.", biometricClient.VoiceCaptureDevice.DisplayName)

							'define that the voice source will be a stream
							voice.CaptureOptions = NBiometricCaptureOptions.Stream

							'add NVoice to NSubject
							subject.Voices.Add(voice)

							'create capturing task
							Dim task As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.Segment, subject)

							'Perform task
							biometricClient.PerformTask(task)
							Dim status As NBiometricStatus = task.Status

							If status = NBiometricStatus.Ok Then
								' save voice to file
								Using sound = subject.Voices(1).SoundBuffer
									sound.Save(args(1))
									Console.WriteLine("voice saved successfully")
								End Using

								' save template to file
								File.WriteAllBytes(args(0), subject.GetTemplateBuffer().ToArray())
								Console.WriteLine("template saved successfully")
							Else
								Console.WriteLine("Failed to capture: " & status)
								If task.Error IsNot Nothing Then
									Throw task.Error
								End If
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
