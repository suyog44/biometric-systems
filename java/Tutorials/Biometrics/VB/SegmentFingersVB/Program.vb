Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Licensing

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(Constants.vbTab & "{0} [image] [position] <optional: missing positions>", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "[image]             - image containing fingerprints")
		Console.WriteLine(Constants.vbTab & "[position]          - fingerpints position in provided image")
		Console.WriteLine(Constants.vbTab & "[missing positions] - one or more NFPosition value of missing fingers")
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "valid positions:")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & "PlainRightFourFingers = 13, PlainLeftFourFingers = 14, PlainThumbs = 15")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & "RightThumb = 1, RightIndex = 2, RightMiddle = 3, RightRing = 4, RightLittle = 5")
		Console.WriteLine(Constants.vbTab + Constants.vbTab & "LeftThumb = 6, LeftIndex = 7, LeftMiddle = 8, LeftRing = 9, LeftLittle = 10")
		Console.WriteLine()
		Console.WriteLine(Constants.vbTab & "example: {0} image.png 15", TutorialUtils.GetAssemblyName())
		Console.WriteLine(Constants.vbTab & "example: {0} image.png 13 2 3", TutorialUtils.GetAssemblyName())
		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		Const Components As String = "Biometrics.FingerSegmentation,Biometrics.FingerExtraction"

		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			' Obtain license
			If (Not NLicense.ObtainComponents("/local", 5000, Components)) Then
				Throw New ApplicationException(String.Format("Could not obtain licenses for components: {0}", Components))
			End If

			Using biometricClient = New NBiometricClient()
				Using subject = New NSubject()
					Using finger = New NFinger()
						' Read finger image from file and add it to NFinger object
						finger.FileName = args(0)

						' Set finger position
						finger.Position = CType(Integer.Parse(args(1)), NFPosition)

						' Add finger image from file to NSubject
						subject.Fingers.Add(finger)

						' Set missing finger positions
						For i As Integer = 2 To args.Length - 1
							subject.MissingFingers.Add(CType(Integer.Parse(args(i)), NFPosition))
						Next i

						' Create Segment and Create Template task
						Dim task As NBiometricTask = biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate, subject)

						' Perform task
						biometricClient.PerformTask(task)

						' Get task status
						Dim status As NBiometricStatus = task.Status
						If status = NBiometricStatus.Ok Then
							' Check if wrong hand is detected
							If finger.WrongHandWarning Then
								Console.WriteLine("Warning: possibly wrong hand.")
							End If
							Dim segmentCount As Integer = subject.Fingers.Count
							Console.WriteLine("Found {0} segements", segmentCount - 1)

							For i As Integer = 1 To segmentCount - 1
								If subject.Fingers(i).Status = NBiometricStatus.Ok Then
									Console.Write(Constants.vbTab & " {0}: ", subject.Fingers(i).Position)
									subject.Fingers(i).Image.Save(subject.Fingers(i).Position & ".png")
									Console.WriteLine("Saving image...")
								Else
									Console.WriteLine(Constants.vbTab & " {0}: {1}", subject.Fingers(i).Position, subject.Fingers(i).Status)
								End If
							Next i
						Else
							Console.WriteLine("Segementation failed. Status: {0}", status)
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
